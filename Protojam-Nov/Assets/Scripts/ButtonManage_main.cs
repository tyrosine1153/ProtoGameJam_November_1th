using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManage_main : MonoBehaviour
{
    AudioManager Amanager;
    private void Start()
    {
        if (GameObject.Find("AudioManager") != null)
        {
            Amanager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
            Amanager.PlayBGM(0);
        }

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
