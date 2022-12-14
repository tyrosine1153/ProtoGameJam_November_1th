using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPopup : MonoBehaviour
{
    [SerializeField] private GameObject[] pages;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button startButton;
    [SerializeField] private Button closeButton;

    private int _currentPageIndex = -1;

    private void Awake()
    {
        leftButton.onClick.AddListener(() => { SetPage(_currentPageIndex - 1); });
        rightButton.onClick.AddListener(() => { SetPage(_currentPageIndex + 1); });
    }

    public void Show(Action onClickStart = null)
    {
        startButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();

        startButton.onClick.AddListener(() =>
        {
            onClickStart?.Invoke();
            Close();
        });
        closeButton.onClick.AddListener(() =>
        {
            onClickStart?.Invoke();
            Close();
        });

        gameObject.SetActive(true);
        SetPage(0);
    }

    private void SetPage(int index)
    {
        if (AudioManager.IsInitialized) AudioManager.Instance.PlaySFX(0);
        if (index < 0 || index >= pages.Length || index == _currentPageIndex)
        {
            return;
        }
        _currentPageIndex = index;

        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == _currentPageIndex);
        }
        leftButton.gameObject.SetActive(_currentPageIndex > 0);
        rightButton.gameObject.SetActive(_currentPageIndex < pages.Length - 1);
    }

    public void Close()
    {
        if(AudioManager.IsInitialized) AudioManager.Instance.PlayBGM(0);
        gameObject.SetActive(false);
    }
}
