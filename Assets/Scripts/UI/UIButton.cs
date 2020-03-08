using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // 1
using System;
using System.Security.Permissions;
using UnityEngine.Events;
using Sweet.UI;




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
	public bool isHover = false;

	public UIPageText dialogText;

	public delegate void ClickEvent(PointerEventData evtData);
	public event ClickEvent OnClick;

	private void Reset()
	{
		dialogText = FindObjectOfType<UIPageText>();
	}

	private void Awake()
	{
		img = GetComponent<Image>();
	}

	private void Start()
	{
		SetSprite(idleSprite);
		HighlightGear(false);
	}

	private void Update()
	{
		if (lockButton)
		{

			SetSprite(lockSprite);
		}
		else
		{
			if (!dialogText.IsTextAnimationEnd())
			{
				SetSprite(lockSprite);
			}else
			{
				if (img.sprite == lockSprite)
				{
					if (isHover)
					{
						SetSprite(hoverSprite);
					}else
					{
						SetSprite(idleSprite);
					}		
				}
			}
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (lockButton || !dialogText.IsTextAnimationEnd()) return;

		if (eventData.button == PointerEventData.InputButton.Left)
		{
			SetSprite(leftClickSprite);

			//Sound
			switch (buttonType)
			{
				case ButtonType.Forge:
					//if (!lockButton)
					AudioManager.instance.AddItem.Post(GameManager.instance.gameObject);
					break;
				case ButtonType.Validate:
					// if(!lockButton)
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
					// if (!lockButton)
					AudioManager.instance.RemoveItem.Post(GameManager.instance.gameObject);
					break;
				case ButtonType.Validate:
					break;
			}
		}
		OnClick?.Invoke(eventData);

	}
	public void OnPointerUp(PointerEventData eventData)
	{
		if (lockButton || !dialogText.IsTextAnimationEnd()) return;
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
		SetCursor(isHover);
		

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

	private void SetCursor(bool isHover)
	{
		CursorManager.instance.SetHoverCursor(isHover);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		SetSprite(idleSprite);
		isHover = false;

		SetCursor(isHover);

		MouseSparkle.instance.SetSparkleSize(0.5f);
		MouseSparkle.instance.SetBurstAmount(30);
		HighlightGear(false);
	}

	private void SetSprite(Sprite s)
	{
		if (lockButton || !dialogText.IsTextAnimationEnd())
		{
			img.sprite = lockSprite;
		}
		else
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
			}
			else
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
