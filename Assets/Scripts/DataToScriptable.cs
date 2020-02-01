using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DataToScriptable
{
    [MenuItem("Assets/GenerateData")]
    public static void CreateScriptable()
    {
        string fileData = System.IO.File.ReadAllText("./Assets/Data/Chara.csv");
        string[] lines = fileData.Split("\n"[0]);

        string name;
        string lastName;

        string privateText = "";

        int countLine = 0;

        foreach (string item in lines)
        {           
            if(countLine > 0)
            {
                bool nameRandom = IntParseFast(item.Trim().Split(","[0])[0]) == 0;

                if (nameRandom)
                {
                    string fileDataName = System.IO.File.ReadAllText("./Assets/Data/Name.csv");
                    string[] lines2 = fileDataName.Split("\n"[0]);


                    int countNbrc_Name = IntParseFast(lines2[0].Trim().Split(","[0])[0]);

                    int nbrc_Name = Random.Range(1, countNbrc_Name);
                    int nbrSurc_Name = Random.Range(1, countNbrc_Name);                    

                    name = (lines2[nbrc_Name].Trim()).Split(","[0])[0];
                    lastName = (lines2[nbrSurc_Name].Trim()).Split(","[0])[1];
                }
                else
                {
                    name = item.Trim().Split(","[0])[1];
                    lastName = item.Trim().Split(","[0])[2];
                }

                int[] gearValues =
                {
                    IntParseFast(item.Trim().Split(","[0])[3]),
                    IntParseFast(item.Trim().Split(","[0])[4]),
                    IntParseFast(item.Trim().Split(","[0])[5])
                };

                string gearExpectationA_str = item.Trim().Split(","[0])[6];
                string gearExpectationB_str = item.Trim().Split(","[0])[7];
                string gearExpectationC_str = item.Trim().Split(","[0])[8];
                
                Vector2[] gearExpectation =
                {
                    new Vector2(IntParseFast(gearExpectationA_str.Split(";"[0])[0]), IntParseFast(gearExpectationA_str.Split(";"[0])[1])),
                    new Vector2(IntParseFast(gearExpectationB_str.Split(";"[0])[0]), IntParseFast(gearExpectationB_str.Split(";"[0])[1])),
                    new Vector2(IntParseFast(gearExpectationC_str.Split(";"[0])[0]), IntParseFast(gearExpectationC_str.Split(";"[0])[1]))
                };
                               

                float hero = float.Parse(item.Trim().Split(","[0])[9].Replace(".", ","));

                bool isPrivateText = IntParseFast(item.Trim().Split(","[0])[10]) == 1;

                if (isPrivateText)
                {
                    privateText = item.Trim().Split(","[0])[11];
                }


                int ironAdd = IntParseFast(item.Trim().Split(","[0])[12]);

                CharacterScriptable.CreateInstance(name, lastName, nameRandom, gearValues, gearExpectation, hero, isPrivateText, privateText, ironAdd);


            }

            countLine++;
        }
        

        
        
    }

    static int IntParseFast(string value)
    {
        int result = 0;
        for (int i = 0; i < value.Length; i++)
        {
            char letter = value[i];
            result = 10 * result + (letter - 48);
        }
        return result;
    }

}
