using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket
{
    public string runeName;
    public string runeDesc;

    public int runeDamage;
    public int runeVel;
    public int runeKnock;

    //Empty Ctor
    public Socket()
    {

    }

    public Socket(string name, string desc, int dmg, int vel, int knock)
    {
        runeName = name;
        runeDesc = desc;

        runeDamage = dmg;
        runeVel = vel;
        runeKnock = knock;
    }

}
