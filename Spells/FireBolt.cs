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
        if (fireBallVelocity == new Vector2(0,3))
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
        //Left
        else if(fireBallVelocity == new Vector2(-3,0))
        {
            transform.eulerAngles = new Vector3(0, 0, -90);
        }
        //Right
        else if(fireBallVelocity == new Vector2(3,0))
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        //Down
        else if (fireBallVelocity == new Vector2(0,-3))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        fireBallAnim.Play("FireBolt");
    }
	
	// Update is called once per frame
	void Update ()
    {
        fireballRB.velocity = fireBallVelocity;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.gameObject.tag == "Wall")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Explode_1"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if(other.gameObject.tag == "Enemy" && !isTriggered)
        {
            DamageManager.spellBase = 25;
            isTriggered = true;
            Instantiate(Resources.Load("Prefabs/SpellFX/Explode_1"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
