using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour 
{
    public List<Item> itemData = new List<Item>();

    //For Items
    //public Item(string name, string type, string descipt, string icon, int id, int sell, int quant, bool stack)
    //For Armor:
    //public Armor(string name, string type, string descipt, string icon, int id, int sell, int quant, bool stack, int def, int str, int dex, int end, int intel, int wis, int vit)
    void Awake()
    {
        //0 No Item
        itemData.Add(new Item("", "", "", "No_Item", 0, 0, 0, false));

        //1 Fire Arcana
        itemData.Add(new Item("Fire Arcana", "Arcana", "", "Ar_Fire", 1, 0, 0, true));
        //2 Water Arcana
        itemData.Add(new Item("Water Arcana", "Arcana", "", "Ar_Water", 2, 0, 0, true));
        //3 Earth Arcana
        itemData.Add(new Item("Earth Arcana", "Arcana", "", "Ar_Earth", 3, 0, 0, true));
        //4 Air Arcana
        itemData.Add(new Item("Wind Arcana", "Arcana", "", "Ar_Air", 4, 0, 0, true));
        //5 Life Arcana
        itemData.Add(new Item("Life Arcana", "Arcana", "", "Ar_Life", 5, 0, 0, true));

        //6 Small Feather
        itemData.Add(new Item("Small Feather", "Crafting Material", "A small feather.", "I_Feather01", 6, 3, 0, true));
        //7 Red Mushroom
        itemData.Add(new Item("Red Mushroom", "Crafting Material", "A small red mushroom.", "I_RedMush", 7, 2, 0, true));

        //Cecilia Start Gear

        //8 Worn Spear
        itemData.Add(new Weapon("Worn Spear", "Weapon", "A old worn spear.\n<color=red>Base: 3-4 Damage</color>", "W_Spear001", 8, 1, 0, false, 3, 4, 0, 0, 0, 0, 0, 0));
        //9 Casual Cloths
        itemData.Add(new Armor("Casual Cloths", "Body", "Light Fashionable Clothing.\n<color=yellow>Base: +2 Defense</color>", "A_Clothing01", 9, 1, 0, false, 2, 0, 0, 0, 0, 0, 0));
        //10 Adventurer's Loafers
        itemData.Add(new Armor("Adventurer Loafers", "Feet", "Comfortable and functional, for the on the go adventurer.\n<color=yellow>Base: +1 Defense</color>", "A_Shoes01", 10, 1, 0, false, 1, 0, 0, 0, 0, 0, 0));
        
        //Copper Bands
        
        //11 Mighty Copper Band
        itemData.Add(new Armor("<color=#00ff00ff>Mighty Copper Band</color>", "Ring", "A simple copper band imbued with arcana.\n<color=#00ff00ff>Base: +5 Strength</color>", "Ac_Ring01B", 11, 150, 0, false, 0, 5, 0, 0, 0, 0, 0));
        //12 Nimble Copper Band
        itemData.Add(new Armor("<color=#00ff00ff>Nimble Copper Band</color>", "Ring", "A simple copper band imbued with arcana.\n<color=#00ff00ff>Base: +5 Dexterity</color>", "Ac_Ring01C", 12, 150, 0, false, 0, 0, 5, 0, 0, 0, 0));
        //13 Arcane Copper Band
        itemData.Add(new Armor("<color=#00ff00ff>Arcane Copper Band</color>", "Ring", "A simple copper band imbued with arcana.\n<color=#00ff00ff>Base: +5 Intelligence</color>", "Ac_Ring01D", 13, 150, 0, false, 0, 0, 0, 0, 5, 0, 0));
        //14 Enduring Copper Band
        itemData.Add(new Armor("<color=#00ff00ff>Enduring Copper Band</color>", "Ring", "A simple copper band imbued with arcana.\n<color=#00ff00ff>Base: +5 Endurance</color>", "Ac_Ring01A", 14, 150, 0, false, 0, 0, 0, 5, 0, 0, 0));
        //15 Sage Copper Band
        itemData.Add(new Armor("<color=#00ff00ff>Sage Copper Band</color>", "Ring", "A simple copper band imbued with arcana.\n<color=#00ff00ff>Base: +5 Wisdom</color>", "Ac_Ring01E", 15, 150, 0, false, 0, 0, 0, 0, 0, 5, 0));
        //16 Copper Band
        itemData.Add(new Armor("<color=#00ff00ff>Robust Copper Band</color>", "Ring", "A simple copper band.\n<color=#00ff00ff>Base: +5 Vitality</color>", "Ac_Ring01F", 16, 150, 0, false, 0, 0, 0, 0, 0, 0, 5));
        
        //Iron Bands
        
        //17 Mighty Iron Band
        itemData.Add(new Armor("<color=lightblue>Mighty Iron Band</color>", "Ring", "A simple iron band inbued with arcana.\n<color=#00ff00ff>Base: +10 Strength</color>", "Ac_Ring02B", 17, 100, 0, false, 0, 10, 0, 0, 0, 0, 0));
        //18 Nimble Iron Band
        itemData.Add(new Armor("<color=lightblue>Nimble Iron Band</color>", "Ring", "A simple iron band inbued with arcana.\n<color=#00ff00ff>Base: +10 Dexterity</color>", "Ac_Ring02C", 18, 100, 0, false, 0, 0, 10, 0, 0, 0, 0));
        //19 Arcane Iron Band
        itemData.Add(new Armor("<color=lightblue>Arcane Iron Band</color>", "Ring", "A simple iron band inbued with arcana.\n<color=#00ff00ff>Base: +10 Intelligence</color>", "Ac_Ring02D", 19, 100, 0, false, 0, 0, 0, 0, 10, 0, 0));
        //20 Enduring Iron Band
        itemData.Add(new Armor("<color=lightblue>Enduring Iron Band</color>", "Ring", "A simple iron band imbued with arcana.\n<color=#00ff00ff>Base: +10 Endurance</color>", "Ac_Ring02A", 20, 100, 0, false, 0, 0, 0, 10, 0, 0, 0));
        //21 Sage Iron Band
        itemData.Add(new Armor("<color=lightblue>Sage Iron Band</color>", "Ring", "A simple iron band imbued with arcana.\n<color=#00ff00ff>Base: +10 Wisdom</color>", "Ac_Ring02E", 21, 100, 0, false, 0, 0, 0, 0, 0, 10, 0));
        //22 Iron Band
        itemData.Add(new Armor("<color=lightblue>Iron Band</color>", "Ring", "A simple iron band.\n<color=#00ff00ff>Base: +10 Vitality</color>", "Ac_Ring02F", 22, 100, 0, false, 0, 0, 0, 0, 0, 0, 10));

        //Gold Bands

        //23 Mighty Gold Band
        itemData.Add(new Armor("<color=magenta>Mighty Gold Band</color>", "Ring", "A simple gold band imbued with arcana.\n<color=#00ff00ff>Base: +15 Strength</color>", "Ac_Ring03B", 23, 250, 0, false, 0, 15, 0, 0, 0, 0, 0));
        //24 Nimble Gold Band
        itemData.Add(new Armor("<color=magenta>Nimble Gold Band</color>", "Ring", "A simple gold band imbued with arcana.\n<color=#00ff00ff>Base: +15 Dexterity</color>", "Ac_Ring03C", 24, 250, 0, false, 0, 0, 15, 0, 0, 0, 0));
        //25 Arcane Gold Band
        itemData.Add(new Armor("<color=magenta>Arcane Gold Band</color>", "Ring", "A simple gold band imbued with arcana.\n<color=#00ff00ff>Base: +15 Intelligence</color>", "Ac_Ring03D", 25, 250, 0, false, 0, 0, 0, 0, 15, 0, 0));
        //26 Enduring Gold Band
        itemData.Add(new Armor("<color=magenta>Enduring Gold Band</color>", "Ring", "A simple gold band imbued with arcana.\n<color=#00ff00ff>Base: +15 Endurance</color>", "Ac_Ring03A", 26, 250, 0, false, 0, 0, 0, 15, 0, 0, 0));
        //27 Sage Gold Band
        itemData.Add(new Armor("<color=magenta>Sage Gold Band</color>", "Ring", "A simple gold band imbued with arcana.\n<color=#00ff00ff>Base: +15 Wisdom</color>", "Ac_Ring03E", 27, 250, 0, false, 0, 0, 0, 0, 0, 15, 0));
        //28 Gold Band
        itemData.Add(new Armor("<color=magenta>Gold Band</color>", "Ring", "A simple gold band.\n<color=#00ff00ff>Base: +15 Vitality</color>", "Ac_Ring03F", 28, 250, 0, false, 0, 0, 0, 0, 0, 0, 15));

        //29 Arcana Voucher
        itemData.Add(new Item("Arcana Voucher", "Quest Item", "An aged voucher entitling the holder a starter set of Arcana", "I_Scroll", 29, 5, 0, true));

        //30 G-Slime Crystal
        itemData.Add(new Item("G-Slime Crystal", "Treasure", "A small green crystal created by slimes when they are under stress. Can be sold for gold.", "I_Jade", 30, 50, 0, true));

        //Small Pendants

        //31 Pendent of Might
        itemData.Add(new Armor("<color=#00ff00ff>Pendent of Might</color>", "Neck", "A pendent with a small stone imbued with arcana.\n<color=#00ff00ff>Base: +5 Strength</color>", "Ac_Necklace02B", 31, 150, 0, false, 0, 5, 0, 0, 0, 0, 0));
        //32 Pendent of Agility
        itemData.Add(new Armor("<color=#00ff00ff>Pendent of Agility</color>", "Neck", "A pendent with a small stone imbued with arcana.\n<color=#00ff00ff>Base: +5 Dexterity</color>", "Ac_Necklace02C", 32, 150, 0, false, 0, 0, 5, 0, 0, 0, 0));
        //33 Pendent of Endurance
        itemData.Add(new Armor("<color=#00ff00ff>Pendent of Endurance</color>", "Neck", "A pendent with a small stone imbued with arcana.\n<color=#00ff00ff>Base: +5 Endurance</color>", "Ac_Necklace02D", 33, 150, 0, false, 0, 0, 0, 5, 0, 0, 0));
        //34 Pendent of Intelligence
        itemData.Add(new Armor("<color=#00ff00ff>Pendent of Intelligence</color>", "Neck", "A pendent with a small stone imbued with arcana.\n<color=#00ff00ff>Base: +5 Intelligence</color>", "Ac_Necklace02A", 34, 150, 0, false, 0, 0, 0, 0, 5, 0, 0));
        //35 Pendent of Wisdom
        itemData.Add(new Armor("<color=#00ff00ff>Pendent of Wisdom</color>", "Neck", "A pendent with a small stone imbued with arcana.\n<color=#00ff00ff>Base: +5 Wisdom</color>", "Ac_Necklace02E", 35, 150, 0, false, 0, 0, 0, 0, 0, 5, 0));
        //36 Small Pendent
        itemData.Add(new Armor("<color=#00ff00ff>Pendent of Vitality</color>", "Neck", "A pedent with a small stone imbued with arcana.\n<color=#00ff00ff>Base: +5 Vitality</color>", "Ac_Necklace02F", 36, 150, 0, false, 0, 0, 0, 0, 0, 0, 5));

        //37 Hardened Carapace
        itemData.Add(new Item("Hard Carapace", "Crafting Material", "A small piece of a hard shell.", "I_SolidShell_1", 37, 5, 0, true));

        //Spears

        //38 Worn Spear
        itemData.Add(new Weapon("Worn Spear", "Weapon", "A old worn spear.\n<color=red>Base: 5-7 Damage</color>", "W_Spear001", 38, 1, 0, false, 5, 7, 0, 0, 0, 0, 0, 0));
        //39 Training Spear
        itemData.Add(new Weapon("Training Spear", "Weapon", "A dull lightweight spear used to train novices.\n<color=red>Base: 7-9 Damage</color>", "W_Spear002", 39, 50, 0, false, 7, 9, 0, 0, 0, 0, 0, 0));
        
        //40 Iron Spear
        itemData.Add(new Weapon("Iron Spear", "Weapon", "A sturdy spear made out of iron. \n<color=red>Base: 9-12 Damage</color>", "W_Spear003", 40, 250, 0, false, 9, 12, 0, 0, 0, 0, 0, 0));
        //41 Mighty Iron Spear
        itemData.Add(new Weapon("<color=#00ff00ff>Mighty Iron Spear</color>", "Weapon", "A sturdy spear made out of iron infused with arcana. \n<color=red>Base: 10-13 Damage</color> \n<color=#00ff00ff>+7 Strength</color>", "W_Spear003_A", 41, 300, 0, false, 10, 13, 7, 0, 0, 0, 0, 0));
        //42 Nimble Iron Spear
        itemData.Add(new Weapon("<color=#00ff00ff>Nimble Iron Spear</color>", "Weapon", "A sturdy spear made out of iron infused with arcana. \n<color=red>Base: 10-13 Damage</color> \n<color=#00ff00ff>+7 Dexterity</color>", "W_Spear003_B", 42, 300, 0, false, 10, 13, 0, 7, 0, 0, 0, 0));
        //43 Enduring Iron Spear
        itemData.Add(new Weapon("<color=#00ff00ff>Enduring Iron Spear</color>", "Weapon", "A sturdy spear made out of iron infused with arcana. \n<color=red>Base: 10-13 Damage</color> \n<color=#00ff00ff>+7 Endurance</color>", "W_Spear003_C", 43, 300, 0, false, 10, 13, 0, 0, 7, 0, 0, 0));
        //44 Arcane Iron Spear
        itemData.Add(new Weapon("<color=#00ff00ff>Arcane Iron Spear</color>", "Weapon", "A sturdy spear made out of iron infused with arcana. \n<color=red>Base: 10-13 Damage</color> \n<color=#00ff00ff>+7 Intelligence</color>", "W_Spear003_D", 44, 300, 0, false, 10, 13, 0, 0, 0, 7, 0, 0));
        //45 Sage Iron Spear
        itemData.Add(new Weapon("<color=#00ff00ff>Sage Iron Spear</color>", "Weapon", "A sturdy spear made out of iron infused with arcana. \n<color=red>Base: 10-13 Damage</color> \n<color=#00ff00ff>+7 Wisdom</color>", "W_Spear003_E", 45, 300, 0, false, 10, 13, 0, 0, 0, 0, 7, 0));
        //46 Robust Iron Spear
        itemData.Add(new Weapon("<color=#00ff00ff>Robust Iron Spear</color>", "Weapon", "A sturdy spear made out of iron infused with arcana. \n<color=red>Base: 10-13 Damage</color> \n<color=#00ff00ff>+7 Vitality</color>", "W_Spear003_F", 46, 300, 0, false, 10, 13, 0, 0, 0, 0, 0, 7));
        //47 Warring Iron Spear
        itemData.Add(new Weapon("<color=lightblue>Warring Iron Spear</color>", "Weapon", "A sturdy spear made out of iron infused with arcana. \n<color=red>Base: 12-15 Damage</color> \n<color=#00ff00ff>+7 Strength</color> \n<color=#00ff00ff>+7 Dexterity</color>", "W_Spear003_G", 47, 375, 0, false, 12, 15, 7, 7, 0, 0, 0, 0));
        //48 Potent Iron Spear
        itemData.Add(new Weapon("<color=lightblue>Potent Iron Spear</color>", "Weapon", "A sturdy spear made out of iron infused with arcana. \n<color=red>Base: 12-15 Damage</color> \n<color=#00ff00ff>+7 Intelligence</color> \n<color=#00ff00ff>+7 Wisdom</color>", "W_Spear003_H", 48, 375, 0, false, 12, 15, 0, 0, 0, 7, 7, 0));
        //49 Hardy Iron Spear
        itemData.Add(new Weapon("<color=lightblue>Hardy Iron Spear</color>", "Weapon", "A sturdy spear made out of iron infused with arcana. \n<color=red>Base: 12-15 Damage</color> \n<color=#00ff00ff>+7 Endurance</color> \n<color=#00ff00ff>+7 Vitality</color>", "W_Spear003_I", 49, 375, 0, false, 12, 15, 0, 0, 7, 0, 0, 7));
        //50 Knightly Iron Spear
        itemData.Add(new Weapon("<color=magenta>Knightly Iron Spear</color>", "Weapon", "A sturdy spear made out of iron infused with arcana. \n<color=red>Base: 14-17 Damage</color> \n<color=#00ff00ff>+7 Strength</color> \n<color=#00ff00ff>+7 Dexterity</color>\n<color=#00ff00ff>+7 Endurance</color> ", "W_Spear003_J", 50, 475, 0, false, 14, 17, 7, 7, 7, 0, 0, 0));
        //51 Ancient Iron Spear
        itemData.Add(new Weapon("<color=magenta>Ancient Iron Spear</color>", "Weapon", "A sturdy spear made out of iron infused with arcana. \n<color=red>Base: 14-17 Damage</color> \n<color=#00ff00ff>+7 Intelligence</color> \n<color=#00ff00ff>+7 Wisdom</color>\n<color=#00ff00ff>+7 Vitality</color> ", "W_Spear003_K", 51, 475, 0, false, 14, 17, 0, 0, 0, 7, 7, 7));
        //52 Omni Iron Spear
        itemData.Add(new Weapon("<color=orange>Omni Iron Spear</color>", "Weapon", "A sturdy spear made out of iron infused with arcana. \n<color=red>Base: 16-19 Damage</color> \n<color=#00ff00ff>+7 Strength</color>   <color=#00ff00ff> +7 Dexterity</color> \n<color=#00ff00ff>+7 Endurance</color> <color=#00ff00ff> +7 Intelligence</color> \n<color=#00ff00ff>+7 Wisdom</color>      <color=#00ff00ff> +7 Vitality</color> ", "W_Spear003_L", 52, 600, 0, false, 16, 19, 7, 7, 7, 7, 7, 7));
       
        //53 Steel Spear
        itemData.Add(new Weapon("Steel Spear", "Weapon", "An sturdy spear made out of steel. \n<color=red>Base: 28-36 Damage</color>", "W_Spear005", 53, 250, 0, false, 28, 36, 0, 0, 0, 0, 0, 0));
        //54 Mighty Steel Spear
        itemData.Add(new Weapon("<color=#00ff00ff>Mighty Steel Spear</color>", "Weapon", "A sturdy spear made out of steel infused with arcana. \n<color=red>Base: 32-40 Damage</color> \n<color=#00ff00ff>+14 Strength</color>", "W_Spear005_A", 54, 350, 0, false, 32, 40, 14, 0, 0, 0, 0, 0));
        //55 Nimble Steel Spear
        itemData.Add(new Weapon("<color=#00ff00ff>Nimble Steel Spear</color>", "Weapon", "A sturdy spear made out of steel infused with arcana. \n<color=red>Base: 32-40 Damage</color> \n<color=#00ff00ff>+14 Dexterity</color>", "W_Spear005_B", 55, 350, 0, false, 32, 40, 0, 14, 0, 0, 0, 0));
        //56 Enduring Steel Spear
        itemData.Add(new Weapon("<color=#00ff00ff>Enduring Steel Spear</color>", "Weapon", "A sturdy spear made out of steel infused with arcana. \n<color=red>Base: 32-40 Damage</color> \n<color=#00ff00ff>+14 Endurance</color>", "W_Spear005_C", 56, 350, 0, false, 32, 40, 0, 0, 14, 0, 0, 0));
        //57 Arcane Steel Spear
        itemData.Add(new Weapon("<color=#00ff00ff>Arcane Steel Spear</color>", "Weapon", "A sturdy spear made out of steel infused with arcana. \n<color=red>Base: 32-40 Damage</color> \n<color=#00ff00ff>+14 Intelligence</color>", "W_Spear005_D", 57, 350, 0, false, 32, 40, 0, 0, 0, 14, 0, 0));
        //58 Sage Steel Spear
        itemData.Add(new Weapon("<color=#00ff00ff>Sage Steel Spear</color>", "Weapon", "A sturdy spear made out of steel infused with arcana. \n<color=red>Base: 32-40 Damage</color> \n<color=#00ff00ff>+14 Wisdom</color>", "W_Spear005_E", 58, 350, 0, false, 32, 40, 0, 0, 0, 0, 14, 0));
        //59 Robust Steel Spear
        itemData.Add(new Weapon("<color=#00ff00ff>Robust Steel Spear</color>", "Weapon", "A sturdy spear made out of steel infused with arcana. \n<color=red>Base: 32-40 Damage</color> \n<color=#00ff00ff>+14 Strength</color>", "W_Spear005_F", 59, 350, 0, false, 32, 40, 0, 0, 0, 0, 0, 14));
        //60 Warring Steel Spear
        itemData.Add(new Weapon("<color=lightblue>Warring Steel Spear</color>", "Weapon", "A sturdy spear made out of steel infused with arcana. \n<color=red>Base: 36-44 Damage</color> \n<color=#00ff00ff>+14 Strength</color> \n<color=#00ff00ff>+14 Dexterity</color>", "W_Spear005_G", 60, 450, 0, false, 36, 44, 14, 14, 0, 0, 0, 0));
        //61 Potent Steel Spear
        itemData.Add(new Weapon("<color=lightblue>Potent Steel Spear</color>", "Weapon", "A sturdy spear made out of steel infused with arcana. \n<color=red>Base: 36-44 Damage</color> \n<color=#00ff00ff>+14 Intelligence</color> \n<color=#00ff00ff>+14 Wisdom</color>", "W_Spear005_H", 61, 450, 0, false, 36, 44, 0, 0, 0, 14, 14, 0));
        //62 Hardy Steel Spear
        itemData.Add(new Weapon("<color=lightblue>Hardy Steel Spear</color>", "Weapon", "A sturdy spear made out of steel infused with arcana. \n<color=red>Base: 36-44 Damage</color> \n<color=#00ff00ff>+14 Endurance</color> \n<color=#00ff00ff>+14 Vitality</color>", "W_Spear005_I", 62, 450, 0, false, 36, 44, 0, 0, 14, 0, 0, 14));
        //63 Knightly Steel Spear
        itemData.Add(new Weapon("<color=magenta>Knightly Steel Spear</color>", "Weapon", "A sturdy spear made out of steel infused with arcana. \n<color=red>Base: 40-48 Damage</color> \n<color=#00ff00ff>+14 Strength</color> \n<color=#00ff00ff>+14 Dexterity</color>\n<color=#00ff00ff>+14 Endurance</color>", "W_Spear005_J", 63, 550, 0, false, 40, 48, 14, 14, 14, 0, 0, 0));
        //64 Ancient Steel Spear
        itemData.Add(new Weapon("<color=magenta>Ancient Steel Spear</color>", "Weapon", "A sturdy spear made out of steel infused with arcana. \n<color=red>Base: 40-48 Damage</color> \n<color=#00ff00ff>+14 Intelligence</color> \n<color=#00ff00ff>+14 Wisdom</color>\n<color=#00ff00ff>+14 Vitality</color>", "W_Spear005_K", 64, 550, 0, false, 40, 48, 0, 0, 0, 14, 14, 14));
        //65 Omni Steel Spear
        itemData.Add(new Weapon("<color=orange>Omni Steel Spear</color>", "Weapon", "A sturdy spear made out of steel infused with arcana. \n<color=red>Base: 40-48 Damage</color> \n<color=#00ff00ff>+14 Strength</color>   <color=#00ff00ff> +14 Dexterity</color> \n<color=#00ff00ff>+14 Endurance</color> <color=#00ff00ff> +14 Intelligence</color> \n<color=#00ff00ff>+14 Wisdom</color>      <color=#00ff00ff> +14 Vitality</color> ", "W_Spear005_L", 65, 650, 0, false, 44, 52, 14, 14, 14, 14, 14, 14));

        //66 Mythril Spear
        itemData.Add(new Weapon("Mythril Spear", "Weapon", "A sturdy spear made out of the rare metal, mythril. \n<color=red>Base: 40-48 Damage</color>", "W_Spear012", 66, 500, 0, false, 40, 48, 0, 0, 0, 0, 0, 0));
        //67 Mighty Mythril Spear
        itemData.Add(new Weapon("<color=#00ff00ff>Mighty Mythril Spear</color>", "Weapon", "A sturdy spear made out of the rare metal, mythril and infused with arcana. \n<color=red>Base: 45-53 Damage</color> \n<color=#00ff00ff>+21 Strength</color>", "W_Spear011_A", 67, 650, 0, false, 45, 53, 21, 0, 0, 0, 0, 0));
        //68 Nimble Mythril Spear
        itemData.Add(new Weapon("<color=#00ff00ff>Nimble Mythril Spear</color>", "Weapon", "A sturdy spear made out of the rare metal, mythril and infused with arcana. \n<color=red>Base: 45-53 Damage</color> \n<color=#00ff00ff>+21 Dexterity</color>", "W_Spear011_B", 68, 650, 0, false, 45, 53, 0, 21, 0, 0, 0, 0));
        //69 Enduring Mythril Spear
        itemData.Add(new Weapon("<color=#00ff00ff>Enduring Mythril Spear</color>", "Weapon", "A sturdy spear made out of the rare metal, mythril and infused with arcana. \n<color=red>Base: 45-53 Damage</color> \n<color=#00ff00ff>+21 Endurance</color>", "W_Spear011_C", 69, 650, 0, false, 45, 53, 0, 0, 21, 0, 0, 0));
        //70 Arcane Mythril Spear
        itemData.Add(new Weapon("<color=#00ff00ff>Arcane Mythril Spear</color>", "Weapon", "A sturdy spear made out of the rare metal, mythril and infused with arcana. \n<color=red>Base: 45-53 Damage</color> \n<color=#00ff00ff>+21 Intelligence</color>", "W_Spear011_D", 70, 650, 0, false, 45, 53, 0, 0, 0, 21, 0, 0));
        //71 Sage Mythril Spear
        itemData.Add(new Weapon("<color=#00ff00ff>Sage Mythril Spear</color>", "Weapon", "A sturdy spear made out of the rare metal, mythril and infused with arcana. \n<color=red>Base: 45-53 Damage</color> \n<color=#00ff00ff>+21 Wisdom</color>", "W_Spear011_E", 71, 650, 0, false, 45, 53, 0, 0, 0, 0, 21, 0));
        //72 Robust Mythril Spear
        itemData.Add(new Weapon("<color=#00ff00ff>Robust Mythril Spear</color>", "Weapon", "A sturdy spear made out of the rare metal, mythril and infused with arcana. \n<color=red>Base: 45-53 Damage</color> \n<color=#00ff00ff>+21 Wisdom</color>", "W_Spear011_F", 72, 650, 0, false, 45, 53, 0, 0, 0, 0, 0, 21));
        //73 Warring Mythril Spear
        itemData.Add(new Weapon("<color=lightblue>Warring Mythril Spear</color>", "Weapon", "A sturdy spear made out of the rare metal, mythril and infused with arcana. \n<color=red>Base: 50-58 Damage</color> \n<color=#00ff00ff>+21 Strength</color> \n<color=#00ff00ff>+21 Dexterity</color>", "W_Spear011_G", 73, 800, 0, false, 45, 53, 21, 21, 0, 0, 0, 0));
        //74 Potent Mythril Spear
        itemData.Add(new Weapon("<color=lightblue>Potent Mythril Spear</color>", "Weapon", "A sturdy spear made out of the rare metal, mythril and infused with arcana. \n<color=red>Base: 50-58 Damage</color> \n<color=#00ff00ff>+21 Intelligence</color> \n<color=#00ff00ff>+21 Wisdom</color>", "W_Spear011_H", 74, 800, 0, false, 45, 53, 0, 0, 0, 21, 21, 0));
        //75 Hardy Mythril Spear
        itemData.Add(new Weapon("<color=lightblue>Hardy Mythril Spear</color>", "Weapon", "A sturdy spear made out of the rare metal, mythril and infused with arcana. \n<color=red>Base: 50-58 Damage</color> \n<color=#00ff00ff>+21 Endurance</color> \n<color=#00ff00ff>+21 Vitality</color>", "W_Spear011_I", 75, 800, 0, false, 45, 53, 0, 0, 21, 0, 0, 21));
        //76 Knightly Mythril Spear
        itemData.Add(new Weapon("<color=magenta>Knightly Mythril Spear</color>", "Weapon", "A sturdy spear made out of the rare metal, mythril and infused with arcana. \n<color=red>Base: 55-63 Damage</color> \n<color=#00ff00ff>+21 Strength</color> \n<color=#00ff00ff>+21 Dexterity</color>\n<color=#00ff00ff>+21 Endurance</color>", "W_Spear011_J", 76, 950, 0, false, 45, 53, 21, 21, 21, 0, 0, 0));
        //77 Ancient Mythril Spear
        itemData.Add(new Weapon("<color=magenta>Ancient Mythril Spear</color>", "Weapon", "A sturdy spear made out of the rare metal, mythril and infused with arcana. \n<color=red>Base: 55-63 Damage</color> \n<color=#00ff00ff>+21 Intelligence</color> \n<color=#00ff00ff>+21 Wisdom</color>\n<color=#00ff00ff>+21 Vitality</color>", "W_Spear011_K", 77, 950, 0, false, 45, 53, 0, 0, 0, 21, 21, 21));
        //78 Omni Mythril Spear
        itemData.Add(new Weapon("<color=orange>Omni Mythril Spear</color>", "Weapon", "A sturdy spear made out of steel infused with arcana. \n<color=red>Base: 60-68 Damage</color> \n<color=#00ff00ff>+21 Strength</color>   <color=#00ff00ff> +21 Dexterity</color> \n<color=#00ff00ff>+21 Endurance</color> <color=#00ff00ff> +21 Intelligence</color> \n<color=#00ff00ff>+21 Wisdom</color>      <color=#00ff00ff> +21 Vitality</color> ", "W_Spear011_L", 78, 1100, 0, false, 60, 68, 21, 21, 21, 21, 21, 21));

        //79 Unidentified Spear
        itemData.Add(new Item("<color=yellow>Unidentified Spear</color>", "Treasure", "An unidentified spear. Inspect to reveal it!", "W_Spear001_A", 79, 250, 0, false));
        //80 Unidentified Necklace
        itemData.Add(new Item("<color=yellow>Unidentified Pendant</color>", "Treasure", "An unidentified pendant. Inspect to reveal it!", "Ac_Necklace02G", 80, 100, 0, false));
        //81 Unidentified Ring
        itemData.Add(new Item("<color=yellow>Unidentified Ring</color>", "Treasure", "An unidentified ring. Inspect to reveal it!", "Ac_Ring01G", 81, 100, 0, false));

        //82 Last Rites
        itemData.Add(new Weapon("<color=lightblue>Last Rites</color>", "Weapon", "A rare spear used by a legendary mercenary from a far away land. It's very light but the edge is very sharp. \n<color=red>Base: 20-28 Damage</color> \n<color=#00ff00ff>+5 Strength</color> \n<color=#00ff00ff>+14 Dexterity</color> \n<color=#00ff00ff>+5 Endurance</color>", "W_Spear004", 82, 250, 0, false, 20, 28, 5, 14, 5, 0, 0, 0));
        //83 Judicator
        itemData.Add(new Weapon("<color=lightblue>Judicator</color>", "Weapon", "A rare spear used by a legendary mercenary from a far away land. It's very light but the edge is very sharp. \n<color=red>Base: 20-28 Damage</color> \n<color=#00ff00ff>+5 Strength</color> \n<color=#00ff00ff>+14 Dexterity</color> \n<color=#00ff00ff>+5 Endurance</color>", "W_Spear004", 83, 250, 0, false, 20, 28, 5, 14, 5, 0, 0, 0));
        //84 Sky Talon
        itemData.Add(new Weapon("<color=lightblue>Sky Talon</color>", "Weapon", "A rare spear used by a legendary mercenary from a far away land. It's very light but the edge is very sharp. \n<color=red>Base: 20-28 Damage</color> \n<color=#00ff00ff>+5 Strength</color> \n<color=#00ff00ff>+14 Dexterity</color> \n<color=#00ff00ff>+5 Endurance</color>", "W_Spear004", 84, 250, 0, false, 20, 28, 5, 14, 5, 0, 0, 0));
        //85 Needler
        itemData.Add(new Weapon("<color=lightblue>Needler</color>", "Weapon", "A rare spear used by a legendary mercenary from a far away land. It's very light but the edge is very sharp. \n<color=red>Base: 20-28 Damage</color> \n<color=#00ff00ff>+5 Strength</color> \n<color=#00ff00ff>+14 Dexterity</color> \n<color=#00ff00ff>+5 Endurance</color>", "W_Spear004", 85, 250, 0, false, 20, 28, 5, 14, 5, 0, 0, 0));
        //86 Demonbane
        itemData.Add(new Weapon("<color=lightblue>Demonbane</color>", "Weapon", "A rare spear used by a legendary mercenary from a far away land. It's very light but the edge is very sharp. \n<color=red>Base: 20-28 Damage</color> \n<color=#00ff00ff>+5 Strength</color> \n<color=#00ff00ff>+14 Dexterity</color> \n<color=#00ff00ff>+5 Endurance</color>", "W_Spear004", 86, 250, 0, false, 20, 28, 5, 14, 5, 0, 0, 0));
        //87 Harbinger
        itemData.Add(new Weapon("<color=lightblue>Harbinger</color>", "Weapon", "A rare spear used by a legendary mercenary from a far away land. It's very light but the edge is very sharp. \n<color=red>Base: 20-28 Damage</color> \n<color=#00ff00ff>+5 Strength</color> \n<color=#00ff00ff>+14 Dexterity</color> \n<color=#00ff00ff>+5 Endurance</color>", "W_Spear004", 87, 250, 0, false, 20, 28, 5, 14, 5, 0, 0, 0));
        //88 Eclipse
        itemData.Add(new Weapon("<color=lightblue>Eclipse</color>", "Weapon", "A rare spear used by a legendary mercenary from a far away land. It's very light but the edge is very sharp. \n<color=red>Base: 20-28 Damage</color> \n<color=#00ff00ff>+5 Strength</color> \n<color=#00ff00ff>+14 Dexterity</color> \n<color=#00ff00ff>+5 Endurance</color>", "W_Spear004", 88, 250, 0, false, 20, 28, 5, 14, 5, 0, 0, 0));
        //89 Thundercaller
        itemData.Add(new Weapon("<color=lightblue>Thundercaller</color>", "Weapon", "A rare spear used by a legendary mercenary from a far away land. It's very light but the edge is very sharp. \n<color=red>Base: 20-28 Damage</color> \n<color=#00ff00ff>+5 Strength</color> \n<color=#00ff00ff>+14 Dexterity</color> \n<color=#00ff00ff>+5 Endurance</color>", "W_Spear004", 89, 250, 0, false, 20, 28, 5, 14, 5, 0, 0, 0));
        //90 Dawnbreak
        itemData.Add(new Weapon("<color=lightblue>Dawnbreak</color>", "Weapon", "A rare spear used by a legendary mercenary from a far away land. It's very light but the edge is very sharp. \n<color=red>Base: 20-28 Damage</color> \n<color=#00ff00ff>+5 Strength</color> \n<color=#00ff00ff>+14 Dexterity</color> \n<color=#00ff00ff>+5 Endurance</color>", "W_Spear004", 90, 250, 0, false, 20, 28, 5, 14, 5, 0, 0, 0));
        //91 Heavensfall
        itemData.Add(new Weapon("<color=lightblue>Heavensfall</color>", "Weapon", "A rare spear used by a legendary mercenary from a far away land. It's very light but the edge is very sharp. \n<color=red>Base: 20-28 Damage</color> \n<color=#00ff00ff>+5 Strength</color> \n<color=#00ff00ff>+14 Dexterity</color> \n<color=#00ff00ff>+5 Endurance</color>", "W_Spear004", 91, 250, 0, false, 20, 28, 5, 14, 5, 0, 0, 0));

        //92 Wayfarer Cloths
        itemData.Add(new Armor("Wayfarer Cloths", "Body", "Clothing for the on the go adventurer.\n<color=yellow>Base: +4 Defense</color>", "A_Clothing02", 92, 75, 0, false, 4, 0, 0, 0, 0, 0, 0));
        //93 Leather Armor
        itemData.Add(new Armor("Leather Armor", "Body", "Light armor made from leather.\n<color=yellow>Base: +6 Defense</color>", "A_Armour01", 93, 150, 0, false, 6, 0, 0, 0, 0, 0, 0));
        //94 Mighty Leather Armor
        itemData.Add(new Armor("<color=lime>Mighty Leather Armor</color>", "Body", "Light armor made from leather and imbued with arcana.\n<color=yellow>Base: +7 Defense</color>\n<color=lime>+5 Strength</color>", "A_Armour01_A", 94, 200, 0, false, 7, 5, 0, 0, 0, 0, 0));
        //95 Nimble Leather Armor
        itemData.Add(new Armor("<color=lime>Nimble Leather Armor</color>", "Body", "Light armor made from leather and imbued with arcana.\n<color=yellow>Base: +7 Defense</color>\n<color=lime>+5 Dexterity</color>", "A_Armour01_B", 95, 200, 0, false, 7, 0, 5, 0, 0, 0, 0));
        //96 Enduring Leather Armor
        itemData.Add(new Armor("<color=lime>Enduring Leather Armor</color>", "Body", "Light armor made from leather and imbued with arcana.\n<color=yellow>Base: +7 Defense</color>\n<color=lime>+5 Endurance</color>", "A_Armour01_C", 96, 200, 0, false, 7, 0, 0, 5, 0, 0, 0));
        //97 Arcane Leather Armor
        itemData.Add(new Armor("<color=lime>Arcane Leather Armor</color>", "Body", "Light armor made from leather and imbued with arcana.\n<color=yellow>Base: +7 Defense</color>\n<color=lime>+5 Intelligence</color>", "A_Armour01_D", 97, 200, 0, false, 7, 0, 0, 0, 5, 0, 0));
        //98 Sage Leather Armor
        itemData.Add(new Armor("<color=lime>Sage Leather Armor</color>", "Body", "Light armor made from leather and imbued with arcana.\n<color=yellow>Base: +7 Defense</color>\n<color=lime>+5 Wisdom</color>", "A_Armour01_E", 98, 200, 0, false, 7, 0, 0, 0, 0, 5, 0));
        //99 Robust Leather Armor
        itemData.Add(new Armor("<color=lime>Robust Leather Armor</color>", "Body", "Light armor made from leather and imbued with arcana.\n<color=yellow>Base: +7 Defense</color>\n<color=lime>+5 Vitality</color>", "A_Armour01_F", 99, 200, 0, false, 7, 0, 0, 0, 0, 0, 5));
        //100 Warring Leather Armor
        itemData.Add(new Armor("<color=lightblue>Warring Leather Armor</color>", "Body", "Light armor made from leather and imbued with arcana.\n<color=yellow>Base: +8 Defense</color>\n<color=lime>+5 Strength\n+5 Dexterity</color>", "A_Armour01_G", 100, 250, 0, false, 8, 5, 5, 0, 0, 0, 0));
        //101 Potent Leather Armor
        itemData.Add(new Armor("<color=lightblue>Potent Leather Armor</color>", "Body", "Light armor made from leather and imbued with arcana.\n<color=yellow>Base: +8 Defense</color>\n<color=lime>+5 Intelligence\n+5 Wisdom</color>", "A_Armour01_I", 101, 250, 0, false, 8, 0, 0, 0, 5, 5, 0));
        //102 Hardy Leather Armor
        itemData.Add(new Armor("<color=lightblue>Hardy Leather Armor</color>", "Body", "Light armor made from leather and imbued with arcana.\n<color=yellow>Base: +8 Defense</color>\n<color=lime>+5 Endurance\n+5 Vitality</color>", "A_Armour01_H", 102, 250, 0, false, 8, 0, 0, 5, 0, 0, 5));
        
        //103 Knightly Leather Armor
        itemData.Add(new Armor("<color=magenta>Knightly Leather Armor</color>", "Body", "Light armor made from leather and imbued with arcana.\n<color=yellow>Base: +9 Defense</color>\n<color=lime>+5 Strength\n+5 Dexterity\n+5 Endurance</color>", "A_Armour01_J", 103, 300, 0, false, 9, 5, 5, 5, 0, 0, 0));
        //104 Ancient Leather Armor
        itemData.Add(new Armor("<color=magenta>Ancient Leather Armor</color>", "Body", "Light armor made from leather and imbued with arcana.\n<color=yellow>Base: +9 Defense</color>\n<color=lime>+5 Intelligence\n+5 Wisdom\n+5 Vitality</color>", "A_Armour01_K", 104, 300, 0, false, 9, 0, 0, 0, 5, 5, 5));
        //105 Omni Leather Armor
        itemData.Add(new Armor("<color=orange>Omni Leather Armor</color>", "Body", "Light armor made from leather and imbued with arcana.\n<color=yellow>Base: +9 Defense</color> \n<color=#00ff00ff>+5 Strength</color>   <color=#00ff00ff> +5 Dexterity</color> \n<color=#00ff00ff>+5 Endurance</color> <color=#00ff00ff> +5 Intelligence</color> \n<color=#00ff00ff>+5 Wisdom</color>      <color=#00ff00ff> +5 Vitality</color> ", "A_Armour01_L", 105, 350, 0, false, 10, 5, 5, 5, 5, 5, 5));

        //106 Wayfarer Boots
        itemData.Add(new Armor("Wayfarer Boots", "Feet", "Boots fit for an adventurer.\n<color=yellow>Base: +2 Defense</color>", "A_Shoes02", 106, 50, 0, false, 2, 0, 0, 0, 0, 0, 0));
        //107 Leather Boots
        itemData.Add(new Armor("Leather Boots", "Feet", "Boots made from leather.\n<color=yellow>Base: +3 Defense</color>", "A_Shoes03", 107, 100, 0, false, 3, 0, 0, 0, 0, 0, 0));
        //108 Mighty Leather Boots
        itemData.Add(new Armor("<color=lime>Mighty Leather Boots</color>", "Feet", "Boots made from leather imbued with arcana.\n<color=yellow>Base: +4 Defense</color>\n<color=lime>+3 Strength</color>", "A_Shoes03_A", 108, 150, 0, false, 4, 3, 0, 0, 0, 0, 0));
        //109 Nimble Leather Boots
        itemData.Add(new Armor("<color=lime>Nimble Leather Boots</color>", "Feet", "Boots made from leather imbued with arcana.\n<color=yellow>Base: +4 Defense</color>\n<color=lime>+3 Dexterity</color>", "A_Shoes03_B", 109, 150, 0, false, 4, 0, 3, 0, 0, 0, 0));
        //110 Enduring Leather Boots
        itemData.Add(new Armor("<color=lime>Enduring Leather Boots</color>", "Feet", "Boots made from leather imbued with arcana.\n<color=yellow>Base: +4 Defense</color>\n<color=lime>+3 Endurance</color>", "A_Shoes03_C", 110, 150, 0, false, 4, 0, 0, 3, 0, 0, 0));
        //111 Arcane Leather Boots
        itemData.Add(new Armor("<color=lime>Arcane Leather Boots</color>", "Feet", "Boots made from leather imbued with arcana.\n<color=yellow>Base: +4 Defense</color>\n<color=lime>+3 Intelligence</color>", "A_Shoes03_D", 111, 150, 0, false, 4, 0, 0, 0, 3, 0, 0));
        //112 Sage Leather Boots
        itemData.Add(new Armor("<color=lime>Sage Leather Boots</color>", "Feet", "Boots made from leather imbued with arcana.\n<color=yellow>Base: +4 Defense</color>\n<color=lime>+3 Wisdom</color>", "A_Shoes03_E", 112, 150, 0, false, 4, 0, 0, 0, 0, 3, 0));
        //113 Robust Leather Boots
        itemData.Add(new Armor("<color=lime>Robust Leather Boots</color>", "Feet", "Boots made from leather imbued with arcana.\n<color=yellow>Base: +4 Defense</color>\n<color=lime>+3 Vitality</color>", "A_Shoes03_F", 113, 150, 0, false, 4, 0, 0, 0, 0, 0, 3));
        //114 Warring Leather Boots
        itemData.Add(new Armor("<color=lightblue>Warring Leather Boots</color>", "Feet", "Boots made from leather imbued with arcana.\n<color=yellow>Base: +5 Defense</color>\n<color=lime>+3 Strength\n+3 Dexterity</color>", "A_Shoes03_I", 114, 200, 0, false, 5, 3, 3, 0, 0, 0, 0));
        //115 Potent Leather Boots
        itemData.Add(new Armor("<color=lightblue>Potent Leather Boots</color>", "Feet", "Boots made from leather imbued with arcana.\n<color=yellow>Base: +5 Defense</color>\n<color=lime>+3 Intelligence\n+3 Wisdom</color>", "A_Shoes03_H", 115, 200, 0, false, 5, 0, 0, 0, 3, 3, 0));
        //116 Hardy Leather Boots
        itemData.Add(new Armor("<color=lightblue>Hardy Leather Boots</color>", "Feet", "Boots made from leather imbued with arcana.\n<color=yellow>Base: +5 Defense</color>\n<color=lime>+3 Endurance\n+3 Vitality</color>", "A_Shoes03_G", 116, 200, 0, false, 5, 0, 0, 3, 0, 0, 3));

        //117 Leather Cap
        itemData.Add(new Armor("Leather Cap", "Head", "A helm made out of leather.\n<color=yellow>Base: +2 Defense</color>", "C_Elm01", 117, 75, 0, false, 2, 2, 2, 0, 0, 0, 0));
        //118 Mighty Leather Cap
        itemData.Add(new Armor("<color=lime>Mighty Leather Cap</color>", "Head", "A helm made out of leather imbued with arcana.\n<color=yellow>Base: +3 Defense\n</color><color=lime>+2 Strength</color>", "C_Elm01_A", 118, 100, 0, false, 3, 2, 0, 0, 0, 0, 0));
        //119 Nimble Leather Cap
        itemData.Add(new Armor("<color=lime>Nimble Leather Cap</color>", "Head", "A helm made out of leather imbued with arcana.\n<color=yellow>Base: +3 Defense\n</color><color=lime>+2 Dexterity</color>", "C_Elm01_B", 119, 100, 0, false, 3, 0, 2, 0, 0, 0, 0));
        //120 Enduring Leather Cap
        itemData.Add(new Armor("<color=lime>Enduring Leather Cap</color>", "Head", "A helm made out of leather imbued with arcana.\n<color=yellow>Base: +3 Defense\n</color><color=lime>+2 Endurance</color>", "C_Elm01_C", 120, 100, 0, false, 3, 0, 0, 2, 0, 0, 0));
        //121 Arcane Leather Cap
        itemData.Add(new Armor("<color=lime>Arcane Leather Cap</color>", "Head", "A helm made out of leather imbued with arcana.\n<color=yellow>Base: +3 Defense\n</color><color=lime>+2 Intelligence</color>", "C_Elm01_D", 121, 100, 0, false, 3, 0, 0, 0, 2, 0, 0));
        //122 Sage Leather Cap
        itemData.Add(new Armor("<color=lime>Sage Leather Cap</color>", "Head", "A helm made out of leather imbued with arcana.\n<color=yellow>Base: +3 Defense\n</color><color=lime>+2 Wisdom</color>", "C_Elm01_E", 122, 100, 0, false, 3, 0, 0, 0, 0, 2, 0));
        //123 Robust Leather Cap
        itemData.Add(new Armor("<color=lime>Robust Leather Cap</color>", "Head", "A helm made out of leather imbued with arcana.\n<color=yellow>Base: +3 Defense\n</color><color=lime>+2 Vitality</color>", "C_Elm01_F", 123, 100, 0, false, 3, 0, 0, 0, 0, 0, 2));
        //124 Warrring Leather Cap
        itemData.Add(new Armor("<color=lightblue>Warring Leather Cap</color>", "Head", "A helm made out of leather imbued with arcana.\n<color=yellow>Base: +4 Defense\n</color><color=lime>+2 Strength\n+2 Dexterity</color>", "C_Elm01_G", 124, 125, 0, false, 4, 2, 2, 0, 0, 0, 0));
        //125 Potent Leather Cap
        itemData.Add(new Armor("<color=lightblue>Potent Leather Cap</color>", "Head", "A helm made out of leather imbued with arcana.\n<color=yellow>Base: +4 Defense\n</color><color=lime>+2 Intelligence\n+2 Wisdom</color>", "C_Elm01_H", 125, 125, 0, false, 4, 0, 0, 0, 2, 2, 0));
        //126 Hardy Leather Cap
        itemData.Add(new Armor("<color=lightblue>Hardy Leather Cap</color>", "Head", "A helm made out of leather imbued with arcana.\n<color=yellow>Base: +4 Defense\n</color><color=lime>+2 Endurance\n+2 Vitality</color>", "C_Elm01_I", 126, 125, 0, false, 5, 0, 0, 2, 0, 0, 2));

        //127 Small Vial
        itemData.Add(new Item("Small Vial", "Crafting Material", "A small empty vial.", "I_Bottle02", 127, 100, 0, true));
        //128 Unidentified Herb
        itemData.Add(new Item("Unidentified Herb", "Crafting Material", "A herb used for alchemy.", "I_Leaf", 128, 10, 0, true));
        //129 Common Herb
        itemData.Add(new Item("Common Herb", "Crafting Material", "A weak herb used for alchemy.", "I_Leaf_1", 129, 25, 0, true));
        //130 Uncommon Herb
        itemData.Add(new Item("Uncommon Herb", "Crafting Material", "A herb used for alchemy.", "I_Leaf_2", 130, 50, 0, true));
        //130 Rare Herb
        itemData.Add(new Item("Rare Herb", "Crafting Material", "A strong herb used for alchemy.", "I_Leaf_3", 131, 100, 0, true));

        //118 Iron Helm
        itemData.Add(new Armor("<color=lime>Iron Helm</color>", "Head", "A helm made out of leather.\n<color=yellow>Base: +2 Defense\n</color><color=lime>+2 Endurance\n+2 Vitality</color>", "C_Elm03", 118, 175, 0, false, 2, 0, 0, 2, 0, 0, 2));
        //119 Cloth Hat
        itemData.Add(new Armor("<color=lime>Cloth Hat</color>", "Head", "A helm made out of leather.\n<color=yellow>Base: +2 Defense\n</color><color=lime>+2 Intelligence\n+2 Wisdom</color>", "C_Hat01", 119, 175, 0, false, 2, 0, 0, 0, 2, 2, 0));

        



        //89 Worn Sword

        //90 Training Sword

        //91 Iron Sword

        //92 Mighty Iron Sword

        //93 Nimble Iron Sword

        //94 Enduring Iron Sword

        //95 Arcane Iron Sword

        //96 Sage Iron Sword

        //97 Robust Iron Sword

        //98 Warring Iron Sword

        //99 Potent Iron Sword

        //100 Hardy Iron Sword

        //101 Knightly Iron Sword

        //102 Ancient Iron Sword

        //103 Omni Iron Sword


        //104 Steel Sword

        //105 Mighty Steel Sword 

        //106 Nimble Steel Sword

        //107 Enduring Steel Sword

        //108 Arcane Steel Sword

        //109 Sage Steel Sword

        //110 Robust Steel Sword

        //111 Warring Steel Sword

        //112 Potent Steel Sword

        //113 Hardy Steel Sword

        //114 Knightly Steel Sword

        //115 Ancient Steel Sword

        //116 Omni Steel Sword


        //117 Mythril Sword

        //118 Mighty Mythril Sword

        //119 Nimble Mythril Sword

        //120 Enduring Mythril Sword

        //121 Arcane Mythril Sword

        //122 Sage Mythril Sword

        //123 Robust Mythril Sword

        //124 Warring Mythril Sword

        //125 Potent Mythril Sword

        //126 Hardy Mythril Sword

        //127 Knightly Mythril Sword

        //128 Ancient Mythril Sword

        //129 Omni Mythril Sword


        //130 Lion's Pride

        //131 Oathkeeper

        //132 Crusade

        //133 Gladius

        //134 Winterthorn

        //135 Echo

        //136 Glimmer

        //137 Tyrhung

        //138 Purity

        //139 Blessed Defender


        //140 Worn Bow

        //141 Training Bow


        //142 Maple Bow

        //143 Mighty Maple Bow

        //144 Nimble Maple Bow

        //145 Enduring Maple Bow

        //146 Enduring Maple Bow

        //147 Arcana Maple Bow

        //148 Sage Maple Bow

        //149 Robust Maple Bow

        //150 Warring Maple Bow

        //151 Potent Maple Bow

        //152 Hardy Maple Bow

        //153 Knightly Maple Bow

        //154 Ancient Maple Bow

        //155 Omni Maple Bow


        //156 Ash Bow

        //157 Mighty Ash Bow

        //158 Nimble Ash Bow

        //159 Enduring Ash Bow

        //160 Arcane Ash Bow

        //161 Sage Ash Bow

        //162 Robust Ash Bow

        //163 Warring Ash Bow

        //164 Potent Ash Bow

        //165 Hardy Ash Bow

        //166 Knightly Ash Bow

        //167 Ancient Ash Bow

        //168 Omni Ash Bow


        //169 Yew Bow

        //170 Mighty Yew Bow

        //171 Nimble Yew Bow

        //172 Enduring Yew Bow

        //173 Arcane Yew Bow

        //174 Sage Yew Bow

        //175 Robust Yew Bow

        //176 Warring Yew Bow

        //177 Potent Yew Bow

        //178 Hardy Yew Bow

        //179 Knightly Yew Bow

        //180 Ancient Yew Bow

        //181 Omni Yew Bow


        //182 Ashwood

        //183 Heartpiercer

        //184 Stinger

        //185 Splinter

        //186 Lash

        //187 Cyclone

        //188 Skyfire

        //189 Comet

        //190 Tranquility

        //191 Solitude


        //192 Worn Daggers

        //193 Training Daggers


        //194 Iron Daggers

        //195 Mighty Iron Daggers

        //196 Nimble Iron Daggers

        //194 Enduring Iron Daggers

        //195 Arcane Iron Daggers

        //196 Sage Iron Daggers

        //197 Robust Iron Daggers

        //198 Warring Iron Daggers

        //199 Potent Iron Daggers

        //200 Hardy Iron Daggers

        //201 Knightly Iron Daggers

        //202 Ancient Iron Daggers

        //203 Omni Iron Daggers


        //204 Steel Daggers

        //205 Mighty Steel Daggers

        //206 Nimble Steel Daggers

        //207 Enduring Steel Daggers

        //208 Arcane Steel Daggers

        //209 Sage Steel Daggers

        //210 Robust Steel Daggers

        //211 Warring Steel Daggers

        //212 Potent Steel Daggers

        //213 Hardy Steel Daggers

        //214 Knightly Steel Daggers

        //215 Ancient Steel Daggers

        //216 Omni Steel Daggers


        //217 Mythril Daggers

        //218 Mighty Mythril Daggers

        //219 Nimble Mythril Daggers

        //220 Enduring Mythril Daggers

        //221 Arcane Mythril Daggers

        //222 Sage Mythril Daggers

        //223 Robust Mythril Daggers

        //224 Warring Mythril Daggers

        //225 Potent Mythril Daggers

        //226 Hardy Mythril Daggers

        //227 Knightly Mythril Daggers

        //228 Ancient Mythril Daggers

        //229 Omni Mythril Daggers


        //330 Scarlet

        //331 Twinkle

        //332 Shadow Edge

        //333 Nightmare

        //334 Maelstrom

        //335 Extinction

        //336 Silverlight

        //337 Remorse

        //338 Doomblade

        //339 Eternity



        //340 Red Shroom

        //341 Red Shroom 1 Star

        //342 Red Shroom 2 Star

        //343 Red Shroom 3 Star

        //344 Brown Shroom

        //345 Brown Shroom 1 Star

        //346 Brown Shroom 2 Star

        //347 Brown Shroom 3 Star

        //348 Purple Shroom

        //349 Purple Shroom 1 Star

        //350 Purple Shroom 2 Star

        //351 Purple Shroom 3 Star

        //352 Teal Shroom

        //353 Teal Shroom 1 Star

        //354 Teal Shroom 2 Star

        //355 Teal Shroom 3 Star

        //356 Hard Carapace

        //357 Hard Carapace 1 Star

        //358 Hard Carapace 2 Star

        //359 Hard Carapace 3 Star

        //360 Bat Wing

        //362 Bat Wing 1 Star

        //363 Bat Wing 2 Star

        //364 Bat Wing 3 Star

        //365 Small Fang

        //366 Small Fang 1 Star

        //367 Small Fang 2 Star

        //368 Small Fang 3 Star

        //369 Feather

        //370 Feather 1 Star

        //371 Feather 2 Star

        //372 Feather 3 Star

        //373 G-Slime Crystal

        //374 G-Slime Crystal 1 Star

        //375 G-Slime Crystal 2 Star

        //376 G-Slime Crystal 3 Star

        //377 B-Slime Crystal

        //378 B-Slime Crystal 1 Star

        //379 B-Slime Crystal 2 Star

        //380 B-Slime Crystal 3 Star

        //381 R-Slime Crystal

        //382 R-Slime Crystal 1 Star

        //383 R-Slime Crystal 2 Star

        //384 R-Slime Crystal 3 Star

        //385 Y-Slime Crystal

        //386 Y-Slime Crystal 1 Star

        //387 Y-Slime Crystal 2 Star

        //388 Y-Slime Crystal 3 Star

        //389 Diamond

        //390 Diamond 1 Star

        //391 Diamond 2 Star

        //392 Diamond 3 Star

        //393 Weak Herb

        //394 Weak Herb 1 Star

        //395 Weak Herb 2 Star

        //396 Weak Herb 3 Star

        //397 Strong Herb

        //398 Strong Herb 1 Star

        //399 Strong Herb 2 Star

        //400 Strong Herb 3 Star






    }

    public Item FindItem(int id)
    {
        Item foundItem = new Item();
        for (int i = 0; i < itemData.Count; i++)
        {
            if(itemData[i].itemID == id)
            {
                foundItem = itemData[i];
                break;
            }
        }
        return foundItem;
    }

   
    public void IdentifySpear(int slot)
    {
        //Compile List of Spears
        List<Item> spearList = new List<Item>();

        //Level 1-5
        if (PlayerStats.playerLevel >= 0 && PlayerStats.playerLevel <= 5)
        {
            spearList.Add(itemData[38]);
            spearList.Add(itemData[39]);
        }

        //Level 6-15
        if (PlayerStats.playerLevel > 5 && PlayerStats.playerLevel <= 15)
        {
            for(int i = 40; i <= 52; i++)
            {
                spearList.Add(itemData[i]);
            }
            //Last Rites
            spearList.Add(itemData[80]);
        }

        //Level 16-25
        else if(PlayerStats.playerLevel > 15 && PlayerStats.playerLevel <= 25)
        {
            for (int i = 53; i <= 65; i++)
            {
                spearList.Add(itemData[i]);
            }
        }

        //Level 26-35
        else if(PlayerStats.playerLevel > 26 && PlayerStats.playerLevel <= 35)
        {
            for (int i = 66; i <= 78; i++)
            {
                spearList.Add(itemData[i]);
            }
        }

        int tempInt = Random.Range(0, spearList.Count);
        InventoryManager.playerInventory[slot] = FindItem(spearList[tempInt].itemID);
        GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
        //GameObject drop = Instantiate(Resources.Load("Prefabs/Items/" + spearList[tempInt].itemIconName),
        //new Vector2(TestCharController.player.transform.position.x, TestCharController.player.transform.position.y), Quaternion.identity) as GameObject;

    }

    public void IdentifyNeck(int slot)
    {
        List<Item> neckList = new List<Item>();

        //Level 1-10
        if (PlayerStats.playerLevel >= 0 && PlayerStats.playerLevel <= 10)
        {
            for (int i = 31; i <= 36; i++)
            {
                neckList.Add(itemData[i]);
            }
        }


        int tempInt = Random.Range(0, neckList.Count);
        InventoryManager.playerInventory[slot] = FindItem(neckList[tempInt].itemID);
        GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
        //GameObject drop = Instantiate(Resources.Load("Prefabs/Items/" + neckList[tempInt].itemIconName),
        //new Vector2(TestCharController.player.transform.position.x, TestCharController.player.transform.position.y), Quaternion.identity) as GameObject;

    }

    public void IdentifyRing(int slot)
    {
        List<Item> ringList = new List<Item>();

        //Level 1-10
        if (PlayerStats.playerLevel >= 0 && PlayerStats.playerLevel <= 10)
        {
            for (int i = 11; i <= 16; i++)
            {
                ringList.Add(itemData[i]);
            }
        }

        //Level 11-20
        if (PlayerStats.playerLevel > 10 && PlayerStats.playerLevel <= 20)
        {
            for (int i = 17; i <= 22; i++)
            {
                ringList.Add(itemData[i]);
            }
        }

        //Level 20-30
        if (PlayerStats.playerLevel > 20 && PlayerStats.playerLevel <= 30)
        {
            for (int i = 23; i <= 28; i++)
            {
                ringList.Add(itemData[i]);
            }
        }


        int tempInt = Random.Range(0, ringList.Count);
        InventoryManager.playerInventory[slot] = FindItem(ringList[tempInt].itemID);
        GameObject.Find("InventoryController").GetComponent<InventoryController>().UpdateInventory();
        //GameObject drop = Instantiate(Resources.Load("Prefabs/Items/" + ringList[tempInt].itemIconName),
        // new Vector2(TestCharController.player.transform.position.x, TestCharController.player.transform.position.y), Quaternion.identity) as GameObject;
    }




}
