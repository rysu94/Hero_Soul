using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{

    bool triggered = false;
    public List<GameObject> monsters = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        StartCoroutine(WaterPush());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy" && !monsters.Contains(collider.gameObject))
        {
            monsters.Add(collider.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && monsters.Contains(other.gameObject) && !triggered)
        {
            monsters.Remove(other.gameObject);
        }
    }
    IEnumerator WaterPush()
    {
        yield return new WaitForSeconds(.3f);
        triggered = true;
        for (int i = 0; i < monsters.Count; i++)
        {
            if (!monsters[i].GetComponent<Monster>().invulnerable)
            {
                int tempInt = DamageManager.MagicDamage(monsters[i], 45);
                monsters[i].GetComponent<Monster>().DamageMonster(tempInt);
                Instantiate(Resources.Load("Prefabs/SpellFX/Bubble_Pop"), new Vector2(monsters[i].transform.position.x, monsters[i].transform.position.y), Quaternion.identity);

                StartCoroutine(monsters[i].GetComponent<Monster>().CustomSlideRoutine(2));
            }
        }

    }
}
