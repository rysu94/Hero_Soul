using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class TreasureDatabase : MonoBehaviour
{

    public int itemChance;
    public int totalDrops;

    //Loot Rarity %

    //49% chance common
    //35% chance uncommon
    //10% chance rare
    //5% chance epic
    //1% chance legend


    //common rarity
    public List<int> commonTreasureDropItemID = new List<int>();
    //uncommon rarity
    public List<int> uncommonTreasureDropID = new List<int>();
    //rare rarity
    public List<int> rareTreasureDropID = new List<int>();
    //Epic rarity
    public List<int> epicTreasureDropID = new List<int>();
    //legendary rarity
    public List<int> legendaryTreasureDropID = new List<int>();

    public void GetTreasureLootTable(ref Item[] treasureInv, int biome)
    {
        //LootTable for Forest of Beginning
        if (biome == 1)
        {
            //Initalize total treasure drop variables
            itemChance = 100;
            totalDrops = 0;

            //Initialize the treasure loot table
            ClearTables();

            //common drops
            commonTreasureDropItemID.Add(6); //small feather
            commonTreasureDropItemID.Add(7); //red mushroom

            //uncommon drops
            for (int i = 11; i < 17; i++)
            {
                uncommonTreasureDropID.Add(i);
            }

            while (itemChance > 0)
            {
                //Total drops between 1-2
                itemChance -= Random.Range(50, 151);
                totalDrops++;
            }

            print(totalDrops);

            //Assign Item Id's to the treasure inv array based on how many total drops
            for (int i = 0; i < totalDrops; i++)
            {
                List<int> tempList = DetermineItemRarity();
                treasureInv[i] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[tempList[Random.Range(0, tempList.Count)]];
                if(treasureInv[i].stackable)
                {
                    treasureInv[i].itemQuantity = Random.Range(1, 6);
                }
                else
                {
                    treasureInv[i].itemQuantity = 1;
                }                
            }
        }

        //LootTable for Mier Mineshaft
        if(biome == 2)
        {
            itemChance = 100;
            totalDrops = 0;

            ClearTables();

            //common drops
            commonTreasureDropItemID.Add(6); //small feather
            commonTreasureDropItemID.Add(7); //red mushroom

            //uncommon drops
            for (int i = 11; i < 17; i++)
            {
                uncommonTreasureDropID.Add(i);
            }

            while (itemChance > 0)
            {
                //Total drops between 1-2
                itemChance -= Random.Range(50, 151);
                totalDrops++;
            }

            print(totalDrops);

            //Assign Item Id's to the treasure inv array based on how many total drops
            for (int i = 0; i < totalDrops; i++)
            {
                List<int> tempList = DetermineItemRarity();
                treasureInv[i] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[tempList[Random.Range(0, tempList.Count)]];
                if (treasureInv[i].stackable)
                {
                    treasureInv[i].itemQuantity = Random.Range(1, 6);
                }
                else
                {
                    treasureInv[i].itemQuantity = 1;
                }
            }

        }
    }

    //Clears all the loot lists
    void ClearTables()
    {
        commonTreasureDropItemID.Clear();
        uncommonTreasureDropID.Clear();
        rareTreasureDropID.Clear();
        epicTreasureDropID.Clear();
        legendaryTreasureDropID.Clear();
    }

    List<int> DetermineItemRarity()
    {
        
        List<int> pickedList = new List<int>();

        while(pickedList.Count == 0)
        {
            int tempInt = Random.Range(1, 101);
            if(tempInt <= 49)
            {
                pickedList = commonTreasureDropItemID;
            }
            else if(tempInt > 49 && tempInt <= 84)
            {
                pickedList = uncommonTreasureDropID;
            }
            else if(tempInt > 84 && tempInt <= 94)
            {
                pickedList = rareTreasureDropID;
            }
            else if(tempInt > 94 && tempInt <= 99)
            {
                pickedList = epicTreasureDropID;
            }
            else if(tempInt == 100)
            {
                pickedList = legendaryTreasureDropID;
            }
        }

        return pickedList;
    }

}
