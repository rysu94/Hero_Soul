using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWipe : MonoBehaviour
{
    public GameObject startWipe;

	// Use this for initialization
	void Start ()
    {
		if(LevelCreator.startWipe)
        {
            StartCoroutine(StartRoutine());
            
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator StartRoutine()
    {
        startWipe.SetActive(true);
        TestCharController.inDialogue = true;
        yield return new WaitForSeconds(2f);
        TestCharController.inDialogue = false;
        startWipe.SetActive(false);
        LevelCreator.startWipe = false;
    }
}
