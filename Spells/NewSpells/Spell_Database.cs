using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Database : MonoBehaviour
{
    public static bool init = false;

    public static List<Spell> spellData = new List<Spell>();

	// Use this for initialization
	void Awake ()
    {
	    if(!init)
        {
            init = true;
            //0 No Card
            spellData.Add(new Spell("None", 0, 0, 0, 0, 0));
            //1 Embers
            spellData.Add(new Spell("Embers", 1, 2, 5, 1, 1));
            //2 Firebolt
            spellData.Add(new Spell("Firebolt", 2, 4, 5, 3, 1));
            //3 Blaze
            spellData.Add(new Spell("Blaze", 3, 4, 4, 3, 2));
            //4 Magma
            spellData.Add(new Spell("Magma", 4, 4, 5, 3, 3));
            //5 Flame Wave
            spellData.Add(new Spell("Flame Wave", 5, 2, 3, 1, 2));
            //6 Enfire
            spellData.Add(new Spell("Enfire", 6, 0, 0, 0, 1));

            //9 Stone
            spellData.Add(new Spell("Stone", 9, 1, 1, 1, 1));
            //10 Boulder
            spellData.Add(new Spell("Boulder", 10, 1, 1, 1, 2));
            //11 Earthen
            spellData.Add(new Spell("Earthen", 11, 1, 1, 1, 1));
            //12 Impale
            spellData.Add(new Spell("Impale", 12, 1, 1, 1, 2));
            //13 Rupture
            spellData.Add(new Spell("Rupture", 13, 4, 5, 3, 2));

            //17 Venom
            spellData.Add(new Spell("Venom", 17, 1, 1, 1, 1));
            //18 Enliven
            spellData.Add(new Spell("Enliven", 18, 1, 1, 1, 2));
            //19 Entangle
            spellData.Add(new Spell("Entangle", 19, 1, 1, 1, 2));
            //20 Mend
            spellData.Add(new Spell("Mend", 20, 1, 1, 1, 1));

            //25 Bubble
            spellData.Add(new Spell("Bubble", 25, 1, 1, 1, 1));
            //26 Water
            spellData.Add(new Spell("Water", 26, 1, 1, 1, 2));
            //27 Entomb
            spellData.Add(new Spell("Entomb", 27, 0, 0, 0, 2));
            //28 Icicle
            spellData.Add(new Spell("Icicle", 28, 1, 1, 1, 1));

            //33 Twister
            spellData.Add(new Spell("Twister", 33, 1, 1, 1, 2));
            //34 Zap
            spellData.Add(new Spell("Zap", 34, 1, 1, 1, 1));
            //35 Ball
            spellData.Add(new Spell("Ball", 35, 1, 1, 1, 2));
            //36 Razor
            spellData.Add(new Spell("Razor", 36, 1, 1, 1, 2));
        }	
	}

    public Spell FindSpell(int id)
    {
        Spell foundSpell = spellData[0];
        for (int i = 0; i < spellData.Count; i++)
        {
            if(spellData[i].spellID == id)
            {
                foundSpell = spellData[i];
                return foundSpell;
            }
        }
        return foundSpell;
    }
	



}
