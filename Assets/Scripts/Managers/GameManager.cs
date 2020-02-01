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
            list.Add(new Character());
            StartPhase(list);
        }
    }

    //Phase Handling functions
    public void StartPhase(List<Character> queue)
    {
        if (queue[0] != null) {
            StartCoroutine(CharacterEntrance(queue[0]));
        } else {
            StartCoroutine(VoidPhase());
        }
    }

    private IEnumerator CharacterEntrance(Character enteringChar)
    {
        Debug.Log("Character entering the forge");
        phaseHelper.Enter(enteringChar);
        yield return new WaitForSeconds(phaseHelper.entranceDuration);
        Debug.Log("Character entered the forge");
    }

    private IEnumerator VoidPhase()
    {
        Debug.Log("Nobody's here");
        yield return new WaitForSeconds(phaseHelper.BlankPhase());
    }
}
