using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SledController : MonoBehaviour, IUpdatable
{
    [SerializeField] private InputAction moveAction;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private Transform frontDog;

    private float _speed;

    private void OnEnable()
    {
        moveAction.Enable();
        UpdateManager.Register(this);
    }

    private void OnDisable()
    {
        moveAction.Disable();
        UpdateManager.Unregister(this);
    }

    public void OnUpdate(float deltaTime)
    {
        HandleHorizontalInput(deltaTime);
    }
    
    private void HandleHorizontalInput(float deltaTime)
    {
        var move = moveAction.ReadValue<Vector2>();

        _speed += acceleration * move.x * deltaTime;
        _speed = Mathf.Clamp(_speed, -maxSpeed, maxSpeed);
        
        // frontDog.transform.position += _speed * deltaTime * Vector3.right;
    }
}
