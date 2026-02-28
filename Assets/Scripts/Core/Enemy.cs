using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Enemy : MonoBehaviour, IInteractable
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _minShootDelay = 1f;
    [SerializeField] private float _maxShootDelay = 3f;

    private Rigidbody2D _rb;
    private BulletSpawner _bulletSpawner;
    private EnemySpawner _enemySpawner;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _rb.velocity = Vector2.left * _moveSpeed;
        ScheduleNextShot();
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    public void Init(BulletSpawner bulletSpawner, EnemySpawner enemySpawner)
    {
        _bulletSpawner = bulletSpawner;
        _enemySpawner = enemySpawner;
    }

    private void ScheduleNextShot()
    {
        float delay = Random.Range(_minShootDelay, _maxShootDelay);
        Invoke(nameof(Shoot), delay);
    }

    private void Shoot()
    {
        if (gameObject.activeInHierarchy)
        {
            _bulletSpawner.Spawn(transform.position, Vector2.left, Owner.Enemy);
            ScheduleNextShot();
        }
    }

    public void Die()
    {
        _enemySpawner.NotifyEnemyDied(this);
        _enemySpawner.Despawn(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Boundary>(out _))
        {
            _enemySpawner.Despawn(this);
        }
    }
}