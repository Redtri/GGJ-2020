using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonGear : MonoBehaviour, IPointerClickHandler
{
    private bool rightClick = false;
    private int index = 0;
    public void OnPointerClick(PointerEventData eventData)
    {
        index = transform.GetSiblingIndex();
        if(eventData.button == PointerEventData.InputButton.Right) {
            rightClick = true;
        }else if (eventData.button == PointerEventData.InputButton.Left) {
            rightClick = false;
        }
        CharacterManager.instance.UpdateActorGearValues(index, rightClick);
    }
}
