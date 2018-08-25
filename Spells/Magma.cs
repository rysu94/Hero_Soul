using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magma : MonoBehaviour {

    public bool isTriggered = false;
    float sizeX = .55f;
    float sizeY = .55f;

    float multiplier;

    public Vector2 vel;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(EnlargeRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        float x = transform.right.x * 2;
        float y = transform.right.y * 2;
        GetComponent<Rigidbody2D>().velocity = vel;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.gameObject.tag == "Wall" && !isTriggered)
        {
            isTriggered = true;
            GameObject tempObj;
            tempObj = Instantiate(Resources.Load("Prefabs/SpellFX/Explode_1"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity) as GameObject;
            tempObj.transform.localScale = new Vector3(sizeX/2f, sizeY/2f, 1);
            tempObj = Instantiate(Resources.Load("Prefabs/SpellFX/Lava"), new Vector2(transform.position.x, transform.position.y-.05f), Quaternion.identity) as GameObject;
            tempObj.transform.localScale = new Vector3(sizeX/1.5f, sizeY/3f, 1);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "Enemy" && !isTriggered)
        {
            DamageManager.spellBase = (int)(25 * multiplier);
            int tempInt = DamageManager.MagicDamage(other.gameObject, (int)(20 * multiplier));
            other.gameObject.GetComponent<Monster>().DamageMonster(tempInt);
            isTriggered = true;
            GameObject tempObj;
            tempObj = Instantiate(Resources.Load("Prefabs/SpellFX/Explode_1"), new Vector2(other.transform.position.x, other.transform.position.y), Quaternion.identity) as GameObject;
            tempObj.transform.localScale = new Vector3(sizeX, sizeY, 1);
            tempObj = Instantiate(Resources.Load("Prefabs/SpellFX/Lava"), new Vector2(other.transform.position.x, other.transform.position.y-.05f), Quaternion.identity) as GameObject;
            tempObj.transform.localScale = new Vector3(sizeX, sizeY/2f, 1);
            Destroy(this.gameObject);
        }
    }

    IEnumerator EnlargeRoutine()
    {
        multiplier = 1;
        for(int i = 0; i < 25; i++)
        {
            transform.localScale = new Vector3(sizeX += .1f, sizeY += .1f, 1);
            multiplier += .25f;
            yield return new WaitForSeconds(.1f);
        }       
    }
}
