using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Physical representation of the character in the world
public class CharacterActor : MonoBehaviour
{
    [Header("Data")]
    public Character data;
    //TODO : Put this variable into Character scriptable object
    public int maxGearUpgrade;

    [Header("Visuals")]
    public SpriteRenderer[] bodyParts;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void LoadSkin(List<Sprite> sprites)
    {
        for(int i = 0; i < sprites.Count; ++i) {
            bodyParts[i].sprite = sprites[i];
        }
    }

    public void UpdateGearValue(int index, bool minus = false)
    {
        if (data.gearValue.Length > 0 && index < data.gearValue.Length) {
            if (minus) {
                if(data.gearValue[index] > 0) {
                    --data.gearValue[index];
                    ++GameManager.instance.playerHelper.ironAmount;
                }
            } else if(GameManager.instance.playerHelper.ironAmount > 0 && data.gearValue[index] < maxGearUpgrade) {
                ++data.gearValue[index];
                --GameManager.instance.playerHelper.ironAmount;
            }
        }
    }

    public void LoadCharacterProfile(Character character)
    {
        data = character;
    }

    public void EnterForge(float entranceDuration)
    {
        GetComponent<Animator>().SetTrigger("entrance");
    }

    public void LeaveForge(float leaveDuration)
    {
        GetComponent<Animator>().SetTrigger("leaving");
    }
}
