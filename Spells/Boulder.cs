using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{

    public bool isTriggered = false;

	// Use this for initialization
	void Start ()
    {
        if (TestCharController.player.GetComponent<TestCharController>().north)
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if (TestCharController.player.GetComponent<TestCharController>().south)
        {
            transform.eulerAngles = new Vector3(0, 0, 270);
        }
        else if (TestCharController.player.GetComponent<TestCharController>().east)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(TestCharController.player.GetComponent<TestCharController>().west)
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        float x = transform.right.x * 2;
        float y = transform.right.y * 2;
        GetComponent<Rigidbody2D>().velocity = new Vector2(x, y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "Enemy" && !isTriggered)
        {
            DamageManager.spellBase = 35;
            isTriggered = true;
            Destroy(this.gameObject);
        }
    }
}
