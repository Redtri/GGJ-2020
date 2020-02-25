using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScreenShake
{
    public Transform cam;
    private Vector3 startPos;
    public float shakeDuration;
    private float shakeStartTime;
    public float intensity;
    private bool shaking;

    public void Init()
    {
        startPos = cam.position;
        shakeStartTime = Time.time + shakeDuration;
    }

    public void Update(float time)
    {
        if (shaking) {
            if (time - shakeStartTime < shakeDuration) {
                cam.position = new Vector3(startPos.x + Random.insideUnitCircle.x * intensity, startPos.y + Random.insideUnitCircle.y * intensity, startPos.z);
            } else {
                cam.position = startPos;
                shaking = false;
            }
        }
    }

    public void Shake(float shakeDura)
    {
        if(GameManager.instance.screenShakeEnabled){
            shaking = true;
            shakeDuration = shakeDura;
            shakeStartTime = Time.time;
        }
    }
}
