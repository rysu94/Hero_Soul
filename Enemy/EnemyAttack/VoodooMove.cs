using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoodooMove : MonoBehaviour
{
    public GameObject host;
    public GameObject[] orbs = new GameObject[3];
    public float[] orbAngle = new float[3];

    public int numOrbs = 3;

    public float hostX;
    public float hostY;

    bool distanceSwitch = false;
    public float distance = .25f;

	// Use this for initialization
	void Start ()
    {
        //orb 1 90
        orbAngle[0] = 90;
        //orb 2 210
        orbAngle[1] = 210;
        //orb3 330
        orbAngle[2] = 330;
        StartCoroutine(RotateBalls());	
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    IEnumerator RotateBalls()
    {
        while(numOrbs > 0)
        {
            while (GameController.paused)
            {
                yield return null;
            }

            for(int i = 0; i < orbAngle.Length; i++)
            {
                orbAngle[i] += 2f;
                if(orbAngle[i] == 360)
                {
                    orbAngle[i] = 0;
                }
            }

            if(distance < .5f && !distanceSwitch)
            {
                distance += .005f;
                if(distance >= .5f)
                {
                    distanceSwitch = true;
                }
            }

            if(distance > .05f && distanceSwitch)
            {
                distance -= .001f;
                if(distance <= .05f)
                {
                    distanceSwitch = false;
                }
            }




            for (int i = 0; i < orbs.Length; i++)
            {
                hostX = transform.position.x + Mathf.Sin(orbAngle[i] * (Mathf.PI / 180)) * distance;
                hostY = transform.position.y + Mathf.Cos(orbAngle[i] * (Mathf.PI / 180)) * distance;
                orbs[i].transform.position = new Vector2(hostX , hostY);
            }

            yield return new WaitForSeconds(.01f);
        }
        
    }


}
