using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bless_Cast : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(BlessRoutine());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator BlessRoutine()
    {
        yield return new WaitForSeconds(.5f);
        GameObject.Find("States").GetComponent<StateManager>().AddState(5, 12, 1, true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
