using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/*
Hero Project Deck Class
By: Ryan Su




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
    public static int deckSize = 15;

    //Stores the player's arcana deck
    public static Card[] playerDeck = new Card[35];
    

    //Stores which card have been used
    public static bool[] usedCards = new bool[35];

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
            for(int i = 0; i < playerDeck.Length; i++)
            {
                playerDeck[i] = (CardDatabase.arcana_Blank);
            }

            
            int tempInt = Random.Range(1, GameObject.Find("CardData").GetComponent<CardDatabase>().cardData.Count);
            for (int i = 0; i < 5; i++)
            {
                //tempInt = Random.Range(1, GameObject.Find("CardData").GetComponent<CardDatabase>().cardData.Count);
                //playerDeck[i] = GameObject.Find("CardData").GetComponent<CardDatabase>().cardData[tempInt];
                //playerDeck[i] = CardDatabase.arcana_FireBolt;
                //playerDeck[i] = (CardDatabase.arcana_Blank);
            }
            //tempInt = Random.Range(1, GameObject.Find("CardData").GetComponent<CardDatabase>().cardData.Count);
            for (int i = 5; i < 10; i++)
            {
                //tempInt = Random.Range(1, GameObject.Find("CardData").GetComponent<CardDatabase>().cardData.Count);
                //playerDeck[i] = GameObject.Find("CardData").GetComponent<CardDatabase>().cardData[tempInt];
                //playerDeck[i] = CardDatabase.arcana_Embers;
                //playerDeck[i] = (CardDatabase.arcana_Blank);
            }
            //tempInt = Random.Range(1, GameObject.Find("CardData").GetComponent<CardDatabase>().cardData.Count);
            for (int i = 10; i < 15; i++)
            {
                //tempInt = Random.Range(1, GameObject.Find("CardData").GetComponent<CardDatabase>().cardData.Count);
                //playerDeck[i] = GameObject.Find("CardData").GetComponent<CardDatabase>().cardData[tempInt];
                //playerDeck[i] = CardDatabase.arcana_Blaze;
                //playerDeck[i] = (CardDatabase.arcana_Blank);
            }
            
            //initialize the used cards array
            for (int i = 0; i < 35; i++)
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
                tempInt = Random.Range(0, deckSize);
                if(!pickedCardIndex.Contains(tempInt))
                {
                    //print(tempInt);
                    isValid = true;
                }
                pickedCardIndex.Add(tempInt);
                activeCards[i] = playerDeck[tempInt];
            }
            
        }
        //cardsLeft = deckSize-3;
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
        cardsLeft = deckSize;
        StartDraw();

        GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
        tempObj.GetComponent<Text>().text = "<color=#00ff00ff>Arcana Refreshed.</color>";
    }

    //replaces 1 card use when a card is used
    public static void ReplaceCard(int x)
    {
        //Create a list of valid cards
        List<int> tempList = new List<int>();
        //string tempString = "";
        for(int i = 0; i < deckSize; i++)
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
            //print("Added: " + playerDeck[tempList[tempInt]].cardName);
            pickedCardIndex.RemoveAt(x - 1);
            pickedCardIndex.Add(tempList[tempInt]);
        }
        else if(tempList.Count <= 0)
        {
            activeCards.Add(CardDatabase.arcana_Blank);
        }

        
    }

    //Replaces all 3 cards when shuffle ability is used
    public static void Shuffle(int x)
    {
        //Create a list of valid cards
        List<int> tempList = new List<int>();
        //string tempString = "";
        for (int i = 0; i < deckSize; i++)
        {
            if (usedCards[i] == false && i != pickedCardIndex[0] && i != pickedCardIndex[1] && i != pickedCardIndex[2])
            {
                tempList.Add(i);
                //tempString += i + " ";
            }
        }
        //print(tempString);
        if(tempList.Count > 0)
        {
            int tempInt = Random.Range(0, tempList.Count);
            pickedCardIndex[x - 1] = tempList[tempInt];
            activeCards[x - 1] = playerDeck[tempList[tempInt]];

            GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
            tempObj.GetComponent<Text>().text = "Redrew <color=yellow>" + activeCards[x - 1].cardName + "</color>";
        }
        else
        {
            GameObject.Find("ErrorNoise").GetComponent<AudioSource>().Play();
            GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
            tempObj.GetComponent<Text>().text = "<color=red>No Cards to Redraw.</color>";
        }



    }

    //Removes a card from the Active List
    public static void UseCard(int x)
    {
        
        usedCards[pickedCardIndex[x - 1]] = true;
        activeCards.RemoveAt(x - 1);
        ReplaceCard(x);
        if(cardsLeft > 0)
        {
            cardsLeft--;
        }
            
        //print(pickedCardIndex[0] + " " + pickedCardIndex[1] + " " + pickedCardIndex[2]);

    }
}
