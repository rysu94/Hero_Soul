using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Rebounding : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator Rebounding()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);

        }

    }
}
