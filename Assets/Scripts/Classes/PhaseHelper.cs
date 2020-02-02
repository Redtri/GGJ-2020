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
    public Character previousCharacter;

    public delegate void BasicEvent();
    public BasicEvent onEntranceEnd;
    public BasicEvent onLeaving;
    public BasicEvent onLeavingEnd;
    public delegate void BoolEvent(bool val1);
    public BoolEvent onPhaseEnd;

    //New character entering the forge
    public void Enter(Character character, bool end = false)
    {
        if (!end) {
            CharacterManager.instance.charactersInQueue.Remove(character);
            CharacterManager.instance.AddCharacterToQueue();

            //TODO : TEST FIX BUG
            currentCharacter = character;

            //Here trigger animations and stuff
        }
        CharacterManager.instance.UpdateActorProfile(character);
        CharacterManager.instance.characterActor.EnterForge(entranceDuration);
    }

    //Phase is over, returns whether there was anybody in the forge
    public bool PhaseEnd()
    {
        onPhaseEnd?.Invoke(currentCharacter.doesExist);
        CharacterManager.instance.characterActor.LeaveForge(leaveDuration);

        if (!currentCharacter.doesExist) {
            CharacterManager.instance.charactersInQueue.Remove(CharacterManager.instance.charactersInQueue[0]);
            CharacterManager.instance.AddCharacterToQueue();
        }

        return currentCharacter.doesExist;
    }

    public void EntranceEnd()
    {
        onEntranceEnd?.Invoke();
        if (GameManager.instance.gameOver) {
            CharacterManager.instance.endCharArrived = true;
        }
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
