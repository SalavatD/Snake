namespace Snake
{
    public class GameBoardSize
    {
        public int width { get; set; }
        public int height { get; set; }

        public override string ToString()
        {
            return  "(" + width + "x" + height + ")";
        }
    }
}
