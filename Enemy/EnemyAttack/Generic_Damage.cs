using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generic_Damage : MonoBehaviour
{
    public int damage = 0;
    public bool pure = false;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            DamageManager.PlayerDamage(damage, collider.gameObject, pure);
        }
    }
}
