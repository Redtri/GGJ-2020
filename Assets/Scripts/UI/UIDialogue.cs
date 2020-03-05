using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using BrunoMikoski.TextJuicer;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using Sweet.UI;

public class UIDialogue : MonoBehaviour
{
	public DialogueScriptableObject dialogData;
	public TextMeshProUGUI nameText;
	//public float bubbleAlpha;
	public UIPageText pageText;
	public UITransition transition;

	//private TMP_TextJuicer juicer;
	
	
    private Text text;

	private float progress = 0;
	public float speed = 10;

	private void Start()
	{
		//juicer = GetComponent<TMP_TextJuicer>();
		//pageText = GetComponent<UIPageText>();
		DisplayDialog(false);
    }

	private void OnEnable()
	{
		GameManager.instance.phaseHelper.onEntrance += OnEntranceStart;
		GameManager.instance.phaseHelper.onEntranceEnd += OnEntranceEnd;
		GameManager.instance.phaseHelper.onLeaving += OnLeave;
		

	}

	private void OnDisable()
	{
		GameManager.instance.phaseHelper.onEntrance -= OnEntranceStart;
		GameManager.instance.phaseHelper.onEntranceEnd -= OnEntranceEnd;
		GameManager.instance.phaseHelper.onLeaving -= OnLeave;
	}

	private void OnEntranceStart()
	{
		DisplayDialog(true);
	}

	private void OnEntranceEnd()
	{
		
		Character c = GameManager.instance.phaseHelper.currentCharacter;
		nameText.text = c.c_Name + "  " + c.c_Surname;
		SetText(GetString());	
	}

	private void OnLeave()
	{
		//if(anyone)
		DisplayDialog(false);
	}

	private void DisplayDialog(bool show)
	{
		if (show)
		{
			ResetFields();
			transition.Show();
		}else
		{
			transition.Hide();
			ResetFields();
		}
	
		//Appear and disapear Text

		/*Sequence leaveSequence = DOTween.Sequence();

		leaveSequence.Append(dialogTxt.DOFade( (show) ? 1f : 0f, .5f));
		leaveSequence.Join(dialogTxt.transform.parent.GetComponent<MaskableGraphic>().DOFade((show) ? bubbleAlpha : 0f, .5f));
		leaveSequence.Join(nameText.DOFade((show) ? 1f : 0f, .5f));
		leaveSequence.Join(nameText.transform.parent.GetComponent<MaskableGraphic>().DOFade((show) ? bubbleAlpha : 0f, .5f));

		if(!show){
			leaveSequence.AppendCallback(() => ResetFields());
		}*/
	}

	private void ResetFields()
	{
		SetText(" ");
		nameText.text = " ";
	}


	public void SetText(string txt) {
		pageText.LoadText(txt,true);
		//Debug.Log("Set text : " + txt);
       // DOTween.To(() => dialogTxt.text, x =>  dialogTxt.text = x, txt, 2.0f);

      //  dialogTxt.text = "";
	}    

	
	private string GetString()
	{
		Character c = GameManager.instance.phaseHelper.currentCharacter;
		if (c == null) return "";

        if (c.privateText)
        {
            return c.forcedText;
        }
        
		float dist = 0;
		GearType gt = c.GetFarestGear(out dist);
		switch (gt)
		{
			case GearType.SWORD:
				return dialogData.sword.GetRandom(dist);
			case GearType.BOW:
				return dialogData.bow.GetRandom(dist);
			case GearType.ARMOR:
				return dialogData.armor.GetRandom(dist);
		}

		return "";
	}
}
