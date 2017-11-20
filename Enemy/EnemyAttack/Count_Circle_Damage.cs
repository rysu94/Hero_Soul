using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Count_Circle_Damage : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !TestCharController.invuln)
        {
            //Play the player's getting hurt SFX
            TestCharController.playerAttack.clip = TestCharController.hurtNoise[Random.Range(0, TestCharController.hurtNoise.Length)];
            TestCharController.playerAttack.Play();
            GameObject.Find("States").GetComponent<StateManager>().AddState(5, 7, 1, false);
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}
