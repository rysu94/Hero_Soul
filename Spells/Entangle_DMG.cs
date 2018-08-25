using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entangle_DMG : MonoBehaviour
{

    public GameObject target;
    public int timer = 15;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(EntangleTick());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator EntangleTick()
    {
        while(timer > 0)
        {
            if(timer % 3 == 0)
            {
                int tempInt = DamageManager.MagicDamage(target.gameObject, 15);
                target.gameObject.GetComponent<Monster>().DamageMonster(tempInt);
                GetComponent<AudioSource>().Play();
            }
            yield return new WaitForSeconds(1f);
            timer--;
        }
        Destroy(gameObject);
    }
}
