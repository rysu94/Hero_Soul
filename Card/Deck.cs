using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/*
Hero Project Deck Class
By: Ryan Su

This scripts defines the Room class.


*/

public class Deck : MonoBehaviour
{
    private static Deck myDeck = null;
    public static Deck Instance
    {
        get { return myDeck; }
    }


    public static bool init = false;
    //The 3 cards that have been drawn from the deck

    public static List<Card> activeCards = new List<Card>();


    //Stores the player's arcana deck
    public static Card[] playerDeck = new Card[15];
    

    //Stores which card have been used
    public static bool[] usedCards = new bool[15];

    //Indexes of picked cards
    public static List<int> pickedCardIndex = new List<int>();

    public static int cardsLeft;
    public Text deckText;


    private void Awake()
    {
        myDeck = this;
    }

    // Use this for initialization
    void Start ()
    {
        deckText = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        deckText.text = cardsLeft.ToString();
	}

    public static void InitDeck()
    {
        if (!init)
        {
            //fills player deck with blanks if player deck is empty
            for (int i = 0; i < 5; i++)
            {
                playerDeck[i] = CardDatabase.arcana_FireBolt;
            }
            for (int i = 5; i < 10; i++)
            {
                playerDeck[i] = CardDatabase.arcana_Embers;
            }
            for (int i = 10; i < 15; i++)
            {
                playerDeck[i] = CardDatabase.arcana_Blaze;
            }


            //initialize the used cards array
            for (int i = 0; i < 15; i++)
            {
                usedCards[i] = false;
            }

            if (activeCards.Count <= 0)
            {
                activeCards.Add(CardDatabase.arcana_Blank);
                activeCards.Add(CardDatabase.arcana_Blank);
                activeCards.Add(CardDatabase.arcana_Blank);
            }


            cardsLeft = 15;
            init = true;
        }
    }

    //This is called at the start of a level, adds 3 cards to your active list
    public static void StartDraw()
    {
        InitDeck();

        int tempInt = 0;
        for(int i = 0; i < 3; i++)
        {
            bool isValid = false;
            while(!isValid)
            {
                tempInt = Random.Range(0, 15);
                if(!pickedCardIndex.Contains(tempInt))
                {
                    //print(tempInt);
                    isValid = true;
                }
                pickedCardIndex.Add(tempInt);
                activeCards[i] = playerDeck[tempInt];
            }
        }
        //print(pickedCardIndex[0] + " " + pickedCardIndex[1] + " " + pickedCardIndex[2]);
    }

    //Call this at start of every level, to refresh deck
    public static void ResetDeck()
    {
        //clears the picked card index
        pickedCardIndex.Clear();
        //resets all cards to unused
        for(int i = 0; i < usedCards.Length; i++)
        {
            usedCards[i] = false;
        }
        cardsLeft = 15;
    }

    //replaces 1 card use when a card is used
    public static void ReplaceCard(int x)
    {
        //Create a list of valid cards
        List<int> tempList = new List<int>();
        //string tempString = "";
        for(int i = 0; i < 15; i++)
        {
            if(usedCards[i] == false && i != pickedCardIndex[0] && i != pickedCardIndex[1] && i != pickedCardIndex[2])
            {
                tempList.Add(i);
                //tempString += i + " ";
            }
        }
        //print("tempList: " + tempString);
        if(tempList.Count > 0)
        {
            int tempInt = Random.Range(0, tempList.Count);
            activeCards.Add(playerDeck[tempList[tempInt]]);
            //print("Added: " + tempList[tempInt]);
            pickedCardIndex.RemoveAt(x - 1);
            pickedCardIndex.Add(tempList[tempInt]);
        }
        else if(tempList.Count <= 0)
        {
            activeCards.Add(CardDatabase.arcana_Blank);
        }
        
    }

    //Replaces all 3 cards when shuffle ability is used
    void Shuffle()
    {

    }

    //Removes a card from the Active List
    public static void UseCard(int x)
    {
        
        usedCards[pickedCardIndex[x - 1]] = true;
        activeCards.RemoveAt(x - 1);
        ReplaceCard(x);
        cardsLeft--;
        //print(pickedCardIndex[0] + " " + pickedCardIndex[1] + " " + pickedCardIndex[2]);

    }
}
