using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionPopup : MonoBehaviour
{
    GameObject optionPopup;
    GameObject optionButton;
    GameObject menuButton = null;
    AudioManager Amanager;
    public Slider slider_Bgm;
    public Slider slider_Sfx;
    private void Awake()
    {
        optionPopup = GameObject.Find("OptionPopupImage");
        optionButton = GameObject.Find("Button_Option");
        Amanager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        if (SceneManager.GetActiveScene().name == "Main") menuButton = GameObject.Find("MenuButton");
    }
    // Start is called before the first frame update
    void Start()
    {
        Amanager.PlayBGM(0);
        optionPopup.SetActive(false);
    }
    void Update()
    {
    }
    void BGMChangeCheck()
    {
        Amanager.BGMVolume = slider_Bgm.value;
    }
    void SFXChangeCheck()
    {
        Amanager.SFXVolume = slider_Sfx.value;
    }
    void OnEnable()
    {
        slider_Bgm.onValueChanged.AddListener(delegate { BGMChangeCheck(); });
        slider_Sfx.onValueChanged.AddListener(delegate { SFXChangeCheck(); });
    }

    void OnDisable()
    {
        //Un-Register Slider Events
        slider_Bgm.onValueChanged.RemoveAllListeners();
        slider_Sfx.onValueChanged.RemoveAllListeners();
    }

    public void popup_on()
    {
        optionPopup.SetActive(true);
        optionButton.SetActive(false);

        if (menuButton != null) menuButton.SetActive(false);
    }
    public void popup_off()
    {
        optionPopup.SetActive(false);
        optionButton.SetActive(true);

        if (menuButton != null) menuButton.SetActive(true);
    }
}
