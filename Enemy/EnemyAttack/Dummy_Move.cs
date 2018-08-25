using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy_Move : MonoBehaviour
{
    public Rigidbody2D dummyMissileRB;


    // Use this for initialization
    void Start()
    {
        dummyMissileRB = GetComponent<Rigidbody2D>();
        StartCoroutine(OrboMove());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Player" && !TestCharController.invuln)
        {
            TrialDodgeController.hitCount++;
            //Play the player's getting hurt SFX
            TestCharController.playerAttack.clip = TestCharController.hurtNoise[Random.Range(0, TestCharController.hurtNoise.Length)];
            TestCharController.playerAttack.Play();
            Destroy(gameObject);
        }
    }

    IEnumerator OrboMove()
    {
        while (gameObject)
        {
            //Check if the object is paused
            while (GameController.paused)
            {
                dummyMissileRB.velocity = new Vector2(0, 0);
                yield return null;
            }

            yield return new WaitForSeconds(.1f);
            if (dummyMissileRB.velocity.magnitude <= 0)
            {
                dummyMissileRB.velocity = (TestCharController.player.transform.position - transform.position).normalized * .5f;
            }

        }

    }
}
