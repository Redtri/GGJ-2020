using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIMainScreen : MonoBehaviour
{
	public GameObject splashScreen;
	public Image transition;
	public TextMeshProUGUI mainTitle;
	public ParticleSystem fire;
	public MeshRenderer wall;
	public float duration;

	private bool isPlaying = false;

	public void StartGame()
	{
		Sequence startSequence = DOTween.Sequence();
		//Fade background to black
		startSequence.Append(transition.DOFade(1f, duration/4f));
		startSequence.Join(mainTitle.transform.DOScale(new Vector3(1.25f, 1.25f, 1f), duration/4f));
		startSequence.Join(mainTitle.fontMaterial.DOFloat(1f, "_GlowPower", duration/4f));
		//Sequence HALFWAY
		
		//Fade out title
		startSequence.Append(mainTitle.transform.DOScale(new Vector3(1f, 1f, 1f), duration/4f));
		startSequence.Join(mainTitle.DOFade(0f, duration/4f));
		startSequence.Join(mainTitle.fontMaterial.DOFloat(0f, "_GlowPower", duration/4f));
		//Fade black screen to game screen
		startSequence.AppendCallback(() => splashScreen.gameObject.SetActive(false));
		startSequence.Join(transition.DOFade(0f, duration/4f));
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
				if(!Input.GetKeyDown(KeyCode.Mouse0))
                    AudioManager.instance.Validate.Post(GameManager.instance.gameObject);
				isPlaying = true;
				StartGame();
			}
		}
	}
}
