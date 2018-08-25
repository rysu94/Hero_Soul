using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBolt : MonoBehaviour
{
    public AudioSource fireBallNoise;
    public Rigidbody2D fireballRB;

    public Vector2 fireBallVelocity;

    public Animator fireBallAnim;

    public bool isTriggered = false;


	// Use this for initialization
	void Start ()
    {
        fireBallNoise = GetComponent<AudioSource>();
        fireballRB = GetComponent<Rigidbody2D>();
        fireBallVelocity = TestCharController.playerVel;
        fireBallAnim = GetComponent<Animator>();
        
        //Up
        if (TestCharController.player.GetComponent<TestCharController>().north)
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
            fireballRB.velocity = new Vector2(0, 3);
        }
        //Left
        else if(TestCharController.player.GetComponent<TestCharController>().west)
        {
            transform.eulerAngles = new Vector3(0, 0, -90);
            fireballRB.velocity = new Vector2(-3, 0);
        }
        //Right
        else if(TestCharController.player.GetComponent<TestCharController>().east)
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
            fireballRB.velocity = new Vector2(3, 0);
        }
        //Down
        else if (TestCharController.player.GetComponent<TestCharController>().south)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            fireballRB.velocity = new Vector2(0, -3);
        }

        fireBallAnim.Play("FireBolt");
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.gameObject.tag == "Wall")
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
            Destroy(this.gameObject);
        }
        else if(other.gameObject.tag == "Enemy" && !isTriggered)
        {
            int tempInt = DamageManager.MagicDamage(other.gameObject, 50);
            other.gameObject.GetComponent<Monster>().DamageMonster(tempInt);
            isTriggered = true;
            int tempInt2 = Random.Range(1, 4);
            switch (tempInt2)
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
            Destroy(this.gameObject);
        }
    }
}
