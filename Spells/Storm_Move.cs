using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storm_Move : MonoBehaviour
{

    bool triggered = false;
    public List<GameObject> monsters = new List<GameObject>();

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(TwisterTick());

    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //print("Enter");
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
        }
        else if (other.gameObject.tag == "Enemy" && !monsters.Contains(other.gameObject))
        {
            monsters.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //print("Exit");
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
        if (other.gameObject.tag == "Enemy" && monsters.Contains(other.gameObject) && !triggered)
        {
            monsters.Remove(other.gameObject);
        }
    }

    IEnumerator TwisterTick()
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
                    int tempInt = DamageManager.MagicDamage(monsters[i], 45);
                    monsters[i].GetComponent<Monster>().DamageMonster(tempInt);
                    Instantiate(Resources.Load("Prefabs/SpellFX/Wind_Explode"), new Vector2(monsters[i].transform.position.x, monsters[i].transform.position.y), Quaternion.identity);
                }
            }
        }
    }


}
