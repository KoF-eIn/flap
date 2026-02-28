using UnityEngine;
using System.Collections;
using System;

public class EnemySpawner : Spawner<Enemy>
{
    public event Action<Enemy> EnemyDied;

    [SerializeField] private float _spawnInterval = 2f;
    [SerializeField] private float _spawnX = 12f;
    [SerializeField] private float _minY = -4f;
    [SerializeField] private float _maxY = 4f;

    private Coroutine _spawnCoroutine;
    private BulletSpawner _bulletSpawner;

    public void Initialize(BulletSpawner bulletSpawner)
    {
        _bulletSpawner = bulletSpawner;
    }

    private void Start()
    {
        StartSpawning();
    }

    private void StartSpawning()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
        }
        _spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(_spawnInterval);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        float y = UnityEngine.Random.Range(_minY, _maxY);
        Vector2 position = new Vector2(_spawnX, y);
        Enemy enemy = Spawn(position, Quaternion.identity);
        enemy.Init(_bulletSpawner, this);
    }

    public void NotifyEnemyDied(Enemy enemy)
    {
        EnemyDied?.Invoke(enemy);
    }

    public void StopSpawning()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
    }

    public override void ResetPool()
    {
        StopSpawning();
        base.ResetPool();
        StartSpawning();
    }
}