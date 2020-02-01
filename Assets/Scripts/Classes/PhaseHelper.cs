using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PhaseHelper
{
    public float entranceDuration;
    public Vector2 waitRange;
    public bool isEntering { get; private set; }

    //New character entering the forge
    public void Enter(Character character)
    {
        CharacterManager.instance.UpdateActorProfile(character);
        //Here trigger animations and stuff
        CharacterManager.instance.characterActor.EnterForge(entranceDuration);
    }

    //New blank phase, nobody's here, returns a duration included inside waitRange
    public float BlankPhase()
    {
        //Add logic here, trigger events ?
        return Random.Range(waitRange.x, waitRange.y);
    }
}
