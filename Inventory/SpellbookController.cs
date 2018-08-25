using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SpellbookController : MonoBehaviour
{
    public GameObject[] cardSlots = new GameObject[8];

    public static int spellbookPage = 1;

    public Animator backAnimator;
    public AudioSource buttonNoise;

    public GameObject arcana;

    public Text elementText;

    //Controller Support
    public int cardX, cardY;
    public static int selectIndex;
    public bool padActive = false;
    public GameObject selectCard;

    // Use this for initialization
    void Start ()
    {
        Image tempSprite = GetComponent<Image>();
        tempSprite.sprite = Resources.Load<Sprite>("Inventory/Spell_1");
        spellbookPage = 1;
        elementText.text = "<color=red>Fire Arcana</color>";
        GetPage();
        cardX = 0;
        cardY = 0;
        padActive = false;
    }
	
	// Update is called once per frame
    void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //raycast to see if its above an item slot
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPoint.z = Camera.main.transform.position.z;
            Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider != null && hit.collider.gameObject.tag == "Spell_Back")
            {
                buttonNoise.Play();
                backAnimator.Play("Spellbook_BackPress");
                this.gameObject.SetActive(false);
                arcana.SetActive(true);
                Sprite tempSprite = Resources.Load<Sprite>("Inventory/Inv_3");
                InventoryController.invImage.sprite = tempSprite;
                InventoryController.inSpellbook = false;
            }

            else if (hit.collider != null && hit.collider.gameObject.tag == "SpellTab_Fire")
            {
                arcana.SetActive(false);
                buttonNoise.Play();
                Image tempSprite = GetComponent<Image>();
                tempSprite.sprite = Resources.Load<Sprite>("Inventory/Spell_1");
                spellbookPage = 1;
                elementText.text = "<color=red>Fire Arcana</color>";
                GetPage();
            }

            else if (hit.collider != null && hit.collider.gameObject.tag == "SpellTab_Earth")
            {
                arcana.SetActive(false);
                buttonNoise.Play();
                Image tempSprite = GetComponent<Image>();
                tempSprite.sprite = Resources.Load<Sprite>("Inventory/Spell_2");
                spellbookPage = 2;
                elementText.text = "<color=yellow>Earth Arcana</color>";
                GetPage();
            }

            else if (hit.collider != null && hit.collider.gameObject.tag == "SpellTab_Life")
            {
                arcana.SetActive(false);
                buttonNoise.Play();
                Image tempSprite = GetComponent<Image>();
                tempSprite.sprite = Resources.Load<Sprite>("Inventory/Spell_3");
                spellbookPage = 3;
                elementText.text = "<color=lime>Life Arcana</color>";
                GetPage();
            }

            else if (hit.collider != null && hit.collider.gameObject.tag == "SpellTab_Water")
            {
                arcana.SetActive(false);
                buttonNoise.Play();
                Image tempSprite = GetComponent<Image>();
                tempSprite.sprite = Resources.Load<Sprite>("Inventory/Spell_4");
                elementText.text = "<color=lightblue>Water Arcana</color>";
                spellbookPage = 4;
                GetPage();
            }

            else if (hit.collider != null && hit.collider.gameObject.tag == "SpellTab_Wind")
            {
                arcana.SetActive(false);
                buttonNoise.Play();
                Image tempSprite = GetComponent<Image>();
                tempSprite.sprite = Resources.Load<Sprite>("Inventory/Spell_5");
                elementText.text = "Wind Arcana";
                spellbookPage = 5;
                GetPage();
            }
        }

        if(GameController.xbox360Enabled())
        {
            TogglePages();
            SelectCard();
        }

    }

    void GetPage()
    {
        if (spellbookPage == 1)
        {
            Image tempSprite = GetComponent<Image>();
            tempSprite.sprite = Resources.Load<Sprite>("Inventory/Spell_1");
            elementText.text = "<color=red>Fire Arcana</color>";

            for (int i = 0; i < cardSlots.Length; i++)
            {
                Image tempIMG = cardSlots[i].GetComponent<Image>();
                if (InventoryManager.playerSpellbook[i + 1])
                {
                    tempIMG.sprite = Resources.Load<Sprite>("Cards/Arcana_" + GameObject.Find("CardData").GetComponent<CardDatabase>().FindCard(i + 1).cardName);
                    cardSlots[i].GetComponent<Spellbook_Slot>().cardName = GameObject.Find("CardData").GetComponent<CardDatabase>().FindCard(i + 1).cardName;
                    GameObject.Find("Special_Room_Data").GetComponent<CardTooltipDatabase>().FindCardInfo(GameObject.Find("CardData").GetComponent<CardDatabase>().FindCard(i + 1).cardName, 
                        ref cardSlots[i].GetComponent<Spellbook_Slot>().cardType, ref cardSlots[i].GetComponent<Spellbook_Slot>().cardDesc);
                }
                else if (!InventoryManager.playerSpellbook[i + 1])
                {
                    tempIMG.sprite = Resources.Load<Sprite>("Cards/Arcana_Back");
                    cardSlots[i].GetComponent<Spellbook_Slot>().cardName = "";
                    cardSlots[i].GetComponent<Spellbook_Slot>().cardType = "";
                    cardSlots[i].GetComponent<Spellbook_Slot>().cardDesc = "";
                }
            }
        }
        else if(spellbookPage == 2)
        {
            Image tempSprite = GetComponent<Image>();
            tempSprite.sprite = Resources.Load<Sprite>("Inventory/Spell_2");
            elementText.text = "<color=yellow>Earth Arcana</color>";

            for (int i = 0; i < cardSlots.Length; i++)
            {
                Image tempIMG = cardSlots[i].GetComponent<Image>();
                if (InventoryManager.playerSpellbook[i + 9])
                {
                    tempIMG.sprite = Resources.Load<Sprite>("Cards/Arcana_" + GameObject.Find("CardData").GetComponent<CardDatabase>().FindCard(i + 9).cardName);
                    cardSlots[i].GetComponent<Spellbook_Slot>().cardName = GameObject.Find("CardData").GetComponent<CardDatabase>().FindCard(i + 9).cardName;
                    GameObject.Find("Special_Room_Data").GetComponent<CardTooltipDatabase>().FindCardInfo(GameObject.Find("CardData").GetComponent<CardDatabase>().FindCard(i + 9).cardName,
                        ref cardSlots[i].GetComponent<Spellbook_Slot>().cardType, ref cardSlots[i].GetComponent<Spellbook_Slot>().cardDesc);
                }
                else if (!InventoryManager.playerSpellbook[i + 9])
                {
                    tempIMG.sprite = Resources.Load<Sprite>("Cards/Arcana_Back");
                    cardSlots[i].GetComponent<Spellbook_Slot>().cardName = "";
                    cardSlots[i].GetComponent<Spellbook_Slot>().cardType = "";
                    cardSlots[i].GetComponent<Spellbook_Slot>().cardDesc = "";
                }
            }
        }
        else if (spellbookPage == 3)
        {
            Image tempSprite = GetComponent<Image>();
            tempSprite.sprite = Resources.Load<Sprite>("Inventory/Spell_3");
            elementText.text = "<color=lime>Life Arcana</color>";

            for (int i = 0; i < cardSlots.Length; i++)
            {
                Image tempIMG = cardSlots[i].GetComponent<Image>();
                if (InventoryManager.playerSpellbook[i + 17])
                {
                    tempIMG.sprite = Resources.Load<Sprite>("Cards/Arcana_" + GameObject.Find("CardData").GetComponent<CardDatabase>().FindCard(i + 17).cardName);
                    cardSlots[i].GetComponent<Spellbook_Slot>().cardName = GameObject.Find("CardData").GetComponent<CardDatabase>().FindCard(i + 17).cardName;
                    GameObject.Find("Special_Room_Data").GetComponent<CardTooltipDatabase>().FindCardInfo(GameObject.Find("CardData").GetComponent<CardDatabase>().FindCard(i + 17).cardName,
                        ref cardSlots[i].GetComponent<Spellbook_Slot>().cardType, ref cardSlots[i].GetComponent<Spellbook_Slot>().cardDesc);
                }
                else if (!InventoryManager.playerSpellbook[i + 17])
                {
                    tempIMG.sprite = Resources.Load<Sprite>("Cards/Arcana_Back");
                    cardSlots[i].GetComponent<Spellbook_Slot>().cardName = "";
                    cardSlots[i].GetComponent<Spellbook_Slot>().cardType = "";
                    cardSlots[i].GetComponent<Spellbook_Slot>().cardDesc = "";
                }
            }
        }
        else if (spellbookPage == 4)
        {
            Image tempSprite = GetComponent<Image>();
            tempSprite.sprite = Resources.Load<Sprite>("Inventory/Spell_4");
            elementText.text = "<color=lightblue>Water Arcana</color>";

            for (int i = 0; i < cardSlots.Length; i++)
            {
                Image tempIMG = cardSlots[i].GetComponent<Image>();
                if (InventoryManager.playerSpellbook[i + 25])
                {
                    tempIMG.sprite = Resources.Load<Sprite>("Cards/Arcana_" + GameObject.Find("CardData").GetComponent<CardDatabase>().FindCard(i + 25).cardName);
                    cardSlots[i].GetComponent<Spellbook_Slot>().cardName = GameObject.Find("CardData").GetComponent<CardDatabase>().FindCard(i + 25).cardName;
                    GameObject.Find("Special_Room_Data").GetComponent<CardTooltipDatabase>().FindCardInfo(GameObject.Find("CardData").GetComponent<CardDatabase>().FindCard(i + 25).cardName,
                        ref cardSlots[i].GetComponent<Spellbook_Slot>().cardType, ref cardSlots[i].GetComponent<Spellbook_Slot>().cardDesc);
                }
                else if (!InventoryManager.playerSpellbook[i + 25])
                {
                    tempIMG.sprite = Resources.Load<Sprite>("Cards/Arcana_Back");
                    cardSlots[i].GetComponent<Spellbook_Slot>().cardName = "";
                    cardSlots[i].GetComponent<Spellbook_Slot>().cardType = "";
                    cardSlots[i].GetComponent<Spellbook_Slot>().cardDesc = "";
                }
            }
        }
        else if (spellbookPage == 5)
        {
            Image tempSprite = GetComponent<Image>();
            tempSprite.sprite = Resources.Load<Sprite>("Inventory/Spell_5");
            elementText.text = "Wind Arcana";

            for (int i = 0; i < cardSlots.Length; i++)
            {
                Image tempIMG = cardSlots[i].GetComponent<Image>();
                if (InventoryManager.playerSpellbook[i + 33])
                {
                    tempIMG.sprite = Resources.Load<Sprite>("Cards/Arcana_" + GameObject.Find("CardData").GetComponent<CardDatabase>().FindCard(i + 33).cardName);
                    cardSlots[i].GetComponent<Spellbook_Slot>().cardName = GameObject.Find("CardData").GetComponent<CardDatabase>().FindCard(i + 33).cardName;
                    GameObject.Find("Special_Room_Data").GetComponent<CardTooltipDatabase>().FindCardInfo(GameObject.Find("CardData").GetComponent<CardDatabase>().FindCard(i + 33).cardName,
                        ref cardSlots[i].GetComponent<Spellbook_Slot>().cardType, ref cardSlots[i].GetComponent<Spellbook_Slot>().cardDesc);
                }
                else if (!InventoryManager.playerSpellbook[i + 33])
                {
                    tempIMG.sprite = Resources.Load<Sprite>("Cards/Arcana_Back");
                    cardSlots[i].GetComponent<Spellbook_Slot>().cardName = "";
                    cardSlots[i].GetComponent<Spellbook_Slot>().cardType = "";
                    cardSlots[i].GetComponent<Spellbook_Slot>().cardDesc = "";
                }
            }
        }
    }

    //Controller Support

    public void TogglePages()
    {
        if(InputManager.R_Bumper())
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            cardX = 0;
            cardY = 0;
            spellbookPage++;
            if(spellbookPage > 5)
            {
                spellbookPage = 1;
            }
            GetPage();
        }
        else if(InputManager.L_Bumper())
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            cardX = 0;
            cardY = 0;
            spellbookPage--;
            if(spellbookPage < 1)
            {
                spellbookPage = 5;
            }
            GetPage();
        }
    }

    public void SelectCard()
    {
        if(Mathf.Abs(InputManager.MainHorizontal()) > Mathf.Abs(InputManager.MainVertical()) && !padActive)
        {
            if(InputManager.MainHorizontal() > .5f)
            {
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                padActive = true;
                cardX++;
                if(cardX > 3)
                {
                    cardX = 0;
                }
                StartCoroutine(PadBuffer());
            }
            else if(InputManager.MainHorizontal() < -.5f)
            {
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                padActive = true;
                cardX--;
                if (cardX < 0)
                {
                    cardX = 3;
                }
                StartCoroutine(PadBuffer());
            }
        }
        else if(Mathf.Abs(InputManager.MainVertical()) > Mathf.Abs(InputManager.MainHorizontal()) && !padActive)
        {
            if(InputManager.MainVertical() > .5f)
            {
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                padActive = true;
                cardY++;
                if (cardY > 1)
                {
                    cardY = 0;
                }
                StartCoroutine(PadBuffer());
            }
            else if(InputManager.MainVertical() < -.5f)
            {
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                padActive = true;
                cardY--;
                if (cardY < 0)
                {
                    cardY = 1;
                }
                StartCoroutine(PadBuffer());
            }
        }

        Destroy(selectCard);
        selectIndex = (cardY * 4) + cardX;
        selectCard = Instantiate(Resources.Load("Prefabs/Inventory/Spell_Select"), cardSlots[selectIndex].transform) as GameObject;
    }

    IEnumerator PadBuffer()
    {
        yield return new WaitForSeconds(.25f);
        padActive = false;
    }

}
