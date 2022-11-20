using UnityEngine;

// 인게임에서만 하는 모든 시스템 (인게임 전용 GameManager)
public class Stage : Singleton<Stage>
{
    public bool isTimerPlaying;

    private void Start()
    {
        InGameCanvas.Instance.TutorialPopup.Show(GameManager.Instance.GameStart);
    }

    private void Update()
    {
        if (!isTimerPlaying) return;

        if (Timer > TimerMin)
        {
            Timer -= Time.deltaTime;
        }

        if (CurrentOrder != null)
        {
            CurrentOrder.Time -= Time.deltaTime;
        }
    }

    #region Stage Flow Data

    public const float TimerMin = 0;
    public const float TimerMax = 40;
    private float _timer;

    public float Timer
    {
        get => _timer;
        set
        {
            _timer = Mathf.Clamp(value, TimerMin, TimerMax);
            InGameCanvas.Instance.TimerFillAmount = _timer / TimerMax;
            if (_timer <= TimerMin)
            {
                GameManager.Instance.GameEnd();
            }
        }
    }

    #endregion

    #region Order Managing

    public Order CurrentOrder { get; private set; }

    private const float SuccessBonus = 4f;

    private void SetOrder()
    {
        CurrentOrder = new Order
        {
            OnOrderSuccess = OnOrderSuccess,
            OnOrderFail = OnOrderFail,
            OnSubmitFail = OnSubmitFail,
        };

        InGameCanvas.Instance.CookingView.ShowOrderInformation(CurrentOrder);
    }

    public void SubmitOrder(MaterialType[] recipe)
    {
        if (CurrentOrder == null || CurrentOrder.Time <= 0)
        {
            return;
        }
        
        CurrentOrder.SubmitOrder(recipe);
    }

    private void OnSubmitFail()
    {
        InGameCanvas.Instance.CookingView.ShowSubmitResult(false, false, null);
    }

    private void OnOrderFail()
    {
        CurrentOrder = null;

        _gameData.OrderCount++;
        _gameData.FailCount++;

        InGameCanvas.Instance.CookingView.ShowSubmitResult(false, true, SetOrder);
    }

    private void OnOrderSuccess()
    {
        CurrentOrder = null;

        _gameData.OrderCount++;
        _gameData.SuccessCount++;
        Timer += SuccessBonus;

        InGameCanvas.Instance.CookingView.ShowSubmitResult(true, true, SetOrder);
    }

    #endregion

    #region Stage Flow

    private GameData _gameData;

    public void SetStage(GameData gameData)
    {
        _gameData = gameData;
        Timer = TimerMax;
    }

    public void StartStage()
    {
        SetOrder();
        isTimerPlaying = true;
    }

    public void EndStage()
    {
        isTimerPlaying = false;
        InGameCanvas.Instance.CookingView.OnEndGame();
    }

    #endregion
}