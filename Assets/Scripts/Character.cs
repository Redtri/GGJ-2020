using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{
    public bool doesExist;
    public string c_Name;
    public string c_Surname;

    public bool nameRandom;
    public int[] gearValue = new int[3];
    public Vector2[] gearExpectation = new Vector2[3];

    public float hero = 1;

    public bool privateText;
    public string forcedText;

    public int ironAdd;

    public List<GearSkin> gears;

    const float maxGearValue = 10f;

    public Character()
    {
        doesExist = false;
        gears = new List<GearSkin>();
    }

    public Character(string p_name, string p_surname, bool p_randomName, int[] p_gearValue, Vector2[] p_gearExpectation, float p_hero, bool p_privateText, string p_forcedText, int p_ironAdd)
    {
        doesExist = true;
        c_Name = p_name;
        c_Surname = p_surname;
        nameRandom = p_randomName;
        gearValue = p_gearValue;
        gearExpectation = p_gearExpectation;
        hero = p_hero;
        privateText = p_privateText;
        forcedText = p_forcedText;

        gears = new List<GearSkin>();
    }
    public void InitSprites(List<GearSkin> tGears)
    {
        gears = tGears;
    }

    public Character(Character toCopy)
    {
        gears = new List<GearSkin>();
        doesExist = true;
        c_Name = toCopy.c_Name;
        c_Surname = toCopy.c_Surname;
        nameRandom = toCopy.nameRandom;
        
        gearValue = new int[3]
        {
            toCopy.gearValue[0],
            toCopy.gearValue[1],
            toCopy.gearValue[2]
        };      
    
        gearExpectation = new Vector2[3]
        {
              toCopy.gearExpectation[0],
              toCopy.gearExpectation[1],
              toCopy.gearExpectation[2]
        };

        gears = new List<GearSkin>();

        hero = toCopy.hero;
        privateText = toCopy.privateText;
        forcedText = toCopy.forcedText;
        ironAdd = toCopy.ironAdd;
    }

    public float Battle()
    {
        float p1 = 1f - Mathf.Abs(gearExpectation[0].x - gearValue[0]) / maxGearValue;

        float p2 = 1f - Mathf.Abs(gearExpectation[1].x - gearValue[1]) / maxGearValue;
        float p3 = 1f - Mathf.Abs(gearExpectation[2].x - gearValue[2]) / maxGearValue;

        float p1bis = 1f - Mathf.Abs(gearExpectation[0].y - gearValue[0]) / maxGearValue;
        float p2bis = 1f - Mathf.Abs(gearExpectation[1].y - gearValue[1]) / maxGearValue;
        float p3bis = 1f - Mathf.Abs(gearExpectation[2].y - gearValue[2]) / maxGearValue;

        /*
        Debug.Log("=============");
        Debug.LogError(p1 + " - " + p1bis);
        Debug.LogError(p2 + " - " + p2bis);
        Debug.LogError(p3 + " - " + p3bis);
        Debug.Log("=============");
        */

        p1 = Mathf.Max(p1, p1bis);
        p2 = Mathf.Max(p2, p2bis);
        p3 = Mathf.Max(p3, p3bis);

        return Mathf.Min(p1, p2, p3);

    }


}
