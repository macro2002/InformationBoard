using InformationBoard.Function;
using InformationBoard.View;
using InformationBoard.ViewModel;
using System.Windows;

namespace InformationBoard
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            viewModel = VM.Main = new MainViewModel();
            DataContext = viewModel;
            LastFlight.Content = new LastFlightControl();
            ArrivalsStats.Content = new StatisticsControl(0);
            DepartureStats.Content = new StatisticsControl(1);
            Chart.Content = new ChartControl();
            Simulation.Start();
        }

        private void PrevSpeed_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ChangeSpeed(0);
        }

        private void NextSpeed_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ChangeSpeed(1);
        }
    }
}
