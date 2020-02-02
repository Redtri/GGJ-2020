using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // 1
using System;
using UnityEngine.Events;


public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{

	private Image img;
	public Sprite idleSprite;
	public Sprite hoverSprite;
	public Sprite leftClickSprite;
	public Sprite rightClickSprite;
	public Sprite lockSprite;
	[SerializeField]
	public UnityEvent clickEvent;
	
	public int gearLevel;

	public bool lockButton = false;
	private bool isHover = false;


	private void Awake()
	{
		img = GetComponent<Image>();
		SetSprite(idleSprite);
	}

	private void Update()
	{
		if (lockButton)
		{
			SetSprite(lockSprite);
		}else
		{
			
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			SetSprite(leftClickSprite);
		}
		else
		{
			SetSprite(rightClickSprite);
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
	}

	

	public void OnPointerExit(PointerEventData eventData)
	{
		SetSprite(idleSprite);
		isHover = false;
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

	
}
