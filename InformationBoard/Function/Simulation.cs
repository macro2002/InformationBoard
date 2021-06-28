using InformationBoard.Model;
using InformationBoard.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InformationBoard.Function
{
    public class Simulation
    {
        public static int Speed;
        public static int Tick;
        public static DateTime DateEmulation;
        private static ObservableCollection<Flight> Flights;

        public static async void Start()
        {
            Speed = 1;
            DateEmulation = DateTime.Now;
            Flights = Data.InitializeData();
            await Task.Run(() => Emulation());
        }

        private static void Emulation()
        {
            Tick = 0;
            while (true)
            {
                Thread.Sleep(1);
                if (Tick >= (10000/ Speed))
                {
                    DateEmulation = DateEmulation.AddSeconds(10);
                    CheckFlightAsync();
                    Tick = 0;
                }
                else
                {
                    Tick++;
                }
            }
        }

        private static async void CheckFlightAsync()
        {
            await Task.Run(() => CheckFlight());
        }

        private static void CheckFlight()
        {
            Flight flight = Flights.FirstOrDefault(t => t.Date.Minute == DateEmulation.Minute);
            if(flight != null)
            {
                if(VM.Main.LastFlight.Flight != flight)
                {
                    VM.Main.LastFlight.ChangeFlight(flight);
                }
            }
        }

        public static int ChangeSpeed(int direction)
        {
            switch (Speed)
            {
                case 1:
                    if (direction == 0)
                    {
                        //Скорость без изменения(Минимальная скорость)
                    }
                    else
                    {
                        Speed = 10;
                    }
                    break;
                case 10:
                    if (direction == 0)
                    {
                        Speed = 1;
                    }
                    else
                    {
                        Speed = 100;
                    }
                    break;
                case 100:
                    if (direction == 0)
                    {
                        Speed = 10;
                    }
                    else
                    {
                        Speed = 1000;
                    }
                    break;
                case 1000:
                    if (direction == 0)
                    {
                        Speed = 100;
                    }
                    else
                    {
                        Speed = 10000;
                    }
                    break;
                case 10000:
                    if (direction == 0)
                    {
                        Speed = 1000;
                    }
                    else
                    {
                        //Скорость без изменения(Максимальная скорость)
                    }
                    break;
            }
            Tick = 0;
            return Speed;
        }
    }
}
