using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arcana_Book : MonoBehaviour
{
    public float bookDistance;

    public GameObject bookPanel;
    public bool panelOpen = false;

    public GameObject interactText;
    public GameObject interactPrefab;

    public GameObject helpPanel;
    public bool textMade = false;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //check distance of book pedestal to the player
        bookDistance = Vector3.Distance(transform.position, TestCharController.player.transform.position);

        if(bookDistance < .35 && !textMade)
        {
            if (GameController.xbox360Enabled())
            {
                interactText = Instantiate(Resources.Load("Prefabs/Interact_XBox"), new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity) as GameObject;
            }
            else if(!GameController.xbox360Enabled())
            {
                print("DS");
                interactText = Instantiate(interactPrefab, new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity);
            }
            textMade = true;
        }

        else if(bookDistance > .35)
        {
            Destroy(interactText);
            bookPanel.SetActive(false);
            panelOpen = false;
            helpPanel.SetActive(false);
            textMade = false;
        }


        //Interaction actions
        if (bookDistance < .35 && InputManager.A_Button() && !panelOpen)
        {
            Destroy(interactText);
            bookPanel.SetActive(true);
            panelOpen = true;
            helpPanel.SetActive(true);
            TestCharController.inDialogue = true;
        }
        else if (bookDistance < .35 && (InputManager.B_Button() || Input.GetKeyDown(KeyCode.F)) && panelOpen)
        {
            Destroy(interactText);
            bookPanel.SetActive(false);
            panelOpen = false;
            helpPanel.SetActive(false);
            StartCoroutine(CloseRoutine());
        }
    }
    IEnumerator CloseRoutine()
    {
        yield return new WaitForSeconds(.5f);
        TestCharController.inDialogue = false;
    }
}
