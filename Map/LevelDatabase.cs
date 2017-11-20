using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
Hero Project Level Database
By: Ryan Su

Stores the properties of each levels.

*/


public class LevelDatabase : MonoBehaviour
{
    // public Level(int row, int col, int type, int room, int crit, int branch, int x, int y)
    public static List<Level> levelList = new List<Level>();


    //Level 0 (Town)
    public static Level weiss = new Level(3, 3, 0, 0, 0, 0, 1, "Weiss", false);

    //Level 1
    public static Level forestOfBeginning = new Level(25, 25, 1, 12, 2, 24, 12, "Forest of Beginning", true);

    //level 2
    public static Level mierMines = new Level(25, 25, 2, 12, 2, 24, 12, "Mier Mines", true);

    void Awake()
    {
        levelList.Add(weiss);
        levelList.Add(forestOfBeginning);
        levelList.Add(mierMines);
    }

}