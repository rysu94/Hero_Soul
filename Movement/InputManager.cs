using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    //Left Joystick Input & WSAD/ArrowKeys
    public static float MainHorizontal()
    {
        float r = 0;
        r += Input.GetAxis("J_MainHorizontal");
        r += Input.GetAxis("K_MainHorizontal");
        return Mathf.Clamp(r, -1f, 1f);
    }

    public static float MainVertical()
    {
        float r = 0;
        r += Input.GetAxis("J_MainVertical");
        r += Input.GetAxis("K_MainVertical");
        return Mathf.Clamp(r, -1f, 1f);
    }

    public static Vector2 MainJoystick()
    {
        return new Vector2(MainHorizontal(), MainVertical());
    }

    public static float CursorHorizontal()
    {
        float r = 0;
        r += Input.GetAxis("J_CursorHorizontal");
        return Mathf.Clamp(r, -1f, 1f);
    }

    public static float CursorVertical()
    {
        float r = 0;
        r += Input.GetAxis("J_CursorVertical");
        return Mathf.Clamp(r, -1f, 1f);
    }

    public static Vector2 CursorJoystick()
    {
        return new Vector2(CursorHorizontal(), CursorVertical());
    }

    //--Button Input--------------------------------------

    //A Button on Xbox Controller & F Interact on keyboard
    public static bool A_Button()
    {
        return Input.GetButtonDown("A_Button");
    }

    public static bool B_Button()
    {
        return Input.GetButtonDown("B_Button");
    }

    public static bool X_Button()
    {
        return Input.GetButtonDown("X_Button");
    }

    public static bool Y_Button()
    {
        return Input.GetButtonDown("Y_Button");
    }

    public static bool L_Bumper()
    {
        return Input.GetButtonDown("J_LBumper");
    }

    public static bool R_Bumper()
    {
        return Input.GetButtonDown("J_RBumper");
    }

    public static bool J_Cast()
    {
        return Input.GetButtonDown("J_Cast");
    }

    public static bool J_Tab()
    {
        return Input.GetButtonDown("J_Tab");
    }

    public static bool J_Space()
    {
        return Input.GetButtonDown("J_Space");
    } 

    public static float J_Trigger()
    {
        float r = 0;
        r += Input.GetAxis("J_Triggers");
        return Mathf.Clamp(r, -1f, 1f);
    }

    public static float J_DPadHorizontal()
    {
        float r = 0;
        r += Input.GetAxis("J_DPadHorizontal");
        return Mathf.Clamp(r, -1f, 1f);
    }

    public static float J_DPadVertical()
    {
        float r = 0;
        r += Input.GetAxis("J_DPadVertical");
        return Mathf.Clamp(r, -1f, 1f);
    }

    public static bool J_Back()
    {
        return Input.GetButtonDown("J_Back");
    }

}
