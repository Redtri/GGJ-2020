using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DataToScriptable
{
    [MenuItem("Assets/GenerateData")]
    public static void CreateScriptable()
    {
        CharacterScriptable someInstance = ScriptableObject.CreateInstance<CharacterScriptable>() as CharacterScriptable;

        someInstance.Init("LE udr", "Gorlock", true, new int[]{ 0,1,2}, new Vector2[] { new Vector2(0,1), new Vector2(0, 1), new Vector2(0, 1)}, 1, false, "blablabla" );
    }


    
}
