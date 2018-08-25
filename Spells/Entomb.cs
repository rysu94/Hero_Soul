using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entomb : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(EntombRoutine());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator EntombRoutine()
    {
        GetComponentInParent<Monster>().frozen = true;
        yield return new WaitForSeconds(10f);
        GetComponentInParent<Monster>().frozen = false;
    }
}
