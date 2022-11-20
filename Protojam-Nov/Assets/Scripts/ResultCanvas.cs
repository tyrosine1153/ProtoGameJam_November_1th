using UnityEngine;
using UnityEngine.UI;

public class ResultCanvas : MonoBehaviour
{
    [SerializeField] private FaderUI faderUI;

    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainButton;
    private void Start()
    {
        restartButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.StopBGM();
            faderUI.StartFade(FaderUI.Fade.In, () => SceneManagerEx.LoadScene(SceneType.InGame));
        });
        mainButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.StopBGM();
            faderUI.StartFade(FaderUI.Fade.In, () => SceneManagerEx.LoadScene(SceneType.Main));
        });
    }
}
