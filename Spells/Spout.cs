using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spout : MonoBehaviour
{

    int startAngle = 0;
    public List<GameObject> enemies = new List<GameObject>();

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(SpoutRoutine());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator SpoutRoutine()
    {
        while (true)
        {
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(.5f);
            GetComponent<BoxCollider2D>().enabled = true;
            for (int i = 0; i < 360; i += 60)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Bubble_Spell"), new Vector2(transform.position.x, transform.position.y), Quaternion.Euler(0, 0, i + startAngle));
            }
            startAngle += 15;
            yield return new WaitForSeconds(2.5f);
            GetComponent<BoxCollider2D>().enabled = false;
            enemies.Clear();
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && !enemies.Contains(other.gameObject))
        {
            enemies.Add(other.gameObject);
            int tempInt = DamageManager.MagicDamage(other.gameObject, 25);
            other.gameObject.GetComponent<Monster>().DamageMonster(tempInt);
            Instantiate(Resources.Load("Prefabs/SpellFX/Bubble_Pop"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        }
    }
}
