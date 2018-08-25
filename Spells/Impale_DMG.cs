using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impale_DMG : MonoBehaviour
{

    bool triggered = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy" && !triggered)
        {
            triggered = true;
            int tempInt = DamageManager.MagicDamage(collider.gameObject, 45);
            collider.gameObject.GetComponent<Monster>().DamageMonster(tempInt);
            Instantiate(Resources.Load("Prefabs/SpellFX/Earth_Explode"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
