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
    public static Level weiss = new Level(3, 3, 0, 0, 0, 0, 1, "Weiss", false, true);

    //Level 1
    public static Level forestOfBeginning = new Level(25, 25, 1, 12, 4, 24, 12, "Forest of Beginning", true, false);

    //level 2
    public static Level mierMines = new Level(25, 25, 2, 12, 2, 24, 12, "Mier Mines", true, false);

    //Level 3 (Basic Movement/Combat Tutorial)
    public static Level lakesideTraining = new Level(1, 6, 3, 0, 0, 0, 0, "Lakeside Training", false, true);

    //Level 4 Galahad Tomb
    public static Level galahadTomb = new Level(25, 25, 4, 12, 4, 24, 12, "Galahad's Tomb", true, false);

    //Level 5 Port
    public static Level port = new Level(2, 3, 5, 0, 0, 0, 1, "Port Almere", false, true);

    //Level 6 Weiss(Cecilia)
    public static Level ceciliaWeiss = new Level(3, 3, 6, 0, 0, 1, 0, "Weiss(Cecilia)", false, true);

    //Level 7 Camp
    public static Level camp = new Level(1, 1, 7, 0, 0, 0, 0, "Base Camp", false, true);

    void Awake()
    {
        levelList.Add(weiss);
        levelList.Add(forestOfBeginning);
        levelList.Add(mierMines);
        levelList.Add(lakesideTraining);
        levelList.Add(galahadTomb);
        levelList.Add(port);
        levelList.Add(ceciliaWeiss);
    }

}