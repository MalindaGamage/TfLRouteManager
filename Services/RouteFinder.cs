using System;
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
            var visited = new bool[_network.Stations.Length];
            var stationIndexes = new int[_network.Stations.Length];
            int nodeCount = 0;

            for (int i = 0; i < _network.Stations.Length; i++)
            {
                var station = _network.Stations[i];
                distances[i] = station == from ? 0 : double.PositiveInfinity;
                stationIndexes[i] = i;
                nodeCount++;
            }

            while (nodeCount != 0)
            {
                int smallestIndex = -1;
                for (int i = 0; i < _network.Stations.Length; i++)
                {
                    if (!visited[i] && (smallestIndex == -1 || distances[i] < distances[smallestIndex]))
                    {
                        smallestIndex = i;
                    }
                }

                if (smallestIndex == -1 || distances[smallestIndex] == double.PositiveInfinity)
                {
                    break;
                }

                var smallest = _network.Stations[smallestIndex];
                visited[smallestIndex] = true;
                nodeCount--;

                if (smallest == to)
                {
                    var path = new string[_network.Stations.Length];
                    var directions = new string[_network.Stations.Length];
                    int pathCount = 0;

                    while (previous[smallestIndex] != null)
                    {
                        path[pathCount] = smallest.Name;
                        var previousStation = previous[smallestIndex];
                        var connection = _network.FindConnection(previousStation, smallest);

                        if (connection != null)
                        {
                            directions[pathCount] = $"{connection.Line.Name}({connection.Direction}): {previousStation.Name} to {smallest.Name}: {connection.DelayedTime} min";
                        }

                        smallest = previousStation;
                        smallestIndex = Array.IndexOf(_network.Stations, smallest);
                        pathCount++;
                    }

                    path[pathCount] = startStation;

                    Array.Reverse(path, 0, pathCount + 1);
                    Array.Reverse(directions, 0, pathCount);

                    string[] finalPath = new string[pathCount + 1];
                    string[] finalDirections = new string[pathCount];
                    Array.Copy(path, finalPath, pathCount + 1);
                    Array.Copy(directions, finalDirections, pathCount);

                    return (finalPath, finalDirections);
                }

                for (int i = 0; i < smallest.ConnectionCount; i++)
                {
                    var neighbor = smallest.Connections[i];
                    if (neighbor.IsClosed) continue;

                    int neighborIndex = Array.IndexOf(_network.Stations, neighbor.To);
                    if (neighborIndex < 0 || visited[neighborIndex]) continue;

                    var alt = distances[smallestIndex] + neighbor.DelayedTime;
                    if (alt < distances[neighborIndex])
                    {
                        distances[neighborIndex] = alt;
                        previous[neighborIndex] = smallest;
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
