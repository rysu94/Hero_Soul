using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Gemini : MonoBehaviour
{
    public GameObject partner;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(HomingRoutine());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator HomingRoutine()
    {
        //Small delay before the effect takes place
        //Determine the closest enemy
        yield return new WaitForSeconds(.15f);
        while (true)
        {
            if (partner != null)
            {
                //Rotate the projectile until it faces the enemy
                Vector3 targetRot = partner.transform.position;
                targetRot.z = 0;

                Vector3 objPos = transform.position;
                targetRot.x = targetRot.x - objPos.x;
                targetRot.y = targetRot.y - objPos.y;

                float angle = Mathf.Atan2(targetRot.y, targetRot.x) * Mathf.Rad2Deg;
                Quaternion newRot = Quaternion.Euler(new Vector3(0, 0, angle));

                transform.rotation = Quaternion.Lerp(transform.rotation, newRot, .05f);

            }
            yield return new WaitForSeconds(.15f);

        }
    }
}
