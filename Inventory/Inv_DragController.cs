using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inv_DragController : MonoBehaviour
{
    public GameObject slot;
    public GameObject tooltip;
    public GameObject tooltip_small;
    public GameObject card;

    public GameObject spellbook;
    
    // Use this for initialization
	void Start ()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = Camera.main.transform.position.z;
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        //Inventory slot tooltip
        if (hit.collider != null && hit.collider.tag == "Inv_Slot")
        {
            slot = hit.transform.gameObject;
            //print("Get Slot: " + slot.GetComponent<Slot>().slotNumber);

            Item slotItem = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().FindItem(slot.GetComponent<Slot>().itemID);
            //Create the Item Tooltip
            if (slotItem.itemID != 0)
            {
                tooltip.gameObject.SetActive(true);
                tooltip.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 5));

                tooltip.transform.position = new Vector3(tooltip.transform.position.x + 1f, tooltip.transform.position.y - .5f, tooltip.transform.position.z);

                //Find item name
                GameObject tempObj = tooltip.transform.Find("Item_Name").gameObject;
                Text tempText = tempObj.GetComponent<Text>();
                tempText.text = slotItem.itemName;
                //Find item desc
                tempObj = tooltip.transform.Find("Item_Desc").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = slotItem.itemDescription;
                //Find item type
                tempObj = tooltip.transform.Find("Item_Type").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = slotItem.itemType;
                //Find item sell
                tempObj = tooltip.transform.Find("Item_Sell1").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = "Sells for ";
                tempObj = tooltip.transform.Find("Item_Sell2").gameObject;
                tempText = tempObj.GetComponent<Text>();
                if (slotItem.stackable)
                {
                    tempText.text = (slotItem.itemSellCost * slot.GetComponent<Slot>().itemAmount).ToString() + " gold";
                }
                else if (!slotItem.stackable)
                {
                    tempText.text = (slotItem.itemSellCost).ToString() + " gold";
                }
            }
        }

        else if(hit.collider != null && hit.collider.tag == "Stash_Slot")
        {
            slot = hit.transform.gameObject;
            Item slotItem = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().FindItem(slot.GetComponent<StashSlot>().itemID);
            if (slotItem.itemID != 0)
            {
                tooltip.gameObject.SetActive(true);
                tooltip.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 5));

                tooltip.transform.position = new Vector3(tooltip.transform.position.x - 1.25f, tooltip.transform.position.y - .5f, tooltip.transform.position.z);

                //Find item name
                GameObject tempObj = tooltip.transform.Find("Item_Name").gameObject;
                Text tempText = tempObj.GetComponent<Text>();
                tempText.text = slotItem.itemName;
                //Find item desc
                tempObj = tooltip.transform.Find("Item_Desc").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = slotItem.itemDescription;
                //Find item type
                tempObj = tooltip.transform.Find("Item_Type").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = slotItem.itemType;
                //Find item sell
                tempObj = tooltip.transform.Find("Item_Sell1").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = "Sells for ";
                tempObj = tooltip.transform.Find("Item_Sell2").gameObject;
                tempText = tempObj.GetComponent<Text>();
                if (slotItem.stackable)
                {
                    tempText.text = (slotItem.itemSellCost * slotItem.itemQuantity).ToString();
                }
                else if (!slotItem.stackable)
                {
                    tempText.text = (slotItem.itemSellCost).ToString() + " gold";
                }
            }
        }
        //Equip Item Tool Tips
        else if (hit.collider != null && hit.collider.tag == "Inv_Equip")
        {
            slot = hit.transform.gameObject;
            Item slotItem = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().FindItem(slot.GetComponent<EquipSlot>().itemID);
            if (slotItem.itemID != 0)
            {
                tooltip.gameObject.SetActive(true);
                tooltip.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 5));

                tooltip.transform.position = new Vector3(tooltip.transform.position.x - 1.25f, tooltip.transform.position.y - .5f, tooltip.transform.position.z);

                //Find item name
                GameObject tempObj = tooltip.transform.Find("Item_Name").gameObject;
                Text tempText = tempObj.GetComponent<Text>();
                tempText.text = slotItem.itemName;
                //Find item desc
                tempObj = tooltip.transform.Find("Item_Desc").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = slotItem.itemDescription;
                //Find item type
                tempObj = tooltip.transform.Find("Item_Type").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = slotItem.itemType;
                //Find item sell
                tempObj = tooltip.transform.Find("Item_Sell1").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = "Sells for ";
                tempObj = tooltip.transform.Find("Item_Sell2").gameObject;
                tempText = tempObj.GetComponent<Text>();
                if (slotItem.stackable)
                {
                    tempText.text = (slotItem.itemSellCost * slotItem.itemQuantity).ToString();
                }
                else if (!slotItem.stackable)
                {
                    tempText.text = (slotItem.itemSellCost).ToString() + " gold";
                }
            }
        }

        else if (hit.collider != null && hit.collider.tag == "Inv_Treasure")
        {
            slot = hit.transform.gameObject;
            Item slotItem = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().FindItem(slot.GetComponent<TreasureSlot>().itemID);
            if (slotItem.itemID != 0)
            {
                tooltip.gameObject.SetActive(true);
                tooltip.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 5));

                tooltip.transform.position = new Vector3(tooltip.transform.position.x - .85f, tooltip.transform.position.y - .5f, tooltip.transform.position.z);

                //Find item name
                GameObject tempObj = tooltip.transform.Find("Item_Name").gameObject;
                Text tempText = tempObj.GetComponent<Text>();
                tempText.text = slotItem.itemName;
                //Find item desc
                tempObj = tooltip.transform.Find("Item_Desc").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = slotItem.itemDescription;
                //Find item type
                tempObj = tooltip.transform.Find("Item_Type").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = slotItem.itemType;
                //Find item sell
                tempObj = tooltip.transform.Find("Item_Sell1").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = "Sells for ";
                tempObj = tooltip.transform.Find("Item_Sell2").gameObject;
                tempText = tempObj.GetComponent<Text>();
                if (slotItem.stackable)
                {
                    tempText.text = (slotItem.itemSellCost * slot.GetComponent<TreasureSlot>().itemQuantity).ToString();
                }
                else if (!slotItem.stackable)
                {
                    tempText.text = (slotItem.itemSellCost).ToString();
                }
            }

        }

        //Arcana Tooltip
        else if (hit.collider != null && hit.collider.tag == "Inv_Arcana")
        {
            slot = hit.transform.gameObject;
            card.gameObject.SetActive(false);

            tooltip.gameObject.SetActive(true);
            tooltip.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 5));

            tooltip.transform.position = new Vector3(tooltip.transform.position.x + 1f, tooltip.transform.position.y - .5f, tooltip.transform.position.z);

            //Find item name
            GameObject tempObj = tooltip.transform.Find("Item_Name").gameObject;
            Text tempText = tempObj.GetComponent<Text>();
            tempText.text = slot.GetComponent<ArcanaTooltip>().arcanaName;

            //Find item desc
            tempObj = tooltip.transform.Find("Item_Desc").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = slot.GetComponent<ArcanaTooltip>().desc;

            //Find item type
            tempObj = tooltip.transform.Find("Item_Type").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = slot.GetComponent<ArcanaTooltip>().itemType;

            //Find item sell
            tempObj = tooltip.transform.Find("Item_Sell1").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = "Not Sellable";
            tempObj = tooltip.transform.Find("Item_Sell2").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = "";
        }

        //Card ToolTip
        else if (hit.collider != null && hit.collider.tag == "Inv_Card")
        {
            slot = hit.transform.gameObject;

            tooltip.gameObject.SetActive(true);
            tooltip.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 5));

            tooltip.transform.position = new Vector3(tooltip.transform.position.x - 1.35f, tooltip.transform.position.y - .5f, tooltip.transform.position.z);

            card.gameObject.SetActive(true);
            card.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 5));

            card.transform.position = new Vector3(card.transform.position.x - 2.52f, card.transform.position.y - .5f, card.transform.position.z);

            //Find item name
            GameObject tempObj = tooltip.transform.Find("Item_Name").gameObject;
            Text tempText = tempObj.GetComponent<Text>();
            tempText.text = slot.GetComponent<CardSlot>().cardName;

            //Find item type
            tempObj = tooltip.transform.Find("Item_Type").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = slot.GetComponent<CardSlot>().cardElement;

            //Find item desc
            tempObj = tooltip.transform.Find("Item_Desc").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = slot.GetComponent<CardSlot>().cardDesc;

            //Find item sell
            tempObj = tooltip.transform.Find("Item_Sell1").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = "";
            tempObj = tooltip.transform.Find("Item_Sell2").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = "";

            //Change Sprite
            card.GetComponent<Image>().sprite = slot.GetComponent<CardSlot>().cardImage;
        }

        else if (hit.collider != null && hit.collider.tag == "Inv_Stats")
        {
            slot = hit.transform.gameObject;
            tooltip.gameObject.SetActive(true);
            tooltip.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 5));
            tooltip.transform.position = new Vector3(tooltip.transform.position.x - .85f, tooltip.transform.position.y - .5f, tooltip.transform.position.z);

            //Find item name
            GameObject tempObj = tooltip.transform.Find("Item_Name").gameObject;
            Text tempText = tempObj.GetComponent<Text>();
            tempText.text = "Stat Information";
            //Find item type
            tempObj = tooltip.transform.Find("Item_Type").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = "";
            //Find item desc
            tempObj = tooltip.transform.Find("Item_Desc").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = slot.GetComponent<StatInfo>().desc;
            //Find item sell
            tempObj = tooltip.transform.Find("Item_Sell1").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = "";
            tempObj = tooltip.transform.Find("Item_Sell2").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = "";

        }

        else if(hit.collider != null && hit.collider.gameObject.tag == "Spell_Card" && hit.collider.gameObject.GetComponent<Spellbook_Slot>().cardName != "")
        {
            slot = hit.transform.gameObject;
            tooltip.gameObject.SetActive(true);
            tooltip.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 5));
            tooltip.transform.position = new Vector3(tooltip.transform.position.x - .85f, tooltip.transform.position.y - .5f, tooltip.transform.position.z);

            //Find item name
            GameObject tempObj = tooltip.transform.Find("Item_Name").gameObject;
            Text tempText = tempObj.GetComponent<Text>();
            tempText.text = slot.GetComponent<Spellbook_Slot>().cardName;

            //Find item type
            tempObj = tooltip.transform.Find("Item_Type").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = slot.GetComponent<Spellbook_Slot>().cardType;

            //Find item desc
            tempObj = tooltip.transform.Find("Item_Desc").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = slot.GetComponent<Spellbook_Slot>().cardDesc;

            //Find item sell
            tempObj = tooltip.transform.Find("Item_Sell1").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = "";
            tempObj = tooltip.transform.Find("Item_Sell2").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = "";
        }

        else if(hit.collider != null && hit.collider.tag == "Pedastal_Button" && hit.collider.gameObject.GetComponent<Arcana_Book_Select>().cardName != "")
        {
            slot = hit.transform.gameObject;
            tooltip.gameObject.SetActive(true);
            tooltip.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 5));

            tooltip.transform.position = new Vector3(tooltip.transform.position.x - 1.35f, tooltip.transform.position.y - .5f, tooltip.transform.position.z);

            //Find item name
            GameObject tempObj = tooltip.transform.Find("Item_Name").gameObject;
            Text tempText = tempObj.GetComponent<Text>();
            tempText.text = slot.GetComponent<Arcana_Book_Select>().cardName;

            //Find item type
            tempObj = tooltip.transform.Find("Item_Type").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = slot.GetComponent<Arcana_Book_Select>().cardType;

            //Find item desc
            tempObj = tooltip.transform.Find("Item_Desc").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = slot.GetComponent<Arcana_Book_Select>().cardDesc;

            //Find item sell
            tempObj = tooltip.transform.Find("Item_Sell1").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = "";
            tempObj = tooltip.transform.Find("Item_Sell2").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = "";
        }

        else if (hit.collider != null && hit.collider.gameObject.tag == "Active_Card" && hit.collider.gameObject.GetComponent<Spellbook_Slot>().cardName != "")
        {
            slot = hit.transform.gameObject;
            tooltip.gameObject.SetActive(true);
            tooltip.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 5));
            tooltip.transform.position = new Vector3(tooltip.transform.position.x + .85f, tooltip.transform.position.y + .5f, tooltip.transform.position.z);

            //Find item name
            GameObject tempObj = tooltip.transform.Find("Item_Name").gameObject;
            Text tempText = tempObj.GetComponent<Text>();
            tempText.text = slot.GetComponent<Spellbook_Slot>().cardName;

            //Find item type
            tempObj = tooltip.transform.Find("Item_Type").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = slot.GetComponent<Spellbook_Slot>().cardType;

            //Find item desc
            tempObj = tooltip.transform.Find("Item_Desc").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = slot.GetComponent<Spellbook_Slot>().cardDesc;

            //Find item sell
            tempObj = tooltip.transform.Find("Item_Sell1").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = "";
            tempObj = tooltip.transform.Find("Item_Sell2").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = "";
        }

        else if (hit.collider != null && hit.collider.gameObject.tag == "Player_State")
        {
            slot = hit.transform.gameObject;
            tooltip_small.gameObject.SetActive(true);
            tooltip_small.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 5));
            tooltip_small.transform.position = new Vector3(tooltip_small.transform.position.x - .85f, tooltip_small.transform.position.y - .5f, tooltip_small.transform.position.z);

            //Find item name
            GameObject tempObj = tooltip_small.transform.Find("Item_Name").gameObject;
            Text tempText = tempObj.GetComponent<Text>();
            tempText.text = slot.GetComponent<StateController>().stateName;

            //Find item type
            tempObj = tooltip_small.transform.Find("Item_Type").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = slot.GetComponent<StateController>().stateType;

            //Find item desc
            tempObj = tooltip_small.transform.Find("Item_Desc").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = slot.GetComponent<StateController>().stateDesc;

        }

        else if (hit.collider != null && hit.collider.gameObject.tag == "Quest_Reward")
        {
            slot = hit.transform.gameObject;
            tooltip_small.gameObject.SetActive(true);
            tooltip_small.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 5));
            tooltip_small.transform.position = new Vector3(tooltip_small.transform.position.x - .85f, tooltip_small.transform.position.y - .5f, tooltip_small.transform.position.z);

            //Find item name
            GameObject tempObj = tooltip_small.transform.Find("Item_Name").gameObject;
            Text tempText = tempObj.GetComponent<Text>();
            tempText.text = "Reward";

            //Find item type
            tempObj = tooltip_small.transform.Find("Item_Type").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = "Do this quest to recieve:";

            //Find item desc
            tempObj = tooltip_small.transform.Find("Item_Desc").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = slot.GetComponent<Reward_Slab>().desc;

        }

        else /*if (hit.collider == null || hit.collider.tag == "Inv_Slot")*/
        {
            tooltip_small.SetActive(false);
            tooltip.gameObject.SetActive(false);
            card.gameObject.SetActive(false);
        }


        if(GameController.xbox360Enabled() && InventoryController.inInv && !TestCharController.inTreasure)
        {
            //Controller Tooltips
            if (InventoryController.selectTab == 0 && InventoryController.inInv &&
                InventoryManager.playerInventory[InventoryController.selectedIndex].itemID != 0)
            {
                slot = GameObject.Find("InventoryController").GetComponent<InventoryController>().slotList[InventoryController.selectedIndex];
                card.SetActive(false);
                tooltip.gameObject.SetActive(true);
                tooltip.transform.position = GameObject.Find("InventoryController").GetComponent<InventoryController>().slotList[InventoryController.selectedIndex].transform.position;

                tooltip.transform.position = new Vector3(tooltip.transform.position.x + 1f, tooltip.transform.position.y - .5f, tooltip.transform.position.z);

                //Find item name
                GameObject tempObj = tooltip.transform.Find("Item_Name").gameObject;
                Text tempText = tempObj.GetComponent<Text>();
                tempText.text = InventoryManager.playerInventory[InventoryController.selectedIndex].itemName;
                //Find item desc
                tempObj = tooltip.transform.Find("Item_Desc").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = InventoryManager.playerInventory[InventoryController.selectedIndex].itemDescription;
                //Find item type
                tempObj = tooltip.transform.Find("Item_Type").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = InventoryManager.playerInventory[InventoryController.selectedIndex].itemType;
                //Find item sell
                tempObj = tooltip.transform.Find("Item_Sell1").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = "Sells for ";
                tempObj = tooltip.transform.Find("Item_Sell2").gameObject;
                tempText = tempObj.GetComponent<Text>();
                if (InventoryManager.playerInventory[InventoryController.selectedIndex].stackable)
                {
                    tempText.text = (InventoryManager.playerInventory[InventoryController.selectedIndex].itemSellCost * slot.GetComponent<Slot>().itemAmount).ToString() + " gold";
                }
                else if (!InventoryManager.playerInventory[InventoryController.selectedIndex].stackable)
                {
                    tempText.text = (InventoryManager.playerInventory[InventoryController.selectedIndex].itemSellCost).ToString() + " gold";
                }

            }
            else if (InventoryController.selectTab == 1 && InventoryController.inInv &&
                InventoryManager.playerEquipment[InventoryController.selectedIndex].itemID != 0)
            {
                tooltip.gameObject.SetActive(true);
                tooltip.transform.position = GameObject.Find("InventoryController").GetComponent<InventoryController>().equipList[InventoryController.selectedIndex].transform.position;
                card.SetActive(false);
                tooltip.transform.position = new Vector3(tooltip.transform.position.x - 1.25f, tooltip.transform.position.y - .5f, tooltip.transform.position.z);

                //Find item name
                GameObject tempObj = tooltip.transform.Find("Item_Name").gameObject;
                Text tempText = tempObj.GetComponent<Text>();
                tempText.text = InventoryManager.playerEquipment[InventoryController.selectedIndex].itemName;
                //Find item desc
                tempObj = tooltip.transform.Find("Item_Desc").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = InventoryManager.playerEquipment[InventoryController.selectedIndex].itemDescription;
                //Find item type
                tempObj = tooltip.transform.Find("Item_Type").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = InventoryManager.playerEquipment[InventoryController.selectedIndex].itemType;
                //Find item sell
                tempObj = tooltip.transform.Find("Item_Sell1").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = "Sells for ";
                tempObj = tooltip.transform.Find("Item_Sell2").gameObject;
                tempText = tempObj.GetComponent<Text>();
                if (InventoryManager.playerEquipment[InventoryController.selectedIndex].stackable)
                {
                    tempText.text = (InventoryManager.playerEquipment[InventoryController.selectedIndex].itemSellCost * GameObject.Find("InventoryController").GetComponent<InventoryController>().equipList[InventoryController.selectedIndex].GetComponent<Slot>().itemAmount).ToString() + " gold";
                }
                else if (!InventoryManager.playerEquipment[InventoryController.selectedIndex].stackable)
                {
                    tempText.text = (InventoryManager.playerEquipment[InventoryController.selectedIndex].itemSellCost).ToString() + " gold";
                }
            }
            else if (InventoryController.selectTab == 2 && InventoryController.inInv)
            {
                slot = ArcanaDeck_Manager.cardSlabList[InventoryController.selectedIndex];

                tooltip.gameObject.SetActive(true);
                tooltip.transform.position = ArcanaDeck_Manager.cardSlabList[InventoryController.selectedIndex].transform.position;

                tooltip.transform.position = new Vector3(tooltip.transform.position.x - 1.35f, tooltip.transform.position.y - .5f, tooltip.transform.position.z);

                card.gameObject.SetActive(true);
                card.transform.position = ArcanaDeck_Manager.cardSlabList[InventoryController.selectedIndex].transform.position;

                card.transform.position = new Vector3(card.transform.position.x - 2.52f, card.transform.position.y - .5f, card.transform.position.z);

                //Find item name
                GameObject tempObj = tooltip.transform.Find("Item_Name").gameObject;
                Text tempText = tempObj.GetComponent<Text>();
                tempText.text = slot.GetComponent<CardSlot>().cardName;

                //Find item type
                tempObj = tooltip.transform.Find("Item_Type").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = slot.GetComponent<CardSlot>().cardElement;

                //Find item desc
                tempObj = tooltip.transform.Find("Item_Desc").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = slot.GetComponent<CardSlot>().cardDesc;

                //Find item sell
                tempObj = tooltip.transform.Find("Item_Sell1").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = "";
                tempObj = tooltip.transform.Find("Item_Sell2").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = "";

                //Change Sprite
                card.GetComponent<Image>().sprite = slot.GetComponent<CardSlot>().cardImage;
            }
            else
            {
                tooltip.gameObject.SetActive(false);
                card.SetActive(false);
            }
        }

        if (GameController.xbox360Enabled() && TestCharController.inTreasure)
        {
            if(Treasure_Chest.slotList[Treasure_Chest.selectedIndex].GetComponent<TreasureSlot>().itemID != 0)
            {
                tooltip.gameObject.SetActive(true);
                tooltip.transform.position = Treasure_Chest.slotList[Treasure_Chest.selectedIndex].transform.position;
                tooltip.transform.position = new Vector3(tooltip.transform.position.x - 1.55f, tooltip.transform.position.y - .5f, tooltip.transform.position.z);

                //Find item name
                GameObject tempObj = tooltip.transform.Find("Item_Name").gameObject;
                Text tempText = tempObj.GetComponent<Text>();
                tempText.text = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().FindItem(Treasure_Chest.slotList[Treasure_Chest.selectedIndex].GetComponent<TreasureSlot>().itemID).itemName;
                //Find item desc
                tempObj = tooltip.transform.Find("Item_Desc").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().FindItem(Treasure_Chest.slotList[Treasure_Chest.selectedIndex].GetComponent<TreasureSlot>().itemID).itemDescription;
                //Find item type
                tempObj = tooltip.transform.Find("Item_Type").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().FindItem(Treasure_Chest.slotList[Treasure_Chest.selectedIndex].GetComponent<TreasureSlot>().itemID).itemType;
                //Find item sell
                tempObj = tooltip.transform.Find("Item_Sell1").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = "Sells for ";
                tempObj = tooltip.transform.Find("Item_Sell2").gameObject;
                tempText = tempObj.GetComponent<Text>();
                if (GameObject.Find("InventoryController").GetComponent<ItemDatabase>().FindItem(Treasure_Chest.slotList[Treasure_Chest.selectedIndex].GetComponent<TreasureSlot>().itemID).stackable)
                {
                    tempText.text = (GameObject.Find("InventoryController").GetComponent<ItemDatabase>().FindItem(Treasure_Chest.slotList[Treasure_Chest.selectedIndex].GetComponent<TreasureSlot>().itemID).itemSellCost * GameObject.Find("InventoryController").GetComponent<ItemDatabase>().FindItem(Treasure_Chest.slotList[Treasure_Chest.selectedIndex].GetComponent<TreasureSlot>().itemID).itemQuantity).ToString() + " gold";
                }
                else if (!GameObject.Find("InventoryController").GetComponent<ItemDatabase>().FindItem(Treasure_Chest.slotList[Treasure_Chest.selectedIndex].GetComponent<TreasureSlot>().itemID).stackable)
                {
                    tempText.text = (GameObject.Find("InventoryController").GetComponent<ItemDatabase>().FindItem(Treasure_Chest.slotList[Treasure_Chest.selectedIndex].GetComponent<TreasureSlot>().itemID).itemSellCost).ToString() + " gold";
                }
            }
            else
            {
                tooltip.gameObject.SetActive(false);
                card.SetActive(false);
            }

        }
        if(GameController.xbox360Enabled() && InventoryController.inSpellbook)
        {
            slot = spellbook.GetComponent<SpellbookController>().cardSlots[SpellbookController.selectIndex];

            if (slot.GetComponent<Spellbook_Slot>().cardName != "")
            {
                tooltip.gameObject.SetActive(true);
                tooltip.transform.position = slot.transform.position;

                tooltip.transform.position = new Vector3(tooltip.transform.position.x - 1.35f, tooltip.transform.position.y - .5f, tooltip.transform.position.z);

                //Find item name
                GameObject tempObj = tooltip.transform.Find("Item_Name").gameObject;
                Text tempText = tempObj.GetComponent<Text>();
                tempText.text = slot.GetComponent<Spellbook_Slot>().cardName;

                //Find item type
                tempObj = tooltip.transform.Find("Item_Type").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = slot.GetComponent<Spellbook_Slot>().cardType;

                //Find item desc
                tempObj = tooltip.transform.Find("Item_Desc").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = slot.GetComponent<Spellbook_Slot>().cardDesc;

                //Find item sell
                tempObj = tooltip.transform.Find("Item_Sell1").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = "";
                tempObj = tooltip.transform.Find("Item_Sell2").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = "";
            }
            else
            {
                tooltip.gameObject.SetActive(false);
                card.SetActive(false);
            }
        }


        if (GameController.xbox360Enabled() && !InventoryController.inInv && !InventoryController.inSpellbook && !TestCharController.inTreasure)
        {
            tooltip.gameObject.SetActive(false);
            card.SetActive(false);
        }
    }

}   



