using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AK.Wwise.Event BattlefieldAmbiance;
    public AK.Wwise.Event ForgeFire;
    public AK.Wwise.Event IntensityCalm;
    public AK.Wwise.Event IntensityLow;
    public AK.Wwise.Event IntensityMedium;
    public AK.Wwise.Event IntensityHigh;
    public AK.Wwise.Event IntensityExtreme;
    public AK.Wwise.Event SetWinning;
    public AK.Wwise.Event SetLoosing;
    public AK.Wwise.Event DoorOpen;
    public AK.Wwise.Event AddItem;
    public AK.Wwise.Event RemoveItem;
    public AK.Wwise.Event Validate;
    public AK.Wwise.Event DialEvent;
    public AK.Wwise.Event DeathEvent;
    public AK.Wwise.Event HammerHit;


    public static AudioManager instance;

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

    public void SetIntensityCalm()
    {
        IntensityCalm.Post(gameObject);
    }

    public void SetIntensityLow()
    {
        IntensityLow.Post(gameObject);
    }

    public void SetIntensityMedium()
    {
        IntensityMedium.Post(gameObject);
    }

    public void SetIntensityHigh()
    {
        IntensityHigh.Post(gameObject);
    }

    public void SetIntensityExtreme()
    {
        IntensityExtreme.Post(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        BattlefieldAmbiance.Post(gameObject);
        ForgeFire.Post(gameObject);
    }
}
