using System.Windows;

namespace Snake
{
    public partial class MainWindow : Window
    {
        public MainWindow(string serverAdress, InitialiseResponseBody initialiseconnectResponse)
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(serverAdress, initialiseconnectResponse, this);
        }
    }
}
