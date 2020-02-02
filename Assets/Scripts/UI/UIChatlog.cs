using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class UIChatlog : MonoBehaviour
{

	private Text[] texts;
	private int count = 0;

	private void Awake()
	{
		texts = GetComponentsInChildren<Text>();
		texts.OrderBy(t => t.transform.position.y);
		texts = texts.Reverse().ToArray();
		foreach(Text t in texts)
		{
			t.text = "";
		}
	}



	public void SendMessage(string text, float delay)
	{
		StartCoroutine(LogMessage(text,delay));
	}

	private IEnumerator LogMessage(string text, float delay)
	{
		yield return new WaitForSecondsRealtime(delay);
		
		for(int i = texts.Length-1; i>0; i--)
		{
			texts[i].text = texts[i - 1].text;
		}
		
		texts[0].text = text;
		
	}

	public static void AddLogMessage(string message, float value)
	{
		FindObjectOfType<UIChatlog>().SendMessage(message, value);
	}
}
