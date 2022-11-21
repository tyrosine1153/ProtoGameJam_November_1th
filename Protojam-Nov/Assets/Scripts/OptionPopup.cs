using UnityEngine;
using UnityEngine.UI;

public class OptionPopup : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Button closeButton;

    private void OnEnable()
    {
        bgmSlider.onValueChanged.AddListener(value => AudioManager.Instance.BGMVolume = value);
        sfxSlider.onValueChanged.AddListener(value => AudioManager.Instance.SFXVolume = value);
        closeButton.onClick.AddListener(Close);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        if (Stage.IsInitialized) Stage.Instance.isTimerPlaying = false;
    }

    public void Close()
    {
        gameObject.SetActive(false);
        if (Stage.IsInitialized) Stage.Instance.isTimerPlaying = true;
    }
}
