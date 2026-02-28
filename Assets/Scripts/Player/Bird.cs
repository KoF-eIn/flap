using System;
using UnityEngine;

[RequireComponent(typeof(BirdMover))]
[RequireComponent(typeof(ScoreCounter))]
[RequireComponent(typeof(BirdCollisionHandler))]
[RequireComponent(typeof(BirdShoot))]
public class Bird : MonoBehaviour
{
    public event Action Died;

    [SerializeField] private BirdMover _mover;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private BirdCollisionHandler _handler;
    [SerializeField] private BirdShoot _shoot;
    [SerializeField] private InputReader _inputReader;

    private bool _isDead;

    public bool IsDead => _isDead;

    private void OnEnable()
    {
        _handler.CollisionDetected += ProcessCollision;
        _inputReader.JumpPressed += OnJumpPressed;
        _inputReader.ShootPressed += OnShootPressed;
    }

    private void OnDisable()
    {
        _handler.CollisionDetected -= ProcessCollision;
        _inputReader.JumpPressed -= OnJumpPressed;
        _inputReader.ShootPressed -= OnShootPressed;
    }

    private void OnJumpPressed()
    {
        if (!_isDead)
        {
            _mover.Jump();
        }
    }

    private void OnShootPressed()
    {
        if (!_isDead)
            _shoot.Shoot();
    }

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is Bullet bullet && bullet.Owner == Owner.Enemy)
        {
            _isDead = true;
            _shoot.Disable();
            Died?.Invoke();
        }
    }

    public void Reset()
    {
        _isDead = false;
        _scoreCounter.Reset();
        _mover.Reset();
        _shoot.Enable();
    }
}