using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PhaseHelper
{
    public float entranceDuration;
    public float leaveDuration;
    public Vector2 waitRange;
    public bool isEntering { get; private set; }
    public Character currentCharacter;

    public delegate void BasicEvent();
    public BasicEvent onEntranceEnd;
    public delegate void BoolEvent(bool val1);
    public BoolEvent onPhaseEnd;

    //New character entering the forge
    public void Enter(Character character)
    {
        currentCharacter = character;
        CharacterManager.instance.UpdateActorProfile(character);
        //Here trigger animations and stuff
        CharacterManager.instance.characterActor.EnterForge(entranceDuration);
    }
    public void PhaseEnd()
    {
        onPhaseEnd?.Invoke(currentCharacter == null);
        CharacterManager.instance.characterActor.LeaveForge(leaveDuration);
    }

    public void EntranceEnd()
    {
        onEntranceEnd?.Invoke();
    }

    //New blank phase, nobody's here, returns a duration included inside waitRange
    public float BlankPhase()
    {
        currentCharacter = null;
        //Add logic here, trigger events ?
        return Random.Range(waitRange.x, waitRange.y);
    }
}
