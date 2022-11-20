using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultCanvas : MonoBehaviour
{
    [SerializeField] private FaderUI faderUI;

    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainButton;

    [SerializeField] private TextMeshProUGUI orderCountText;
    [SerializeField] private TextMeshProUGUI successCountText;
    [SerializeField] private TextMeshProUGUI failCountText;
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

        StartCoroutine(CoSetResult());
    }
    
    private IEnumerator CoSetResult()
    {
        while (!GameManager.IsInitialized)
        {
            yield return null;
        }
        var orderCount = GameManager.Instance.CurrentGameData.OrderCount;
        var successCount = GameManager.Instance.CurrentGameData.SuccessCount;
        var failCount = GameManager.Instance.CurrentGameData.FailCount;
        
        orderCountText.text = orderCount.ToString();
        successCountText.text = successCount.ToString();
        failCountText.text = failCount.ToString();
    }
}
