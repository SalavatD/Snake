using System.Windows;

namespace Snake
{
    public partial class ConnectWindow : Window
    {
        public ConnectWindow()
        {
            InitializeComponent();
            DataContext = new ConnectWindowViewModel();
        }
    }
}
