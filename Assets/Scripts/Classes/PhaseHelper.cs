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
	public bool isWaitPhase { get; private set; }
	public Character currentCharacter;
    public Character previousCharacter;

    public delegate void BasicEvent();
    public BasicEvent onEntranceEnd;
    public BasicEvent onEntrance;
    public BasicEvent onLeaving;
    public BasicEvent onLeavingEnd;
	public BasicEvent onWaitStart;
	public BasicEvent onWaitEnd;
    public delegate void BoolEvent(bool val1);
    public BoolEvent onPhaseEnd;

    //Sound
    public int soldiersInc;

	

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

        //Sound
        AudioManager.instance.DoorOpen.Post(GameManager.instance.gameObject);

        soldiersInc++;
        soldiersInc = Mathf.Clamp(soldiersInc, 0, 8);
        switch (soldiersInc)
        {
            case 0:
                AudioManager.instance.SetIntensityCalm();
                break;
            case 1:
                AudioManager.instance.SetIntensityLow();
                break;
            case 2:
                AudioManager.instance.SetIntensityLow();
                break;
            case 3:
                AudioManager.instance.SetIntensityMedium();
                break;
            case 4:
                AudioManager.instance.SetIntensityMedium();
                break;
            case 5:
                AudioManager.instance.SetIntensityHigh();
                break;
            case 6:
                AudioManager.instance.SetIntensityHigh();
                break;
            case 7:
                AudioManager.instance.SetIntensityExtreme();
                break;
            case 8:
                AudioManager.instance.SetIntensityExtreme();
                break;
        }

        onEntrance?.Invoke();
    }

    //Phase is over, returns whether there was anybody in the forge
    public void PhaseEnd()
    {
		// onPhaseEnd?.Invoke(currentCharacter.doesExist);
		onPhaseEnd?.Invoke(true);
		CharacterManager.instance.characterActor.LeaveForge(leaveDuration);

        currentCharacter.privateText = false;
        CharacterManager.instance.charactersInQueue.Remove(CharacterManager.instance.charactersInQueue[0]);
        CharacterManager.instance.AddCharacterToQueue();

      //  return currentCharacter.doesExist;
    }

    public void EntranceEnd()
    {
        onEntranceEnd?.Invoke();
        if (GameManager.instance.gameOver) {
            CharacterManager.instance.endCharArrived = true;
        }

        //Sound
        AudioManager.instance.DialEvent.Post(GameManager.instance.gameObject);
    }

    //Character has left
    public void LeavingEnd()
    {
        onLeavingEnd?.Invoke();
        GameManager.instance.StartPhase();

        //Sound
        AudioManager.instance.DoorOpen.Post(GameManager.instance.gameObject);
    }

	//No character phase
	public void StartWait()
	{
		isWaitPhase = true;
		onWaitStart?.Invoke();
	}

	//No character phase end
	public void EndWait()
	{
		isWaitPhase = false;
		onWaitEnd?.Invoke();
	}
	//return the duration of the no character phase
	public float GetWaitDuration()
	{
		return Random.Range(waitRange.x, waitRange.y);
	}
}
