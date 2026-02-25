using UnityEngine;
using System;

public class InputReader : MonoBehaviour
{
    public event Action JumpPressed;
    public event Action ShootPressed;

    private void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
            JumpPressed?.Invoke();

        if (Input.GetKeyDown(KeyCode.X))
            ShootPressed?.Invoke();
    }
}