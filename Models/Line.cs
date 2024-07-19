namespace TfLRouteManager.Models
{
    public class Line
    {
        public string Name { get; set; }
        public Station[] Stations { get; set; } = new Station[50];
        public int StationCount { get; set; } = 0;

        public void AddStation(Station station)
        {
            if (StationCount < Stations.Length)
            {
                Stations[StationCount++] = station;
            }
        }
    }
}
