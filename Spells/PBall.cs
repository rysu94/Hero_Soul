using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBall : MonoBehaviour {

    bool isTriggered = false;
    float sizeX = 1f;
    float sizeY = 1f;
    float multiplier = 1;

    // Use this for initialization
    void Start()
    {
        transform.rotation = Quaternion.identity;
        StartCoroutine(EnlargeRoutine());
        StartCoroutine(ExpireRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.xbox360Enabled())
        {
            Vector2 vel = Move_Cross.crossPos - (Vector2)transform.position;
            GetComponent<Rigidbody2D>().velocity = vel.normalized * .25f;
        }
        else
        {
            Vector2 vel = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            GetComponent<Rigidbody2D>().velocity = vel.normalized * .25f;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.gameObject.tag == "Wall" && !isTriggered)
        {
            isTriggered = true;
            GameObject tempObj;
            tempObj = Instantiate(Resources.Load("Prefabs/SpellFX/Thunder_Explode"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity) as GameObject;
            tempObj.transform.localScale = new Vector3(sizeX, sizeY, 1);
            Cursor.SetCursor((Texture2D)Resources.Load("Cursor"), Vector2.zero, CursorMode.Auto);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "Enemy" && !isTriggered)
        {
            int tempInt = DamageManager.MagicDamage(other.gameObject, (int)(30 * multiplier));
            other.gameObject.GetComponent<Monster>().DamageMonster(tempInt);
            isTriggered = true;
            GameObject tempObj;
            tempObj = Instantiate(Resources.Load("Prefabs/SpellFX/Thunder_Explode"), new Vector2(other.transform.position.x, other.transform.position.y), Quaternion.identity) as GameObject;
            tempObj.transform.localScale = new Vector3(sizeX, sizeY, 1);
            Cursor.SetCursor((Texture2D)Resources.Load("Cursor"), Vector2.zero, CursorMode.Auto);
            Destroy(this.gameObject);
        }
    }

    IEnumerator EnlargeRoutine()
    {
        multiplier = 1;
        for (int i = 0; i < 25; i++)
        {
            if (multiplier <= 3)
            {
                transform.localScale = new Vector3(sizeX += .15f, sizeY += .15f, 1);
                multiplier += .25f;
            }
            yield return new WaitForSeconds(.5f);
        }
    }

    IEnumerator ExpireRoutine()
    {
        ArcanaController.isCasting = true;
        Cursor.SetCursor((Texture2D)Resources.Load("Crosshair"), Vector2.zero, CursorMode.Auto);
        yield return new WaitForSeconds(10f);
        GameObject tempObj;
        tempObj = Instantiate(Resources.Load("Prefabs/SpellFX/Thunder_Explode"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity) as GameObject;
        tempObj.transform.localScale = new Vector3(sizeX, sizeY, 1);
        Cursor.SetCursor((Texture2D)Resources.Load("Cursor"), Vector2.zero, CursorMode.Auto);
        ArcanaController.isCasting = false;
        Destroy(this.gameObject);
    }
}
