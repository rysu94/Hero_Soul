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
        tempObj = Instantiate(dazedPrefab, GameObject.Find("Player_States_Panel").transform);
        TestCharController.inDialogue = true;


        if(TestCharController.player.GetComponent<TestCharController>().south)
            TestCharController.player.GetComponent<Animator>().Play("Weak_Down");
        if (TestCharController.player.GetComponent<TestCharController>().north)
            TestCharController.player.GetComponent<Animator>().Play("Weak_Up");
        if (TestCharController.player.GetComponent<TestCharController>().west)
            TestCharController.player.GetComponent<Animator>().Play("Weak_Left");
        if (TestCharController.player.GetComponent<TestCharController>().east)
            TestCharController.player.GetComponent<Animator>().Play("Weak_Right");


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
