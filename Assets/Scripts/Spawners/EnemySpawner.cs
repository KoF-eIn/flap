using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private Transform _container;
    [SerializeField] private float _spawnInterval = 2f;
    [SerializeField] private float _spawnX = 12f;
    [SerializeField] private float _minY = -4f;
    [SerializeField] private float _maxY = 4f;
    [SerializeField] private int _initialPoolSize = 5;

    [SerializeField] private Game _game;
    [SerializeField] private BulletSpawner _bulletSpawner;

    private ObjectPool<Enemy> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(_prefab, _container, _initialPoolSize);
    }

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 1f, _spawnInterval);
    }

    private void Spawn()
    {
        float y = Random.Range(_minY, _maxY);
        Vector2 position = new Vector2(_spawnX, y);
        Enemy enemy = _pool.Get(position, Quaternion.identity);
        enemy.Init(_game, _bulletSpawner, this);
    }

    public void Return(Enemy enemy) => _pool.Return(enemy);

    public void Reset() => _pool.Reset();
}