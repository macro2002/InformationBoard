using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InformationBoard.Model
{
    public class ChartStat : INotifyPropertyChanged
    {
        private DateTime date;
        private int arrivals;
        private int arrivalsView;
        private double arrivalsProcent;
        private int departure;
        private int departureView;
        private double departureProcent;

        private double width;
        private double height;

        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }
        public int Arrivals
        {
            get { return arrivals; }
            set
            {
                arrivals = value;
                OnPropertyChanged("Arrivals");
            }
        }
        public int ArrivalsView
        {
            get { return arrivalsView; }
            set
            {
                arrivalsView = value;
                OnPropertyChanged("ArrivalsView");
            }
        }
        public double ArrivalsProcent
        {
            get { return arrivalsProcent; }
            set
            {
                arrivalsProcent = value;
                OnPropertyChanged("ArrivalsProcent");
            }
        }
        public int Departure
        {
            get { return departure; }
            set
            {
                departure = value;
                OnPropertyChanged("Departure");
            }
        }
        public int DepartureView
        {
            get { return departureView; }
            set
            {
                departureView = value;
                OnPropertyChanged("DepartureView");
            }
        }
        public double DepartureProcent
        {
            get { return departureProcent; }
            set
            {
                departureProcent = value;
                OnPropertyChanged("DepartureProcent");
            }
        }
        public double Width
        {
            get { return width; }
            set
            {
                width = value;
                OnPropertyChanged("Width");
            }
        }
        public double Height
        {
            get { return height; }
            set
            {
                height = value;
                OnPropertyChanged("Height");
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
