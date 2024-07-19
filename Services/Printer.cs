using System;

namespace TfLRouteManager.Services
{
    public class Printer
    {
        private TfLNetwork _network;

        public Printer(TfLNetwork network)
        {
            _network = network;
        }

        public void PrintClosedTracks()
        {
            bool anyClosed = false;
            for (int i = 0; i < _network.StationCount; i++)
            {
                var station = _network.Stations[i];
                for (int j = 0; j < station.ConnectionCount; j++)
                {
                    var connection = station.Connections[j];
                    if (connection.IsClosed)
                    {
                        Console.WriteLine($"{connection.From.Name} - {connection.To.Name}: Closed");
                        anyClosed = true;
                    }
                }
            }
            if (!anyClosed)
            {
                Console.WriteLine("No closed tracks.");
            }
        }

        public void PrintDelayedTracks()
        {
            bool anyDelayed = false;
            for (int i = 0; i < _network.StationCount; i++)
            {
                var station = _network.Stations[i];
                for (int j = 0; j < station.ConnectionCount; j++)
                {
                    var connection = station.Connections[j];
                    if (connection.DelayedTime > connection.NormalTime)
                    {
                        Console.WriteLine($"{connection.From.Name} to {connection.To.Name}: {connection.NormalTime} min -> {connection.DelayedTime} min");
                        anyDelayed = true;
                    }
                }
            }
            if (!anyDelayed)
            {
                Console.WriteLine("No delayed tracks.");
            }
        }
    }
}
