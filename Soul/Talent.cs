using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Talent : MonoBehaviour
{
    //talent name
    public string talentName;
    //talent Img
    public string talentIMG;

    //STR
    public int talentStr;
    //DEX
    public int talentDex;
    //END
    public int talentEnd;
    //INT
    public int talentInt;
    //WIS
    public int talentWis;
    //VIT
    public int talentVit;
    //DEF
    public int talentDef;
    //Leech
    public int talentLeech;
    //Plunder
    public int talentPlund;
    //Deck
    public int talentDeck;
    //Alc
    public int talentAlc;
    //Collector
    public int talentCollect;
    //Assassin
    public int talentAss;
    //Hunt
    public int talentHunt;
    //Evasion
    public int talentEva;
    //Armor
    public int talentArm;
    //Shield
    public int talentShield;
    //Berserk
    public int talentBerserk;
    //Speed
    public int talentSpeed;
    //Twin
    public int talentTwin;

    //Description
    public string talentDesc;


    public Talent(string name, string img, int str, int dex, int end, int intel, int wis, int vit, int def, 
        int leech, int plund, int alc, int collect, int ass, int hunt, int eva, int arm, int shield, int berserk, int deck, int speed, int twin, string desc)
    {
        talentName = name;
        talentIMG = img;
        talentStr = str;
        talentDex = dex;
        talentEnd = end;
        talentInt = intel;
        talentWis = wis;
        talentVit = vit;
        talentDef = def;
        talentLeech = leech;
        talentPlund = plund;
        talentAlc = alc;
        talentCollect = collect;
        talentAss = ass;
        talentHunt = hunt;
        talentEva = eva;
        talentArm = arm;
        talentShield = shield;
        talentBerserk = berserk;
        talentDeck = deck;
        talentSpeed = speed;
        talentTwin = twin;
        talentDesc = desc;
    }

}
