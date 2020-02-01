using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Physical representation of the character in the world
public class CharacterActor : MonoBehaviour
{
    [Header("Data")]
    //TODO : Put this variable in the GameManager instead of THE FUCKING CHARACTER
    public Character data;
    public int ironAmount;
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
        if (data.gearValue.Length > 0 && index < data.gearValue.Length) {
            if (minus) {
                if(data.gearValue[index] > 0) {
                    --data.gearValue[index];
                    ++ironAmount;
                }
            } else if(ironAmount > 0 && data.gearValue[index] < maxGearUpgrade) {
                ++data.gearValue[index];
                --ironAmount;
            }
        }
    }
}
