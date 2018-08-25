using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingWeapon : MonoBehaviour
{
    public GameObject weapon;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(Swing());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator Swing()
    {
        while(true)
        {
            GetComponent<AudioSource>().Play();
            for(float i = 0; i > -90; i-=5f)
            {
                weapon.transform.rotation = Quaternion.Euler(0, 0, i);
                yield return new WaitForSeconds(.01f);
            }
            yield return new WaitForSeconds(Random.Range(0.1f, 1f));
            GetComponent<AudioSource>().Play();
            for (float i = -90; i < 0; i+=5f)
            {
                weapon.transform.rotation = Quaternion.Euler(0, 0, i);
                yield return new WaitForSeconds(.01f);
            }
            yield return new WaitForSeconds(Random.Range(0.1f, 1f));
        }
    }
}
