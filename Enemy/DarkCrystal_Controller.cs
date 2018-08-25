using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkCrystal_Controller : Monster
{


	// Use this for initialization
	void Start ()
    {
        monsterHealth = 250;
        contactDamage = 30;

        player = GameObject.FindGameObjectWithTag("Player");
        monsterSprite = GetComponent<SpriteRenderer>();
        monsterRB = GetComponent<Rigidbody2D>();

        //Slime King Loot Table


        //Arcana [fire, water, earth, air, life]
        arcanaDrop.Add(0);
        arcanaDrop.Add(0);
        arcanaDrop.Add(0);
        arcanaDrop.Add(0);
        arcanaDrop.Add(0);

        //experience drop
        experienceDrop = 1;
    }
	
	// Update is called once per frame
	void Update ()
    {

	}
}
