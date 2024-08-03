namespace TfLRouteManager.Models
{
    public class StationHashTable
    {
        private Station[] _stations;
        private int _size;

        public StationHashTable(int size)
        {
            _size = size;
            _stations = new Station[_size];
        }

        private int Hash(string key)
        {
            // A simple hash function that ensures the hash value fits within the table size
            int hash = 0;
            foreach (var c in key)
            {
                hash = (hash * 31 + c) % _size;
            }
            return hash;
        }

        public void Add(string key, Station station)
        {
            int index = Hash(key);
            while (_stations[index] != null)
            {
                index = (index + 1) % _size;
            }
            _stations[index] = station;
        }

        public Station Get(string key)
        {
            int index = Hash(key);
            int startIndex = index;
            while (_stations[index] != null)
            {
                if (_stations[index].Name == key)
                {
                    return _stations[index];
                }
                index = (index + 1) % _size;
                if (index == startIndex) // We looped back to the start
                {
                    break;
                }
            }
            return null;
        }
    }
}
