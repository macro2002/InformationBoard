using InformationBoard.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace InformationBoard.View
{
    /// <summary>
    /// Логика взаимодействия для LastFlightControl.xaml
    /// </summary>
    public partial class LastFlightControl : UserControl
    {
        public LastFlightControl()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            DataContext = VM.Main.LastFlight;
        }
    }
}
