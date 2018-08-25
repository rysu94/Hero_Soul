using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_FlameLash : Spell_Projectile
{
    //The size of an arm of the lash
    public int lashSize = 3;

    public List<GameObject> lashes = new List<GameObject>();
    public int[] lashAngle = new int[4];

    public override IEnumerator StartRoutine()
    {
        lashAngle[0] = 0;
        lashAngle[1] = 90;
        lashAngle[2] = 180;
        lashAngle[3] = 270;
        lashes.Clear();
        //Instantiate the lashes
        for (int i = 0; i < 4; i++)
        {
            int angle = 90 * i;
            for (int j = 1; j < lashSize + 1; j++)
            {
                Vector3 lashPos = new Vector3(0, 0);
                if (i == 0)
                {
                    lashPos = new Vector3(.25f, 0);
                }
                else if (i == 1)
                {
                    lashPos = new Vector3(0, .25f);
                }
                else if (i == 2)
                {
                    lashPos = new Vector3(-.25f, 0);
                }
                else if (i == 3)
                {
                    lashPos = new Vector3(0, -.25f);
                }
                GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/SpellFX/NewSpell/FlameLash_Chain_New"), transform.position + lashPos * j * .4f, Quaternion.Euler(0, 0, angle));
                lashes.Add(tempObj);
                tempObj.GetComponent<New_Firebolt>().frozen = true;
                yield return null;
            }
        }

        StartCoroutine(FlameLash());
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

        for(int i = 0; i < lashes.Count; i++)
        {
            if (lashes[i] != null)
            {
                lashes[i].GetComponent<New_Firebolt>().frozen = false;
            }
                
        }
        
    }

    IEnumerator FlameLash()
    {
        while (true)
        {
            for (int i = 0; i < lashAngle.Length; i++)
            {
                lashAngle[i] += 3;
                if (lashAngle[i] >= 360)
                {
                    lashAngle[i] = 0;
                }

                for (int j = i * lashSize; j < (i * lashSize) + lashSize; j++)
                {
                    float lashX = transform.position.x + Mathf.Cos(lashAngle[i] * (Mathf.PI / 180)) * .11f * (j % lashSize + 1);
                    float lashY = transform.position.y + Mathf.Sin(lashAngle[i] * (Mathf.PI / 180)) * .11f * (j % lashSize + 1);
                    if(lashes[j] != null)
                    {
                        lashes[j].transform.position = new Vector2(lashX, lashY);
                        //if(lashAngle > )
                        lashes[j].transform.rotation = Quaternion.Euler(0, 0, lashAngle[i]);
                    }

                }
            }
            yield return new WaitForSeconds(.01f);
        }
    }

}
