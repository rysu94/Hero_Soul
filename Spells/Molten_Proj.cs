using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molten_Proj : MonoBehaviour
{
    bool triggered = false;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(MoltenRoutine());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Enemy" && !triggered)
        {
            triggered = true;
            int tempInt = DamageManager.MagicDamage(collider.gameObject, 40);
            collider.gameObject.GetComponent<Monster>().DamageMonster(tempInt);
            Destroy(gameObject);
        }
    }

    IEnumerator MoltenRoutine()
    {
        yield return new WaitForSeconds(.75f);
        Instantiate(Resources.Load("Prefabs/SpellFX/Explode_1"), new Vector2(transform.parent.position.x, transform.parent.position.y), Quaternion.identity);
        //GameObject tempObj = Instantiate(Resources.Load("Prefabs/SpellFX/Lava"), new Vector2(transform.parent.position.x, transform.parent.position.y), Quaternion.identity) as GameObject;
        //tempObj.transform.localScale = new Vector3(1f, .75f ,0);
    }
}
