using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC: MonoBehaviour
{
    public bool move;

    public string name;

    //1 up 2 down 3 left 4 right
    public int defaultPos;

    public string NPC_ID;

    public GameObject dialogue;
    public GameObject dialogueImg;

    public GameObject interactText;
    public GameObject interactPrefab;
    public bool textMade = false;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    public void TurnPlayer()
    {
        Vector2 playerVec = transform.position - TestCharController.player.transform.position;
        float angle = Vector2.Angle(new Vector2(-1, 0), playerVec);
        
        if(playerVec.x > 0 && playerVec.y > 0)
        {
            angle = 360 - angle;
        }
        if(playerVec.x< 0 && playerVec.y> 0)
        {
            angle = 360 - angle;
        }


        if (angle <= 45 || angle >= 315)
        {
            GetComponent<Animator>().Play("M" + NPC_ID + "_NPC_Right_Idle");
        }
        else if(angle > 45 && angle< 135)
        {
            GetComponent<Animator>().Play("M" + NPC_ID + "_NPC_Up_Idle");
        }
        else if(angle >= 135 && angle <= 225)
        {
            GetComponent<Animator>().Play("M" + NPC_ID + "_NPC_Left_Idle");
        }
        else if (angle > 225 && angle< 315)
        {
            GetComponent<Animator>().Play("M" + NPC_ID + "_NPC_Down_Idle");
        }
        
    }

    public void ReturnPlayer()
    {
        if (defaultPos == 1)
        {
            GetComponent<Animator>().Play("M" + NPC_ID + "_NPC_Up_Idle");
        }
        else if (defaultPos == 2)
        {
            GetComponent<Animator>().Play("M" + NPC_ID + "_NPC_Down_Idle");
        }
        else if (defaultPos == 3)
        {
            GetComponent<Animator>().Play("M" + NPC_ID + "_NPC_Left_Idle");
        }
        else if (defaultPos == 4)
        {
            GetComponent<Animator>().Play("M" + NPC_ID + "_NPC_Right_Idle");
        }
    }
}
