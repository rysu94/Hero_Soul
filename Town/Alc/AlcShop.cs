using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AlcShop : MonoBehaviour
{

    public AudioSource click;
    public Animator buttonAnim;
    public GameObject alcConfirm;
    public GameObject error;

    public GameObject shopHUD;
    public GameObject shopDialogue;

    public GameObject shopUpgrade;
    public GameObject shopCustom;
    public GameObject shopMenu;

    public bool isClicked = false;

    public Text gold;

    public GameObject playerInv;
    public GameObject slotPrefab;

    public List<GameObject> playerSlotList = new List<GameObject>();

    public int alcShopID;

    public GameObject shopkeeperInv;
    public GameObject shopSlotPrefab;

    public List<GameObject> shopKeeperSlotList = new List<GameObject>();

    public int itemBuyAmount;
    public GameObject buyPrompt;
    public Text buyPromptText;
    public Text buyText;
    public int buyIndex;
    public bool buying;

    // Use this for initialization
    void Start()
    {
        for(int i = 0; i < InventoryManager.playerInventory.Length; i++)
        {
            playerSlotList.Add(Instantiate(slotPrefab, playerInv.transform, false));
            playerSlotList[i].GetComponent<Slot>().slotNumber = i;
        }
        UpdateInventory();

        for(int i = 0; i < GetShopInv(alcShopID).Length; i++)
        {
            shopKeeperSlotList.Add(Instantiate(shopSlotPrefab, shopkeeperInv.transform, false));
            shopKeeperSlotList[i].GetComponent<ShopSlot>().slotNumber = i;
            shopKeeperSlotList[i].GetComponent<ShopSlot>().shopID = alcShopID;
        }
        UpdateShopkeeperInventory();


    }

    // Update is called once per frame
    void Update()
    {
        gold.text = InventoryManager.playerGold.ToString();

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = Camera.main.transform.position.z;
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (Input.GetMouseButtonDown(0))
        {
            if (hit.collider != null && hit.collider.tag == "Shop_Buy")
            {
                buttonAnim.Play("Button");
                click.Play();
                shopDialogue.GetComponent<ShopDialogue>().Clear();
                shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("I've got the finest wares.", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Shopkeeper", "Shopkeeper",0.9f, -1));
                shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
                shopUpgrade.SetActive(false);
                shopCustom.SetActive(false);
                shopMenu.SetActive(true);
                alcConfirm.SetActive(false);
                PotionManager.inUpgrade = false;
            }

            else if(hit.collider != null && hit.collider.tag == "Destroy_Item_Yes")
            {
                click.Play();

                //if buying add item to player inventory
                if(buying)
                {
                    //find the matching index
                    int matchingIndex = -1;
                    for (int i = 0; i < InventoryManager.playerInventory.Length; i++)
                    {
                        if (InventoryManager.playerInventory[i].itemID == GetShopInv(alcShopID)[buyIndex].itemID)
                        {
                            matchingIndex = i;
                            break;
                        }
                    }

                    //find if item exists in inv and is stackable
                    if (GetShopInv(alcShopID)[buyIndex].stackable && matchingIndex > -1)
                    {
                        GetShopInv(alcShopID)[buyIndex].itemQuantity -= itemBuyAmount;
                        if (GetShopInv(alcShopID)[buyIndex].itemQuantity <= 0)
                        {
                            GetShopInv(alcShopID)[buyIndex] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                        }
                 
                        InventoryManager.playerInventory[matchingIndex].itemQuantity += itemBuyAmount;
                        if(InventoryManager.playerInventory[matchingIndex].itemQuantity > 99)
                        {
                            int extraItems = InventoryManager.playerInventory[matchingIndex].itemQuantity - 99;
                            //find next empty slot
                            InventoryManager.playerInventory[matchingIndex].itemQuantity = 99;

                            bool invFull = true;
                            for (int i = 0; i < InventoryManager.playerInventory.Length; i++)
                            {
                                if (InventoryManager.playerInventory[i].itemID == 0)
                                {
                                    int id = InventoryManager.playerInventory[i].itemID;
                                    InventoryManager.playerInventory[i] = InventoryManager.playerInventory[i] = new Item(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[id].itemName,
                                        GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[id].itemType, GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[id].itemDescription,
                                        GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[id].itemIconName, id,
                                        GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[id].itemSellCost, extraItems, true);
                               
                                    GetShopInv(alcShopID)[buyIndex].itemQuantity = itemBuyAmount;
                                    invFull = false;
                                    break;
                                }
                            }
                            if (invFull)
                            {
                                error.GetComponent<AudioSource>().Play();
                            }
                                        
                         }
                    }

                    //Add item to first empty spot
                    else
                    {
                        bool invFull = true;

                        for(int i = 0; i < InventoryManager.playerInventory.Length; i++)
                        {
                            if(InventoryManager.playerInventory[i].itemID == 0)
                            {
                                GetShopInv(alcShopID)[buyIndex].itemQuantity -= itemBuyAmount;
                                int id = GetShopInv(alcShopID)[buyIndex].itemID;
                                InventoryManager.playerInventory[i] = InventoryManager.playerInventory[i] = new Item(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[id].itemName,
                                        GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[id].itemType, GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[id].itemDescription,
                                        GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[id].itemIconName, id,
                                        GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[id].itemSellCost, itemBuyAmount, true);
                                invFull = false;
                                break;
                            }
                        }
                        if(invFull)
                        {
                            error.GetComponent<AudioSource>().Play();
                        }
                    }
                        
                }
                //if selling add item to shop inventory
                else
                {

                }

                ClosePrompt();
            }
            else if (hit.collider != null && hit.collider.tag == "Destroy_Item_No")
            {
                click.Play();
                ClosePrompt();
            }
            else if (hit.collider != null && hit.collider.tag == "Destroy_Item_Add")
            {
                if(itemBuyAmount < GetShopInv(alcShopID)[buyIndex].itemQuantity)
                {
                    click.Play();
                    itemBuyAmount++;
                    buyPromptText.text = itemBuyAmount.ToString();
                    buyText.text = "Would you like to buy/sell " + GetShopInv(alcShopID)[buyIndex].itemName + " for " + GetShopInv(alcShopID)[buyIndex].itemSellCost * itemBuyAmount + " gold?";
                }
                else
                {
                    error.GetComponent<AudioSource>().Play();
                }
                

            }
            else if (hit.collider != null && hit.collider.tag == "Destroy_Item_Sub")
            {
                if(itemBuyAmount > 0)
                {
                    click.Play();
                    itemBuyAmount--;
                    buyPromptText.text = itemBuyAmount.ToString();
                    buyText.text = "Would you like to buy/sell " + GetShopInv(alcShopID)[buyIndex].itemName + " for " + GetShopInv(alcShopID)[buyIndex].itemSellCost * itemBuyAmount + " gold?";
                }
                else
                {
                    error.GetComponent<AudioSource>().Play();
                }
            }

        }
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < InventoryManager.playerInventory.Length; i++)
        {
            GameObject tempObj = playerSlotList[i].transform.Find("Item_IMG").gameObject;
            Image tempInvImage = tempObj.GetComponent<Image>();
            Sprite tempSprite = Resources.Load<Sprite>("Item/" + InventoryManager.playerInventory[i].itemIconName);
            tempInvImage.sprite = tempSprite;
            playerSlotList[i].GetComponent<Slot>().slotImage = tempSprite;

            playerSlotList[i].GetComponent<Slot>().itemID = InventoryManager.playerInventory[i].itemID;

            GameObject tempTextObj = playerSlotList[i].transform.Find("Item_Num").gameObject;
            Text tempText = tempTextObj.GetComponent<Text>();

            //Find out how many of something player has
            int tempInt = InventoryManager.playerInventory[i].itemQuantity;
            if (tempInt > 0 && InventoryManager.playerInventory[i].stackable)
            {
                tempText.text = tempInt.ToString();
                playerSlotList[i].GetComponent<Slot>().itemAmount = tempInt;
            }
            else if (tempInt > 0 && !InventoryManager.playerInventory[i].stackable)
            {
                tempText.text = "";
                playerSlotList[i].GetComponent<Slot>().itemAmount = tempInt;
            }
            else if (tempInt == 0)
            {
                tempText.text = "";
                playerSlotList[i].GetComponent<Slot>().itemAmount = 0;
            }
            else if (tempInt < 0)
            {
                tempText.text = "ERROR";
                playerSlotList[i].GetComponent<Slot>().itemAmount = -1;
            }

            //item type
            playerSlotList[i].GetComponent<Slot>().itemType = InventoryManager.playerInventory[i].itemType;
        }
    }

    public void UpdateShopkeeperInventory()
    {
        for (int i = 0; i < GetShopInv(alcShopID).Length; i++)
        {
            GameObject tempObj = shopKeeperSlotList[i].transform.Find("Item_IMG").gameObject;
            Image tempInvImage = tempObj.GetComponent<Image>();
            Sprite tempSprite = Resources.Load<Sprite>("Item/" + GetShopInv(alcShopID)[i].itemIconName);
            tempInvImage.sprite = tempSprite;
            shopKeeperSlotList[i].GetComponent<ShopSlot>().slotImage = tempSprite;

            shopKeeperSlotList[i].GetComponent<ShopSlot>().itemID = GetShopInv(alcShopID)[i].itemID;

            GameObject tempTextObj = shopKeeperSlotList[i].transform.Find("Item_Num").gameObject;
            Text tempText = tempTextObj.GetComponent<Text>();

            //Find out how many of something player has
            int tempInt = GetShopInv(alcShopID)[i].itemQuantity;
            if (tempInt > 0 && GetShopInv(alcShopID)[i].stackable)
            {
                tempText.text = tempInt.ToString();
                shopKeeperSlotList[i].GetComponent<ShopSlot>().itemAmount = tempInt;
            }
            else if (tempInt > 0 && !(GetShopInv(alcShopID)[i].stackable))
            {
                tempText.text = "";
                shopKeeperSlotList[i].GetComponent<ShopSlot>().itemAmount = tempInt;
            }
            else if (tempInt == 0)
            {
                tempText.text = "";
                shopKeeperSlotList[i].GetComponent<ShopSlot>().itemAmount = 0;
            }
            else if (tempInt < 0)
            {
                tempText.text = "ERROR";
                shopKeeperSlotList[i].GetComponent<ShopSlot>().itemAmount = -1;
            }

            //item type
            shopKeeperSlotList[i].GetComponent<ShopSlot>().itemType = GetShopInv(alcShopID)[i].itemType;
        }
    }

    Item[] GetShopInv(int id)
    {
        switch(id)
        {
            default:
                return InventoryManager.alcShopWeissInventory;
            case 1:
                return InventoryManager.alcShopWeissInventory;
        }

    }


    public void OpenPrompt(int index, bool buy)
    {
        InventoryManager.inShop = true;
        buying = buy;
        buyPrompt.SetActive(true);
        itemBuyAmount = 1;
        buyPromptText.text = "1";
        buyIndex = index;
        buyText.text = "Would you like to buy/sell " + GetShopInv(alcShopID)[buyIndex].itemName + " for " + GetShopInv(alcShopID)[buyIndex].itemSellCost * itemBuyAmount + " gold?";
    }

    public void ClosePrompt()
    {
        InventoryManager.inShop = false;
        buyPrompt.SetActive(false);
        UpdateInventory();
        UpdateShopkeeperInventory();
    }
}