using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Sweet.UI;

public class UI_Manager : MonoBehaviour
{
    public Canvas pauseScreen;
    public Transform pauseScreenContainer;
    public Transform gameLayout;
    public UIIngot ingots;
    public TextMeshProUGUI ironAmountTxt;
    public TextMeshProUGUI[] gearTexts;
    public UISlider[] bars;
    public UIButton[] buttons;

    public Transform reachPos;

    public static UI_Manager instance;

    private bool isDown;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        CharacterManager.instance.onCharacterUpdate += UpdateGearUI;
        GameManager.instance.phaseHelper.onEntranceEnd += EnableButtons;
       // GameManager.instance.phaseHelper.onPhaseEnd += DisableButtons;
		GameManager.instance.phaseHelper.onLeaving += OnCharacterLeave;
		GameManager.instance.phaseHelper.onWaitStart += DisableButtons;
        DisableButtons();
    }    

    private void OnDisable()
    {
        CharacterManager.instance.onCharacterUpdate -= UpdateGearUI;
        GameManager.instance.phaseHelper.onEntranceEnd -= EnableButtons;
        //GameManager.instance.phaseHelper.onPhaseEnd -= DisableButtons;
		GameManager.instance.phaseHelper.onLeaving -= OnCharacterLeave;
		GameManager.instance.phaseHelper.onWaitStart -= DisableButtons;


	}

	public void UpdateGearUI(CharacterActor characterUpdated)
    {
        for(int i = 0; i < characterUpdated.data.gearValue.Length; ++i) {
            gearTexts[i].text = characterUpdated.data.gearValue[i].ToString();
            bars[i].value = (float)characterUpdated.data.gearValue[i] / (float)characterUpdated.maxGearUpgrade;
        }
        ironAmountTxt.text = GameManager.instance.playerHelper.ironAmount.ToString();
    }

    public void EnableButtons()
    {
        foreach(UIButton bt in buttons) {
			bt.SetLock(false);
		}
    }

	private void OnCharacterLeave()
	{
		Character c = GameManager.instance.phaseHelper.currentCharacter;
		for (int i = 0; i <c.gearExpectation.Length; i++)
		{
			Vector2 exp = c.gearExpectation[i];
			float min = Mathf.Clamp01(exp.x / Character.maxGearValue);
			float max = Mathf.Clamp01(exp.y / Character.maxGearValue);
			float v = GameManager.instance.phaseHelper.currentCharacter.gearValue[i]/Character.maxGearValue;
			float delta = c.GetDistanceToRange(i);
			if(delta!=0)
			bars[i].SetComparator(min,max, 2, 10, 1);
			/*if (max < 1)
			{
				bars[i].SetComparator(max, 1, 2, 10, 1);
			}else if(min > 0)
			{
				bars[i].SetComparator(0, min, 2, 10, 1);
			}*/
		}
		DisableButtons();
	}

    public void DisableButtons()
    {
        foreach (UIButton bt in buttons) {
			bt.SetLock(true);
        }
    }

    public void PauseScreen(bool show)
    {
        if(show){
            pauseScreen.enabled = true;
        }else{
            pauseScreen.enabled = false;
        }
        foreach(MaskableGraphic maskable in pauseScreen.GetComponentsInChildren<MaskableGraphic>()){
            maskable.color = new Color(maskable.color.r, maskable.color.g, maskable.color.b, 0f);
        }
    }

    public void FadePauseScreen(bool show, float amount)
    {
        float percent = (show) ? amount : 1-amount;

        if(show){
            pauseScreen.enabled = true;
        }else if(percent <= 0.0001f){
            pauseScreen.enabled = false;
        }

        foreach(MaskableGraphic maskable in pauseScreen.GetComponentsInChildren<MaskableGraphic>()){
            maskable.color = new Color(maskable.color.r, maskable.color.g, maskable.color.b, percent);
        }
    }
}
