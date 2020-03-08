using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using Sweet.UI;
[DefaultExecutionOrder(-2000)]
public class GameManager : MonoBehaviour
{
	
    public static GameManager instance;

    public float winPercentAlive;
    public float winLoseAlive;
    public int nbCharCheck;
    [HideInInspector] public PhaseHelper phaseHelper;
    [HideInInspector] public PlayerHelper playerHelper;
	public AnimationCurve deathCurve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 0));

    public UIIngot ingots;

    [Header("SYSTEM")]
    public List<OutsideEvent> outsideEvents;
    public List<ParticleEvent> particleEvents;
    public float pauseTransition;
    public bool gameOver {get; private set;}
    public int nbDead { get; private set; }
    public bool winning { get; private set; }
    public bool screenShakeEnabled{get; private set;}
    public float volumeSFX {get; private set;}
    public float volumeMusic {get; private set;}
	private int phaseCount = 0;
    private bool gameStarted = false;
    private bool gamePaused = false;

    public event BasicEvent onGameEnd;

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
        UI_Manager.instance.PauseScreen(false);
        Init();
    }

    private void Update()
    {
        HandlingGameState();
    }

    private void Init()
    {
        CharacterManager.instance.Init();
        screenShakeEnabled = true;
        nbDead = 0;
        winning = false;
        phaseCount = 0;
        gameStarted = false;
        gameOver = false;
        theWinRatio = 0f;
    }

    private void EndGame()
    {
        onGameEnd?.Invoke();
        Init();
    }

    private void TriggerEvents(bool waitPhase)
    {
        Debug.Log("Starting coroutines for outside events");
        for(int i = 0; i < outsideEvents.Count; ++i){
            if(outsideEvents[i].isUp && (!outsideEvents[i].onlyTriggerInWaitPhase || waitPhase && outsideEvents[i].onlyTriggerInWaitPhase)){
                StartCoroutine(OutsideEventCoroutine(outsideEvents[i]));
            }
        }
        for(int i = 0; i < particleEvents.Count; ++i){
            //Events need to be initialized once, and this function is called at the start of the game for the very first time 
            if(!waitPhase){
                particleEvents[i].Init();
            }
            if(particleEvents[i].isUp && (!particleEvents[i].onlyTriggerInWaitPhase || waitPhase && particleEvents[i].onlyTriggerInWaitPhase)){
                StartCoroutine(OutsideEventCoroutine(particleEvents[i]));
            }
        }
    }

    private void HandlingGameState()
    {
        if(!gamePaused){
            if(Input.anyKeyDown){
                //If the game started, we only check if the ending character has arrived
                if(gameStarted){
                    if(CharacterManager.instance.endCharArrived)
                        UIMainScreen.instance.Fade(true).AppendCallback(() => EndGame()); //TODO : Here, call the reset function
                    else{
                        if(Input.GetKeyDown(KeyCode.Escape)){
                            Pause();
                        }
                    }
                //Otherwise, we are at the beginning of the game
                }else{
                    gameStarted = true;
                    if(!Input.GetKeyDown(KeyCode.Mouse0)){
                        AudioManager.instance.Validate.Post(GameManager.instance.gameObject);
                    }
                    UIMainScreen.instance.Fade(false).AppendCallback(() => StartPhase());
                    UIMainScreen.instance.Fade(false).AppendCallback(() => TriggerEvents(false));
                }
            }
        }else{
            if(Input.GetKeyDown(KeyCode.Escape)){
                Pause();
            }
        }
    }

    //Function called by code
    public void Pause()
    {
        gamePaused = !gamePaused;
        StartCoroutine(PausingGame());
    }

    //Function called by button
    public void OverridePause(bool resume)
    {
        if(resume == gamePaused){
            gamePaused = !gamePaused;
            StartCoroutine(PausingGame());
        }
    }

    private IEnumerator PausingGame()
    {
        //TODO : Here, handle pause Sound events
        float startTime = Time.unscaledTime;

        while(Time.unscaledTime - startTime < pauseTransition + 0.01f){
            //Just clamping timescale value
            float newTimeScale = ((gamePaused) ? -Time.unscaledDeltaTime : Time.unscaledDeltaTime); 
            Time.timeScale = Mathf.Clamp(Time.timeScale + newTimeScale, 0, 1f);
            //Updating the pause screen fade status in the UI manager
            UI_Manager.instance.FadePauseScreen(gamePaused, (Time.unscaledTime - startTime)/pauseTransition);

            yield return new WaitForEndOfFrame();
        }
        Time.timeScale = (gamePaused) ? 0f : 1f;

        yield return null;
    }

	//Phase Handling functions
	public void StartPhase(bool canWait = true)
    {
        if (!gameOver) {
			if (canWait && CharacterManager.instance.WillWait() && phaseCount > CharacterManager.instance.nbrFirstCharInForge)
			{
				StartCoroutine(WaitPhase());
			}else
			{
				StartCoroutine(CharacterEntrance(CharacterManager.instance.charactersInQueue[0]));
			}
			phaseCount++;
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
			phaseHelper.PhaseEnd();
			if (!gameOver)
			{
				EffectManager.instance.screenShake.Shake(0.1f);
				StartCoroutine(CharacterLeaving());
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

	private IEnumerator WaitPhase()
	{
		phaseHelper.StartWait();
        TriggerEvents(true);
		yield return new WaitForSeconds(phaseHelper.GetWaitDuration());
		phaseHelper.EndWait();
		StartPhase(false);
	}

    private IEnumerator OutsideEventCoroutine(OutsideEvent outEvent)
    {
        if((outEvent.onlyTriggerInWaitPhase && phaseHelper.isWaitPhase) || !outEvent.onlyTriggerInWaitPhase){
            if(outEvent.isUp){
                Debug.Log("Sound should be played");
                //outEvent.TryTrigger()?.Post(gameObject);
                AudioSource src = GetComponent<AudioSource>(); 
                AudioClip clip =  outEvent.TryTrigger();
                if(clip){
                    src.PlayOneShot(clip);
                    if(outEvent.screenShakeAmount != 0.0f)
                        EffectManager.instance.screenShake.Shake(1f, outEvent.screenShakeAmount);
                    if(outEvent.vignetteAmount != 0.0f)
                        EffectManager.instance.Vign(1f, outEvent.vignetteAmount);
                }

                yield return new WaitForSeconds(outEvent.currentCooldown);
                StartCoroutine(OutsideEventCoroutine(outEvent));
            }
        }
        yield return null;
    }

    private IEnumerator CharacterLeaving()
    {
		phaseHelper.StartLeave();
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

			UILog.AddLogMessage(phaseHelper.currentCharacter.GetVictoryLog(), rand, UILog.LogType.Positive);
            StartCoroutine(LivingSoundPosting(rand));

            //Debug.Log("Vivant");
        }
        else // Char Loose
        {
            ++nbDead;

            //Sound
            //AudioManager.instance.DeathEvent.Post(GameManager.instance.gameObject);

            //Debug.Log("Mort");
		//	UIChatlog.AddLogMessage(phaseHelper.currentCharacter.GetDeathLog(),Random.Range(3,20));
			//Debug.Log("Mort");
			//	UIChatlog.AddLogMessage(phaseHelper.currentCharacter.GetDeathLog(),Random.Range(3,20));
			UILog.AddLogMessage(phaseHelper.currentCharacter.GetDeathLog(), rand, UILog.LogType.Negative);
            StartCoroutine(DeathSoundPosting(rand));

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
			UILog.AddLogMessage((int)(theWinRatio * 100) + "% chances to win the war", rand + 2, UILog.LogType.Positive);
		}
		else
		{
			UILog.AddLogMessage((int)(theWinRatio * 100) + "% chances to win the war", rand + 2, UILog.LogType.Negative);
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
            AudioManager.instance.SetWinning.Post(gameObject);
        } else {
            winning = false;
            //TODO : call wwise events for winning state
            AudioManager.instance.SetLoosing.Post(gameObject);
        }
        Debug.Log(winRatio + " alive " + loseRatio + " dead. Winning : " + winning);

        Debug.Log("Total nb characters " + totCharacter);
        if(totCharacter >= nbCharCheck) {
            if ( winRatio > winPercentAlive) {
                gameOver = true;
                Debug.Log("WIN!");
                //Sound
                AudioManager.instance.MusicWin.Post(gameObject);
                AudioManager.instance.SetIntensityCalm();
            }
            else if( winRatio <= winLoseAlive) {
                gameOver = true;
                Debug.Log("LOSE");
                //Sound
                AudioManager.instance.MusicLoose.Post(gameObject);
                AudioManager.instance.SetIntensityCalm();
            }
        }
    }

    public void SetMusicVolume(float value)
    {
        volumeMusic = value * 100f;
        //TODO : Set Wwise volume
    }

    public void SetSFXVolume(float value)
    {
        volumeSFX = value * 100f;
        //TODO : Set Wwise volume
    }

    public void EnableScreenShake(bool value)
    {
        screenShakeEnabled = value;
    }

    IEnumerator DeathSoundPosting(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        AudioManager.instance.DeathEvent.Post(GameManager.instance.gameObject);
        Debug.Log("Dead sound");
    }

    IEnumerator LivingSoundPosting(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        AudioManager.instance.LivingEvent.Post(GameManager.instance.gameObject);
        Debug.Log("Living sound");
    }
}
