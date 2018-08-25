using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifespan_Spell : MonoBehaviour
{
    // 1 Fire  2 Earth
    public int element;

    public float timer;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(DecayRoutine());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator DecayRoutine()
    {
        yield return new WaitForSeconds(timer + Random.Range(-1, 1));

        if(element == 1)
        {
            int tempInt = Random.Range(1, 4);
            switch (tempInt)
            {
                default:
                    break;
                case 1:
                    Instantiate(Resources.Load("Prefabs/SpellFX/Explode_1"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(Resources.Load("Prefabs/SpellFX/Explode_1_B"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    break;
                case 3:
                    Instantiate(Resources.Load("Prefabs/SpellFX/Explode_1_C"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    break;
            }
        }

        Destroy(gameObject);
    }
}
