using InformationBoard.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace InformationBoard.ViewModel
{
    public class ChartViewModel : INotifyPropertyChanged
    {
        private double chartWidth;
        private double chartHeight;
        private ObservableCollection<ChartStat> chartStats;

        public double ChartWidth
        {
            get { return chartWidth; }
            set
            {
                chartWidth = value;
                OnPropertyChanged("ChartWidth");
            }
        }
        public double ChartHeight
        {
            get { return chartHeight; }
            set
            {
                chartHeight = value;
                OnPropertyChanged("ChartHeight");
            }
        }
        public ObservableCollection<ChartStat> ChartStats
        {
            get { return chartStats; }
            set
            {
                chartStats = value;
                OnPropertyChanged("ChartStats");
            }
        }

        public ChartViewModel(DateTime date)
        {
            ChartStats = new ObservableCollection<ChartStat>();
            DateTime chartDate = new DateTime(date.Year, date.Month, date.AddDays(-1).Day, date.AddHours(1).Hour, 0, 0);
            for(int i = 0; i < 25; i++)
            {
                ChartStats.Add(new ChartStat { Date = chartDate} );
                chartDate = chartDate.AddHours(1);
            }
        }

        public void ItemSize()
        {
            if(ChartStats != null)
            {
                double itemWidth = ChartWidth / 27;
                foreach(var item in ChartStats)
                {
                    item.Width = itemWidth;
                    item.Height = ChartHeight;
                }
                ChartStats.Last().Width = 0;
            }
        }

        public void AddData(FlightStatistics stat)
        {
            ChartStat current = ChartStats.FirstOrDefault(c => c.Date == new DateTime(stat.Date.Year, stat.Date.Month, stat.Date.Day, stat.Date.Hour, 0, 0));
            if (current != null)
            {
                if(current.Date != ChartStats.Last().Date)
                {
                    if (stat.Direction == 0)
                    {
                        current.Arrivals = current.Arrivals + stat.Passengers;
                    }
                    else
                    {
                        current.Departure = current.Departure + stat.Passengers;
                    }
                    AnimationAsync(current, stat.Direction);
                }
                else
                {
                    AddHourAsync(stat.Date);
                }
            }
        }

        private async void AnimationAsync(ChartStat current, int direction)
        {
            Task column = Task.Run(() => AnimationColumn(current, direction));
            Task total = Task.Run(() => AnimationTotal(current, direction));
            await Task.WhenAll(new[] { column, total });
        }

        private void AnimationColumn(ChartStat current, int direction)
        {
            if(direction == 0)
            {
                while (current.ArrivalsProcent < current.Arrivals / Convert.ToDouble(1000))
                {
                    current.ArrivalsProcent = current.ArrivalsProcent + 0.01;
                    Thread.Sleep(25);
                }
            }
            else
            {
                while (current.DepartureProcent < current.Departure / Convert.ToDouble(1000))
                {
                    current.DepartureProcent = current.DepartureProcent + 0.01;
                    Thread.Sleep(25);
                }
            }
        }

        private void AnimationTotal(ChartStat current, int direction)
        {
            if (direction == 0)
            {
                while (current.ArrivalsView < current.Arrivals)
                {
                    current.ArrivalsView = current.ArrivalsView + 1;
                    Thread.Sleep(10);
                }
            }
            else
            {
                while (current.DepartureView < current.Departure)
                {
                    current.DepartureView = current.DepartureView + 01;
                    Thread.Sleep(10);
                }
            }
        }

        private async void AddHourAsync(DateTime date)
        {
            await Task.Run(() => AddHour(date));
        }

        private void AddHour(DateTime date)
        {
            ChartStat first = ChartStats.First();
            double width = first.Width;
            ChartStat last = ChartStats.Last();
            while (first.Width > 0)
            {
                first.Width = first.Width - (width / 10);
                last.Width = last.Width + (width / 10);
                if(first.Width < (width / 10))
                {
                    first.Width = 0;
                    last.Width = width;
                }
                Thread.Sleep(10);
            }
            var stat = new ChartStat { Date = new DateTime(date.Year, date.Month, date.Day, date.AddHours(1).Hour, 0, 0), Height = last.Height };
            Application.Current.Dispatcher.BeginInvoke(new Action(delegate ()
            {    
                chartStats.Add(stat);
                chartStats.Remove(first);
            }));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
