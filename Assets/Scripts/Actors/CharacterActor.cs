using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

//Physical representation of the character in the world
public class CharacterActor : MonoBehaviour
{
    [Header("Data")]
    public Character data;
    //TODO : Put this variable into Character scriptable object
    public int maxGearUpgrade;

    [Header("Visuals")]
    public SpriteRenderer[] gearParts;
    public SpriteRenderer skinRender;

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

    public void LoadGearSkins()
    {
        for (int i = 0; i < data.gears.Count; ++i) {
            switch (data.gearValue[i]) {
                case 0:
                    gearParts[i].sprite = data.gears[i].stateSkins[0];
                    break;
                case 1:
                case 2:
                case 3:
                    gearParts[i].sprite = data.gears[i].stateSkins[1];
                break;
                case 4:
                case 5:
                case 6:
                    gearParts[i].sprite = data.gears[i].stateSkins[2];
                break;
                case 7:
                case 8:
                    gearParts[i].sprite = data.gears[i].stateSkins[3];
                break;
            }
        }
    }

    public void LoadSkin()
    {
        skinRender.sprite = data.skin;
    }

    public void UpdateGearValue(int index, bool minus = false)
    {
        if (data.gearValue.Length > 0 && index < data.gearValue.Length) {
            if (minus) {
                if(data.gearValue[index] > 0) {
                    --data.gearValue[index];
                    ++GameManager.instance.playerHelper.ironAmount;     
                    EffectManager.instance.screenShake.Shake(0.01f);

                }
            } else if(GameManager.instance.playerHelper.ironAmount > 0 && data.gearValue[index] < maxGearUpgrade) {
                ++data.gearValue[index];
                --GameManager.instance.playerHelper.ironAmount;

                WhiteBalance balance = null;
                EffectManager.instance.postProcessVolume.profile.TryGet(out balance);
                DOVirtual.Float(0, 80f, 0.2f, (float value) => UpdateLens(value, balance))
                         .OnComplete(() => DOVirtual.Float(80f, 0, 0.4f, (float value) => UpdateLens(value, balance)));


                EffectManager.instance.screenShake.Shake(0.05f);
            }
        }
        LoadGearSkins();
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

        foreach(var sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.color = new Color(0, 0, 0, 0.0f);
            sprite.DOColor(new Color(1, 1, 1, 1.0f), 1.0f);
        }

        //GetComponent<Animator>().SetTrigger("entrance");
    }

    public void LeaveForge(float leaveDuration)
    {

        Sequence myAwesomeSequence = DOTween.Sequence();
        myAwesomeSequence.Append(transform.DOMoveX(basePosition.x, leaveDuration));
        myAwesomeSequence.Join(transform.DOMoveY(basePosition.y, leaveDuration).SetEase(sinuoisde));

        foreach (var sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.DOColor(new Color(0, 0, 0, 0.0f), 1.0f);
        }

        //GetComponent<Animator>().SetTrigger("leaving");
    }

    private void UpdateLens(float value, WhiteBalance lens)
    {
        lens.temperature.value = value;
    }


}
