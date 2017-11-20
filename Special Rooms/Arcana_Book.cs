using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arcana_Book : MonoBehaviour
{
    public float bookDistance;

    public GameObject bookPanel;
    public bool panelOpen = false;

    public GameObject interactPrefab;



	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //check distance of book pedestal to the player
        bookDistance = Vector3.Distance(transform.position, TestCharController.player.transform.position);

        if(bookDistance < .35)
        {
            interactPrefab.SetActive(true);

            //Interaction actions
            if (Input.GetKeyDown(KeyCode.F) && !panelOpen)
            {
                bookPanel.SetActive(true);
                panelOpen = true;
                TestCharController.inTreasure = true;

            }
            else if(Input.GetKeyDown(KeyCode.F) && panelOpen)
            {
                bookPanel.SetActive(false);
                panelOpen = false;
                TestCharController.inTreasure = false;
            }
        }

        else if(bookDistance > .35)
        {
            interactPrefab.SetActive(false);
            bookPanel.SetActive(false);
            panelOpen = false;
            TestCharController.inTreasure = false;
        }
    }
}
