using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Treasure_Chest : MonoBehaviour
{

    public Animator chestAnimator;
    public AudioSource chestOpenNoise;

    public GameObject interactPrefab;
    public GameObject treasureUI;
    public bool treasureUIOpen = false;

    public float chestDistance;

    public GameObject treasureSlot;
    public GameObject treasureSlotPanel;
    public List<GameObject> slotList = new List<GameObject>();

    public Animator takeAll;
    public AudioSource takeAllSound;


	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {

        //Check if the Chest has been opened
        if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].isOpened)
        {
            chestAnimator.Play("Chest_Open_Idle");
        }

        //raycast to see if its above an item slot
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = Camera.main.transform.position.z;
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if(Input.GetMouseButtonDown(0) && hit.collider != null && hit.collider.tag == "Treasure_TkAll")
        {
            takeAll.Play("DecipherButtonClick");
            takeAllSound.Play();
            for(int i = 0; i < LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv.Length; i++)
            {
                if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv[i] != null)
                {
                    //check to see if item is in player inv
                    for (int j = 0; j < InventoryManager.playerInventory.Length; j++)
                    {
                        if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv[i].itemID ==
                            InventoryManager.playerInventory[j].itemID)
                        {
                            //Check if item is stackable
                            if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv[i].stackable)
                            {
                                //Add the treasure item stack to the player item stack
                                InventoryManager.playerInventory[j].itemQuantity += LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv[i].itemQuantity;
                                //Remove item from treasure inv
                                LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv[i] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                            }
                        }
                        //If item not found or is not stackable put it in an empty slot
                        else
                        {
                            for (int k = 0; k < InventoryManager.playerInventory.Length; k++)
                            {
                                if (InventoryManager.playerInventory[k].itemID == 0 || InventoryManager.playerInventory[k] == null)
                                {
                                    InventoryManager.playerInventory[k] = LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv[i];
                                    LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv[i] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                                }
                            }
                        }
                    }
                }
                //Update Treasure Inv
                UpdateTreasureInv();
                //Update player inv
                GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
            }
            
        }


        //check proximity of chest and player
        chestDistance = Vector3.Distance(transform.position, TestCharController.player.transform.position);
        //print(chestDistance);
        if(chestDistance <= .35f)
        {
            interactPrefab.SetActive(true);
            //On First open of the treasure chest
            if(Input.GetKeyDown(KeyCode.F) && !LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].isOpened && !treasureUIOpen)
            {
                chestAnimator.Play("Open_Chest");
                chestOpenNoise.Play();
                LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].isOpened = true;
                GenerateTreasureSlots();
                UpdateTreasureInv();
                treasureUI.SetActive(true);
                treasureUIOpen = true;
                TestCharController.inTreasure = true;
                GameObject.Find("InventoryController").GetComponent<InventoryController>().OpenInv();
            }
            //Every subsequent opening after the first
            else if (Input.GetKeyDown(KeyCode.F) && LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].isOpened && !treasureUIOpen)
            {
                if(slotList.Count == 0)
                {
                    GenerateTreasureSlots();
                }
                UpdateTreasureInv();
                treasureUI.SetActive(true);
                treasureUIOpen = true;
                TestCharController.inTreasure = true;
                GameObject.Find("InventoryController").GetComponent<InventoryController>().OpenInv();
            }
            //Closing the chest UI
            else if(Input.GetKeyDown(KeyCode.F) && LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].isOpened && treasureUIOpen)
            {
                treasureUI.SetActive(false);
                treasureUIOpen = false;
                TestCharController.inTreasure = false;
                GameObject.Find("InventoryController").GetComponent<InventoryController>().CloseInventory();
            }
        }
        else if(chestDistance > .35f)
        {
            interactPrefab.SetActive(false);
            treasureUI.SetActive(false);
            treasureUIOpen = false;
            TestCharController.inTreasure = false;
        }
	}

    //Generate the Treasure Slots
    void GenerateTreasureSlots()
    {
        for(int i = 0; i < 18; i++)
        {
            GameObject tempObj = Instantiate(treasureSlot);
            tempObj.transform.SetParent(treasureSlotPanel.transform, false);
            tempObj.GetComponent<TreasureSlot>().slotNum = i;
            slotList.Add(tempObj);
        }
    }

    public void UpdateTreasureInv()
    {
        for(int i = 0; i < 18; i++)
        {
            //Access the room treasure inv
            Item tempItem = LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv[i];

            //Change the Slot Icon
            GameObject tempObj = slotList[i].transform.Find("Item_IMG").gameObject;
            Image tempInvImage = tempObj.GetComponent<Image>();
            Sprite tempSprite = Resources.Load<Sprite>("Item/" + tempItem.itemIconName);
            tempInvImage.sprite = tempSprite;

            tempObj = slotList[i].transform.Find("Item_Num").gameObject;
            Text tempText = tempObj.GetComponent<Text>();
            if (tempItem.stackable)
            {
                tempText.text = tempItem.itemQuantity.ToString();
            }
            else
            {
                tempText.text = "";
            }

            slotList[i].GetComponent<TreasureSlot>().slotImage = tempSprite;
            slotList[i].GetComponent<TreasureSlot>().itemID = tempItem.itemID;
            slotList[i].GetComponent<TreasureSlot>().itemQuantity = tempItem.itemQuantity;
        }
        
    }
}
