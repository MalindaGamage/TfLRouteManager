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
            // Initialize lines and stations manually based on the CSV data

            // Bakerloo Line
            var bakerlooLine = new Line { Name = "Bakerloo" };
            AddStationsAndConnections(bakerlooLine, new (string, double, string)[]
            {
                ("Baker Street", 1.68, "Southbound"),
                ("Regents Park", 1.85, "Southbound"),
                ("Oxford Circus", 1.95, "Southbound"),
                ("Piccadilly Circus", 1.35, "Southbound"),
                ("Charing Cross", 0.95, "Southbound"),
                ("Embankment", 0.97, "Northbound"),
                ("Charing Cross", 1.60, "Northbound"),
                ("Piccadilly Circus", 2.02, "Northbound"),
                ("Oxford Circus", 1.72, "Northbound"),
                ("Regents Park", 1.65, "Northbound"),
                ("Baker Street", 0.0, "Northbound")
            });

            // Central Line
            var centralLine = new Line { Name = "Central" };
            AddStationsAndConnections(centralLine, new (string, double, string)[]
            {
                ("White City", 2.68, "Eastbound"),
                ("Shepherds Bush", 1.37, "Eastbound"),
                ("Holland Park", 1.08, "Eastbound"),
                ("Notting Hill Gate", 1.17, "Eastbound"),
                ("Queensway", 1.35, "Eastbound"),
                ("Lancaster Gate", 1.92, "Eastbound"),
                ("Marble Arch", 1.00, "Eastbound"),
                ("Bond Street", 1.10, "Eastbound"),
                ("Oxford Circus", 0.98, "Eastbound"),
                ("Tottenham Court Road", 1.63, "Eastbound"),
                ("Holborn", 0.87, "Eastbound"),
                ("Chancery Lane", 1.52, "Eastbound"),
                ("St Pauls", 1.62, "Eastbound"),
                ("Bank", 1.62, "Eastbound"),
                ("Liverpool Street", 1.65, "Westbound"),
                ("Bank", 1.63, "Westbound"),
                ("St Pauls", 1.52, "Westbound"),
                ("Chancery Lane", 0.85, "Westbound"),
                ("Holborn", 1.38, "Westbound"),
                ("Tottenham Court Road", 1.02, "Westbound"),
                ("Oxford Circus", 1.03, "Westbound"),
                ("Bond Street", 1.02, "Westbound"),
                ("Marble Arch", 1.62, "Westbound"),
                ("Lancaster Gate", 1.65, "Westbound"),
                ("Queensway", 1.18, "Westbound"),
                ("Notting Hill Gate", 1.18, "Westbound"),
                ("Holland Park", 1.52, "Westbound"),
                ("Shepherds Bush", 2.77, "Westbound"),
                ("White City", 0.0, "Westbound")
            });

            // Victoria Line
            var victoriaLine = new Line { Name = "Victoria" };
            AddStationsAndConnections(victoriaLine, new (string, double, string)[]
            {
                ("Walthamstow", 2.12, "Southbound"),
                ("Blackhorse Road", 1.90, "Southbound"),
                ("Tottenham Hale", 2.00, "Southbound"),
                ("Seven Sisters", 3.77, "Southbound"),
                ("Finsbury Park", 2.90, "Southbound"),
                ("Highbury", 2.77, "Southbound"),
                ("Kings Cross", 1.32, "Southbound"),
                ("Euston", 1.30, "Southbound"),
                ("Warren Street", 1.72, "Southbound"),
                ("Oxford Circus", 1.78, "Southbound"),
                ("Green Park", 1.88, "Southbound"),
                ("Victoria", 1.83, "Southbound"),
                ("Pimlico", 1.40, "Southbound"),
                ("Vauxhall", 2.30, "Southbound"),
                ("Stockwell", 2.03, "Southbound"),
                ("Brixton", 2.18, "Northbound"),
                ("Stockwell", 2.23, "Northbound"),
                ("Vauxhall", 1.37, "Northbound"),
                ("Pimlico", 2.18, "Northbound"),
                ("Victoria", 1.95, "Northbound"),
                ("Green Park", 1.97, "Northbound"),
                ("Oxford Circus", 1.53, "Northbound"),
                ("Warren Street", 1.32, "Northbound"),
                ("Euston", 1.35, "Northbound"),
                ("Kings Cross", 2.87, "Northbound"),
                ("Highbury", 2.40, "Northbound"),
                ("Finsbury Park", 4.25, "Northbound"),
                ("Seven Sisters", 1.60, "Northbound"),
                ("Tottenham Hale", 1.97, "Northbound"),
                ("Blackhorse Road", 2.12, "Northbound"),
                ("Walthamstow", 0.0, "Northbound")
            });

            // Waterloo & City Line
            var waterlooCityLine = new Line { Name = "Waterloo & City" };
            AddStationsAndConnections(waterlooCityLine, new (string, double, string)[]
            {
                ("Waterloo", 4.87, "Eastbound"),
                ("Bank", 4.20, "Westbound")
            });

            // Jubilee Line
            var jubileeLine = new Line { Name = "Jubilee" };
            AddStationsAndConnections(jubileeLine, new (string, double, string)[]
            {
                ("Stanmore", 1.95, "Eastbound"),
                ("Canons Park", 1.93, "Eastbound"),
                ("Queensbury", 1.72, "Eastbound"),
                ("Kingsbury", 3.47, "Eastbound"),
                ("Wembley Park", 2.60, "Eastbound"),
                ("Neasden", 1.43, "Eastbound"),
                ("Dollis Hill", 1.80, "Eastbound"),
                ("Willesden Green", 1.68, "Eastbound"),
                ("Kilburn", 1.63, "Eastbound"),
                ("West Hampstead", 1.25, "Eastbound"),
                ("Finchley Road", 1.18, "Eastbound"),
                ("Swiss Cottage", 1.52, "Eastbound"),
                ("St Johns Wood", 2.77, "Eastbound"),
                ("Baker Street", 2.10, "Eastbound"),
                ("Bond Street", 1.78, "Eastbound"),
                ("Green Park", 1.87, "Eastbound"),
                ("Westminster", 1.38, "Eastbound"),
                ("Waterloo", 1.02, "Eastbound"),
                ("Southwark", 1.65, "Eastbound"),
                ("London Bridge", 2.25, "Eastbound"),
                ("Bermondsey", 1.48, "Eastbound"),
                ("Canada Water", 2.50, "Eastbound"),
                ("Canary Wharf", 2.23, "Eastbound"),
                ("North Greenwich", 2.15, "Eastbound"),
                ("Canning Town", 2.15, "Eastbound"),
                ("West Ham", 3.15, "Eastbound"),
                ("Stratford", 2.42, "Westbound"),
                ("West Ham", 2.13, "Westbound"),
                ("Canning Town", 2.17, "Westbound"),
                ("North Greenwich", 1.98, "Westbound"),
                ("Canary Wharf", 2.63, "Westbound"),
                ("Canada Water", 1.52, "Westbound"),
                ("Bermondsey", 2.17, "Westbound"),
                ("London Bridge", 1.77, "Westbound"),
                ("Southwark", 0.97, "Westbound"),
                ("Waterloo", 1.43, "Westbound"),
                ("Westminster", 1.82, "Westbound"),
                ("Green Park", 1.82, "Westbound"),
<<<<<<< HEAD
                ("Bond Street", 2.28, "Westbound"),
=======
                                ("Bond Street", 2.28, "Westbound"),
>>>>>>> bd5f9049a79149d3a35d1b7509f52415eddc7834
                ("Baker Street", 2.85, "Westbound"),
                ("St Johns Wood", 1.52, "Westbound"),
                ("Swiss Cottage", 1.18, "Westbound"),
                ("Finchley Road", 1.20, "Westbound"),
                ("West Hampstead", 1.55, "Westbound"),
                ("Kilburn", 2.07, "Westbound"),
                ("Willesden Green", 1.67, "Westbound"),
                ("Dollis Hill", 1.38, "Westbound"),
                ("Neasden", 2.65, "Westbound"),
                ("Wembley Park", 3.47, "Westbound"),
                ("Kingsbury", 1.85, "Westbound"),
                ("Queensbury", 2.23, "Westbound"),
                ("Canons Park", 2.87, "Westbound"),
                ("Stanmore", 0.0, "Westbound")
        });

            // Northern Line
            var northernLine = new Line { Name = "Northern" };
            AddStationsAndConnections(northernLine, new (string, double, string)[]
            {
                ("Euston (CX)", 1.18, "Southbound"),
                ("Warren Street", 1.07, "Southbound"),
                ("Goodge Street", 1.32, "Southbound"),
                ("Tottenham Court Road", 0.98, "Southbound"),
                ("Leicester Square", 1.20, "Southbound"),
                ("Charing Cross", 0.80, "Southbound"),
                ("Embankment", 1.37, "Southbound"),
                ("Moorgate", 1.90, "Southbound"),
                ("Bank", 1.55, "Southbound"),
                ("London Bridge", 1.53, "Northbound"),
                ("Bank", 1.77, "Northbound"),
                ("Moorgate", 1.40, "Northbound"),
                ("Waterloo", 1.40, "Northbound"),
                ("Embankment", 0.87, "Northbound"),
                ("Charing Cross", 1.17, "Northbound"),
                ("Leicester Square", 1.03, "Northbound"),
                ("Tottenham Court Road", 1.28, "Northbound"),
                ("Goodge Street", 1.07, "Northbound"),
                ("Warren Street", 1.18, "Northbound"),
                ("Euston (CX)", 0.0, "Northbound")
            });

            // Piccadilly Line
            var piccadillyLine = new Line { Name = "Piccadilly" };
            AddStationsAndConnections(piccadillyLine, new (string, double, string)[]
            {
                ("Hammersmith", 1.38, "Eastbound"),
                ("Barons Court", 2.55, "Eastbound"),
                ("Earls Court", 1.37, "Eastbound"),
                ("Gloucester Road", 1.28, "Eastbound"),
                ("South Kensington", 2.48, "Eastbound"),
                ("Knightsbridge", 1.10, "Eastbound"),
                ("Hyde Park Corner", 1.73, "Eastbound"),
                ("Green Park", 1.10, "Eastbound"),
                ("Piccadilly Circus", 1.17, "Eastbound"),
                ("Leicester Square", 0.77, "Eastbound"),
                ("Covent Garden", 1.40, "Eastbound"),
                ("Holborn", 1.55, "Eastbound"),
                ("Russell Square", 1.90, "Eastbound"),
                ("Kings Cross", 1.67, "Westbound"),
                ("Russell Square", 1.37, "Westbound"),
                ("Holborn", 1.53, "Westbound"),
                ("Covent Garden", 0.77, "Westbound"),
                ("Leicester Square", 1.07, "Westbound"),
                ("Piccadilly Circus", 1.18, "Westbound"),
                ("Green Park", 1.73, "Westbound"),
                ("Hyde Park Corner", 1.12, "Westbound"),
                ("Knightsbridge", 2.23, "Westbound"),
                ("South Kensington", 1.37, "Westbound"),
                ("Gloucester Road", 1.37, "Westbound"),
                ("Earls Court", 2.68, "Westbound"),
                ("Barons Court", 1.32, "Westbound"),
                ("Hammersmith", 0.0, "Westbound")
            });

            // Circle Line (Inner and Outer)
            var circleLine = new Line { Name = "Circle" };
            AddStationsAndConnections(circleLine, new (string, double, string)[]
            {
                ("Edgware Road", 1.85, "Inner"),
                ("Paddington (Circle)", 1.65, "Inner"),
                ("Bayswater", 1.47, "Inner"),
                ("Notting Hill Gate", 1.58, "Inner"),
                ("High Street Kensington", 1.80, "Inner"),
                ("Gloucester Road", 1.43, "Inner"),
                ("South Kensington", 1.98, "Inner"),
                ("Sloane Square", 1.80, "Inner"),
                ("Victoria", 1.42, "Inner"),
                ("St James Park", 1.50, "Inner"),
                ("Westminster", 1.37, "Inner"),
                ("Embankment", 1.37, "Inner"),
                ("Temple", 1.40, "Inner"),
                ("Blackfriars", 1.52, "Inner"),
                ("Mansion House", 0.98, "Inner"),
                ("Cannon Street", 0.97, "Inner"),
                ("Monument", 1.80, "Inner"),
                ("Tower Hill", 1.30, "Inner"),
                ("Aldgate", 1.75, "Inner"),
                ("Liverpool Street", 1.32, "Inner"),
                ("Moorgate", 1.38, "Inner"),
                ("Barbican", 1.20, "Inner"),
                ("Farringdon", 3.12, "Inner"),
                ("Kings Cross St Pancras", 1.65, "Inner"),
                ("Euston Square", 1.30, "Inner"),
                ("Great Portland Street", 1.57, "Inner"),
                ("Baker Street (Circle)", 1.88, "Inner"),
                ("Edgware Road", 2.08, "Outer"),
                ("Paddington (H&C)", 1.33, "Outer"),
                ("Royal Oak", 1.72, "Outer"),
                ("Westbourne Park", 1.48, "Outer"),
                ("Ladbroke Grove", 1.28, "Outer"),
                ("Latimer Road", 1.50, "Outer"),
                ("Wood Lane", 1.50, "Outer"),
                ("Shepherds Bush Market", 1.15, "Outer"),
                ("Goldhawk Road", 2.43, "Outer"),
                ("Hammersmith (H&C)", 2.05, "Outer"),
                ("Goldhawk Road", 1.15, "Outer"),
                ("Shepherds Bush Market", 1.50, "Outer"),
                ("Wood Lane", 1.50, "Outer"),
                ("Latimer Road", 1.50, "Outer"),
                ("Ladbroke Grove", 1.37, "Outer"),
                ("Westbourne Park", 1.48, "Outer"),
                ("Royal Oak", 1.78, "Outer"),
                ("Paddington (H&C)", 1.52, "Outer"),
                ("Edgware Road", 2.33, "Outer"),
                ("Baker Street", 1.47, "Outer"),
                ("Great Portland Street", 1.90, "Outer"),
                ("Euston Square", 1.25, "Outer"),
                ("Kings Cross St Pancras", 1.75, "Outer"),
                ("Farringdon", 2.98, "Outer"),
                ("Barbican", 1.22, "Outer"),
                ("Moorgate", 1.32, "Outer"),
                ("Liverpool Street", 1.18, "Outer"),
                ("Aldgate", 2.18, "Outer"),
                ("Tower Hill", 1.37, "Outer"),
                ("Monument", 1.48, "Outer"),
                ("Cannon Street", 0.88, "Outer"),
                ("Mansion House", 0.93, "Outer"),
                ("Blackfriars", 1.22, "Outer"),
                ("Temple", 1.37, "Outer"),
                ("Embankment", 1.43, "Outer"),
                ("Westminster", 1.40, "Outer"),
                ("St James Park", 1.52, "Outer"),
                ("Victoria", 1.33, "Outer"),
                ("Sloane Square", 1.75, "Outer"),
                ("South Kensington", 2.00, "Outer"),
                ("Gloucester Road", 1.48, "Outer"),
                ("High Street Kensington", 2.23, "Outer"),
                ("Notting Hill Gate", 1.68, "Outer"),
                ("Bayswater", 1.77, "Outer"),
                ("Paddington (Circle)", 1.63, "Outer"),
                ("Edgware Road", 2.15, "Outer")
        });

            // District Line
            var districtLine = new Line { Name = "District" };
            AddStationsAndConnections(districtLine, new (string, double, string)[]
            {
                ("Edgware Road", 1.85, "Westbound"),
                ("Paddington (Circle)", 1.65, "Westbound"),
                ("Bayswater", 1.47, "Westbound"),
                ("Notting Hill Gate", 1.58, "Westbound"),
                ("High Street Kensington", 2.72, "Westbound"),
                ("Earls Court", 3.05, "Eastbound"),
                ("High Street Kensington", 1.68, "Eastbound"),
                ("Notting Hill Gate", 1.77, "Eastbound"),
                ("Bayswater", 1.63, "Eastbound"),
                ("Paddington (Dis)", 2.15, "Eastbound"),
                ("Edgware Road", 0.0, "Eastbound")
            });

            // North London Overground Line
            var northOvergroundLine = new Line { Name = "North London Overground" };
            AddStationsAndConnections(northOvergroundLine, new (string, double, string)[]
            {
                ("Richmond", 26.0, "Southbound"),
                ("West Hampstead", 18.0, "Southbound"),
                ("Highbury", 18.0, "Northbound"),
                ("West Hampstead", 26.0, "Northbound"),
                ("Richmond", 0.0, "Northbound")
            });

            // Initialize interchanges based on specific known interchanges in the data
            AddInterchange("Baker Street", bakerlooLine.Stations[0], circleLine.Stations[13], 2.0, "Southbound", "Outer");
            AddInterchange("Victoria", victoriaLine.Stations[11], circleLine.Stations[8], 2.0, "Southbound", "Inner");
            AddInterchange("Oxford Circus", victoriaLine.Stations[8], bakerlooLine.Stations[2], 2.0, "Southbound", "Southbound");
            AddInterchange("Kings Cross St Pancras", victoriaLine.Stations[6], circleLine.Stations[21], 2.0, "Southbound", "Inner");
            AddInterchange("Euston", northernLine.Stations[0], victoriaLine.Stations[7], 2.0, "Southbound", "Southbound");
            AddInterchange("Highbury", victoriaLine.Stations[5], northOvergroundLine.Stations[2], 2.0, "Southbound", "Southbound");
            AddInterchange("Stratford", jubileeLine.Stations[26], northOvergroundLine.Stations[3], 2.0, "Eastbound", "Eastbound");
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