using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Razor : MonoBehaviour
{



	// Use this for initialization
	void Start ()
    {
        StartCoroutine(RazorRoutine());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator RazorRoutine()
    {
        while(true)
        {
            if (TestCharController.player.GetComponent<TestCharController>().north)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Razor_Wind"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 180 + Random.Range(-15, 15)));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().east)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Razor_Wind"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 90 + Random.Range(-15, 15)));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().west)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Razor_Wind"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 270 + Random.Range(-15, 15)));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().south)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Razor_Wind"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 0 + Random.Range(-15, 15)));
            }
            yield return new WaitForSeconds(.1f);
        }
    }
}
