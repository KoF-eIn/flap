using UnityEngine;

public class BulletSpawner : Spawner<Bullet>
{
    public void Spawn(Vector2 position, Vector2 direction, Owner owner)
    {
        Bullet bullet = Spawn(position, Quaternion.identity);
        bullet.Init(this);
        bullet.Initialize(position, direction, owner);
    }
}