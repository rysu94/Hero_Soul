using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enwater_Cast : MonoBehaviour
{
    public GameObject[] glint = new GameObject[3];


	// Use this for initialization
	void Start ()
    {
        StartCoroutine(EnwaterRoutine());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator EnwaterRoutine()
    {
        glint[0].SetActive(true);
        yield return new WaitForSeconds(.33f);
        glint[1].SetActive(true);
        GameObject.Find("States").GetComponent<StateManager>().AddState(30, 11, 1, true);
        yield return new WaitForSeconds(.33f);
        glint[2].SetActive(true);
        yield return new WaitForSeconds(1.33f);
        Destroy(gameObject);
    }
}
