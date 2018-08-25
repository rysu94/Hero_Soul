using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Sell_Mult_Controller : MonoBehaviour
{
    public bool buying = false;

    public Text prompt, itemNum;
    public Button sellConfirm, sellBack, sellAdd, sellSub;
    public static int sellQuant;
    public static int buyQuant;

    public int itemQuant;
    public int id;

    // Use this for initialization
    void Start ()
    {
        sellAdd.onClick.AddListener(Add);
        sellSub.onClick.AddListener(Sub);
        sellBack.onClick.AddListener(Back);
        sellConfirm.onClick.AddListener(Purchase);
        itemNum.text = "1";
        sellQuant = 1;
        buyQuant = 1;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(InputManager.R_Bumper())
        {
            Add();
        }
        else if(InputManager.L_Bumper())
        {
            Sub();
        }
        else if(InputManager.A_Button())
        {
            Purchase();
        }
        else if(InputManager.B_Button())
        {
            Back();
        }
	}

    void Purchase()
    {
        //Selling
        if(!buying && InventoryManager.playerGold >= GameObject.Find("ShopController").GetComponent<ItemDatabase>().FindItem(id).itemSellCost * sellQuant)
        {
            //Find Item   
            for(int i = 0; i < InventoryManager.playerInventory.Length; i++)
            {
                if(InventoryManager.playerInventory[i].itemID == id)
                {
                    GameObject.Find("PurchaseNoise").GetComponent<AudioSource>().Play();
                    InventoryManager.playerInventory[i].itemQuantity -= sellQuant;

                    if(InventoryManager.playerInventory[i].itemQuantity <= 0)
                    {
                        InventoryManager.playerInventory[i] = GameObject.Find("ShopController").GetComponent<ItemDatabase>().FindItem(0);
                    }

                    InventoryManager.playerGold += GameObject.Find("ShopController").GetComponent<ItemDatabase>().FindItem(id).itemSellCost * sellQuant;
                    //New Item (string name, string type, string descipt, string icon, int id, int sell, int quant, bool stack)
                    Shop_Database.buybackList.Add(new Item(
                        GameObject.Find("ShopController").GetComponent<ItemDatabase>().FindItem(id).itemName,
                        GameObject.Find("ShopController").GetComponent<ItemDatabase>().FindItem(id).itemType,
                        GameObject.Find("ShopController").GetComponent<ItemDatabase>().FindItem(id).itemDescription,
                        GameObject.Find("ShopController").GetComponent<ItemDatabase>().FindItem(id).itemIconName,
                        id,
                        GameObject.Find("ShopController").GetComponent<ItemDatabase>().FindItem(id).itemSellCost,
                        sellQuant,
                        true));
                    GameObject.Find("ShopController").GetComponent<Shop_Controller>().UpdateShop();
                    break;
                }
            }

        }
        //Buying
        else if(buying && InventoryManager.playerGold >= GameObject.Find("ShopController").GetComponent<ItemDatabase>().FindItem(id).itemSellCost * buyQuant * 2)
        {
            //Search if player possesses item
            bool itemFound = false;

            for(int i = 0; i < InventoryManager.playerInventory.Length; i++)
            {
                if(InventoryManager.playerInventory[i].itemID == id)
                {
                    itemFound = true;
                    GameObject.Find("PurchaseNoise").GetComponent<AudioSource>().Play();
                    InventoryManager.playerInventory[i].itemQuantity += buyQuant;
                    InventoryManager.playerGold -= GameObject.Find("ShopController").GetComponent<ItemDatabase>().FindItem(id).itemSellCost * buyQuant * 2;                
                    GameObject.Find("ShopController").GetComponent<Shop_Controller>().UpdateShop();
                    break;
                }
            }

            //Create a new item
            if(!itemFound)
            {
                //Search for an empty space
                bool spaceFound = false;
                
                for(int i = 0; i < InventoryManager.playerInventory.Length; i++)
                {
                    if(InventoryManager.playerInventory[i].itemID == 0)
                    {
                        spaceFound = true;
                        GameObject.Find("PurchaseNoise").GetComponent<AudioSource>().Play();
                        InventoryManager.playerGold -= GameObject.Find("ShopController").GetComponent<ItemDatabase>().FindItem(id).itemSellCost * buyQuant * 2;
                        InventoryManager.playerInventory[i] = GameObject.Find("ShopController").GetComponent<ItemDatabase>().FindItem(id);
                        InventoryManager.playerInventory[i].itemQuantity = buyQuant;
                        GameObject.Find("ShopController").GetComponent<Shop_Controller>().UpdateShop();
                        break;
                    }
                }                
                if(!spaceFound)
                {
                    GameObject.Find("ErrorNoise").GetComponent<AudioSource>().Play();
                } 
                 
            }
        }
        else
        {
            GameObject.Find("ErrorNoise").GetComponent<AudioSource>().Play();
        }
        GameObject.Find("ShopController").GetComponent<Shop_Controller>().sellMult.SetActive(false);
    }


    void Back()
    {
        GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
        GameObject.Find("ShopController").GetComponent<Shop_Controller>().sellMult.SetActive(false);
    }


    void Add()
    {
        if(sellQuant < itemQuant && !buying)
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            sellQuant++;
            itemNum.text = sellQuant.ToString();
            prompt.text = "How many would you like to buy? <color=yellow>(" + GameObject.Find("ShopController").GetComponent<ItemDatabase>().FindItem(id).itemSellCost * sellQuant + " gold)</color>";
        }
        else if(buyQuant < 99 && buying)
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            buyQuant++;
            itemNum.text = buyQuant.ToString();
            prompt.text = "How many would you like to buy? <color=yellow>(" + GameObject.Find("ShopController").GetComponent<ItemDatabase>().FindItem(id).itemSellCost * buyQuant * 2 + " gold)</color>";
        }
        else
        {
            GameObject.Find("ErrorNoise").GetComponent<AudioSource>().Play();
        }
        
    }

    void Sub()
    {
        if(sellQuant > 1 && !buying)
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            sellQuant--;
            itemNum.text = sellQuant.ToString();
            prompt.text = "How many would you like to buy? <color=yellow>(" + GameObject.Find("ShopController").GetComponent<ItemDatabase>().FindItem(id).itemSellCost * sellQuant + " gold)</color>";
        }
        else if(buyQuant > 1 && buying)
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            buyQuant--;
            itemNum.text = buyQuant.ToString();
            prompt.text = "How many would you like to buy? <color=yellow>(" + GameObject.Find("ShopController").GetComponent<ItemDatabase>().FindItem(id).itemSellCost * buyQuant * 2 + " gold)</color>";
        }
        else
        {
            GameObject.Find("ErrorNoise").GetComponent<AudioSource>().Play();
        }      
    }
}
