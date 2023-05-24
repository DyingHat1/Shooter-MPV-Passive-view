using UnityEngine;

public class Rifle : Weapon
{
    public Rifle(Vector2 position, float rotation, float shootDelay, int damage, float bulletSpeed) : base(position, rotation, shootDelay, damage, bulletSpeed, "Rifle") { }
}
