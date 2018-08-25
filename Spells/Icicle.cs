using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : MonoBehaviour
{

    public GameObject icicleBreak;
    List<GameObject> enemies = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        StartCoroutine(IcicleRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy" && !enemies.Contains(collider.gameObject))
        {
            enemies.Add(collider.gameObject);
        }
    }

    IEnumerator IcicleRoutine()
    {
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < enemies.Count; i++)
        {
            int tempInt = DamageManager.MagicDamage(enemies[i].gameObject, 50);
            enemies[i].gameObject.GetComponent<Monster>().DamageMonster(tempInt);
            Instantiate(Resources.Load("Prefabs/SpellFX/Ice_Break"), enemies[i].transform);
        }
        
    }
}
