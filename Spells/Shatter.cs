using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shatter : MonoBehaviour
{
    bool triggered = false;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.rotation = Quaternion.identity;
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy" && !triggered)
        {
            triggered = true;
            int tempInt = DamageManager.MagicDamage(collider.gameObject, 65);
            collider.gameObject.GetComponent<Monster>().DamageMonster(tempInt);
            Instantiate(Resources.Load("Prefabs/SpellFX/Ice_Explode"), new Vector2(collider.gameObject.transform.position.x, collider.gameObject.transform.position.y), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
