using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Bestiary_Database : MonoBehaviour
{
    public static bool init = false;

    public static Entry[] monsterDB = new Entry[46];

    public GameObject bestiaryPage;

	// Use this for initialization
	void Start ()
    {
        InitDB();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void InitDB()
    {
        if(!init)
        {
            init = true;
            for (int i = 0; i < monsterDB.Length; i++)
            {
                monsterDB[i] = new Entry(
                    //Name
                    "Empty",
                    //Health
                    "50",
                    //Weakness
                    "Fire",
                    //Drop
                    "Red Mushroom\nUnidentified Ring",
                    //Arcana
                    "x3 Earth\nx1 Life",
                    //Desc
                    "Mushy!",
                    //Sprite
                    "Mushy",
                    //Background
                    "Forest_Back",
                    //Foreground
                    "Forest_Fore",
                    //Discovered?
                    false
                    );
            }

            //string name, string weak, string drop, string arcana, string desc, string sprite, string back, string fore

            //Monster 1 - Mushy
            monsterDB[0] = new Entry(
                //Name
                "Mushy",
                //Health
                "50",
                //Weakness
                "Fire",
                //Drop
                "Red Mushroom\nUnidentified Ring",
                //Arcana
                "x3 Earth\nx1 Life",
                //Desc
                "Found in the <color=yellow>Koros Forest</color>\n\nThe Mushy is a mushroom monster that, when sensing danger, spews toxic bubbles that have a chance to poison on contact. The best way to deal with these bubbles is to attack and pop them avoiding direct contact.",
                //Sprite
                "Mushy",
                //Background
                "Forest_Back",
                //Foreground
                "Forest_Fore",
                //Discoverd?
                false
                );

            //Monster 2 - Green Slime
            monsterDB[1] = new Entry(
                //Name
                "G-Slime",
                //Health
                "65",
                //Weakness
                "Earth",
                //Drop
                "G-Slime Crystal\nUnidentified Ring",
                //Arcana
                "x2 Water\nx1 Air\nx1 Life",
                //Desc
                "Found in the <color=yellow>Koros Forest</color>\n\nThe green slime species is native to the Koros Forest. While they are small in nature, slimes have the unique ability to combine and grow larger one day becoming a slime king.",
                //Sprite
                "Green_Slime",
                //Background
                "Forest_Back",
                //Foreground
                "Forest_Fore",
                //Discovered?
                false
                );

            //Monster 3 - Orbo
            monsterDB[2] = new Entry(
                //Name
                "Orbo",
                //Health
                "65",
                //Weakness
                "Water",
                //Drop
                "Hard Carapace\nUnidentified Ring",
                //Arcana
                "x2 Earth\nx1 Life",
                //Desc
                "Found in the <color=yellow>Koros Forest</color>\n\nOrbos are a type of land oyster, which have hard exterior shells that protect soft and gooey innards. Orbos move in quick bursts to evade enemies, and when threatened will open their shells and launch hard pearl-like projectiles, some of which can be deflected.",
                //Sprite
                "Orbo",
                //Background
                "Forest_Back",
                //Foreground
                "Forest_Fore",
                //Discovered?
                false
                );

            //Monster 4 - Masky
            monsterDB[3] = new Entry(
                //Name
                "Masky",
                //Health
                "80",
                //Weakness
                "Water",
                //Drop
                "Hard Carapace\nUnidentified Ring",
                //Arcana
                "x3 Earth\nx1 Life",
                //Desc
                "Found in the <color=yellow>Koros Forest</color>\n\nCalled a Masky becuase of its sharp resemblance to a wooden mask. This monster tends to skirt the treeline in an attempt to line up targets and then unleashing a high speed barrage of projectiles.",
                //Sprite
                "Masky",
                //Background
                "Forest_Back",
                //Foreground
                "Forest_Fore",
                //Discoverd?
                false
                );

            //Monster 5 - Voodoo
            monsterDB[4] = new Entry(
                //Name
                "Voodoo",
                //Health
                "40",
                //Weakness
                "Water",
                //Drop
                "Hard Carapace\nUnidentified Ring",
                //Arcana
                "x2 Fire\nx1 Earth\nx1 Life",
                //Desc
                "Found in the <color=yellow>Koros Forest</color>\n\nVoodoos are corrupted wood spirits that use spirit fires, which rotate around them, to damage enemies. The voodoo's spirit fires expand and contract making it far easier to damage them when they are contracted.",
                //Sprite
                "Voodoo",
                //Background
                "Forest_Back",
                //Foreground
                "Forest_Fore",
                //Discovered?
                false
                );

            //Monster 6 - Chomper
            monsterDB[5] = new Entry(
                //Name
                "Chomper",
                //Health
                "100",
                //Weakness
                "Fire",
                //Drop
                "Hard Carapace\nUnidentified Ring",
                //Arcana
                "x1 Water\nx3 Earth\nx1 Life",
                //Desc
                "Found in the <color=yellow>Koros Forest</color>\n\nChompers are carnivorous plants with hard shells which are impervious to physical damage. Chompers have vast root networks, which it uses to attack potential prey from below. Chompers can only be damaged by magic or when it's maw opens exposing the plant. Hitting the chomper with fire Arcana will also cause it to open its maw.",
                //Sprite
                "Chomper",
                //Background
                "Forest_Back",
                //Foreground
                "Forest_Fore",
                //Discovered
                false
                );

            //Monster 7 - Batty
            monsterDB[6] = new Entry(
                //Name
                "Batty",
                //Health
                "145",
                //Weakness
                "Air",
                //Drop
                "Bat Wing\nUnidentified Ring",
                //Arcana
                "x1 Earth\nx2 Air\nx1 Life",
                //Desc
                "Found in the <color=yellow>Mier Mines</color>\n\nBattys are a variety of bat that reside in the Mier Mines. While they are not bloodthirsty or carnivorous, they are still known to attack wayward travelers that cross their path.",
                //Sprite
                "Batty",
                //Background
                "Cave_Back",
                //Foreground
                "Cave_Fore",
                //Discovered
                false
                );

            //Monster 8 - Rokky
            monsterDB[7] = new Entry(
                //Name
                "Rokky",
                //Health
                "275",
                //Weakness
                "Water",
                //Drop
                "Bat Wing\nUnidentified Ring",
                //Arcana
                "x4 Earth\nx1 Life",
                //Desc
                "Found in the <color=yellow>Mier Mines</color>\n\nClosely resembling rocks, the monster known as the Rokky is commonly found in caves throughout Alteria. Rokkys are slow moving and when they feel threatened will launch small parts of themselves, which become smaller sentient beings known as Pebbles.",
                //Sprite
                "Rokky",
                //Background
                "Cave_Back",
                //Foreground
                "Cave_Fore",
                //Discovered
                false
                );

            //Monster 9 - Pebble
            monsterDB[8] = new Entry(
                //Name
                "Pebble",
                //Health
                "150",
                //Weakness
                "Water",
                //Drop
                "Bat Wing\nUnidentified Ring",
                //Arcana
                "x1 Earth",
                //Desc
                "Found in the <color=yellow>Mier Mines</color>\n\nPebbles are small monsters spawned from Rokkys. While alone they are relatively harmless, in large number they can grow to become a problem.",
                //Sprite
                "Pebble",
                //Background
                "Cave_Back",
                //Foreground
                "Cave_Fore",
                //Discovered
                false
                );

            //Monster 10 - Emburr
            monsterDB[9] = new Entry(
                //Name
                "Emburr",
                //Health
                "155",
                //Weakness
                "Water",
                //Drop
                "Bat Wing\nUnidentified Ring",
                //Arcana
                "x3 Fire",
                //Desc
                "Found in the <color=yellow>Mier Mines</color>\n\nPebbles are small monsters spawned from Rokkys. While alone they are relatively harmless, in large number they can grow to become a problem.",
                //Sprite
                "Pebble",
                //Background
                "Cave_Back",
                //Foreground
                "Cave_Fore",
                //Discovered
                false
                );

        }
    }

    public void AddEntry(string monsterName)
    {
        //Add Beastiary Entry
        switch (monsterName)
        {
            default:
                break;
            case "Mushy":
                if (!monsterDB[0].discovered)
                {
                    monsterDB[0].discovered = true;
                    GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
                    tempObj.GetComponent<Text>().text = "<color=yellow>Bestiary Entry Added</color>: " + monsterName;
                }
                break;
            case "G-Slime":
                if (!monsterDB[1].discovered)
                {
                    monsterDB[1].discovered = true;
                    GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
                    tempObj.GetComponent<Text>().text = "<color=yellow>Bestiary Entry Added</color>: " + monsterName;
                }
                break;
            case "Orbo":
                if (!monsterDB[2].discovered)
                {
                    monsterDB[2].discovered = true;
                    GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
                    tempObj.GetComponent<Text>().text = "<color=yellow>Bestiary Entry Added</color>: " + monsterName;
                }
                break;
            case "Masky":
                if (!monsterDB[3].discovered)
                {
                    monsterDB[3].discovered = true;
                    GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
                    tempObj.GetComponent<Text>().text = "<color=yellow>Bestiary Entry Added</color>: " + monsterName;
                }
                break;
            case "Voodoo":
                if (!monsterDB[4].discovered)
                {
                    monsterDB[4].discovered = true;
                    GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
                    tempObj.GetComponent<Text>().text = "<color=yellow>Bestiary Entry Added</color>: " + monsterName;
                }
                break;
            case "Chomper":
                if (!monsterDB[5].discovered)
                {
                    monsterDB[5].discovered = true;
                    GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
                    tempObj.GetComponent<Text>().text = "<color=yellow>Bestiary Entry Added</color>: " + monsterName;
                }
                break;
            case "Batty":
                if (!monsterDB[6].discovered)
                {
                    monsterDB[6].discovered = true;
                    GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
                    tempObj.GetComponent<Text>().text = "<color=yellow>Bestiary Entry Added</color>: " + monsterName;
                }
                break;
            case "Rokky":
                if (!monsterDB[7].discovered)
                {
                    monsterDB[7].discovered = true;
                    GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
                    tempObj.GetComponent<Text>().text = "<color=yellow>Bestiary Entry Added</color>: " + monsterName;
                }
                break;
            case "Pebble":
                if (!monsterDB[8].discovered)
                {
                    monsterDB[8].discovered = true;
                    GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
                    tempObj.GetComponent<Text>().text = "<color=yellow>Bestiary Entry Added</color>: " + monsterName;
                }
                break;
        }
        bestiaryPage.SetActive(true);
        bestiaryPage.GetComponent<Beastiary_Controller>().UpdatePage();
        bestiaryPage.SetActive(false);
    }
        
}
