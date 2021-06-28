using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InformationBoard.Model
{
    public class Flight : INotifyPropertyChanged
    {
        private int type;
        private int direction;
        private DateTime date;
        private string city;

        public int Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged("Type");
            }
        }

        public int Direction
        {
            get { return direction; }
            set
            {
                direction = value;
                OnPropertyChanged("Direction");
            }
        }

        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        public string City
        {
            get { return city; }
            set
            {
                city = value;
                OnPropertyChanged("City");
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
