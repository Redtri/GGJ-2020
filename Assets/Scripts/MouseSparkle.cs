using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MouseSparkle : MonoBehaviour
{
    private  VisualEffect vfx;
    private Camera cam;

    public static MouseSparkle instance;

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


    private void Start()
    {
        vfx = GetComponent<VisualEffect>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            var point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

            transform.position = point;            

            vfx.SendEvent("OnBurst");

            //Sound
            AudioManager.instance.HammerHit.Post(GameManager.instance.gameObject);
        }
    }

    public void SetSparkleSize(float sizeMultiply)
    {
        vfx.SetFloat("MultiplySize", sizeMultiply);
    }

    public void SetBurstAmount(int amount)
    {
        vfx.SetInt("BurstAmount", amount);
    }
}
