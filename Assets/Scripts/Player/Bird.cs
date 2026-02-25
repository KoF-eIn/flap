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
    [SerializeField] private BirdShoot _shoot;          
    [SerializeField] private InputReader _inputReader;  

    private void OnEnable()
    {
        _handler.CollisionDetected += ProcessCollision;
        _inputReader.JumpPressed += OnJumpPressed;   
    }

    private void OnDisable()
    {
        _handler.CollisionDetected -= ProcessCollision;
        _inputReader.JumpPressed -= OnJumpPressed;
    }

    private void OnJumpPressed()
    {
        if (!IsDead)
            _mover.Jump();
    }

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is Bullet bullet && bullet.Owner == Owner.Enemy)
        {
            IsDead = true;
            _shoot.Disable();  
            Died?.Invoke();
        }
    }

    public void Reset()
    {
        IsDead = false;
        _scoreCounter.Reset();
        _mover.Reset();
        _shoot.Enable();
    }
}