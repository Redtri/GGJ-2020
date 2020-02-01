using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public Character currentCharacter;
    public CharacterSkin[] characterTemplates;

    public static CharacterManager instance;

    public delegate void CharacterEvent(Character character);
    public CharacterEvent onCharacterUpdate;

    private void Awake()
    {
        if (!instance) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        onCharacterUpdate?.Invoke(currentCharacter);
        characterTemplates[Random.Range(0, characterTemplates.Length)].SwapSprites(currentCharacter.bodyParts);
    }

    public void UpdateCurrentCharacter(int index, bool rightClick)
    {
        currentCharacter.UpdateGearValue(index, rightClick);
        onCharacterUpdate?.Invoke(currentCharacter);
    }
}
