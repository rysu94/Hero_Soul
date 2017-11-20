using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chomper_Tentacle : MonoBehaviour
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
            DamageManager.PlayerDamage(10, TestCharController.player.gameObject, false);
            //Play the player's getting hurt SFX
            TestCharController.playerAttack.clip = TestCharController.hurtNoise[Random.Range(0, TestCharController.hurtNoise.Length)];
            TestCharController.playerAttack.Play();
        }
    }
}
