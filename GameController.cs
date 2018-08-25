using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    //Controls whether monsters will be paused
    public static bool paused = false;

    public static bool xbox360_Enabled = false;

    public static float gameTime;


    //Cecilia Tutorial Vars








    public static bool xbox360Enabled()
    {
        string[] names = Input.GetJoystickNames();
        //print(names.Length);
        for (int i = 0; i < names.Length; i++)
        {
            if(names[i].Length > 0)
            {
                return true;
            }
        }
        return false;
    }
}
