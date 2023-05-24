using UnityEngine;

public abstract class Enemy : Creature
{
    protected Player Target { get; private set; }
    protected float MaxAttackDistance { get; private set; }

    protected Enemy(int health, Vector2 position, float rotation, float maxAttackDistance, Player target) : base(health, position, rotation)
    {
        Target = target;
        MaxAttackDistance = maxAttackDistance;
    }

    protected abstract bool TryAttackTarget(Player target, float deltaTime);

    protected Vector2 GetDirectionToTarget(Transformable target)
    {
        return (target.Position - Position) / GetDistanceToTarget(target);
    }

    protected float GetDistanceToTarget(Transformable target)
    {
        return Vector2.Distance(Position, target.Position);
    }
}
