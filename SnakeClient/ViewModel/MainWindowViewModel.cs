using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Snake
{
    class MainWindowViewModel
    {
        private int gameElementWidth = 10;
        private string serverAdress;
        private ResponseBody responseBody;
        private Point currentSnake = new Point(0, 0);
        private MainWindow view;
        private MainWindowModel model;

        public MainWindowViewModel
            (
            string serverAdress,
            InitialiseResponseBody initialiseconnectResponse,
            MainWindow view
            )
        {
            this.serverAdress = serverAdress;
            this.view = view;
            model = new MainWindowModel();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(TimerTick);
            timer.Interval = new TimeSpan(1000 * 2 * initialiseconnectResponse.timeUntilNextTurnMilliseconds);
            timer.Start();

            view.KeyDown += new KeyEventHandler(OnButtonKeyDown);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            responseBody = model.GetState(serverAdress);

            if (Math.Abs(currentSnake.X - responseBody.currentPosition.X) > 0 ||
                Math.Abs(currentSnake.Y - responseBody.currentPosition.Y) > 0)
            {
                Application.Current.MainWindow.Width = responseBody.gameBoardSize.width * gameElementWidth + 25;
                Application.Current.MainWindow.Height = responseBody.gameBoardSize.height * gameElementWidth + 50;
                view.gameField.Height = responseBody.gameBoardSize.height * gameElementWidth;
                view.gameField.Width = responseBody.gameBoardSize.width * gameElementWidth;

                Paint();
            }
        }

        private void Paint()
        {
            view.gameField.Children.Clear();
            foreach (System.Drawing.PointF i in responseBody.snake)
            {
                Ellipse snakeElement = new Ellipse
                {
                    Fill = Brushes.DarkGreen,
                    Width = gameElementWidth,
                    Height = gameElementWidth
                };
                Canvas.SetTop(snakeElement, i.Y * gameElementWidth);
                Canvas.SetLeft(snakeElement, i.X * gameElementWidth);
                view.gameField.Children.Add(snakeElement);
            }
            foreach (System.Drawing.PointF i in responseBody.food)
            {
                Ellipse newFood = new Ellipse
                {
                    Fill = Brushes.DarkRed,
                    Width = gameElementWidth,
                    Height = gameElementWidth
                };
                Canvas.SetTop(newFood, i.Y * gameElementWidth);
                Canvas.SetLeft(newFood, i.X * gameElementWidth);
                view.gameField.Children.Add(newFood);
            }
        }

        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            RequestBody requestBody = null;
            switch (e.Key)
            {
                case Key.Down:
                    requestBody = new RequestBody("Bottom");
                    break;
                case Key.Up:
                    requestBody = new RequestBody("Top");
                    break;
                case Key.Left:
                    requestBody = new RequestBody("Left");
                    break;
                case Key.Right:
                    requestBody = new RequestBody("Right");
                    break;
                default:
                    break;
            }
            if (requestBody != null)
                model.PostDirection(requestBody, serverAdress);
        }
    }
}
