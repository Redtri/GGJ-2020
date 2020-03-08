using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Sweet.UI
{
	[RequireComponent(typeof(ScrollRect))]
	public class UILog : MonoBehaviour
	{
		private ScrollRect scrollRect;

		public enum LogType { Neutral, Positive, Negative, Important }
		[SerializeField]
		private UITextBase neutralTextAsset;
		[SerializeField]
		private UITextBase positiveTextAsset;
		[SerializeField]
		private UITextBase negativeTextAsset;
		[SerializeField]
		private UITextBase importantTextAsset;
		[SerializeField]
		private bool animatedLog = true;

		private void Awake()
		{
			scrollRect = GetComponent<ScrollRect>();
		}

		public void AddLog(string text, LogType logType)
		{
			InstantiateText(text, GetTextAsset(logType));
			StartCoroutine(UpdateScrollBarNextFrame());
		}

		public void AddLog(string text)
		{
			AddLog(text, LogType.Neutral);
		}

		private IEnumerator UpdateScrollBarNextFrame()
		{
			yield return null;
			yield return null;
			scrollRect.verticalScrollbar.value = 0;
		}

		private UITextBase GetTextAsset(LogType logType)
		{
			switch (logType)
			{
				case LogType.Neutral:
					return neutralTextAsset;
				case LogType.Positive:
					return positiveTextAsset;
				case LogType.Negative:
					return negativeTextAsset;
				case LogType.Important:
					return importantTextAsset;
				default:
					return neutralTextAsset;
			}
		}

		private void InstantiateText(string text, UITextBase textAsset)
		{
			if (textAsset == null) return;
			UITextBase temp = Instantiate(textAsset, scrollRect.content.transform);
			temp.SetText(text, animatedLog);
		}

		private IEnumerator WaitBeforeLog(string txt, float delay,LogType logType)
		{
			yield return new WaitForSeconds(delay);
			AddLog(txt, logType);
		}

		public static void AddLogMessage(string txt,float delay, LogType logType)
		{
			UILog log = FindObjectOfType<UILog>();
			if (log)
			{
				log.StartCoroutine( log.WaitBeforeLog(txt, delay, logType));
			}else
			{
				Debug.LogError("No UIlog");
			}
		}

		public void Clear()
		{
			UITextBase[] txtBase = GetComponentsInChildren<UITextBase>();

			for(int i =0; i<txtBase.Length; i++)
			{
				Destroy(txtBase[i].gameObject);
			}
		}
	}
}
