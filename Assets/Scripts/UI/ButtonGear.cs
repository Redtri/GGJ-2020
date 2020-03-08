﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ButtonGear : MonoBehaviour
{
    public Image bar;
    private bool rightClick = false;
    private int index = 0;
	private UIButton button;

	private void Awake()
	{
		button = GetComponent<UIButton>();
	}

	private void OnEnable()
	{
		button.OnClick += OnClick;
	}

	private void OnDisable()
	{
		button.OnClick -= OnClick;
	}

	public void OnClick(PointerEventData eventData)
    {
       // if (!GetComponent<UIButton>().lockButton)
       // {
            GameObject myEventSystem = GameObject.Find("EventSystem");
            index = transform.GetSiblingIndex();
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                rightClick = true;
            }
            else if (eventData.button == PointerEventData.InputButton.Left)
            {
                rightClick = false;
            }
            CharacterManager.instance.UpdateActorGearValues(index, rightClick);
            myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
      //  }       
    }
    public void UpdateBar(float fillAmount)
    {
        bar.fillAmount = fillAmount;
    }

    /*
    private void UpdateLens(float value, LensDistortion lens)
    {
        lens.intensity.value = value;
    }*/

}
