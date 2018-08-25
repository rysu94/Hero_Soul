using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public int wallHP = 10;
    public bool isTriggered = false;
    float color = 1;

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
        if ((collider.gameObject.tag == "Enemy_Proj" || collider.gameObject.tag == "Enemy" || collider.gameObject.tag == "Weapon"))
        {
            if(collider.gameObject.tag == "Enemy_Proj")
            {
                Destroy(collider.gameObject);
            }
            
            wallHP--;
            color -= .05f;
            GetComponent<SpriteRenderer>().color = new Color(color, color, color);
            if (wallHP <= 0)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Earth_Explode"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                gameObject.SetActive(false);
            }

        }

        else if(collider.gameObject.tag == "Wall")
        {
            print("Wall");
            Instantiate(Resources.Load("Prefabs/SpellFX/Earth_Explode"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            gameObject.SetActive(false);
        }

    }
}
