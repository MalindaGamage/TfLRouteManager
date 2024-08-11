using System;

public class DynamicArray<T>
{
    private T[] _items;
    private int _size;

    public DynamicArray(int capacity = 4)
    {
        _items = new T[capacity];
        _size = 0;
    }

    public int Count => _size;

    public void Add(T item)
    {
        if (_size == _items.Length)
        {
            Resize();
        }
        _items[_size++] = item;
    }

    public T Get(int index)
    {
        if (index < 0 || index >= _size)
        {
            throw new IndexOutOfRangeException();
        }
        return _items[index];
    }

    public T[] ToArray()
    {
        T[] result = new T[_size];
        for (int i = 0; i < _size; i++)
        {
            result[i] = _items[i];
        }
        return result;
    }

    private void Resize()
    {
        int newCapacity = _items.Length * 2;
        T[] newArray = new T[newCapacity];
        for (int i = 0; i < _items.Length; i++)
        {
            newArray[i] = _items[i];
        }
        _items = newArray;
    }
}
