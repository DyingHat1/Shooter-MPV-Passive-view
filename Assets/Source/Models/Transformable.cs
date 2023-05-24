using System;
using UnityEngine;

public abstract class Transformable
{
    public Vector2 Position { get; private set; }
    public float Rotation { get; private set; }

    public event Action Moved;
    public event Action Rotated;
    public event Action Destroyed;

    public Transformable(Vector2 position, float rotation)
    {
        Position = position;
        Rotation = rotation;
    }

    public virtual void MoveTo(Vector2 position)
    {
        Position = new Vector3(position.x, position.y, 0);
        Moved?.Invoke();
    }

    public void Rotate(float delta)
    {
        Rotation = delta;
        Rotated?.Invoke();
    }

    public void Destroy()
    {
        Destroyed?.Invoke();
    }
}
