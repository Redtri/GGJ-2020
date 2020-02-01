using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Data")]
    //TODO : Put this variable in the GameManager instead of THE FUCKING CHARACTER
    public int ironAmount;
    //TODO : Create a data class for character that would hold these values (+ Scriptable object for individual templates)
    public int[] gears;
    public int maxGearUpgrade;

    [Header("Visuals")]
    public SpriteRenderer[] bodyParts;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void UpdateGearValue(int index, bool minus = false)
    {
        if (gears.Length > 0 && index < gears.Length) {
            if (minus) {
                if(gears[index] > 0) {
                    --gears[index];
                    ++ironAmount;
                }
            } else if(ironAmount > 0 && gears[index] < maxGearUpgrade) {
                ++ gears[index];
                --ironAmount;
            }
        }
    }
}
