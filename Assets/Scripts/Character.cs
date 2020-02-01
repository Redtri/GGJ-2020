﻿using System.Collections;
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

        /*
        string fileData = System.IO.File.ReadAllText("./Assets/Data/Name.csv");
        string[] lines = fileData.Split("\n"[0]);


        int countNbrc_Name = IntParseFast(lines[0].Trim().Split(";"[0])[0]);

        int nbrc_Name = Random.Range(1, countNbrc_Name);
        int nbrSurc_Name = Random.Range(1, countNbrc_Name);

        c_Name = (lines[nbrc_Name].Trim()).Split(";"[0])[0];
        c_Surname = (lines[nbrSurc_Name].Trim()).Split(";"[0])[1];

        Debug.Log(c_Name + " " + c_Surname);

        CharacterManager.instance.AddChara(c_Name+ c_Surname);*/
    public Character()
    {
        c_Name = "GEAROES";
    }
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
}
