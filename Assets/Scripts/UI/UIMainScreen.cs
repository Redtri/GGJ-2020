using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIMainScreen : MonoBehaviour
{
	public GameObject splashScreen;
	public Image transition;
	public Text mainTitle;
	public ParticleSystem fire;
	public MeshRenderer wall;
	public float duration;

	private bool isPlaying = false;

	public void StartGame()
	{
		StopAllCoroutines();
		
		Sequence startSequence = DOTween.Sequence();
		//Fade background to black
		startSequence.Append(transition.DOFade(1f, duration/2f));
		startSequence.Join(mainTitle.transform.DOScale(new Vector3(1.25f, 1.25f, 1f), duration/4f));
		//Fade out title
		startSequence.Append(mainTitle.transform.DOScale(new Vector3(1f, 1f, 1f), duration/4f));
		startSequence.Join(mainTitle.DOFade(0f, duration/4f));
		//Fade black screen to game screen
		startSequence.AppendCallback(() => splashScreen.gameObject.SetActive(false));
		startSequence.Join(transition.DOFade(0f, duration/2f));
		//Start game
		startSequence.AppendCallback(() => GameManager.instance.StartPhase());
		//startSequence.AppendCallback(() => GetComponent<Canvas>().enabled = false);
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
}
