using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainScreen : MonoBehaviour
{
	public Image splashScreen;
	public Text mainTitle;
	public float duration;

	private bool isPlaying = false;

	public void StartGame()
	{
		StopAllCoroutines();
		StartCoroutine(Transition(duration));
	}

	private void Update()
	{
		if (!isPlaying)
		{
			if (Input.anyKeyDown)
			{
				isPlaying = true;
				StartGame();
			}
		}
	}

	private IEnumerator Transition(float duration)
	{
		float t = 0;
		while(t < duration)
		{
			t += Time.deltaTime;
			float n = 1 - t / duration;
			splashScreen.color = new Color(splashScreen.color.r, splashScreen.color.g, splashScreen.color.b, n*n);
			mainTitle.color = new Color(splashScreen.color.r, splashScreen.color.g, splashScreen.color.b, n*n/2);

			yield return null;
		}
		splashScreen.gameObject.SetActive(false);
		mainTitle.gameObject.SetActive(false);
		GameManager.instance.StartPhase();
	}
}
