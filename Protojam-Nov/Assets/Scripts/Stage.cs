using System.Collections;
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

    private IEnumerator BalanceTimer()
    {
        for (; Order.MaxTime < 5f; Order.MaxTime -= 1f)
        {
            Debug.Log("MaxTime : " + Order.MaxTime);
            for (var time = 0f; time < 15; time += Time.deltaTime)
            {
                yield return null;
            }
        }
    }

    #region Stage Flow Data

    private const float TimerMin = 0;
    private const float TimerMax = 30;
    private float _timer;

    private float Timer
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
        _gameData.OrderCount++;

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
        AudioManager.Instance.PlaySFX(SFXType.fail);
        CurrentOrder = null;

        _gameData.FailCount++;

        InGameCanvas.Instance.CookingView.ShowSubmitResult(false, true, SetOrder);
    }

    private void OnOrderSuccess()
    {
        AudioManager.Instance.PlaySFX(SFXType.success);
        CurrentOrder = null;

        _gameData.SuccessCount++;
        Timer += SuccessBonus;

        InGameCanvas.Instance.CookingView.ShowSubmitResult(true, true, SetOrder);
    }

    #endregion

    #region Stage Flow

    private GameData _gameData;
    private Coroutine _balanceTimerCoroutine;

    public void SetStage(GameData gameData)
    {
        _gameData = gameData;
        Timer = TimerMax;
    }

    public void StartStage()
    {
        SetOrder();
        isTimerPlaying = true;
        _balanceTimerCoroutine = StartCoroutine(BalanceTimer());
    }

    public void EndStage()
    {
        isTimerPlaying = false;
        InGameCanvas.Instance.CookingView.OnEndGame();
        InGameCanvas.Instance.FaderUI.StartFade(FaderUI.Fade.In);
        if(_balanceTimerCoroutine != null) StopCoroutine(_balanceTimerCoroutine);
    }

    #endregion
}