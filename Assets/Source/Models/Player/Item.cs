using System;
using UnityEngine;

public class Item : Transformable
{
    public readonly string Name;

    public event Action Dropped;
    public event Action Collected;

    public Item(Vector2 position, float rotation, string name) : base(position, rotation)
    {
        Name = name;
    }

    public void Drop(Vector2 position)
    {
        MoveTo(position);
        Dropped?.Invoke();
    }

    public void PickUp()
    {
        Collected?.Invoke();
    }
}
