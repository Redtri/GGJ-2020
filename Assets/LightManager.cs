using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{


    public Sprite ligth_on;
    public Sprite ligth_off;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.phaseHelper.onEntrance += DisplayLigth;
        GameManager.instance.phaseHelper.onEntranceEnd += HideLigth;
        GameManager.instance.phaseHelper.onLeaving += DisplayLigth;
    }

    public void DisplayLigth()
    {
        GetComponent<SpriteRenderer>().sprite = ligth_on;
    }

    public void HideLigth()
    {
        GetComponent<SpriteRenderer>().sprite = ligth_off;
    }
}
