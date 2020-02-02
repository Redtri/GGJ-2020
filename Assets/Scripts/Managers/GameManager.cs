using System.Collections;
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
    public bool winning;

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
       // StartPhase();
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

            EffectManager.instance.screenShake.Shake(0, 0.1f);

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

			UIChatlog.AddLogMessage(phaseHelper.currentCharacter.GetVictoryLog(), Random.Range(3, 20), UIChatlog.TyopeOfLog.Good);
			//Debug.Log("Vivant");
		}
        else // Char Loose
        {
            ++nbDead;

			//Debug.Log("Mort");
			//	UIChatlog.AddLogMessage(phaseHelper.currentCharacter.GetDeathLog(),Random.Range(3,20));
			UIChatlog.AddLogMessage(phaseHelper.currentCharacter.GetDeathLog(), Random.Range(3, 20), UIChatlog.TyopeOfLog.Bad);

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

        //float winRatio = (float)CharacterManager.instance.charactersAlive.Count / (float)totCharacter;
        //float loseRatio = ((float)nbDead / (float)totCharacter);
		float winRatio = 0;
		foreach(Character c in CharacterManager.instance.charactersAlive)
		{
			winRatio += c.hero;
		}
		winRatio /= totCharacter;
		float loseRatio = 1 - winRatio;
		

        if(winRatio > 0.5f) {
            winning = true;
            //TODO : call wwise events for winning state
        } else {
            winning = false;
            //TODO : call wwise events for winning state
        }
        Debug.Log(winRatio + " alive " + loseRatio + " dead. Winning : " + winning);

        Debug.Log("Total nb characters " + totCharacter);
        if(totCharacter >= nbCharCheck) {
            if ( winRatio > winPercentAlive) {
                Debug.Log("WIN!");
            }
            else if( winRatio <= winLoseAlive) {
                Debug.Log("LOSE");
            }
        }
    }
}
