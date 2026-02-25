using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Bullet _prefab;
    [SerializeField] private Transform _container;
    [SerializeField] private int _initialPoolSize = 10;

    private ObjectPool<Bullet> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Bullet>(_prefab, _container, _initialPoolSize);
    }

    public void Spawn(Vector2 position, Vector2 direction, Owner owner)
    {
        Bullet bullet = _pool.Get(position, Quaternion.identity);
        bullet.Init(this);
        bullet.Initialize(position, direction, owner);
    }

    public void Return(Bullet bullet) => _pool.Return(bullet);

    public void Reset() => _pool.Reset();
}