using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    public TextMeshProUGUI ironAmountTxt;
    public TextMeshProUGUI[] gearTexts;
    
    private void Start()
    {
        CharacterManager.instance.onCharacterUpdate += UpdateGearUI;

    }

    private void OnDisable()
    {
        CharacterManager.instance.onCharacterUpdate -= UpdateGearUI;
    }

    public void UpdateGearUI(Character characterUpdated)
    {
        for(int i = 0; i < characterUpdated.gears.Length; ++i) {
            gearTexts[i].text = characterUpdated.gears[i].ToString();
        }
        ironAmountTxt.text = characterUpdated.ironAmount.ToString();
    }
}
