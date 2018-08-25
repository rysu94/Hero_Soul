using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impale : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(StartImpale());
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.rotation = Quaternion.identity;
	}

    IEnumerator StartImpale()
    {
        Instantiate(Resources.Load("Prefabs/SpellFX/Impale"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        yield return new WaitForSeconds(.5f);
        GetComponent<AudioSource>().Play();
        for(int i = 0; i < 360; i+=60)
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Impale_Offset"), new Vector2(transform.position.x, transform.position.y), Quaternion.Euler(0, 0, i));
        }
        yield return new WaitForSeconds(.5f);
        GetComponent<AudioSource>().Play();
        for (int i = 0; i < 360; i += 45)
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Impale_Offset2"), new Vector2(transform.position.x, transform.position.y), Quaternion.Euler(0, 0, i));
        }
    }
}
