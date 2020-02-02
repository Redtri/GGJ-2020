using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UI_Manager : MonoBehaviour
{
    public Transform gameLayout;
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
}
