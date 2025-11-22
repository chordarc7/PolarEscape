using UnityEngine;

public class OptionManager : MonoBehaviour
{
    public void OnSensitivityChange(float value)
    {
        TiltInputManager.Sensitivity = value;
    }
    
    public void OnXOffsetChange(float value)
    {
        TiltInputManager.XOffset = value;
    }
    
    public void OnYOffsetChange(float value)
    {
        TiltInputManager.YOffset = value;
    }

    public void OnDeadZoneChange(float value)
    {
        TiltInputManager.DeadZone = value;
    }

    public void OnSmoothingChange(float value)
    {
        TiltInputManager.Smoothing = value;
    }
}
