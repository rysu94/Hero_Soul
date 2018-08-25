using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{

    public bool isTriggered = false;

    // Use this for initialization
    void Start ()
    {
        float x = -transform.up.x * .25f;
        float y = -transform.up.y * .25f;
        GetComponent<Rigidbody2D>().velocity = new Vector2(x, y);
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.rotation = Quaternion.identity;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.gameObject.tag == "Wall")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Bubble_Pop"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Enemy" && !isTriggered)
        {
            int tempInt = DamageManager.MagicDamage(other.gameObject, 45);
            other.gameObject.GetComponent<Monster>().DamageMonster(tempInt);
            Instantiate(Resources.Load("Prefabs/SpellFX/Bubble_Pop"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            isTriggered = true;
            Destroy(gameObject);
        }
    }
}
