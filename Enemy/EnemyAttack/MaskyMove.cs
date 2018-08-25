﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskyMove : MonoBehaviour
{

    public Rigidbody2D maskyMissileRB;


    // Use this for initialization
    void Start()
    {
        maskyMissileRB = GetComponent<Rigidbody2D>();
        StartCoroutine(MaskyAttack());
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
        else if(other.gameObject.tag == "Comp_Shield")
        {
            Destroy(gameObject);
            Companion_Controller.shieldAmount -= 10;
        }
        else if (other.gameObject.tag == "Player" && !TestCharController.invuln)
        {
            DamageManager.PlayerDamage(10, TestCharController.player.gameObject, false);
            //Play the player's getting hurt SFX
            TestCharController.playerAttack.clip = TestCharController.hurtNoise[Random.Range(0, TestCharController.hurtNoise.Length)];
            TestCharController.playerAttack.Play();
            Destroy(gameObject);
        }
    }

    IEnumerator MaskyAttack()
    {
        while(gameObject)
        {
            //Check if the object is paused
            while (GameController.paused)
            {
                maskyMissileRB.velocity = new Vector2(0, 0);
                yield return null;
            }

            yield return new WaitForSeconds(.1f);
            maskyMissileRB.velocity = new Vector2(0, -1.5f);
        }

    }
}

