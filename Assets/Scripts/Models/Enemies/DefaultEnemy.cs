using System;
using UnityEngine;

public class DefaultEnemy : Enemy, IUpdatable
{
    private int _damage;
    private float _velocity;
    private float _elapsedTime;

    public Vector2 Direction { get; private set; }
    public bool IsMooving { get; private set; }

    public event Action Attacking;

    public DefaultEnemy(int health, Vector2 position, float rotation, float maxAttackDistance, int damage, float velocity, Player target) 
        : base(health, position, rotation, maxAttackDistance, target)
    {
        IsMooving = false;
        _elapsedTime = Config.DefaultEnemyAttackDelay;
        _damage = damage;
        _velocity = velocity;
    }

    public void Update(float deltaTime)
    {
        _elapsedTime -= deltaTime;

        if (GetDistanceToTarget(Target) <= Config.DefaultEnemyMaxDistanceToChase)
        {
            IsMooving = true;
            TryAttackTarget(Target, deltaTime);
        }
        else
        {
            IsMooving = false;
        }
    }

    protected override bool TryAttackTarget(Player target, float deltaTime)
    {
        if (GetDistanceToTarget(target) <= MaxAttackDistance)
        {
            IsMooving = false;

            if (_elapsedTime <= 0)
            {
                Attacking?.Invoke();
                target.ApplyDamage(_damage);
                _elapsedTime = Config.DefaultEnemyAttackDelay;
                return true;
            }
        }
        else
        {
            MoveToTarget(target, deltaTime);
        }

        return false;
    }

    private void MoveToTarget(Transformable target, float deltaTime)
    {
        Vector2 direction = GetDirectionToTarget(target);
        Direction = direction;
        MoveTo(Position + direction * _velocity * deltaTime);
    }
}
