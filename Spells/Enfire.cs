using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enfire : MonoBehaviour
{
    

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(EnfireRoutine());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator EnfireRoutine()
    {
        yield return new WaitForSeconds(.5f);
        GameObject.Find("States").GetComponent<StateManager>().AddState(30, 8, 1, true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
