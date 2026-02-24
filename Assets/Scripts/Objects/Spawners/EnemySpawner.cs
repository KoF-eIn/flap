using UnityEngine;

public class EnemySpawner : Spawner<Enemy>
{
    public static EnemySpawner Instance { get; private set; }

    [Header("Spawn Settings")]
    [SerializeField] private float _spawnInterval = 2f;
    [SerializeField] private float _spawnX = 12f;
    [SerializeField] private float _minY = -4f;
    [SerializeField] private float _maxY = 4f;

    private void Awake()
    {
        base.Awake();
        Instance = this;
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, _spawnInterval);
    }

    private void SpawnEnemy()
    {
        float randomY = Random.Range(_minY, _maxY);
        Vector2 spawnPos = new Vector2(_spawnX, randomY);
        Spawn(spawnPos, Quaternion.identity);
    }

    public void ReturnEnemy(Enemy enemy)
    {
        Despawn(enemy);
    }
}