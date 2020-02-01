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
    public LinkedList<Character> charactersAlive;

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

    public void GetNewCharacter()
    {
        float percent = Random.Range(0.0f, 1.0f);

        if(percent <= percentNobody)
        {

        }
        else if (percent > percentNobody && percent <= percentAlive)
        {

        }
        else if (percent > percentAlive)
        {

        }
    }
}