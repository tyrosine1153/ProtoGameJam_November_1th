using System.Collections;
<<<<<<< HEAD:Protojam-Nov/Assets/Scenes/Scripts/GameManager.cs
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int maxScoreNum = 0;
    public static int successNum = 0;
    public static int failNum = 0;
    TextMeshProUGUI scoreboard = null;
    // Start is called before the first frame update
    void Start()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Main":
                GameObject.Find("Text_Score").GetComponent<TextMeshProUGUI>().text = "" + maxScoreNum;
                break;
            case "Ingame":
                Initial_Number();
                break;
            case "Results":
                GameObject.Find("sum").GetComponent<TextMeshProUGUI>().text = "" + (successNum + failNum);
                GameObject.Find("success").GetComponent<TextMeshProUGUI>().text = "" + successNum;
                GameObject.Find("fail").GetComponent<TextMeshProUGUI>().text = "" + failNum;
                break;
            default:
                break;

        }
    }
    public void Add_success()
    {
        successNum++;
    }
    public void Add_fail()
    {
        failNum++;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Ingame");
    }
    public void GoResults()
    {
        SceneManager.LoadScene("Results");
    }
    public void ReplaceScore()
    {
        maxScoreNum = ((successNum + failNum) > maxScoreNum) ? maxScoreNum = successNum + failNum : maxScoreNum;
    }
    public void Initial_Number()
    {
        successNum = 0;
        failNum = 0;
    }
    public void ToMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
=======
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
>>>>>>> ingame-ui:Protojam-Nov/Assets/Scripts/GameManager.cs
