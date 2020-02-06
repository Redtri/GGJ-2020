using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "NameList", menuName = "FateSmith/NameList", order = 0)]
public class NameScriptableObject : ScriptableObject
{

	public string[] names;
	public string[] surnames;
#if UNITY_EDITOR
	[MenuItem("Assets/GenerateNameList")]
	public static void CreateScriptable()
	{
		string fileData = System.IO.File.ReadAllText("./Assets/Data/Name.csv");
		string[] lines = fileData.Split("\n"[0]);

		List<string> lNames = new List<string>();
		List<string> lSurnames = new List<string>();
		for(int i= 1; i< lines.Length; i++)
		{
			
			string[] names = lines[i].Trim().Split(',');
			lNames.Add(names[0]);
			lSurnames.Add(names[1]);
			
		}
		CreateInstance(lNames.ToArray(), lNames.ToArray());

	}
#endif
#if UNITY_EDITOR
	public static NameScriptableObject CreateInstance(string[] names, string[] surnames)
	{
		NameScriptableObject data = CreateInstance<NameScriptableObject>();
		data.names = names;
		data.surnames = surnames;

		string path = AssetDatabase.GetAssetPath(Selection.activeObject);

		path = "Assets/Data/Scriptable/namesList";

		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + ".asset");

		AssetDatabase.CreateAsset(data, assetPathAndName);

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		EditorUtility.FocusProjectWindow();
		Selection.activeObject = data;
		return data;
	}
#endif

	public string GetRandomName()
	{
		return names[Random.Range(0, names.Length)];
	}

	public string GetRandomSurname()
	{
		return surnames[Random.Range(0, surnames.Length)];
	}
}
