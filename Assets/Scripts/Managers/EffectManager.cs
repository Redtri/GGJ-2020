using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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
        if (Input.GetKeyDown(KeyCode.Space)) {
            screenShake.Shake(0.0f, 1.0f);
        }
        screenShake.Update(Time.time);
    }
}
