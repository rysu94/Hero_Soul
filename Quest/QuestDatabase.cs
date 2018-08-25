using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDatabase : MonoBehaviour
{
    //string name, int type, string img, string desc, List<string> obj, List<Item> items, int xp, int gold
    public static Quest testQuest, testSideQuest1, testSideQuest2;
    public static Quest quest1, quest2, quest3, quest4, quest5;
    public static Quest side1, side2, side3, side4, side5;
    public static Quest ceQuest1, ceQuest2, ceQuest3;

    // Use this for initialization
    void Start ()
    {
        //Koros Forest Explore Quest=====================================================
        List<string> objectives1 = new List<string>();
        List<Item> items1 = new List<Item>();

        objectives1.Add("Defeat the dungeon boss.");

        items1.Add(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().FindItem(79));

        testQuest = new Quest("Explore Koros", 0, "I_Map",
            //Quest Desc
            "You find yourself in the living dungeon known as the Koros Forest, monsters and winding paths obstruct your path forward.",
            //Quest Obj
            objectives1,
            //Quest reward items
            items1,
            //xp
            150,
            //gold
            50
            );

        //Koros Forest Guild Quest 1=====================================================
        List<string> objectives2 = new List<string>();
        List<Item> items2 = new List<Item>();

        objectives2.Add("Defeat 0/5 Green Slimes.");
        objectives2.Add("Obtain 1 G-Slime Crystal.");

        testSideQuest1 = new Quest("Slime Hunter I", 1, "I_Jade",
            //Quest Desc
            "Slimes have always been a menace for the kingdom consuming crop and livestock, the guild has requested their extermination.",
            //Quest Obj
            objectives2,
            //Quest reward items
            items2,
            //xp
            300,
            //gold
            200
            );

        //Koros Forest Magic Quest 1=====================================================
        List<string> objectives3 = new List<string>();
        List<Item> items3 = new List<Item>();

        objectives3.Add("Discover 0/10 unique Arcana.");

        testSideQuest2 = new Quest("Card Collector I", 1, "Deck",
            //Quest Desc
            "The magic shop keeper, Fiona, would like to study Arcana more throughly. She has requested you to show her the cards you discover in your travels.",
            //Quest Obj
            objectives3,
            //Quest reward items
            items3,
            //xp
            500,
            //gold
            0
            );

        //Koros Forest Guild Quest 2=====================================================
        List<string> objectives4 = new List<string>();
        List<Item> items4 = new List<Item>();

        objectives4.Add("Defeat 0/7 Maskys.");

        side1 = new Quest("Masky Hunter I", 1, "Masky_Hunter",
            //Quest Desc
            "Masky's are dangerous to any travelers in the Koros Forest. The guild has been requested to thin their numbers.",
            //Quest Obj
            objectives4,
            //Quest reward items
            items4,
            //xp
            300,
            //gold
            150
            );

        //Koros Forest Guild Quest 3=====================================================
        List<string> objectives5 = new List<string>();
        List<Item> items5 = new List<Item>();

        objectives5.Add("Gather 0/10 Red Mushrooms.");
        objectives5.Add("Gather 0/10 Hard Carapace");
        objectives5.Add("Gather 0/5 Small Feather");

        side2 = new Quest("Stocking Up I", 1, "P_Orange05",
            //Quest Desc
            "The Alchemist Theo has requested the guild for adventurers to fetch him some alchemy ingredients to replenish his stocks.",
            //Quest Obj
            objectives5,
            //Quest reward items
            items5,
            //xp
            350,
            //gold
            250
            );

        //Mier Mines Explore Quest Quest 2=====================================================
        List<string> objectives6 = new List<string>();
        List<Item> items6 = new List<Item>();

        objectives6.Add("Defeat the dungeon boss.");

        items6.Add(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().FindItem(79));

        quest1 = new Quest("Explore Mier", 0, "I_Map",
            //Quest Desc
            "You find yourself in the living dungeon known as the Mier Mines, now abandoned monsters and winding paths obstruct your path forward to hidden riches.",
            //Quest Obj
            objectives6,
            //Quest reward items
            items6,
            //xp
            350,
            //gold
            250
            );

        //Cecilia Tutorial Quest 1=====================================================
        List<string> objectives7 = new List<string>();
        List<Item> items7 = new List<Item>();

        objectives7.Add("Reach the town of Weiss");

        //items7.Add(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().FindItem(79));

        ceQuest1 = new Quest("Guild Exam", 0, "I_Scroll",
            //Quest Desc
            "The Adventurer Guild holds annual exams for people looking to join their ranks. It's always been your dream to be like your father, and this is your first step towards that goal.",
            //Quest Obj
            objectives7,
            //Quest reward items
            items7,
            //xp
            100,
            //gold
            0
            );

        //Cecilia Tutorial Quest 2=====================================================
        List<string> objectives8 = new List<string>();
        List<Item> items8 = new List<Item>();

        objectives8.Add("Talk to Theo");

        ceQuest2 = new Quest("The Apprentice", 0, "I_Bottle03",
            //Quest Desc
            "You saved Maurice, the alchemy apprentice, from monsters. He asked you to tell his master that he is going to be a litttle late. His master should be the owner of the Alchemy Shop in Weiss.",
            //Quest Obj
            objectives8,
            //Quest reward items
            items8,
            //xp
            100,
            //gold
            0
            );

        //Cecilia Tutorial Quest 3=====================================================
        List<string> objectives9 = new List<string>();
        List<Item> items9 = new List<Item>();

        objectives9.Add("Talk to Fiona.");

        ceQuest3 = new Quest("It's Magic", 0, "W_Book01",
            //Quest Desc
            "Byron, the head of the Adventurer Guild, has given you an Arcana Voucher. The voucher entitles you to a free starter set of Arcana. You should go meet Fiona the magic shop owner in Weiss.",
            //Quest Obj
            objectives9,
            //Quest reward items
            items9,
            //xp
            100,
            //gold
            0
            );


    }

    // Update is called once per frame
    void Update ()
    {
        /*
		if(Input.GetKeyDown(KeyCode.Y))
        {
            GetComponent<QuestManager>().AddQuest(testQuest);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            GetComponent<QuestManager>().RemoveQuest("Explore Koros");
        }
        */
    }
}
