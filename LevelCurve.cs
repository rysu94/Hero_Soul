using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class stores the information on the amount of XP needed for a level up

public class LevelCurve : MonoBehaviour
{
    public static int[] levelCurve = new int[25];

    public static void InitLevelCurve()
    {
        levelCurve[0] = 50;
        levelCurve[1] = 112;
        levelCurve[2] = 204;
        levelCurve[3] = 329;
        levelCurve[4] = 485;
        levelCurve[5] = 672;
        levelCurve[6] = 890;
        levelCurve[7] = 1137;
        levelCurve[8] = 1417;
        levelCurve[9] = 1723;
        levelCurve[10] = 2061;
        levelCurve[11] = 2425;
        levelCurve[12] = 2817;
        levelCurve[13] = 3237;
        levelCurve[14] = 3681;
        levelCurve[15] = 4151;
        levelCurve[16] = 4645;
        levelCurve[17] = 5162;
        levelCurve[18] = 5701;
        levelCurve[19] = 6262;
        levelCurve[20] = 6843;
        levelCurve[21] = 7442;
        levelCurve[22] = 8061;
        levelCurve[23] = 8694;
        levelCurve[24] = 9346;
    }
}
