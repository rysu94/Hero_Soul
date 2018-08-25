using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twister : MonoBehaviour
{

    bool triggered = false;
    public List<GameObject> monsters = new List<GameObject>();

    // Use this for initialization
    void Start ()
    {
        if (TestCharController.player.GetComponent<TestCharController>().north)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1.5f);
        }
        else if (TestCharController.player.GetComponent<TestCharController>().south)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1.5f);
        }
        else if (TestCharController.player.GetComponent<TestCharController>().east)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(1.5f, 0);
        }
        else if (TestCharController.player.GetComponent<TestCharController>().west)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-1.5f, 0);
        }

        StartCoroutine(MoveRoutine());
        StartCoroutine(TwisterTick());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator MoveRoutine()
    {
        yield return new WaitForSeconds(.35f);
        if (TestCharController.player.GetComponent<TestCharController>().north)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1) * .1f;
        }
        else if (TestCharController.player.GetComponent<TestCharController>().south)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1) * .1f;
        }
        else if (TestCharController.player.GetComponent<TestCharController>().east)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0) * .1f;
        }
        else if (TestCharController.player.GetComponent<TestCharController>().west)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0) * .1f;
        }
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
                    int tempInt = DamageManager.MagicDamage(monsters[i], 25);
                    monsters[i].GetComponent<Monster>().DamageMonster(tempInt);
                    Instantiate(Resources.Load("Prefabs/SpellFX/Wind_Explode"), new Vector2(monsters[i].transform.position.x, monsters[i].transform.position.y), Quaternion.identity);
                }
            }
        }
    }

}
