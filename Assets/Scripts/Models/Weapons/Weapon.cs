using System;
using UnityEngine;

public abstract class Weapon : Item, IUpdatable
{
    protected readonly int Damage;
    protected readonly float BulletSpeed;
    private readonly float _shootDelay;
    private float _elapsedTime;
    private Vector2 _shootDirection;

    public event Action<Bullet> Shot;

    public Weapon(Vector2 position, float rotation, float shootDelay, int damage, float bulletSpeed, string name) 
        : base(position, rotation, name)
    {
        _shootDirection = Vector2.right;
        Damage = damage;
        BulletSpeed = bulletSpeed;
        _shootDelay = shootDelay;
        _elapsedTime = shootDelay;
    }

    public void Update(float deltaTime)
    {
        _elapsedTime += deltaTime;
    }

    public bool TryShoot()
    {
        if (_elapsedTime >= _shootDelay)
        {
            Shoot();
            _elapsedTime = 0;
            return true;
        }

        return false;
    }

    public void RotateToDirection(Vector2 direction)
    {
        _shootDirection = direction;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Rotate(angle);
    }

    protected Vector2 GetBulletDirection()
    {
        return _shootDirection.normalized;
    }

    protected virtual void Shoot()
    {
        Vector2 direction = GetBulletDirection();
        Bullet bullet = new Bullet(Position, Rotation, direction, BulletSpeed, Damage);
        Shot?.Invoke(bullet);
    }
}
