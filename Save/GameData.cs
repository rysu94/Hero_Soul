using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class GameData
{
    //Save Index
    public int saveIndex;

    //Game Playtime
    public float playtime = 0;
    
    //Character Choice
    public int characterChoice;

    //Location
    public string mapName;

    //Character Stat Data
    public int strength, dexterity, intelligence, vitality, endurance, wisdom;
    public int strGrowth, dexGrowth, intGrowth, vitGrowth, endGrowth, wisGrowth;
    public int playerLevel, playerExp;
    public int breakMeter, breakLevel;

    //Charcter Inventory Data
    public Item[] playerInventory = new Item[36];
    public Item[] playerStash = new Item[36];
    public int playerGold;

    //Character Equipment Data
    public Item[] playerEquipment = new Item[7];

    //Arcana Deck Data
    public int fireArcana, earthArcana, lifeArcana, waterArcana, windArcana;
    public int deckSize;
    public Card[] playerDeck = new Card[35];

    //Talent Tree
    public List<Talent> playerTalentTree = new List<Talent>();
    public int[] playerSelectedSkill = new int[6];

    //Quest Data

    //Spellbook Data
    public bool[] playerSpellbook = new bool[41];

    //Bestiary Data

    //Story Flags

}
