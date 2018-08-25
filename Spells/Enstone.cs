using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enstone : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(EnstoneRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator EnstoneRoutine()
    {
        yield return new WaitForSeconds(.5f);
        GameObject.Find("States").GetComponent<StateManager>().AddState(30, 10, 1, true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}

