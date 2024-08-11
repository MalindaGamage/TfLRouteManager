using System;

namespace TfLRouteManager.Models
{
    public class Line
    {
        public string Name { get; set; }
        public Station[] Stations { get; set; }
        public int StationCount { get; set; } = 0;

        public Line(int initialCapacity = 10) // Start with a small initial capacity
        {
            Stations = new Station[initialCapacity];
        }

        public void AddStation(Station station)
        {
            if (StationCount == Stations.Length)
            {
                Resize();
            }
            Stations[StationCount++] = station;
        }

        private void Resize()
        {
            int newCapacity = Stations.Length * 2; // Double the capacity
            Station[] newStationsArray = new Station[newCapacity];
            for (int i = 0; i < Stations.Length; i++)
            {
                newStationsArray[i] = Stations[i];
            }
            Stations = newStationsArray;
        }
    }
}
