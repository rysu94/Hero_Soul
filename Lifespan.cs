using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifespan : MonoBehaviour
{
    public float lifespan;


	// Use this for initialization
	void Start ()
    {
        StartCoroutine(StartDecay());
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    IEnumerator StartDecay()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(gameObject);
    }
}
