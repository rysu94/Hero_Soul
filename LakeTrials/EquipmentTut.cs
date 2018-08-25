using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EquipmentTut : MonoBehaviour {

    public GameObject tutMessage;
    public Text tutText;

    public GameObject finger;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(InventoryManager.playerEquipment[0].itemID != 0)
        {
            finger.SetActive(false);
        }
	}

    public void EquipTut()
    {
        if (!TutorialDatabase.tut2_A && TutorialDatabase.tut2)
        {
            finger.SetActive(true);
            TutorialDatabase.tut2_A = true;
            tutMessage.GetComponent<LifespanHide>().active = false;
            tutMessage.SetActive(false);
            tutText.text = "Equip Basics:\nDrag and drop items in their respective slots to equip them to your character.";
            tutMessage.SetActive(true);
        }
    }
}
