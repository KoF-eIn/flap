using UnityEngine;

public class BirdMover : MonoBehaviour
{
    [SerializeField] private float _tapForce;
    [SerializeField] private float _speed;

    private Vector3 _startPosition;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        if (_rigidbody2D == null)
            Debug.LogError("Rigidbody2D not found on Bird!");
    }

    private void Start()
    {
        _startPosition = transform.position;
        Reset();
    }

    public void Jump()
    {
        if (_rigidbody2D == null) return;

        _rigidbody2D.velocity = new Vector2(_speed, _tapForce);
    }

    public void Reset()
    {
        transform.position = _startPosition;
        transform.rotation = Quaternion.identity;
        _rigidbody2D.velocity = Vector2.zero;
    }
}