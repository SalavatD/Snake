using System.Collections.Generic;
using System.Drawing;

namespace Snake
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
    }
}
