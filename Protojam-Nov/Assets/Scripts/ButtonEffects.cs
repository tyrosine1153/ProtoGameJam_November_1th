using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySFX(SFXType.select);
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        AudioManager.Instance.PlaySFX(SFXType.uiSelect);
    }
}
