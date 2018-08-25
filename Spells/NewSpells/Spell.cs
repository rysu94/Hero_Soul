using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell
{
    public string spellName;

    public int spellID;

    public int spellDamage, spellVelocity, spellKnockback;

    public int spellDuration;

    public int spellCost;

    public Spell()
    {

    }

    public Spell(string name, int id, int dmg, int vel, int knock, int cost)
    {
        spellName = name;
        spellID = id;
        spellDamage = dmg;
        spellVelocity = vel;
        spellKnockback = knock;
        spellCost = cost;
    }

}
