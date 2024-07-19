using System;

namespace TfLRouteManager.Services
{
    public class DelayManager
    {
        private TfLNetwork _network;

        public DelayManager(TfLNetwork network)
        {
            _network = network;
        }

        public void AddDelay(string fromStation, string toStation, double delay)
        {
            try
            {
                if (delay <= 0)
                {
                    Console.WriteLine("Delay must be a positive number.");
                    return;
                }

                Console.WriteLine("Do you want to add the delay to both directions? (yes/no):");
                var input = Console.ReadLine().ToLower();
                bool bothDirections = input == "yes" || input == "y";

                var from = _network.FindStation(fromStation);
                var to = _network.FindStation(toStation);

                if (from != null && to != null)
                {
                    var connection = _network.FindConnection(from, to);
                    if (connection != null)
                    {
                        connection.DelayedTime += delay;
                        Console.WriteLine($"Added {delay} minutes delay from {fromStation} to {toStation}.");
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
                            reverseConnection.DelayedTime += delay;
                            Console.WriteLine($"Added {delay} minutes delay from {toStation} to {fromStation}.");
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
                Console.WriteLine($"Error adding delay: {ex.Message}");
            }
        }

        public void RemoveDelay(string fromStation, string toStation, bool bothDirections = false)
        {
            try
            {
                var from = _network.FindStation(fromStation);
                var to = _network.FindStation(toStation);

                Console.WriteLine("Do you want to remove the delay to both directions? (yes/no):");
                var input = Console.ReadLine().ToLower();
                bothDirections = input == "yes" || input == "y";

                if (from != null && to != null)
                {
                    var connection = _network.FindConnection(from, to);
                    if (connection != null)
                    {
                        connection.DelayedTime = connection.NormalTime;
                        Console.WriteLine($"Removed delay from {fromStation} to {toStation}.");
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
                            reverseConnection.DelayedTime = reverseConnection.NormalTime;
                            Console.WriteLine($"Removed delay from {toStation} to {fromStation}.");
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
                Console.WriteLine($"Error removing delay: {ex.Message}");
            }
        }
    }
}
