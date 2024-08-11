using System.Collections.Generic;
using TfLRouteManager.Models;

public class SimpleHashTable
{
    private LinkedList<KeyValuePair<string, Station>>[] _buckets;
    private int _count;
    private const double LoadFactorThreshold = 0.75;

    public SimpleHashTable(int initialSize)
    {
        _buckets = new LinkedList<KeyValuePair<string, Station>>[initialSize];
        _count = 0;
    }

    public void Add(string key, Station value)
    {
        if (_count >= _buckets.Length * LoadFactorThreshold)
        {
            Resize();
        }

        int index = GetIndex(key, _buckets.Length);
        if (_buckets[index] == null)
        {
            _buckets[index] = new LinkedList<KeyValuePair<string, Station>>();
        }

        // Check if the key already exists and update the value
        var bucket = _buckets[index];
        var existingKvp = default(KeyValuePair<string, Station>?);

        foreach (var kvp in bucket)
        {
            if (kvp.Key == key)
            {
                existingKvp = kvp;
                break;
            }
        }

        if (existingKvp.HasValue)
        {
            bucket.Remove(existingKvp.Value);
            _count--; // Decrease count before adding the updated key-value pair
        }

        bucket.AddLast(new KeyValuePair<string, Station>(key, value));
        _count++;
    }

    public Station Get(string key)
    {
        int index = GetIndex(key, _buckets.Length);
        if (_buckets[index] != null)
        {
            foreach (var kvp in _buckets[index])
            {
                if (kvp.Key == key)
                {
                    return kvp.Value;
                }
            }
        }
        return null;
    }

    private void Resize()
    {
        int newSize = _buckets.Length * 2;
        var newBuckets = new LinkedList<KeyValuePair<string, Station>>[newSize];

        foreach (var bucket in _buckets)
        {
            if (bucket != null)
            {
                foreach (var kvp in bucket)
                {
                    int newIndex = GetIndex(kvp.Key, newSize);
                    if (newBuckets[newIndex] == null)
                    {
                        newBuckets[newIndex] = new LinkedList<KeyValuePair<string, Station>>();
                    }
                    newBuckets[newIndex].AddLast(kvp);
                }
            }
        }

        _buckets = newBuckets;
    }

    private int GetIndex(string key, int arrayLength)
    {
        int hash = key.GetHashCode();
        return (hash & 0x7FFFFFFF) % arrayLength;
    }
}
