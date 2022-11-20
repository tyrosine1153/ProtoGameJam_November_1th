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
    bool audiomissing;
    public Slider BGMslider;
    public Slider SFXslider;
    private void Awake()
    {
        optionPopup = GameObject.Find("OptionPopupImage");
        optionButton = GameObject.Find("Button_Option");
        if (GameObject.Find("AudioManager") != null) Amanager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        else audiomissing = true;
        if (SceneManager.GetActiveScene().name == "Main") menuButton = GameObject.Find("MenuButton");
    }
    // Start is called before the first frame update
    void Start()
    {
        optionPopup.SetActive(false);
    }
    private void OnEnable()
    {
        BGMslider.onValueChanged.AddListener(delegate { BGMChangeCheck(); });
        SFXslider.onValueChanged.AddListener(delegate { SFXChangeCheck(); });
    }
    private void OnDisable()
    {
    }
    void BGMChangeCheck()
    {
        Amanager.BGMVolume = BGMslider.value;
    }
    void SFXChangeCheck()
    {
        Amanager.SFXVolume = SFXslider.value;
    }

    public void popup_on()
    {
        optionPopup.SetActive(true);
        if (!audiomissing)
        {
            BGMslider.value = Amanager.BGMVolume;
            SFXslider.value = Amanager.SFXVolume;
        }
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
