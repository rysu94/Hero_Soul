using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static bool init = false;
    //This value will keep track of player gold
    public static int playerGold = 3000;

    //These values will keep track of player Arcana
    public static int fireArcana = 100;
    public static int waterArcana = 100;
    public static int earthArcana = 100;
    public static int windArcana = 100;
    public static int lifeArcana = 100;

    //The Player's Inventory
    public static Item[] playerInventory = new Item[36];
    //The Player's Equipment
    public static Item[] playerEquipment = new Item[7];
    //The Player's Card Spellbook
    public static bool[] playerSpellbook = new bool[41];
    //The Player's Stash
    public static Item[] playerStash = new Item[36];
    //Weiss Alc
    public static Item[] alcShopWeissInventory = new Item[27];

    public static bool inShop = false;



    void Awake()
    {
        if(!init)
        {
            for (int i = 0; i < playerInventory.Length; i++)
            {
                playerInventory[i] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
            }
            for (int i = 0; i < playerEquipment.Length; i++)
            {
                playerEquipment[i] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
            }
            for (int i = 0; i < playerSpellbook.Length; i++)
            {
                playerSpellbook[i] = false;
            }
            for (int i = 0; i < playerStash.Length; i++)
            {
                playerStash[i] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
            }
            for(int i = 0; i < alcShopWeissInventory.Length; i++)
            {
                alcShopWeissInventory[i] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
            }

            alcShopWeissInventory[0] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[7];
            alcShopWeissInventory[0].itemQuantity = 99;



            //Add starting gear to stash
            playerStash[0] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[8];
            playerStash[1] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[9];
            playerStash[2] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[10];

            playerEquipment[0] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[38];
            playerEquipment[3] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[9];
            playerEquipment[5] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[10];
            init = true;
        }
    }
        

    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    //Search if an item ID is in player's inventory
    public static int SearchItem(int id)
    {
        int itemIndex = -1;

        //Find the first index where the id's match
        for(int i = 0; i < playerInventory.Length; i++)
        {
            if(playerInventory[i].itemID == id && playerInventory[i].itemQuantity < 99)
            {
                itemIndex = i;
                break;
            }
        }
        return itemIndex;
    }
}
