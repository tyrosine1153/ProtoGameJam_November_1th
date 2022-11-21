using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    AudioManager Amanager;
    // Start is called before the first frame update
    void Start()
    {
        Amanager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Amanager.PlaySFX(SFXType.select);
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Amanager.PlaySFX(SFXType.uiSelect);
    }
}
