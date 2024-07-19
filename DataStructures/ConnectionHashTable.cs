using System;
using TfLRouteManager.Models;
public class ConnectionHashTable
{
    private class HashNode
    {
        public string Key { get; set; }
        public Connection Value { get; set; }
        public HashNode Next { get; set; }
    }

    private HashNode[] _buckets;

    public ConnectionHashTable(int size)
    {
        _buckets = new HashNode[size];
    }

    private int GetBucketIndex(string key)
    {
        int hashCode = key.GetHashCode();
        int bucketIndex = Math.Abs(hashCode) % _buckets.Length;
        return bucketIndex;
    }

    public void Add(string key, Connection value)
    {
        int bucketIndex = GetBucketIndex(key);
        var newNode = new HashNode { Key = key, Value = value, Next = _buckets[bucketIndex] };
        _buckets[bucketIndex] = newNode;
    }

    public Connection Get(string key)
    {
        int bucketIndex = GetBucketIndex(key);
        var currentNode = _buckets[bucketIndex];
        while (currentNode != null)
        {
            if (currentNode.Key == key)
            {
                return currentNode.Value;
            }
            currentNode = currentNode.Next;
        }
        return null;
    }
}
