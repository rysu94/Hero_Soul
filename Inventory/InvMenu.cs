using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InvMenu : MonoBehaviour
{
    public GameObject destroyPanel;
    public Text destroyTextNum;
    public int numDestroyed;

    public GameObject splitPanel;
    public Text splitTextNum;
    public int numSplit;

    public int slotNumber;
    public Sprite slotImage;
    public int itemAmount;
    public int itemID;
    public string itemType;

    public static bool inMenu = false;

    public AudioSource noise;
    public AudioSource error;

    public Text useText;
    public Text splitText;
    public Text destroyText;                                               

    public static bool choiceHover = false;

    bool padBuffer = false;

    //1 = Equip  2 = Use  3 = Inspect
    public int optionID = 0;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = Camera.main.transform.position.z;
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 100, 1<<8);

        //if(hit.collider != null)
          //print(hit.collider.name);

        //Check if item can be used
        if(hit.collider != null && hit.collider.tag == "Inv_Menu_Use" && !inMenu)
        {
            choiceHover = true;

            useText.color = new Color(255, 255, 0);
            splitText.color = new Color(255, 255, 255);
            destroyText.color = new Color(255, 255, 255);
            if (optionID == 1 && Input.GetMouseButtonDown(0))
            {
                InventoryController.equip.SetActive(true);
                InventoryController.equipToggle = true;
                noise.Play();
                switch(itemType)
                {
                    default:
                        break;
                    case "Weapon":
                        //Check if there is a weapon equipped
                        if(InventoryManager.playerEquipment[0].itemID == 0)
                        {
                            InventoryManager.playerEquipment[0] = InventoryManager.playerInventory[slotNumber];
                            InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                            GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                            GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                        }
                        else if(InventoryManager.playerEquipment[0].itemID != 0)
                        {
                            Item tempItem = InventoryManager.playerEquipment[0];
                            InventoryManager.playerEquipment[0] = InventoryManager.playerInventory[slotNumber];
                            InventoryManager.playerInventory[slotNumber] = tempItem;
                            GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                            GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                        }
                        break;

                    case "Head":
                        //Check if there is a head equipped
                        if (InventoryManager.playerEquipment[1].itemID == 0)
                        {
                            InventoryManager.playerEquipment[1] = InventoryManager.playerInventory[slotNumber];
                            InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                            GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                            GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                        }
                        else if (InventoryManager.playerEquipment[1].itemID != 0)
                        {
                            Item tempItem = InventoryManager.playerEquipment[1];
                            InventoryManager.playerEquipment[1] = InventoryManager.playerInventory[slotNumber];
                            InventoryManager.playerInventory[slotNumber] = tempItem;
                            GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                            GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                        }
                        break;

                    case "Neck":
                        //Check if there is a neck equipped
                        if (InventoryManager.playerEquipment[2].itemID == 0)
                        {
                            InventoryManager.playerEquipment[2] = InventoryManager.playerInventory[slotNumber];
                            InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                            GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                            GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                        }
                        else if (InventoryManager.playerEquipment[2].itemID != 0)
                        {
                            Item tempItem = InventoryManager.playerEquipment[2];
                            InventoryManager.playerEquipment[2] = InventoryManager.playerInventory[slotNumber];
                            InventoryManager.playerInventory[slotNumber] = tempItem;
                            GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                            GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                        }
                        break;

                    case "Body":
                        //Check if there is a body equipped
                        if (InventoryManager.playerEquipment[3].itemID == 0)
                        {
                            InventoryManager.playerEquipment[3] = InventoryManager.playerInventory[slotNumber];
                            InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                            GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                            GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                        }
                        else if (InventoryManager.playerEquipment[3].itemID != 0)
                        {
                            Item tempItem = InventoryManager.playerEquipment[3];
                            InventoryManager.playerEquipment[3] = InventoryManager.playerInventory[slotNumber];
                            InventoryManager.playerInventory[slotNumber] = tempItem;
                            GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                            GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                        }
                        break;

                    case "Feet":
                        //Check if there is a feet equipped
                        if (InventoryManager.playerEquipment[5].itemID == 0)
                        {
                            InventoryManager.playerEquipment[5] = InventoryManager.playerInventory[slotNumber];
                            InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                            GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                            GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                        }
                        else if (InventoryManager.playerEquipment[5].itemID != 0)
                        {
                            Item tempItem = InventoryManager.playerEquipment[5];
                            InventoryManager.playerEquipment[5] = InventoryManager.playerInventory[slotNumber];
                            InventoryManager.playerInventory[slotNumber] = tempItem;
                            GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                            GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                        }
                        break;

                    case "Ring":
                        //Check if there is a ring equipped
                        if (InventoryManager.playerEquipment[4].itemID == 0)
                        {
                            InventoryManager.playerEquipment[4] = InventoryManager.playerInventory[slotNumber];
                            InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                            GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                            GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                        }
                        else if (InventoryManager.playerEquipment[6].itemID == 0)
                        {
                            InventoryManager.playerEquipment[6] = InventoryManager.playerInventory[slotNumber];
                            InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                            GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                            GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                        }
                        else if (InventoryManager.playerEquipment[4].itemID != 0)
                        {
                            Item tempItem = InventoryManager.playerEquipment[4];
                            InventoryManager.playerEquipment[4] = InventoryManager.playerInventory[slotNumber];
                            InventoryManager.playerInventory[slotNumber] = tempItem;
                            GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                            GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                        }
                        break;
                }
                gameObject.SetActive(false);
                InventoryController.invMenuToggle = false;
            }
            
            //Use
            else if(optionID == 2)
            {
                choiceHover = true;
                noise.Play();
            }

            //Inspect
            else if(optionID == 3 && Input.GetMouseButtonDown(0) && !inMenu)
            {
                choiceHover = true;
                noise.Play();
                //Spear
                if(itemID == 79)
                {
                    InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                    GameObject.Find("InventoryController").GetComponent<ItemDatabase>().IdentifySpear(slotNumber);
                    gameObject.SetActive(false);
                    InventoryController.invMenuToggle = false;
                }
                //Neck
                if(itemID == 80)
                {
                    InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                    GameObject.Find("InventoryController").GetComponent<ItemDatabase>().IdentifyNeck(slotNumber);
                    gameObject.SetActive(false);
                    InventoryController.invMenuToggle = false;
                }
                //Ring
                if(itemID == 81)
                {
                    InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                    GameObject.Find("InventoryController").GetComponent<ItemDatabase>().IdentifyRing(slotNumber);
                    gameObject.SetActive(false);
                    InventoryController.invMenuToggle = false;
                }
                


            }

        }

        //Check if item can be split
        else if(hit.collider != null && hit.collider.tag == "Inv_Menu_Split" && !inMenu)
        {
            choiceHover = true;
            splitText.color = new Color(255, 255, 0);
            useText.color = new Color(255, 255, 255);
            destroyText.color = new Color(255, 255, 255);
            if (Input.GetMouseButtonDown(0) && InventoryManager.playerInventory[slotNumber].itemQuantity > 1 && InventoryManager.playerInventory[slotNumber].stackable)
            {
                noise.Play();
                numSplit = 1;
                splitTextNum.text = numSplit.ToString();
                splitPanel.SetActive(true);
                inMenu = true;
            }
            else if(Input.GetMouseButtonDown(0) && (InventoryManager.playerInventory[slotNumber].itemQuantity <= 1 || !InventoryManager.playerInventory[slotNumber].stackable))
            {
                error.Play();
            }
        }

        else if(hit.collider != null && hit.collider.tag == "Split_Item_Yes" && inMenu)
        {
            if(Input.GetMouseButtonDown(0))
            {
                bool itemFound = false;
                //check for an empty inv slot
                for(int i = 0; i < InventoryManager.playerInventory.Length; i++)
                {
                    if (InventoryManager.playerInventory[i].itemID == 0)
                    {
                        noise.Play();
                        InventoryManager.playerInventory[i] = new Item(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].itemName,
                            itemType, GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].itemDescription,
                            GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].itemIconName, itemID,
                            GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].itemSellCost, 0, true);

                        InventoryManager.playerInventory[i].itemQuantity = numSplit;
                        InventoryManager.playerInventory[slotNumber].itemQuantity -= numSplit;

                        print(InventoryManager.playerInventory[i].itemQuantity + " " + InventoryManager.playerInventory[slotNumber].itemQuantity);
                        GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                        splitPanel.SetActive(false);
                        gameObject.SetActive(false);
                        InventoryController.invMenuToggle = false;
                        inMenu = false;
                        itemFound = true;
                        break;
                    }  
                }

                if(!itemFound)
                {
                    error.Play();
                    splitPanel.SetActive(false);
                    gameObject.SetActive(false);
                    InventoryController.invMenuToggle = false;
                    inMenu = false;
                }

            }
        }

        else if(hit.collider != null && hit.collider.tag == "Split_Item_No" && inMenu)
        {
            if(Input.GetMouseButtonDown(0))
            {
                noise.Play();
                splitPanel.SetActive(false);
                gameObject.SetActive(false);
                InventoryController.invMenuToggle = false;
                inMenu = false;
            }
        }

        else if(hit.collider != null && hit.collider.tag == "Split_Item_Add" && inMenu)
        {
            if(Input.GetMouseButtonDown(0) && numSplit < itemAmount-1)
            {
                noise.Play();
                numSplit++;
                splitTextNum.text = numSplit.ToString();
            }
            else if(Input.GetMouseButtonDown(0) && numSplit >= itemAmount-1)
            {
                error.Play();
            }
        }

        else if(hit.collider != null && hit.collider.tag == "Split_Item_Sub" && inMenu)
        {
            if (Input.GetMouseButtonDown(0) && numSplit > 1)
            {
                noise.Play();
                numSplit--;
                splitTextNum.text = numSplit.ToString();
            }
            else if(Input.GetMouseButtonDown(0) && numSplit <= 1)
            {
                error.Play();
            }
        }

        //If player hovers over destroy item confirm
        else if (hit.collider != null && hit.collider.tag == "Inv_Menu_Destroy" && !inMenu)
        {
            choiceHover = true;
            splitText.color = new Color(255, 255, 255);
            useText.color = new Color(255, 255, 255);
            destroyText.color = new Color(255, 255, 0);
            if (Input.GetMouseButtonDown(0) && InventoryManager.playerInventory[slotNumber].stackable && InventoryManager.playerInventory[slotNumber].itemID != 8 
                && InventoryManager.playerInventory[slotNumber].itemID != 9 && InventoryManager.playerInventory[slotNumber].itemID != 10)
            {
                noise.Play();
                numDestroyed = 1;
                destroyTextNum.text = numDestroyed.ToString();
                destroyPanel.SetActive(true);
                inMenu = true;
            }
            else if(Input.GetMouseButtonDown(0) && !InventoryManager.playerInventory[slotNumber].stackable && InventoryManager.playerInventory[slotNumber].itemID != 8
                && InventoryManager.playerInventory[slotNumber].itemID != 9 && InventoryManager.playerInventory[slotNumber].itemID != 10)
            {
                noise.Play();
                //InventoryManager.playerInventory[slotNumber].itemQuantity--;
                InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                gameObject.SetActive(false);
                InventoryController.invMenuToggle = false;
            }
            else
            {
                if(Input.GetMouseButtonDown(0))
                {
                    error.Play();
                }
                
            }
        }

        else if (hit.collider != null && hit.collider.tag == "Destroy_Item_Yes" && inMenu)
        {
            if (Input.GetMouseButtonDown(0))
            {
                noise.Play();
                InventoryManager.playerInventory[slotNumber].itemQuantity -= numDestroyed;
                if(InventoryManager.playerInventory[slotNumber].itemQuantity <= 0)
                {
                    InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                }
                GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                destroyPanel.SetActive(false);
                gameObject.SetActive(false);
                inMenu = false;
                InventoryController.invMenuToggle = false;
            }
        }

        else if(hit.collider != null && hit.collider.tag == "Destroy_Item_No" && inMenu)
        {
            if(Input.GetMouseButtonDown(0))
            {
                noise.Play();
                destroyPanel.SetActive(false);
                gameObject.SetActive(false);
                inMenu = false;
                InventoryController.invMenuToggle = false;
            }
        }

        else if(hit.collider != null && hit.collider.tag == "Destroy_Item_Add" && inMenu)
        {
            if (Input.GetMouseButtonDown(0) && numDestroyed < itemAmount)
            {
                noise.Play();
                numDestroyed++;
                destroyTextNum.text = numDestroyed.ToString();
            }
            else if(Input.GetMouseButtonDown(0) && numDestroyed == itemAmount)
            {
                error.Play();
            }
        }

        else if(hit.collider != null && hit.collider.tag == "Destroy_Item_Sub" && inMenu)
        {
            if (Input.GetMouseButtonDown(0) && numDestroyed > 1)
            {
                noise.Play();
                numDestroyed--;
                destroyTextNum.text = numDestroyed.ToString();
            }
            else if(Input.GetMouseButtonDown(0) && numDestroyed == 1)
            {
                error.Play();
            }
        }
        else
        {
            choiceHover = false;
            useText.color = new Color(255, 255, 255);
            splitText.color = new Color(255, 255, 255);
            destroyText.color = new Color(255, 255, 255);
        }


        //Controller 

        //Splitting items
        if(splitPanel.activeInHierarchy && GameController.xbox360Enabled() && inMenu)
        {
            if (InputManager.MainHorizontal() > .5f && !padBuffer)
            {
                if (numSplit < itemAmount - 1)
                {
                    noise.Play();
                    numSplit++;
                    splitTextNum.text = numSplit.ToString();
                    padBuffer = true;
                    StartCoroutine(PadBuffer());
                }
                else if (numSplit >= itemAmount - 1)
                {
                    error.Play();
                }
            }
            else if(InputManager.MainHorizontal() < -.5f && !padBuffer)
            {
                if (numSplit > 1)
                {
                    noise.Play();
                    numSplit--;
                    splitTextNum.text = numSplit.ToString();
                    padBuffer = true;
                    StartCoroutine(PadBuffer());
                }
                else if (numSplit <= 1)
                {
                    error.Play();
                }
            }
            else if(InputManager.A_Button())
            {
                bool itemFound = false;
                //check for an empty inv slot
                for (int i = 0; i < InventoryManager.playerInventory.Length; i++)
                {
                    if (InventoryManager.playerInventory[i].itemID == 0)
                    {
                        noise.Play();
                        InventoryManager.playerInventory[i] = new Item(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].itemName,
                            itemType, GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].itemDescription,
                            GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].itemIconName, itemID,
                            GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].itemSellCost, 0, true);

                        InventoryManager.playerInventory[i].itemQuantity = numSplit;
                        InventoryManager.playerInventory[slotNumber].itemQuantity -= numSplit;

                        print(InventoryManager.playerInventory[i].itemQuantity + " " + InventoryManager.playerInventory[slotNumber].itemQuantity);
                        GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                        splitPanel.SetActive(false);
                        gameObject.SetActive(false);
                        InventoryController.invMenuToggle = false;
                        inMenu = false;
                        itemFound = true;
                        break;
                    }
                }

                if (!itemFound)
                {
                    error.Play();
                    splitPanel.SetActive(false);
                    gameObject.SetActive(false);
                    InventoryController.invMenuToggle = false;
                    inMenu = false;
                }
            }
            else if(InputManager.B_Button())
            {
                noise.Play();
                splitPanel.SetActive(false);
                gameObject.SetActive(false);
                InventoryController.invMenuToggle = false;
                inMenu = false;
            }
        }

        if (destroyPanel.activeInHierarchy && GameController.xbox360Enabled() && inMenu)
        {
            if (InputManager.MainHorizontal() > .5f && !padBuffer)
            {
                if (numDestroyed < itemAmount)
                {
                    noise.Play();
                    numDestroyed++;
                    destroyTextNum.text = numDestroyed.ToString();
                    padBuffer = true;
                    StartCoroutine(PadBuffer());
                }
                else if (numDestroyed == itemAmount)
                {
                    error.Play();
                }
            }
            else if (InputManager.MainHorizontal() < -.5f)
            {
                if (numDestroyed > 1 && !padBuffer)
                {
                    noise.Play();
                    numDestroyed--;
                    destroyTextNum.text = numDestroyed.ToString();
                    padBuffer = true;
                    StartCoroutine(PadBuffer());
                }
                else if (numDestroyed == 1)
                {
                    error.Play();
                }
            }
            else if (InputManager.A_Button())
            {
                noise.Play();
                InventoryManager.playerInventory[slotNumber].itemQuantity -= numDestroyed;
                if (InventoryManager.playerInventory[slotNumber].itemQuantity <= 0)
                {
                    InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                }
                GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                destroyPanel.SetActive(false);
                gameObject.SetActive(false);
                inMenu = false;
                InventoryController.invMenuToggle = false;
            }
            else if (InputManager.B_Button())
            {
                noise.Play();
                destroyPanel.SetActive(false);
                gameObject.SetActive(false);
                inMenu = false;
                InventoryController.invMenuToggle = false;
            }


        }


    }

    //Controller Controls

    public void PadEquip()
    {
        if (optionID == 1)
        {
            noise.Play();
            switch (itemType)
            {
                default:
                    break;
                case "Weapon":
                    //Check if there is a weapon equipped
                    if (InventoryManager.playerEquipment[0].itemID == 0)
                    {
                        InventoryManager.playerEquipment[0] = InventoryManager.playerInventory[slotNumber];
                        InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                        GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                        GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                    }
                    else if (InventoryManager.playerEquipment[0].itemID != 0)
                    {
                        Item tempItem = InventoryManager.playerEquipment[0];
                        InventoryManager.playerEquipment[0] = InventoryManager.playerInventory[slotNumber];
                        InventoryManager.playerInventory[slotNumber] = tempItem;
                        GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                        GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                    }
                    break;

                case "Head":
                    //Check if there is a head equipped
                    if (InventoryManager.playerEquipment[1].itemID == 0)
                    {
                        InventoryManager.playerEquipment[1] = InventoryManager.playerInventory[slotNumber];
                        InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                        GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                        GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                    }
                    else if (InventoryManager.playerEquipment[1].itemID != 0)
                    {
                        Item tempItem = InventoryManager.playerEquipment[1];
                        InventoryManager.playerEquipment[1] = InventoryManager.playerInventory[slotNumber];
                        InventoryManager.playerInventory[slotNumber] = tempItem;
                        GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                        GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                    }
                    break;

                case "Neck":
                    //Check if there is a neck equipped
                    if (InventoryManager.playerEquipment[2].itemID == 0)
                    {
                        InventoryManager.playerEquipment[2] = InventoryManager.playerInventory[slotNumber];
                        InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                        GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                        GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                    }
                    else if (InventoryManager.playerEquipment[2].itemID != 0)
                    {
                        Item tempItem = InventoryManager.playerEquipment[2];
                        InventoryManager.playerEquipment[2] = InventoryManager.playerInventory[slotNumber];
                        InventoryManager.playerInventory[slotNumber] = tempItem;
                        GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                        GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                    }
                    break;

                case "Body":
                    //Check if there is a body equipped
                    if (InventoryManager.playerEquipment[3].itemID == 0)
                    {
                        InventoryManager.playerEquipment[3] = InventoryManager.playerInventory[slotNumber];
                        InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                        GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                        GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                    }
                    else if (InventoryManager.playerEquipment[3].itemID != 0)
                    {
                        Item tempItem = InventoryManager.playerEquipment[3];
                        InventoryManager.playerEquipment[3] = InventoryManager.playerInventory[slotNumber];
                        InventoryManager.playerInventory[slotNumber] = tempItem;
                        GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                        GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                    }
                    break;

                case "Feet":
                    //Check if there is a feet equipped
                    if (InventoryManager.playerEquipment[5].itemID == 0)
                    {
                        InventoryManager.playerEquipment[5] = InventoryManager.playerInventory[slotNumber];
                        InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                        GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                        GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                    }
                    else if (InventoryManager.playerEquipment[5].itemID != 0)
                    {
                        Item tempItem = InventoryManager.playerEquipment[5];
                        InventoryManager.playerEquipment[5] = InventoryManager.playerInventory[slotNumber];
                        InventoryManager.playerInventory[slotNumber] = tempItem;
                        GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                        GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                    }
                    break;

                case "Ring":
                    //Check if there is a ring equipped
                    if (InventoryManager.playerEquipment[4].itemID == 0)
                    {
                        InventoryManager.playerEquipment[4] = InventoryManager.playerInventory[slotNumber];
                        InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                        GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                        GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                    }
                    else if (InventoryManager.playerEquipment[6].itemID == 0)
                    {
                        InventoryManager.playerEquipment[6] = InventoryManager.playerInventory[slotNumber];
                        InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                        GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                        GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                    }
                    else if (InventoryManager.playerEquipment[4].itemID != 0)
                    {
                        Item tempItem = InventoryManager.playerEquipment[4];
                        InventoryManager.playerEquipment[4] = InventoryManager.playerInventory[slotNumber];
                        InventoryManager.playerInventory[slotNumber] = tempItem;
                        GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                        GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                    }
                    break;
            }
            gameObject.SetActive(false);
            InventoryController.invMenuToggle = false;
        }
    }

    public void PadInspect()
    {
        if (optionID == 3)
        {
            choiceHover = true;
            noise.Play();
            //Spear
            if (itemID == 79)
            {
                InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                GameObject.Find("InventoryController").GetComponent<ItemDatabase>().IdentifySpear(slotNumber);
                gameObject.SetActive(false);
                InventoryController.invMenuToggle = false;
            }
            //Neck
            if (itemID == 80)
            {
                InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                GameObject.Find("InventoryController").GetComponent<ItemDatabase>().IdentifyNeck(slotNumber);
                gameObject.SetActive(false);
                InventoryController.invMenuToggle = false;
            }
            //Ring
            if (itemID == 81)
            {
                InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                GameObject.Find("InventoryController").GetComponent<ItemDatabase>().IdentifyRing(slotNumber);
                gameObject.SetActive(false);
                InventoryController.invMenuToggle = false;
            }
        }
    }

    public void PadSplit()
    {
        if (InventoryManager.playerInventory[InventoryController.selectedIndex].itemQuantity > 1 && InventoryManager.playerInventory[InventoryController.selectedIndex].stackable)
        {
            noise.Play();
            numSplit = 1;
            splitTextNum.text = numSplit.ToString();
            splitPanel.SetActive(true);
            StartCoroutine(InputBuffer());
        }
        else if ((InventoryManager.playerInventory[InventoryController.selectedIndex].itemQuantity <= 1 || !InventoryManager.playerInventory[InventoryController.selectedIndex].stackable))
        {
            error.Play();
        }
    }

    public void PadDestroy()
    {
        if (InventoryManager.playerInventory[InventoryController.selectedIndex].stackable && InventoryManager.playerInventory[InventoryController.selectedIndex].itemID != 8
            && InventoryManager.playerInventory[InventoryController.selectedIndex].itemID != 9 && InventoryManager.playerInventory[InventoryController.selectedIndex].itemID != 10)
        {
            noise.Play();
            numDestroyed = 1;
            destroyTextNum.text = numDestroyed.ToString();
            destroyPanel.SetActive(true);
            StartCoroutine(InputBuffer());
        }
        else if (!InventoryManager.playerInventory[InventoryController.selectedIndex].stackable && InventoryManager.playerInventory[InventoryController.selectedIndex].itemID != 8
            && InventoryManager.playerInventory[InventoryController.selectedIndex].itemID != 9 && InventoryManager.playerInventory[InventoryController.selectedIndex].itemID != 10)
        {
            noise.Play();
            //InventoryManager.playerInventory[slotNumber].itemQuantity--;
            InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
            GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
            gameObject.SetActive(false);
            InventoryController.invMenuToggle = false;
        }
        else
        {
            error.Play();
        }
    }

    IEnumerator InputBuffer()
    {
        yield return new WaitForSeconds(.25f);
        inMenu = true;
    }

    IEnumerator PadBuffer()
    {
        yield return new WaitForSeconds(.25f);
        padBuffer = false;
        
    }

}
