using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OutsideEvent
{
    [Range(0,1)] public float triggerChance;
    public Vector2 catapultCooldownRange;
    public bool onlyTriggerInWaitPhase;
    public float screenShakeAmount;
    public float vignetteAmount;
    public float currentCooldown {get; private set;}
    protected float refreshTime;
    public bool isUp { get {return Time.time - refreshTime >= currentCooldown;} }
    public AK.Wwise.Event eventSFX;
    [Header("DEBUG")]
    public AudioClip clip;

    /*public AK.Wwise.Event TryTrigger()
    {
        refreshTime = Time.time;
        if(Random.Range(0,1) <= triggerChance){
            currentCooldown = Random.Range(catapultCooldownRange.x, catapultCooldownRange.y);

            return eventSFX;
        }
        return null;
    }*/
    public virtual AudioClip TryTrigger()
    {
        refreshTime = Time.time;
        if(Random.Range(0,1) <= triggerChance){
            currentCooldown = Random.Range(catapultCooldownRange.x, catapultCooldownRange.y);

            return clip;
        }
        return null;
    }
}
