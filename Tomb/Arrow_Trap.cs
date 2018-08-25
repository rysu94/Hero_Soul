using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Trap : MonoBehaviour
{
    public float delay;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(ArrowRoutine());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator ArrowRoutine()
    {
        yield return new WaitForSeconds(delay);
        while (true)
        {
            yield return new WaitForSeconds(3f);
            Instantiate(Resources.Load("Prefabs/Tomb_Tiles/Trap_Arrow"), transform.position, Quaternion.Euler(0,0,45));
        }
    }
}
