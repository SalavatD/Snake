namespace SnakeServer.Models
{
    public struct GameBoardSize
    {
        public int width { get; set; }
        public int height { get; set; }

        public GameBoardSize(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
    }
}
