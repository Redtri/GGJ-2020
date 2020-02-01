using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class CharacterScriptable : ScriptableObject
{
   [SerializeField]
   Character character;

    public void Init(string c_Name, string c_Surname, bool nameRandom, int[] gearValue, Vector2[] gearExpectation, int hero, bool privateText, string forcedText)
    {
        character.c_Name = c_Name;
        character.c_Surname = c_Surname;
        character.nameRandom = nameRandom;
        character.gearValue = gearValue;
        character.gearExpectation = gearExpectation;
        character.hero = hero;
        character.privateText = privateText;
        character.forcedText = forcedText;
    }



}


