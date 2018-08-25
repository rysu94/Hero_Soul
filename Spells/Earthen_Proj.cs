using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthen_Proj : MonoBehaviour
{

    int projHit = 0;
    float color = 1;
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
        if(collider.gameObject.tag == "Enemy_Proj")
        {
            Destroy(collider.gameObject);
            projHit++;
            color -= .25f;
            GetComponent<SpriteRenderer>().color = new Color(color,color,color);
            if (projHit == 3)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Earth_Explode"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                gameObject.SetActive(false);
            }
            
        }
        else if(collider.gameObject.tag == "Enemy" && !triggered)
        {
            triggered = true;
            int tempInt = DamageManager.MagicDamage(collider.gameObject, 10);
            collider.gameObject.GetComponent<Monster>().DamageMonster(tempInt);
            Instantiate(Resources.Load("Prefabs/SpellFX/Earth_Explode"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
