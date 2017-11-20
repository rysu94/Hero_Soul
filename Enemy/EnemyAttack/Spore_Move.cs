using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spore_Move : MonoBehaviour
{
    public Rigidbody2D sporeRB;
    public int lifespan = 8;

    public AudioSource pop;
    public Animator popAnim;

	// Use this for initialization
	void Start ()
    {
        sporeRB = GetComponent<Rigidbody2D>();
        StartCoroutine(DecayRoutine());
        StartCoroutine(SporeMove());
        pop = GetComponent<AudioSource>();
        popAnim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Wall" || other.gameObject.tag == "Weapon")
        {
            StartCoroutine(Pop());
        }
        else if(other.gameObject.tag == "Player" && !TestCharController.invuln)
        {
            DamageManager.PlayerDamage(5, TestCharController.player.gameObject, false);
            //Play the player's getting hurt SFX
            TestCharController.playerAttack.clip = TestCharController.hurtNoise[Random.Range(0, TestCharController.hurtNoise.Length)];
            TestCharController.playerAttack.Play();
            StartCoroutine(Pop());

            //Chance for poison state
            int tempInt = Random.Range(1, 6);
            if(tempInt == 1)
            {
                GameObject.Find("States").GetComponent<StateManager>().AddState(25, 1, 1, true);
            }
        }
    }

    IEnumerator DecayRoutine()
    {
        while(lifespan > 0)
        {

            //Check if the object is paused
            while (GameController.paused)
            {
                yield return null;
            }

            yield return new WaitForSeconds(1f);
            lifespan--;
        }
        StartCoroutine(Pop());    
    }

    IEnumerator SporeMove()
    {
        float x = transform.right.x * .25f;
        float y = transform.right.y * .25f;
        while (lifespan > 0)
        {

            //Check if the object is paused
            while (GameController.paused)
            {
                sporeRB.velocity = new Vector2(0, 0);
                yield return null;
            }

            sporeRB.velocity = new Vector2(x, y);
            yield return new WaitForSeconds(1f);
            sporeRB.velocity = new Vector2(-x, -y);
            yield return new WaitForSeconds(.25f);

        }
    }

    IEnumerator Pop()
    {
        pop.Play();
        popAnim.Play("Spore_Pop");
        yield return new WaitForSeconds(.15f);
        Destroy(gameObject);
    }
}
