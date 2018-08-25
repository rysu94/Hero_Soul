using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magma_Lava : MonoBehaviour
{
    bool triggered = false;
    public List<GameObject> monsters = new List<GameObject>();

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(LavaTick());
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Enemy" && !monsters.Contains(collider.gameObject))
        {
            monsters.Add(collider.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy" && monsters.Contains(collider.gameObject) && !triggered)
        {
            monsters.Remove(collider.gameObject);
        }
    }

    IEnumerator LavaTick()
    {
        while(true)
        {
            triggered = false;
            yield return new WaitForSeconds(1.5f);
            triggered = true;
            for (int i = 0; i < monsters.Count; i++)
            {
                if (!monsters[i].GetComponent<Monster>().invulnerable)
                {
                    int tempInt = DamageManager.MagicDamage(monsters[i], 15);
                    monsters[i].GetComponent<Monster>().DamageMonster(tempInt);
                    Instantiate(Resources.Load("Prefabs/SpellFX/Explode_1"), new Vector2(monsters[i].transform.position.x, monsters[i].transform.position.y), Quaternion.identity);
                }          
            }
        }
    }
}
