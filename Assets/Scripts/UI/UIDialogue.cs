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
    public GameObject children;
	public TextMeshProUGUI nameText;

	//private TMP_TextJuicer juicer;
	private TextMeshProUGUI tmp;
    private Text text;

	private float progress = 0;
	public float speed = 10;

	private void Awake()
	{
		//juicer = GetComponent<TMP_TextJuicer>();
		tmp = GetComponent<TextMeshProUGUI>();
    }

	private void OnEnable()
	{
		GameManager.instance.phaseHelper.onEntranceEnd += OnEntrance;
		GameManager.instance.phaseHelper.onLeavingEnd += OnLeave;
	}

	private void OnDisable()
	{
		GameManager.instance.phaseHelper.onEntranceEnd -= OnEntrance;
		GameManager.instance.phaseHelper.onLeavingEnd -= OnLeave;
	}

	private void OnEntrance()
	{
		SetText(GetString());
		Character c = GameManager.instance.phaseHelper.currentCharacter;
		nameText.text = c.c_Name + "  " + c.c_Surname;
	}

	private void OnLeave()
	{
		SetText("");
		nameText.text = "";
	}


	public void SetText(string txt) {
		progress = 0;

        if (!children.GetComponent<Text>())
        {
            text = children.AddComponent<Text>();
        }

        text.DOText(txt, 2.0f).OnUpdate(() => UpdateText(text)).OnComplete(() => text.text = "");
        
        tmp.text = "";
	}    


    private void UpdateText(Text text)
    {
        Debug.Log(text);

        tmp.text = text.text;
    }
	
	private string GetString()
	{
		Character c = GameManager.instance.phaseHelper.currentCharacter;
		if (c == null) return "";
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
