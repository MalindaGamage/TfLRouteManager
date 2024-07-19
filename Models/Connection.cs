namespace TfLRouteManager.Models
{
    public class Connection
    {
        public Station From { get; set; }
        public Station To { get; set; }
        public double NormalTime { get; set; }
        public double DelayedTime { get; set; }
        public Line Line { get; set; }
        public string Direction { get; set; }
        public bool IsClosed { get; set; } = false;
    }
}
