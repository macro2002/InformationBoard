using InformationBoard.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace InformationBoard.View
{
    /// <summary>
    /// Логика взаимодействия для ChartControl.xaml
    /// </summary>
    public partial class ChartControl : UserControl
    {
        public ChartControl()
        {
            InitializeComponent();
            Initialize();
        }

        public void Initialize()
        {
            DataContext = VM.Main.Chart;
        }

        private void Chart_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            VM.Main.Chart.ChartWidth = e.NewSize.Width;
            VM.Main.Chart.ChartHeight = e.NewSize.Height;
            VM.Main.Chart.ItemSize();
        }
    }
}
