using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPool : MonoBehaviour
{
    
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            TestCharController.slowModifier = 1;
        }
    }
    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            TestCharController.slowModifier = .25f;
        }
        
    }


}
