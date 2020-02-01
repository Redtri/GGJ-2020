using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrunoMikoski.TextJuicer;
using TMPro;
public class UIDialogue : MonoBehaviour
{
	private TMP_TextJuicer juicer;
	private TextMeshProUGUI tmp;

	private float progress = 0;
	public float speed = 10;

	private void Awake()
	{
		juicer = GetComponent<TMP_TextJuicer>();
		tmp = GetComponent<TextMeshProUGUI>();
	}

	private void Update()
	{
		juicer.SetDirty();
		if(tmp.text.Length > 0)
		{
			progress += Time.deltaTime / (float)(tmp.text.Length) *  speed;
		}
		
		juicer.SetProgress(progress);
		juicer.SetDirty();
		
	}

	public void SetText(string txt) {
		progress = 0;
		tmp.text = txt;
	}
}
