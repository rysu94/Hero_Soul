using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class EquipSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int slotNum;
    public Sprite slotImage;
    public int itemID;
    public string itemType;

    public GameObject dragSlot;
    public GameObject slotImageObj;
    public GameObject slotTextObj;

    public AudioSource noise;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemID > 0)
        {
            noise.Play();
            dragSlot = Instantiate(Resources.Load("Prefabs/Inventory/Drag_Item")) as GameObject;
            dragSlot.transform.SetParent(GameObject.Find("HUD").transform, false);
            dragSlot.GetComponent<Image>().sprite = slotImage;
            dragSlot.GetComponentInChildren<Text>().text = "";

            slotImageObj = transform.Find("Item_IMG").gameObject;
            slotImageObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/No_Item");
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

            //When interacting with the player inventory
            if (hit.collider != null && hit.collider.tag == "Inv_Slot")
            {
                //check if the slot is empty or filled
                int raySlotNum = hit.transform.gameObject.GetComponent<Slot>().slotNumber;
                //if filled and matches the slot type swap item places
                if (InventoryManager.playerInventory[raySlotNum].itemID > 0 && InventoryManager.playerInventory[raySlotNum].itemType == itemType)
                {
                    Item tempItem = InventoryManager.playerEquipment[slotNum];
                    InventoryManager.playerEquipment[slotNum] = InventoryManager.playerInventory[raySlotNum];
                    InventoryManager.playerInventory[raySlotNum] = tempItem;
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                    Destroy(dragSlot);
                }
                //if empty move item to the empty slot
                else if (InventoryManager.playerInventory[raySlotNum].itemID == 0)
                {
                    InventoryManager.playerInventory[raySlotNum] = InventoryManager.playerEquipment[slotNum];
                    InventoryManager.playerEquipment[slotNum] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
                    GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                    Destroy(dragSlot);
                }
            }

            else if(hit.collider != null && hit.collider.tag == "Inv_Equip" && hit.collider.transform.gameObject.GetComponent<EquipSlot>().itemType == itemType)
            {
                int raySlotNum = hit.transform.gameObject.GetComponent<EquipSlot>().slotNum;
                Item tempItem = InventoryManager.playerEquipment[slotNum];
                InventoryManager.playerEquipment[slotNum] = InventoryManager.playerEquipment[raySlotNum];
                InventoryManager.playerEquipment[raySlotNum] = tempItem;
                GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                Destroy(dragSlot);
            }
            else if(hit.collider != null && hit.collider.tag == "Inv_Equip" && (slotNum == 4 || slotNum == 6) && 
                (hit.collider.transform.gameObject.GetComponent<EquipSlot>().slotNum == 4 || hit.collider.transform.gameObject.GetComponent<EquipSlot>().slotNum == 6))
            {
                int raySlotNum = hit.transform.gameObject.GetComponent<EquipSlot>().slotNum;
                InventoryManager.playerEquipment[raySlotNum] = InventoryManager.playerEquipment[slotNum];
                InventoryManager.playerEquipment[slotNum] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                Destroy(dragSlot);
            }
            else if(hit.collider != null && hit.collider.tag == "Inv_Garbage")
            {
                InventoryManager.playerEquipment[slotNum].itemQuantity--;
                InventoryManager.playerEquipment[slotNum] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateEquip();
                Destroy(dragSlot);
            }

            else if (hit.collider == null || hit.collider.tag != "Inv_Slot")
            {
                Destroy(dragSlot);
                slotImageObj.GetComponent<Image>().sprite = slotImage;
            }
        }
    }




}
