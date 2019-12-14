namespace Snake
{
    public class RequestBody
    {
        public string direction { get; set; }

        public RequestBody(string direction)
        {
            this.direction = direction;
        }
    }
}
