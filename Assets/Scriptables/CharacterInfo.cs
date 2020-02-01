using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CharacterInfo", menuName = "Scriptable/CharacterInfo", order = 1)]
public class CharacterInfo : ScriptableObject
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
