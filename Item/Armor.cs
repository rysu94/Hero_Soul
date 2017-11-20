using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item
{
    //defense stat
    public int defense;

    //Bonus player stats
    public int strength;
    public int dexterity;
    public int endurance;
    public int intelligence;
    public int wisdom;
    public int vitality;

    public Armor(string name, string type, string descipt, string icon, int id, int sell, int quant, bool stack, int def, int str, int dex, int end, int intel, int wis, int vit)
    {
        itemName = name;
        itemType = type;
        itemDescription = descipt;
        itemIconName = icon;
        itemID = id;
        itemSellCost = sell;
        itemQuantity = quant;
        stackable = stack;

        defense = def;

        strength = str;
        dexterity = dex;
        endurance = end;
        intelligence = intel;
        wisdom = wis;
        vitality = vit;
    }

    public override int GetSTR()
    {
        return strength;
    }

    public override int GetDEX()
    {
        return dexterity;
    }

    public override int GetEND()
    {
        return endurance;
    }

    public override int GetVIT()
    {
        return vitality;
    }

    public override int GetWIS()
    {
        return wisdom;
    }

    public override int GetINT()
    {
        return intelligence;
    }

    public override int GetDEF()
    {
        return defense;
    }

}
