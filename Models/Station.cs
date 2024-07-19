namespace TfLRouteManager.Models
{
    public class Station
    {
        public string Name { get; set; }
        public Connection[] Connections { get; private set; } = new Connection[50];
        public int ConnectionCount { get; private set; } = 0;

        public void AddConnection(Connection connection)
        {
            if (ConnectionCount < Connections.Length)
            {
                Connections[ConnectionCount++] = connection;
            }
        }

        public Connection FindConnection(Station to)
        {
            for (int i = 0; i < ConnectionCount; i++)
            {
                if (Connections[i].To == to)
                {
                    return Connections[i];
                }
            }
            return null;
        }
    }
}
