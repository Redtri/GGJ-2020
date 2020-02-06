using UnityEngine;
using System.Collections;


[CreateAssetMenu(fileName = "log data", menuName = "FateSmith/createLog", order = 0)]
public class LogScriptableObject : ScriptableObject
{
	public string[] muchSword;
	public string[] lessSword;
	public string[] muchBow;
	public string[] lessBow;
	public string[] muchArmor;
	public string[] lessArmor;

	public string[] victoryLog;

	public string[] neutralLog;

	public string GetRandom(string[] strs)
	{
		return strs[Random.Range(0, strs.Length)];
	}
}
