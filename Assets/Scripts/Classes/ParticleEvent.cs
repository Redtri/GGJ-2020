using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParticleEvent : OutsideEvent
{
    [System.Serializable]
    public class ParticleSystemOffset
    {
        public ParticleSystem system;
        public Vector3 minRandomOffset;
        public Vector3 maxRandomOffset;
        [HideInInspector] public Vector3 basePosition;
    }

    public List<ParticleSystemOffset> particleSystems;

    public void Init()
    {
        for(int i = 0; i < particleSystems.Count; ++i){
            particleSystems[i].basePosition = particleSystems[i].system.transform.position;
        }
    }

    private void ResetPositions()
    {
        for(int i = 0; i < particleSystems.Count; ++i){
            particleSystems[i].system.transform.position = particleSystems[i].basePosition;
        }
    }

    private void RandomOffsets(bool playSystems = true)
    {
        for(int i = 0; i < particleSystems.Count; ++i){
            particleSystems[i].system.transform.position += new Vector3(Random.Range(particleSystems[i].minRandomOffset.x, particleSystems[i].maxRandomOffset.x),
                                                                        Random.Range(particleSystems[i].minRandomOffset.y, particleSystems[i].maxRandomOffset.y),
                                                                        Random.Range(particleSystems[i].minRandomOffset.z, particleSystems[i].maxRandomOffset.z)
                                                                    );
            if(playSystems)
                particleSystems[i].system.Play(true);
        }
    }

    public override bool TryTrigger(GameObject go)
    {
        ResetPositions();
        refreshTime = Time.time;
        if (Random.Range(0, 1) <= triggerChance) {
            currentCooldown = Random.Range(catapultCooldownRange.x, catapultCooldownRange.y);

            AudioManager.instance.outsideEvents[(int)eventSFX].Post(go, (uint)AkCallbackType.AK_MusicSyncUserCue, CallbackFunction);

            return true;
        }
        return false;
    }

    public override void CallbackFunction(object in_cookie, AkCallbackType in_type, object in_info)
    {
        if (screenShakeAmount != 0.0f)
            EffectManager.instance.screenShake.Shake(1f, screenShakeAmount);
        if (vignetteAmount != 0.0f)
            EffectManager.instance.Vign(1f, vignetteAmount);

        RandomOffsets();
    }
}
