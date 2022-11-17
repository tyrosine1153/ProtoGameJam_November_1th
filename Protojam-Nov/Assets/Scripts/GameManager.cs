using System.Collections;
using UnityEngine;

public class GameData
{
    public int OrderCount;
    public int SuccessCount;
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
    public bool isGamePlaying = false;

    public void GameOver()
    {
        GameEnd(false);
    }

    public void GameClear()
    {
        GameEnd(true);
    }

    [ContextMenu("GameStart")]
    public void TestGameStart()
    {
        GameStart();
    }

    public void GameStart()
    {
        if (isGamePlaying) return;
        isGamePlaying = true;

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

        yield return new WaitForSeconds(0f);

        // Todo : 대충 지연과 연출

        Stage.Instance.StartStage();

        yield break;
    }

    public void GameEnd(bool isGameClear)
    {
        if (!isGamePlaying) return;
        isGamePlaying = false;

        // Todo : 연출

        StartCoroutine(CoGameEnd(isGameClear));
    }

    private IEnumerator CoGameEnd(bool isGameClear)
    {
        Stage.Instance.EndStage();

        // Todo : 대충 지연과 연출

        // Todo : CurrentGameData 게임 결과 창에 전달하기

        yield break;
    }
}