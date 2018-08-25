using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storm : MonoBehaviour
{

    public GameObject[] twister = new GameObject[5];
    public float[] twisterAngle = new float[5];

    public float hostX;
    public float hostY;

    bool distanceSwitch = false;
    public float distance = .25f;

    // Use this for initialization
    void Start ()
    {
        twisterAngle[0] = 72;
        twisterAngle[1] = 144;
        twisterAngle[2] = 216;
        twisterAngle[3] = 288;
        twisterAngle[4] = 0;
        StartCoroutine(RotateBalls());
        //StartCoroutine(WindTick());
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator RotateBalls()
    {
        while (true)
        {
            while (GameController.paused)
            {
                yield return null;
            }

            for (int i = 0; i < twisterAngle.Length; i++)
            {
                twisterAngle[i] += .5f;
                if (twisterAngle[i] == 360)
                {
                    twisterAngle[i] = 0;
                }
            }

            if (distance < .75f && !distanceSwitch)
            {
                distance += .005f;
                if (distance >= .75f)
                {
                    distanceSwitch = true;
                }
            }

            if (distance > .05f && distanceSwitch)
            {
                distance -= .001f;
                if (distance <= .05f)
                {
                    distanceSwitch = false;
                }
            }

            for (int i = 0; i < twister.Length; i++)
            {
                hostX = transform.position.x + Mathf.Sin(twisterAngle[i] * (Mathf.PI / 180)) * distance;
                hostY = transform.position.y + Mathf.Cos(twisterAngle[i] * (Mathf.PI / 180)) * distance;
                twister[i].transform.position = new Vector2(hostX, hostY);
            }

            yield return new WaitForSeconds(.01f);
        }

    }

    IEnumerator WindTick()
    {
        while (true)
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Razor_Wind"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, Random.Range(0, 360)));
            yield return new WaitForSeconds(.5f);
        }
    }
}
