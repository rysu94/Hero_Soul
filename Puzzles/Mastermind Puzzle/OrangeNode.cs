using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OrangeNode : MonoBehaviour
{
    public GameObject interactText;
    public bool textMade = false;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        float distance = Vector3.Distance(transform.position, TestCharController.player.transform.position);
        //print(distance);
        if (distance < .75 && !textMade)
        {
            if (GameController.xbox360Enabled())
            {
                interactText = Instantiate(Resources.Load("Prefabs/Interact_XBox") as GameObject, new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity);
            }
            else
            {
                interactText = Instantiate(Resources.Load("Prefabs/Interact") as GameObject, new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity);
            }
            textMade = true;
        }
        else if (distance >= .75)
        {
            Destroy(interactText);
            textMade = false;
        }

        //If interact is pressed
        if (distance < .75 && (InputManager.A_Button() || Input.GetKeyDown(KeyCode.F)))
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            AddOrange();
        }
    }

    void AddOrange()
    {
        GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
        tempObj.GetComponent<Text>().text = "Obtained an Orange";
        Mastermind_Puzzle.heldFruit = 2;
    }

}
