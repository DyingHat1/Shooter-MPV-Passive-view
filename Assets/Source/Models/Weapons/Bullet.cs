using System;
using UnityEngine;

public class Bullet : Transformable, IUpdatable
{
    private int _damage;
    private float _velocity;
    private Vector2 _direction;

    public Bullet(Vector2 position, float rotation, Vector2 direction, float velocity, int damage) : base(position, rotation)
    {
        _velocity = velocity;
        _direction = direction;
        _damage = damage;
    }

    public void Update(float deltaTime)
    {
        MoveTo(Position + _direction * _velocity * deltaTime);
    }

    public void HitTarget(Creature target)
    {
        target.ApplyDamage(_damage);
        Destroy();
    }
}
