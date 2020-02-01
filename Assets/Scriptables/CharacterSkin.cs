using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CharacterSkin", menuName = "Scriptable/CharacterSkin", order = 1)]
public class CharacterSkin : ScriptableObject
{
    public Sprite[] bodyParts;

    public void SwapSprites(SpriteRenderer[] renders, Sprite[] overrideSkin = null) {
        if(renders.Length == bodyParts.Length) {
            for(int i = 0; i < renders.Length; ++i) {
                if (overrideSkin == null) {
                    renders[i].sprite = bodyParts[i];
                } else {
                    renders[i].sprite = overrideSkin[i];
                }
            }
        }
    }

    public Sprite[] CherryPick(CharacterSkin[] skins)
    {
        Sprite[] proceduralSkin = new Sprite[bodyParts.Length];

        for(int i = 0; i < bodyParts.Length; ++i) {
            proceduralSkin[i] = skins[Random.Range(0, skins.Length - 1)].bodyParts[i];
        }

        return proceduralSkin;
    }
}
