using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magma : MonoBehaviour {

    public bool isTriggered = false;

    float multiplier;

    // Use this for initialization
    void Start()
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
        else if (TestCharController.player.GetComponent<TestCharController>().west)
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
        StartCoroutine(EnlargeRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        float x = transform.right.x * 2;
        float y = transform.right.y * 2;
        GetComponent<Rigidbody2D>().velocity = new Vector2(x, y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.gameObject.tag == "Wall")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Explode_1"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "Enemy" && !isTriggered)
        {
            DamageManager.spellBase = (int)(25 * multiplier);
            isTriggered = true;
            Instantiate(Resources.Load("Prefabs/SpellFX/Explode_1"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    IEnumerator EnlargeRoutine()
    {
        float sizeX = .85f;
        float sizeY = .6f;
        multiplier = 1;
        for(int i = 0; i < 25; i++)
        {
            transform.localScale = new Vector3(sizeX += .071f, sizeY += .05f, 1);
            multiplier += .05f;
            yield return new WaitForSeconds(.1f);
        }
        
    }
}
