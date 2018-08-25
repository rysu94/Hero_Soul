using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Anim : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(ChangeSpeed());

	}
    IEnumerator ChangeSpeed()
    {
        GetComponent<Animator>().speed = Random.Range(0, 2000);
        yield return new WaitForSeconds(.1f);
        GetComponent<Animator>().speed = 1;
    }
	
}
