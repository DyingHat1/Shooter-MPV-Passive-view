using System;
using UnityEngine;

public abstract class Creature : Transformable
{
    public int Health { get; private set; }

    public event Action<Creature> Died;
    public event Action HealthChanged;

    public Creature(int health, Vector2 position, float rotation) : base(position, rotation)
    {
        Health = health;
    }

    public void ApplyDamage(int damage)
    {
        if (damage >= Health)
        {
            Health = 0;
            Die();
        }
        else
        {
            Health -= damage;
        }

        HealthChanged?.Invoke();
    }

    protected virtual void Die()
    {
        Died?.Invoke(this);
        Destroy();
    }
}
