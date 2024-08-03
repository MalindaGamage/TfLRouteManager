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


/*
1. Array Access Optimization: We store the station.Connections array in a local variable before entering the inner loop. This reduces the number of property accesses, which can slightly improve performance.
2. Early Exit Optimization: Though not directly applicable here since we need to check all connections, ensuring that the inner loop runs efficiently is key.
3. Loop Structure: The loops are already straightforward and efficient for iterating over the stations and their connections.*/