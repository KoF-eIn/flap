using System;
using UnityEngine;

[RequireComponent(typeof(BirdMover))]
[RequireComponent(typeof(ScoreCounter))]
[RequireComponent(typeof(BirdCollisionHandler))]
[RequireComponent(typeof(BirdShoot))]
public class Bird : MonoBehaviour
{
    public event Action Died;
    public bool IsDead { get; private set; }

    [SerializeField] private BirdMover _mover;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private BirdCollisionHandler _handler;

    private void OnEnable() => _handler.CollisionDetected += ProcessCollision;
    private void OnDisable() => _handler.CollisionDetected -= ProcessCollision;

    private void Update()
    {
        if (IsDead) return;
        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
            _mover.Jump();
    }

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is Bullet bullet && bullet.Owner == Owner.Enemy)
        {
            IsDead = true;
            Died?.Invoke();
        }
    }

    public void Reset()
    {
        IsDead = false;
        _scoreCounter.Reset();
        _mover.Reset();
    }
}