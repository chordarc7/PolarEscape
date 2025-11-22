using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class DogController : MonoBehaviour, IUpdatable
{
    [SerializeField] private InputAction moveAction;
    [SerializeField] private float horizontalMaxSpeed;
    [SerializeField] private float horizontalAcceleration;
    [SerializeField] private float verticalAcceleration;

    private float _horizontalSpeed;
    private float _verticalSpeed;
    private bool _accelerating;

    private void OnEnable()
    {
        moveAction.Enable();
        TouchSimulation.Enable();
        EnhancedTouchSupport.Enable();

        Touch.onFingerDown += OnFingerDown;
        Touch.onFingerUp += OnFingerUp;
        
        UpdateManager.Register(this);
    }

    private void OnDisable()
    {
        moveAction.Disable();
        
        Touch.onFingerDown -= OnFingerDown;
        Touch.onFingerUp -= OnFingerUp;
        
        TouchSimulation.Disable();
        EnhancedTouchSupport.Disable();
        
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
        var x = moveAction.ReadValue<Vector2>().x;
#elif UNITY_ANDROID
        var x = TiltInputManager.Tilt();
#endif

        _horizontalSpeed += horizontalAcceleration * x * deltaTime;
        _horizontalSpeed = Mathf.Clamp(_horizontalSpeed, -horizontalMaxSpeed, horizontalMaxSpeed);
        
        transform.position += _horizontalSpeed * deltaTime * Vector3.right;
    }
    
    private void HandleVerticalInput(float deltaTime)
    {
        var y = _accelerating ? 1f : -1f;
        
        _verticalSpeed += verticalAcceleration * y * deltaTime;
        _verticalSpeed = Mathf.Max(_verticalSpeed, 0);
        
        transform.position += _verticalSpeed * deltaTime * Vector3.up;
    }

    private void OnFingerDown(Finger finger)
    {
        _accelerating = true;
    }
    
    private void OnFingerUp(Finger finger)
    {
        _accelerating = false;
    }
}
