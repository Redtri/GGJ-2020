using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CharacterSkin", menuName = "Scriptable/CharacterSkin", order = 1)]
public class CharacterSkin : ScriptableObject
{
    public Sprite[] bodyParts;

    public void SwapSprites(SpriteRenderer[] renders) {
        if(renders.Length == bodyParts.Length) {
            for(int i = 0; i < renders.Length; ++i) {
                renders[i].sprite = bodyParts[i];
            }
        }
    }
}
