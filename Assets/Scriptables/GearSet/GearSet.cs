using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GearSet", menuName = "ScriptableObjects/GearSet", order = 1)]
public class GearSet : ScriptableObject
{
    public List<GearSkin> gearParts;

    public void SwapSprites(SpriteRenderer[] renders, Sprite[] overrideSkin = null) {
        if(renders.Length == gearParts.Count) {
            for(int i = 0; i < renders.Length; ++i) {
                if (overrideSkin == null) {
                    renders[i].sprite = gearParts[i].stateSkins[0];
                } else {
                    renders[i].sprite = overrideSkin[i];
                }
            }
        }
    }
}
