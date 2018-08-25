using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int slotNumber;
    public Sprite slotImage;
    public int itemAmount;
    public int itemID;
    public string itemType;
    public GameObject dragSlot;
    public GameObject slotImageObj;
    public GameObject slotTextObj;
    public AudioSource noise;

    void Awake()
    {
        noise = GameObject.Find("GenNoise").GetComponent<AudioSource>();
    }

    //Begin mouse drag
    //1. raycast to check if mouse is on top of a slot
    //2. get item data on the slot 
    //3. instantiate the Drag Image prefab
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(itemID > 0 && !InventoryManager.inShop && !InvMenu.inMenu)
        {
            noise.Play();
            dragSlot = Instantiate(Resources.Load("Prefabs/Inventory/Drag_Item")) as GameObject;
            dragSlot.transform.SetParent(GameObject.Find("HUD").transform, false);
            dragSlot.GetComponent<Image>().sprite = slotImage;
            if(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].stackable)
            {
                dragSlot.GetComponentInChildren<Text>().text = itemAmount.ToString();
            }
            else if(!GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].stackable)
            {
                dragSlot.GetComponentInChildren<Text>().text = "";
            }
            

            slotImageObj = transform.Find("Item_IMG").gameObject;
            slotImageObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/No_Item");
            slotTextObj = transform.Find("Item_Num").gameObject;
            slotTextObj.GetComponent<Text>().text = "";
            
        }
       
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (itemID > 0 && !InventoryManager.inShop && !InvMenu.inMenu)
        {
            dragSlot.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 5));
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (itemID > 0 && !InventoryManager.inShop && !InvMenu.inMenu)
        {
            noise.Play();
            //raycast to see if its above an item slot
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPoint.z = Camera.main.transform.position.z;
            Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider != null && hit.collider.tag == "Inv_Slot")
            {
                //check if the slot is empty or filled
                int raySlotNum = hit.transform.gameObject.GetComponent<Slot>().slotNumber;
                //if filled swap item places
                if (InventoryManager.playerInventory[raySlotNum].itemID > 0 && InventoryManager.playerInventory[raySlotNum].itemID != InventoryManager.playerInventory[slotNumber].itemID)
                {
                    Item tempItem = InventoryManager.playerInventory[slotNumber];
                    InventoryManager.playerInventory[slotNumber] = InventoryManager.playerInventory[raySlotNum];
                    InventoryManager.playerInventory[raySlotNum] = tempItem;
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                    //GameObject.Find("AlcShopButton").GetComponent<AlcShop>().UpdateInventory();

                }
                //if empty move item to the empty slot
                else if (InventoryManager.playerInventory[raySlotNum].itemID == 0)
                {
                    InventoryManager.playerInventory[raySlotNum] = InventoryManager.playerInventory[slotNumber];
                    InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                    //GameObject.Find("AlcShopButton").GetComponent<AlcShop>().UpdateInventory();

                }

                else if (InventoryManager.playerInventory[raySlotNum].itemID == InventoryManager.playerInventory[slotNumber].itemID && raySlotNum != slotNumber && InventoryManager.playerInventory[raySlotNum].stackable)
                {
                    int tempInt = InventoryManager.playerInventory[raySlotNum].itemQuantity + InventoryManager.playerInventory[slotNumber].itemQuantity;

                    if (tempInt <= 99)
                    {
                        InventoryManager.playerInventory[raySlotNum].itemQuantity += InventoryManager.playerInventory[slotNumber].itemQuantity;
                        InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                    }
                    else if (tempInt > 99)
                    {
                        InventoryManager.playerInventory[raySlotNum].itemQuantity = 99;
                        InventoryManager.playerInventory[slotNumber].itemQuantity = tempInt - 99;
                    }
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                    //GameObject.Find("AlcShopButton").GetComponent<AlcShop>().UpdateInventory();

                }
                else
                {
                    slotImageObj.GetComponent<Image>().sprite = slotImage;
                    if (GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].stackable)
                    {
                        slotTextObj.GetComponent<Text>().text = itemAmount.ToString();
                    }
                    else if (!GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].stackable)
                    {
                        slotTextObj.GetComponent<Text>().text = "";
                    }
                }
                Destroy(dragSlot);
            }

            else if (hit.collider != null && hit.collider.tag == "Inv_Garbage")
            {
                if (GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].stackable)
                {
                    InventoryManager.playerInventory[slotNumber].itemQuantity = 0;
                    InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                }
                else if (!GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].stackable)
                {
                    InventoryManager.playerInventory[slotNumber].itemQuantity--;
                    InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                }

                Destroy(dragSlot);
            }
            //Put items in equipment
            else if (hit.collider != null && hit.collider.tag == "Inv_Equip")
            {
                int equipSlotNum = hit.transform.gameObject.GetComponent<EquipSlot>().slotNum;
                //Wep
                if (equipSlotNum == 0 && itemType == "Weapon" && InventoryManager.playerEquipment[0].itemID == 0)
                {
                    InventoryManager.playerEquipment[0] = InventoryManager.playerInventory[slotNumber];
                    InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                }
                else if (equipSlotNum == 0 && itemType == "Weapon" && InventoryManager.playerEquipment[0].itemID != 0)
                {
                    Item tempItem = InventoryManager.playerEquipment[0];
                    InventoryManager.playerEquipment[0] = InventoryManager.playerInventory[slotNumber];
                    InventoryManager.playerInventory[slotNumber] = tempItem;
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                }
                //Head
                else if (equipSlotNum == 1 && itemType == "Head" && InventoryManager.playerEquipment[1].itemID == 0)
                {
                    InventoryManager.playerEquipment[1] = InventoryManager.playerInventory[slotNumber];
                    InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                }
                else if (equipSlotNum == 1 && itemType == "Head" && InventoryManager.playerEquipment[1].itemID != 0)
                {
                    Item tempItem = InventoryManager.playerEquipment[1];
                    InventoryManager.playerEquipment[1] = InventoryManager.playerInventory[slotNumber];
                    InventoryManager.playerInventory[slotNumber] = tempItem;
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                }
                //Neck
                else if (equipSlotNum == 2 && itemType == "Neck" && InventoryManager.playerEquipment[2].itemID == 0)
                {
                    InventoryManager.playerEquipment[2] = InventoryManager.playerInventory[slotNumber];
                    InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                }
                else if (equipSlotNum == 2 && itemType == "Neck" && InventoryManager.playerEquipment[2].itemID != 0)
                {
                    Item tempItem = InventoryManager.playerEquipment[2];
                    InventoryManager.playerEquipment[2] = InventoryManager.playerInventory[slotNumber];
                    InventoryManager.playerInventory[slotNumber] = tempItem;
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                }
                //Body
                else if (equipSlotNum == 3 && itemType == "Body" && InventoryManager.playerEquipment[3].itemID == 0)
                {
                    InventoryManager.playerEquipment[3] = InventoryManager.playerInventory[slotNumber];
                    InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                }
                else if (equipSlotNum == 3 && itemType == "Body" && InventoryManager.playerEquipment[3].itemID != 0)
                {
                    Item tempItem = InventoryManager.playerEquipment[3];
                    InventoryManager.playerEquipment[3] = InventoryManager.playerInventory[slotNumber];
                    InventoryManager.playerInventory[slotNumber] = tempItem;
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                }
                //Ring 1
                else if (equipSlotNum == 4 && itemType == "Ring" && InventoryManager.playerEquipment[4].itemID == 0)
                {
                    InventoryManager.playerEquipment[4] = InventoryManager.playerInventory[slotNumber];
                    InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                }
                else if (equipSlotNum == 4 && itemType == "Ring" && InventoryManager.playerEquipment[4].itemID != 0)
                {
                    Item tempItem = InventoryManager.playerEquipment[4];
                    InventoryManager.playerEquipment[4] = InventoryManager.playerInventory[slotNumber];
                    InventoryManager.playerInventory[slotNumber] = tempItem;
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                }
                //Feet
                else if (equipSlotNum == 5 && itemType == "Feet" && InventoryManager.playerEquipment[5].itemID == 0)
                {
                    InventoryManager.playerEquipment[5] = InventoryManager.playerInventory[slotNumber];
                    InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                }
                else if (equipSlotNum == 5 && itemType == "Feet" && InventoryManager.playerEquipment[5].itemID != 0)
                {
                    Item tempItem = InventoryManager.playerEquipment[5];
                    InventoryManager.playerEquipment[5] = InventoryManager.playerInventory[slotNumber];
                    InventoryManager.playerInventory[slotNumber] = tempItem;
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                }
                //Ring 2
                else if (equipSlotNum == 6 && itemType == "Ring" && InventoryManager.playerEquipment[6].itemID == 0)
                {
                    InventoryManager.playerEquipment[6] = InventoryManager.playerInventory[slotNumber];
                    InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                }
                else if (equipSlotNum == 6 && itemType == "Ring" && InventoryManager.playerEquipment[6].itemID != 0)
                {
                    Item tempItem = InventoryManager.playerEquipment[6];
                    InventoryManager.playerEquipment[6] = InventoryManager.playerInventory[slotNumber];
                    InventoryManager.playerInventory[slotNumber] = tempItem;
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                }

                else
                {
                    slotImageObj.GetComponent<Image>().sprite = slotImage;
                    if (InventoryManager.playerInventory[slotNumber].stackable)
                    {
                        slotTextObj.GetComponent<Text>().text = itemAmount.ToString();
                    }
                    else if (!InventoryManager.playerInventory[slotNumber].stackable)
                    {
                        slotTextObj.GetComponent<Text>().text = "";
                    }

                }


                Destroy(dragSlot);
            }

            //Interacting with the Player Stash
            else if (hit.collider != null && hit.collider.tag == "Stash_Slot")
            {
                //check if the slot is empty or filled
                int raySlotNum = hit.transform.gameObject.GetComponent<StashSlot>().slotNumber;
                //if filled swap item places
                if (InventoryManager.playerStash[raySlotNum].itemID > 0 && InventoryManager.playerStash[raySlotNum].itemID != InventoryManager.playerInventory[slotNumber].itemID)
                {
                    Item tempItem = InventoryManager.playerInventory[slotNumber];
                    InventoryManager.playerInventory[slotNumber] = InventoryManager.playerStash[raySlotNum];
                    InventoryManager.playerStash[raySlotNum] = tempItem;
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                    GameObject.Find("PlayerStash").GetComponent<StashInteract>().UpdateStash();
                }
                //if empty move item to the empty slot
                else if (InventoryManager.playerStash[raySlotNum].itemID == 0)
                {
                    InventoryManager.playerStash[raySlotNum] = InventoryManager.playerInventory[slotNumber];
                    InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                    GameObject.Find("PlayerStash").GetComponent<StashInteract>().UpdateStash();
                }

                else if (InventoryManager.playerStash[raySlotNum].itemID == InventoryManager.playerInventory[slotNumber].itemID && InventoryManager.playerStash[raySlotNum].stackable)
                {
                    int tempInt = InventoryManager.playerStash[raySlotNum].itemQuantity + InventoryManager.playerInventory[slotNumber].itemQuantity;

                    if (tempInt <= 99)
                    {
                        InventoryManager.playerStash[raySlotNum].itemQuantity += InventoryManager.playerInventory[slotNumber].itemQuantity;
                        InventoryManager.playerInventory[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                    }
                    else if (tempInt > 99)
                    {
                        InventoryManager.playerStash[raySlotNum].itemQuantity = 99;
                        InventoryManager.playerInventory[slotNumber].itemQuantity = tempInt - 99;
                    }
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                    GameObject.Find("PlayerStash").GetComponent<StashInteract>().UpdateStash();
                }
                else
                {
                    slotImageObj.GetComponent<Image>().sprite = slotImage;
                    if (GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].stackable)
                    {
                        slotTextObj.GetComponent<Text>().text = itemAmount.ToString();
                    }
                    else if (!GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].stackable)
                    {
                        slotTextObj.GetComponent<Text>().text = "";
                    }
                }
                Destroy(dragSlot);
            }

            //Shop Processing
            else if (hit.collider != null &&  hit.collider.tag == "Shop_Slot")
            {
                if(hit.collider.GetComponent<ShopSlot>().shopID == 1)
                {
                    GameObject.Find("AlcShopButton").GetComponent<AlcShop>().OpenPrompt(hit.collider.GetComponent<ShopSlot>().slotNumber, false);
                }
                Destroy(dragSlot);
            }

            else
            {
                Destroy(dragSlot);
                slotImageObj.GetComponent<Image>().sprite = slotImage;
                if(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].stackable)
                {
                    slotTextObj.GetComponent<Text>().text = itemAmount.ToString();
                }
                else if(!GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].stackable)
                {
                    slotTextObj.GetComponent<Text>().text = "";
                }   
            }
        } 
    }

}
