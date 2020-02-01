using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-5000)]
public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;

    [HideInInspector()]
    public Dictionary<string, int> characters;

    [HideInInspector()]
    public List<Character> charactersAlive;

    [HideInInspector()]
    public LinkedList<Character> charactersInQueue;
    
    [Header("Scriptable Object")]
    public CharacterScriptable[] scriptableChara;

    [Header("Queue")]
    public float percentNobody;
    public float percentAlive;
    public float percentNewChar;

    [Header("Misc")]
    public CharacterActor characterActor;
    public CharacterSkin[] characterTemplates;

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
        onCharacterUpdate?.Invoke(characterActor);
        int randomIndex = Random.Range(0, characterTemplates.Length);
        characterTemplates[randomIndex].SwapSprites(characterActor.bodyParts, characterTemplates[randomIndex].CherryPick(characterTemplates));

        percentNewChar = 100f - percentNobody - percentAlive;
    }

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

    public void UpdateCurrentCharacter(int index, bool rightClick)
    {
        characterActor.UpdateGearValue(index, rightClick);
        onCharacterUpdate?.Invoke(characterActor);
    }

    public void AddCharacterToQueue()
    {
        float percent = Random.Range(0.0f, 1.0f);

        Character c = null;

        //Il n'y a personne on ajoute null
        if (percent <= percentNobody)
        {           
            charactersInQueue.AddLast(c);
        }
        // On ajoute un personnage vivant
        else if (percent > percentNobody && percent <= percentAlive)
        {
            int randomAlive = Random.Range(0, charactersAlive.Count - 1);

            while (!charactersInQueue.Contains(charactersAlive[randomAlive]))
            {
                randomAlive = Random.Range(0, charactersAlive.Count - 1);
            }

            charactersInQueue.AddLast(charactersAlive[randomAlive]);
        }
        // On ajoute un nouveau personnage
        else if (percent > percentAlive)
        {
            c = scriptableChara[Random.Range(0, scriptableChara.Length - 1)].character;

            if (c.nameRandom)
            {
                string fileDataName = System.IO.File.ReadAllText("./Assets/Data/Name.csv");
                string[] lines2 = fileDataName.Split("\n"[0]);


                int countNbrc_Name = IntParseFast(lines2[0].Trim().Split(","[0])[0]);

                int nbrc_Name = Random.Range(1, countNbrc_Name);
                int nbrSurc_Name = Random.Range(1, countNbrc_Name);

                c.c_Name = (lines2[nbrc_Name].Trim()).Split(","[0])[0];
                c.c_Surname = (lines2[nbrSurc_Name].Trim()).Split(","[0])[1];

                charactersInQueue.AddLast(c);
            }

        }
    }

    void int IntParseFast(string value)
    {
        int result = 0;
        for (int i = 0; i < value.Length; i++)
        {
            char letter = value[i];
            result = 10 * result + (letter - 48);
        }
        return resu
}