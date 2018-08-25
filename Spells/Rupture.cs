using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rupture : MonoBehaviour
{
    public List<GameObject> rocks = new List<GameObject>();
    public GameObject parent;
    Vector3 mousePos;

    // Use this for initialization
    void Start ()
    {
        GameObject tempObj = Instantiate(Resources.Load("Sound/One_Shot_Audio") as GameObject, new Vector2(0, 0), Quaternion.identity);
        tempObj.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sound/Rupture_Summon");
        tempObj.GetComponent<AudioSource>().Play();
        StartCoroutine(RuptureRoutine());
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(GameController.xbox360Enabled())
        {
            mousePos = Move_Cross.crossPos;
        }
        else
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        

        if (TestCharController.player.GetComponent<TestCharController>().north)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (TestCharController.player.GetComponent<TestCharController>().east)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (TestCharController.player.GetComponent<TestCharController>().west)
        {
            transform.rotation = Quaternion.Euler(0, 0, 270);
        }
        else if (TestCharController.player.GetComponent<TestCharController>().south)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        for (int i = 0; i < rocks.Count; i++)
        {
            Vector2 rockVec = mousePos - rocks[i].transform.position;
            float angle = 0;
            if (TestCharController.player.GetComponent<TestCharController>().north)
            {
                angle = (Mathf.Atan(rockVec.x / rockVec.y)) * (180 / Mathf.PI) * -1;
                if(rockVec.y < 0)
                {
                    angle += 180;
                }
            }
            else if (TestCharController.player.GetComponent<TestCharController>().east)
            {
                angle = (Mathf.Atan(rockVec.y / rockVec.x)) * (180 / Mathf.PI);
                if (rockVec.x < 0)
                {
                    angle += 180;
                }
            }
            else if (TestCharController.player.GetComponent<TestCharController>().west)
            {
                angle = (Mathf.Atan(rockVec.y / rockVec.x)) * (180 / Mathf.PI);
                if (rockVec.x > 0)
                {
                    angle += 180;
                }
            }
            else if (TestCharController.player.GetComponent<TestCharController>().south)
            {
                angle = (Mathf.Atan(rockVec.x / rockVec.y)) * (180 / Mathf.PI) * -1;
                if (rockVec.y > 0)
                {
                    angle += 180;
                }
            }

            rocks[i].transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }

    IEnumerator RuptureRoutine()
    {
        Cursor.SetCursor((Texture2D)Resources.Load("Crosshair"), Vector2.zero, CursorMode.Auto);
        ArcanaController.isCasting = true;
        yield return new WaitForSeconds(3f);
        for(int i = 0; i < 5; i++)
        {
            GetComponent<AudioSource>().Play();
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rocks[0].transform.parent = parent.transform;
            rocks[0].AddComponent<Rigidbody2D>();
            rocks[0].AddComponent<StoneMove>().speed = 3;
            rocks.Remove(rocks[0]);
            yield return new WaitForSeconds(.5f);
        }
        ArcanaController.isCasting = false;
        Cursor.SetCursor((Texture2D)Resources.Load("Cursor"), Vector2.zero, CursorMode.Auto);
    }
}
