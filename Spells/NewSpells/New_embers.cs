using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_embers : Spell_Projectile
{
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
    }

}
