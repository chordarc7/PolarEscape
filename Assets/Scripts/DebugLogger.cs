using TMPro;
using UnityEngine;

public class DebugLogger : MonoBehaviour
{
    public static DebugLogger Instance { get; private set; }
    
    [SerializeField] private TextMeshProUGUI text;

    private void Awake()
    {
        Instance = this;
    }
    
    public void Log(string msg)
    {
        text.text = msg;
    }
}
