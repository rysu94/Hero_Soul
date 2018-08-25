using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_Blaze : Spell_Projectile
{
    void Update()
    {
        //Spell Size
        transform.localScale = projScale * size;      
    }

    public override IEnumerator StartRoutine()
    {
        //Up
        if (TestCharController.player.GetComponent<TestCharController>().north)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, (velocity * .5f));
        }
        //Left
        else if (TestCharController.player.GetComponent<TestCharController>().west)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-(velocity * .5f), 0);
        }
        //Right
        else if (TestCharController.player.GetComponent<TestCharController>().east)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2((velocity * .5f), 0);
        }
        //Down
        else if (TestCharController.player.GetComponent<TestCharController>().south)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -(velocity * .5f));
        }
        transform.rotation = Quaternion.identity;
        yield return new WaitForSeconds(2f);
        Explode();
        Destroy(gameObject);
    }

    IEnumerator ExplodeRoutine()
    {
        yield return new WaitForSeconds(2f);
        Explode();
        Destroy(gameObject);
    }

    public void Explode()
    {
        for (float i = 0; i < 360; i += 22.5f)
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/NewSpell/Embers_New"), transform.position, Quaternion.Euler(0, 0, i));
        }

        Instantiate(Resources.Load("Prefabs/SpellFX/Explode_1_Large"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    }

    public override void PlayDestructionEffect()
    {
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
        Explode();
    }
}
