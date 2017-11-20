using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoodooController : Monster
{

    public Animator voodooAnim;

	// Use this for initialization
	void Start ()
    {
        monsterHealth = 40;
        contactDamage = 10;

        player = GameObject.FindGameObjectWithTag("Player");
        voodooAnim = GetComponent<Animator>();
        monsterSprite = GetComponent<SpriteRenderer>();
        monsterRB = GetComponent<Rigidbody2D>();

        //Arcana [fire, water, earth, air, life]
        arcanaDrop.Add(1);
        arcanaDrop.Add(0);
        arcanaDrop.Add(0);
        arcanaDrop.Add(0);
        arcanaDrop.Add(1);

        experienceDrop = 3;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
