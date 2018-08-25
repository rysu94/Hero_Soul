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
    public Animator[] arcanaCard = new Animator[3];

    public Image[] cardSprite = new Image[3];
    public Image[] cardGlow = new Image[3];

    public Text cardDiscovered;
    public Text decipherText;

    public Text cardName;
    public Text cardType;
    public Text cardDesc;

    public bool discovering = false;

    public GameObject cardSelect;
    public int selectIndex = 0;
    public GameObject tooltip;

	// Use this for initialization
	void Start ()
    {
		if(LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].isOpened)
        {
            decipherText.text = "Deciphered!";
            arcanaCard[0].Play("Arcana_Card_Reveal_Idle");
            arcanaCard[1].Play("Arcana_Card_Reveal_Idle");
            arcanaCard[2].Play("Arcana_Card_Reveal_Idle");
            cardDiscovered.text = "New Arcana Discovered!";
            UpdateCards();

            for(int i = 0; i < LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard.Length; i++)
            {
                cardSprite[i].sprite = Resources.Load<Sprite>("Cards/Arcana_" + LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard[i].cardName);

                cardSprite[i].gameObject.GetComponent<Arcana_Book_Select>().cardName = LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard[i].cardName;
                string tempType = "";
                string tempDesc = "";
                GameObject.Find("Special_Room_Data").GetComponent<CardTooltipDatabase>().FindCardInfo(LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard[i].cardName, ref tempType, ref tempDesc);
                cardSprite[i].gameObject.GetComponent<Arcana_Book_Select>().cardType = tempType;
                cardSprite[i].gameObject.GetComponent<Arcana_Book_Select>().cardDesc = tempDesc;
                cardSprite[i].gameObject.GetComponent<Arcana_Book_Select>().index = i;

                if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard[i].cardRarity == "Common")
                {
                    cardGlow[i].color = new Color(1f, 1f, 1f);
                }
                else if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard[i].cardRarity == "Uncommon")
                {
                    cardGlow[i].color = new Color(0f, 1f, .4f);
                }
                else if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard[i].cardRarity == "Rare")
                {
                    cardGlow[i].color = new Color(0f, .5f, 1f);
                }
                else if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard[i].cardRarity == "Epic")
                {
                    cardGlow[i].color = new Color(.8f, 0f, 1f);
                }
                else if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard[i].cardRarity == "Legend")
                {
                    cardGlow[i].color = new Color(1f, .6f, 1f);
                }

                //cardName.text = LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard[i].cardName;
                //GameObject.Find("Special_Room_Data").GetComponent<CardTooltipDatabase>().FindCardInfo(LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard[i].cardName, ref tempType, ref tempDesc);
            }
            UpdateCards();
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
                arcanaCard[0].Play("Arcana_Card_Fade");
                arcanaCard[1].Play("Arcana_Card_Fade");
                arcanaCard[2].Play("Arcana_Card_Fade");
                StartCoroutine(CardDiscoverRoutine());
                LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].isOpened = true;
            }

            if(hit.collider != null && hit.collider.tag == "Close_Button" && !discovering)
            {
                gameObject.SetActive(false);
                TestCharController.inDialogue = false;
            }
        }

        //Gampad Controllers
        if(GameController.xbox360Enabled() && TestCharController.inDialogue)
        {
            DecipherCard();
            ChooseCard();
            SelectCard();
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


        for(int i = 0; i < LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard.Length; i++)
        {
            cardSprite[i].sprite = Resources.Load<Sprite>("Cards/Arcana_" + LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard[i].cardName);

            cardSprite[i].gameObject.GetComponent<Arcana_Book_Select>().cardName = LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard[i].cardName;
            string tempType = "";
            string tempDesc = "";
            GameObject.Find("Special_Room_Data").GetComponent<CardTooltipDatabase>().FindCardInfo(LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard[i].cardName, ref tempType, ref tempDesc);
            cardSprite[i].gameObject.GetComponent<Arcana_Book_Select>().cardType = tempType;
            cardSprite[i].gameObject.GetComponent<Arcana_Book_Select>().cardDesc = tempDesc;
            cardSprite[i].gameObject.GetComponent<Arcana_Book_Select>().index = i;

            if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard[i].cardRarity == "Common")
            {
                cardGlow[i].color = new Color(1f, 1f, 1f);
            }
            else if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard[i].cardRarity == "Uncommon")
            {
                cardGlow[i].color = new Color(0f, 1f, .4f);
            }
            else if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard[i].cardRarity == "Rare")
            {
                cardGlow[i].color = new Color(0f, .5f, 1f);
            }
            else if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard[i].cardRarity == "Epic")
            {
                cardGlow[i].color = new Color(.8f, 0f, 1f);
            }
            else if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard[i].cardRarity == "Legend")
            {
                cardGlow[i].color = new Color(1f, .6f, 1f);
            }
        }


        yield return new WaitForSeconds(1f);
        decipherText.text = "Deciphered!";
        cardDiscovered.text = "Choose one:";

        

        arcanaNoise.Play();
        discovering = false;
        InventoryController.inInv = false;
    }


    public void UpdateCards()
    {
        for (int i = 0; i < cardGlow.Length; i++)
        {
            if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].pickedCardIndex == i)
            {
                arcanaCard[i].Play("Arcana_Card_Reveal_Idle");
            }
            else
            {
                cardSprite[i].sprite = Resources.Load<Sprite>("Cards/Arcana_Back");
                cardGlow[i].color = new Color(1f, 1f, 1f);
                cardSprite[i].gameObject.GetComponent<Arcana_Book_Select>().cardName = "";
            }
        }
    }


    //Gamepad Controls

    public void DecipherCard()
    {
        if(InputManager.A_Button() && !LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].isOpened && !cardDiscover)
        {
            buttonNoise.Play();
            decipherButton.Play("DecipherButtonClick");
            arcanaCard[0].Play("Arcana_Card_Fade");
            arcanaCard[1].Play("Arcana_Card_Fade");
            arcanaCard[2].Play("Arcana_Card_Fade");
            StartCoroutine(CardDiscoverRoutine());
            LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].isOpened = true;
        }
    }

    public void ChooseCard()
    {
        if(LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].isOpened)
        {
            Destroy(cardSelect);
            cardSelect = Instantiate(Resources.Load("Prefabs/Inventory/Arcana_Book_Select"), arcanaCard[selectIndex].transform) as GameObject;

            if(arcanaCard[selectIndex].transform.Find("Font_Card_IMG").GetComponent<Arcana_Book_Select>().cardName != "")
            {
                tooltip.gameObject.SetActive(true);
                tooltip.transform.position = arcanaCard[selectIndex].transform.position;
                tooltip.transform.position = new Vector3(tooltip.transform.position.x - 1.55f, tooltip.transform.position.y - .5f, tooltip.transform.position.z);
                //Find item name
                GameObject tempObj = tooltip.transform.Find("Item_Name").gameObject;
                Text tempText = tempObj.GetComponent<Text>();
                tempText.text = arcanaCard[selectIndex].transform.Find("Font_Card_IMG").GetComponent<Arcana_Book_Select>().cardName;

                //Find item type
                tempObj = tooltip.transform.Find("Item_Type").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = arcanaCard[selectIndex].transform.Find("Font_Card_IMG").GetComponent<Arcana_Book_Select>().cardType;

                //Find item desc
                tempObj = tooltip.transform.Find("Item_Desc").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = arcanaCard[selectIndex].transform.Find("Font_Card_IMG").GetComponent<Arcana_Book_Select>().cardDesc;

                //Find item sell
                tempObj = tooltip.transform.Find("Item_Sell1").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = "";
                tempObj = tooltip.transform.Find("Item_Sell2").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = "";
            }
            else
            {
                tooltip.SetActive(false);
            }


            if (InputManager.L_Bumper())
            {
                buttonNoise.Play();

                selectIndex--;
                if(selectIndex < 0)
                {
                    selectIndex = 2;
                }

                
            }
            else if(InputManager.R_Bumper())
            {
                buttonNoise.Play();

                selectIndex++;
                if (selectIndex > 2)
                {
                    selectIndex = 0;
                }

            }
        }
    }

    public void SelectCard()
    {
        if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].isOpened)
        {
            if (InputManager.A_Button())
            {
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                if (arcanaCard[selectIndex].transform.Find("Font_Card_IMG").GetComponent<Arcana_Book_Select>().cardName != "" && 
                    LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].pickedCardIndex == -1)
                {
                    LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].pickedCardIndex = selectIndex;
                    InventoryManager.playerSpellbook[LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard[selectIndex].cardID] = true;
                    LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].fontCard[selectIndex].cardQuant++;
                    UpdateCards();
                }
            }
        }
    }


}
