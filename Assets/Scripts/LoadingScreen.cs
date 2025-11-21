using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text percentText;

    public void Show()
    {
        gameObject.SetActive(true);
        SetProgress(0f);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetProgress(float t)
    {
        t = Mathf.Clamp01(t);
        slider.value = t;
        percentText.text = $"loading... {Mathf.RoundToInt(t * 100f)}%";
    }
}