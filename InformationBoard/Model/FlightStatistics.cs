namespace InformationBoard.Model
{
    public class FlightStatistics : Flight
    {
        private int passengers;

        public int Passengers
        {
            get { return passengers; }
            set
            {
                passengers = value;
                OnPropertyChanged("Passengers");
            }
        }
    }
}
