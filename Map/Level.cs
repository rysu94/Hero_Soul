using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
Hero Project Level Class
By: Ryan Su

This class contains the information used to generate levels and rooms.

*/


public class Level 
{
    //Properties of a Level

    //Number of rows and columns the level grid contains
    public int levelRow;
    public int levelColumn;

    //Determines the level tile type, ie forest, desert, etc.
    // 1 = Forest of Beginning
    public int levelType;

    //This is the number of steps in the level's critical path
    public int critPath;

    //This the the number of side branches off the critical path
    public int numBranches;

    //Starting Coords
    public int startX;
    public int startY;

    //Player Start coords
    public int playerX;
    public int playerY;

    //Level's Name
    public string levelName;

    //Does the level have a boss
    public bool isBoss;

    //Does the level have a shop?
    public bool shopMade;

    //Does the level have a mosaic?
    public bool mosaicMade;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //The Level Class constructor
    public Level(int row, int col, int type, int crit, int branch, int x, int y, string name, bool boss, bool shop)
    {
        levelRow = row;
        levelColumn = col;
        levelType = type;
        critPath = crit;
        numBranches = branch;
        startX = x;
        startY = y;
        playerX = x;
        playerY = y;
        isBoss = boss;
        shopMade = shop;
        mosaicMade = false;
    }
}
