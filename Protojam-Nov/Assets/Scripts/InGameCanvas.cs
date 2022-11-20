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
    [SerializeField] private Image orderTimerFillImage;
    
    [Header("Other")]
    [SerializeField] private TutorialPopup tutorialPopup;
    public TutorialPopup TutorialPopup => tutorialPopup;
    
    [SerializeField] private OptionPopup optionPopup;
    [SerializeField] private Button exitButton;

    public int SuccessCount
    {
        set => successCountText.text = value.ToString("D3");
    }

    public int OrderCount
    {
        set => orderCountText.text = value.ToString("D3");
    }

    public float TimerFillAmount
    {
        set => timerFillImage.fillAmount = value;
    }
    
    public float OrderTimerFillAmount
    {
        set => orderTimerFillImage.fillAmount = value;
    }

    protected override void Awake()
    {
        base.Awake();
        optionButton.onClick.AddListener(() => optionPopup.Show());
        exitButton.onClick.AddListener(() => GameManager.Instance.GameEnd(true));
    }
}
