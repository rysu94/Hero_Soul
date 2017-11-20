using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMove : MonoBehaviour
{

    public int direction;
    public bool isTriggered = false;


    // Use this for initialization
    void Start()
    {
        StartCoroutine(MoveRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator MoveRoutine()
    {
        for(int i = 0; i < 10; i++)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + .018f);
            yield return new WaitForSeconds(.05f);
        }
        while (direction > 0)
        {
            //north
            if (direction == 1)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 3);
            }
            //south
            else if (direction == 2)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, -3);
            }
            //east
            else if (direction == 3)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(3, 0);
            }
            //west
            else if (direction == 4)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-3, 0);
            }
            yield return new WaitForSeconds(.1f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "Enemy" && !isTriggered)
        {
            DamageManager.spellBase = 25;
            isTriggered = true;
            Destroy(this.gameObject);
        }
    }
}
