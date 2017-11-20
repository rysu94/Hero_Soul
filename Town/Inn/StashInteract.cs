using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StashInteract : MonoBehaviour
{

    //The Canvas object prefab for the interact text
    public GameObject interactTextPrefab;
    public GameObject interactText;
    public bool textMade = false;

    //stash hud element
    public GameObject stash;
    public GameObject stashPanel;

    //Inventory slot prefab
    public GameObject invSlot;

    public List<GameObject> stashList = new List<GameObject>();

    // Use this for initialization
    void Start ()
    {
        //Generate the slots
        for (int i = 0; i < InventoryManager.playerStash.Length; i++)
        {
            stashList.Add(Instantiate(invSlot));
            stashList[i].transform.SetParent(stashPanel.transform, false);
            stashList[i].GetComponent<StashSlot>().slotNumber = i;
        }
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        float distance = Vector3.Distance(transform.position, TestCharController.player.transform.position);
        //print(distance);
        if (distance < .35 && !textMade)
        {
            interactText = Instantiate(interactTextPrefab, new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity);
            textMade = true;
        }
        else if (distance >= .35)
        {
            stash.SetActive(false);
            Destroy(interactText);
            textMade = false;
        }

        //StashProcessing
        if (distance < .35 && Input.GetKeyDown(KeyCode.F))
        {
            stash.SetActive(true);
            GameObject.Find("PlayerStash").GetComponent<StashInteract>().UpdateStash();
            GameObject.Find("InventoryController").GetComponent<InventoryController>().OpenInv();
        }
    }

    public void UpdateStash()
    {
        for (int i = 0; i < InventoryManager.playerStash.Length; i++)
        {
            GameObject tempObj = stashList[i].transform.Find("Item_IMG").gameObject;
            Image tempInvImage = tempObj.GetComponent<Image>();
            Sprite tempSprite = Resources.Load<Sprite>("Item/" + InventoryManager.playerStash[i].itemIconName);
            tempInvImage.sprite = tempSprite;
            stashList[i].GetComponent<StashSlot>().slotImage = tempSprite;

            stashList[i].GetComponent<StashSlot>().itemID = InventoryManager.playerStash[i].itemID;

            GameObject tempTextObj = stashList[i].transform.Find("Item_Num").gameObject;
            Text tempText = tempTextObj.GetComponent<Text>();

            //Find out how many of something player has
            int tempInt = InventoryManager.playerStash[i].itemQuantity;
            if (tempInt > 0 && InventoryManager.playerStash[i].stackable)
            {
                tempText.text = tempInt.ToString();
                stashList[i].GetComponent<StashSlot>().itemAmount = tempInt;
            }
            else if (tempInt > 0 && !InventoryManager.playerStash[i].stackable)
            {
                tempText.text = "";
                stashList[i].GetComponent<StashSlot>().itemAmount = tempInt;
            }
            else if (tempInt == 0)
            {
                tempText.text = "";
                stashList[i].GetComponent<StashSlot>().itemAmount = 0;
            }
            else if (tempInt < 0)
            {
                tempText.text = "ERROR";
                stashList[i].GetComponent<StashSlot>().itemAmount = -1;
            }

            //item type
            stashList[i].GetComponent<StashSlot>().itemType = InventoryManager.playerStash[i].itemType;
        }
    }
}
