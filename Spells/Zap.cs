using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zap : MonoBehaviour
{
    List<GameObject> enemies = new List<GameObject>();

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(DamageTick());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Enemy" && !enemies.Contains(collider.gameObject))
        {
            enemies.Add(collider.gameObject);
        }
    }

    IEnumerator DamageTick()
    {
        ArcanaController.isCasting = true;
        yield return new WaitForSeconds(.25f);
        for(int i = 0; i < enemies.Count; i++)
        {
            int tempInt = DamageManager.MagicDamage(enemies[i].gameObject, 50);
            enemies[i].gameObject.GetComponent<Monster>().DamageMonster(tempInt);
            Instantiate(Resources.Load("Prefabs/SpellFX/Thunder_Explode"), new Vector2(enemies[i].transform.position.x, 
                enemies[i].transform.position.y), Quaternion.identity);
        }
        ArcanaController.isCasting = false;
    }
}
