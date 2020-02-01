using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GearSkin", menuName = "ScriptableObjects/GearSkin", order = 1)]
public class GearSkin : ScriptableObject
{
    public Sprite[] stateSkins;
}
