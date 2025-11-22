using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class DogController : MonoBehaviour, IUpdatable
{
    [SerializeField] private InputAction moveAction;
    [SerializeField] private float horizontalMaxSpeed;
    [SerializeField] private float horizontalAcceleration;
    [SerializeField] private float verticalAcceleration;

    private float _horizontalSpeed;
    private float _verticalSpeed;

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
        HandleVerticalInput(deltaTime);
    }
    
    private void HandleHorizontalInput(float deltaTime)
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        var move = moveAction.ReadValue<Vector2>();
#elif UNITY_ANDROID
        var move = TiltInputManager.Tilt(deltaTime);
#endif

        _horizontalSpeed += horizontalAcceleration * move.x * deltaTime;
        _horizontalSpeed = Mathf.Clamp(_horizontalSpeed, -horizontalMaxSpeed, horizontalMaxSpeed);
        
        transform.position += _horizontalSpeed * deltaTime * Vector3.right;
    }
    
    private void HandleVerticalInput(float deltaTime)
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        var move = moveAction.ReadValue<Vector2>();
#elif UNITY_ANDROID
        var move = TiltInputManager.Tilt(deltaTime);
#endif
        
        _verticalSpeed += verticalAcceleration * move.y * deltaTime;
        _verticalSpeed = Mathf.Max(_verticalSpeed, 0);
        
        transform.position += _verticalSpeed * deltaTime * Vector3.up;
    }
}
