using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dazed : StateController
{
    public GameObject dazedPrefab;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(StateDecayRoutine());
        StartCoroutine(DazeTickRoutine());
    }

    IEnumerator DazeTickRoutine()
    {
        GameObject tempObj = Instantiate(dazedPrefab, GameObject.Find("Player_States_Panel").transform);
        TestCharController.inDialogue = true;
        while (stateTime > 0)
        {
            //Check if the object is paused
            while (GameController.paused)
            {
                yield return null;
            }
            TestCharController.inDialogue = true;
            yield return new WaitForSeconds(1f);
        }
        TestCharController.inDialogue = false;
        Destroy(tempObj);

    }

}
