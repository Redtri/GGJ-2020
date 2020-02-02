using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GearType { SWORD, BOW, ARMOR }

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
    public Sprite skin;

    const float maxGearValue = 8;

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
    public void InitSprites(List<GearSkin> tGears, Sprite tSkin)
    {
        gears = tGears;        skin = tSkin;
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
      /*  float p1 = 1f - Mathf.Abs(gearExpectation[0].x - gearValue[0]) / maxGearValue;

        float p2 = 1f - Mathf.Abs(gearExpectation[1].x - gearValue[1]) / maxGearValue;
        float p3 = 1f - Mathf.Abs(gearExpectation[2].x - gearValue[2]) / maxGearValue;

        float p1bis = 1f - Mathf.Abs(gearExpectation[0].y - gearValue[0]) / maxGearValue;
        float p2bis = 1f - Mathf.Abs(gearExpectation[1].y - gearValue[1]) / maxGearValue;
        float p3bis = 1f - Mathf.Abs(gearExpectation[2].y - gearValue[2]) / maxGearValue;
		*/
		/*
        Debug.Log("=============");
        Debug.LogError(p1 + " - " + p1bis);
        Debug.LogError(p2 + " - " + p2bis);
        Debug.LogError(p3 + " - " + p3bis);
        Debug.Log("=============");
        */

		/*p1 = Mathf.Max(p1, p1bis);
        p2 = Mathf.Max(p2, p2bis);
        p3 = Mathf.Max(p3, p3bis);*/

		float sword = GetGearNormalizedDelta(0);
		float bow = GetGearNormalizedDelta(1);
		float armor = GetGearNormalizedDelta(2) ;
		return Mathf.Min(sword, bow, armor);
    }

	

	public float GetGearNormalizedDelta(int index)
	{
		return 1 - Mathf.Abs(GetDistanceToRange(index)) / maxGearValue;
	}


	public float GetDistanceToRange(int index)
	{
		
		if(gearValue[index] < gearExpectation[index].x)
		{
			return gearExpectation[index].x - gearValue[index];
		}else
		{
			if (gearValue[index] > gearExpectation[index].y)
			{
				return gearExpectation[index].y - gearValue[index];
			}
			else
			{
				return 0;
			}
		}
	}
	

	public GearType GetFarestGear(out float distance)
	{
		GearType type = GearType.ARMOR;
		distance = 0;
		float sword = GetGearNormalizedDelta(0);
		float bow = GetGearNormalizedDelta(1);
		float armor = GetGearNormalizedDelta(2);
		if (sword < bow)
		{
			if (armor < sword)
			{
				//armor
				type = GearType.ARMOR;
				distance = GetDistanceToRange(2);
			}
			else
			{
				//Sword
				type = GearType.SWORD;
				distance = GetDistanceToRange(0);
			}	
		}
		else
		{
			if(armor < bow)
			{
				//armor
				type = GearType.ARMOR;
				distance = GetDistanceToRange(2);
			}
			else
			{
				//bow
				type = GearType.BOW;
				distance = GetDistanceToRange(1);
			}
		}

		return type;
	}

	public string GetVictoryLog()
	{
		return CharacterManager.instance.logData.GetRandom(CharacterManager.instance.logData.victoryLog);
	}

	public string GetDeathLog()
	{
		float d = 0;
		var gt =GetFarestGear(out d);
		string str = c_Name + " " + c_Surname + " ";
		var lData = CharacterManager.instance.logData;
		switch (gt)
		{
			case GearType.SWORD:
				if(d > 0)
				{
					return str + lData.GetRandom(lData.lessSword);
				}
				else
				{
					return str + lData.GetRandom(lData.muchSword);
				}
				
			case GearType.BOW:
				if (d > 0)
				{
					return str + lData.GetRandom(lData.lessBow);
				}
				else
				{
					return str + lData.GetRandom(lData.muchBow);
				}
			case GearType.ARMOR:
				if (d > 0)
				{
					return str + lData.GetRandom(lData.lessArmor);
				}
				else
				{
					return str + lData.GetRandom(lData.muchArmor);
				}
		}
		return str + "died from a bug";
	}

}
