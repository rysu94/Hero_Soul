using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameLashDamage : MonoBehaviour
{
    bool triggered = false;

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
        if(collider.gameObject.tag == "Enemy" && !triggered)
        {
            triggered = true;
            int tempInt = DamageManager.MagicDamage(collider.gameObject, 35);
            collider.gameObject.GetComponent<Monster>().DamageMonster(tempInt);
            Instantiate(Resources.Load("Prefabs/SpellFX/Explode_1"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            gameObject.SetActive(false);
        }

    }
}
