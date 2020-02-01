using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    public Transform gameLayout;
    public TextMeshProUGUI ironAmountTxt;
    public TextMeshProUGUI[] gearTexts;
    
    private void OnEnable()
    {
        CharacterManager.instance.onCharacterUpdate += UpdateGearUI;
        GameManager.instance.phaseHelper.onEntranceEnd += InverseUI;
        GameManager.instance.phaseHelper.onPhaseEnd += DisplayUI;
    }

    private void OnDisable()
    {
        CharacterManager.instance.onCharacterUpdate -= UpdateGearUI;
        GameManager.instance.phaseHelper.onEntranceEnd -= InverseUI;
        GameManager.instance.phaseHelper.onPhaseEnd -= DisplayUI;
    }

    public void UpdateGearUI(CharacterActor characterUpdated)
    {
        for(int i = 0; i < characterUpdated.data.gearValue.Length; ++i) {
            gearTexts[i].text = characterUpdated.data.gearValue[i].ToString();
        }
        ironAmountTxt.text = GameManager.instance.playerHelper.ironAmount.ToString();
    }

    public void InverseUI()
    {
        gameLayout.gameObject.SetActive(!gameLayout.gameObject.activeInHierarchy);
    }

    public void DisplayUI(bool value)
    {
        gameLayout.gameObject.SetActive(value);
    }
}
