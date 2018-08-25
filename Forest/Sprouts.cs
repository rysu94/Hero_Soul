using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprouts : MonoBehaviour
{
    bool inAnimation = false;
    bool isAwake = false;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(!inAnimation)
        {
            float distance = Vector2.Distance(transform.position, TestCharController.player.transform.position);
            //print(distance);
            if (distance <= .75f && !isAwake)
            {
                StartCoroutine(PlayAwake());
            }
            else if(distance >= 1f && isAwake)
            {
                StartCoroutine(PlayDecay());
            }
        }
        

	}

    IEnumerator PlayAwake()
    {
        inAnimation = true;
        GetComponent<Animator>().Play("Sprout_Grow");
        yield return new WaitForSeconds(1f);
        inAnimation = false;
        isAwake = true;
    }

    IEnumerator PlayDecay()
    {
        isAwake = false;
        inAnimation = true;
        GetComponent<Animator>().Play("Sprout_Die");
        yield return new WaitForSeconds(.4f);
        inAnimation = false;
    }
}
