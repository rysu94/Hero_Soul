using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

/*
Hero Project Level Creator
By: Ryan Su

This scripts generates the grid which stores the rooms on a level

*/


public class LevelCreator : MonoBehaviour
{

    public int tileSize = 48;

    public static bool newLevel = true;

    public int levelInt;

    //This contains the loaded level to be used to generate the grid
    public Level loadedLevel;

    //This is the Grid that hold the level layout
    public static Room[,] levelGrid;

    //The x and y dimensions of the grid
    public static int gridX;
    public static int gridY;

    //The starting coodinates for the walk
    public int startX;
    public int startY;

    //The current coordinates of the walk
    public int currentX;
    public int currentY;
    public int peekX;
    public int peekY;
    bool isValidCoordinate;

    //The room number generated in sequence, used to determine start and end rooms
    public int roomNum;

    //Holds information about the Level tiles the Room will use
    public int levelType;

    //These determine how many rooms are to be generated
    public int critSteps;
    public int numBranches;

    //This is the string that will be used to print the grid to the console
    public string testGrid;

    //These are the Coords to start the Player
    public GameObject player;
    public Animator playerAnim;
    public static float playerStartX = 0;
    public static float playerStartY = 0;
    public static string startTag = "Down";

    //Manages Fading in the screen
    public GameObject screenWipe;
    public Animator fadeIn;

    public string levelString;

    //These Manage the Door on the level
    public GameObject north;
    public GameObject west1;
    public GameObject west2;
    public GameObject west3;
    public GameObject south1;
    public GameObject south2;
    public GameObject south3;
    public GameObject east1;
    public GameObject east2;
    public GameObject east3;

    public bool isOpen;

    //These vairables manage where the player is in the level grid
    public static int playerCurrentX;
    public static int playerCurrentY;

    //This tells which level info to load
    public int levelTag;

    public bool loadBoss;

    public static bool startWipe = false;

    public static bool potionEnabled = true;
    public GameObject potion1;
    public GameObject potion2;

    public GameObject breakFrame;
    public static bool breakEnabled = true;

    public bool resetCursor = false;

    public static GameObject nodeGrid;

    // Use this for initialization
    void Start()
    {
        //This only runs if the level needs to be made
        if (newLevel)
        {
            newLevel = false;


            //Load the level data of the level we want generated
            if (levelTag == 0)
            {
                loadedLevel = LevelDatabase.weiss;
                GenTown(loadedLevel);
            }

            else if (levelTag == 1)
            {
                loadedLevel = LevelDatabase.forestOfBeginning;
                LevelDatabase.forestOfBeginning.shopMade = false;
                //Call function to generate the level based on loaded level's properties
                GenLevel(loadedLevel);
            }

            else if (levelTag == 2)
            {
                loadedLevel = LevelDatabase.mierMines;
                LevelDatabase.mierMines.shopMade = false;
                GenLevel(loadedLevel);
            }

            else if(levelTag == 3)
            {
                playerStartX = -0.571f;
                playerStartY = 0.153f;
                startTag = "Right";
                TestCharController.arcanaEnabled = false;
                potionEnabled = false;
                loadedLevel = LevelDatabase.lakesideTraining;
                GenTown(loadedLevel);
            }
            else if(levelTag == 4)
            {
                loadedLevel = LevelDatabase.galahadTomb;
                LevelDatabase.galahadTomb.shopMade = false;
                GenLevel(loadedLevel);
            }
            else if(levelTag == 5)
            {
                loadedLevel = LevelDatabase.port;
                GenTown(loadedLevel);
            }
            else if (levelTag == 6)
            {
                loadedLevel = LevelDatabase.ceciliaWeiss;
                GenTown(loadedLevel);
            }
            else if (levelTag == 7)
            {
                loadedLevel = LevelDatabase.camp;
                GenTown(loadedLevel);
            }

            //Bring player to the level's start
            SceneManager.LoadScene(levelGrid[playerCurrentX, playerCurrentY].roomName);
            startWipe = true;
            //Reset the deck
            if (Deck.init)
            {
                Deck.ResetDeck();
            }
   

            //initialize the arcana deck
            if(!Deck.init)
            {
                Deck.StartDraw();
            }

            //Clear the buyback list
            Shop_Database.ClearBuyback();


        }
        

        //Plays the fade in animation for smooth screen transitions
        fadeIn = screenWipe.GetComponent<Animator>();
        fadeIn.Play("FadeIn");

        //Spawns player in the level
        playerAnim = player.GetComponent<Animator>();
        SpawnPlayer(playerStartX, playerStartY, startTag);

        //Prints the level grid to console, for testing only
        //PrintGrid(levelGrid, gridX, gridY);
        //print(playerCurrentX+ " " + playerCurrentY);

        GameObject.Find("MiniMap").GetComponent<Minimap>().GenMinimap(levelGrid, playerCurrentX, playerCurrentY);

        //Check if the minimap is toggled
        if (!TestCharController.miniMapToggle)
        {
            TestCharController.miniMap.SetActive(false);
        }

        //Create the pathing grid
        nodeGrid = Instantiate(Resources.Load("Prefabs/Pathing/Node_Grid"), new Vector2(0, 0), Quaternion.identity) as GameObject;

        //Spawn Puzzles
        //Determine which array to use

        //Koros
        if (levelTag == 1 || levelTag == 4)
        {
            //Is it a puzzle?
            bool isPuzzle = false;
            int puzzleType = 0;
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Puzzle"))
            {
                isPuzzle = true;
                puzzleType = enemy.GetComponent<Puzzle_Spawn>().puzzleType;
            }

            if (isPuzzle)
            {
                switch (puzzleType)
                {
                    default:
                        break;
                    case 1:
                        Instantiate(Resources.Load("Prefabs/Puzzles/Mastermind/Mastermind_Puzzle"), new Vector2(0, 0), Quaternion.identity);
                        break;
                    case 2:
                        if ((int)Time.time % 2 > 0)
                        {
                            Instantiate(Resources.Load("Prefabs/Puzzles/Runes/Rune_Puzzle"), new Vector2(0, 0), Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(Resources.Load("Prefabs/Puzzles/Runes/Rune_Puzzle_2"), new Vector2(0, 0), Quaternion.identity);
                        }

                        break;
                }
            }


            //Spawn Troop
            if (!levelGrid[playerCurrentX, playerCurrentY].roomClear && !levelGrid[playerCurrentX, playerCurrentY].isBoss)
            {
            //print("Loaded Level " + levelTag);
            nodeGrid.GetComponent<Path_Nodes>().UpdateNodes();

            //Initialize the troop database
            if(!Troop_Database.init)
            {
                Troop_Database.initTroops();
                Troop_Database.init = true;
            }
            
           

                bool isBoss = false;
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    if(enemy.GetComponent<Monster>().boss)
                    {
                        isBoss = true;
                    }
                }


                if (!isPuzzle && !isBoss)
                {
                    int troopIndex = Random.Range(0, Troop_Database.korosTroop.Length);
                    for (int i = 0; i < Troop_Database.korosTroop[troopIndex].Length; i++)
                    {
                        //Find a valid pathing node
                        bool valid = false;
                        int nodeIndex = 0;
                        int count = 0;
                        List<int> pickedNodes = new List<int>();
                        while (!valid && count < 50)
                        {
                            nodeIndex = Random.Range(44, 182);
                            if (nodeGrid.GetComponent<Path_Nodes>().nodeList[nodeIndex].GetComponent<Node>().valid
                                && nodeIndex != 49 && nodeIndex != 50 && nodeIndex != 51 && nodeIndex != 52 && nodeIndex != 53
                                && nodeIndex != 58 && nodeIndex != 59 && nodeIndex != 60 && nodeIndex != 61 && nodeIndex != 62 && nodeIndex != 63
                                && nodeIndex != 64 && nodeIndex != 79 && nodeIndex != 80 && nodeIndex != 81 && nodeIndex != 82 && nodeIndex != 83
                                && nodeIndex != 83 && nodeIndex != 84 && nodeIndex != 100 && nodeIndex != 101 && nodeIndex != 102 && nodeIndex != 103
                                && nodeIndex != 104 && nodeIndex != 105 && nodeIndex != 120 && nodeIndex != 121 && nodeIndex != 122 && nodeIndex != 123
                                && nodeIndex != 124 && nodeIndex != 125 && nodeIndex != 141 && nodeIndex != 142 && nodeIndex != 143 && nodeIndex != 144
                                && nodeIndex != 145 && nodeIndex != 146 && nodeIndex != 161 && nodeIndex != 162 && nodeIndex != 163 && nodeIndex != 164
                                && nodeIndex != 165 && nodeIndex != 166 && nodeIndex != 166 && nodeIndex != 172 && nodeIndex != 173 && nodeIndex != 174
                                && nodeIndex != 175 && nodeIndex != 176 && !pickedNodes.Contains(nodeIndex))
                            {
                                valid = true;
                                pickedNodes.Add(nodeIndex);
                            }
                            count++;
                        }

                        //print("Spawning " + Troop_Database.korosTroop[troopIndex][i]);
                        if (Troop_Database.korosTroop[troopIndex][i] != "")
                        {
                            Instantiate(Troop_Database.GetEnemy(Troop_Database.korosTroop[troopIndex][i]), nodeGrid.GetComponent<Path_Nodes>().nodeList[nodeIndex].transform.position, Quaternion.identity);
                        }
                    }
                }
                
            }
        }

        else if(levelTag == 0)
        {
            //Spawn Troop
            if (!levelGrid[playerCurrentX, playerCurrentY].roomClear && !levelGrid[playerCurrentX, playerCurrentY].isBoss)
            {
                //print("Loaded Level " + levelTag);
                nodeGrid.GetComponent<Path_Nodes>().UpdateNodes();

                //Initialize the troop database
                if (!Troop_Database.init)
                {
                    Troop_Database.initTroops();
                    Troop_Database.init = true;
                }



                bool isBoss = false;
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    if (enemy.GetComponent<Monster>().boss)
                    {
                        isBoss = true;
                    }
                }


                if (!isBoss)
                {
                    int troopIndex = Random.Range(0, Troop_Database.weissTroop.Length);
                    for (int i = 0; i < Troop_Database.weissTroop[troopIndex].Length; i++)
                    {
                        //Find a valid pathing node
                        bool valid = false;
                        int nodeIndex = 0;
                        int count = 0;
                        List<int> pickedNodes = new List<int>();
                        while (!valid && count < 50)
                        {
                            nodeIndex = Random.Range(44, 182);
                            if (nodeGrid.GetComponent<Path_Nodes>().nodeList[nodeIndex].GetComponent<Node>().valid
                                && nodeIndex != 49 && nodeIndex != 50 && nodeIndex != 51 && nodeIndex != 52 && nodeIndex != 53
                                && nodeIndex != 58 && nodeIndex != 59 && nodeIndex != 60 && nodeIndex != 61 && nodeIndex != 62 && nodeIndex != 63
                                && nodeIndex != 64 && nodeIndex != 79 && nodeIndex != 80 && nodeIndex != 81 && nodeIndex != 82 && nodeIndex != 83
                                && nodeIndex != 83 && nodeIndex != 84 && nodeIndex != 100 && nodeIndex != 101 && nodeIndex != 102 && nodeIndex != 103
                                && nodeIndex != 104 && nodeIndex != 105 && nodeIndex != 120 && nodeIndex != 121 && nodeIndex != 122 && nodeIndex != 123
                                && nodeIndex != 124 && nodeIndex != 125 && nodeIndex != 141 && nodeIndex != 142 && nodeIndex != 143 && nodeIndex != 144
                                && nodeIndex != 145 && nodeIndex != 146 && nodeIndex != 161 && nodeIndex != 162 && nodeIndex != 163 && nodeIndex != 164
                                && nodeIndex != 165 && nodeIndex != 166 && nodeIndex != 166 && nodeIndex != 172 && nodeIndex != 173 && nodeIndex != 174
                                && nodeIndex != 175 && nodeIndex != 176 && !pickedNodes.Contains(nodeIndex))
                            {
                                valid = true;
                                pickedNodes.Add(nodeIndex);
                            }
                            count++;
                        }

                        print("Spawning " + Troop_Database.weissTroop[troopIndex][i]);
                        if (Troop_Database.weissTroop[troopIndex][i] != "")
                        {
                            Instantiate(Troop_Database.GetEnemy(Troop_Database.weissTroop[troopIndex][i]), nodeGrid.GetComponent<Path_Nodes>().nodeList[nodeIndex].transform.position, Quaternion.identity);
                        }
                    }
                }

            }
        }


        //Checks if the room has been cleared
        if (levelGrid[playerCurrentX, playerCurrentY].roomClear)
        {
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                enemy.gameObject.SetActive(false);
            }
        }

        else if (!levelGrid[playerCurrentX, playerCurrentY].roomClear)
        {
            //print("false");
            levelGrid[playerCurrentX, playerCurrentY].enemyList.Clear();

            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                levelGrid[playerCurrentX, playerCurrentY].enemyList.Add(enemy);
            }

            /*
            foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("Puzzle"))
            {
                levelGrid[playerCurrentX, playerCurrentY].enemyList.Add(puzzle);
            }
            */

            levelGrid[playerCurrentX, playerCurrentY].enemyCount = levelGrid[playerCurrentX, playerCurrentY].enemyList.Count;
        }

        isOpen = false;

        //Check if the player has a deck
        int blankCount = 0;
        for(int i = 0; i < Deck.deckSize; i++)
        {
            if (Deck.playerDeck[i].cardID == 0)
            {
                blankCount++;
            }
        }
        if(blankCount >= 15 || !TestCharController.arcanaEnabled)
        {
            GameObject.Find("ArcanaFrame").SetActive(false);
            GameObject.Find("PlayerDeck").SetActive(false);
        }

        potion1 = GameObject.Find("Heal_Potion");
        potion2 = GameObject.Find("Mana_Potion");
        breakFrame = GameObject.Find("Ultimate_Bar_Frame");

        //Spawn Items
        for(int i = 0; i < levelGrid[playerCurrentX,playerCurrentY].roomItems.Count; i++)
        {
            levelGrid[playerCurrentX, playerCurrentY].roomItems[i].SpawnItem();
        }

        //Move Destructibles
        for(int i = 0; i < levelGrid[playerCurrentX, playerCurrentY].roomDestructibles.Count; i++)
        {
            levelGrid[playerCurrentX, playerCurrentY].roomDestructibles[i].AlignDestructible();
        }


    }

    // Update is called once per frame
    void Update()
    {
        if(!resetCursor)
        {
            resetCursor = true;
            //Change the mouse cursor
            Cursor.SetCursor((Texture2D)Resources.Load("Cursor"), Vector2.zero, CursorMode.Auto);
        }
        //Opens the doors if the room has been cleared
        if(levelGrid[playerCurrentX,playerCurrentY].roomClear && !isOpen)
        {
           OpenDoors();
           isOpen = true;
        }

        //Potions enabled?
        if (!potionEnabled)
        {
            potion1.SetActive(false);
            potion2.SetActive(false);
        }
        else if (potionEnabled)
        {
            potion1.SetActive(true);
            potion2.SetActive(true);         
        }


        //Break Enabled?
        if (!breakEnabled)
        {
            breakFrame.SetActive(false);
        }
        else
        {
            breakFrame.SetActive(true); 
        }


        //Set map name
        //mapName.text = loadedLevel.levelName;
    }

    //=====================================================
    //               Generate Dungeon
    //=====================================================

    // Generates a Random Level Grid based on the Level Data 
    public void GenLevel(Level loadedLevel)
    {
        //Extracts the loaded level's info
        GetLevelInfo(loadedLevel);

        roomNum = 1;

        //Generates an empty Grid of Rooms with the dimensions of the loaded level
        levelGrid = new Room[gridX, gridY];

        currentX = startX;
        currentY = startY;

        //Creates the first room of the Level
        levelGrid[startX, startY] = new Room(11, 7, levelType, roomNum, true, false, false, true, false, false);
        roomNum++;
        //print("Start:" + startX + " " + startY);

        //Next room after first always go north
        peekX = currentX;
        peekY = currentY;
        GoNorth(ref peekX, ref peekY);
        currentX = peekX;
        currentY = peekY;
        levelGrid[currentX, currentY] = new Room(11, 7, levelType, roomNum, false, false, false, false, false, false); 
        roomNum++;



        //Generates the critical path for the dungeon
        for (int i = 0; i < critSteps-3; i++)
        {
            //print("Step:" + (i+1));
            isValidCoordinate = false;

            int loopbreak = 0;
            while (!isValidCoordinate)
            {
                loopbreak++;
                peekX = currentX;
                peekY = currentY;

                //Determines the direction to take for the walk
                int tempInt = Random.Range(1, 4);
                if (tempInt == 1)
                {
                    GoNorth(ref peekX, ref peekY);
                }
                else if (tempInt == 2)
                {
                    GoEast(ref peekX, ref peekY);
                }
                else if (tempInt == 3)
                {
                    GoWest(ref peekX, ref peekY);
                }
                else if (tempInt == 4)
                {
                    GoSouth(ref peekX, ref peekY);
                }

                //check if the new peek coordinates are valid
                isValidCoordinate = CheckValid(peekX, peekY);

                //if no valid room can be found, the last created room becomes the end
                if (loopbreak > 100)
                {
                    levelGrid[currentX, currentY].isEnd = true;
                    levelGrid[currentX, currentY].roomClear = true;
                    levelGrid[currentX,currentY].roomName = levelGrid[currentX, currentY].GetRoom(levelGrid[currentX, currentY].biomeLevel);
                    break;
                }
            }

            if (loopbreak > 100)
            {
                critSteps = roomNum;
                break;
            }
            //set the new current x and y to the the peek coordinates
            currentX = peekX;
            currentY = peekY;

            //set the room number
            
            levelGrid[currentX, currentY] = new Room(11, 7, levelType, roomNum, false, false, false, false, false, false);
            roomNum++;
        }
        //Generate last room, always north
        peekX = currentX;
        peekY = currentY;
        GoNorth(ref peekX, ref peekY);
        currentX = peekX;
        currentY = peekY;
        
        if (loadBoss)
        {
            levelGrid[currentX, currentY] = new Room(11, 7, levelType, roomNum, false, false, false, false, false, true);
        }
        else
        {
            levelGrid[currentX, currentY] = new Room(11, 7, levelType, roomNum, false, true, false, true, false, false);
        }

        
        //Gen Critical Path End

        //Generate the Branches

        for (int i = 0; i < numBranches; i++)
        {
            GenBranches(ref levelGrid);
        }


    }

    //=====================================================
    //               Generate Towns
    //=====================================================

    void GenTown(Level loadLevel)
    {
        //Get the level's info
        GetLevelInfo(loadedLevel);

        //Determines the total number of rooms that need to be generated
        int totalRooms = gridX * gridY;

        //Generates an empty Grid of Rooms with the dimensions of the loaded level
        levelGrid = new Room[gridX, gridY];

        currentX = startX;
        currentY = startY;

        roomNum = 1;

        for (int i = 0; i < gridX; i++)
        {
            for(int j = 0; j < gridY; j++)
            {
                //Weiss
                if((loadedLevel.levelType == 0 || loadedLevel.levelType == 6) && ((i == 0 && j == 1) || (i == 1 && j == 0) || (i == 1 && j == 2) || (i == 2 && j == 1)))
                {
                    levelGrid[i, j] = new Room(11, 7, levelType, roomNum, false, false, false, false, false, false);
                }
                else if(loadedLevel.levelType == 0 && (i == 1 && j == 1))
                {
                    levelGrid[i, j] = new Room(11, 7, levelType, roomNum, false, false, false, false, false, true);
                }
                else if(loadedLevel.levelType == 0)
                {
                    levelGrid[i, j] = new Room(11, 7, levelType, roomNum, false, false, false, false, false, false);
                }
                //Lakeside Training
                else if(loadedLevel.levelType == 3 && i == 0 && j == 5)
                {
                    levelGrid[i, j] = new Room(11, 7, levelType, roomNum, false, true, false, true, false, false);
                }
                else if(loadedLevel.levelType == 3)
                {
                    levelGrid[i, j] = new Room(11, 7, levelType, roomNum, false, false, false, false, false, false);
                }
                //Port Almere
                else if(loadedLevel.levelType == 5 && (i == 0 && j == 1))
                {
                    levelGrid[i, j] = new Room(11, 7, levelType, roomNum, false, true, false, true, false, false);
                }

                else
                {
                    levelGrid[i, j] = new Room(11, 7, levelType, roomNum, false, false, false, true, false, false);
                }
                roomNum++;
            }
        }


    }

    //This function gets the level info from the Level Manager
    void GetLevelInfo(Level levelInfo)
    {
        gridX = levelInfo.levelRow;
        gridY = levelInfo.levelColumn;
        startX = levelInfo.startX;
        startY = levelInfo.startY;
        critSteps = levelInfo.critPath;
        numBranches = levelInfo.numBranches;
        playerCurrentX = levelInfo.playerX;
        playerCurrentY = levelInfo.playerY;
        levelType = levelInfo.levelType;
        loadBoss = levelInfo.isBoss;
    }


    //These functions handle navigating the grid and updating the current grid position.
    void GoNorth(ref int x, ref int y)
    {
        x -= 1;
    }

    void GoSouth(ref int x, ref int y)
    {
        x += 1;
    }

    void GoEast(ref int x, ref int y)
    {
        y += 1;
    }

    void GoWest(ref int x, ref int y)
    {
        y -= 1;
    }

    //Checks if the current x and Y are valid
    bool CheckValid(int x, int y)
    {

        //Check if Coord is in bounds
        if (x >= 0 && x < gridX)
        {
            if (y >= 0 && y < gridY)
            {
                //Check if the grid space is occupied
                if (levelGrid[x, y] == null)
                {
                    //print(x + " " + y);          
                    return true;
                }
            }
        }
        return false;
    }

    //Prints the Level Grid to the console
    public void PrintGrid(Room[,] levelGrid, int x, int y)
    {
        testGrid = null;
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                if (levelGrid[i, j] != null)
                {
                    if (levelGrid[i, j] == levelGrid[playerCurrentX, playerCurrentY])
                    {
                        testGrid += "p ";
                    }
                    else if (levelGrid[i, j] != levelGrid[playerCurrentX, playerCurrentY])
                    {
                        testGrid += "o ";
                    }
                }
                else if (levelGrid[i, j] == null)
                {
                    testGrid += "x ";
                }
            }
            testGrid += "\n";
        }
        print(testGrid);
    }

    void SpawnPlayer(float x, float y, string tag)
    {
        player.transform.position = new Vector2(x, y);
        if (tag == "Up")
        {
            playerAnim.Play("TestUpIdle");
        }
        else if (tag == "Left")
        {
            playerAnim.Play("TestLeftIdle");
        }
        else if (tag == "Right")
        {
            playerAnim.Play("TestRightIdle");
        }
        else if (tag == "Down")
        {
            playerAnim.Play("TestDownIdle");
        }

        //Spawn Companion
        if(levelTag == 3 || levelTag == 6 || levelTag == 7)
        {
            TestCharController.companionID = 0;
        }
        SpawnCompanion(tag);
    }
    /*=========================================================================
                               Companion 
    ===========================================================================*/
    public void SpawnCompanion(string tag)
    {
        switch (TestCharController.companionID)
        {
            default:
                break;
            case 0:
                GameObject.Find("Companion_HUD").SetActive(false);
                break;
            //Cecilia
            case 1:
                break;
            //Leon
            case 2:
                GameObject tempObj = Instantiate(Resources.Load("Prefabs/Companion/Leon_Companion"), new Vector2(player.transform.position.x, player.transform.position.y - .15f), Quaternion.identity) as GameObject;
                if (tag == "Up")
                {
                    tempObj.GetComponent<Animator>().Play("C_Leon_Up_Idle");
                }
                else if (tag == "Left")
                {
                    tempObj.GetComponent<Animator>().Play("C_Leon_Left_Idle");
                }
                else if (tag == "Right")
                {
                    tempObj.GetComponent<Animator>().Play("C_Leon_Right_Idle");
                }
                else if (tag == "Down")
                {
                    tempObj.GetComponent<Animator>().Play("C_Leon_Down_Idle");
                }
                break;
            //Risette
            case 3:
                break;
            //Sparrow
            case 4:
                break;
        }
    }

    //=====================================================
    //               Generate Branches
    //=====================================================

    //Determines how many empty spaces
    public int GetValidSurroundingRooms(Room[,] levelGrid, int roomX, int roomY)
    {
        int numBorder = 0;

        //Check North
        if ((roomX - 1) >= 0)
        {
            if (levelGrid[roomX - 1, roomY] == null)
            {
                numBorder++;
            }
        }

        //Check East
        if ((roomY + 1) < gridY)
        {
            if (levelGrid[roomX, roomY + 1] == null)
            {
                numBorder++;
            }
        }

        //Check West
        if ((roomY - 1) >= 0)
        {
            if (levelGrid[roomX, roomY - 1] == null)
            {
                numBorder++;
            }
        }

        //Check South
        if ((roomX + 1) < gridX)
        {
            if (levelGrid[roomX + 1, roomY] == null)
            {
                numBorder++;
            }
        }

        //print(numBorder);


        return numBorder;
    }


    //This function generates a branch off the critical path with a special room at the end
    //Each Branch will try to generate 3 rooms as long as there is available spaces, with the final room being a special room

    void GenBranches(ref Room[,] levelGrid)
    {
        //the total number of rooms generated so far
        int totalRooms = 0;

        //The variables that hold the coordinates where the branches are being made
        int branchX = 0;
        int branchY = 0;

        //An int used to generate random integers
        int tempInt = 0;

        //Fail Safe int to break out of while loops
        int loopBreak = 0;

        bool hasValidSpace = false;
        //Find a Room that has a valid space bordering it
        while (!hasValidSpace)
        {
            loopBreak++;

            //randomly choose the room numbers to generate the branches
            tempInt = Random.Range(2, critSteps-2);
            //print("Room#: " + tempInt);

            //find the Room in the level grid that has the matching Room Number
            for (int i = 0; i < gridX; i++)
            {
                for (int j = 0; j < gridY; j++)
                {


                    if (levelGrid[i, j] != null && levelGrid[i, j].roomNum == tempInt)
                    {
                        //print(i + " " + j);
                        branchX = i;
                        branchY = j;
                    }
                }
            }
            //Checks all surrounding rooms and counts how many valid spaces there are
            if (GetValidSurroundingRooms(levelGrid, branchX, branchY) > 0)
            {
                hasValidSpace = true;
                //print("Starting: " +branchX + " " + branchY);
            }

            //fail safe, breaks out of loop after 50 iterations
            if (loopBreak > 50)
            {
                print("50 iterations! 2nd");
                break;
            }

        }

        loopBreak = 0;
        //Loops until 3 rooms are made, if there are enough valid spaces
        while (totalRooms < 3)
        {
            //Generates a Branch Room
            hasValidSpace = false;
            loopBreak++;
            //Loop until no more valid rooms can be generated
            int loopBreak2 = 0;
            while (!hasValidSpace)
            {
                loopBreak2++;
                peekX = branchX;
                peekY = branchY;
                //Generate a random number to begin the walk
                tempInt = Random.Range(1, 5);
                if (tempInt == 1)
                {
                    GoNorth(ref peekX, ref peekY);
                }
                else if (tempInt == 2)
                {
                    GoSouth(ref peekX, ref peekY);
                }
                else if (tempInt == 3)
                {
                    GoEast(ref peekX, ref peekY);
                }
                else if (tempInt == 4)
                {
                    GoWest(ref peekX, ref peekY);
                }

                //check if the coordinates are valid
                hasValidSpace = CheckValid(peekX, peekY);
                if (loopBreak2 > 50)
                {
                    print("50 iterations! 1st while");
                    break;
                }
            }
            branchX = peekX;
            branchY = peekY;

            //Check around the new room to see if there is a valid space, if there continue the branch, if not end the branch
            if (GetValidSurroundingRooms(levelGrid, branchX, branchY) > 0)
            {
                //create the room
                if (totalRooms < 2)
                {
                    levelGrid[branchX, branchY] = new Room(11, 7, levelType, 0, false, false, false, false, false, false);
                    //print("Room Made @ " + branchX + " " + branchY);
                }
                //Create the special room
                else if (totalRooms >= 2)
                {
                    levelGrid[branchX, branchY] = new Room(11, 7, levelType, 0, false, false, true, true, false, false);
                    //print("Room Made @ " + branchX + " " + branchY);
                }

                totalRooms++;

            }
            else if (GetValidSurroundingRooms(levelGrid, branchX, branchY) <= 0)
            {
                //create last branch room
                levelGrid[branchX, branchY] = new Room(11, 7, levelType, 0, false, false, true, true, false, false);
                //print("Room Made @ " + branchX + " " + branchY);

                //exit the while loop
                break;
            }

            if (loopBreak > 50)
            {
                print("50 iterations! 1st while");
                break;
            }
        }

    }


    //open doors when room is clear
    public void OpenDoors()
    {
        if (playerCurrentX - 1 >= 0 && levelGrid[playerCurrentX - 1 ,playerCurrentY] != null)
        {
            north.gameObject.SetActive(false);
        }
        if (playerCurrentX + 1 < gridX  && levelGrid[playerCurrentX + 1, playerCurrentY] != null)
        {
            south1.gameObject.SetActive(false);
            south2.SetActive(false);
            south3.SetActive(false);
        }
        if (playerCurrentY + 1 < gridY && levelGrid[playerCurrentX, playerCurrentY + 1] != null)
        {
            east1.gameObject.SetActive(false);
            east2.gameObject.SetActive(false);
            east3.SetActive(false);
        }
        if (playerCurrentY - 1 >= 0 && levelGrid[playerCurrentX, playerCurrentY - 1] != null)
        {
            west1.gameObject.SetActive(false);
            west2.gameObject.SetActive(false);
            west3.SetActive(false);
        }

        //if it is a boss room, open the north door
        if(levelGrid[playerCurrentX, playerCurrentY].isBoss)
        {
            north.gameObject.SetActive(false);
        }

    }

    void CheckExplored(Room[,] levelGrid, int x, int y, ref GameObject tile)
    {
        if(levelGrid[x,y].isExplored)
        {
            Image tempImage = tile.GetComponent<Image>();
            tempImage.color = new Color(.5f, 1f, 0f, 1);
        }
    }





}
