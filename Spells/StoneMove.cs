using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMove : MonoBehaviour
{
    public float speed = 0;
    public bool isTriggered = false;

    // Use this for initialization
    void Start()
    {
        //StartCoroutine(MoveRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        float x = -transform.up.x * speed;
        float y = -transform.up.y * speed;
        GetComponent<Rigidbody2D>().velocity = new Vector2(x, y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.gameObject.tag == "Wall")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Earth_Explode"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Enemy" && !isTriggered)
        {
            int tempInt = DamageManager.MagicDamage(other.gameObject, 50);
            other.gameObject.GetComponent<Monster>().DamageMonster(tempInt);
            Instantiate(Resources.Load("Prefabs/SpellFX/Earth_Explode"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            isTriggered = true;
            Destroy(gameObject);
        }
    }
}
