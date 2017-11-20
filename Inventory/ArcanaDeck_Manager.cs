using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ArcanaDeck_Manager : MonoBehaviour
{
    public GameObject[] cardList = new GameObject[15];

    public Text fire;
    public Text water;
    public Text earth;
    public Text wind;
    public Text life;

    public GameObject spellbook;
    public AudioSource bookNoise;


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		for(int i = 0; i < 15; i++)
        {
            string cardName = Deck.playerDeck[i].cardName;
            Text listText = cardList[i].GetComponent<Text>();
            listText.text = cardName;
            if(Deck.usedCards[i])
            {
                listText.color = Color.red;
            }

            cardList[i].GetComponent<CardSlot>().cardName = cardName;
            cardList[i].GetComponent<CardSlot>().cardImage = Resources.Load<Sprite>("Cards/Arcana_" + cardName);
            GetComponent<CardTooltipDatabase>().FindCardInfo(cardName, ref cardList[i].GetComponent<CardSlot>().cardElement, ref cardList[i].GetComponent<CardSlot>().cardDesc);

        }

        fire.text = InventoryManager.fireArcana.ToString();
        water.text = InventoryManager.waterArcana.ToString();
        earth.text = InventoryManager.earthArcana.ToString();
        wind.text = InventoryManager.windArcana.ToString();
        life.text = InventoryManager.lifeArcana.ToString();

        if (Input.GetMouseButtonDown(0))
        {
            //raycast to see if its above an item slot
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPoint.z = Camera.main.transform.position.z;
            Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if(hit.collider != null && hit.collider.gameObject.tag == "SpellBook")
            {
                bookNoise.Play();
                spellbook.SetActive(true);
                this.gameObject.SetActive(false);
                InventoryController.inSpellbook = true;
            }
        }



    }
}
