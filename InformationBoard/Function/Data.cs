using InformationBoard.Model;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace InformationBoard.Function
{
    public class Data
    {
        private static string DataBase = "base.txt";

        public static ObservableCollection<Flight> InitializeData()
        {
            ObservableCollection<Flight> flights = new ObservableCollection<Flight>();
            using (var read = new StreamReader(DataBase, Encoding.UTF8))
            {
                string line;
                while ((line = read.ReadLine()) != null)
                {
                    string[] strArray = line.Split(';');
                    Flight flight = new Flight();
                    if (!string.IsNullOrEmpty(strArray[0]))
                        flight.Type = Convert.ToInt32(strArray[0]);
                    if (!string.IsNullOrEmpty(strArray[1]))
                        flight.Direction = Convert.ToInt32(strArray[1]);
                    if (!string.IsNullOrEmpty(strArray[2]))
                        flight.Date = Convert.ToDateTime(strArray[2]);
                    if (!string.IsNullOrEmpty(strArray[3]))
                        flight.City = strArray[3];
                    flights.Add(flight);
                }
            }
            return flights;
        }
    }
}
