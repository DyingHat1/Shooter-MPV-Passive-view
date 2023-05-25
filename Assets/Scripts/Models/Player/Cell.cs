using System;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    private Stack<Item> _items = new Stack<Item>();

    public int Count => _items.Count;
    public string Name { get; private set; }

    public event Action ItemsChanged;

    public Cell() 
    {
        Name = Config.EmptyItemSlotName;
    }

    public void AddItem(Item item)
    {
        if (Count == 0)
        {
            Name = item.Name;
        }

        _items.Push(item);
        item.PickUp();
        ItemsChanged?.Invoke();
    }

    public void SpendItem()
    {
        Item item = _items.Pop();
        item.Destroy();
        OnItemsChanged();
    }

    public void Drop(Vector2 position)
    {
        Item item = _items.Pop();
        item.Drop(position);
        OnItemsChanged();
    }

    private void OnItemsChanged()
    {
        if (Count == 0)
        {
            Name = Config.EmptyItemSlotName;
        }

        ItemsChanged?.Invoke();
    }
}
