using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject options;
    
    public void OnStartPress()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnOptionPress()
    {
        options.SetActive(true);
    }
    
    public void OnQuitPress()
    {
        Application.Quit();
    }
}
