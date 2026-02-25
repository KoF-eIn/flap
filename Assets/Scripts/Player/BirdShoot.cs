using UnityEngine;

[RequireComponent(typeof(Bird))]
public class BirdShoot : MonoBehaviour
{
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private KeyCode _shootKey = KeyCode.X;
    [SerializeField] private float _cooldown = 0.3f;

    private Bird _bird;
    private float _lastShootTime;
    private bool _isActive = true;

    private void Awake() => _bird = GetComponent<Bird>();

    private void OnEnable() => _bird.Died += OnBirdDied;
    private void OnDisable() => _bird.Died -= OnBirdDied;

    private void OnBirdDied() => _isActive = false;

    private void Update()
    {
        if (!_isActive) return;

        if (Input.GetKeyDown(_shootKey) && Time.time >= _lastShootTime + _cooldown)
        {
            _bulletSpawner.Spawn(_firePoint.position, transform.right, Owner.Player);
            _lastShootTime = Time.time;
        }
    }
}