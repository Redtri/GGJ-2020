using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//Physical representation of the character in the world
public class CharacterActor : MonoBehaviour
{
    [Header("Data")]
    public Character data;
    //TODO : Put this variable into Character scriptable object
    public int maxGearUpgrade;

    [Header("Visuals")]
    public SpriteRenderer[] bodyParts;

    [Header("Dotween")]
    public Transform reachPosition;
    public AnimationCurve sinuoisde;


    private Vector3 basePosition;

    void Start()
    {
        basePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void Update()
    {
        
    }

    public void LoadSkin()
    {
        for (int i = 0; i < data.gears.Count; ++i) {
            switch (data.gearValue[i]) {
                case 1:
                case 2:
                case 3:
                    bodyParts[i].sprite = data.gears[i].stateSkins[0];
                break;
                case 4:
                case 5:
                case 6:
                    bodyParts[i].sprite = data.gears[i].stateSkins[1];
                break;
                case 7:
                case 8:
                    bodyParts[i].sprite = data.gears[i].stateSkins[2];
                break;
            }
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
        LoadSkin();
    }

    public void LoadCharacterProfile(Character character)
    {
        data = character;
    }

    public void EnterForge(float entranceDuration)
    {

        Sequence myAwesomeSequence = DOTween.Sequence();
        myAwesomeSequence.Append(transform.DOMoveX(reachPosition.position.x, entranceDuration));       
        myAwesomeSequence.Join(transform.DOMoveY(reachPosition.position.y, entranceDuration).SetEase(sinuoisde));

        //GetComponent<Animator>().SetTrigger("entrance");
    }

    public void LeaveForge(float leaveDuration)
    {

        Sequence myAwesomeSequence = DOTween.Sequence();
        myAwesomeSequence.Append(transform.DOMoveX(basePosition.x, leaveDuration));
        myAwesomeSequence.Join(transform.DOMoveY(basePosition.y, leaveDuration).SetEase(sinuoisde));

        //GetComponent<Animator>().SetTrigger("leaving");
    }
}
