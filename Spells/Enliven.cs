using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enliven : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(EnlivenRoutine());
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator EnlivenRoutine()
    {
        yield return new WaitForSeconds(.5f);
        GameObject.Find("States").GetComponent<StateManager>().AddState(30, 9, 1, true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
