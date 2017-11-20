using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Embers : MonoBehaviour
{

    public Vector2 emberVelocity;

    public Vector2 localVel;

	// Use this for initialization
	void Start ()
    {
        DamageManager.totalHits = 0;
        emberVelocity = TestCharController.playerVel;
        //Up
        if (emberVelocity == new Vector2(0, 3))
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        //Left
        else if (emberVelocity == new Vector2(-3, 0))
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
        //Right
        else if (emberVelocity == new Vector2(3, 0))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        //Down
        else if (emberVelocity == new Vector2(0, -3))
        {
            transform.eulerAngles = new Vector3(0, 0, -90);
        }

        StartCoroutine(RemoveRoutine());

    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    IEnumerator RemoveRoutine()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
