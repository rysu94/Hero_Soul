using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class StashSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
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

    // Use this for initialization
    void Start ()
    {
        noise = GameObject.Find("GenNoise").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemID > 0)
        {
            noise.Play();
            dragSlot = Instantiate(Resources.Load("Prefabs/Inventory/Drag_Item")) as GameObject;
            dragSlot.transform.SetParent(GameObject.Find("HUD").transform, false);
            dragSlot.GetComponent<Image>().sprite = slotImage;
            if (GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].stackable)
            {
                dragSlot.GetComponentInChildren<Text>().text = itemAmount.ToString();
            }
            else if (!GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].stackable)
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
        if (itemID > 0)
        {
            dragSlot.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 5));
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (itemID > 0)
        {
            noise.Play();
            //raycast to see if its above an item slot
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPoint.z = Camera.main.transform.position.z;
            Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            //Interacting with stash slots
            if(hit.collider != null && hit.collider.tag == "Stash_Slot")
            {
                //check if the slot is empty or filled
                int raySlotNum = hit.transform.gameObject.GetComponent<StashSlot>().slotNumber;
                //if filled swap item places
                if (InventoryManager.playerStash[raySlotNum].itemID > 0 && InventoryManager.playerStash[raySlotNum].itemID != InventoryManager.playerStash[slotNumber].itemID)
                {
                    Item tempItem = InventoryManager.playerStash[slotNumber];
                    InventoryManager.playerStash[slotNumber] = InventoryManager.playerStash[raySlotNum];
                    InventoryManager.playerStash[raySlotNum] = tempItem;
                    GameObject.Find("PlayerStash").GetComponent<StashInteract>().UpdateStash();
                }
                //if empty move item to the empty slot
                else if (InventoryManager.playerStash[raySlotNum].itemID == 0)
                {
                    InventoryManager.playerStash[raySlotNum] = InventoryManager.playerStash[slotNumber];
                    InventoryManager.playerStash[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                    GameObject.Find("PlayerStash").GetComponent<StashInteract>().UpdateStash();
                }
                //Stack Item
                else if (InventoryManager.playerStash[raySlotNum].itemID == InventoryManager.playerStash[slotNumber].itemID && InventoryManager.playerStash[raySlotNum].stackable)
                {
                    int tempInt = InventoryManager.playerStash[raySlotNum].itemQuantity + InventoryManager.playerStash[slotNumber].itemQuantity;

                    if (tempInt <= 99)
                    {
                        InventoryManager.playerStash[raySlotNum].itemQuantity += InventoryManager.playerStash[slotNumber].itemQuantity;
                        InventoryManager.playerStash[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                    }
                    else if (tempInt > 99)
                    {
                        InventoryManager.playerStash[raySlotNum].itemQuantity = 99;
                        InventoryManager.playerStash[slotNumber].itemQuantity = tempInt - 99;
                    }
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

            //interacting with inv slots
            else if(hit.collider != null && hit.collider.tag == "Inv_Slot")
            {
                //check if the slot is empty or filled
                int raySlotNum = hit.transform.gameObject.GetComponent<Slot>().slotNumber;

                //if filled swap item places
                if (InventoryManager.playerInventory[raySlotNum].itemID > 0 && InventoryManager.playerInventory[raySlotNum].itemID != InventoryManager.playerStash[slotNumber].itemID)
                {
                    Item tempItem = InventoryManager.playerStash[slotNumber];
                    InventoryManager.playerStash[slotNumber] = InventoryManager.playerInventory[raySlotNum];
                    InventoryManager.playerInventory[raySlotNum] = tempItem;
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                    GameObject.Find("PlayerStash").GetComponent<StashInteract>().UpdateStash();

                }

                //if empty move item to the empty slot
                else if (InventoryManager.playerInventory[raySlotNum].itemID == 0)
                {
                    InventoryManager.playerInventory[raySlotNum] = InventoryManager.playerStash[slotNumber];
                    InventoryManager.playerStash[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                    GameObject.Find("PlayerStash").GetComponent<StashInteract>().UpdateStash();

                }

                //Stack Item
                else if (InventoryManager.playerInventory[raySlotNum].itemID == InventoryManager.playerStash[slotNumber].itemID && InventoryManager.playerInventory[raySlotNum].stackable)
                {
                    int tempInt = InventoryManager.playerInventory[raySlotNum].itemQuantity + InventoryManager.playerStash[slotNumber].itemQuantity;

                    if (tempInt <= 99)
                    {
                        InventoryManager.playerInventory[raySlotNum].itemQuantity += InventoryManager.playerStash[slotNumber].itemQuantity;
                        InventoryManager.playerStash[slotNumber] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                    }
                    else if (tempInt > 99)
                    {
                        InventoryManager.playerInventory[raySlotNum].itemQuantity = 99;
                        InventoryManager.playerStash[slotNumber].itemQuantity = tempInt - 99;
                    }
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                    GameObject.Find("PlayerStash").GetComponent<StashInteract>().UpdateStash();
                }


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
    }

}
