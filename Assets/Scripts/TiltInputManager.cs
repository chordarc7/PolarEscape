using UnityEngine;

public class TiltInputManager
{
    public static float XOffset { get; set; }
    public static float YOffset { get; set; }
    public static float Sensitivity = 1f;
    public static float DeadZone = 0.1f;
    public static float Smoothing = 1f;

    private static float _lastX;
    private static float _lastY;

    public static Vector2 Tilt(float deltaTime)
    {
        var x = Input.acceleration.x;
        var y = Input.acceleration.y;
        
        // calibration
        x += XOffset;
        y += YOffset;

        // dead zone
        if (Mathf.Abs(x) < DeadZone) x = 0;
        if (Mathf.Abs(y) < DeadZone) y = 0;

        // sensitivity
        x *= Mathf.Max(Sensitivity, 0.01f);
        y *= Mathf.Max(Sensitivity, 0.01f);
        x = Mathf.Clamp(x, -1f, 1f);
        y = Mathf.Clamp(y, -1f, 1f);

        // smoothing
        x = Mathf.Lerp(_lastX, x, 1f - Mathf.Exp(-Smoothing * deltaTime));
        y = Mathf.Lerp(_lastY, y, 1f - Mathf.Exp(-Smoothing * deltaTime));
        
        _lastX = x;
        _lastY = y;
        
        return new Vector2(x, y);
    }
}
