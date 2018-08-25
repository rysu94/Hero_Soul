using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Hero Project Card Class
By: Ryan Su

Stores the properties of an Arcana Card

*/

[Serializable]
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

    //Card Quantity
    public int cardQuant;

   
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
    public Card(string name, int cost, int fire, int earth, int life, int wind, int water, string rarity, string element, int id, int quant)
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
        cardQuant = quant;

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
                returnName = "Cards/NewSpell/Arcana_None";
                break;

            //Fire arcana
            case "Embers":
                returnName = "Cards/NewSpell/Arcana_Embers";
                break;
            case "Firebolt":
                returnName = "Cards/NewSpell/Arcana_FireBolt";
                break;
            case "Blaze":
                returnName = "Cards/NewSpell/Arcana_Blaze";
                break;
            case "Flame Wave":
                returnName = "Cards/NewSpell/Arcana_Flame Wave";
                break;
            case "Magma":
                returnName = "Cards/NewSpell/Arcana_Magma";
                break;
            case "Fire Spin":
                returnName = "Cards/NewSpell/Arcana_Fire Spin";
                break;
            case "Molten":
                returnName = "Cards/NewSpell/Arcana_Molten";
                break;
            case "Enfire":
                returnName = "Cards/NewSpell/Arcana_Enfire";
                break;

            //Earth Arcana
            case "Stone":
                returnName = "Cards/NewSpell/Arcana_Stone";
                break;
            case "Boulder":
                returnName = "Cards/NewSpell/Arcana_Boulder";
                break;
            case "Earthen":
                returnName = "Cards/NewSpell/Arcana_Earthen";
                break;
            case "Enstone":
                returnName = "Cards/NewSpell/Arcana_Enstone";
                break;
            case "Impale":
                returnName = "Cards/NewSpell/Arcana_Impale";
                break;
            case "Quake":
                returnName = "Cards/NewSpell/Arcana_Quake";
                break;
            case "Rupture":
                returnName = "Cards/NewSpell/Arcana_Rupture";
                break;
            case "Wall":
                returnName = "Cards/NewSpell/Arcana_Wall";
                break;

            //Life Arcana
            case "Bless":
                returnName = "Cards/NewSpell/Arcana_Bless";
                break;
            case "Bloom":
                returnName = "Cards/NewSpell/Arcana_Bloom";
                break;
            case "Enliven":
                returnName = "Cards/NewSpell/Arcana_Enliven";
                break;
            case "Entangle":
                returnName = "Cards/NewSpell/Arcana_Entangle";
                break;
            case "Guardian":
                returnName = "Cards/NewSpell/Arcana_Guardian";
                break;
            case "Heal":
                returnName = "Cards/NewSpell/Arcana_Heal";
                break;
            case "Mend":
                returnName = "Cards/NewSpell/Arcana_Mend";
                break;
            case "Venom":
                returnName = "Cards/NewSpell/Arcana_Venom";
                break;

            //Water Arcana
            case "Bubble":
                returnName = "Cards/NewSpell/Arcana_Bubble";
                break;
            case "Entomb":
                returnName = "Cards/NewSpell/Arcana_Entomb";
                break;
            case "Enwater":
                returnName = "Cards/NewSpell/Arcana_Enwater";
                break;
            case "Icicle":
                returnName = "Cards/NewSpell/Arcana_Icicle";
                break;
            case "Shatter":
                returnName = "Cards/NewSpell/Arcana_Shatter";
                break;
            case "Spout":
                returnName = "Cards/NewSpell/Arcana_Spout";
                break;
            case "Tidal":
                returnName = "Cards/NewSpell/Arcana_Tidal";
                break;
            case "Water":
                returnName = "Cards/NewSpell/Arcana_Water";
                break;

            //Wind Arcana
            case "Aero":
                returnName = "Cards/NewSpell/Arcana_Aero";
                break;
            case "Ball":
                returnName = "Cards/NewSpell/Arcana_Ball";
                break;
            case "Gust":
                returnName = "Cards/NewSpell/Arcana_Gust";
                break;
            case "Razor":
                returnName = "Cards/NewSpell/Arcana_Razor";
                break;
            case "Storm":
                returnName = "Cards/NewSpell/Arcana_Storm";
                break;
            case "Tempest":
                returnName = "Cards/NewSpell/Arcana_Tempest";
                break;
            case "Twister":
                returnName = "Cards/NewSpell/Arcana_Twister";
                break;
            case "Zap":
                returnName = "Cards/NewSpell/Arcana_Zap";
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
