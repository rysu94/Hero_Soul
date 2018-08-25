using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WeaponRack : MonoBehaviour
{
    public GameObject interactText;
    public GameObject interactPrefab;
    public bool textMade = false;

    public GameObject wornSpear;
    public GameObject wornSword;
    public GameObject wornBow;
    public GameObject wornDaggers;

    public GameObject tutMessage;
    public Text tutText;

    public static bool tut1 = false;
    public static bool tut2 = false;

    public static bool used = false;

    public GameObject arrowRight;
    public GameObject finger;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float distance = Vector3.Distance(transform.localPosition, TestCharController.player.transform.position);
        //print(distance);
        if(Byron2.phase1 &&  !used)
        {
            if (distance < .5 && !textMade)
            {
                interactText = Instantiate(interactPrefab, new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity);
                textMade = true;
            }
            else if (distance >= .5)
            {
                Destroy(interactText);
                textMade = false;
            }

            if (distance < .5 && Input.GetKeyDown(KeyCode.F) && !TestCharController.inDialogue)
            {
                Destroy(interactText);
                textMade = false;
                Instantiate(wornSpear, TestCharController.player.transform.position, Quaternion.identity);
                if(!TutorialDatabase.tut2)
                {
                    tutText.text = "Item Basics:\nWhenever you pick up an item, you can press [Tab] to open you inventory.";
                    tutMessage.SetActive(true);
                    TutorialDatabase.tut2 = true;
                }

                tut1 = true;
                used = true;
            }
        }
        if(Input.GetKeyDown(KeyCode.Tab) && !TutorialDatabase.tut3 && TutorialDatabase.tut2)
        {
            TutorialDatabase.tut3 = true;
            tutMessage.GetComponent<LifespanHide>().active = false;
            tutMessage.SetActive(false);
            tutText.text = "Equipment Basics:\nTo open your equipment menu, select the \"Equip\" tab on the left of the inventory menu.";
            tutMessage.SetActive(true);
            tut1 = false;

            arrowRight.SetActive(true);
            //finger.SetActive(true);
        }



    }
}
