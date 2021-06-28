using InformationBoard.ViewModel;
using System.Windows.Controls;

namespace InformationBoard.View
{
    /// <summary>
    /// Логика взаимодействия для StatisticsControl.xaml
    /// </summary>
    public partial class StatisticsControl : UserControl
    {
        public StatisticsControl(int direction)
        {
            InitializeComponent();
            Initialize(direction);
        }

        public void Initialize(int direction)
        {
            if (direction == 0)
            {
                DataContext = VM.Main.ArrivalsStats;
            }
            else
            {
                DataContext = VM.Main.DepartureStats;
            }

        }
    }
}
