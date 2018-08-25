using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tempest : MonoBehaviour
{
    int tempCount = 0;
    Vector2 zapLoc;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(tempestRoutine());
        
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator tempestRoutine()
    {
        ArcanaController.isCasting = true;
        Cursor.SetCursor((Texture2D)Resources.Load("Crosshair"), Vector2.zero, CursorMode.Auto);
        while (tempCount < 40)
        {
            if(GameController.xbox360Enabled())
            {
                zapLoc = Move_Cross.crossPos;
            }
            else
            {
                zapLoc = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            
            GetComponent<Rigidbody2D>().velocity = ((zapLoc - (Vector2)transform.position).normalized) * 1f;
            Instantiate(Resources.Load("Prefabs/SpellFX/Zap"), transform.position, Quaternion.identity);
            tempCount++;
            yield return new WaitForSeconds(.25f);
        }
        ArcanaController.isCasting = false;
        Cursor.SetCursor((Texture2D)Resources.Load("Cursor"), Vector2.zero, CursorMode.Auto);
        Destroy(gameObject);
    }
}
