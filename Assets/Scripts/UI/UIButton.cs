﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // 1
using System;
using UnityEngine.Events;





public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{

    public enum ButtonType
    {
        Forge,
        Validate
    }

	public enum GearType
	{
		Sword,
		Bow,
		Armor
	}

    private Image img;
	public Sprite idleSprite;
	public Sprite hoverSprite;
	public Sprite leftClickSprite;
	public Sprite rightClickSprite;
	public Sprite lockSprite;
	[SerializeField]
	public UnityEvent clickEvent;

    public ButtonType buttonType = ButtonType.Forge;
	public GearType gearType;
    public int gearLevel;

	public bool lockButton = false;
	private bool isHover = false;


	private void Awake()
	{
		img = GetComponent<Image>();
		SetSprite(idleSprite);
		HighlightGear(false);
	}

	private void Update()
	{
		if (lockButton)
		{
			SetSprite(lockSprite);
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			SetSprite(leftClickSprite);

            //Sound
            switch (buttonType)
            {
                case ButtonType.Forge:
                    AudioManager.instance.AddItem.Post(GameManager.instance.gameObject);
                    break;
                case ButtonType.Validate:
                    AudioManager.instance.Validate.Post(GameManager.instance.gameObject);
                    break;
            }
}
		else
		{
			SetSprite(rightClickSprite);
            //Sound
            switch (buttonType)
            {
                case ButtonType.Forge:
                    AudioManager.instance.RemoveItem.Post(GameManager.instance.gameObject);
                    break;
                case ButtonType.Validate:
                    break;
            }
        }

       
	}
	public void OnPointerUp(PointerEventData eventData)
	{
		if (isHover)
		{
			SetSprite(hoverSprite);
			clickEvent.Invoke();
		}
		else
		{
			SetSprite(idleSprite);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		SetSprite(hoverSprite);
		isHover = true;

        switch (buttonType)
        {
            case ButtonType.Forge:
                MouseSparkle.instance.SetSparkleSize(0.7f);
                MouseSparkle.instance.SetBurstAmount(50);
				HighlightGear(true);
                break;
            case ButtonType.Validate:
                MouseSparkle.instance.SetSparkleSize(1f);
                MouseSparkle.instance.SetBurstAmount(80);
                break;
        }

    }

	public void OnPointerExit(PointerEventData eventData)
	{
		SetSprite(idleSprite);
		isHover = false;

        MouseSparkle.instance.SetSparkleSize(0.5f);
        MouseSparkle.instance.SetBurstAmount(30);
		HighlightGear(false);
    }

	private void SetSprite(Sprite s)
	{
		if (lockButton)
		{
			img.sprite = lockSprite;
		}else
		{
			img.sprite = s;
		}
	}

	public void SetLock(bool l)
	{
		if (l)
		{
			lockButton = true;
			SetSprite(lockSprite);
		}
		else
		{
			lockButton = false;
			if (isHover)
			{
				SetSprite(hoverSprite);
			}else
			{
				SetSprite(idleSprite);
			}
            
		}
	}

	private void HighlightGear(bool show = true)
	{
		CharacterActor charRef = CharacterManager.instance.characterActor;

		charRef.gearParts[(int)gearType].material.SetFloat("_Brightness", (show) ? 3f : 0f);
	}	
}
