using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private Slider xOffsetSlider;
    [SerializeField] private Slider deadZoneSlider;
    
    private void OnEnable()
    {
        sensitivitySlider.value = TiltInputManager.Sensitivity;
        xOffsetSlider.value = TiltInputManager.XOffset;
        deadZoneSlider.value = TiltInputManager.DeadZone;
    }

    public void OnSensitivityChange(float value)
    {
        TiltInputManager.Sensitivity = value;
    }
    
    public void OnXOffsetChange(float value)
    {
        TiltInputManager.XOffset = value;
    }
    
    public void OnDeadZoneChange(float value)
    {
        TiltInputManager.DeadZone = value;
    }
}
