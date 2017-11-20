using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Font_Database : MonoBehaviour 
{

    public int itemChance;
    public int totalDrops;

    //Loot Rarity %

    //49% chance common
    //35% chance uncommon
    //10% chance rare
    //5% chance epic
    //1% chance common


    //common rarity
    public List<Card> commonTreasureDropItemID = new List<Card>();
    //uncommon rarity
    public List<Card> uncommonTreasureDropID = new List<Card>();
    //rare rarity
    public List<Card> rareTreasureDropID = new List<Card>();
    //Epic rarity
    public List<Card> epicTreasureDropID = new List<Card>();
    //legendary rarity
    public List<Card> legendaryTreasureDropID = new List<Card>();

    public void GetFontCard(ref Card fontCard, int biome)
    {
        ClearTables();

        //Forest of Beginning
        if(biome == 1)
        {
            //Add Common Cards
            commonTreasureDropItemID.Add(CardDatabase.arcana_Blaze);
            commonTreasureDropItemID.Add(CardDatabase.arcana_FireBolt);
            commonTreasureDropItemID.Add(CardDatabase.arcana_Stone);
            commonTreasureDropItemID.Add(CardDatabase.arcana_Boulder);

            //Add Uncommon Cards
            uncommonTreasureDropID.Add(CardDatabase.arcana_Blaze);
            uncommonTreasureDropID.Add(CardDatabase.arcana_Magma);
            uncommonTreasureDropID.Add(CardDatabase.arcana_Earthen);
            uncommonTreasureDropID.Add(CardDatabase.arcana_Impale);

            List<Card> tempList = DetermineItemRarity();
            int tempInt = Random.Range(0, tempList.Count);

            fontCard = tempList[tempInt];
            print(fontCard.cardName);
        }

        //Mier Mineshaft
        if (biome == 2)
        {
            //Add Common Cards
            commonTreasureDropItemID.Add(CardDatabase.arcana_Blaze);
            commonTreasureDropItemID.Add(CardDatabase.arcana_FireBolt);
            commonTreasureDropItemID.Add(CardDatabase.arcana_Stone);
            commonTreasureDropItemID.Add(CardDatabase.arcana_Boulder);

            //Add Uncommon Cards
            uncommonTreasureDropID.Add(CardDatabase.arcana_Blaze);
            uncommonTreasureDropID.Add(CardDatabase.arcana_Magma);
            uncommonTreasureDropID.Add(CardDatabase.arcana_Earthen);
            uncommonTreasureDropID.Add(CardDatabase.arcana_Impale);

            List<Card> tempList = DetermineItemRarity();
            int tempInt = Random.Range(0, tempList.Count);

            fontCard = tempList[tempInt];
            print(fontCard.cardName);
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

    List<Card> DetermineItemRarity()
    {

        List<Card> pickedList = new List<Card>();

        while (pickedList.Count == 0)
        {
            int tempInt = Random.Range(1, 101);
            if (tempInt <= 49)
            {
                pickedList = commonTreasureDropItemID;
            }
            else if (tempInt > 49 && tempInt <= 84)
            {
                pickedList = uncommonTreasureDropID;
            }
            else if (tempInt > 84 && tempInt <= 94)
            {
                pickedList = rareTreasureDropID;
            }
            else if (tempInt > 94 && tempInt <= 99)
            {
                pickedList = epicTreasureDropID;
            }
            else if (tempInt == 100)
            {
                pickedList = legendaryTreasureDropID;
            }
        }

        return pickedList;
    }



}
