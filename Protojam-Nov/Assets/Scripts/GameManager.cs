using System.Collections;
using UnityEngine;

public class GameData
{
    private int _orderCount;
    public int OrderCount
    {
        get => _orderCount;
        set
        {
            _orderCount = value;
            InGameCanvas.Instance.OrderCount = value;
        }
    }
    
    private int _successCount;
    public int SuccessCount
    {
        get => _successCount;
        set
        {
            _successCount = value;
            InGameCanvas.Instance.SuccessCount = value;
        }
    }

    public int FailCount;

    public GameData()
    {
        OrderCount = 0;
        SuccessCount = 0;
        FailCount = 0;
    }
}

public class GameManager : Singleton<GameManager>
{
    public GameData CurrentGameData;
    public bool IsGamePlaying { get; private set; }

    [ContextMenu("GameStart")]
    public void TestGameStart()
    {
        GameStart();
    }

    public void GameStart()
    {
        if (IsGamePlaying) return;
        IsGamePlaying = true;

        CurrentGameData = new GameData();

        // Todo : 연출

        StartCoroutine(CoGameStart());
    }
    
    private IEnumerator CoGameStart()
    {
        while (!Stage.IsInitialized)
        {
            yield return null;
        }

        Stage.Instance.SetStage(CurrentGameData);

        yield return new WaitForSeconds(0.5f);

        // Todo : 대충 지연과 연출

        Stage.Instance.StartStage();

        yield break;
    }

    public void GameEnd(bool isExit = false)
    {
        if (!IsGamePlaying) return;
        IsGamePlaying = false;

        CurrentGameData = new GameData();
        // Todo : 연출

        StartCoroutine(CoGameEnd(isExit));
    }

    private IEnumerator CoGameEnd(bool isExit)
    {
        Stage.Instance.EndStage();

        // Todo : 대충 지연과 연출

        yield return new WaitForSeconds(5f);
        // Todo : CurrentGameData 게임 결과 창에 전달하기
        if (isExit)
        {
            SceneManagerEx.LoadScene(SceneType.Main);
        }
        else
        {
            SceneManagerEx.LoadScene(SceneType.Result);
        }
    }
}