using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class CharacterScriptable : ScriptableObject
{
   [SerializeField]
   public Character character;

    public void Init(string c_Name, string c_Surname, bool nameRandom, int[] gearValue, Vector2[] gearExpectation, float hero, bool privateText, string forcedText, int ironAdd)
    {
        character = new Character(c_Name, c_Surname, nameRandom, gearValue, gearExpectation, hero, privateText, forcedText, ironAdd);

    /*    character.c_Name = c_Name;
        character.c_Surname = c_Surname;
        character.nameRandom = nameRandom;
        character.gearValue = gearValue;
        character.gearExpectation = gearExpectation;
        character.hero = hero;
        character.privateText = privateText;
        character.forcedText = forcedText;*/
    }

    public static CharacterScriptable CreateInstance(string c_Name, string c_Surname, bool nameRandom, int[] gearValue, Vector2[] gearExpectation, float hero, bool privateText, string forcedText, int ironAdd)
    {
        CharacterScriptable data = CreateInstance<CharacterScriptable>();
        data.Init(c_Name, c_Surname, nameRandom, gearValue, gearExpectation, hero, privateText, forcedText, ironAdd);

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);

        path = "Assets/Data/Scriptable/" + (nameRandom ? "r_" : "o_");

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + c_Name + " " + c_Surname + ".asset");

        AssetDatabase.CreateAsset(data, assetPathAndName);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = data;


        return data;
    }



}


