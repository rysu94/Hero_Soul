using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Arcana_Book_Controller : MonoBehaviour
{
    public bool cardDiscover = false;

    public AudioSource buttonNoise;
    public AudioSource arcanaNoise;

    public Animator decipherButton;
    public Animator arcanaCard;

    public Image cardSprite;
    public Image cardGlow;

    public Text cardDiscovered;
    public Text decipherText;

    public Text cardName;
    public Text cardType;
    public Text cardDesc;

    public bool discovering = false;
	// Use this for initialization
	void Start ()
    {
		if(LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].isOpened)
        {
            decipherText.text = "Deciphered!";
            arcanaCard.Play("Arcana_Card_Reveal_Idle");
            cardDiscovered.text = "New Arcana Discovered!";

            cardSprite.sprite = Resources.Load<Sprite>("Cards/Arcana_" + LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard.cardName);

            if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard.cardRarity == "Common")
            {
                cardGlow.color = new Color(1f, 1f, 1f);
            }
            else if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard.cardRarity == "Uncommon")
            {
                cardGlow.color = new Color(0f, 1f, .4f);
            }
            else if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard.cardRarity == "Rare")
            {
                cardGlow.color = new Color(0f, .5f, 1f);
            }
            else if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard.cardRarity == "Epic")
            {
                cardGlow.color = new Color(.8f, 0f, 1f);
            }
            else if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard.cardRarity == "Legend")
            {
                cardGlow.color = new Color(1f, .6f, 1f);
            }

            cardName.text = LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard.cardName;

            string tempType = "";
            string tempDesc = "";
            GameObject.Find("Special_Room_Data").GetComponent<CardTooltipDatabase>().FindCardInfo(LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard.cardName, ref tempType, ref tempDesc);
            cardType.text = tempType;
            cardDesc.text = tempDesc;

        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetMouseButtonDown(0))
        {
            //raycast to see if its above an item slot
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPoint.z = Camera.main.transform.position.z;
            Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if(hit.collider != null && hit.collider.gameObject.tag == "Inv_Arcana_Book" && !cardDiscover && !LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].isOpened)
            {
                buttonNoise.Play();
                decipherButton.Play("DecipherButtonClick");
                arcanaCard.Play("Arcana_Card_Fade");
                StartCoroutine(CardDiscoverRoutine());
                LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].isOpened = true;
            }

            if(hit.collider != null && hit.collider.tag == "Close_Button" && !discovering)
            {
                gameObject.SetActive(false);
            }
        }
	}

    IEnumerator CardDiscoverRoutine()
    {
        discovering = true;
        InventoryController.inInv = true;
        decipherText.text = "Deciphering.";
        cardDiscover = true;
        yield return new WaitForSeconds(1f);
        decipherText.text = "Deciphering..";
        yield return new WaitForSeconds(1f);
        decipherText.text = "Deciphering...";

        cardSprite.sprite = Resources.Load<Sprite>("Cards/Arcana_" + LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard.cardName);

        if(LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard.cardRarity == "Common")
        {
            cardGlow.color = new Color(1f, 1f, 1f);
        }
        else if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard.cardRarity == "Uncommon")
        {
            cardGlow.color = new Color(0f, 1f, .4f);
        }
        else if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard.cardRarity == "Rare")
        {
            cardGlow.color = new Color(0f, .5f, 1f);
        }
        else if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard.cardRarity == "Epic")
        {
            cardGlow.color = new Color(.8f, 0f, 1f);
        }
        else if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard.cardRarity == "Legend")
        {
            cardGlow.color = new Color(1f, .6f, 1f);
        }

        yield return new WaitForSeconds(1f);
        decipherText.text = "Deciphered!";
        cardDiscovered.text = "New Arcana Discovered!";

        cardName.text = LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard.cardName;

        string tempType = "";
        string tempDesc = "";
        GameObject.Find("Special_Room_Data").GetComponent<CardTooltipDatabase>().FindCardInfo(LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard.cardName, ref tempType, ref tempDesc);
        cardType.text = tempType;
        cardDesc.text = tempDesc;

        InventoryManager.playerSpellbook[LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard.cardID] = true;

        arcanaNoise.Play();
        discovering = false;
        InventoryController.inInv = false;
    }


}
