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

    public override AudioClip TryTrigger()
    {
        ResetPositions();
        if(base.TryTrigger() != null)
        {
            RandomOffsets();

            return clip;
        }
        return null;
    }
}
