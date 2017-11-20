using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTextAnim : MonoBehaviour
{

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
        while(this != null)
        {
            for(int i = 0; i < 50; i++)
            {
                float newY = transform.position.y + .00025f;
                transform.position = new Vector2(transform.position.x, newY);
                yield return new WaitForSeconds(.01f);
            }
            for (int i = 0; i < 50; i++)
            {
                float newY = transform.position.y - .00025f;
                transform.position = new Vector2(transform.position.x, newY);
                yield return new WaitForSeconds(.01f);
            }
        }
    }
}
