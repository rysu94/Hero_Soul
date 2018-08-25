using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shatter_Line : MonoBehaviour
{

    Vector2 mousePos, playerPos, normal;
    int numShatters = 0;

    float xIncrement, yIncrement;

	// Use this for initialization
	void Start ()
    {
        if(GameController.xbox360Enabled())
        {
            mousePos = Move_Cross.crossPos;
        }
        else
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
		
        playerPos = transform.position;

        float distance = Vector2.Distance(mousePos, playerPos);
        numShatters = (int)Mathf.Floor(distance / .32f);
        if(numShatters <= 0)
        {
            numShatters = 1;
        }

        normal = mousePos - playerPos;

        xIncrement = normal.x / numShatters;
        yIncrement = normal.y / numShatters;

        StartCoroutine(StartLine());

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator StartLine()
    {
        Vector2 pos = transform.position;
        for(int i = 0; i < numShatters; i++)
        {
            yield return new WaitForSeconds(.2f);
            pos = new Vector2(pos.x + xIncrement, pos.y + yIncrement);
            Instantiate(Resources.Load("Prefabs/SpellFX/Shatter"), pos, Quaternion.identity);
        }
    }
}
