using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troop_Database : MonoBehaviour
{
    public static bool init = false;

    public static string[][] weissTroop = new string[9][];

    public static string[][] korosTroop = new string[25][];

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    /// <summary>
    /// as
    /// </summary>
    public static void initTroops()
    {
        //Weiss Troops
        weissTroop[0] = new string[5] { "Oculops", "", "", "", "" };
        weissTroop[1] = new string[5] { "Oculops", "Oculops", "", "", "" };
        weissTroop[2] = new string[5] { "Oculops", "Oculops", "Oculops", "", "" };
        weissTroop[3] = new string[5] { "Goblin", "", "", "", "" };
        weissTroop[4] = new string[5] { "Goblin", "Goblin", "", "", "" };
        weissTroop[5] = new string[5] { "Goblin", "Goblin", "Goblin", "", "" };
        weissTroop[6] = new string[5] { "Goblin", "Oculops", "", "", "" };
        weissTroop[7] = new string[5] { "Goblin", "Oculops", "Goblin", "", "" };
        weissTroop[8] = new string[5] { "Goblin", "Oculops", "Oculops", "", "" };

        //Koros Troops
        korosTroop[0] = new string[5] { "Mushy", "", "", "", "" };
        korosTroop[1] = new string[5] { "Mushy", "Mushy", "", "", "" };
        korosTroop[2] = new string[5] { "Mushy", "Mushy", "Mushy", "", "" };
        korosTroop[3] = new string[5] { "Mushy", "Mushy", "Mushy", "Mushy", "" };
        korosTroop[4] = new string[5] { "G-Slime", "", "", "", "" };
        korosTroop[5] = new string[5] { "G-Slime", "G-Slime", "", "", "" };
        korosTroop[6] = new string[5] { "G-Slime", "G-Slime", "G-Slime", "", "" };
        korosTroop[7] = new string[5] { "G-Slime", "G-Slime", "G-Slime", "G-Slime", "" };
        korosTroop[8] = new string[5] { "Orbo", "", "", "", "" };
        korosTroop[9] = new string[5] { "Orbo", "Orbo", "", "", "" };
        korosTroop[10] = new string[5] { "Orbo", "Orbo", "Orbo", "", "" };
        korosTroop[11] = new string[5] { "Masky", "Masky", "", "", "" };
        korosTroop[12] = new string[5] { "Masky", "Masky", "Masky", "", "" };
        korosTroop[13] = new string[5] { "Chomper", "", "", "", "" };
        korosTroop[14] = new string[5] { "Chomper", "Chomper", "", "", "" };

        korosTroop[15] = new string[5] { "Mushy", "G-Slime", "G-Slime", "", "" };
        korosTroop[16] = new string[5] { "Mushy", "Mushy", "Mushy", "G-Slime", "G-Slime" };
        korosTroop[17] = new string[5] { "Orbo", "G-Slime", "G-Slime", "Voodoo", "Voodoo" };
        korosTroop[18] = new string[5] { "Voodoo", "Voodoo", "", "", "" };
        korosTroop[19] = new string[5] { "Voodoo", "G-Slime", "G-Slime", "Voodoo", "" };
        korosTroop[20] = new string[5] { "Mushy", "G-Slime", "G-Slime", "Masky", "Masky" };
        korosTroop[21] = new string[5] { "Mushy", "G-Slime", "G-Slime", "Chomper", "" };
        korosTroop[22] = new string[5] { "G-Slime", "G-Slime", "G-Slime", "Orbo", "" };
        korosTroop[23] = new string[5] { "Masky", "G-Slime", "G-Slime", "", "" };
        korosTroop[24] = new string[5] { "Orbo", "G-Slime", "G-Slime", "Chomper", "Orbo" };



    }

    public static GameObject GetEnemy(string enemy)
    {
        switch(enemy)
        {
            default:
                break;
            case "Mushy":
                return Resources.Load("Prefabs/Enemies/Mushy") as GameObject;
            case "G-Slime":
                return Resources.Load("Prefabs/Enemies/Slime_Green") as GameObject;
            case "Orbo":
                return Resources.Load("Prefabs/Enemies/Orbo") as GameObject;
            case "Masky":
                return Resources.Load("Prefabs/Enemies/Masky") as GameObject;
            case "Voodoo":
                return Resources.Load("Prefabs/Enemies/Voodoo") as GameObject;
            case "Chomper":
                return Resources.Load("Prefabs/Enemies/Chomper") as GameObject; 
            case "Batty":
                return Resources.Load("Prefabs/Enemies/Batty") as GameObject;
            case "Rokky":
                return Resources.Load("Prefabs/Enemies/Rokky") as GameObject;
            case "Emburr":
                return Resources.Load("Prefabs/Enemies/Emburr") as GameObject;
            case "Oculops":
                return Resources.Load("Prefabs/Enemies/Oculops") as GameObject;
            case "Goblin":
                return Resources.Load("Prefabs/Enemies/Goblin") as GameObject;
        }
        return null;
    }
}
