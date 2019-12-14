using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace SnakeServer.Models
{
    class GameLogic
    {
        #region Поля класса

        // Статическая переменная, хранящая ссылку на объект GameLogic:
        public static GameLogic gameLogic = new GameLogic();

        // Инициализация рандома:
        static readonly Random random = new Random();

        // Ссылка на объект ResponseBody,
        // который возвращается в качестве ответа
        // в GameController:
        static ResponseBody responseBody = new ResponseBody(
            0, 0,
            new GameBoardSize(0, 0),
            new PointF(0, 0),
            new List<PointF>(),
            new List<PointF>(),
            0, 0, 0);

        // Ссылка на объект, содержащий в себе размер доски:
        public GameBoardSize gameBoardSize;

        // Ссылка на объект класса Timer:
        static Timer timer;

        // Счет игры и длина змеи:
        int score;
        int length = 2;

        // Состояние игры: 
        // 0 - начало новой игры
        // 1 - идет игра
        // 2 - текущая игра окончена
        int gameStatus;

        //  Направление и текущее направление, начальные значения равны "Top":
        string direction = "Top";
        string previousDirection = "Top";

        // Следование ТЗ:
        readonly int turnTime = 2000;
        public int timeUntilNextTurnMilliseconds { get; } = 250;
        public int turnNumber = 0;

        // Количество еды на игровом поле:
        readonly int numberOfFood = 5;

        // Размер игрового поля:
        readonly int width = 50;
        readonly int height = 50;

        // Списки, закрытые типом PointF. Содержит в себе координаты точек тела змеи еды:
        readonly List<PointF> snake = new List<PointF>();
        readonly List<PointF> food = new List<PointF>();

        // Текущее положение головы змеи:
        PointF currentPosition;

        // Объект синхронизации доступа к управлению направлением змейки
        readonly object syncObject = new object();

        #endregion

        /// <summary>
        /// Метод, вызываемый из InitialiseController.
        /// Инициирует начало игры.
        /// </summary>
        public void InitialiseGame()
        {
            timer = new Timer(Update, null, turnTime, timeUntilNextTurnMilliseconds);

            gameBoardSize = new GameBoardSize(width, height);
            currentPosition = new PointF(gameBoardSize.width / 2, gameBoardSize.height / 2);

            if ((gameStatus == 0 || gameStatus == 2))
            {
                for (int i = 0; i < numberOfFood; i++) food.Add(PlaceFood());
                gameStatus = 1;
            }
        }

        /// <summary>
        /// Главный метод класса GameLogic, вызывется каждые
        /// timeUntilNextTurnMilliseconds миллисекунд.
        /// Проверяет, попытки змеи съесть еду и укусить саму себя.
        /// Сохраняет в переменую response текущее состояние игры.
        /// </summary>
        /// <param name="object"></param>
        void Update(object @object)
        {
            // Направлениие движения змеи:
            if (direction == "Bottom")
                currentPosition.Y += 1;
            else if (direction == "Top")
                currentPosition.Y -= 1;
            else if (direction == "Left")
                currentPosition.X -= 1;
            else if (direction == "Right")
                currentPosition.X += 1;
            snake.Add(currentPosition);

            turnNumber++;

            // Проверка выхода за пределы игрового поля:
            if ((currentPosition.X < 0) ||
                (currentPosition.Y < 0) ||
                (currentPosition.X > gameBoardSize.width - 1) ||
                (currentPosition.Y > gameBoardSize.height - 1))
                GameOver();

            // Проверка на съеденость змеёй еды:
            for (int i = 0; i < numberOfFood; i++)
            {
                if ((Math.Abs(food[i].X - currentPosition.X) < 1) &&
                (Math.Abs(food[i].Y - currentPosition.Y) < 1))
                {
                    food[i] = PlaceFood();
                    score++;
                    length += 1;
                }
            }
            

            // Проверка на то, что змея укусила себя:
            for (int i = 0; i < snake.Count - 1; i++)
            {
                if ((Math.Abs(snake[i].X - currentPosition.X) < 1) &&
                     (Math.Abs(snake[i].Y - currentPosition.Y) < 1))
                {
                    GameOver();
                    break;
                }
            }

            if (snake.Count > length)
                snake.RemoveAt(0);

            responseBody = new ResponseBody(
                turnNumber,
                timeUntilNextTurnMilliseconds,
                gameBoardSize,
                currentPosition,
                snake,
                food,
                gameStatus,
                score,
                length
                );
        }

        /// <summary>
        /// Метод, вызываемый при получении POST запроса от клиента,
        /// и меняющий направление движения змейки, если это возможно.
        /// </summary>
        public void ChangeMoving(string movingDirection)
        {
            lock(syncObject)
            {
                if (movingDirection == "Bottom" && previousDirection != "Top")
                {
                    direction = "Bottom";
                    previousDirection = direction;
                }
                else if (movingDirection == "Top" && previousDirection != "Bottom")
                {
                    direction = "Top";
                    previousDirection = direction;
                }
                else if (movingDirection == "Left" && previousDirection != "Right")
                {
                    direction = "Left";
                    previousDirection = direction;
                }
                else if (movingDirection == "Right" && previousDirection != "Left")
                {
                    direction = "Right";
                    previousDirection = direction;
                }
                Thread.Sleep(timeUntilNextTurnMilliseconds);
            }
        }

        /// <summary>
        /// Метод, возвращающий новые координаты еды.
        /// </summary>
        PointF PlaceFood()
        {
        // Конструкция необходимая для того, чтобы не создать "еду" под змеёй:
        regenerate:;
            PointF foodPiont = new PointF(
                random.Next(1, gameBoardSize.width - 1),
                random.Next(1, gameBoardSize.height - 1)
                );
            for (int i = 0; i < snake.Count; i++)
            {
                if ((Math.Abs(snake[i].X - foodPiont.X) < 1) &&
                     (Math.Abs(snake[i].Y - foodPiont.Y) < 1))
                {
                    goto regenerate;
                }
            }
            return foodPiont;
        }

        /// <summary>
        /// Возвращает response в GameController на GET запрос от клиента.
        /// </summary>
        public ResponseBody GameState()
        {
            return responseBody;
        }

        /// <summary>
        ///  Метод, вызываемый при проигрыше текущей игры.
        /// </summary>
        void GameOver()
        {
            snake.Clear();
            currentPosition = new PointF(gameBoardSize.width / 2, gameBoardSize.height / 2);
            direction = "Top";
            length = 2;
            score = 0;
            gameStatus = 2;
            turnNumber = 0;
        }
    }
}
