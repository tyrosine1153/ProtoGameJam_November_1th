using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManage_main : MonoBehaviour
{
    AudioManager amanager;
    FaderUI faderUI;
    private void Start()
    {
        amanager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        faderUI = GameObject.Find("Fade").GetComponent<FaderUI>();
    }
    public void StartGame()
    {
        faderUI.startFade(FaderUI.Fade.In, "InGame");

    }
    public void ToMain()
    {
        amanager.StopBGM();
        faderUI.startFade(FaderUI.Fade.In, "Main");
    }
    public void exitGame()
    {
        Application.Quit();
    }
}
