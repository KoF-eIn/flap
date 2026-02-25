using UnityEngine;

public class BirdShoot : MonoBehaviour
{
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private KeyCode _shootKey = KeyCode.X;
    [SerializeField] private float _cooldown = 0.3f;

    private float _lastShootTime;
    private bool _isActive = true;

    private void Update()
    {
        if (!_isActive) return;

        if (Input.GetKeyDown(_shootKey) && Time.time >= _lastShootTime + _cooldown)
        {
            _bulletSpawner.Spawn(_firePoint.position, transform.right, Owner.Player);
            _lastShootTime = Time.time;
        }
    }

    public void Disable()
    {
        _isActive = false;
    }

    public void Enable()
    {
        _isActive = true;
    }
}