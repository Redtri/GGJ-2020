using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIChatlog : MonoBehaviour
{

	public int maxLog = 8;
	public Font font;
	public int textSize = 15;
	private List<Text> texts = new List<Text>();


	
	void Update()
	{
		if(Random.value > 0.94f)
		{
			if(Random.value > 0.5f)
			{
				SendMessage("gustave -> prout", Random.Range(0, 2));
			}
			else
			{
				SendMessage("gustave -> dead", Random.Range(0, 3));
			}
			
		}
	}


	public void SendMessage(string text, float delay)
	{
		StartCoroutine(LogMessage(text,delay));
	}

	private IEnumerator LogMessage(string text, float delay)
	{
		yield return new WaitForSecondsRealtime(delay);
		if (texts.Count >= maxLog)
		{
			Destroy(texts[0].gameObject);
			texts.RemoveAt(0);
		}
		GameObject g = new GameObject("chatLog");
		g.transform.parent = transform;
		Text t = g.AddComponent<Text>();
		t.text = text;
		t.font = font;
		t.fontSize = textSize;
		texts.Add(t);
		
	}
}
