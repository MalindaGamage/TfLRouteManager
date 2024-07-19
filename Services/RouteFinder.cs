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
            var nodes = new Station[_network.StationCount];
            var previousLine = new Line[_network.Stations.Length];
            bool[] visited = new bool[_network.StationCount];
            int nodeCount = 0;

            for (int i = 0; i < _network.StationCount; i++)
            {
                var station = _network.Stations[i];
                if (station.Name == startStation)
                {
                    distances[i] = 0;
                }
                else
                {
                    distances[i] = double.PositiveInfinity;
                }
                nodes[nodeCount++] = station;
            }

            while (nodeCount != 0)
            {
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
                visited[Array.IndexOf(_network.Stations, smallest)] = true;

                if (smallest.Name == endStation)
                {
                    var path = new string[_network.StationCount];
                    var directions = new string[_network.StationCount];
                    int pathCount = 0;
                    while (previous[Array.IndexOf(_network.Stations, smallest)] != null)
                    {
                        path[pathCount] = smallest.Name;
                        var previousStation = previous[Array.IndexOf(_network.Stations, smallest)];
                        var connection = _network.FindConnection(previousStation, smallest);
                        directions[pathCount++] = $"{connection.Line.Name}({connection.Direction}): {previousStation.Name} to {smallest.Name}: {connection.DelayedTime} min";
                        smallest = previous[Array.IndexOf(_network.Stations, smallest)];
                    }
                    path[pathCount] = startStation;
                    Array.Resize(ref path, pathCount + 1);
                    Array.Resize(ref directions, pathCount);
                    Array.Reverse(path);
                    Array.Reverse(directions);
                    return (path, directions);
                }

                for (int i = 0; i < smallest.ConnectionCount; i++)
                {
                    var neighbor = smallest.Connections[i];
                    if (neighbor.IsClosed) continue;

                    int neighborIndex = Array.IndexOf(_network.Stations, neighbor.To);
                    if (visited[neighborIndex]) continue;

                    var alt = distances[Array.IndexOf(_network.Stations, smallest)] + neighbor.DelayedTime;
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
