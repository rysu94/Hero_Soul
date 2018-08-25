using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Splinter : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(SplinterRoutine());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //Spawn Splinters
    IEnumerator SplinterRoutine()
    {
        while(GetComponent<Spell_Projectile>().size > .5f && !GetComponent<Spell_Projectile>().child)
        {
            yield return new WaitForSeconds(.5f);
            GetComponent<Spell_Projectile>().size -= .5f;
            GetComponent<Spell_Projectile>().PlayDestructionEffect();
            Vector3 angle = transform.rotation.eulerAngles;
            GameObject tempObj = Instantiate(gameObject, transform.position, Quaternion.Euler(0, 0, angle.z + 15));
            tempObj.GetComponent<Spell_Projectile>().child = true;
            tempObj.GetComponent<Spell_Projectile>().tempSize = GetComponent<Spell_Projectile>().size;
            GameObject tempObj2 = Instantiate(gameObject, transform.position, Quaternion.Euler(0, 0, angle.z - 15));
            tempObj2.GetComponent<Spell_Projectile>().child = true;
            tempObj2.GetComponent<Spell_Projectile>().tempSize = GetComponent<Spell_Projectile>().size;
        }
    }
}
