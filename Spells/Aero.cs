using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aero : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(AeroRoutine());
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator AeroRoutine()
    {
        yield return new WaitForSeconds(.5f);
        GameObject.Find("States").GetComponent<StateManager>().AddState(30, 6, 1, false);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
