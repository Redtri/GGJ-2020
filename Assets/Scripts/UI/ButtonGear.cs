using System.Collections;
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
        GameObject myEventSystem = GameObject.Find("EventSystem");

        index = transform.GetSiblingIndex();
        if(eventData.button == PointerEventData.InputButton.Right) {
            rightClick = true;
            //GetComponent<Button>().onClick.Invoke();
        }
        else if (eventData.button == PointerEventData.InputButton.Left) {
            rightClick = false;
        }
        CharacterManager.instance.UpdateActorGearValues(index, rightClick);
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

    public void UpdateBar(float fillAmount)
    {
        bar.fillAmount = fillAmount;
    }
}
