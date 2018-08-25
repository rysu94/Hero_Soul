using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDeflection : MonoBehaviour
{
    public bool reflected = false;
    public float speedFactor;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!reflected && collision.gameObject.tag == "Weapon")
        {
            //Get player facing
            Vector2 facingAngle = new Vector2(0, 0);
            if (TestCharController.player.GetComponent<TestCharController>().north)
            {
                facingAngle = new Vector2(0, 1);
            }
            else if (TestCharController.player.GetComponent<TestCharController>().south)
            {
                facingAngle = new Vector2(0, -1);
            }
            else if (TestCharController.player.GetComponent<TestCharController>().east)
            {
                facingAngle = new Vector2(1, 0);
            }
            else if (TestCharController.player.GetComponent<TestCharController>().west)
            {
                facingAngle = new Vector2(-1, 0);
            }

            Vector2 mouseVec = Camera.main.ScreenToWorldPoint(Input.mousePosition) - TestCharController.player.transform.position;
            float mouseAngle = Vector2.Angle(facingAngle, mouseVec);
            //print(mouseAngle);

            Vector2 dir = transform.position - TestCharController.player.transform.position;
            if (mouseAngle >= 90)
            {
                //Check left or right
                Vector3 cross = Vector3.Cross(facingAngle, mouseVec);
                if(cross.z > 0)
                {
                    //right
                    if (TestCharController.player.GetComponent<TestCharController>().north)
                    {
                        dir = new Vector2(1, 0);
                    }
                    else if (TestCharController.player.GetComponent<TestCharController>().south)
                    {
                        dir = new Vector2(-1, 0);
                    }
                    else if (TestCharController.player.GetComponent<TestCharController>().east)
                    {
                        dir = new Vector2(0, 1);
                    }
                    else if (TestCharController.player.GetComponent<TestCharController>().west)
                    {
                        dir = new Vector2(0, -1);
                    }
                }
                else
                {
                    //left
                    if (TestCharController.player.GetComponent<TestCharController>().north)
                    {
                        dir = new Vector2(-1, 0);
                    }
                    else if (TestCharController.player.GetComponent<TestCharController>().south)
                    {
                        dir = new Vector2(1, 0);
                    }
                    else if (TestCharController.player.GetComponent<TestCharController>().east)
                    {
                        dir = new Vector2(0, -1);
                    }
                    else if (TestCharController.player.GetComponent<TestCharController>().west)
                    {
                        dir = new Vector2(0, 1);
                    }
                }
            }
            else
            {
                dir = mouseVec;
            }


            //Get vector from object to player
            //Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - TestCharController.player.transform.position;
            //Vector2 dir = transform.position - TestCharController.player.transform.position;
            Vector2 direction = GetComponent<Rigidbody2D>().velocity;
            //Deflect now returns to sender
            GetComponent<Rigidbody2D>().velocity = direction.normalized * -speedFactor;
            gameObject.tag = "Proj_Deflect";
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Prefabs/Enemies/Proj_Deflectabled");
            GameObject.Find("DeflectNoise").GetComponent<AudioSource>().Play();
            StartCoroutine(Deflect());
            //GameObject.Find("BreakCharge").GetComponent<AudioSource>().Play();
            reflected = true;
        }
    }

    IEnumerator Deflect()
    {
        while(true)
        {
            Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/DeflectGlint"), transform.position, Quaternion.identity);
            yield return new WaitForSeconds(.25f);
            
        }
    }
}
