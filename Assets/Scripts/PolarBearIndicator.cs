using TMPro;
using UnityEngine;

public class PolarBearIndicator : MonoBehaviour, IUpdatable
{
    [SerializeField] private Transform polarBear;
    [SerializeField] private Transform player;
    [SerializeField] private TextMeshProUGUI text;
    
    private void OnEnable()
    {
        UpdateManager.Register(this);
    }
    
    private void OnDisable()
    {
        UpdateManager.Unregister(this);
    }

    public void OnUpdate(float deltaTime)
    {
        var dir = polarBear.position - player.position;
        var angle = Mathf.Atan2(dir.x, -dir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        
        text.SetText(Vector3.Distance(polarBear.position, player.position).ToString(format: "F0") + "m");
    }
}
