namespace SnakeServer.Models
{
    public class InitialiseResponseBody
    {
        public int timeUntilNextTurnMilliseconds { get; set; }

        public InitialiseResponseBody(int timeUntilNextTurnMilliseconds)
        {
            this.timeUntilNextTurnMilliseconds = timeUntilNextTurnMilliseconds;
        }
    }
}
