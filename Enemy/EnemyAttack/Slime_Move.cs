using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Move : MonoBehaviour
{
    public bool up;

    public GameObject slime1;
    public GameObject slime2;
    public GameObject slime3;
    public GameObject slime4;
    public GameObject slime5;


	// Use this for initialization
	void Start ()
    {
		if(!up)
        {
            CheckPosition();
            Vector2 dir = GetAngle(90);
            slime1.GetComponent<Rigidbody2D>().velocity = dir * 1.5f;
            dir = GetAngle(120);
            slime2.GetComponent<Rigidbody2D>().velocity = dir * 1.5f;
            dir = GetAngle(60);
            slime3.GetComponent<Rigidbody2D>().velocity = dir * 1.5f;
            dir = GetAngle(150);
            slime4.GetComponent<Rigidbody2D>().velocity = dir * 1.5f;
            dir = GetAngle(30);
            slime5.GetComponent<Rigidbody2D>().velocity = dir * 1.5f;
        }
        else if(up)
        {
            CheckPosition();
            Vector2 dir = GetAngle(270);
            slime1.GetComponent<Rigidbody2D>().velocity = dir * 1.5f;
            dir = GetAngle(240);
            slime2.GetComponent<Rigidbody2D>().velocity = dir * 1.5f;
            dir = GetAngle(300);
            slime3.GetComponent<Rigidbody2D>().velocity = dir * 1.5f;
            dir = GetAngle(210);
            slime4.GetComponent<Rigidbody2D>().velocity = dir * 1.5f;
            dir = GetAngle(330);
            slime5.GetComponent<Rigidbody2D>().velocity = dir * 1.5f;
        }

        //Check if the slime quest is active
        for(int i = 0; i < QuestManager.activeSideQuests.Count; i++)
        {
            if(QuestManager.activeSideQuests[i].questName == "Slime Hunter I")
            {
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Quest_Listener"))
                {
                    if (enemy.GetComponent<Slime_Hunter_Listener>())
                    {
                        enemy.GetComponent<Slime_Hunter_Listener>().UpdateSlime();
                    }
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //Check Slime posiiton
    void CheckPosition()
    {
        //slime 1
        if(slime1.transform.position.x < -2.25)
        {
            slime1.transform.position = new Vector2(-2f, slime1.transform.position.y);
        }
        else if(slime1.transform.position.x > 2.15)
        {
            slime1.transform.position = new Vector2(1.95f, slime1.transform.position.y);
        }
        //slime 2
        if (slime2.transform.position.x < -2.25)
        {
            slime2.transform.position = new Vector2(-2f, slime2.transform.position.y);
        }
        else if (slime2.transform.position.x > 2.15)
        {
            slime2.transform.position = new Vector2(1.95f, slime2.transform.position.y);
        }
        //slime 3
        if (slime3.transform.position.x < -2.25)
        {
            slime3.transform.position = new Vector2(-2f, slime3.transform.position.y);
        }
        else if (slime3.transform.position.x > 2.15)
        {
            slime3.transform.position = new Vector2(1.95f, slime3.transform.position.y);
        }
        //slime 4
        if (slime4.transform.position.x < -2.25)
        {
            slime4.transform.position = new Vector2(-2f, slime4.transform.position.y);
        }
        else if (slime4.transform.position.x > 2.15)
        {
            slime4.transform.position = new Vector2(1.95f, slime4.transform.position.y);
        }
        //slime 5
        if (slime5.transform.position.x < -2.25)
        {
            slime5.transform.position = new Vector2(-2f, slime5.transform.position.y);
        }
        else if (slime5.transform.position.x > 2.15)
        {
            slime5.transform.position = new Vector2(1.95f, slime5.transform.position.y);
        }
    }


    Vector2 GetAngle(float angle)
    {
        Vector2 newAngle = new Vector2(0, 0);
        float radians = angle * (Mathf.PI / 180);
        newAngle = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        return newAngle;
    }
}
