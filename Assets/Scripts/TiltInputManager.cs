using UnityEngine;
using UnityEngine.InputSystem;

public class TiltInputManager : MonoBehaviour
{
    private static TiltInputManager Instance { get; set; }
    
    public static float XOffset { get; set; }
    public static float Sensitivity = 1f;
    public static float DeadZone = 0.1f;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
        
        DontDestroyOnLoad(this);
    }
    
    private void Start()
    {
        InputSystem.EnableDevice(GravitySensor.current);
    }
    
    public static float Tilt()
    {
        // -1: left, 1: right
        var x = GravitySensor.current.gravity.ReadValue().x;
        
        // calibration
        x += XOffset;

        // dead zone
        if (Mathf.Abs(x) < DeadZone) x = 0;

        // sensitivity
        x *= Mathf.Max(Sensitivity, 0.01f);
        x = Mathf.Clamp(x, -1f, 1f);

        return x;
    }
}
