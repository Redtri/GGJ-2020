using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIMainScreen : MonoBehaviour
{
	public static UIMainScreen instance;
	public GameObject splashScreen;
	public Image transition;
	public TextMeshProUGUI mainTitle;
	public ParticleSystem fire;
	public MeshRenderer wall;
	public float duration;

	private void Awake()
	{
		if(!instance)
			instance = this;
		else
			Destroy(gameObject);
	}

	public Sequence Fade(bool fadeIn)
	{
		Sequence fadeSequence = DOTween.Sequence();

		if(fadeIn){
			//Fade background
			fadeSequence.Append(transition.DOFade(1f, duration/3f));
			fadeSequence.Join(mainTitle.DOFade(1f, duration/3f));
			fadeSequence.Join(mainTitle.transform.DOScale(new Vector3(1.25f, 1.25f, 1f), duration/3f));
			fadeSequence.Join(mainTitle.fontMaterial.DOFloat(1f, "_GlowPower", duration/3f));
			
			//Fade title
			fadeSequence.Append(mainTitle.transform.DOScale(new Vector3(1f, 1f, 1f), duration/3f));
			fadeSequence.Join(mainTitle.fontMaterial.DOFloat(0f, "_GlowPower", duration/3f));
			//Fade black screen to game screen
			fadeSequence.AppendCallback(() => splashScreen.gameObject.SetActive(true));
			fadeSequence.Join(transition.DOFade(0f, duration/3f));
		}else{
			//Fade background
			fadeSequence.Append(transition.DOFade(1f, duration/3f));
			fadeSequence.Join(mainTitle.transform.DOScale(new Vector3(1.25f, 1.25f, 1f), duration/3f));
			fadeSequence.Join(mainTitle.fontMaterial.DOFloat(1f, "_GlowPower", duration/3f));
			
			//Fade title
			fadeSequence.Append(mainTitle.transform.DOScale(new Vector3(1f, 1f, 1f), duration/3f));
			fadeSequence.Join(mainTitle.DOFade(0f, duration/3f));
			fadeSequence.Join(mainTitle.fontMaterial.DOFloat(0f, "_GlowPower", duration/3f));
			//Fade black screen to game screen
			fadeSequence.AppendCallback(() => splashScreen.gameObject.SetActive(false));
			fadeSequence.Join(transition.DOFade(0f, duration/3f));
		}

		return fadeSequence;
	}
}
