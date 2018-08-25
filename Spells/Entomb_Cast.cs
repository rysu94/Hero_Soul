using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Entomb_Cast : MonoBehaviour
{

    public float speed = 0;
    public bool isTriggered = false;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        float x = -transform.up.x * speed;
        float y = -transform.up.y * speed;
        GetComponent<Rigidbody2D>().velocity = new Vector2(x, y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.gameObject.tag == "Wall")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Ice_Break"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Enemy" && !isTriggered)
        {
            isTriggered = true;
            Instantiate(Resources.Load("Prefabs/SpellFX/Ice_Break"), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            if (other.gameObject.name != "Slime_Boss")
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Entomb_Prison"), other.gameObject.transform);   
            }
            else
            {
                GameObject tempObj = Instantiate(Resources.Load("Prefabs/SpellText"), other.gameObject.transform.position, Quaternion.identity) as GameObject;
                tempObj.transform.SetParent(GameObject.Find("HUD").transform, false);
                RectTransform tempRect = tempObj.GetComponent<RectTransform>();
                tempObj.GetComponent<Text>().text = "Resist";
            }
            Destroy(gameObject);

        }
    }
}
