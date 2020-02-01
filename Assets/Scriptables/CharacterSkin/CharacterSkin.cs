using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CharacterSkin", menuName = "ScriptableObjects/CharacterSkin", order = 1)]
public class CharacterSkin : ScriptableObject
{
    public GearSkin[] bodyParts;

    public void SwapSprites(SpriteRenderer[] renders, Sprite[] overrideSkin = null) {
        if(renders.Length == bodyParts.Length) {
            for(int i = 0; i < renders.Length; ++i) {
                if (overrideSkin == null) {
                    renders[i].sprite = bodyParts[i].stateSkins[0];
                } else {
                    renders[i].sprite = overrideSkin[i];
                }
            }
        }
    }
}
