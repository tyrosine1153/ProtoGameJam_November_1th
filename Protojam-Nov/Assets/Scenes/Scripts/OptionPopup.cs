using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPopup : MonoBehaviour
{
    GameObject optionPopup;
    private void Awake()
    {
        optionPopup = GameObject.Find("OptionPopupImage");
    }
    // Start is called before the first frame update
    void Start()
    {
        optionPopup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void popup_on()
    {
        optionPopup.SetActive(true);
    }
    public void popup_off()
    {
        optionPopup.SetActive(false);
    }
}
