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
        itemData.Add(new Weapon("Worn Spear", "Weapon", "A old worn spear.\n<color=red>Base: 6-9 Damage</color>", "W_Spear001", 8, 1, 0, false, 6, 9, 0, 0, 0, 0, 0, 0));
        //9 Casual Cloths
        itemData.Add(new Armor("Casual Cloths", "Body", "Light Fashionable Clothing.\n<color=yellow>Base: +2 Defense</color>", "A_Clothing01", 9, 1, 0, false, 2, 0, 0, 0, 0, 0, 0));
        //10 Adventurer's Loafers
        itemData.Add(new Armor("Adventurer Loafers", "Feet", "Comfortable and functional, for the on the go adventurer.\n<color=yellow>Base: +1 Defense</color>", "A_Shoes01", 10, 1, 0, false, 1, 0, 0, 0, 0, 0, 0));
        
        //Copper Bands
        
        //11 Mighty Copper Band
        itemData.Add(new Armor("<color=#00ff00ff>Mighty Copper Band</color>", "Ring", "A simple copper band imbued with arcana.\n<color=#00ff00ff>Base: +5 Strength</color>", "Ac_Ring01B", 11, 25, 0, false, 0, 5, 0, 0, 0, 0, 0));
        //12 Nimble Copper Band
        itemData.Add(new Armor("<color=#00ff00ff>Nimble Copper Band</color>", "Ring", "A simple copper band imbued with arcana.\n<color=#00ff00ff>Base: +5 Dexterity</color>", "Ac_Ring01C", 12, 25, 0, false, 0, 0, 5, 0, 0, 0, 0));
        //13 Arcane Copper Band
        itemData.Add(new Armor("<color=#00ff00ff>Arcane Copper Band</color>", "Ring", "A simple copper band imbued with arcana.\n<color=#00ff00ff>Base: +5 Intelligence</color>", "Ac_Ring01D", 13, 25, 0, false, 0, 0, 0, 0, 5, 0, 0));
        //14 Enduring Copper Band
        itemData.Add(new Armor("<color=#00ff00ff>Enduring Copper Band</color>", "Ring", "A simple copper band imbued with arcana.\n<color=#00ff00ff>Base: +5 Endurance</color>", "Ac_Ring01A", 14, 25, 0, false, 0, 0, 0, 5, 0, 0, 0));
        //15 Sage Copper Band
        itemData.Add(new Armor("<color=#00ff00ff>Sage Copper Band</color>", "Ring", "A simple copper band imbued with arcana.\n<color=#00ff00ff>Base: +5 Wisdom</color>", "Ac_Ring01E", 15, 25, 0, false, 0, 0, 0, 0, 0, 5, 0));
        //16 Copper Band
        itemData.Add(new Armor("<color=#00ff00ff>Copper Band</color>", "Ring", "A simple copper band.\n<color=#00ff00ff>Base: +5 Vitality</color>", "Ac_Ring01F", 16, 25, 0, false, 0, 0, 0, 0, 0, 0, 5));
        
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
        itemData.Add(new Armor("<color=lightblue>Iron Band</color>", "Ring", "A simple iron band.\n<color=#00ff00ff>Base: +10 Vitality</color>", "Ac_Ring01", 22, 100, 0, false, 0, 0, 0, 0, 0, 0, 10));

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

        //29 Training Spear
        itemData.Add(new Weapon("Training Spear", "Weapon", "A dull lightweight spear used to train novices.\n<color=red>Base: 10-15 Damage</color>", "W_Spear002", 29, 25, 0, false, 10, 15, 0, 0, 0, 0, 0, 0));

        //30 G-Slime Crystal
        itemData.Add(new Item("G-Slime Crystal", "Treasure", "A small green crystal created by slimes when they are under stress. Can be sold for gold.", "I_Jade", 30, 50, 0, true));

        //Small Pendants

        //31 Pendent of Might
        itemData.Add(new Armor("<color=#00ff00ff>Pendent of Might</color>", "Neck", "A pendent with a small stone imbued with arcana.\n<color=#00ff00ff>Base: +3 Strength</color>", "Ac_Necklace02B", 31, 75, 0, false, 0, 3, 0, 0, 0, 0, 0));
        //32 Pendent of Agility
        itemData.Add(new Armor("<color=#00ff00ff>Pendent of Agility</color>", "Neck", "A pendent with a small stone imbued with arcana.\n<color=#00ff00ff>Base: +3 Dexterity</color>", "Ac_Necklace02C", 32, 75, 0, false, 0, 0, 3, 0, 0, 0, 0));
        //33 Pendent of Endurance
        itemData.Add(new Armor("<color=#00ff00ff>Pendent of Endurance</color>", "Neck", "A pendent with a small stone imbued with arcana.\n<color=#00ff00ff>Base: +3 Endurance</color>", "Ac_Necklace02D", 33, 75, 0, false, 0, 0, 0, 3, 0, 0, 0));
        //34 Pendent of Intelligence
        itemData.Add(new Armor("<color=#00ff00ff>Pendent of Intelligence</color>", "Neck", "A pendent with a small stone imbued with arcana.\n<color=#00ff00ff>Base: +3 Intelligence</color>", "Ac_Necklace02A", 34, 75, 0, false, 0, 0, 0, 0, 3, 0, 0));
        //35 Pendent of Wisdom
        itemData.Add(new Armor("<color=#00ff00ff>Pendent of Wisdom</color>", "Neck", "A pendent with a small stone imbued with arcana.\n<color=#00ff00ff>Base: +3 Wisdom</color>", "Ac_Necklace02E", 35, 75, 0, false, 0, 0, 0, 0, 0, 3, 0));
        //36 Small Pendent
        itemData.Add(new Armor("<color=#00ff00ff>Small Pendent</color>", "Neck", "A pedent with a small stone imbued with arcana.\n<color=#00ff00ff>Base: +3 Vitality</color>", "Ac_Necklace02F", 36, 75, 0, false, 0, 0, 0, 0, 0, 0, 3));

        //37 Hardened Carapace
        itemData.Add(new Item("Hard Carapace", "Crafting Material", "A small piece of a hard shell.", "I_SolidShell_1", 37, 5, 0, true));

        //Spears

        //38 Worn Spear
        itemData.Add(new Weapon("Worn Spear", "Weapon", "A old worn spear.\n<color=red>Base: 6-9 Damage</color>", "W_Spear001", 38, 1, 0, false, 6, 9, 0, 0, 0, 0, 0, 0));
        //39 Training Spear
        itemData.Add(new Weapon("Training Spear", "Weapon", "A dull lightweight spear used to train novices.\n<color=red>Base: 10-15 Damage</color>", "W_Spear002", 39, 25, 0, false, 10, 15, 0, 0, 0, 0, 0, 0));
        
        //40 Iron Spear
        itemData.Add(new Weapon("Iron Spear", "Weapon", "A sturdy spear made out of iron. \n<color=red>Base: 18-25 Damage</color>", "W_Spear003", 40, 100, 0, false, 18, 25, 0, 0, 0, 0, 0, 0));
        //41 Mighty Iron Spear
        itemData.Add(new Weapon("<color=#00ff00ff>Mighty Iron Spear</color>", "Weapon", "A sturdy spear made out of iron infused with arcana. \n<color=red>Base: 20-27 Damage</color> \n<color=#00ff00ff>+7 Strength</color>", "W_Spear003_A", 41, 150, 0, false, 20, 27, 7, 0, 0, 0, 0, 0));
        //42 Nimble Iron Spear
        itemData.Add(new Weapon("<color=#00ff00ff>Nimble Iron Spear</color>", "Weapon", "A sturdy spear made out of iron infused with arcana. \n<color=red>Base: 20-27 Damage</color> \n<color=#00ff00ff>+7 Dexterity</color>", "W_Spear003_B", 42, 150, 0, false, 20, 27, 0, 7, 0, 0, 0, 0));
        //43 Enduring Iron Spear
        itemData.Add(new Weapon("<color=#00ff00ff>Enduring Iron Spear</color>", "Weapon", "A sturdy spear made out of iron infused with arcana. \n<color=red>Base: 20-27 Damage</color> \n<color=#00ff00ff>+7 Endurance</color>", "W_Spear003_C", 43, 150, 0, false, 20, 27, 0, 0, 7, 0, 0, 0));
        //44 Arcane Iron Spear
        itemData.Add(new Weapon("<color=#00ff00ff>Arcane Iron Spear</color>", "Weapon", "A sturdy spear made out of iron infused with arcana. \n<color=red>Base: 20-27 Damage</color> \n<color=#00ff00ff>+7 Intelligence</color>", "W_Spear003_D", 44, 150, 0, false, 20, 27, 0, 0, 0, 7, 0, 0));
        //45 Sage Iron Spear
        itemData.Add(new Weapon("<color=#00ff00ff>Sage Iron Spear</color>", "Weapon", "A sturdy spear made out of iron infused with arcana. \n<color=red>Base: 20-27 Damage</color> \n<color=#00ff00ff>+7 Wisdom</color>", "W_Spear003_E", 45, 150, 0, false, 20, 27, 0, 0, 0, 0, 7, 0));
        //46 Robust Iron Spear
        itemData.Add(new Weapon("<color=#00ff00ff>Robust Iron Spear</color>", "Weapon", "A sturdy spear made out of iron infused with arcana. \n<color=red>Base: 20-27 Damage</color> \n<color=#00ff00ff>+7 Vitality</color>", "W_Spear003_F", 46, 150, 0, false, 20, 27, 0, 0, 0, 0, 0, 7));
        //47 Warring Iron Spear
        itemData.Add(new Weapon("<color=lightblue>Warring Iron Spear</color>", "Weapon", "A sturdy spear made out of iron infused with arcana. \n<color=red>Base: 23-30 Damage</color> \n<color=#00ff00ff>+7 Strength</color> \n<color=#00ff00ff>+7 Dexterity</color>", "W_Spear003_G", 47, 200, 0, false, 23, 30, 7, 7, 0, 0, 0, 0));
        //48 Potent Iron Spear
        itemData.Add(new Weapon("<color=lightblue>Potent Iron Spear</color>", "Weapon", "A sturdy spear made out of iron infused with arcana. \n<color=red>Base: 23-30 Damage</color> \n<color=#00ff00ff>+7 Intelligence</color> \n<color=#00ff00ff>+7 Wisdom</color>", "W_Spear003_H", 48, 200, 0, false, 23, 30, 0, 0, 0, 7, 7, 0));
        //49 Hardy Iron Spear
        itemData.Add(new Weapon("<color=lightblue>Hardy Iron Spear</color>", "Weapon", "A sturdy spear made out of iron infused with arcana. \n<color=red>Base: 23-30 Damage</color> \n<color=#00ff00ff>+7 Endurance</color> \n<color=#00ff00ff>+7 Vitality</color>", "W_Spear003_I", 49, 200, 0, false, 23, 30, 0, 0, 7, 0, 0, 7));
        //50 Knightly Iron Spear
        itemData.Add(new Weapon("<color=magenta>Knightly Iron Spear</color>", "Weapon", "A sturdy spear made out of iron infused with arcana. \n<color=red>Base: 26-33 Damage</color> \n<color=#00ff00ff>+7 Strength</color> \n<color=#00ff00ff>+7 Dexterity</color>\n<color=#00ff00ff>+7 Endurance</color> ", "W_Spear003_J", 50, 250, 0, false, 26, 33, 7, 7, 7, 0, 0, 0));
        //51 Ancient Iron Spear
        itemData.Add(new Weapon("<color=magenta>Ancient Iron Spear</color>", "Weapon", "A sturdy spear made out of iron infused with arcana. \n<color=red>Base: 26-33 Damage</color> \n<color=#00ff00ff>+7 Intelligence</color> \n<color=#00ff00ff>+7 Wisdom</color>\n<color=#00ff00ff>+7 Vitality</color> ", "W_Spear003_K", 51, 250, 0, false, 26, 33, 0, 0, 0, 7, 7, 7));
        //52 Omni Iron Spear
        itemData.Add(new Weapon("<color=orange>Omni Iron Spear</color>", "Weapon", "A sturdy spear made out of iron infused with arcana. \n<color=red>Base: 29-36 Damage</color> \n<color=#00ff00ff>+7 Strength</color>   <color=#00ff00ff> +7 Dexterity</color> \n<color=#00ff00ff>+7 Endurance</color> <color=#00ff00ff> +7 Intelligence</color> \n<color=#00ff00ff>+7 Wisdom</color>      <color=#00ff00ff> +7 Vitality</color> ", "W_Spear003_L", 52, 300, 0, false, 29, 36, 7, 7, 7, 7, 7, 7));
       
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

        //79 Last Rites

        //80 Judicator

        //81 Sky Talon

        //82 Needler

        //83 Demonbane

        //84 Harbinger

        //85 Eclipse

        //86 Thundercaller

        //87 Dawnbreak

        //88 Heavensfall



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
}
