using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _jumpForce = 8f;
    [SerializeField] private float _gravityScale = 2f;

    private Rigidbody2D _rb;
    private bool _isDead;

    public bool IsDead => _isDead;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = _gravityScale;
        _rb.freezeRotation = true;
    }

    private void Update()
    {
        if (_isDead) return;
        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
        }
    }

    public void Die()
    {
        if (_isDead) return;
        _isDead = true;
        GetComponent<Collider2D>().enabled = false;
        Game.Instance.GameOver();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Bullet>(out var bullet) && bullet.Owner == Owner.Enemy)
        {
            Die();
        }
    }
}