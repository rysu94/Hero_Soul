using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mosaic_Manager : MonoBehaviour
{
    public static bool init = false;

    public static bool[] korosMosaic = new bool[9];
    public static bool[] mierMosaic = new bool[9];
    public static bool[] tombMosaic = new bool[9];
    public static bool[] darkMosaic = new bool[9];

    public static bool korosClaimed = false;
    public static bool mierClaimed = false;
    public static bool tombClaimed = false;
    public static bool darkClaimed = false;

	// Use this for initialization
	void Start ()
    {
        if(!init)
        {
            init = true;
            for (int i = 0; i < korosMosaic.Length; i++)
            {
                korosMosaic[i] = false;
                mierMosaic[i] = false;
                tombMosaic[i] = false;
                darkMosaic[i] = false;
            }
            korosMosaic[Random.Range(0,korosMosaic.Length)] = true;
            mierMosaic[Random.Range(0, mierMosaic.Length)] = true;
            tombMosaic[Random.Range(0, tombMosaic.Length)] = true;
            darkMosaic[Random.Range(0, darkMosaic.Length)] = true;

            korosClaimed = false;
            mierClaimed = false;
            tombClaimed = false;
            darkClaimed = false;
        }
        
	}
	
}
