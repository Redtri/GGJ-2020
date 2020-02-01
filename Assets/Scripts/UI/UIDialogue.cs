using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrunoMikoski.TextJuicer;
using TMPro;
public class UIDialogue : MonoBehaviour
{
	public DialogueScriptableObject dialogData;
	private TMP_TextJuicer juicer;
	private TextMeshProUGUI tmp;

	private float progress = 0;
	public float speed = 10;

	private void Awake()
	{
		juicer = GetComponent<TMP_TextJuicer>();
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
		SetText(dialogData.sword.low[Random.Range(0, 4)]);	
	}

	private void OnLeave()
	{
		SetText("");
	}

	private void Update()
	{
		if(tmp.text.Length > 0)
		{
			progress += Time.deltaTime / (float)(tmp.text.Length) *  speed;
		}else
		{
			progress = 0;
		}
		
		juicer.SetProgress(progress);
		juicer.SetDirty();
		
	}

	public void SetText(string txt) {
		progress = 0;
		tmp.text = txt;
		juicer.SetDirty();
	}
}
