using UnityEngine;

public class BulletSpawner : Spawner<Bullet>
{
    public static BulletSpawner Instance { get; private set; }

    private void Awake()
    {
        base.Awake();
        Instance = this;
    }

    public void SpawnBullet(Vector2 position, Vector2 direction, Owner owner)
    {
        Bullet bullet = Spawn(position, Quaternion.identity);
        bullet.Initialize(position, direction, owner);
    }

    public void ReturnBullet(Bullet bullet)
    {
        Despawn(bullet);
    }
}