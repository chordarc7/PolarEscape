using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameButtons : MonoBehaviour
{
    public void OnRetryPress()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnReturnPress()
    {
        SceneManager.LoadScene("Title");
    }

    public void OnQuitPress()
    {
        Application.Quit();
    }
}
