using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : Creature
{
    private readonly float _velocity;
    private Weapon _weapon;
    private Inventory _inventory;
    private Vector2 _leftBottomBoundry;
    private Vector2 _rightTopBoundry;

    public event Action WeaponShot;

    public Player(int health, Vector2 position, float rotation, float velocity, Weapon weapon, Vector2 leftBottomBoundry, 
        Vector2 rightTopBoundry, Inventory inventory) : base(health, position, rotation)
    {
        _inventory = inventory;
        _leftBottomBoundry = leftBottomBoundry;
        _rightTopBoundry = rightTopBoundry;
        _velocity = velocity;
        _weapon = weapon;
    }

    public void Shoot()
    {
        if (_inventory.HasAmmo())
            if (_weapon.TryShoot())
            {
                _inventory.Shoot();
                WeaponShot?.Invoke();
            }
    }

    public void Move(Vector2 direction, float deltaTime)
    {
        _weapon.RotateToDirection(direction);
        Vector2 newPosition = direction * _velocity * deltaTime;

        if (CanMove(Position + newPosition))
            MovePlayer(newPosition);
    }

    public void TryPickUpItem(Item item)
    {
        if (Vector2.Distance(Position, item.Position) <= Config.MinDistanceToCollectItem)
        {
            _inventory.CollectItem(item);
        }
    }

    public void AddItem(Item item)
    {
        _inventory.CollectItem(item);
    }

    public void ClearSavedResult()
    {
        MoveTo(Vector2.zero);
        GameSaver.Save(this, new Inventory(new List<Cell>()));
    }

    public void SaveResult()
    {
        GameSaver.Save(this, _inventory);
    }

    private void MovePlayer(Vector2 newPosition)
    {
        MoveTo(Position + newPosition);
        _weapon.MoveTo(_weapon.Position + newPosition);
    }

    private bool CanMove(Vector2 position)
    {
        return ((position.x < _rightTopBoundry.x) && (position.x > _leftBottomBoundry.x) && 
            (position.y < _rightTopBoundry.y) && (position.y > _leftBottomBoundry.y));
    }
}
