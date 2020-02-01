using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-5000)]
public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;

    Dictionary<string, int> characters;
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
    public void UpdateActorProfile(Character character)
    {
        characterActor.data = character;
        onCharacterUpdate?.Invoke(characterActor);
    }

}