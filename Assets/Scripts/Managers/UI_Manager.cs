using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UI_Manager : MonoBehaviour
{
    public Canvas pauseScreen;
    public Transform pauseScreenContainer;
    public Transform gameLayout;
    public UIIngot ingots;
    public TextMeshProUGUI ironAmountTxt;
    public TextMeshProUGUI[] gearTexts;
    public Image[] bars;
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
        GameManager.instance.phaseHelper.onPhaseEnd += DisableButtons;
        DisableButtons();
    }    

    private void OnDisable()
    {
        CharacterManager.instance.onCharacterUpdate -= UpdateGearUI;
        GameManager.instance.phaseHelper.onEntranceEnd -= EnableButtons;
        GameManager.instance.phaseHelper.onPhaseEnd -= DisableButtons;
    }

    public void UpdateGearUI(CharacterActor characterUpdated)
    {
        for(int i = 0; i < characterUpdated.data.gearValue.Length; ++i) {
            gearTexts[i].text = characterUpdated.data.gearValue[i].ToString();
            bars[i].fillAmount = (float)characterUpdated.data.gearValue[i] / (float)characterUpdated.maxGearUpgrade;
        }
        ironAmountTxt.text = GameManager.instance.playerHelper.ironAmount.ToString();
    }

    public void EnableButtons()
    {
        foreach(UIButton bt in buttons) {
			bt.SetLock(false);
		}
    }

    public void DisableButtons(bool val1 = false)
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
