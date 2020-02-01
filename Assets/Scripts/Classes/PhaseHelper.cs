﻿using System.Collections;
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
    public Character previousCharacter;

    public delegate void BasicEvent();
    public BasicEvent onEntranceEnd;
    public BasicEvent onLeavingEnd;
    public delegate void BoolEvent(bool val1);
    public BoolEvent onPhaseEnd;

    //New character entering the forge
    public void Enter(Character character)
    {
        CharacterManager.instance.charactersInQueue.Remove(character);
        CharacterManager.instance.AddCharacterToQueue();
        
        //TODO : TEST FIX BUG
        currentCharacter = character;

        CharacterManager.instance.UpdateActorProfile(character);
        //Here trigger animations and stuff
        CharacterManager.instance.characterActor.EnterForge(entranceDuration);
    }
    //Phase is over, returns whether there was anybody in the forge
    public bool PhaseEnd()
    {
        onPhaseEnd?.Invoke(currentCharacter == null);
        CharacterManager.instance.characterActor.LeaveForge(leaveDuration);

        return currentCharacter == null;
    }

    public void EntranceEnd()
    {
        onEntranceEnd?.Invoke();
    }

    //Character has left
    public void LeavingEnd()
    {
        onLeavingEnd?.Invoke();
        GameManager.instance.StartPhase();
    }

    //New blank phase, nobody's here, returns a duration included inside waitRange
    public float BlankPhase()
    {
        currentCharacter = null;
        //Add logic here, trigger events ?
        return Random.Range(waitRange.x, waitRange.y);
    }
}
