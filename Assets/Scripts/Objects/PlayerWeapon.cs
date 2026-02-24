using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerWeapon : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] private KeyCode _shootKey = KeyCode.X;
    [SerializeField] private float _shootCooldown = 0.3f;
    [SerializeField] private Transform _firePoint;

    private float _lastShootTime;

    private Player _player;

    private BulletSpawner _bulletSpawner;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _bulletSpawner = FindObjectOfType<BulletSpawner>();

        if (_firePoint == null) _firePoint = transform;
    }

    private void Update()
    {
        if (_player.IsDead) return;

        if (Input.GetKeyDown(_shootKey) && Time.time >= _lastShootTime + _shootCooldown)
        {
            Shoot();
            _lastShootTime = Time.time;
        }
    }

    private void Shoot()
    {
        Vector2 direction = transform.right;
        _bulletSpawner.SpawnBullet(_firePoint.position, direction, Owner.Player);
    }
}