using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PolarBear : MonoBehaviour, IUpdatable
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed;

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
        var dir = (target.position - transform.position).normalized;
        transform.Translate(dir * (speed * deltaTime), Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) SceneManager.LoadScene("GameOver");
    }
}
