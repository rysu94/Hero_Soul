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
    public Item[] treasureInv = new Item[18];

    public Card fontCard;
    

	void Awake()
    {

    }
    
    // Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

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
                roomID = Random.Range(1, 9);
                break;
        }
        

        

        if (isSpecial)
        {
            roomID = Random.Range(1, 3);

            //=======================================================
            //Treasure Room Processing
            //=======================================================

            //if the room is a treasure room fill the treasure inv array
            if (roomID == 2)
            {
                for (int i = 0; i < 18; i++)
                {
                    //Fill the array with "No Items"
                    treasureInv[i] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                }

                //Get the Treasure LootTable
                GameObject.Find("Special_Room_Data").GetComponent<TreasureDatabase>().GetTreasureLootTable(ref treasureInv, biomeLevel);
            }

            //Arcana Font
            else if (roomID == 1)
            {
                GameObject.Find("Special_Room_Data").GetComponent<Font_Database>().GetFontCard(ref fontCard, biomeLevel);
            }

        }
        else if(isStart)
        {
            roomID = Random.Range(1, 2);
        }
        else if(isEnd)
        {
            roomID = Random.Range(1, 2);
        }
        else if(isBoss)
        {
            roomID = Random.Range(1, 2);
        }



        if (level == 0)
        {
            roomName = "Town_" + roomNum;
            return roomName;
        }

        else if(level == 1)
        {
            if(isSpecial)
            {
                roomName = "Forest_" + roomID + "_Special";
                //Debug.Log(roomName);
                return roomName;
            }
            else if(isStart)
            {
                roomName = "Forest_" + roomID + "_Start";
                //Debug.Log(roomName);
                return roomName;
            }
            else if(isEnd)
            {
                roomName = "Forest_" + roomID + "_Exit";
                //Debug.Log(roomName);
                return roomName;
            }
            else if(isBoss)
            {
                roomName = "Forest_" + roomID + "_Boss";
                //Debug.Log("!!!!" + roomName);
                return roomName;
            }


            roomName = "Forest_" + roomID;
            //Debug.Log(roomName);
            return roomName;
        }

        else if(level == 2)
        {
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

        }

        roomName = "Cave_" + roomID;
        //Debug.Log(roomName);
        return roomName;
    }
}
