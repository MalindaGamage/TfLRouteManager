using System;

namespace TfLRouteManager.Services
{
    public class TrackManager
    {
        private TfLNetwork _network;

        public TrackManager(TfLNetwork network)
        {
            _network = network;
        }

        public void CloseTrack(string fromStation, string toStation, bool bothDirections = false)
        {
            try
            {
                var from = _network.FindStation(fromStation);
                var to = _network.FindStation(toStation);

                if (from != null && to != null)
                {
                    var connection = _network.FindConnection(from, to);
                    connection.IsClosed = true;
                    Console.WriteLine($"Closed track from {fromStation} to {toStation}.");

                    if (bothDirections)
                    {
                        var reverseConnection = _network.FindConnection(to, from);
                        if (reverseConnection != null)
                        {
                            reverseConnection.IsClosed = true;
                            Console.WriteLine($"Closed track from {toStation} to {fromStation}.");
                        }
                        else
                        {
                            Console.WriteLine($"No direct connection found from {toStation} to {fromStation}.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"One or both stations are not found in the network: {fromStation}, {toStation}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error closing track: {ex.Message}");
            }
        }

        public void OpenTrack(string fromStation, string toStation, bool bothDirections = false)
        {
            try
            {
                var from = _network.FindStation(fromStation);
                var to = _network.FindStation(toStation);

                if (from != null && to != null)
                {
                    var connection = _network.FindConnection(from, to);
                    if (connection != null)
                    {
                        connection.IsClosed = false;
                        Console.WriteLine($"Opened track from {fromStation} to {toStation}.");
                    }
                    else
                    {
                        Console.WriteLine($"No direct connection found from {fromStation} to {toStation}.");
                    }

                    if (bothDirections)
                    {
                        var reverseConnection = _network.FindConnection(to, from);
                        if (reverseConnection != null)
                        {
                            reverseConnection.IsClosed = false;
                            Console.WriteLine($"Opened track from {toStation} to {fromStation}.");
                        }
                        else
                        {
                            Console.WriteLine($"No direct connection found from {toStation} to {fromStation}.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"One or both stations are not found in the network: {fromStation}, {toStation}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error opening track: {ex.Message}");
            }
        }
    }
}