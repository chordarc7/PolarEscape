using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private GameObject buttons;
    
    public void OnStartPress()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnOptionPress()
    {
        // open option window
    }
    
    public void OnQuitPress()
    {
        Application.Quit();
    }
}
