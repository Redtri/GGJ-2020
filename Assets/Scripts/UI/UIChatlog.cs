using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;



public class UIChatlog : MonoBehaviour
{

	private Text[] texts;
	private int count = 0;

    public enum TyopeOfLog
    {
        Bad,
        Neutral,
        Good
    }


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


    private void Update()
    {
        /*
        TyopeOfLog type = TyopeOfLog.Good;

        switch (Random.Range(0, 3))
        {
            case 0:
                type = TyopeOfLog.Bad;
                break;
            case 1:
                type = TyopeOfLog.Neutral;
                break;
            case 2:
                type = TyopeOfLog.Good;
                break;
        }

        FindObjectOfType<UIChatlog>().SendMessage("Salut " + (Random.Range(0,100) > 50 ?  "toi" : "mec "), 0.5f, type);*/
    }


    public void SendMessage(string text, float delay, TyopeOfLog tyopeOfLog)
	{
		StartCoroutine(LogMessage(text,delay, tyopeOfLog));
	}

	private IEnumerator LogMessage(string text, float delay, TyopeOfLog tyopeOfLog)
	{
		yield return new WaitForSecondsRealtime(delay);
		
		for(int i = texts.Length-1; i>0; i--)
		{
			texts[i].text = texts[i - 1].text;
            texts[i].color = texts[i - 1].color;
        }

        texts[0].text = text;

        switch (tyopeOfLog)
        {
            case TyopeOfLog.Bad:
                texts[0].color = Color.red;
                break;

            case TyopeOfLog.Neutral:
                texts[0].color = Color.black;
                break;

            case TyopeOfLog.Good:
                texts[0].color = Color.green;
                break;
        }
    }

	public static void AddLogMessage(string message, float delay, TyopeOfLog tyopeOfLog)
	{
		FindObjectOfType<UIChatlog>().SendMessage(message, delay);
	}
}
