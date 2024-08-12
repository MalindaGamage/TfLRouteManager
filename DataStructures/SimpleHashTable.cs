using TfLRouteManager.Models;

public class SimpleHashTable
{
    private KeyValuePair[] _buckets;
    private int _count;
    private const double LoadFactorThreshold = 0.75;

    public SimpleHashTable(int initialSize)
    {
        _buckets = new KeyValuePair[initialSize];
        _count = 0;
    }

    public void Add(string key, Station value)
    {
        if (_count >= _buckets.Length * LoadFactorThreshold)
        {
            Resize();
        }

        int index = GetIndex(key, _buckets.Length);

        // Handle collisions by linear probing
        while (_buckets[index] != null && _buckets[index].Key != key)
        {
            index = (index + 1) % _buckets.Length;
        }

        if (_buckets[index] == null)
        {
            _count++;
        }

        _buckets[index] = new KeyValuePair(key, value);
    }

    public Station Get(string key)
    {
        int index = GetIndex(key.ToLower(), _buckets.Length);

        // Handle collisions by linear probing
        while (_buckets[index] != null)
        {
            if (_buckets[index].Key == key)
            {
                return _buckets[index].Value;
            }
            index = (index + 1) % _buckets.Length;
        }

        return null;
    }

    private void Resize()
    {
        int newSize = _buckets.Length * 2;
        var newBuckets = new KeyValuePair[newSize];

        foreach (var pair in _buckets)
        {
            if (pair != null)
            {
                int newIndex = GetIndex(pair.Key, newSize);

                // Handle collisions by linear probing
                while (newBuckets[newIndex] != null)
                {
                    newIndex = (newIndex + 1) % newSize;
                }

                newBuckets[newIndex] = pair;
            }
        }

        _buckets = newBuckets;
    }

    private int GetIndex(string key, int arrayLength)
    {
        int hash = key.GetHashCode();
        return (hash & 0x7FFFFFFF) % arrayLength;
    }

    private class KeyValuePair
    {
        public string Key { get; }
        public Station Value { get; }

        public KeyValuePair(string key, Station value)
        {
            Key = key;
            Value = value;
        }
    }
}
