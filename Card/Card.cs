using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Hero Project Card Class
By: Ryan Su

Stores the properties of an Arcana Card

*/

public class Card 
{

    //the name of the card
    public string cardName;

    //The mana cost of the card
    public int manaCost;

    //The card element
    public string cardElement;

    public string cardRarity;

    //The Arcana Cost for crafting the card
    public int fireArcanaCost;
    public int earthArcanaCost;
    public int lifeArcanaCost;
    public int windArcanaCost;
    public int waterArcanaCost;
    
    //The image address of the card
    public string cardAddress;

    //Card ID
    public int cardID;

   
    //Manages whether or not the card has been used
    bool isUsed = false;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //Card class constructor
    public Card(string name, int cost, int fire, int earth, int life, int wind, int water, string rarity, string element, int id)
    {
        cardName = name;
        manaCost = cost;

        cardElement = element;
        cardRarity = rarity;

        fireArcanaCost = fire;
        earthArcanaCost = earth;
        lifeArcanaCost = life;
        windArcanaCost = wind;
        waterArcanaCost = water;

        cardID = id;

        cardAddress = GetAddress();
    }

    //Gets the resource address of the image of the card
    string GetAddress()
    {
        string returnName = "";

        switch (cardName)
        {
            default:
                returnName = "Cards/Arcana_None";
                break;
            case "No Card":
                returnName = "Cards/Arcana_None";
                break;

            //Fire arcana
            case "Embers":
                returnName = "Cards/Arcana_Embers";
                break;
            case "Firebolt":
                returnName = "Cards/Arcana_FireBolt";
                break;
            case "Blaze":
                returnName = "Cards/Arcana_Blaze";
                break;
            case "Flame Lash":
                returnName = "Cards/Arcana_Flame Lash";
                break;
            case "Magma":
                returnName = "Cards/Arcana_Magma";
                break;
            case "Fire Spin":
                returnName = "Cards/Arcana_Fire Spin";
                break;
            case "Molten":
                returnName = "Cards/Arcana_Molten";
                break;
            case "Enfire":
                returnName = "Cards/Arcana_Enfire";
                break;

            //Earth Arcana
            case "Stone":
                returnName = "Cards/Arcana_Stone";
                break;
            case "Boulder":
                returnName = "Cards/Arcana_Boulder";
                break;
            case "Earthen":
                returnName = "Cards/Arcana_Earthen";
                break;
            case "Enstone":
                returnName = "Cards/Arcana_Enstone";
                break;
            case "Impale":
                returnName = "Cards/Arcana_Impale";
                break;
            case "Quake":
                returnName = "Cards/Arcana_Quake";
                break;
            case "Rupture":
                returnName = "Cards/Arcana_Rupture";
                break;
            case "Wall":
                returnName = "Cards/Arcana_Wall";
                break;

            //Life Arcana
            case "Bless":
                returnName = "Cards/Arcana_Bless";
                break;
            case "Bloom":
                returnName = "Cards/Arcana_Bloom";
                break;
            case "Enliven":
                returnName = "Cards/Arcana_Enliven";
                break;
            case "Entangle":
                returnName = "Cards/Arcana_Entangle";
                break;
            case "Guardian":
                returnName = "Cards/Arcana_Guardian";
                break;
            case "Heal":
                returnName = "Cards/Arcana_Heal";
                break;
            case "Mend":
                returnName = "Cards/Arcana_Mend";
                break;
            case "Venom":
                returnName = "Cards/Arcana_Venom";
                break;

            //Water Arcana
            case "Bubble":
                returnName = "Cards/Arcana_Bubble";
                break;
            case "Entomb":
                returnName = "Cards/Arcana_Entomb";
                break;
            case "Enwater":
                returnName = "Cards/Arcana_Enwater";
                break;
            case "Icicle":
                returnName = "Cards/Arcana_Icicle";
                break;
            case "Shatter":
                returnName = "Cards/Arcana_Shatter";
                break;
            case "Spout":
                returnName = "Cards/Arcana_Spout";
                break;
            case "Tidal":
                returnName = "Cards/Arcana_Tidal";
                break;
            case "Water":
                returnName = "Cards/Arcana_Water";
                break;

            //Wind Arcana
            case "Aero":
                returnName = "Cards/Arcana_Aero";
                break;
            case "Ball":
                returnName = "Cards/Arcana_Bqall";
                break;
            case "Gust":
                returnName = "Cards/Arcana_Gust";
                break;
            case "Razor":
                returnName = "Cards/Arcana_Razor";
                break;
            case "Storm":
                returnName = "Cards/Arcana_Storm";
                break;
            case "Tempest":
                returnName = "Cards/Arcana_Tempest";
                break;
            case "Twister":
                returnName = "Cards/Arcana_Twister";
                break;
            case "Zap":
                returnName = "Cards/Arcana_Zap";
                break;
        }

        return returnName;
    }


    //Manages whether or not a Card has been used or not
    void UseCard()
    {
        isUsed = true;
    }

    void ResetCard()
    {
        isUsed = false;
    }
}
