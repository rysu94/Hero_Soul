using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionDatabase : MonoBehaviour
{
    public static bool init = false;

    public static int[][] healthPot = new int[6][];
    public static int[][] healthQuant = new int[10][];

    public static int[][] manaPot = new int[5][];
    public static int[][] manaQuant = new int[5][];

    public static int[][] endPot = new int[5][];
    public static int[][] endQuant = new int[5][];

    // Use this for initialization
    void Start ()
    {
		if(!init)
        {
            init = true;

            //Health Potion Potency
            healthPot[0] = new int[8] { 7, 5, 29, 3, 129, 1, 0, 0 };
            healthPot[1] = new int[8] { 7, 10, 29, 6, 129, 3, 0, 0 };
            healthPot[2] = new int[8] { 7, 15, 29, 9, 130, 1, 0, 0 };
            healthPot[3] = new int[8] { 7, 20, 29, 12, 130, 3, 0, 0 };
            healthPot[4] = new int[8] { 7, 25, 29, 15, 131, 1, 0, 0 };
            healthPot[5] = new int[8] { 7, 30, 29, 18, 131, 3, 0, 0 };

            //Health Potion Quantity
            healthQuant[0] = new int[8] { 7, 5, 29, 3, 127, 1, 0, 0 };
            healthQuant[1] = new int[8] { 7, 5, 29, 3, 127, 1, 0, 0 };
            healthQuant[2] = new int[8] { 7, 5, 29, 3, 127, 1, 0, 0 };
            healthQuant[3] = new int[8] { 7, 5, 29, 3, 127, 1, 0, 0 };
            healthQuant[4] = new int[8] { 7, 5, 29, 3, 127, 1, 0, 0 };
            healthQuant[5] = new int[8] { 7, 5, 29, 3, 127, 1, 0, 0 };
            healthQuant[6] = new int[8] { 7, 5, 29, 3, 127, 1, 0, 0 };
            healthQuant[7] = new int[8] { 7, 5, 29, 3, 127, 1, 0, 0 };
            healthQuant[8] = new int[8] { 7, 5, 29, 3, 127, 1, 0, 0 };
            healthQuant[9] = new int[8] { 7, 5, 29, 3, 127, 1, 0, 0 };
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
