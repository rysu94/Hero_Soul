using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameLash : MonoBehaviour
{

    public GameObject[] lashes = new GameObject[15];

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(LashRoutine());
	}
	
	// Update is called once per frame
	void Update ()
    {
		for(int i = 0; i < lashes.Length; i++)
        {
            lashes[i].transform.rotation = Quaternion.identity;
        }
	}

    IEnumerator LashRoutine()
    {
        lashes[0].SetActive(true);
        yield return new WaitForSeconds(.35f);
        lashes[1].SetActive(true);
        lashes[2].SetActive(true);
        yield return new WaitForSeconds(.35f);
        lashes[3].SetActive(true);
        lashes[4].SetActive(true);
        lashes[5].SetActive(true);
        yield return new WaitForSeconds(.35f);
        lashes[6].SetActive(true);
        lashes[7].SetActive(true);
        lashes[8].SetActive(true);
        lashes[9].SetActive(true);
        yield return new WaitForSeconds(.35f);
        lashes[10].SetActive(true);
        lashes[11].SetActive(true);
        lashes[12].SetActive(true);
        lashes[13].SetActive(true);
        lashes[14].SetActive(true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
