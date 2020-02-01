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

    public float Battle()
    {
        float p1 = 1 - Mathf.Abs(gearExpectation[0].x - gearValue[0]) / maxGearValue;
        float p2 = 1 - Mathf.Abs(gearExpectation[1].x - gearValue[1]) / maxGearValue;
        float p3 = 1 - Mathf.Abs(gearExpectation[2].x - gearValue[2]) / maxGearValue;

        float p1bis = 1 - Mathf.Abs(gearExpectation[0].y - gearValue[0]) / maxGearValue;
        float p2bis = 1 - Mathf.Abs(gearExpectation[1].y - gearValue[1]) / maxGearValue;
        float p3bis = 1 - Mathf.Abs(gearExpectation[2].y - gearValue[2]) / maxGearValue;

        return Mathf.Min(p1, p2, p3, p1bis, p2bis, p3bis);
        
    }


}
