using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bless : StateController
{

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(StateDecayRoutine());
        StartCoroutine(BlessTickRoutine());
    }

    IEnumerator BlessTickRoutine()
    {
        TestCharController.breakInvuln = true;
        while (stateTime > 0)
        {
            //Check if the object is paused
            while (GameController.paused)
            {
                yield return null;
            }

            yield return new WaitForSeconds(1f);
        }
        TestCharController.breakInvuln = false;
    }


}
