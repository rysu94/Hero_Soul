using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Treasure_Chest : MonoBehaviour
{

    public Animator chestAnimator;
    public AudioSource chestOpenNoise;

    public GameObject interactText;
    public GameObject interactPrefab;
    public GameObject treasureUI;
    public static bool treasureUIOpen = false;

    public float chestDistance;

    public GameObject treasureSlot;
    public GameObject treasureSlotPanel;
    public static List<GameObject> slotList = new List<GameObject>();

    public Animator takeAll;
    public AudioSource takeAllSound;

    public GameObject helpPanel;
    public bool textMade = false;

    public static int padX, padY;
    public bool padActive = false;
    public GameObject tooltip;

    public static int selectedIndex;
    public GameObject selectedSlot;

    public int mosaicID = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //Check if the Chest has been opened
        if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].isOpened)
        {
            chestAnimator.Play("Chest_Open_Idle");
        }
        switch(mosaicID)
        {
            default:
                break;
            case 1:
                if(Mosaic_Manager.korosClaimed)
                    LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].isOpened = true;       
                break;
        }

        /*
        //raycast to see if its above an item slot
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = Camera.main.transform.position.z;
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (Input.GetMouseButtonDown(0) && hit.collider != null && hit.collider.tag == "Treasure_TkAll")
        {
            takeAll.Play("DecipherButtonClick");
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            for (int i = 0; i < LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv.Length; i++)
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
        */

        //check proximity of chest and player
        chestDistance = Vector3.Distance(transform.position, TestCharController.player.transform.position);
        //print(chestDistance);
        if (chestDistance <= .35f && !LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].isOpened)
        {
            if(!textMade)
            {
                if (GameController.xbox360Enabled())
                {
                    interactText = Instantiate(Resources.Load("Prefabs/Interact_XBox"), new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity) as GameObject;
                    textMade = true;
                }
                else if(!GameController.xbox360Enabled())
                {
                    interactText = Instantiate(interactPrefab, new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity);
                    textMade = true;
                }
            }

            //On First open of the treasure chest
            if (InputManager.A_Button() && !LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].isOpened)
            {
                chestAnimator.Play("Open_Chest");
                chestOpenNoise.Play();
                LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].isOpened = true;
                CreateTreasure();
                /*
                GenerateTreasureSlots();
                UpdateTreasureInv();
                treasureUI.SetActive(true);
                treasureUIOpen = true;
                TestCharController.inTreasure = true;
                GameObject.Find("InventoryController").GetComponent<InventoryController>().OpenInv();
                helpPanel.SetActive(true);
                Destroy(interactText);
                padX = 0;
                padY = 0;
                padActive = true;
                StartCoroutine(PadBuffer());
                */
            }
            /*
            //Every subsequent opening after the first
            else if (InputManager.A_Button() && LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].isOpened && !treasureUIOpen)
            {
                if (slotList.Count == 0)
                {
                    GenerateTreasureSlots();
                }
                UpdateTreasureInv();
                treasureUI.SetActive(true);
                treasureUIOpen = true;
                TestCharController.inTreasure = true;
                GameObject.Find("InventoryController").GetComponent<InventoryController>().OpenInv();
                helpPanel.SetActive(true);
                Destroy(interactText);
                padActive = true;
                padX = 0;
                padY = 0;
                StartCoroutine(PadBuffer());
            }
            
            //Closing the chest UI
            else if ((InputManager.B_Button() || Input.GetKeyDown(KeyCode.F)) && LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].isOpened && treasureUIOpen)
            {
                treasureUI.SetActive(false);
                treasureUIOpen = false;

                GameObject.Find("InventoryController").GetComponent<InventoryController>().CloseInventory();
                helpPanel.SetActive(false);
                padActive = false;
                textMade = false;

                StartCoroutine(CloseRoutine());
            }
            */
        }
        else if (chestDistance > .35f)
        {
            Destroy(interactText);
            //treasureUI.SetActive(false);
            //treasureUIOpen = false;
            TestCharController.inTreasure = false;
            //helpPanel.SetActive(false);
            padActive = false;
            textMade = false;
        }

        //Controller

        if (GameController.xbox360Enabled() && TestCharController.inTreasure)
        {
            TakeAll();
            NavigateTreasure();
            TakeItem();
        }


    }

    void CreateTreasure()
    {
        for(int i = 0; i < LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv.Length; i++)
        {
            Item tempItem = LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv[i];
            if(tempItem.itemID > 0)
            {
                print(tempItem.itemIconName);
                GameObject tempObj = Instantiate(Resources.Load("Prefabs/Items/" + tempItem.itemIconName), transform.position, Quaternion.identity) as GameObject;
                tempObj.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f)).normalized * Random.Range(0, 1.5f);
            }                
        }

        switch(mosaicID)
        {
            default:
                break;
            case 1:
                Mosaic_Manager.korosClaimed = true;
                break;
        }
    }

    //Generate the Treasure Slots
    void GenerateTreasureSlots()
    {
        slotList.Clear();
        for (int i = 0; i < 18; i++)
        {
            GameObject tempObj = Instantiate(treasureSlot);
            tempObj.transform.SetParent(treasureSlotPanel.transform, false);
            tempObj.GetComponent<TreasureSlot>().slotNum = i;
            slotList.Add(tempObj);
        }
    }

    public void UpdateTreasureInv()
    {
        for (int i = 0; i < 18; i++)
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

    //Controller support

    //Take All Items
    public void TakeAll()
    {
        if (InputManager.Y_Button())
        {
            takeAll.Play("DecipherButtonClick");
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            for (int i = 0; i < LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv.Length; i++)
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
    }

    public void NavigateTreasure()
    {
        if (InputManager.MainHorizontal() > .5f && !padActive)
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            padActive = true;
            padX++;
            if (padX > 5)
            {
                padX = 0;
            }
            StartCoroutine(PadBuffer());

        }
        else if (InputManager.MainHorizontal() < -.5f && !padActive)
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            padActive = true;
            padX--;
            if (padX < 0)
            {
                padX = 5;
            }
            StartCoroutine(PadBuffer());
        }
        else if (InputManager.MainVertical() > .5f && !padActive)
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            padActive = true;
            padY--;
            if (padY < 0)
            {
                padY = 2;
            }
            StartCoroutine(PadBuffer());
        }
        else if (InputManager.MainVertical() < -.5f && !padActive)
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            padActive = true;
            padY++;
            if (padY > 2)
            {
                padY = 0;
            }
            StartCoroutine(PadBuffer());
        }

        Destroy(selectedSlot);
        //print(padX + " " + padY);
        selectedIndex = (padY * 6) + padX;
        print(selectedIndex);
        selectedSlot = Instantiate(Resources.Load("Prefabs/Inventory/Slot_Select"), slotList[selectedIndex].transform) as GameObject;

        //TreasureTooltip();
    }

    IEnumerator PadBuffer()
    {
        yield return new WaitForSeconds(.25f);
        padActive = false;
    }

    public void TakeItem()
    {
        if (InputManager.A_Button() && !padActive)
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            //check to see if item is in player inv
            for (int j = 0; j < InventoryManager.playerInventory.Length; j++)
            {
                if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv[selectedIndex].itemID ==
                    InventoryManager.playerInventory[j].itemID)
                {
                    //Check if item is stackable
                    if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv[selectedIndex].stackable)
                    {
                        //Add the treasure item stack to the player item stack
                        InventoryManager.playerInventory[j].itemQuantity += LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv[selectedIndex].itemQuantity;
                        //Remove item from treasure inv
                        LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv[selectedIndex] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                    }
                }
                //If item not found or is not stackable put it in an empty slot
                else
                {
                    for (int k = 0; k < InventoryManager.playerInventory.Length; k++)
                    {
                        if (InventoryManager.playerInventory[k].itemID == 0 || InventoryManager.playerInventory[k] == null)
                        {
                            InventoryManager.playerInventory[k] = LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv[selectedIndex];
                            LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv[selectedIndex] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                        }
                    }
                }
            }
            UpdateTreasureInv();
            GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
        }
    }

    public void TreasureTooltip()
    {
        tooltip.gameObject.SetActive(true);
        tooltip.transform.position = slotList[selectedIndex].transform.position;
        tooltip.transform.position = new Vector3(tooltip.transform.position.x - 1.25f, tooltip.transform.position.y - .5f, tooltip.transform.position.z);

        //Find item name
        GameObject tempObj = tooltip.transform.Find("Item_Name").gameObject;
        Text tempText = tempObj.GetComponent<Text>();
        tempText.text = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().FindItem(slotList[selectedIndex].GetComponent<TreasureSlot>().itemID).itemName;
        //Find item desc
        tempObj = tooltip.transform.Find("Item_Desc").gameObject;
        tempText = tempObj.GetComponent<Text>();
        tempText.text = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().FindItem(slotList[selectedIndex].GetComponent<TreasureSlot>().itemID).itemDescription;
        //Find item type
        tempObj = tooltip.transform.Find("Item_Type").gameObject;
        tempText = tempObj.GetComponent<Text>();
        tempText.text = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().FindItem(slotList[selectedIndex].GetComponent<TreasureSlot>().itemID).itemType;
        //Find item sell
        tempObj = tooltip.transform.Find("Item_Sell1").gameObject;
        tempText = tempObj.GetComponent<Text>();
        tempText.text = "Sells for ";
        tempObj = tooltip.transform.Find("Item_Sell2").gameObject;
        tempText = tempObj.GetComponent<Text>();
        if (GameObject.Find("InventoryController").GetComponent<ItemDatabase>().FindItem(slotList[selectedIndex].GetComponent<TreasureSlot>().itemID).stackable)
        {
            tempText.text = (GameObject.Find("InventoryController").GetComponent<ItemDatabase>().FindItem(slotList[selectedIndex].GetComponent<TreasureSlot>().itemID).itemSellCost * GameObject.Find("InventoryController").GetComponent<ItemDatabase>().FindItem(slotList[selectedIndex].GetComponent<TreasureSlot>().itemID).itemQuantity).ToString() + " gold";
        }
        else if (!GameObject.Find("InventoryController").GetComponent<ItemDatabase>().FindItem(slotList[selectedIndex].GetComponent<TreasureSlot>().itemID).stackable)
        {
            tempText.text = (GameObject.Find("InventoryController").GetComponent<ItemDatabase>().FindItem(slotList[selectedIndex].GetComponent<TreasureSlot>().itemID).itemSellCost).ToString() + " gold";
        }
    }

    IEnumerator CloseRoutine()
    {
        yield return new WaitForSeconds(.5f);
        TestCharController.inTreasure = false;
        TestCharController.inDialogue = false;
    }
}
