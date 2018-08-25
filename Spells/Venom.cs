using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Venom : MonoBehaviour
{

    public bool isTriggered = false;

    // Use this for initialization
    void Start()
    {
        float x = -transform.up.x * .35f;
        float y = -transform.up.y * .35f;
        GetComponent<Rigidbody2D>().velocity = new Vector2(x, y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.gameObject.tag == "Wall")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Venom_Explode"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Enemy" && !isTriggered)
        {
            isTriggered = true;
            int tempInt = DamageManager.MagicDamage(other.gameObject, 25);
            other.gameObject.GetComponent<Monster>().DamageMonster(tempInt);
            Instantiate(Resources.Load("Prefabs/SpellFX/Venom_Explode"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
