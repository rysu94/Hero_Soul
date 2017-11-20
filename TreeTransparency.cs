using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTransparency : MonoBehaviour
{

    public SpriteRenderer tree;
    public Color treeAlpha;

	// Use this for initialization
	void Start ()
    {
        tree = GetComponent<SpriteRenderer>();
        treeAlpha = tree.color;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            //print("Enter");
            tree.color = new Color(1f, 1f, 1f, .5f);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //print("Exit");
            tree.color = new Color(1f, 1f, 1f, 1f);
            
        }
    }
}
