using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Hero Project Room Class
By: Ryan Su

This scripts defines the Room class.


*/


public class Room 
{

    //Dimensions of a room in 48x48 tiles
    public int roomX;
    public int roomY;

    //Dictates which tiles the Rooms will use
    public int biomeLevel;
    //Dictates which room will be generated when player enters.
    // 0 = Random Room
    // 1-n = Generate the nth Room
    // i.e. Forest_1 corresponds to 1
    //      Forest_2 corresponds to 2, etc
    public int roomNum;

    //This is the Room ID that will be used to generate rooms
    public int roomID;

    public bool isStart;
    public bool isEnd;
    public bool isSpecial;
    public bool isExplored;
    public bool isBoss;

    public string roomName;

    public bool roomClear = false;
    public List<GameObject> enemyList = new List<GameObject>();
    public int enemyCount;

    //Special Room Stuff

    //If the treasure/font has been used.
    public bool isOpened = false;

    //For treasure, store the contents of the treasure
    public Item[] treasureInv = new Item[50];

    public Card[] fontCard = new Card[3];
    public int pickedCardIndex = -1;

    public List<int> specialShopInv = new List<int>();

    public int spawnRune;

    //List of items in the level
    public List<WorldItem> roomItems = new List<WorldItem>();

    //List of destructibles on the level
    public List<WorldDestruct> roomDestructibles = new List<WorldDestruct>();

    //The Room Class constructor
    public Room(int x, int y, int level, int room, bool start, bool end, bool special, bool clear, bool explored, bool boss)
    {
        roomX = x;
        roomY = y;
        biomeLevel = level;
        roomNum = room;
        isStart = start;
        isEnd = end;
        isSpecial = special;
        roomClear = clear;
        isExplored = explored;
        isBoss = boss;
        roomName = GetRoom(level);
    }

    public Room()
    {
        roomX = 0;
        roomY = 0;
        biomeLevel = 0;
        roomNum = 0;
        isStart = false;
        isEnd = false;
        isSpecial = false;
        roomClear = false;
        roomName = "";
        isExplored = false;
        isBoss = false;
    }

    //Get's the scene that is to be generated with the room
    public string GetRoom(int level)
    {
        string roomName = "";

        switch(level)
        {
            default:
                break;
            case 1:
                roomID = Random.Range(1, 14);
                break;
            case 2:
                roomID = Random.Range(1, 11);
                break;
            case 3:
                break;
            case 4:
                roomID = Random.Range(1, 8);
                break;
        }
        

        if (isSpecial)
        {
            switch (level)
            {
                default:
                    break;
                case 1:
                    if(!LevelDatabase.forestOfBeginning.shopMade)
                    {
                        LevelDatabase.forestOfBeginning.shopMade = true;
                        roomID = 3;
                        break;
                    }

                    //Mosaic Processing
                    else if(!LevelDatabase.forestOfBeginning.mosaicMade)
                    {
                        LevelDatabase.forestOfBeginning.mosaicMade = true;

                        //Is the mosaic done?
                        bool isDone = true;
                        for(int i = 0; i < Mosaic_Manager.korosMosaic.Length; i++)
                        {
                            if(!Mosaic_Manager.korosMosaic[i])
                            {
                                isDone = false;
                                break;
                            }
                        }

                        //If it isn't done, make the mosaic room
                        if(!isDone)
                        {
                            roomID = 5;
                            break;
                        }

                    }

                    roomID = Random.Range(1, 7);
                    //check if id matches the shop or the mosaic room
                    while (roomID == 3 || roomID == 5)
                    {
                        roomID = Random.Range(1, 7);
                    }
                    break;

                case 2:
                    roomID = Random.Range(1, 3);
                    break;
                case 4:
                    roomID = Random.Range(1, 2);
                    break;
            }
            

            //=======================================================
            //Special Room Processing
            //=======================================================

            //if the room is a treasure room fill the treasure inv array
            if (roomID == 2)
            {
                for (int i = 0; i < treasureInv.Length; i++)
                {
                    //Fill the array with "No Items"
                    treasureInv[i] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                }

                //Get the Treasure LootTable
                GameObject.Find("Special_Room_Data").GetComponent<TreasureDatabase>().GetTreasureLootTable(ref treasureInv, biomeLevel);
            }

            //Arcana Font
            else if (roomID == 1 || roomID == 4)
            {
                GameObject.Find("Special_Room_Data").GetComponent<Font_Database>().GetFontCard(ref fontCard[0], biomeLevel);
                GameObject.Find("Special_Room_Data").GetComponent<Font_Database>().GetFontCard(ref fontCard[1], biomeLevel);
                GameObject.Find("Special_Room_Data").GetComponent<Font_Database>().GetFontCard(ref fontCard[2], biomeLevel);
            }
            
            //travelling merchant
            else if(roomID == 3)
            {
                int tempInt = Random.Range(1, 3);
                //1 or 2?
                for (int i = 0; i < tempInt; i++)
                {
                    int randomItem = Random.Range(0, 3);
                    if (randomItem == 0)
                    {
                        specialShopInv.Add(Random.Range(11, 17));
                    }
                    else if (randomItem == 1)
                    {
                        specialShopInv.Add(Random.Range(31, 37));
                    }
                    else if (randomItem == 2)
                    {
                        specialShopInv.Add(Random.Range(40, 50));
                    }
                }
            }

            //Runes
            else if(roomID == 6)
            {
                //Initialize runes if not done already
                Rune_Database.InitializeRunes();

                //Take the first rune
                spawnRune = Rune_Database.redRuneOrder[0];
                Rune_Database.redRuneOrder.RemoveAt(0);
                
            }

        }
        else if(isStart)
        {
            roomID = Random.Range(1, 2);
            for(int i = 0; i < treasureInv.Length; i++)
            {
                treasureInv[i] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
            }
            if(!Mosaic_Manager.korosClaimed)
            {
                treasureInv[0] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[52];
            }
                
        }
        else if(isEnd)
        {
            roomID = Random.Range(1, 2);
        }
        else if(isBoss)
        {
            roomID = Random.Range(1, 2);
            //Spawn Boss treasure
            for (int i = 0; i < treasureInv.Length; i++)
            {
                //Fill the array with "No Items"
                treasureInv[i] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
            }

            //Get the Treasure LootTable
            GameObject.Find("Special_Room_Data").GetComponent<TreasureDatabase>().GetBossLootTable(ref treasureInv, biomeLevel);
        }



        if (level == 0)
        {
            TestCharController.arcanaEnabled = false;
            TestCharController.attackEnabled = true;
            roomName = "Town_" + roomNum;
            return roomName;
        }

        else if (level == 1)
        {
            TestCharController.arcanaEnabled = true;
            TestCharController.attackEnabled = true;
            if (isSpecial)
            {
                roomName = "Forest_" + roomID + "_Special";
                //Debug.Log(roomName);
                return roomName;
            }
            else if (isStart)
            {
                roomName = "Forest_" + roomID + "_Start";
                //Debug.Log(roomName);
                return roomName;
            }
            else if (isEnd)
            {
                roomName = "Forest_" + roomID + "_Exit";
                //Debug.Log(roomName);
                return roomName;
            }
            else if (isBoss)
            {
                roomName = "Forest_" + roomID + "_Boss";
                //Debug.Log("!!!!" + roomName);
                return roomName;
            }


            roomName = "Forest_" + roomID;
            //Debug.Log(roomName);
            return roomName;
        }

        else if (level == 2)
        {
            TestCharController.arcanaEnabled = true;
            TestCharController.attackEnabled = true;
            if (isSpecial)
            {
                roomName = "Cave_" + roomID + "_Special";
                //Debug.Log(roomName);
                return roomName;
            }
            else if (isStart)
            {
                roomName = "Cave_" + roomID + "_Start";
                //Debug.Log(roomName);
                return roomName;
            }
            else if (isEnd)
            {
                roomName = "Cave_" + roomID + "_Exit";
                //Debug.Log(roomName);
                return roomName;
            }
            else if (isBoss)
            {
                roomName = "Cave_" + roomID + "_Boss";
                //Debug.Log("!!!!" + roomName);
                return roomName;
            }

            roomName = "Cave_" + roomID;
            //Debug.Log(roomName);
            return roomName;
        }

        else if (level == 3)
        {
            roomName = "Cecilia_Start_" + roomNum;
            return roomName;
        }

        else if (level == 4)
        {
            TestCharController.arcanaEnabled = true;
            TestCharController.attackEnabled = true;
            if (isSpecial)
            {
                roomName = "Tomb_" + roomID + "_Special";
                //Debug.Log(roomName);
                return roomName;
            }
            else if (isStart)
            {
                roomName = "Tomb_" + roomID + "_Start";
                //Debug.Log(roomName);
                return roomName;
            }
            else if (isEnd)
            {
                roomName = "Tomb_" + roomID + "_Exit";
                //Debug.Log(roomName);
                return roomName;
            }
            else if (isBoss)
            {
                roomName = "Tomb_" + roomID + "_Boss";
                //Debug.Log("!!!!" + roomName);
                return roomName;
            }


            roomName = "Tomb_" + roomID;
            //Debug.Log(roomName);
            return roomName;
        }
        else if(level == 5)
        {
            TestCharController.arcanaEnabled = false;
            TestCharController.attackEnabled = false;
            roomName = "Port_" + roomNum;
            return roomName;
        }

        else if (level == 6)
        {
            TestCharController.arcanaEnabled = false;
            TestCharController.attackEnabled = false;
            roomName = "Cecilia_Town_" + roomNum;
            return roomName;
        }
        else if (level == 7)
        {
            TestCharController.arcanaEnabled = false;
            TestCharController.attackEnabled = false;
            roomName = "Camp_" + roomNum;
            return roomName;
        }

        return "";

    }


}
