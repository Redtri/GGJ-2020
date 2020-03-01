using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class EffectManager : MonoBehaviour
{

    public static EffectManager instance;

    public ScreenShake screenShake;

    public Volume postProcessVolume;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        screenShake.Init();
    }

    // Update is called once per frame
    void Update()
    {
        screenShake.Update(Time.time);
    }

    public void Vign(float duration, float intensity)
    {
        Vignette vignette = null;
        postProcessVolume.profile.TryGet<Vignette>(out vignette);

        DOVirtual.Float(0, intensity, duration/2, (float x) => vignette.intensity.value = x).SetLoops(2, LoopType.Yoyo);
    }
}
