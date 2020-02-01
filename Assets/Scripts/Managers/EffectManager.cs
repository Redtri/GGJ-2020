using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public ScreenShake screenShake;

    // Start is called before the first frame update
    void Start()
    {
        screenShake.Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            screenShake.Shake(Time.time);
        }
        screenShake.Update(Time.time);
    }
}
