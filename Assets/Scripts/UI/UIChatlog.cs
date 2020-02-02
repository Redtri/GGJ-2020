using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;



public class UIChatlog : MonoBehaviour
{

	private Text[] texts;
	private int count = 0;

	public float minTimeNeutralLog = 2;
	public float maxTimeNeutralLog = 10;
	private float nextDuration;
	private float lastTime;
	private List<string> neutralList;

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
		neutralList = new List<string>();
		neutralList.AddRange(CharacterManager.instance.logData.neutralLog);
	}

	private string GetNeutralLog()
	{
		if(neutralList.Count == 0)
		{
			neutralList.AddRange(CharacterManager.instance.logData.neutralLog);
		}
		int id = Random.Range(0, neutralList.Count);
		string s = neutralList[id];
		neutralList.RemoveAt(id);
		return s;

	}

    private void Update()
    {
		if(Time.time > lastTime + nextDuration)
		{
			nextDuration = Random.Range(minTimeNeutralLog, maxTimeNeutralLog);
			lastTime = Time.time;

			SendMessage(GetNeutralLog(),Random.Range(0,5),TyopeOfLog.Neutral);
			if (Random.value > 0.5)
				SendMessage(GetNeutralLog(), Random.Range(5, 10), TyopeOfLog.Neutral);
		}
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

	public static void AddLogMessage(string message, float delay, TyopeOfLog typeOfLog)
	{
		FindObjectOfType<UIChatlog>().SendMessage(message, delay,typeOfLog);
	}
}
