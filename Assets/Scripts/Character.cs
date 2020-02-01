using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{

    public string c_Name;
    public string c_Surname;

    public bool nameRandom;
    public int[] gearValue = new int[3];
    public Vector2[] gearExpectation = new Vector2[3];

    public float hero = 1;

    public bool privateText;
    public string forcedText;

    public int ironAdd;

    const int maxGearValue = 8;
    const int minGearValue = 0;    

    public Character(string p_name, string p_surname, bool p_randomName, int[] p_gearValue, Vector2[] p_gearExpectation, float p_hero, bool p_privateText, string p_forcedText, int p_ironAdd)
    {
        c_Name = p_name;
        c_Surname = p_surname;
        nameRandom = p_randomName;
        gearValue = p_gearValue;
        gearExpectation = p_gearExpectation ;
        hero = p_hero;
        privateText = p_privateText;
        forcedText = p_forcedText;
        ironAdd = p_ironAdd;       
    }    

    public Character(Character toCopy)
    {
        c_Name = toCopy.c_Name;
        c_Surname = toCopy.c_Surname;
        nameRandom = toCopy.nameRandom;
        gearValue = toCopy.gearValue;
        gearExpectation = toCopy.gearExpectation;
        hero = toCopy.hero;
        privateText = toCopy.privateText;
        forcedText = toCopy.forcedText;
        ironAdd = toCopy.ironAdd;
    }


}
