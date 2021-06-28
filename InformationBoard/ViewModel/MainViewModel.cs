using InformationBoard.Function;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InformationBoard.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private int speed;
        private LastFlightViewModel lastFlight;
        private StatisticsViewModel arrivalsStats;
        private StatisticsViewModel departureStats;
        private ChartViewModel chart;

        public int Speed
        {
            get { return speed; }
            set
            {
                speed = value;
                OnPropertyChanged("speed");
            }
        }
        public LastFlightViewModel LastFlight
        {
            get { return lastFlight; }
            set
            {
                lastFlight = value;
                OnPropertyChanged("LastFlight");
            }
        }
        public StatisticsViewModel ArrivalsStats
        {
            get { return arrivalsStats; }
            set
            {
                arrivalsStats = value;
                OnPropertyChanged("ArrivalsStats");
            }
        }
        public StatisticsViewModel DepartureStats
        {
            get { return departureStats; }
            set
            {
                departureStats = value;
                OnPropertyChanged("DepartureStats");
            }
        }
        public ChartViewModel Chart
        {
            get { return chart; }
            set
            {
                chart = value;
                OnPropertyChanged("Chart");
            }
        }

        public MainViewModel()
        {
            Speed = 1;
            LastFlight = new LastFlightViewModel();
            ArrivalsStats = new StatisticsViewModel(0);
            DepartureStats = new StatisticsViewModel(1);
            Chart = new ChartViewModel(DateTime.Now);
        }

        public void ChangeSpeed(int direction)
        {
            Speed = Simulation.ChangeSpeed(direction);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
