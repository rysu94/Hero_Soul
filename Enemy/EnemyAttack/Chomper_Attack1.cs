using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chomper_Attack1 : MonoBehaviour
{
    public bool isTriggered;
    public List<GameObject> victims = new List<GameObject>();

    public Vector2 endPoint, startPoint;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(DamageRoutine());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerStay2D(Collider2D collider)
    {
        if ((collider.gameObject.tag == "Player" || collider.gameObject.tag == "Companion") && !victims.Contains(collider.gameObject))
        {
            victims.Add(collider.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if ((collider.gameObject.tag == "Player" || collider.gameObject.tag == "Companion") && victims.Contains(collider.gameObject) && !isTriggered)
        {
            victims.Remove(collider.gameObject);
        }
    }

    IEnumerator DamageRoutine()
    {
        yield return new WaitForEndOfFrame();
        endPoint = new Vector2(TestCharController.player.transform.position.x,
            TestCharController.player.transform.position.y - .25f);
        startPoint = transform.position;


        for(int i = 0; i < 100; i++)
        {
            //Translate to the player
            transform.Translate((endPoint - startPoint)/100);
            yield return new WaitForSeconds(.01f);
        }




        //Move towards the end point
        yield return new WaitForSeconds(3f);
        Instantiate(Resources.Load<GameObject>("Prefabs/Enemies/EnemyAttack/Chomper_Spike"), 
            new Vector2(transform.position.x, transform.position.y + .15f),Quaternion.identity);

        for (int i = 0; i < victims.Count; i++)
        {
            if(victims[i].tag == "Player")
            {
                DamageManager.PlayerDamage(20, victims[i], false);
            }
            else
            {
                victims[i].GetComponent<Companion_Controller>().DamageCompanion(20);
            }
        }


        Destroy(gameObject);
    }
}
