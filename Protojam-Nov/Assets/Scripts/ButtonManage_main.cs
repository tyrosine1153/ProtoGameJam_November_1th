using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManage_main : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("InGame");
    }
    public void exitGame()
    {
        Application.Quit();
    }
}
