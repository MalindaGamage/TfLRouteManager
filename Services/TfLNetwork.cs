using TfLRouteManager.Models;

namespace TfLRouteManager.Services
{
    public class TfLNetwork
    {
        public Line[] Lines { get; set; } = new Line[10];
        public int LineCount { get; set; } = 0;
        public Station[] Stations { get; set; } = new Station[100];
        public int StationCount { get; set; } = 0;
        private StationHashTable _stationHashTable;

        public TfLNetwork()
        {
            _stationHashTable = new StationHashTable(100);
        }

        public void InitializeNetwork()
        {
            // Circle line stations
            var circleLine = new Line { Name = "Circle" };
            AddStationsAndConnections(circleLine, new (string, double, string)[]
            {
                ("Edgware Road", 0, "Outer"),
                ("Baker Street", 2.0, "Outer"),
                ("Great Portland Street", 1.5, "Outer"),
                ("Euston Square", 2.0, "Outer"),
                ("King's Cross St. Pancras", 1.5, "Outer"),
                ("Farringdon", 2.5, "Outer"),
                ("Barbican", 2.0, "Outer"),
                ("Moorgate", 1.5, "Outer"),
                ("Liverpool Street", 1.5, "Outer"),
                ("Aldgate", 2.0, "Outer"),
                ("Tower Hill", 2.0, "Outer"),
                ("Monument", 1.5, "Outer"),
                ("Cannon Street", 2.0, "Outer"),
                ("Mansion House", 2.0, "Outer"),
                ("Blackfriars", 1.5, "Outer"),
                ("Temple", 1.5, "Outer"),
                ("Embankment", 2.0, "Outer"),
                ("Westminster", 2.0, "Outer"),
                ("St. James's Park", 1.5, "Outer"),
                ("Victoria", 2.0, "Outer"),
                ("Sloane Square", 1.5, "Outer"),
                ("South Kensington", 2.0, "Outer"),
                ("Gloucester Road", 1.5, "Outer"),
                ("High Street Kensington", 2.0, "Outer"),
                ("Notting Hill Gate", 2.0, "Outer"),
                ("Bayswater", 1.5, "Outer")
            });

            // Jubilee line stations
            var jubileeLine = new Line { Name = "Jubilee" };
            AddStationsAndConnections(jubileeLine, new (string, double, string)[]
            {
                ("Stanmore", 0, "Southbound"),
                ("Canons Park", 2.5, "Southbound"),
                ("Queensbury", 2.5, "Southbound"),
                ("Kingsbury", 2.5, "Southbound"),
                ("Wembley Park", 3.0, "Southbound"),
                ("Neasden", 2.0, "Southbound"),
                ("Dollis Hill", 2.0, "Southbound"),
                ("Willesden Green", 2.0, "Southbound"),
                ("Kilburn", 2.0, "Southbound"),
                ("West Hampstead", 1.5, "Southbound"),
                ("Finchley Road", 2.0, "Southbound"),
                ("Swiss Cottage", 1.5, "Southbound"),
                ("St. John's Wood", 2.0, "Southbound"),
                ("Baker Street", 2.0, "Southbound"),
                ("Bond Street", 1.5, "Southbound"),
                ("Green Park", 2.0, "Southbound"),
                ("Westminster", 2.0, "Southbound"),
                ("Waterloo", 2.0, "Southbound"),
                ("Southwark", 1.5, "Southbound"),
                ("London Bridge", 2.0, "Southbound"),
                ("Bermondsey", 2.0, "Southbound"),
                ("Canada Water", 2.0, "Southbound"),
                ("Canary Wharf", 2.5, "Southbound"),
                ("North Greenwich", 3.0, "Southbound"),
                ("Canning Town", 2.5, "Southbound"),
                ("West Ham", 3.0, "Southbound"),
                ("Stratford", 2.0, "Southbound")
            });

            // Victoria line stations
            var victoriaLine = new Line { Name = "Victoria" };
            AddStationsAndConnections(victoriaLine, new (string, double, string)[]
            {
                ("Walthamstow Central", 0, "Southbound"),
                ("Blackhorse Road", 2.0, "Southbound"),
                ("Tottenham Hale", 2.0, "Southbound"),
                ("Seven Sisters", 2.0, "Southbound"),
                ("Finsbury Park", 2.5, "Southbound"),
                ("Highbury & Islington", 2.0, "Southbound"),
                ("King's Cross St. Pancras", 1.5, "Southbound"),
                ("Euston", 2.0, "Southbound"),
                ("Warren Street", 1.5, "Southbound"),
                ("Oxford Circus", 1.5, "Southbound"),
                ("Green Park", 1.5, "Southbound"),
                ("Victoria", 1.5, "Southbound"),
                ("Pimlico", 2.0, "Southbound"),
                ("Vauxhall", 2.0, "Southbound"),
                ("Stockwell", 2.5, "Southbound"),
                ("Brixton", 2.0, "Southbound")
            });

            // Overground stations on the North London Line
            var overgroundLine = new Line { Name = "North London Overground" };
            AddStationsAndConnections(overgroundLine, new (string, double, string)[]
            {
                ("Richmond", 0, "Eastbound"),
                ("Kew Gardens", 2.5, "Eastbound"),
                ("Gunnersbury", 2.5, "Eastbound"),
                ("South Acton", 2.5, "Eastbound"),
                ("Acton Central", 2.5, "Eastbound"),
                ("Willesden Junction", 2.5, "Eastbound"),
                ("Kensal Green", 2.5, "Eastbound"),
                ("Kensal Rise", 2.5, "Eastbound"),
                ("Brondesbury", 2.5, "Eastbound"),
                ("Brondesbury Park", 2.5, "Eastbound"),
                ("West Hampstead", 2.5, "Eastbound"),
                ("Finchley Road & Frognal", 2.5, "Eastbound"),
                ("Hampstead Heath", 2.5, "Eastbound"),
                ("Gospel Oak", 2.5, "Eastbound"),
                ("Kentish Town West", 2.5, "Eastbound"),
                ("Camden Road", 2.5, "Eastbound"),
                ("Caledonian Road & Barnsbury", 2.5, "Eastbound"),
                ("Highbury & Islington", 2.5, "Eastbound"),
                ("Canonbury", 2.5, "Eastbound"),
                ("Dalston Junction", 2.5, "Eastbound"),
                ("Haggerston", 2.5, "Eastbound"),
                ("Hoxton", 2.5, "Eastbound"),
                ("Shoreditch High Street", 2.5, "Eastbound"),
                ("Whitechapel", 2.5, "Eastbound"),
                ("Shadwell", 2.5, "Eastbound"),
                ("Wapping", 2.5, "Eastbound"),
                ("Rotherhithe", 2.5, "Eastbound"),
                ("Canada Water", 2.5, "Eastbound"),
                ("Surrey Quays", 2.5, "Eastbound"),
                ("New Cross", 2.5, "Eastbound")
            });

            // Initialize interchanges
            AddInterchange("Baker Street", jubileeLine.Stations[13], circleLine.Stations[1], 2.0, "Westbound", "Outer");
            AddInterchange("Victoria", victoriaLine.Stations[12], circleLine.Stations[21], 2.0, "Southbound", "Outer");
            AddInterchange("Oxford Circus", victoriaLine.Stations[9], circleLine.Stations[15], 2.0, "Southbound", "Outer");

            // Adding Marble Arch and its connections
            var marbleArch = new Station { Name = "Marble Arch" };
            AddStation(marbleArch);

            // Adding connection between Marble Arch and Bond Street
            var bondStreet = jubileeLine.Stations[14];
            var connection = new Connection
            {
                From = marbleArch,
                To = bondStreet,
                NormalTime = 2.0,
                DelayedTime = 2.0,
                Line = jubileeLine,
                Direction = "Eastbound"
            };
            marbleArch.AddConnection(connection);
            bondStreet.AddConnection(new Connection
            {
                From = bondStreet,
                To = marbleArch,
                NormalTime = 2.0,
                DelayedTime = 2.0,
                Line = jubileeLine,
                Direction = "Westbound"
            });

            // Ensure Great Portland Street is added
            var greatPortlandStreet = circleLine.Stations[2];
            AddStation(greatPortlandStreet);

            // Add direct connections for testing
            AddConnection("Baker Street", "Victoria", 5.0, "Southbound");
            AddConnection("Bond Street", "Baker Street", 2.0, "Eastbound");
        }
        private void AddStationsAndConnections(Line line, (string, double, string)[] stations)
        {
            Station previousStation = null;
            foreach (var (stationName, travelTime, direction) in stations)
            {
                var station = new Station { Name = stationName };
                line.AddStation(station);

                if (previousStation != null)
                {
                    var connection = new Connection
                    {
                        From = previousStation,
                        To = station,
                        NormalTime = travelTime,
                        DelayedTime = travelTime,
                        Line = line,
                        Direction = direction
                    };
                    previousStation.AddConnection(connection);
                    station.AddConnection(new Connection
                    {
                        From = station,
                        To = previousStation,
                        NormalTime = travelTime,
                        DelayedTime = travelTime,
                        Line = line,
                        Direction = GetOppositeDirection(direction)
                    });
                }

                AddStation(station);
                previousStation = station;
            }

            AddLine(line);
        }

        public void AddStation(Station station)
        {
            if (StationCount < Stations.Length)
            {
                Stations[StationCount++] = station;
                _stationHashTable.Add(station.Name, station);
            }
        }

        public Station FindStation(string name)
        {
            return _stationHashTable.Get(name);
        }

        public void AddLine(Line line)
        {
            if (LineCount < Lines.Length)
            {
                Lines[LineCount++] = line;
            }
        }
        private void AddInterchange(string stationName, Station fromStation, Station toStation, double interchangeTime, string fromDirection, string toDirection)
        {
            var station = FindStation(stationName);
            if (station != null)
            {
                var interchangeConnection = new Connection
                {
                    From = fromStation,
                    To = toStation,
                    NormalTime = interchangeTime,
                    DelayedTime = interchangeTime,
                    Line = fromStation.Connections[0]?.Line,
                    Direction = fromDirection
                };

                fromStation.AddConnection(interchangeConnection);
                toStation.AddConnection(new Connection
                {
                    From = toStation,
                    To = fromStation,
                    NormalTime = interchangeTime,
                    DelayedTime = interchangeTime,
                    Line = toStation.Connections[0]?.Line,
                    Direction = toDirection
                });
            }
        }

        public void AddConnection(string fromStationName, string toStationName, double travelTime, string direction)
        {
            var fromStation = FindStation(fromStationName);
            var toStation = FindStation(toStationName);
            if (fromStation != null && toStation != null)
            {
                var connection = new Connection
                {
                    From = fromStation,
                    To = toStation,
                    NormalTime = travelTime,
                    DelayedTime = travelTime,
                    Line = fromStation.Connections[0]?.Line,
                    Direction = direction
                };
                fromStation.AddConnection(connection);
                toStation.AddConnection(new Connection
                {
                    From = toStation,
                    To = fromStation,
                    NormalTime = travelTime,
                    DelayedTime = travelTime,
                    Line = toStation.Connections[0]?.Line,
                    Direction = GetOppositeDirection(direction)
                });
            }
        }
        public Connection FindConnection(Station from, Station to)
        {
            return from.FindConnection(to);
        }

        private string GetOppositeDirection(string direction)
        {
            switch (direction)
            {
                case "Eastbound": return "Westbound";
                case "Westbound": return "Eastbound";
                case "Northbound": return "Southbound";
                case "Southbound": return "Northbound";
                case "Inner": return "Outer";
                case "Outer": return "Inner";
                default: return direction;
            }
        }
    }
}
