using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ArcanaDeck_Manager : MonoBehaviour
{
    //public List<GameObject> cardList = new List<GameObject>();
    public bool init = false;

    public Text fire;
    public Text water;
    public Text earth;
    public Text wind;
    public Text life;

    public GameObject spellbook;
    public AudioSource bookNoise;

    public GameObject cardPanel;
    public static List<GameObject> cardSlabList = new List<GameObject>();

	// Use this for initialization
	void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(!init)
        {
            init = true;
            UpdateCards();
        }
        

        fire.text = InventoryManager.fireArcana.ToString();
        water.text = InventoryManager.waterArcana.ToString();
        earth.text = InventoryManager.earthArcana.ToString();
        wind.text = InventoryManager.windArcana.ToString();
        life.text = InventoryManager.lifeArcana.ToString();

    }

    public void UpdateCards()
    {
        foreach (GameObject card in GameObject.FindGameObjectsWithTag("Inv_Card"))
        {
            Destroy(card);
        }

        cardSlabList.Clear();
        for (int i = 0; i < Deck.deckSize; i++)
        {
            GameObject tempObj = Instantiate(Resources.Load("Prefabs/Card_Slab"), cardPanel.transform) as GameObject;
            cardSlabList.Add(tempObj);
            tempObj.transform.Find("Card_Name").GetComponent<Text>().text = Deck.playerDeck[i].cardName;

            tempObj.GetComponent<CardSlot>().cardName = Deck.playerDeck[i].cardName;
            tempObj.GetComponent<CardSlot>().cardImage = Resources.Load<Sprite>("Cards/Arcana_" + Deck.playerDeck[i].cardName);

            if (Deck.usedCards[i])
            {
                tempObj.transform.Find("Card_Name").GetComponent<Text>().color = Color.red;
            }
            GetComponent<CardTooltipDatabase>().FindCardInfo(Deck.playerDeck[i].cardName , ref tempObj.GetComponent<CardSlot>().cardElement, ref tempObj.GetComponent<CardSlot>().cardDesc);

        }
    }
}
