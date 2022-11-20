using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
    [SerializeField] private OptionPopup optionPopup;

    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private FaderUI faderUI;

    private void Start()
    {
        startButton.onClick.AddListener(() =>
            faderUI.StartFade(FaderUI.Fade.In, () => SceneManagerEx.LoadScene(SceneType.InGame)));
        exitButton.onClick.AddListener(Application.Quit);
        optionButton.onClick.AddListener(optionPopup.Show);
    }
}
