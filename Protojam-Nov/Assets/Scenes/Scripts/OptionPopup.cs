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
    private void Awake()
    {
        optionPopup = GameObject.Find("OptionPopupImage");
        optionButton = GameObject.Find("Button_Option");

        if (SceneManager.GetActiveScene().name == "Main") menuButton = GameObject.Find("MenuButton");
    }
    // Start is called before the first frame update
    void Start()
    {
        optionPopup.SetActive(false);
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
