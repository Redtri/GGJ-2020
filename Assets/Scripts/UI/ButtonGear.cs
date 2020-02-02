﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonGear : MonoBehaviour, IPointerClickHandler
{
    public Image bar;
    private bool rightClick = false;
    private int index = 0;
    public void OnPointerClick(PointerEventData eventData)
    {
        index = transform.GetSiblingIndex();
        if(eventData.button == PointerEventData.InputButton.Right) {
            rightClick = true;
            GetComponent<Button>().onClick.Invoke();
        }
        else if (eventData.button == PointerEventData.InputButton.Left) {
            rightClick = false;
        }
        CharacterManager.instance.UpdateActorGearValues(index, rightClick);
    }

    //TODO : Debug de mes couilles
    public void OnSelect(BaseEventData eventData)
    {
        GetComponent<Button>().OnDeselect(eventData);
        Debug.Log(this.gameObject.name + " was selected");
    }

    public void UpdateBar(float fillAmount)
    {
        bar.fillAmount = fillAmount;
    }
}
