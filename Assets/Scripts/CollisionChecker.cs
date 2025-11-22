using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionChecker : MonoBehaviour
{
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private bool isPlayer;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene("GameOver");
            return;
        }

        if (isPlayer && other.CompareTag("Flag"))
        {
            
            SceneManager.LoadScene("Clear");
        }
    }
}
