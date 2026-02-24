using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Enemy : PoolableObject
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 3f;

    [Header("Shooting")]
    [SerializeField] private float _minShootDelay = 1f;
    [SerializeField] private float _maxShootDelay = 3f;

    private Rigidbody2D _rb;
    private BulletSpawner _bulletSpawner;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boundary"))
        {
            EnemySpawner.Instance.ReturnEnemy(this);
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _bulletSpawner = FindObjectOfType<BulletSpawner>();
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
        _rb.velocity = Vector2.left * _moveSpeed;
        ScheduleNextShot();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        CancelInvoke();
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
            _bulletSpawner.SpawnBullet(transform.position, Vector2.left, Owner.Enemy);
            ScheduleNextShot();
        }
    }

    public void Die()
    {
        Game.Instance.AddScore(1);  
        EnemySpawner.Instance.ReturnEnemy(this);
    }
}