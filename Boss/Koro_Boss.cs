using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koro_Boss : MonoBehaviour
{
    

	// Use this for initialization
	void Start ()
    {





    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator BossStart()
    {
        yield return new WaitForSeconds(3f);
    }
}
