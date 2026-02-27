using UnityEngine;

public class BirdShoot : MonoBehaviour
{
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _cooldown = 0.3f;

    private float _lastShootTime;
    private bool _isActive = true;

    public void Shoot()
    {
        if (!_isActive) return;

        if (Time.time < _lastShootTime + _cooldown) return;

        _bulletSpawner.Spawn(_firePoint.position, transform.right, Owner.Player);
        _lastShootTime = Time.time;
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