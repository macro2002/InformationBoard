using InformationBoard.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace InformationBoard.ViewModel
{
    public class StatisticsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<FlightStatistics> Flights = new ObservableCollection<FlightStatistics>();

        private string name;
        private int lastFlight;
        private int count24;
        private int total;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("name");
            }
        }
        public int LastFlight
        {
            get { return lastFlight; }
            set
            {
                lastFlight = value;
                OnPropertyChanged("LastFlight");
            }
        }
        public int Count24
        {
            get { return count24; }
            set
            {
                count24 = value;
                OnPropertyChanged("Count24");
            }
        }
        public int Total
        {
            get { return total; }
            set
            {
                total = value;
                OnPropertyChanged("Total");
            }
        }

        public StatisticsViewModel(int direction)
        {
            if(direction == 0)
            {
                Name = "Количество пассажиров (Прилет)";
            }
            else
            {
                Name = "Количество пассажиров (Вылет)";
            }
        }

        public void AddFlight(Flight flight, DateTime date)
        {
            int passengers = 0;
            Random rand = new Random();
            switch (flight.Type)
            {
                case 1:
                    passengers = rand.Next(5, 20);
                    break;
                case 2:
                    passengers = rand.Next(10, 50);
                    break;
                case 3:
                    passengers = rand.Next(15, 70);
                    break;
                case 4:
                    passengers = rand.Next(20, 95);
                    break;
                case 5:
                    passengers = rand.Next(25, 120);
                    break;
            }
            LastFlight = passengers;
            FlightStatistics stat = new FlightStatistics { Type = flight.Type, Passengers = passengers, Direction = flight.Direction, Date = date, City = flight.City };   
            Flights.Add(stat);
            VM.Main.Chart.AddData(stat);
            AnimationAsync(date);
        }

        private async void AnimationAsync(DateTime date)
        {
            Task count24 = Task.Run(() => AnimationCount24(date));
            Task total = Task.Run(() => AnimationTotal());
            await Task.WhenAll(new[] { count24, total });
        }

        private object lockCount24 = new object();
        private void AnimationCount24(DateTime date)
        {
            lock (lockCount24)
            {
                int sum = Flights.Where(f => f.Date > date.AddDays(-1)).Sum(f => f.Passengers);
                if (Count24 < sum)
                {
                    while (Count24 < sum)
                    {
                        Count24 = Count24 + 1;
                        Thread.Sleep(10);
                    }
                }
                else
                {
                    while (Count24 > sum)
                    {
                        Count24 = Count24 - 1;
                        Thread.Sleep(10);
                    }
                }
            }
        }

        private object lockTotal = new object();
        private void AnimationTotal()
        {
            lock (lockTotal)
            {
                int sum = Flights.Sum(f => f.Passengers);
                {
                    while (Total < sum)
                    {
                        Total = Total + 1;
                        Thread.Sleep(10);
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
