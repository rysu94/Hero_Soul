using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Projectile : MonoBehaviour
{
    //This is the base class for all projectile based Arcana
    public int id;

    //This handles the damage the spell does
    public int baseDamage, minDamage, maxDamage;

    //This handles the velocity of the projectile
    public float velocity;

    //This handles the force the spell knocksback with
    public float knockback;

    //This handles the size of the spell projectile
    public float size;
    public Vector3 projScale;
    public float tempSize;

    //This is the spell's Cost
    public int cost;

    //Checks if the collision event is occuring
    public bool isTriggered = false;

    //This flag checks if the projectile is a child projectile
    public bool child = false;

    //This flags checks if the projectile is frozen
    public bool frozen = false;

	// Use this for initialization
	void Start ()
    {
        //Get the starting scale
        projScale = transform.localScale;
        size = 1;
        if(tempSize != 0)
        {
            size = tempSize;
        }

        //Upload Spell Data
        Spell loadedSpell = Spell_Database.spellData[id];

        baseDamage = loadedSpell.spellDamage;

        velocity = loadedSpell.spellVelocity;
        knockback = loadedSpell.spellKnockback;

        //Read the rune data
        Rune loadedRune = GetComponent<Rune_Database>().FindRune(id);

        StartCoroutine(StartRoutine());

        //If the projectile is a child projectile do not attach any of the spawning effects

        //The Gemini Projectile Effect
        if(GetComponent<Effect_Gemini>())
        {
            StartCoroutine(GeminiRoutine());
            if(child)
            {
                size -= .5f;
                baseDamage /= 2;
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Spell Velocity
        if(frozen)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = transform.right.normalized * (velocity * .5f);
        }


        //Spell Size
        transform.localScale = projScale * size;
                
        if(size <= 0)
        {
            Destroy(gameObject);
        }
    }

    //Checks if the profectile is in the enemy collider
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && !isTriggered)
        {
            isTriggered = true;
            collision.gameObject.GetComponent<Monster>().DamageMonster(DamageManager.MagicDamage(collision.gameObject, (int)(baseDamage * 8 * size)));
            PlayDestructionEffect();

            //StartCoroutine(collision.gameObject.GetComponent<Monster>().CustomSlideRoutine(knockback * .1f));


            //Piercing Effect
            if (GetComponent<Effect_Pierce>())
            {
                if (size > .5f)
                {
                    StartCoroutine("PierceRoutine");
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }

        }
    }

    //The collision event of the spell's collider
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            //Check if the projectile has the looping component attached ignore the collision
            if(GetComponent<Effect_Looping>())
            {
               
            }
            else if(GetComponent<Effect_Rebounding>())
            {
                PlayDestructionEffect();
                Vector3 angle = transform.rotation.eulerAngles;
                transform.rotation = Quaternion.Euler(0, 0, angle.z + 180);
            }
            else
            {
                PlayDestructionEffect();
                Destroy(gameObject);
            }         
        }

        else if(collision.tag == "Enemy" && !isTriggered)
        {
            isTriggered = true;            
            collision.gameObject.GetComponent<Monster>().DamageMonster(DamageManager.MagicDamage(collision.gameObject, (int)(baseDamage * 8 * size)));
            PlayDestructionEffect();

            //StartCoroutine(collision.gameObject.GetComponent<Monster>().CustomSlideRoutine(knockback * .1f));
            

            //Piercing Effect
            if(GetComponent<Effect_Pierce>())
            {
                if(size > .5f)
                {
                    StartCoroutine("PierceRoutine");
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
            
        }

        else if(collision.tag == "Player")
        {

        }
    }

    //Set the size of the projectile
    void SetSize(int size)
    {
        //change scale from 1-10 to 1-3
        float newScale = size / 3f;
        transform.localScale = new Vector2(newScale, newScale);
    }

    //Set the velocity of the object
    void SetVelocity(int vel)
    {
        //change scale from 1-10 to 1-3
        float newVel = vel / 3f;
    }

    //Check which runes are attached
    void CheckRune(Rune loaded)
    {
        //Stat Runes


        //Effect Runes
        
        //Homing
        if(loaded.equippedRunes.Contains(2))
        {
            gameObject.AddComponent<Effect_Homing>();
        }
    }

    void OnBecameInvisible()
    {
        if(GetComponent<Effect_Looping>())
        {
            Vector2 pos = transform.position;
            Vector2 vel = GetComponent<Rigidbody2D>().velocity.normalized;

            float newX = vel.x * 8;
            float newY = vel.y * 8;

            pos = new Vector2(pos.x - newX, pos.y - newY);

            transform.position = pos;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator PierceRoutine()
    {
        yield return new WaitForSeconds(.5f);
        isTriggered = false;
        size -= .5f;
        baseDamage /= 2;
    }

    IEnumerator GeminiRoutine()
    {
        yield return new WaitForSeconds(.15f);
        if(!child)
        {
            Vector3 angle = transform.rotation.eulerAngles;
            GameObject tempObj = Instantiate(gameObject, transform.position, Quaternion.Euler(0, 0, angle.z + 15));
            GameObject tempObj2 = Instantiate(gameObject, transform.position, Quaternion.Euler(0, 0, angle.z - 15));
            tempObj.GetComponent<Spell_Projectile>().child = true;
            tempObj2.GetComponent<Spell_Projectile>().child = true;
            tempObj.GetComponent<Effect_Gemini>().partner = gameObject;
            tempObj2.GetComponent<Effect_Gemini>().partner = gameObject;
        }
    }


    //The destuction animation played for the projectile, overwritten by the specific projectile
    virtual public void PlayDestructionEffect()
    {
        print("NO EFFECT!");
    }

    virtual public IEnumerator StartRoutine()
    {
        yield return null;

    }

}
