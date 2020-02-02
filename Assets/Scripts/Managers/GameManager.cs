﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-2000)]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float winPercentAlive;
    public float winLoseAlive;
    public int nbCharCheck;
    public PhaseHelper phaseHelper;
    public PlayerHelper playerHelper;

    public int nbDead { get; private set; }

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
        if (CharacterManager.instance.charactersInQueue[0].doesExist) {
            StartCoroutine(CharacterEntrance(CharacterManager.instance.charactersInQueue[0]));
        } else {
            StartCoroutine(VoidPhase());
        }
    }

    public void EndPhase()
    {
        //If there was someone in the room, The coroutine for the leaving is called$
        if (phaseHelper.PhaseEnd()) {
            StartCoroutine(CharacterLeaving());
        }//Otherwise, just start another phase
        else {
            StartPhase();
        }
    }

    private IEnumerator CharacterEntrance(Character enteringChar)
    {
        yield return new WaitForEndOfFrame();

        //  Debug.Log(enteringChar.c_Name + " entering the forge");
        phaseHelper.Enter(enteringChar);

        yield return new WaitForSeconds(phaseHelper.entranceDuration);
       // Debug.Log(enteringChar.c_Name + " entered the forge");
        phaseHelper.EntranceEnd();
    }

    private IEnumerator VoidPhase()
    {
        Debug.Log("Nobody's here");
        yield return new WaitForSeconds(phaseHelper.BlankPhase());
        Debug.Log("Time has passed...");
        EndPhase();
    }

    private IEnumerator CharacterLeaving()
    {
        yield return new WaitForSeconds(phaseHelper.leaveDuration);        

        float proba = phaseHelper.currentCharacter.Battle();
        float random = Random.Range(0f, 1f);

        
        if(proba > random) // Char win
        {
            CharacterManager.instance.charactersAlive.Add(phaseHelper.currentCharacter);

            //Weapons are broken
            phaseHelper.currentCharacter.gearValue[0] = Random.Range(0, phaseHelper.currentCharacter.gearValue[0]);
            phaseHelper.currentCharacter.gearValue[1] = Random.Range(0, phaseHelper.currentCharacter.gearValue[1]);
            phaseHelper.currentCharacter.gearValue[2] = Random.Range(0, phaseHelper.currentCharacter.gearValue[2]);

            //Debug.Log("Vivant");
        }
        else // Char Loose
        {
            ++nbDead;

            //Debug.Log("Mort");
		//	UIChatlog.AddLogMessage(phaseHelper.currentCharacter.GetDeathLog(),Random.Range(3,20));

			if (CharacterManager.instance.charactersAlive.Contains(phaseHelper.currentCharacter))
            {
                CharacterManager.instance.charactersAlive.Remove(phaseHelper.currentCharacter);
            }

            if (CharacterManager.instance.charactersInQueue.Contains(phaseHelper.currentCharacter))
            {
               CharacterManager.instance.charactersInQueue.Remove(phaseHelper.currentCharacter);
               CharacterManager.instance.AddCharacterToQueue();
            }
        }
        phaseHelper.LeavingEnd();
        CheckWinLose();
    }

    private void CheckWinLose()
    {
        int totCharacter = nbDead + CharacterManager.instance.charactersAlive.Count;
        Debug.Log("Total nb characters " + totCharacter);
        if(totCharacter >= nbCharCheck) {
            Debug.Log(((float)CharacterManager.instance.charactersAlive.Count / (float)totCharacter) + " alive " + ((float)nbDead / (float)totCharacter) + " dead");
            if ((float)CharacterManager.instance.charactersAlive.Count / (float)totCharacter > winPercentAlive) {
                Debug.Log("WIN!");
            }
            else if(((float)nbDead / (float)totCharacter) >= winLoseAlive) {
                Debug.Log("LOSE");
            }
        }
    }
}
