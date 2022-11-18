using System.Collections;
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
