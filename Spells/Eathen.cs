using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eathen : MonoBehaviour
{
    public GameObject[] earthen = new GameObject[5];
    public float[] earthAngle = new float[5];

    public int numEarth = 3;
    public float hostX;
    public float hostY;

    public float distance = 0;

    // Use this for initialization
    void Start ()
    {
        earthAngle[0] = 90;
        earthAngle[1] = 162;
        earthAngle[2] = 234;
        earthAngle[3] = 306;
        earthAngle[4] = 18;
        StartCoroutine(RotateEarth());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator RotateEarth()
    {
        while (numEarth > 0)
        {
            for (int i = 0; i < earthAngle.Length; i++)
            {
                earthAngle[i] += 2f;
                if (earthAngle[i] == 360)
                {
                    earthAngle[i] = 0;
                }
            }

            for (int i = 0; i < earthen.Length; i++)
            {
                hostX = transform.position.x + Mathf.Sin(earthAngle[i] * (Mathf.PI / 180)) * .25f;
                hostY = transform.position.y + Mathf.Cos(earthAngle[i] * (Mathf.PI / 180)) * .25f;
                earthen[i].transform.position = new Vector2(hostX, hostY);
            }

            yield return new WaitForSeconds(.01f);
        }
    }
}
