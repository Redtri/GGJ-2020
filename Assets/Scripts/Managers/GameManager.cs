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

    private void Update()
    {
        //TEST
        if (Input.GetKeyDown(KeyCode.Space)) {
            List<Character> list = new List<Character>();
            list.Add(CharacterManager.instance.charactersInQueue[0]);
            StartPhase(list);
        }
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
    //DEBUG
    public void StartPhase(List<Character> list)
    {
        if (list[0] != null) {
            StartCoroutine(CharacterEntrance(list[0]));
        } else {
            StartCoroutine(VoidPhase());
        }
    }

    public void EndPhase()
    {
        phaseHelper.PhaseEnd();
    }

    private IEnumerator CharacterEntrance(Character enteringChar)
    {
        Debug.Log("Character entering the forge");
        phaseHelper.Enter(enteringChar);

        yield return new WaitForSeconds(phaseHelper.entranceDuration);
        Debug.Log("Character entered the forge");
        phaseHelper.EntranceEnd();
    }

    private IEnumerator VoidPhase()
    {
        Debug.Log("Nobody's here");
        yield return new WaitForSeconds(phaseHelper.BlankPhase());
        Debug.Log("Time has passed...");
        phaseHelper.PhaseEnd();
    }
}
