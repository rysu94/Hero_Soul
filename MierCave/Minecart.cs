using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minecart : MonoBehaviour
{
    bool isPlayerSliding = false;
    public float redValue = 0.3f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (!TestCharController.invuln)
            {
                TestCharController.player.GetComponent<SpriteRenderer>().color = new Color(1f, redValue, redValue);
                StartCoroutine("ColorReturnRoutinePlayer");
            }

            if (!isPlayerSliding &&!TestCharController.breakInvuln)
            {
                StartCoroutine("PlayerSlideRoutine");
            }
        }
    }

    IEnumerator ColorReturnRoutinePlayer()
    {
        float tempColor = redValue;
        while (tempColor <= 1)
        {
            yield return new WaitForSeconds(.05f);
            tempColor += .1f;
            TestCharController.player.GetComponent<SpriteRenderer>().color = new Color(1f, tempColor, tempColor);
        }
    }

    //This coroutine executes a player slide when contacting an enemy
    IEnumerator PlayerSlideRoutine()
    {
        isPlayerSliding = true;

        //This flag prevents players from moving when they are getting hit
        TestCharController.isHit = true;

        //Determines the vector for the player to get knocked back
        var direction = (TestCharController.player.transform.position - transform.position).normalized;

        TestCharController.player.GetComponent<Rigidbody2D>().velocity = direction * 2;


        //Deal the monster's contact damage to the player
        DamageManager.PlayerDamage(25, TestCharController.player.gameObject, false);

        //Play the player's getting hurt SFX
        if (!TestCharController.breakInvuln)
        {
            TestCharController.playerAttack.clip = TestCharController.hurtNoise[Random.Range(0, TestCharController.hurtNoise.Length)];
            TestCharController.playerAttack.Play();
        }

        yield return new WaitForSeconds(.15f);

        TestCharController.isHit = false;

        isPlayerSliding = false;
    }
}





