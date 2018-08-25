using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    public bool isTriggered = false;
    float angle;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float x = -transform.up.x * 4;
        float y = -transform.up.y * 4;
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
            int tempInt = DamageManager.MagicDamage(other.gameObject, 80);
            other.gameObject.GetComponent<Monster>().DamageMonster(tempInt);
            Instantiate(Resources.Load("Prefabs/SpellFX/Earth_Explode"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            isTriggered = true;

            Instantiate(Resources.Load("Prefabs/SpellFX/Stone_Small"), new Vector2(other.transform.position.x, other.transform.position.y), Quaternion.Euler(0, 0, transform.parent.eulerAngles.z));
            Instantiate(Resources.Load("Prefabs/SpellFX/Stone_Small"), new Vector2(other.transform.position.x, other.transform.position.y), Quaternion.Euler(0, 0, transform.parent.eulerAngles.z + 15));
            Instantiate(Resources.Load("Prefabs/SpellFX/Stone_Small"), new Vector2(other.transform.position.x, other.transform.position.y), Quaternion.Euler(0, 0, transform.parent.eulerAngles.z + 30));
            Instantiate(Resources.Load("Prefabs/SpellFX/Stone_Small"), new Vector2(other.transform.position.x, other.transform.position.y), Quaternion.Euler(0, 0, transform.parent.eulerAngles.z - 15));
            Instantiate(Resources.Load("Prefabs/SpellFX/Stone_Small"), new Vector2(other.transform.position.x, other.transform.position.y), Quaternion.Euler(0, 0, transform.parent.eulerAngles.z - 30));

            Destroy(gameObject);
        }
    }
}
