using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Hero Project CardDatabase Class
By: Ryan Su

Stores all the Arcana Cards

*/

public class CardDatabase : MonoBehaviour
{
    //public Card(string name, int cost, int fire, int earth, int life, int wind, int water)
    


    public static Card arcana_Blank = new Card("No Card", 20, 0, 0, 0, 0, 0, "", "", 0, 0);

    //====================================================================
    //                        Fire Arcana
    //====================================================================

    //Commons, 10 Arcana Each
    public static Card arcana_Embers = new Card("Embers", 1, 10, 0, 0, 0, 0, "Common", "Fire", 1, 0);
    public static Card arcana_FireBolt = new Card("Firebolt", 1, 10, 0, 0, 0, 0, "Common", "Fire", 2, 0);

    //Uncommon, 50 Arcana Each
    public static Card arcana_Blaze = new Card("Blaze", 2, 50, 0, 0, 0, 0, "Uncommon", "Fire", 3, 0);
    public static Card arcana_Magma = new Card("Magma", 3, 100, 40, 10, 0, 0, "Uncommon", "Fire", 4, 0);

    //Rare, 100 Arcana Each
    public static Card arcana_FlameWave = new Card("Flame Wave", 2, 80, 0, 0, 20, 0, "Rare", "Fire", 5, 0);
    public static Card arcana_Enfire = new Card("Enfire", 1, 80, 0, 20, 0, 0, "Rare", "Fire", 6, 0);

    //Epic, 250 Arcana Each
    public static Card arcana_FireSpin = new Card("Fire Spin", 3, 200, 0, 0, 50, 0, "Epic", "Fire", 7, 0);

    //Legend, 500 Arcana Each
    public static Card arcana_Molten = new Card("Molten", 3, 380, 100, 0, 20, 0, "Legend", "Fire", 8, 0);

    //====================================================================
    //                        Earth Arcana
    //====================================================================

    //Commons, 10 Arcana Each
    public static Card arcana_Stone = new Card("Stone", 1, 0, 10, 0, 0, 0, "Common", "Earth", 9, 0);
    public static Card arcana_Boulder = new Card("Boulder", 2, 0, 10, 0, 0, 0, "Common", "Earth", 10, 0);

    //Uncommon, 50 Arcana Each
    public static Card arcana_Earthen = new Card("Earthen", 1, 0, 50, 0, 0, 0, "Uncommon", "Earth", 11, 0);
    public static Card arcana_Impale = new Card("Impale", 2, 0, 40, 0, 0, 10, "Uncommon", "Earth", 12, 0);

    //Rare, 100 Arcana Each
    public static Card arcana_Rupture = new Card("Rupture", 2, 80, 0, 0, 0, 20, "Rare", "Earth", 13, 0);
    public static Card arcana_Wall = new Card("Wall", 1, 80, 0, 0, 0, 20, "Rare", "Earth", 14, 0);

    //Epic, 250 Arcana Each
    public static Card arcana_Enstone = new Card("Enstone", 2, 0, 200, 50, 0, 0, "Epic", "Earth", 15, 0);

    //Legend, 500 Arcana Each
    public static Card arcana_Quake = new Card("Quake", 3, 0, 380, 0, 20, 100, "Legend", "Earth", 16, 0);

    //====================================================================
    //                        Water Arcana
    //====================================================================

    //Commons, 10 Arcana Each
    public static Card arcana_Bubble = new Card("Bubble", 1, 0, 0, 0, 0, 10, "Common", "Water", 25, 0);
    public static Card arcana_Water = new Card("Water", 2, 0, 0, 0, 0, 10, "Common", "Water", 26, 0);

    //Uncommon, 50 Arcana Each
    public static Card arcana_Entomb = new Card("Entomb", 2, 0, 0, 0, 10, 40, "Uncommon", "Water", 27, 0);
    public static Card arcana_Icicle = new Card("Icicle", 1, 0, 0, 0, 10, 40, "Uncommon", "Water", 28, 0);

    //Rare, 100 Arcana Each
    public static Card arcana_Enwater = new Card("Enwater", 1, 0, 0, 0, 0, 100, "Rare", "Water", 29, 0);
    public static Card arcana_Shatter = new Card("Shatter", 2, 0, 0, 0, 30, 70, "Rare", "Water", 30, 0);

    //Epic, 250 Arcana Each
    public static Card arcana_Tidal = new Card("Tidal", 3, 0, 0, 0, 0, 500, "Legend", "Water", 31, 0);

    //Legend, 500 Arcana Each
    public static Card arcana_Spout = new Card("Spout", 3, 0, 0, 0, 0, 250, "Epic", "Water", 32, 0);


    //====================================================================
    //                        Life Arcana
    //====================================================================

    //Commons, 10 Arcana Each
    public static Card arcana_Venom = new Card("Venom", 1, 0, 0, 10, 0, 0, "Common", "Life", 17, 0);
    public static Card arcana_Enliven = new Card("Enliven", 2, 0, 0, 10, 0, 0, "Common", "Life", 18, 0);

    //Uncommon, 50 Arcana Each
    public static Card arcana_Entangle = new Card("Entangle", 2, 0, 10, 40, 0, 0, "Uncommon", "Life", 19, 0);
    public static Card arcana_Mend = new Card("Mend", 1, 0, 0, 50, 0, 0, "Uncommon", "Life", 20, 0);

    //Rare, 100 Arcana Each
    public static Card arcana_Bloom = new Card("Bloom", 1, 0, 30, 70, 0, 0, "Rare", "Life", 21, 0);
    public static Card arcana_Heal = new Card("Heal", 2, 0, 0, 100, 0, 0, "Rare", "Life", 22, 0);

    //Epic, 250 Arcana Each
    public static Card arcana_Guardian = new Card("Guardian", 3, 0, 0, 250, 0, 0, "Epic", "Life", 23, 0);

    //Legend, 500 Arcana Each
    public static Card arcana_Bless = new Card("Bless", 3, 50, 50, 300, 50, 50, "Legend", "Life", 24, 0);

    //====================================================================
    //                        Wind Arcana
    //====================================================================

    //Commons, 10 Arcana Each
    public static Card arcana_Twister = new Card("Twister", 2, 0, 0, 0, 10, 0, "Common", "Wind", 33, 0);
    public static Card arcana_Zap = new Card("Zap", 1, 0, 0, 0, 10, 0, "Common", "Wind", 34, 0);

    //Uncommon, 50 Arcana Each
    public static Card arcana_Ball = new Card("Ball", 2, 10, 0, 0, 40, 0, "Uncommon", "Wind", 35, 0);
    public static Card arcana_Razor = new Card("Razor", 2, 0, 0, 0, 50, 0, "Uncommon", "Wind", 36, 0);

    //Rare, 100 Arcana Each
    public static Card arcana_Gust = new Card("Gust", 1, 0, 0, 0, 100, 0, "Rare", "Wind", 37, 0);
    public static Card arcana_Aero = new Card("Aero", 1, 0, 0, 0, 100, 0, "Rare", "Wind", 38, 0);

    //Epic, 250 Arcana Each
    public static Card arcana_Storm = new Card("Storm", 3, 0, 0, 0, 250, 0, "Epic", "Wind", 39, 0);

    //Legend, 500 Arcana Each
    public static Card arcana_Tempest = new Card("Tempest", 3, 100, 0, 0, 400, 0, "Legend", "Wind", 40, 0);

    public List<Card> cardData = new List<Card>();

    
    void Awake()
    {
        //0
        cardData.Add(arcana_Blank);
        //1
        cardData.Add(arcana_Embers);
        //2
        cardData.Add(arcana_FireBolt);
        //3
        cardData.Add(arcana_Blaze);
        //4
        cardData.Add(arcana_Magma);
        //5
        cardData.Add(arcana_FlameWave);
        //6
        cardData.Add(arcana_Enfire);
        //7
        cardData.Add(arcana_FireSpin);
        //8
        cardData.Add(arcana_Molten);
        //9
        cardData.Add(arcana_Stone);
        //10
        cardData.Add(arcana_Boulder);
        //11
        cardData.Add(arcana_Earthen);
        //12
        cardData.Add(arcana_Impale);
        //13
        cardData.Add(arcana_Rupture);
        //14
        cardData.Add(arcana_Wall);
        //15
        cardData.Add(arcana_Enstone);
        //16
        cardData.Add(arcana_Quake);
        //17
        cardData.Add(arcana_Venom);
        //18
        cardData.Add(arcana_Enliven);
        //19
        cardData.Add(arcana_Entangle);
        //20
        cardData.Add(arcana_Mend);
        //21
        cardData.Add(arcana_Bloom);
        //22
        cardData.Add(arcana_Heal);
        //23
        cardData.Add(arcana_Guardian);
        //24
        cardData.Add(arcana_Bless);
        //25
        cardData.Add(arcana_Bubble);
        //26
        cardData.Add(arcana_Water);
        //27
        cardData.Add(arcana_Entomb);
        //28
        cardData.Add(arcana_Icicle);
        //29
        cardData.Add(arcana_Enwater);
        //30
        cardData.Add(arcana_Shatter);
        //31
        cardData.Add(arcana_Tidal);
        //32
        cardData.Add(arcana_Spout);
        //33
        cardData.Add(arcana_Twister);
        //34
        cardData.Add(arcana_Zap);
        //35
        cardData.Add(arcana_Ball);
        //36
        cardData.Add(arcana_Razor);
        //37
        cardData.Add(arcana_Gust);
        //38
        cardData.Add(arcana_Aero);
        //39
        cardData.Add(arcana_Storm);
        //40
        cardData.Add(arcana_Tempest);
    }
    


    public Card FindCard(int id)
    {
        Card findCard = arcana_Blank;

        for(int i = 0; i < cardData.Count; i++)
        {
            if(cardData[i].cardID == id)
            {
                findCard = cardData[i];
            }
        }

        return findCard;

    }



    //Life Arcana

    //public static Card arcana_Mend = new Card("Mend", 1, 0, 0, 10, 0, 0, );

    //Earth Arcana

    //public static Card arcana_Stone = new Card("Stone", 1, 0, 10, 0, 0, 0);






























    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
