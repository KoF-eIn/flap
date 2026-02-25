using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Bullet : MonoBehaviour, IInteractable
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _lifeTime = 3f;

    public Owner Owner { get; private set; }

    private Rigidbody2D _rb;
    private BulletSpawner _spawner;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Boundary>(out _))
        {
            ReturnToPool();

            return;
        }

        if (Owner == Owner.Player && other.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.Die();
            ReturnToPool();
        }
        else if (Owner == Owner.Enemy && other.TryGetComponent<Bird>(out var bird))
        {
            ReturnToPool();
        }
    }

    public void Init(BulletSpawner spawner)
    {
        _spawner = spawner;
    }

    public void Initialize(Vector2 position, Vector2 direction, Owner owner)
    {
        transform.position = position;

        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody2D>();

            if (_rb == null)
            {
                Debug.LogError("Rigidbody2D is missing on bullet! Please add it to the prefab.");
                return;
            }
        }

        _rb.velocity = direction.normalized * _speed;
        Owner = owner;

        Invoke(nameof(ReturnToPool), _lifeTime);
    }

    private void ReturnToPool() => _spawner.Return(this);
}