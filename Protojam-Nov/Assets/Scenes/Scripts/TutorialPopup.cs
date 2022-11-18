using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopup : MonoBehaviour
{
    GameObject tutorialPopup;
    GameObject optionButton;
    GameObject leftButton;
    GameObject rightButton;
    GameObject[] page = new GameObject[3];
    int pageNum = 0;
    private void Awake()
    {
        tutorialPopup = GameObject.Find("Tutorial_PopupImage");
        optionButton = GameObject.Find("Button_Option");
        leftButton = GameObject.Find("Left_Button");
        rightButton = GameObject.Find("Right_Button");
        for (int i = 0; i<3; i++)
        {
            page[i] = GameObject.Find("Page" + (i + 1));
            page[i].SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        optionButton.SetActive(false);
        page[0].SetActive(true);
        CheckPageHiddenButton();
    }
    public void popup_off()
    {
        tutorialPopup.SetActive(false);
        optionButton.SetActive(true);
    }
    public void page_left()
    {
        if(pageNum-1 > -1)
        {
            page[pageNum].SetActive(false);
            page[--pageNum].SetActive(true);
        }
        CheckPageHiddenButton();
    }
    public void page_right()
    {
        if (pageNum + 1 < 3)
        {
            page[pageNum].SetActive(false);
            page[++pageNum].SetActive(true);
        }
        CheckPageHiddenButton();
    }
    void CheckPageHiddenButton()
    {
        if (pageNum == 0) leftButton.SetActive(false);
        else if (pageNum == 1)
        {
            if (leftButton.activeSelf == false) leftButton.SetActive(true);
            if (rightButton.activeSelf == false) rightButton.SetActive(true);
        }
        else if (pageNum == 2) rightButton.SetActive(false);

    }
}
