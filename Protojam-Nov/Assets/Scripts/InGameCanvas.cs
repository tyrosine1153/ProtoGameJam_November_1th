using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameCanvas : Singleton<InGameCanvas>
{
    [Header("Header")]
    [SerializeField] private TextMeshProUGUI successCountText;
    [SerializeField] private TextMeshProUGUI orderCountText;
    [SerializeField] private Image timerFillImage;
    [SerializeField] private Button optionButton;

    [Header("CookingView")]
    [SerializeField] private CookingView cookingView;
    public CookingView CookingView => cookingView;

    public int SuccessCount
    {
        set => successCountText.text = value.ToString();
    }

    public int OrderCount
    {
        set => orderCountText.text = value.ToString();
    }

    public float TimerFillAmount
    {
        set => timerFillImage.fillAmount = value;
    }
}
