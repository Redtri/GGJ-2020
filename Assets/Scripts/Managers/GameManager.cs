using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-2000)]
public class GameManager : MonoBehaviour
{
	
    public static GameManager instance;

    public float winPercentAlive;
    public float winLoseAlive;
    public int nbCharCheck;
    public PhaseHelper phaseHelper;
    public PlayerHelper playerHelper;
	public AnimationCurve deathCurve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 0));

    public bool gameOver {get; private set;}
    public int nbDead { get; private set; }
    public bool winning { get; private set; }

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
        if(Input.anyKeyDown && CharacterManager.instance.endCharArrived) {
            SceneManager.LoadSceneAsync(0);
        }
    }


    //Phase Handling functions
    public void StartPhase()
    {
        if (!gameOver) {
            if (CharacterManager.instance.charactersInQueue[0].doesExist) {
                StartCoroutine(CharacterEntrance(CharacterManager.instance.charactersInQueue[0]));
            }
            else {
                StartCoroutine(VoidPhase());
            }
        } else {
            Debug.Log("Ending character entering");
            CharacterManager.instance.endCharacter.privateText = true;
            CharacterManager.instance.endCharacter.forcedText = (winning) ? "The allies came out victorious! You greatly contributed to the war effort. We would never have won without your excellent services." : "Your efforts were not enough... We lost. If you choose to fight again, pay close attention to what the soldiers need to adapt their equipment!";
            StartCoroutine(CharacterEntrance(CharacterManager.instance.endCharacter));
        }
    }

    public void EndPhase(bool force = false)
    {
        bool test = false;

        test = force ? true : !GameObject.FindGameObjectWithTag("ValidateButton").GetComponent<UIButton>().lockButton;

        if(test)
        {
            //If there was someone in the room, The coroutine for the leaving is called$
            if (phaseHelper.PhaseEnd())
            {
                if (!gameOver)
                {
                    EffectManager.instance.screenShake.Shake(0, 0.1f);
                    StartCoroutine(CharacterLeaving());
                }//Otherwise, just start another phase
            }
            else
            {
                StartPhase();
            }
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
        EndPhase(true);
    }

    private IEnumerator CharacterLeaving()
    {
        yield return new WaitForSeconds(phaseHelper.leaveDuration);

       // float proba = phaseHelper.currentCharacter.Battle();
        float random = Random.Range(0f, 1f);

		float eval = phaseHelper.currentCharacter.Battle();
		float proba = deathCurve.Evaluate(eval);
		float rand = Random.Range(3, 20);
		if (random > proba) // Char win
        {
            CharacterManager.instance.charactersAlive.Add(phaseHelper.currentCharacter);

            //Weapons are broken
            phaseHelper.currentCharacter.gearValue[0] = Random.Range(0, phaseHelper.currentCharacter.gearValue[0]);
            phaseHelper.currentCharacter.gearValue[1] = Random.Range(0, phaseHelper.currentCharacter.gearValue[1]);
            phaseHelper.currentCharacter.gearValue[2] = Random.Range(0, phaseHelper.currentCharacter.gearValue[2]);

			UIChatlog.AddLogMessage(phaseHelper.currentCharacter.GetVictoryLog(), rand, UIChatlog.TyopeOfLog.Good);
			
			//Debug.Log("Vivant");
		}
        else // Char Loose
        {
            ++nbDead;

			//Debug.Log("Mort");
			//	UIChatlog.AddLogMessage(phaseHelper.currentCharacter.GetDeathLog(),Random.Range(3,20));
			UIChatlog.AddLogMessage(phaseHelper.currentCharacter.GetDeathLog(), rand, UIChatlog.TyopeOfLog.Bad);

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
        CheckWinLose();
        phaseHelper.LeavingEnd();
        CheckWinLose();
		if (theWinRatio > 0.5f)
		{
			UIChatlog.AddLogMessage((int)(theWinRatio * 100) + "% chances to win the war", rand + 2, UIChatlog.TyopeOfLog.Good);
		}
		else
		{
			UIChatlog.AddLogMessage((int)(theWinRatio * 100) + "% chances to win the war", rand + 2, UIChatlog.TyopeOfLog.Bad);
		}
	}
	private float theWinRatio;
    private void CheckWinLose()
    {
        int totCharacter = nbDead + CharacterManager.instance.charactersAlive.Count;

        //float winRatio = (float)CharacterManager.instance.charactersAlive.Count / (float)totCharacter;
        //float loseRatio = ((float)nbDead / (float)totCharacter);
		float winRatio = 0;
		float heroCount = 0;
		foreach(Character c in CharacterManager.instance.charactersAlive)
		{
			winRatio += c.hero;
			heroCount += c.hero;
		}
		heroCount += nbDead;
		winRatio = winRatio/heroCount;
		float loseRatio = 1 - winRatio;

		theWinRatio = winRatio;
		
		

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
                gameOver = true;
                Debug.Log("WIN!");
            }
            else if( winRatio <= winLoseAlive) {
                gameOver = true;
                Debug.Log("LOSE");
            }
        }
    }
}
