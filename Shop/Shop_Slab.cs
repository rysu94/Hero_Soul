using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Shop_Slab : MonoBehaviour
{
    public string itemName;
    public string itemType;
    public string itemDesc;
    public int sell, id, quant;
    public int index = -1;
    public bool sale = false;
    public Text quantText;

    public bool buyback = false;
   
	// Use this for initialization
	void Start ()
    {
        transform.Find("Button").GetComponent<Button>().onClick.AddListener(BuyItem);
        if(quant > 0)
        {
            quantText.text = "x" + quant;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void BuyItem()
    {
        if(InventoryManager.playerGold >= sell)
        {

            //If Item is stackable
            if(GameObject.Find("ShopController").GetComponent<ItemDatabase>().FindItem(id).stackable && !buyback)
            {
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                GameObject.Find("ShopController").GetComponent<Shop_Controller>().ShowMult(id, quant, "buy?", true);
            }
            //if item is stackable and buyingback
            else if(GameObject.Find("ShopController").GetComponent<ItemDatabase>().FindItem(id).stackable && buyback)
            {
                //Find if item is in inv
                bool itemFound = false;

                for(int i = 0; i < InventoryManager.playerInventory.Length; i++)
                {
                    if(InventoryManager.playerInventory[i].itemID == id)
                    {
                        itemFound = true;
                        GameObject.Find("PurchaseNoise").GetComponent<AudioSource>().Play();
                        InventoryManager.playerInventory[i].itemQuantity += quant;
                        InventoryManager.playerGold -= GameObject.Find("ShopController").GetComponent<ItemDatabase>().FindItem(id).itemSellCost * quant;
                        Shop_Database.buybackList.RemoveAt(index);
                        GameObject.Find("ShopController").GetComponent<Shop_Controller>().UpdateShop();
                        break;
                    }
                }

                if(!itemFound)
                {
                    bool slotFound = false;
                    //find empty slot
                    for(int i = 0; i < InventoryManager.playerInventory.Length; i++)
                    {
                        if(InventoryManager.playerInventory[i].itemID == 0)
                        {
                            slotFound = true;
                            GameObject.Find("PurchaseNoise").GetComponent<AudioSource>().Play();
                            InventoryManager.playerInventory[i] = GameObject.Find("ShopController").GetComponent<ItemDatabase>().FindItem(id);
                            GameObject.Find("ShopController").GetComponent<ItemDatabase>().FindItem(id).itemQuantity = quant;
                            InventoryManager.playerGold -= GameObject.Find("ShopController").GetComponent<ItemDatabase>().FindItem(id).itemSellCost * quant;
                            Shop_Database.buybackList.RemoveAt(index);
                            GameObject.Find("ShopController").GetComponent<Shop_Controller>().UpdateShop();
                            break;
                        }
                    }

                    if(!slotFound)
                    {
                        GameObject.Find("ErrorNoise").GetComponent<AudioSource>().Play();
                    }
                }
            }

            else
            {
                //Find empty slot
                bool slotFlound = false;
                for (int i = 0; i < InventoryManager.playerInventory.Length; i++)
                {
                    if (InventoryManager.playerInventory[i].itemID == 0)
                    {
                        slotFlound = true;
                        InventoryManager.playerInventory[i] = GameObject.Find("ShopController").GetComponent<ItemDatabase>().FindItem(id);

                        if (index > -1 && !sale)
                        {
                            Shop_Database.buybackList.RemoveAt(index);
                        }

                        if (sale)
                        {
                            Shop_Database.saleList[Shop_Controller.shopIndex].RemoveAt(index);
                        }

                        //GameObject.Find("ShopController").GetComponent<Shop_Controller>().UpdateShop();
                        break;
                    }
                }

                if (slotFlound)
                {
                    GameObject.Find("PurchaseNoise").GetComponent<AudioSource>().Play();
                    InventoryManager.playerGold -= sell;
                    GameObject.Find("ShopController").GetComponent<Shop_Controller>().UpdateShop();
                }
                else
                {
                    GameObject.Find("ErrorNoise").GetComponent<AudioSource>().Play();
                }
            }
        }

        else
        {
            GameObject.Find("ErrorNoise").GetComponent<AudioSource>().Play();
        }
        
    }
}
