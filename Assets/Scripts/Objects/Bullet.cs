using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Bullet : PoolableObject
{
    [SerializeField] private float _speed = 15f;
    [SerializeField] private float _lifeTime = 3f;

    public Owner Owner { get; private set; }
    private Rigidbody2D _rb;
    private float _spawnTime;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 position, Vector2 direction, Owner owner)
    {
        transform.position = position;
        _rb.velocity = direction.normalized * _speed;
        Owner = owner;
        _spawnTime = Time.time;
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
        Invoke(nameof(ReturnToPool), _lifeTime);
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        CancelInvoke();
    }

    private void ReturnToPool()
    {
        BulletSpawner.Instance.ReturnBullet(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Owner == Owner.Player && other.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.Die();
            ReturnToPool();
        }

        if (other.CompareTag("Boundary"))
        {
            ReturnToPool();
        }
    }
}