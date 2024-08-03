using System;
using System.Collections.Generic;
using System.Text;
using TfLRouteManager.Models;

namespace TfLRouteManager.Services
{
    public class RouteFinder
    {
        private TfLNetwork _network;

        public RouteFinder(TfLNetwork network)
        {
            _network = network;
        }

        public (string[] route, string[] directions) FindFastestRoute(string startStation, string endStation)
        {
            var from = _network.FindStation(startStation);
            var to = _network.FindStation(endStation);

            if (from == null || to == null)
            {
                Console.WriteLine("One or both of the specified stations are not in the network.");
                return (new string[0], new string[0]);
            }

            var previous = new Station[_network.Stations.Length];
            var distances = new double[_network.Stations.Length];
            var nodes = new Station[_network.StationCount];
            var previousLine = new Line[_network.Stations.Length];
            bool[] visited = new bool[_network.Stations.Length];
            int nodeCount = 0;

            for (int i = 0; i < _network.StationCount; i++)
            {
                var station = _network.Stations[i];
                distances[i] = station == from ? 0 : double.PositiveInfinity;
                nodes[nodeCount++] = station; //i
            }

            while (nodeCount != 0)
            {
                // Find the smallest distance node
                int smallestIndex = 0;
                for (int i = 1; i < nodeCount; i++)
                {
                    if (distances[Array.IndexOf(_network.Stations, nodes[i])] < distances[Array.IndexOf(_network.Stations, nodes[smallestIndex])])
                    {
                        smallestIndex = i;
                    }
                }

                var smallest = nodes[smallestIndex];
                nodes[smallestIndex] = nodes[--nodeCount];

                int smallestIndexInStations = Array.IndexOf(_network.Stations, smallest);
                if (smallestIndexInStations < 0 || smallestIndexInStations >= _network.Stations.Length)
                {
                    continue;
                }

                visited[smallestIndexInStations] = true;

                if (smallest == to)
                {
                    var path = new List<string>();
                    var directions = new List<string>();

                    while (previous[smallestIndexInStations] != null)
                    {
                        path.Add(smallest.Name);
                        var previousStation = previous[smallestIndexInStations];
                        var connection = _network.FindConnection(previousStation, smallest);

                        if (connection != null)
                        {
                            directions.Add($"{connection.Line.Name}({connection.Direction}): {previousStation.Name} to {smallest.Name}: {connection.DelayedTime} min");
                        }

                        smallest = previousStation;
                        smallestIndexInStations = Array.IndexOf(_network.Stations, smallest);
                    }

                    path.Add(startStation);
                    path.Reverse();
                    directions.Reverse();

                    return (path.ToArray(), directions.ToArray());
                }

                for (int i = 0; i < smallest.ConnectionCount; i++)
                {
                    var neighbor = smallest.Connections[i];
                    if (neighbor.IsClosed) continue;

                    int neighborIndex = Array.IndexOf(_network.Stations, neighbor.To);
                    if (neighborIndex < 0 || visited[neighborIndex]) continue;

                    var alt = distances[smallestIndexInStations] + neighbor.DelayedTime;
                    if (alt < distances[neighborIndex])
                    {
                        distances[neighborIndex] = alt;
                        previous[neighborIndex] = smallest;
                        previousLine[neighborIndex] = neighbor.Line;
                    }
                }
            }

            return (new string[0], new string[0]); // No path found
        }


        public string GenerateRouteDescription(string[] route, string[] directions)
        {
            if (route.Length == 0)
            {
                return "No route found.";
            }

            var description = new StringBuilder();
            description.AppendLine($"Route: {route[0]} to {route[route.Length - 1]}:");

            for (int i = 0; i < route.Length; i++)
            {
                if (i == 0)
                {
                    description.AppendLine($"(1) Start: {route[i]}");
                }
                else
                {
                    description.AppendLine($"({i + 1}) {directions[i - 1]}");
                    if (i < route.Length - 1)
                    {
                        description.AppendLine($"Change: {route[i]}");
                    }
                }
            }

            description.AppendLine($"Total Journey Time: {CalculateTotalJourneyTime(route)} minutes");

            return description.ToString();
        }

        private double CalculateTotalJourneyTime(string[] route)
        {
            double totalTime = 0.0;
            for (int i = 0; i < route.Length - 1; i++)
            {
                var from = _network.FindStation(route[i]);
                var to = _network.FindStation(route[i + 1]);
                var connection = _network.FindConnection(from, to);
                if (connection != null)
                {
                    totalTime += connection.DelayedTime;
                }
            }
            return totalTime;
        }
    }
}
