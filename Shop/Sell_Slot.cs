using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Sell_Slot : MonoBehaviour
{
    public string itemName;
    public string itemType;
    public string itemDesc;

    public int sell, quant, id, slot;

    // Use this for initialization
    void Start ()
    {
        //gameObject.GetComponent<Button>().onClick.AddListener(SellItem);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SellItem()
    {
        if(id != 0)
        {          
            if(quant > 0)
            {
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                GameObject.Find("ShopController").GetComponent<Shop_Controller>().ShowMult(id, quant, "sell?", false);
            }
            else
            {
                Shop_Database.buybackList.Add(GameObject.Find("ShopController").GetComponent<ItemDatabase>().FindItem(id));
                InventoryManager.playerInventory[slot] = GameObject.Find("ShopController").GetComponent<ItemDatabase>().FindItem(0);
                InventoryManager.playerGold += sell;
                GameObject.Find("PurchaseNoise").GetComponent<AudioSource>().Play();
            }
            
            GameObject.Find("ShopController").GetComponent<Shop_Controller>().UpdateShop();
        }
    }
}
