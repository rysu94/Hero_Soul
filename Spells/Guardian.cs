using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardian : MonoBehaviour
{
    public GameObject guardian;
    public int angle;
    public float hostX;
    public float hostY;

    public float distance;
    public GameObject attackTarget;

    public bool attacking = false;

    public Animator guardAnim;

    // Use this for initialization
    void Start ()
    {
        angle = 90;
        StartCoroutine(RotateGuardian());
    }
	
	// Update is called once per frame
	void Update ()
    {
        //leash range
        distance = 1.5f;
        attackTarget = null;
		foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            float tempDist = Vector2.Distance(enemy.transform.position, TestCharController.player.transform.position);
            if(tempDist < distance)
            {
                distance = tempDist;
                attackTarget = enemy;
            }
        }
        if(attackTarget != null)
        {
            attacking = true;
            if(Vector2.Distance(guardian.transform.position, attackTarget.transform.position) < .1f)
            {
                guardAnim.Play("Guardian_Attack");
            }
            else
            {
                guardian.GetComponent<Rigidbody2D>().velocity = (attackTarget.transform.position - guardian.transform.position).normalized;
            }
            
        }
        else
        {
            if(Vector2.Distance(guardian.transform.position, TestCharController.player.transform.position) > .1f && attacking)
            {
                guardAnim.Play("Guardian");
                guardian.GetComponent<Rigidbody2D>().velocity = (TestCharController.player.transform.position - guardian.transform.position).normalized;
            }
            else
            {
                attacking = false;
            }
        }

	}

    IEnumerator RotateGuardian()
    {
        while(true)
        {
            if(!attacking)
            {
                angle += 1;
                if (angle == 360)
                {
                    angle = 0;
                }
                hostX = transform.position.x + Mathf.Sin(angle * (Mathf.PI / 180)) * .35f;
                hostY = transform.position.y + Mathf.Cos(angle * (Mathf.PI / 180)) * .35f;
                guardian.transform.position = new Vector2(hostX, hostY);
            }
            yield return new WaitForSeconds(.01f);
        }

    }
}
