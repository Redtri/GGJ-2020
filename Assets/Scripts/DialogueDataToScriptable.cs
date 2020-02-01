using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR 
using UnityEditor;

public class DialogueDataToScriptable : MonoBehaviour
{
	[MenuItem("Assets/GenerateDialogueData")]
	public static void CreateScriptable()
	{
		string fileData = System.IO.File.ReadAllText("./Assets/Data/Dialogue.csv");
		string[] lines = fileData.Split("\n"[0]);

		List<string>[] armor = new List<string>[5];
		for (int i = 0; i < armor.Length; i++) armor[i] = new List<string>();
		List<string>[] sword = new List<string>[5];
		for (int i = 0; i < sword.Length; i++) sword[i] = new List<string>();
		List<string>[] bow = new List<string>[5];
		for (int i = 0; i < bow.Length; i++) bow[i] = new List<string>();

		bool ignoreFirst = true;
		foreach (string item in lines)
		{
			if (ignoreFirst)
			{
				ignoreFirst = false;
				continue;
			}

			string[] i = item.Split(';');
			if(i.Length < 10)
			{
				break;
			}

			if (i[3].Length > 0) armor[0].Add(i[3]);
			if (i[4].Length > 0) armor[1].Add(i[4]);
			if (i[5].Length > 0) armor[2].Add(i[5]);
			if (i[6].Length > 0) armor[3].Add(i[6]);
			if (i[7].Length > 0) armor[4].Add(i[7]);

			if (i[8].Length > 0) sword[0].Add(i[8]);
			if (i[9].Length > 0) sword[1].Add(i[9]);
			if (i[10].Length > 0) sword[2].Add(i[10]);
			if (i[11].Length > 0) sword[3].Add(i[11]);
			if (i[12].Length > 0) sword[4].Add(i[12]);

			if (i[13].Length > 0)  bow[0].Add(i[13]);
			if (i[14].Length > 0)  bow[1].Add(i[14]);
			if (i[15].Length > 0) bow[2].Add(i[15]);
			if (i[16].Length > 0) bow[3].Add(i[16]);
			if (i[17].Length > 0) bow[4].Add(i[17]);
		}

		DialogueScriptableObject.CreateInstance(armor, sword, bow);
	}
}
#endif
