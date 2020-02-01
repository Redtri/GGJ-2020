using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PhaseHelper phaseHelper;
    public PlayerHelper playerHelper;

    private void Awake()
    {
        if (!instance) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartPhase();
    }

    private void Update()
    {
    }

    //Phase Handling functions
    public void StartPhase()
    {
        if (CharacterManager.instance.charactersInQueue[0] != null) {
            StartCoroutine(CharacterEntrance(CharacterManager.instance.charactersInQueue[0]));
        } else {
            StartCoroutine(VoidPhase());
        }
    }

    public void EndPhase()
    {
        //If there was someone in the room, The coroutine for the leaving is called
        if (!phaseHelper.PhaseEnd()) {
            StartCoroutine(CharacterLeaving());
        }//Otherwise, just start another phase
        else {
            StartPhase();
        }
    }

    private IEnumerator CharacterEntrance(Character enteringChar)
    {
      //  Debug.Log(enteringChar.c_Name + " entering the forge");
        phaseHelper.Enter(enteringChar);

        yield return new WaitForSeconds(phaseHelper.entranceDuration);
       // Debug.Log(enteringChar.c_Name + " entered the forge");
        phaseHelper.EntranceEnd();
    }

    private IEnumerator VoidPhase()
    {
       // Debug.Log("Nobody's here");
        yield return new WaitForSeconds(phaseHelper.BlankPhase());
        //Debug.Log("Time has passed...");
        phaseHelper.PhaseEnd();
    }

    private IEnumerator CharacterLeaving()
    {
        yield return new WaitForSeconds(phaseHelper.leaveDuration);      
        Debug.Log(phaseHelper.currentCharacter.Battle());

        phaseHelper.LeavingEnd();

    }
}
