using UnityEngine;

// 인게임에서만 하는 모든 시스템 (인게임 전용 GameManager)
public class Stage : Singleton<Stage>
{
    private void Update()
    {
        if (!GameManager.Instance.isGamePlaying) return;

        if (Timer < TimerMax)
        {
            Timer += Time.deltaTime;
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
            // Todo : UI에 표시
            if (_timer >= TimerMax)
            {
                GameManager.Instance.GameClear();
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

        // Todo : UI에 표시
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
        // Todo : UI에 표시
    }

    private void OnOrderFail()
    {
        CurrentOrder = null;

        _gameData.OrderCount++;
        _gameData.FailCount++;

        // Todo : UI에 표시 (대충 X 하고 나가는 애니메이션)
        // 잠시 쉬고
        SetOrder();
    }

    private void OnOrderSuccess()
    {
        CurrentOrder = null;

        _gameData.OrderCount++;
        _gameData.SuccessCount++;
        Timer += SuccessBonus;

        // Todo : UI에 표시 (대충 O 하고 나가는 애니메이션)
        // 잠시 쉬고
        SetOrder();
    }

    #endregion

    #region Stage Flow

    private GameData _gameData;

    public void SetStage(GameData gameData)
    {
        _gameData = gameData;
        Timer = TimerMin;
    }

    public void StartStage()
    {
    }

    public void EndStage()
    {
    }

    #endregion
}