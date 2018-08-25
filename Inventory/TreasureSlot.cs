using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class TreasureSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public int slotNum;
    public Sprite slotImage;
    public int itemID;
    public int itemQuantity;
    public string itemType;

    public GameObject dragSlot;
    public GameObject slotImageObj;
    public GameObject slotTextObj;

    public AudioSource noise;

    void Awake()
    {
        noise = GameObject.Find("GenNoise").GetComponent<AudioSource>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemID > 0)
        {
            noise.Play();
            dragSlot = Instantiate(Resources.Load("Prefabs/Inventory/Drag_Item")) as GameObject;
            dragSlot.transform.SetParent(GameObject.Find("HUD").transform, false);
            dragSlot.GetComponent<Image>().sprite = slotImage;
            if(itemQuantity == 0)
            {
                dragSlot.GetComponentInChildren<Text>().text = "";
            }
            else if(itemQuantity > 0)
            {
                dragSlot.GetComponentInChildren<Text>().text = itemQuantity.ToString();
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
            
            if(hit.collider != null && hit.collider.gameObject.tag == "Inv_Treasure")
            {
                int raySlotNum = hit.transform.gameObject.GetComponent<TreasureSlot>().slotNum;
                //Check if there is an item in treasure slot
                if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv[raySlotNum].itemID > 0)
                {
                    Item tempItem = LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv[slotNum];
                    LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv[slotNum] =
                        LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv[raySlotNum];
                    LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv[raySlotNum] = tempItem;
                    GameObject.Find("Chest_0").GetComponent<Treasure_Chest>().UpdateTreasureInv();
                    Destroy(dragSlot);
                }
                //Check if there isn't an item
                else if(LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv[raySlotNum].itemID == 0)
                {
                    LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv[raySlotNum] = 
                        LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv[slotNum];
                    LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv[slotNum] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                    GameObject.Find("Chest_0").GetComponent<Treasure_Chest>().UpdateTreasureInv();
                    Destroy(dragSlot);                
                }
            }

            else if(hit.collider != null && hit.collider.gameObject.tag == "Inv_Slot")
            {
                int raySlotNum = hit.transform.gameObject.GetComponent<Slot>().slotNumber;

                if(InventoryManager.playerInventory[raySlotNum].itemID == 0)
                {
                    InventoryManager.playerInventory[raySlotNum] = LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv[slotNum];
                    LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv[slotNum] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                    GameObject.Find("Chest_0").GetComponent<Treasure_Chest>().UpdateTreasureInv();
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                    Destroy(dragSlot);
                }

                else if(InventoryManager.playerInventory[raySlotNum].itemID == LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv[slotNum].itemID)
                {
                    //check if quantity will go over the max stack
                    int tempInt = InventoryManager.playerInventory[raySlotNum].itemQuantity + LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv[slotNum].itemQuantity;
                    if(tempInt <= 99)
                    {
                        InventoryManager.playerInventory[raySlotNum].itemQuantity += LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv[slotNum].itemQuantity;
                        LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv[slotNum] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                    }
                    else if(tempInt > 99)
                    {
                        InventoryManager.playerInventory[raySlotNum].itemQuantity = 99;
                        LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].treasureInv[slotNum].itemQuantity = tempInt - 99;
                    }
                    GameObject.Find("Chest_0").GetComponent<Treasure_Chest>().UpdateTreasureInv();
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                    Destroy(dragSlot);
                }
                else
                {
                    Destroy(dragSlot);
                    slotImageObj.GetComponent<Image>().sprite = slotImage;
                    if (GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].stackable)
                    {
                        slotTextObj.GetComponent<Text>().text = itemQuantity.ToString();
                    }
                    else if (!GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].stackable)
                    {
                        slotTextObj.GetComponent<Text>().text = "";
                    }
                }
            }


            else if(hit.collider == null || hit.collider.gameObject.tag != "Inv_Treasure")
            {
                Destroy(dragSlot);
                slotImageObj.GetComponent<Image>().sprite = slotImage;
                if (itemQuantity == 0)
                {
                    slotTextObj.GetComponent<Text>().text = "";
                }
                else if (itemQuantity > 0)
                {
                    slotTextObj.GetComponent<Text>().text = itemQuantity.ToString();
                }
            }

            else if (hit.collider == null)
            {
                Destroy(dragSlot);
                slotImageObj.GetComponent<Image>().sprite = slotImage;
                if (GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].stackable)
                {
                    slotTextObj.GetComponent<Text>().text = itemQuantity.ToString();
                }
                else if (!GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].stackable)
                {
                    slotTextObj.GetComponent<Text>().text = "";
                }
            }


        }
    }
}

