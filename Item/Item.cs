using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    // Item Name
    public string itemName;
    //Item Type: Crafting Material, Treasure, Junk, Weapon, Head, Body, Feet, Neck, Ring, Arcana
    public string itemType;
    public string itemDescription;
    public string itemIconName;

    public int itemID;
    public int itemSellCost;
    public int itemQuantity;

    public bool stackable;


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public Item(string name, string type, string descipt, string icon, int id, int sell, int quant, bool stack)
    {
        itemName = name;
        itemType = type;
        itemDescription = descipt;
        itemIconName = icon;
        itemID = id;
        itemSellCost = sell;
        itemQuantity = quant;
        stackable = stack;
    }

    public Item()
    {
        itemID = -1;
    }


    public virtual int GetSTR()
    {
        return 0;
    }

    public virtual int GetEND()
    {
        return 0;
    }

    public virtual int GetDEX()
    {
        return 0;
    }

    public virtual int GetVIT()
    {
        return 0;
    }

    public virtual int GetWIS()
    {
        return 0;
    }

    public virtual int GetINT()
    {
        return 0;
    }

    public virtual int GetDEF()
    {
        return 0;
    }

    public virtual int GetMinDMG()
    {
        return 0;
    }

    public virtual int GetMaxDMG()
    {
        return 0;
    }

}

