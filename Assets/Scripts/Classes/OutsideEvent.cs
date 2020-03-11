using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEVENT { CATAPULT, ARROW, OTHER}

[System.Serializable]
public class OutsideEvent
{
    [Range(0,1)] public float triggerChance;
    public Vector2 catapultCooldownRange;
    public bool onlyTriggerInWaitPhase;
    public float screenShakeAmount;
    public float vignetteAmount;
    public float currentCooldown {get; protected set;}
    protected float refreshTime;
    public bool isUp { get {return Time.time - refreshTime >= currentCooldown;} }
    public eEVENT eventSFX;

    public virtual bool TryTrigger(GameObject go)
    {
        refreshTime = Time.time;
        if(Random.Range(0,1) <= triggerChance){
            currentCooldown = Random.Range(catapultCooldownRange.x, catapultCooldownRange.y);

            AudioManager.instance.outsideEvents[(int)eventSFX].Post(go, (uint)AkCallbackType.AK_MusicSyncUserCue, CallbackFunction);

            return true;
        }
        return false;
    }

    public virtual void CallbackFunction(object in_cookie, AkCallbackType in_type, object in_info)
    {
        if (screenShakeAmount != 0.0f)
            EffectManager.instance.screenShake.Shake(1f, screenShakeAmount);
        if (vignetteAmount != 0.0f)
            EffectManager.instance.Vign(1f, vignetteAmount);

        
    }
}
