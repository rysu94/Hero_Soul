using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardian_Dmg : MonoBehaviour
{
    bool triggered = false;
    public List<GameObject> monsters = new List<GameObject>();

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(GuardianTick());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && !monsters.Contains(other.gameObject))
        {
            monsters.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && monsters.Contains(other.gameObject) && !triggered)
        {
            monsters.Remove(other.gameObject);
        }
    }

    IEnumerator GuardianTick()
    {
        while (true)
        {
            triggered = false;
            yield return new WaitForSeconds(1.5f);
            triggered = true;
            for (int i = 0; i < monsters.Count; i++)
            {
                if (!monsters[i].GetComponent<Monster>().invulnerable)
                {
                    int tempInt = DamageManager.MagicDamage(monsters[i], 25);
                    monsters[i].GetComponent<Monster>().DamageMonster(tempInt);
                    Instantiate(Resources.Load("Prefabs/SpellFX/Wind_Explode"), new Vector2(monsters[i].transform.position.x, monsters[i].transform.position.y), Quaternion.identity);
                }
            }
        }
    }
}
