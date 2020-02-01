using UnityEngine;
using System.Collections.Generic;
using System.IO;

#if UNITY_EDITOR 
using UnityEditor;
#endif 

public class DialogueScriptableObject : ScriptableObject
{

	/*public List<string>[] armor;
	public List<string>[] sword;
	public List<string>[] bow;*/

	public GearDialog armor;
	public GearDialog sword;
	public GearDialog bow;

	[System.Serializable]
	public class GearDialog{
		public string[] lowest;
		public string[] low;
		public string[] equal;
		public string[] high;
		public string[] highest;

		public void Set(List<string>[] list)
		{
			lowest = list[0].ToArray();
			low = list[1].ToArray();
			equal = list[2].ToArray();
			high = list[3].ToArray();
			highest = list[4].ToArray();
		}
	}



    #if UNITY_EDITOR
    public static DialogueScriptableObject CreateInstance(List<string>[] a, List<string>[] s, List<string>[] b)
	{
		DialogueScriptableObject data = CreateInstance<DialogueScriptableObject>();
		data.armor = new GearDialog();
		data.sword = new GearDialog();
		data.bow = new GearDialog();
		data.armor.Set(a);
		data.sword.Set(s);
		data.bow.Set(b);
		string path = AssetDatabase.GetAssetPath(Selection.activeObject);
		path = "Assets/Data/Scriptable/dialogueData";
		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path+".asset");
		AssetDatabase.CreateAsset(data, assetPathAndName);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		EditorUtility.FocusProjectWindow();
		Selection.activeObject = data;
		return data;
	}
    #endif
}
