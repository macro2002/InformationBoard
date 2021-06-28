using InformationBoard.Function;
using InformationBoard.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace InformationBoard.ViewModel
{
    public class LastFlightViewModel : INotifyPropertyChanged
    {
        private Thickness margin;
        private double opacity;
        private Flight flight;

        public Thickness Margin
        {
            get { return margin; }
            set
            {
                margin = value;
                OnPropertyChanged("Margin");
            }
        }
        public double Opacity
        {
            get { return opacity; }
            set
            {
                opacity = value;
                OnPropertyChanged("Opacity");
            }
        }
        public Flight Flight
        {
            get { return flight; }
            set
            {
                flight = value;
                OnPropertyChanged("Flight");
            }
        }

        public void ChangeFlight(Flight flight)
        {
            Flight = flight;
            AnimationAsync();
            if (flight.Direction == 0)
            {
                VM.Main.ArrivalsStats.AddFlight(flight, Simulation.DateEmulation);
            }
            else
            {
                VM.Main.DepartureStats.AddFlight(flight, Simulation.DateEmulation);
            }
        }

        private async void AnimationAsync()
        {
            await Task.Run(() => Animation());
        }

        private object locking = new object();
        private void Animation()
        {
            lock (locking)
            {
                while (Opacity < 0)
                {
                    switch (Simulation.Speed)
                    {
                        case 1:
                            Thread.Sleep(500);
                            break;
                        case 10:
                            Thread.Sleep(100);
                            break;
                        case 100:
                            Thread.Sleep(50);
                            break;
                        case 1000:
                            Thread.Sleep(1);
                            break;
                        case 10000:
                            break;
                    }
                    if (Opacity <= 0)
                    {
                        Opacity = Opacity - 0.1;
                    }
                }

                int leftMargin = 375;
                Margin = new Thickness(leftMargin, 0, 0, 0);
                Opacity = 1;
                while (leftMargin > 0)
                {
                    leftMargin = leftMargin - 1;
                    switch (Simulation.Speed)
                    {
                        case 1:
                            Thread.Sleep(5);
                            break;
                        case 10:
                            Thread.Sleep(4);
                            break;
                        case 100:
                            Thread.Sleep(3);
                            break;
                        case 1000:
                            Thread.Sleep(2);
                            break;
                        case 10000:
                            leftMargin = leftMargin - 74;
                            Thread.Sleep(1);
                            break;
                    }
                    Margin = new Thickness(leftMargin, 0, 0, 0);
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
