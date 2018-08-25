using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune_Database : MonoBehaviour
{
    public static bool init = false;

    public static List<Rune> runeData = new List<Rune>();

    public static List<GameObject> redSocketData = new List<GameObject>();

    public static List<bool> unlockedRedRunes = new List<bool>();
    public static List<bool> unlockedBlueRunes = new List<bool>();

    public static List<Socket> socketData = new List<Socket>();

    //Contains the indexes 
    public static List<int> redRuneOrder = new List<int>();

    // Use this for initialization
    void Awake ()
    {
        InitializeRunes();
	}

    public Rune FindRune(int id)
    {
        for(int i = 0; i < runeData.Count; i++)
        {
            if (runeData[i].cardID == id)
            {
                return runeData[i];
            }
        }
        return runeData[0];
    }
	
    public static void InitializeRunes()
    {
        if (!init)
        {
            init = true;
            //0 Null
            runeData.Add(new Rune());
            //1  Embers
            runeData.Add(new Rune("Embers", 1, 0, 0, 1, 1, 2, 5, 1));
            //2 Fireball
            runeData.Add(new Rune("Firebolt", 1, 0, 0, 2, 1, 4, 5, 3));
            //3 Blaze
            runeData.Add(new Rune("Blaze", 1, 1, 0, 3, 1, 5, 3, 1));
            //4 Magma
            runeData.Add(new Rune("Magma", 1, 1, 0, 4, 1, 6, 5, 3));
            //5 Flame Wave
            runeData.Add(new Rune("Flame Wave", 1, 1, 1, 5, 1, 4, 4, 0));
            //6 Enfire
            runeData.Add(new Rune("Enfire", 2, 0, 0, 6));
            //7 Fire Spin
            runeData.Add(new Rune("Fire Spin", 1, 1, 1, 7));
            //8 Molten
            runeData.Add(new Rune("Molten", 1, 1, 1, 8));

            //9 Stone
            runeData.Add(new Rune("Stone", 1, 0, 0, 9));
            //10 Boulder
            runeData.Add(new Rune("Boulder", 1, 0, 0, 10));
            //11 Earthen
            runeData.Add(new Rune("Earthen", 2, 1, 0, 11));
            //12 Impale
            runeData.Add(new Rune("Impale", 1, 1, 0, 12));
            //13 Rupture
            runeData.Add(new Rune("Rupture", 1, 1, 0, 13));
            //14 Wall
            runeData.Add(new Rune("Wall", 2, 2, 0, 14));
            //15 Enstone
            runeData.Add(new Rune("Enstone", 2, 2, 0, 15));
            //16 Quake
            runeData.Add(new Rune("Quake", 1, 1, 1, 16));

            //17 Venom
            runeData.Add(new Rune("Venom", 1, 0, 0, 17));
            //18 Enliven
            runeData.Add(new Rune("Enliven", 2, 0, 0, 18));
            //19 Entangle
            runeData.Add(new Rune("Entangle", 1, 1, 0, 19));
            //20 Mend
            runeData.Add(new Rune("Mend", 2, 0, 0, 20));
            //21 Bloom
            runeData.Add(new Rune("Bloom", 2, 2, 0, 21));
            //22 Heal
            runeData.Add(new Rune("Heal", 2, 0, 0, 22));
            //23 Guardian
            runeData.Add(new Rune("Guardian", 1, 1, 2, 23));
            //24 Bless
            runeData.Add(new Rune("Bless", 2, 2, 2, 24));

            //25 Bubble
            runeData.Add(new Rune("Bubble", 1, 0, 0, 25));
            //26 Water
            runeData.Add(new Rune("Water", 1, 0, 0, 26));
            //27 Entomb
            runeData.Add(new Rune("Entomb", 1, 2, 0, 27));
            //28 Icicle
            runeData.Add(new Rune("Icicle", 1, 0, 0, 28));
            //29 Enwater
            runeData.Add(new Rune("Enwater", 2, 0, 0, 29));
            //30 Shatter
            runeData.Add(new Rune("Shatter", 1, 1, 0, 30));
            //31 Tidal
            runeData.Add(new Rune("Tidal", 1, 1, 1, 31));
            //32 Spout
            runeData.Add(new Rune("Spout", 1, 1, 1, 32));

            //33 Twister
            runeData.Add(new Rune("Twister", 1, 0, 0, 33));
            //34 Zap
            runeData.Add(new Rune("Zap", 1, 0, 0, 34));
            //35 Ball
            runeData.Add(new Rune("Ball", 1, 0, 0, 35));
            //36 Razor
            runeData.Add(new Rune("Razor", 1, 0, 0, 36));
            //37 Gust
            runeData.Add(new Rune("Gust", 1, 1, 0, 37));
            //38 Aero
            runeData.Add(new Rune("Aero", 2, 2, 0, 38));
            //39 Storm
            runeData.Add(new Rune("Storm", 1, 1, 1, 39));
            //40 Tempest
            runeData.Add(new Rune("Tempest", 1, 1, 1, 40));


            //Red Socket Data

            //0  Damage
            redSocketData.Add(Resources.Load<GameObject>("Prefabs/DeckBuilder/Socket_Runes/Red/Damage_Rune"));
            //1  Force
            redSocketData.Add(Resources.Load<GameObject>("Prefabs/DeckBuilder/Socket_Runes/Red/Force_Rune"));
            //2  Homing
            redSocketData.Add(Resources.Load<GameObject>("Prefabs/DeckBuilder/Socket_Runes/Red/Force_Rune"));
            //3  Pierce
            redSocketData.Add(Resources.Load<GameObject>("Prefabs/DeckBuilder/Socket_Runes/Red/Piercing_Rune"));
            //4  Velocity
            redSocketData.Add(Resources.Load<GameObject>("Prefabs/DeckBuilder/Socket_Runes/Red/Velocity_Rune"));
            //5  Splitting
            redSocketData.Add(Resources.Load<GameObject>("Prefabs/DeckBuilder/Socket_Runes/Red/Splitting_Rune"));
            //6  Looping
            redSocketData.Add(Resources.Load<GameObject>("Prefabs/DeckBuilder/Socket_Runes/Red/Looping_Rune"));

            //6  Charge
            redSocketData.Add(Resources.Load<GameObject>("Prefabs/DeckBuilder/Socket_Runes/Red/Charging_Rune"));

            //7  Bouncing
            redSocketData.Add(Resources.Load<GameObject>("Prefabs/DeckBuilder/Socket_Runes/Red/Bouncing_Rune"));
            //8  Leeching
            redSocketData.Add(Resources.Load<GameObject>("Prefabs/DeckBuilder/Socket_Runes/Red/Leeching_Rune"));
            //9  Chaos
            redSocketData.Add(Resources.Load<GameObject>("Prefabs/DeckBuilder/Socket_Runes/Red/Chaos_Rune"));
            //10  Multiply
            redSocketData.Add(Resources.Load<GameObject>("Prefabs/DeckBuilder/Socket_Runes/Red/Multiply_Rune"));
            //11  Orbiting
            redSocketData.Add(Resources.Load<GameObject>("Prefabs/DeckBuilder/Socket_Runes/Red/Orbiting_Rune"));


            for (int i = 0; i < redSocketData.Count; i++)
            {
                unlockedRedRunes.Add(false);
            }

            //Test Runes
            unlockedRedRunes[0] = true;
            unlockedRedRunes[1] = true;
            unlockedRedRunes[2] = true;

            //Generate rune order
            while (redRuneOrder.Count < redSocketData.Count)
            {
                int tempInt = Random.Range(0, redSocketData.Count);
                if (!redRuneOrder.Contains(tempInt))
                {
                    redRuneOrder.Add(tempInt);
                }
            }

            string testString = " ";
            for (int i = 0; i < redRuneOrder.Count; i++)
            {
                testString += redRuneOrder[i].ToString() + " ";
            }
            print(testString);


            //Blue Socket Data




            //Socket Data 

            //0 Rune of Bull
            socketData.Add(new Socket("Rune of the Bull", "Increase Damage.\n\n<color=lime>+Damage</color>\n<color=red>+Cost</color>", 2, 0, 0));
            //1 Rune of Hare
            socketData.Add(new Socket("Rune of the Hare", "Increase Velocity.\n\n<color=lime>+Velocity</color>", 0, 2, 0));
            //2 Rune of the Owl
            socketData.Add(new Socket("Rune of the Owl", "Causes projectile based Arcana to seek to nearby enemies.\n\n<color=red>-Velocity</color>", 0, -1, 0));
            //3 Rune of the Eagle
            socketData.Add(new Socket("Rune of the Eagle", "Causes projectiles to pierce enemies.", 0, 0, 0));
            //4 Rune of the Cheetah
            socketData.Add(new Socket("Rune of the Cheetah", "Increase velocity", 0, 0, 0));
            //5 Rune of the Crane
            socketData.Add(new Socket("Rune of the Crane", "Causes projectiles to splinter into smaller projectiles in flight.", 0, 0, 0));
            //6 Rune of the Pig
            socketData.Add(new Socket("Rune of the Pig", "Causes your projectiles to draw enemies towards them.", 0, 0, 0));
            //7 Rune of the Fox
            socketData.Add(new Socket("Rune of the Fox", "Increase velocity", 0, 0, 0));
            //8 Rune of the Twins
            socketData.Add(new Socket("Rune of the Twins", "Increase velocity", 0, 0, 0));
            //9
            socketData.Add(new Socket("Rune of the Cheetah", "Increase velocity", 0, 0, 0));
            //10
            socketData.Add(new Socket("Rune of the Cheetah", "Increase velocity", 0, 0, 0));
            //11
            socketData.Add(new Socket("Rune of the Cheetah", "Increase velocity", 0, 0, 0));


        }
    }

}
