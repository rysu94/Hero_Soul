using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Arcana_Book_Select : MonoBehaviour
{
    public string cardName, cardType, cardDesc;
    public int index;

	// Use this for initialization
	void Start ()
    {
        GetComponent<Button>().onClick.AddListener(SelectCard);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void SelectCard()
    {
        GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
        if(cardName != "" && LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].pickedCardIndex == -1)
        {
            LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].pickedCardIndex = index;
            InventoryManager.playerSpellbook[LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard[index].cardID] = true;
            LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard[index].cardQuant++;
            GameObject.Find("Arcana_Pedastal").GetComponent<Arcana_Book_Controller>().UpdateCards();
            print(cardName);
        }
      
    }
}
