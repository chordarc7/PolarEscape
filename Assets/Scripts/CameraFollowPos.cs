using System.Linq;
using UnityEngine;

public class CameraFollowPos : MonoBehaviour, IUpdatable
{
    [SerializeField] private Transform[] targets;

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
        transform.position = targets.Aggregate(Vector3.zero, (current, target) => current + target.position) / targets.Length;
    }
}
