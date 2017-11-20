using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Count_Circle_Move : MonoBehaviour
{
    public GameObject telegraph;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(MoveRoutine());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator MoveRoutine()
    {
        yield return new WaitForSeconds(3f);
        telegraph.SetActive(false);
        GetComponent<Animator>().Play("Count_Circle_End");

        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
