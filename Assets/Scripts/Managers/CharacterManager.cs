using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-5000)]
public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;

    [HideInInspector()]
    public Dictionary<string, int> characters;

    //[HideInInspector()]
    public List<Character> charactersAlive;

    //[HideInInspector()]
    public List<Character> charactersInQueue;

    [Header("Scriptable Object")]
    public CharacterScriptable[] scriptableChara;

    [Header("Queue")]
    public float percentNobody;
    public float percentAlive;
    public float percentNewChar;

    [Header("Misc")]
    public CharacterActor characterActor;
    public GearSet[] gearTemplates;
    public Sprite[] skinTemplates;

    public delegate void CharacterEvent(CharacterActor character);
    public CharacterEvent onCharacterUpdate;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        } else {
            Destroy(gameObject);
        }

        characters = new Dictionary<string, int> ();

    }

    private void Start()
    {
        percentNewChar = 100f - percentNobody - percentAlive;

        charactersInQueue = new List<Character>();
        charactersAlive = new List<Character>();


        for (int i = 0; i < 5; i++)
        {
            AddCharacterToQueue(true);
        }

        onCharacterUpdate?.Invoke(characterActor);

        UI_Manager.instance.UpdateGearUI(characterActor);

    }

    //Character functions
    public void AddChara(string c_NameSurc_Name)
    {
        if (!characters.ContainsKey(c_NameSurc_Name))
        {
            characters.Add(c_NameSurc_Name, 1);
        }
        else
        {
            characters[c_NameSurc_Name]++;
        }
    }
    //Character ACTOR functions
    public void UpdateActorGearValues(int index, bool rightClick)
    {
        characterActor.UpdateGearValue(index, rightClick);
        onCharacterUpdate?.Invoke(characterActor);
    }

    public void AddCharacterToQueue(bool forceNew = false)
    {
        float percent = Random.Range(0.0f, 1.0f);

        Character c = new Character();

        c.InitSprites(gearTemplates[0].gearParts, skinTemplates[Random.Range(0, skinTemplates.Length)]);

        if (forceNew)
        {
            percent = float.MaxValue;
        }

        //Il n'y a personne on ajoute null
        if (percent <= percentNobody / 100f)
        {
            charactersInQueue.Add(c);
        }
        // On ajoute un personnage vivant
        else if (percent > percentNobody / 100f && percent <= percentAlive / 100f)
        {
            if (charactersAlive.Count > 0)
            {
                int randomAlive = Random.Range(0, charactersAlive.Count);

                int countLoop = 0;
                
                while (charactersInQueue.Contains(charactersAlive[randomAlive]))
                {
                    randomAlive = Random.Range(0, charactersAlive.Count);
                    countLoop++;
                    if(countLoop > 10)
                    {
                        CreateCharacterAndAddToQueue();
                        return;
                    }
                }

                charactersInQueue.Add(charactersAlive[randomAlive]);
            }
        }
        // On ajoute un nouveau personnage
        else if (percent > percentAlive / 100f)
        {
            CreateCharacterAndAddToQueue();
        }
    }

    public void CreateCharacterAndAddToQueue()
    {
        Character scriptChar = scriptableChara[Random.Range(0, scriptableChara.Length)].character;

        Character c = new Character(scriptChar);

        int randomIndex = Random.Range(0, gearTemplates.Length);

        c.InitSprites(gearTemplates[0].gearParts, skinTemplates[Random.Range(0, skinTemplates.Length)]);
            //c.InitSprites(characterTemplates[randomIndex].CherryPick(characterTemplates)); //HERE
        if (c.nameRandom)
        {
            string fileDataName = System.IO.File.ReadAllText("./Assets/Data/Name.csv");
            string[] lines2 = fileDataName.Split("\n"[0]);

            int countNbrc_Name = IntParseFast(lines2[0].Trim().Split(","[0])[0]);

            int nbrc_Name = Random.Range(1, countNbrc_Name);
            int nbrSurc_Name = Random.Range(1, countNbrc_Name);

            c.c_Name = (lines2[nbrc_Name].Trim()).Split(","[0])[0];
            string hisSurname = (lines2[nbrSurc_Name].Trim()).Split(","[0])[1];


            if (characters.ContainsKey(c.c_Name + hisSurname))
                hisSurname += " " + ToRoman(characters[c.c_Name + hisSurname]);

            AddChara(c.c_Name + hisSurname);

            c.c_Surname = hisSurname;

            charactersInQueue.Add(c);
        }
        else
        {
            string name = c.c_Surname;

            if (characters.ContainsKey(c.c_Name + name))
                name += " " + ToRoman(characters[c.c_Name + name]);

            AddChara(c.c_Name + c.c_Surname);

            c.c_Surname = name;

            charactersInQueue.Add(c);

        }
    }

    public int IntParseFast(string value)
    {
        int result = 0;
        for (int i = 0; i < value.Length; i++)
        {
            char letter = value[i];
            result = 10 * result + (letter - 48);
        }
        return result;
   }

    public static string ToRoman(int number)
    {
        if (number < 1) return string.Empty;
        if (number >= 1000) return "M" + ToRoman(number - 1000);
        if (number >= 900) return "CM" + ToRoman(number - 900);
        if (number >= 500) return "D" + ToRoman(number - 500);
        if (number >= 400) return "CD" + ToRoman(number - 400);
        if (number >= 100) return "C" + ToRoman(number - 100);
        if (number >= 90) return "XC" + ToRoman(number - 90);
        if (number >= 50) return "L" + ToRoman(number - 50);
        if (number >= 40) return "XL" + ToRoman(number - 40);
        if (number >= 10) return "X" + ToRoman(number - 10);
        if (number >= 9) return "IX" + ToRoman(number - 9);
        if (number >= 5) return "V" + ToRoman(number - 5);
        if (number >= 4) return "IV" + ToRoman(number - 4);
        if (number >= 1) return "I" + ToRoman(number - 1);
        return string.Empty;
    }

    public void UpdateActorProfile(Character character)
    {
        characterActor.data = character;
        characterActor.LoadGearSkins();
        characterActor.LoadSkin();
        onCharacterUpdate?.Invoke(characterActor);
    }
}
