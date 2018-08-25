using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tidal : MonoBehaviour
{

    public List<GameObject> monsters = new List<GameObject>();

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(TidalRoutine());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator TidalRoutine()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(Resources.Load("Prefabs/SpellFX/Tidal_Wave"), new Vector2(0, 0), Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(.5f);
        DealDamage();
        yield return new WaitForSeconds(.5f);
        Instantiate(Resources.Load("Prefabs/SpellFX/Tidal_Wave"), new Vector2(0, 0), Quaternion.Euler(0, 0, 180));
        yield return new WaitForSeconds(.5f);
        DealDamage();
        yield return new WaitForSeconds(.5f);
        Instantiate(Resources.Load("Prefabs/SpellFX/Tidal_Spiral"), new Vector2(0, 0), Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(.5f);
        DealDamage();
    }

    void DealDamage()
    {
        monsters.Clear();
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            monsters.Add(enemy.gameObject);
        }


        for (int i = 0; i < monsters.Count; i++)
        {
            if (!monsters[i].GetComponent<Monster>().invulnerable)
            {
                int tempInt = DamageManager.MagicDamage(monsters[i], 95);
                monsters[i].GetComponent<Monster>().DamageMonster(tempInt);
                Instantiate(Resources.Load("Prefabs/SpellFX/Bubble_Pop"), new Vector2(monsters[i].transform.position.x, monsters[i].transform.position.y), Quaternion.identity);
            }
        }
    }
}
