using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Snake
{
    class ConnectWindowViewModel : INotifyPropertyChanged
    {
        private InitialiseResponseBody initialiseconnectResponse;
        private MainWindow mainWindow;
        private RelayCommand acceptClick;
        private RelayCommand cancelClick;

        private string serverAdress = "https://localhost:5001";

        public string ServerAdress
        {
            get { return serverAdress; }
            set
            {
                serverAdress = value;
                OnPropertyChanged("ServerAdress");
            }
            
        }

        public RelayCommand AcceptClick
        {
            get { return acceptClick ?? (acceptClick = new RelayCommand(obj => Connect())); }
        }
        
        public RelayCommand CancelClick
        {
            get { return cancelClick ?? (cancelClick = new RelayCommand(obj => Exit())); }
        }

        public void Connect()
        {
            try
            {
                WebRequest webRequest = WebRequest.Create(ServerAdress + "/initialise");
                try
                {
                    StreamReader streamReader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                    initialiseconnectResponse = JsonConvert.DeserializeObject<InitialiseResponseBody>(streamReader.ReadToEnd());
                    streamReader.Close();

                    mainWindow = new MainWindow(ServerAdress, initialiseconnectResponse);
                    Application.Current.MainWindow = mainWindow;
                    Application.Current.Windows[0].Close();
                    mainWindow.Show();
                }
                catch (Exception streamExeption)
                {
                    MessageBox.Show(streamExeption.Message + "\n" + ServerAdress, "Stream exeption");
                    Application.Current.Shutdown();
                }
            }
            catch (Exception requestExeption)
            {
                MessageBox.Show(requestExeption.Message + "\n" + ServerAdress + "\n" + initialiseconnectResponse, "Request exeption");
                Application.Current.Shutdown();
            }
        }

        private void Exit()
        {
            Application.Current.Shutdown();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
