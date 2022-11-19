using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManage_main : MonoBehaviour
{
    private void Start()
    {
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Ingame");
    }
    public void ToMain()
    {
        SceneManager.LoadScene("Main");
    }
    public void exitGame()
    {
        Application.Quit();
    }
}
