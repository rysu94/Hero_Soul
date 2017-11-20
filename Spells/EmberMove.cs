using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmberMove : MonoBehaviour
{
    public Rigidbody2D emberRB;
    public Vector2 right;

    public bool isTriggered = false;
	// Use this for initialization

	void Start ()
    {
        emberRB = GetComponent<Rigidbody2D>();
        right = transform.right;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float x = transform.right.x * 3;
        float y = transform.right.y * 3;
        emberRB.velocity = new Vector2(x,y);
        //print(emberRB.velocity);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.gameObject.tag == "Wall")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Explode_1"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "Enemy" && !isTriggered)
        {
            
            isTriggered = true;
            DamageManager.spellBase = -25;
            DamageManager.totalHits++;
            Instantiate(Resources.Load("Prefabs/SpellFX/Explode_1"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Destroy(this.gameObject);
        }
        
    }
}
