using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour, IUpdatable
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform goal;
    [SerializeField] private Transform start;
    [SerializeField] private Image fill;

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
        var progress = Vector3.Distance(player.position, start.position) / Vector3.Distance(goal.position, start.position);
        fill.fillAmount = progress;
    }
}
