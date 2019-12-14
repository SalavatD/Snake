using System.Collections.Generic;
using System.Drawing;

namespace SnakeServer.Models
{
    public class ResponseBody
    {
        public int turnNumber { get; set; }
        public int timeUntilNextTurnMilliseconds { get; set; }
        public GameBoardSize gameBoardSize { get; set; }
        public PointF currentPosition { get; set; }
        public List<PointF> snake { get; set; }
        public List<PointF> food { get; set; }
        public int GameStatus { get; set; }
        public int GameScore { get; set; }
        public int SnakeLength { get; set; }

        public ResponseBody(
            int turnNumber,
            int timeUntilNextTurnMilliseconds,
            GameBoardSize gameBoardSize,
            PointF currentPosition,
            List<PointF> snakeBody,
            List<PointF> food,
            int gameStatus,
            int gameScore,
            int snakeLength
            )
        {
            this.turnNumber = turnNumber;
            this.timeUntilNextTurnMilliseconds = timeUntilNextTurnMilliseconds;
            this.gameBoardSize = gameBoardSize;
            this.currentPosition = currentPosition;
            this.snake = snakeBody;
            this.food = food;
            GameStatus = gameStatus;
            GameScore = gameScore;
            SnakeLength = snakeLength;
        }
    }
}
