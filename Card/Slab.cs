using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Slab : MonoBehaviour
{
    public int cardID;
    public int cardQuant;
    public int manaCost;
    public string cardName;
    public string cardRarity;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = Camera.main.transform.position.z;
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if(hit.collider != null && Input.GetMouseButtonDown(0) && Card_Interface.canClick)
        {
            if(hit.collider.tag == "Spell_Back" && hit.collider.gameObject.GetComponent<Slab>().cardID == cardID)
            {
                GameObject.Find("CardUISound").GetComponent<AudioSource>().Play();
                if (cardQuant > 1)
                {
                    cardQuant--;
                    Card_Interface.currentDeckCard--;
                    GetComponent<CardDatabase>().FindCard(cardID).cardQuant++;
                    transform.Find("Card_Quant").GetComponent<Text>().text = "x" + cardQuant.ToString();
                }
                else
                {
                    GetComponent<CardDatabase>().FindCard(cardID).cardQuant++;
                    Card_Interface.currentDeckCard--;
                    cardID = 0;
                }
            }
        }
    }

    public void AddQuant()
    {
        cardQuant++;
    }
}
