using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using BrunoMikoski.TextJuicer;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class UIDialogue : MonoBehaviour
{
	public DialogueScriptableObject dialogData;
	public TextMeshProUGUI nameText;
	public float bubbleAlpha;

	//private TMP_TextJuicer juicer;
	private TextMeshProUGUI dialogTxt;
    private Text text;

	private float progress = 0;
	public float speed = 10;

	private void Awake()
	{
		//juicer = GetComponent<TMP_TextJuicer>();
		dialogTxt = GetComponent<TextMeshProUGUI>();
		DialogAppear(false);
    }

	private void OnEnable()
	{
		GameManager.instance.phaseHelper.onEntranceEnd += OnEntrance;
		GameManager.instance.phaseHelper.onPhaseEnd += OnLeave;
	}

	private void OnDisable()
	{
		GameManager.instance.phaseHelper.onEntranceEnd -= OnEntrance;
		GameManager.instance.phaseHelper.onPhaseEnd -= OnLeave;
	}

	private void OnEntrance()
	{
		SetText(GetString());
		Character c = GameManager.instance.phaseHelper.currentCharacter;
		nameText.text = c.c_Name + "  " + c.c_Surname;
		DialogAppear();
	}

	private void OnLeave(bool anyone)
	{
		if(anyone)
			DialogAppear(false);
	}

	private void DialogAppear(bool show = true)
	{
		Sequence leaveSequence = DOTween.Sequence();

		leaveSequence.Append(dialogTxt.DOFade( (show) ? 1f : 0f, .5f));
		leaveSequence.Join(dialogTxt.transform.parent.GetComponent<MaskableGraphic>().DOFade((show) ? bubbleAlpha : 0f, .5f));
		leaveSequence.Join(nameText.DOFade((show) ? 1f : 0f, .5f));
		leaveSequence.Join(nameText.transform.parent.GetComponent<MaskableGraphic>().DOFade((show) ? bubbleAlpha : 0f, .5f));

		if(!show){
			leaveSequence.AppendCallback(() => ResetFields());
		}
	}

	private void ResetFields()
	{
		SetText("");
		nameText.text = "";
	}


	public void SetText(string txt) {

        DOTween.To(() => dialogTxt.text, x =>  dialogTxt.text = x, txt, 2.0f);

        dialogTxt.text = "";
	}    


    private void UpdateText(Text text)
    {
        dialogTxt.text = text.text;
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
