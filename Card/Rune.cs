using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune
{
    public string runeName;

    //0, none  1, red  2, blue
    public List<int> runes = new List<int>();
    public List<bool> runeUnlocks = new List<bool>();
    public List<int> equippedRunes = new List<int>();

    //The card's ID
    public int cardID;

    //Card's base stats
    public int cardType;

    public int cardDamage, cardVelocity, cardKnockback;

    //Empty Rune
    public Rune()
    {

    }

    //Basic Rune
    public Rune(string name, int slot1, int slot2, int slot3, int id)
    {
        runeName = name;

        if(slot1 > 0)
        {
            runes.Add(slot1);
        }
        if (slot2 > 0)
        {
            runes.Add(slot2);
        }
        if (slot3 > 0)
        {
            runes.Add(slot3);
        }

        for (int i = 0; i < runes.Count; i++)
        {
            runeUnlocks.Add(false);
            equippedRunes.Add(-1);

        }
        cardID = id;
        cardType = -1;
    }

    //Damaging Spell
    public Rune(string name, int slot1, int slot2, int slot3, int id,int type, int dmg, int vel, int knock)
    {
        runeName = name;

        if (slot1 > 0)
        {
            runes.Add(slot1);
        }
        if (slot2 > 0)
        {
            runes.Add(slot2);
        }
        if (slot3 > 0)
        {
            runes.Add(slot3);
        }

        for (int i = 0; i < runes.Count; i++)
        {
            runeUnlocks.Add(false);
            equippedRunes.Add(-1);

        }
        cardID = id;

        cardType = type;
        cardDamage = dmg;
        cardVelocity = vel;
        cardKnockback = knock;
    }
}
