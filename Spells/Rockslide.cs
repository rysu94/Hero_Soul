using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rockslide : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(StartRock());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator StartRock()
    {
        yield return new WaitForSeconds(.6f);
        Instantiate(Resources.Load("Prefabs/SpellFX/Earth_Explode"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        yield return new WaitForSeconds(.2f);
        Instantiate(Resources.Load("Prefabs/SpellFX/Impale_Cast"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    }
}
