using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/*
Hero Project AracanaController Class
By: Ryan Su

This script manages all the interactions with using arcana and decks


*/


public class ArcanaController : MonoBehaviour
{
    private static ArcanaController arcana = null;
    public static ArcanaController Instance
    {
        get { return arcana; }
    }


    public bool init = false;

    public string cardObjectName = "CardSelected_";
    public static int cardObjectNum = 1;

    public int manaSnapshot;


    //UI component that displays the arcana's name
    public Text spellName;
    public Image spellIcon;

    //UI component that shows the arcana's image
    public Image activeCard1;
    public Image activeCard2;
    public Image activeCard3;

    public string cardName1, cardName2, cardName3;


    //UI component that shows which Frame is highlighted
    public GameObject cardSelectFrame1;
    public GameObject cardSelectFrame2;
    public GameObject cardSelectFrame3;

    public AudioSource cardSelectNoise;

    public bool isUsing = false;

    //UI componnent that gets the card object
    public GameObject card_1;
    public GameObject card_2;
    public GameObject card_3;

    public GameObject card_1_back;
    public GameObject card_2_back;
    public GameObject card_3_back;

    public Animator card_1_Animator;
    public Animator card_2_Animator;
    public Animator card_3_Animator;

    public AudioSource card_1_Audio;
    public AudioSource card_2_Audio;
    public AudioSource card_3_Audio;

    public AudioSource playerCastAudio;

    public AudioSource errorNoise;

    public string cardType, cardDesc;

    public GameObject spellCrossController;

    //Is the spell targetable?
    public static bool isTarget = false;
    public static bool isCasting = false;

    //Spell HUD above character
    public GameObject arcanaHelp;

    void awake()
    {
        arcana = this;
    }

	// Use this for initialization
	void Start ()
    {
        cardSelectNoise = GetComponent<AudioSource>();
        cardSelectFrame1 = GameObject.Find("CardSelected_1");
        cardSelectFrame2 = GameObject.Find("CardSelected_2");
        cardSelectFrame3 = GameObject.Find("CardSelected_3");
        card_1 = GameObject.Find("Card_1");
        card_2 = GameObject.Find("Card_2");
        card_3 = GameObject.Find("Card_3");
        card_1_Animator = card_1.GetComponent<Animator>();
        card_2_Animator = card_2.GetComponent<Animator>();
        card_3_Animator = card_3.GetComponent<Animator>();
        card_1_Audio = card_1.GetComponent<AudioSource>();
        card_2_Audio = card_2.GetComponent<AudioSource>();
        card_3_Audio = card_3.GetComponent<AudioSource>();
        spellName = GameObject.Find("SpellName").GetComponent<Text>();
        spellIcon = GameObject.Find("Spell_Icon").GetComponent<Image>();
        activeCard1 = GameObject.Find("Card_1").GetComponent<Image>();
        activeCard2 = GameObject.Find("Card_2").GetComponent<Image>();
        activeCard3 = GameObject.Find("Card_3").GetComponent<Image>();

        errorNoise = GameObject.Find("ErrorNoise").GetComponent<AudioSource>();
        playerCastAudio = GameObject.Find("PlayerCastAudio").GetComponent<AudioSource>();

        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if ((Input.GetAxis("Mouse ScrollWheel") > 0f || (InputManager.L_Bumper() && !InputManager.R_Bumper())) && !isUsing && !InventoryController.inInv && !TestCharController.inDialogue && !TestCharController.inTreasure && TestCharController.arcanaEnabled) //forward
        {
            //print("up");
            card_1_Animator.Play("Card_1_Idle");
            card_2_Animator.Play("Card_2_Idle");
            card_3_Animator.Play("Card_3_Idle");
            cardObjectNum--;
            cardSelectNoise.Play();
            if (cardObjectNum <= 0)
            {
                cardObjectNum = 3;
            }
            SetActiveCard();

            //Create and show the card helper hud above character
            arcanaHelp = TestCharController.player.transform.Find("Spell_Help").gameObject;
            arcanaHelp.SetActive(false);
            arcanaHelp.SetActive(true);
            arcanaHelp.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cards/NewSpell/Arcana_" + Deck.activeCards[cardObjectNum - 1].cardName);
        }
        else if((Input.GetAxis("Mouse ScrollWheel") < 0f || (InputManager.R_Bumper() && !InputManager.L_Bumper())) && !isUsing && !InventoryController.inInv && !TestCharController.inDialogue && !TestCharController.inTreasure && TestCharController.arcanaEnabled) //backwards
        {
            //print("down");
            card_1_Animator.Play("Card_1_Idle");
            card_2_Animator.Play("Card_2_Idle");
            card_3_Animator.Play("Card_3_Idle");
            cardObjectNum++;
            cardSelectNoise.Play();
            if (cardObjectNum > 3)
            {
                cardObjectNum = 1;
            }
            SetActiveCard();

            //Create and show the card helper hud above character
            arcanaHelp = TestCharController.player.transform.Find("Spell_Help").gameObject;
            arcanaHelp.SetActive(false);
            arcanaHelp.SetActive(true);
            arcanaHelp.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cards/NewSpell/Arcana_" + Deck.activeCards[cardObjectNum - 1].cardName);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha1) && !isUsing && !InventoryController.inInv && !TestCharController.inDialogue && TestCharController.arcanaEnabled)
        {
            card_1_Animator.Play("Card_1_Idle");
            card_2_Animator.Play("Card_2_Idle");
            card_3_Animator.Play("Card_3_Idle");
            cardObjectNum = 1;
            cardSelectNoise.Play();
            SetActiveCard();

            //Create and show the card helper hud above character
            arcanaHelp = TestCharController.player.transform.Find("Spell_Help").gameObject;
            arcanaHelp.SetActive(false);
            arcanaHelp.SetActive(true);
            arcanaHelp.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cards/NewSpell/Arcana_" + Deck.activeCards[cardObjectNum - 1].cardName);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && !isUsing && !InventoryController.inInv && !TestCharController.inDialogue && TestCharController.arcanaEnabled)
        {
            card_1_Animator.Play("Card_1_Idle");
            card_2_Animator.Play("Card_2_Idle");
            card_3_Animator.Play("Card_3_Idle");
            cardObjectNum = 2;
            cardSelectNoise.Play();
            SetActiveCard();

            //Create and show the card helper hud above character
            arcanaHelp = TestCharController.player.transform.Find("Spell_Help").gameObject;
            arcanaHelp.SetActive(false);
            arcanaHelp.SetActive(true);
            arcanaHelp.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cards/NewSpell/Arcana_" + Deck.activeCards[cardObjectNum - 1].cardName);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && !isUsing && !InventoryController.inInv && !TestCharController.inDialogue && TestCharController.arcanaEnabled)
        {
            card_1_Animator.Play("Card_1_Idle");
            card_2_Animator.Play("Card_2_Idle");
            card_3_Animator.Play("Card_3_Idle");
            cardObjectNum = 3;
            cardSelectNoise.Play();
            SetActiveCard();

            //Create and show the card helper hud above character
            arcanaHelp = TestCharController.player.transform.Find("Spell_Help").gameObject;
            arcanaHelp.SetActive(false);
            arcanaHelp.SetActive(true);
            arcanaHelp.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cards/NewSpell/Arcana_" + Deck.activeCards[cardObjectNum - 1].cardName);
        }

        else if((Input.GetKeyDown(KeyCode.G) || (InputManager.J_Cast())) && !InventoryController.inInv && !TestCharController.inDialogue && !isUsing && !TestCharController.inTreasure)
        {
            if(PlayerStats.mana > 0  && Deck.cardsLeft > 0)
            {
                StartCoroutine(SwapCards(cardObjectNum));
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            }
            else if (PlayerStats.mana > 0)
            {
                errorNoise.Play();
                GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
                tempObj.GetComponent<Text>().text = "<color=red>No Cards to Redraw.</color>";
            }
            else
            {
                errorNoise.Play();
                GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
                tempObj.GetComponent<Text>().text = "<color=red>Not enough mana.</color>";
            }
            
        }
        GetCards();

        if(!init)
        {
            SetActiveCard();
            init = true;
        }
        

        if((Input.GetKeyDown(KeyCode.Q) || Input.GetMouseButtonDown(2) || InputManager.B_Button()) && !InventoryController.inInv && !TestCharController.inDialogue && TestCharController.arcanaEnabled)
        {
            if(!isUsing)
            {
                manaSnapshot = PlayerStats.mana;
                StartCoroutine(UseCardRoutine());
                UseCard();
            }
            
        }
        if(Deck.activeCards[0] != null)
        {
            UpdateCardTooltip();
        }
        


    }

    void GetCards()
    {
        bool changed = false;
        if(cardName1 != Deck.activeCards[0].cardName)
        {
            cardName1 = Deck.activeCards[0].cardName;
            changed = true;
        }
        if (cardName2 != Deck.activeCards[1].cardName)
        {
            cardName2 = Deck.activeCards[1].cardName;
            changed = true;
        }
        if (cardName3 != Deck.activeCards[2].cardName)
        {
            cardName3 = Deck.activeCards[2].cardName;
            changed = true;
        }

        if (changed)
        {
            foreach (GameObject crystal in GameObject.FindGameObjectsWithTag("Spell_Crystal"))
            {
                Destroy(crystal);
            }

            activeCard1.sprite = Resources.Load<Sprite>(Deck.activeCards[0].cardAddress);
            if (activeCard1.GetComponent<Spell_Database>().FindSpell(Deck.activeCards[0].cardID).spellCost > 3)
            {
                for (int i = 0; i < activeCard1.GetComponent<Spell_Database>().FindSpell(Deck.activeCards[0].cardID).spellCost; i++)
                {
                    Instantiate(Resources.Load("Cards/NewSpell/Cost_Crystal_Full"), activeCard1.transform.Find("Mana_Frame").transform);
                }
            }
            else
            {
                for (int i = 0; i < activeCard1.GetComponent<Spell_Database>().FindSpell(Deck.activeCards[0].cardID).spellCost; i++)
                {
                    Instantiate(Resources.Load("Cards/NewSpell/Cost_Crystal_Full"), activeCard1.transform.Find("Mana_Frame_Small").transform);
                }
            }


            activeCard2.sprite = Resources.Load<Sprite>(Deck.activeCards[1].cardAddress);
            if (activeCard2.GetComponent<Spell_Database>().FindSpell(Deck.activeCards[1].cardID).spellCost > 3)
            {
                for (int i = 0; i < activeCard2.GetComponent<Spell_Database>().FindSpell(Deck.activeCards[1].cardID).spellCost; i++)
                {
                    Instantiate(Resources.Load("Cards/NewSpell/Cost_Crystal_Full"), activeCard2.transform.Find("Mana_Frame").transform);
                }
            }
            else
            {
                for (int i = 0; i < activeCard2.GetComponent<Spell_Database>().FindSpell(Deck.activeCards[1].cardID).spellCost; i++)
                {
                    Instantiate(Resources.Load("Cards/NewSpell/Cost_Crystal_Full"), activeCard2.transform.Find("Mana_Frame_Small").transform);
                }
            }


            activeCard3.sprite = Resources.Load<Sprite>(Deck.activeCards[2].cardAddress);
            if (activeCard3.GetComponent<Spell_Database>().FindSpell(Deck.activeCards[2].cardID).spellCost > 3)
            {
                for (int i = 0; i < activeCard3.GetComponent<Spell_Database>().FindSpell(Deck.activeCards[2].cardID).spellCost; i++)
                {
                    Instantiate(Resources.Load("Cards/NewSpell/Cost_Crystal_Full"), activeCard3.transform.Find("Mana_Frame").transform);
                }
            }
            else
            {
                for (int i = 0; i < activeCard3.GetComponent<Spell_Database>().FindSpell(Deck.activeCards[2].cardID).spellCost; i++)
                {
                    Instantiate(Resources.Load("Cards/NewSpell/Cost_Crystal_Full"), activeCard3.transform.Find("Mana_Frame_Small").transform);
                }
            }
        }
    }


    void SetActiveCard()
    {        
        if (cardObjectNum == 1)
        {
            cardSelectFrame1.gameObject.SetActive(true);
            cardSelectFrame2.gameObject.SetActive(false);
            cardSelectFrame3.gameObject.SetActive(false);
            spellName.text = Deck.activeCards[0].cardName;
            spellIcon.sprite = GetIcon(Deck.activeCards[0].cardID);
        }
        else if (cardObjectNum == 2)
        {
            cardSelectFrame1.gameObject.SetActive(false);
            cardSelectFrame2.gameObject.SetActive(true);
            cardSelectFrame3.gameObject.SetActive(false);
            spellName.text = Deck.activeCards[1].cardName;
            spellIcon.sprite = GetIcon(Deck.activeCards[1].cardID);
        }
        else if (cardObjectNum == 3)
        {
            cardSelectFrame1.gameObject.SetActive(false);
            cardSelectFrame2.gameObject.SetActive(false);
            cardSelectFrame3.gameObject.SetActive(true);
            spellName.text = Deck.activeCards[2].cardName;
            spellIcon.sprite = GetIcon(Deck.activeCards[2].cardID);
        }
    }

    void UseCard()
    {
        if(cardObjectNum == 1 && PlayerStats.mana >= Deck.activeCards[0].manaCost)
        {
            cardSelectFrame1.gameObject.SetActive(false);
            card_1_Animator.Play("Card_1_Use");
            card_1_Audio.Play();
            PlayerStats.mana -= Deck.activeCards[0].manaCost;
        }
        else if(cardObjectNum == 2 && PlayerStats.mana >= Deck.activeCards[1].manaCost)
        {
            cardSelectFrame2.gameObject.SetActive(false);
            card_2_Animator.Play("Card_2_Use");
            card_2_Audio.Play();
            PlayerStats.mana -= Deck.activeCards[1].manaCost;
        }
        else if(cardObjectNum == 3 && PlayerStats.mana >= Deck.activeCards[2].manaCost)
        {
            cardSelectFrame3.gameObject.SetActive(false);
            card_3_Animator.Play("Card_3_Use");
            card_3_Audio.Play();
            PlayerStats.mana -= Deck.activeCards[2].manaCost;
        }
        else if (cardObjectNum == 1 && PlayerStats.mana < Deck.activeCards[0].manaCost)
        {
            card_1_Animator.Play("Card_1_Error");
            errorNoise.Play();
            GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
            tempObj.GetComponent<Text>().text = "<color=red>Not enough mana.</color>";
        }
        else if (cardObjectNum == 2 && PlayerStats.mana < Deck.activeCards[1].manaCost)
        {
            card_2_Animator.Play("Card_2_Error");
            errorNoise.Play();
            GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
            tempObj.GetComponent<Text>().text = "<color=red>Not enough mana.</color>";
        }
        else if (cardObjectNum == 3 && PlayerStats.mana < Deck.activeCards[2].manaCost)
        {
            card_3_Animator.Play("Card_3_Error");
            errorNoise.Play();
            GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
            tempObj.GetComponent<Text>().text = "<color=red>Not enough mana.</color>";
        }
    }
    
    IEnumerator SwapCards(int x)
    {
        isUsing = true;
        PlayerStats.mana--;
        switch (x)
        {
            default:
                break;
            case 1:
                card_1_Animator.Play("Card_1_Flip");
                card_1_back.SetActive(true);
                break;
            case 2:
                card_2_Animator.Play("Card_2_Flip");
                card_2_back.SetActive(true);
                break;
            case 3:
                card_3_Animator.Play("Card_3_Flip");
                card_3_back.SetActive(true);
                break;

        }
        yield return new WaitForSeconds(.1f);
        switch(x)
        {
            default:
                break;
            case 1:
                activeCard1.sprite = Resources.Load<Sprite>("Cards/Arcana_Back");
                break;
            case 2:
                activeCard2.sprite = Resources.Load<Sprite>("Cards/Arcana_Back");
                break;
            case 3:
                activeCard3.sprite = Resources.Load<Sprite>("Cards/Arcana_Back");
                break;

        }
        yield return new WaitForSeconds(.8f);
        switch (x)
        {
            default:
                break;
            case 1:
                card_1_Animator.Play("Card_1_Flip2");
                break;
            case 2:
                card_2_Animator.Play("Card_2_Flip2");
                break;
            case 3:
                card_3_Animator.Play("Card_3_Flip2");
                break;

        }
        Deck.Shuffle(x);
        GetCards();
        SetActiveCard();
        yield return new WaitForSeconds(.1f);
        isUsing = false;
        card_1_Animator.Play("Card_1_Idle");
        card_2_Animator.Play("Card_2_Idle");
        card_3_Animator.Play("Card_3_Idle");
        card_1_back.SetActive(false);
        card_2_back.SetActive(false);
        card_3_back.SetActive(false);
    }

    IEnumerator UseCardRoutine()
    {
        isUsing = true;
        TestCharController.inDialogue = true;
        //Player Cast Animation
        if(TestCharController.player.GetComponent<TestCharController>().north)
        {
            TestCharController.player.GetComponent<Animator>().Play("Cast_Up");
        }
        else if (TestCharController.player.GetComponent<TestCharController>().south)
        {
            TestCharController.player.GetComponent<Animator>().Play("Cast_Down");
        }
        else if (TestCharController.player.GetComponent<TestCharController>().west)
        {
            TestCharController.player.GetComponent<Animator>().Play("Cast_Left");
        }
        else if (TestCharController.player.GetComponent<TestCharController>().east)
        {
            TestCharController.player.GetComponent<Animator>().Play("Cast_Right");
        }

        //Update the active card list
        if (manaSnapshot >= Deck.activeCards[cardObjectNum-1].manaCost)
        {
            //print(Deck.activeCards[cardObjectNum - 1].manaCost);

           
           
            playerCastAudio.clip = TestCharController.castNoise[Random.Range(0,2)];
            playerCastAudio.Play();
            yield return new WaitForSeconds(.5f);
            SpawnCardEffect(cardObjectNum);
            yield return new WaitForSeconds(.5f);
            Deck.UseCard(cardObjectNum);
            

        }
        
        card_1_Animator.Play("Card_1_Idle");
        card_2_Animator.Play("Card_2_Idle");
        card_3_Animator.Play("Card_3_Idle");
        SetActiveCard();
        //yield return new WaitForSeconds(.5f);
        TestCharController.inDialogue = false;
        if (TestCharController.player.GetComponent<TestCharController>().north)
        {
            TestCharController.player.GetComponent<Animator>().Play("Test_Up_Idle");
        }
        else if (TestCharController.player.GetComponent<TestCharController>().south)
        {
            TestCharController.player.GetComponent<Animator>().Play("Test_Down_Idle");
        }
        else if (TestCharController.player.GetComponent<TestCharController>().west)
        {
            TestCharController.player.GetComponent<Animator>().Play("Test_Left_Idle");
        }
        else if (TestCharController.player.GetComponent<TestCharController>().east)
        {
            TestCharController.player.GetComponent<Animator>().Play("Test_Right_Idle");
        }

        isUsing = false;
    }

    void UpdateCardTooltip()
    {
        GetComponent<CardTooltipDatabase>().FindCardInfo(Deck.activeCards[0].cardName, ref cardType, ref cardDesc);
        activeCard1.GetComponent<Spellbook_Slot>().cardName = Deck.activeCards[0].cardName;
        activeCard1.GetComponent<Spellbook_Slot>().cardType = cardType;
        activeCard1.GetComponent<Spellbook_Slot>().cardDesc = cardDesc;

        GetComponent<CardTooltipDatabase>().FindCardInfo(Deck.activeCards[1].cardName, ref cardType, ref cardDesc);
        activeCard2.GetComponent<Spellbook_Slot>().cardName = Deck.activeCards[1].cardName;
        activeCard2.GetComponent<Spellbook_Slot>().cardType = cardType;
        activeCard2.GetComponent<Spellbook_Slot>().cardDesc = cardDesc;

        GetComponent<CardTooltipDatabase>().FindCardInfo(Deck.activeCards[2].cardName, ref cardType, ref cardDesc);
        activeCard3.GetComponent<Spellbook_Slot>().cardName = Deck.activeCards[2].cardName;
        activeCard3.GetComponent<Spellbook_Slot>().cardType = cardType;
        activeCard3.GetComponent<Spellbook_Slot>().cardDesc = cardDesc;
    }


    Sprite GetIcon(int id)
    {
        Sprite returnImg = Resources.Load<Sprite>("Cards/Icon/Buff");

        switch(id)
        {
            default:
                break;
            //Embers
            case 1:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Projectile");
                isTarget = false;
                break;
            //Firebolt
            case 2:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Projectile");
                isTarget = false;
                break;
            //Blaze
            case 3:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Projectile");
                isTarget = false;
                break;
            //Magma
            case 4:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Projectile");
                isTarget = false;
                break;
            //Flame Wave
            case 5:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Projectile");
                isTarget = false;
                break;
            //Enfire
            case 6:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Buff");
                isTarget = false;
                break;
            //Firespin
            case 7:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Projectile");
                isTarget = false;
                break;
            //Molten
            case 8:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Target_Persist");
                isTarget = false;
                break;
            //Stone
            case 9:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Projectile");
                isTarget = false;
                break;
            //Boulder
            case 10:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Projectile");
                isTarget = false;
                break;
            //Earthen
            case 11:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Buff");
                isTarget = false;
                break;
            //Impale
            case 12:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Target_Single");
                isTarget = true;
                break;
            //Rupture
            case 13:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Target_Persist");
                isTarget = true;
                break;
            //Wall
            case 14:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Projectile");
                isTarget = false;
                break;
            //Enstone
            case 15:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Buff");
                isTarget = false;
                break;
            //Quake
            case 16:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Projectile");
                isTarget = false;
                break;
            //Venom
            case 17:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Projectile");
                isTarget = false;
                break;
            //Enliven
            case 18:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Buff");
                isTarget = false;
                break;
            //Entangle
            case 19:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Projectile");
                isTarget = false;
                break;
            //Mend
            case 20:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Buff");
                isTarget = false;
                break;
            //Bloom
            case 21:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Buff");
                isTarget = false;
                break;
            //Heal
            case 22:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Buff");
                isTarget = false;
                break;
            //Guardian
            case 23:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Buff");
                isTarget = false;
                break;
            //Bless
            case 24:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Buff");
                isTarget = false;
                break;
            //Bubble
            case 25:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Projectile");
                isTarget = false;
                break;
            //Water
            case 26:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Projectile");
                isTarget = false;
                break;
            //Entomb
            case 27:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Projectile");
                isTarget = false;
                break;
            //Icicle
            case 28:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Target_Single");
                isTarget = true;
                break;
            //Enwater
            case 29:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Buff");
                isTarget = false;
                break;
            //Shatter
            case 30:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Target_Line");
                isTarget = true;
                break;
            //Tidal
            case 31:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Projectile");
                isTarget = false;
                break;
            //Spout
            case 32:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Target_Single");
                isTarget = true;
                break;
            //Twister
            case 33:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Projectile");
                isTarget = false;
                break;
            //Zap
            case 34:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Target_Single");
                isTarget = true;
                break;
            //Ball
            case 35:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Target_Persist");
                isTarget = true;
                break;
            //Razor
            case 36:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Projectile");
                isTarget = false;
                break;
            //Gust
            case 37:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Projectile");
                isTarget = false;
                break;
            //Aero
            case 38:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Buff");
                isTarget = false;
                break;
            //Storm
            case 39:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Buff");
                isTarget = false;
                break;
            //Tempest
            case 40:
                returnImg = Resources.Load<Sprite>("Cards/Icon/Target_Persist");
                isTarget = true;
                break;

        }


        return returnImg;
    }



    void SpawnCardEffect(int card)
    {
        //Fire Spells
        if (Deck.activeCards[card - 1].cardName == "Firebolt")
        {
            if (TestCharController.player.GetComponent<TestCharController>().north)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/NewSpell/FireBolt"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 90));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().east)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/NewSpell/FireBolt"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.identity);
            }
            else if (TestCharController.player.GetComponent<TestCharController>().west)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/NewSpell/FireBolt"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 180));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().south)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/NewSpell/FireBolt"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 270));
            }
            SpawnFire();

        }
        else if (Deck.activeCards[card - 1].cardName == "Embers")
        {
            if (TestCharController.player.GetComponent<TestCharController>().north)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/NewSpell/Embers_New"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 90f));
                Instantiate(Resources.Load("Prefabs/SpellFX/NewSpell/Embers_New"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 78f));
                Instantiate(Resources.Load("Prefabs/SpellFX/NewSpell/Embers_New"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 102f));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().east)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/NewSpell/Embers_New"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.identity);
                Instantiate(Resources.Load("Prefabs/SpellFX/NewSpell/Embers_New"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 12f));
                Instantiate(Resources.Load("Prefabs/SpellFX/NewSpell/Embers_New"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, -12f));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().west)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/NewSpell/Embers_New"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 180f));
                Instantiate(Resources.Load("Prefabs/SpellFX/NewSpell/Embers_New"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 168));
                Instantiate(Resources.Load("Prefabs/SpellFX/NewSpell/Embers_New"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 192f));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().south)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/NewSpell/Embers_New"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 270f));
                Instantiate(Resources.Load("Prefabs/SpellFX/NewSpell/Embers_New"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 258f));
                Instantiate(Resources.Load("Prefabs/SpellFX/NewSpell/Embers_New"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 282f));
            }


            SpawnFire();
        }
        else if (Deck.activeCards[card - 1].cardName == "Blaze")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/NewSpell/Blaze_New"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.identity);

            SpawnFire();
        }
        else if (Deck.activeCards[card - 1].cardName == "Enfire")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Enfire"), TestCharController.player.transform);
        }

        else if (Deck.activeCards[card - 1].cardName == "Fire Spin")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/FireSpin"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.identity);

            SpawnFire();
        }

        else if (Deck.activeCards[card - 1].cardName == "Magma")
        {
            if (TestCharController.player.GetComponent<TestCharController>().north)
            {
                GameObject tempObj = Instantiate(Resources.Load("Prefabs/SpellFX/Magma"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 90)) as GameObject;
                tempObj.GetComponentInChildren<Magma>().vel = new Vector2 (0,3);
            }
            else if (TestCharController.player.GetComponent<TestCharController>().east)
            {
                GameObject tempObj = Instantiate(Resources.Load("Prefabs/SpellFX/Magma"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 0)) as GameObject;
                tempObj.GetComponentInChildren<Magma>().vel = new Vector2(3, 0);
            }
            else if (TestCharController.player.GetComponent<TestCharController>().west)
            {
                GameObject tempObj = Instantiate(Resources.Load("Prefabs/SpellFX/Magma"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 180)) as GameObject;
                tempObj.GetComponentInChildren<Magma>().vel = new Vector2(-3, 0);
            }
            else if (TestCharController.player.GetComponent<TestCharController>().south)
            {
                GameObject tempObj = Instantiate(Resources.Load("Prefabs/SpellFX/Magma"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 270)) as GameObject;
                tempObj.GetComponentInChildren<Magma>().vel = new Vector2(0, -3);
            }

            SpawnFire();
        }

        else if (Deck.activeCards[card - 1].cardName == "Flame Wave")
        {
            if (TestCharController.player.GetComponent<TestCharController>().north)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/NewSpell/FlameLash_New_Core"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 90));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().east)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/NewSpell/FlameLash_New_Core"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 0));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().west)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/NewSpell/FlameLash_New_Core"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 180));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().south)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/NewSpell/FlameLash_New_Core"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 270));
            }

            SpawnFire();
        }

        else if (Deck.activeCards[card - 1].cardName == "Molten")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Molten"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.identity);

        }

        //Earth Spells
        else if (Deck.activeCards[card - 1].cardName == "Stone")
        {
            if (TestCharController.player.GetComponent<TestCharController>().north)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Stone"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 180));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().east)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Stone"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 90));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().west)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Stone"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 270));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().south)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Stone"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 0));
            }

            SpawnEarth();
        }

        else if (Deck.activeCards[card - 1].cardName == "Boulder")
        {
            if (TestCharController.player.GetComponent<TestCharController>().north)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Boulder"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 180));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().east)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Boulder"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 90));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().west)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Boulder"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 270));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().south)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Boulder"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 0));
            }

            SpawnEarth();
        }

        else if (Deck.activeCards[card - 1].cardName == "Enstone")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Enstone"), TestCharController.player.transform);
        }

        else if (Deck.activeCards[card - 1].cardName == "Earthen")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Earthen"), TestCharController.player.transform);
        }

        else if (Deck.activeCards[card - 1].cardName == "Wall")
        {
            if (TestCharController.player.GetComponent<TestCharController>().north)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Wall_N"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.identity);
            }
            else if (TestCharController.player.GetComponent<TestCharController>().east)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Wall_E"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.identity);
            }
            else if (TestCharController.player.GetComponent<TestCharController>().west)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Wall_W"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.identity);
            }
            else if (TestCharController.player.GetComponent<TestCharController>().south)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Wall_S"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.identity);
            }

            SpawnEarth();
        }

        else if (Deck.activeCards[card - 1].cardName == "Impale")
        {
            if(GameController.xbox360Enabled())
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Impale_Cast"), Move_Cross.crossPos, Quaternion.identity);
            }
            else
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Instantiate(Resources.Load("Prefabs/SpellFX/Impale_Cast"), mousePos, Quaternion.identity);
            }

            SpawnEarth();
        }

        else if (Deck.activeCards[card - 1].cardName == "Quake")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Quake"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.identity);

            SpawnEarth();
        }

        else if (Deck.activeCards[card - 1].cardName == "Rupture")
        {
            GameObject tempObj = Instantiate(Resources.Load("Prefabs/SpellFX/Rupture"), TestCharController.player.transform) as GameObject;
            tempObj.GetComponent<Rupture>().parent = Instantiate(Resources.Load("Prefabs/SpellFX/Rupture_Parent"), TestCharController.player.transform) as GameObject;

        }



        //Life Spells
        else if (Deck.activeCards[card - 1].cardName == "Mend")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Mend"), TestCharController.player.transform);
        }

        else if (Deck.activeCards[card - 1].cardName == "Heal")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Heal"), TestCharController.player.transform);
        }

        else if (Deck.activeCards[card - 1].cardName == "Enliven")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Enliven"), TestCharController.player.transform);
        }

        else if (Deck.activeCards[card - 1].cardName == "Guardian")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Guardian"), TestCharController.player.transform);
        }

        else if (Deck.activeCards[card - 1].cardName == "Bless")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Bless_Cast"), TestCharController.player.transform);
        }

        else if (Deck.activeCards[card - 1].cardName == "Venom")
        {
            for(int i = 0; i < 360; i += 30)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Venom_Cast"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, i));
            }

        }

        else if (Deck.activeCards[card - 1].cardName == "Entangle")
        {
            if (TestCharController.player.GetComponent<TestCharController>().north)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Entangle_Cast"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 180));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().east)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Entangle_Cast"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 90));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().west)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Entangle_Cast"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 270));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().south)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Entangle_Cast"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 0));
            }
        }

        else if (Deck.activeCards[card - 1].cardName == "Bloom")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Bloom_Cast"), TestCharController.player.transform);
        }


        //Wind Spells
        else if (Deck.activeCards[card - 1].cardName == "Zap")
        {
            if(GameController.xbox360Enabled())
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Zap"), Move_Cross.crossPos, Quaternion.identity);
            }
            else
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Instantiate(Resources.Load("Prefabs/SpellFX/Zap"), mousePos, Quaternion.identity);
            }

            SpawnElec();
        }

        else if (Deck.activeCards[card - 1].cardName == "Ball")
        {
            if (TestCharController.player.GetComponent<TestCharController>().north)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Ball_Cast"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 180));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().east)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Ball_Cast"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 90));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().west)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Ball_Cast"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 270));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().south)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Ball_Cast"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 0));
            }

            SpawnElec();
        }

        else if (Deck.activeCards[card - 1].cardName == "Aero")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Aero"), TestCharController.player.transform);
        }

        else if (Deck.activeCards[card - 1].cardName == "Twister")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Twister"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.identity);

            SpawnWind();
        }

        else if (Deck.activeCards[card - 1].cardName == "Gust")
        {
            if (TestCharController.player.GetComponent<TestCharController>().north)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Gust"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 180));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().east)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Gust"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 90));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().west)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Gust"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 270));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().south)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Gust"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 0));
            }

            SpawnWind();

        }

        else if (Deck.activeCards[card - 1].cardName == "Razor")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Razor"), TestCharController.player.transform);

            SpawnWind();
        }

        else if (Deck.activeCards[card - 1].cardName == "Storm")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Storm"), TestCharController.player.transform);
            SpawnWind();
        }

        else if (Deck.activeCards[card - 1].cardName == "Tempest")
        {
            if(GameController.xbox360Enabled())
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Tempest"), Move_Cross.crossPos, Quaternion.identity);
            }
            else
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Instantiate(Resources.Load("Prefabs/SpellFX/Tempest"), mousePos, Quaternion.identity);
            }
            SpawnElec();
        }

        //Water Spells
        else if (Deck.activeCards[card - 1].cardName == "Bubble")
        {
            if (TestCharController.player.GetComponent<TestCharController>().north)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Bubble_Spell"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 180));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().east)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Bubble_Spell"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 90));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().west)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Bubble_Spell"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 270));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().south)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Bubble_Spell"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 0));
            }

            SpawnWater();
        }

        else if (Deck.activeCards[card - 1].cardName == "Enwater")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Enwater"), TestCharController.player.transform);
        }

        else if (Deck.activeCards[card - 1].cardName == "Icicle")
        {
            if(GameController.xbox360Enabled())
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Icicle"), Move_Cross.crossPos, Quaternion.identity);
            }
            else
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Instantiate(Resources.Load("Prefabs/SpellFX/Icicle"), mousePos, Quaternion.identity);
            }

            SpawnWater();
        }

        else if (Deck.activeCards[card - 1].cardName == "Water")
        {
            if (TestCharController.player.GetComponent<TestCharController>().north)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Water"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 180));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().east)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Water"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 90));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().west)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Water"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 270));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().south)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Water"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 0));
            }

            SpawnWater();
        }

        else if (Deck.activeCards[card - 1].cardName == "Entomb")
        {
            if (TestCharController.player.GetComponent<TestCharController>().north)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Entomb"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 180));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().east)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Entomb"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 90));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().west)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Entomb"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 270));
            }
            else if (TestCharController.player.GetComponent<TestCharController>().south)
            {
                Instantiate(Resources.Load("Prefabs/SpellFX/Entomb"), new Vector2(TestCharController.player.transform.position.x,
                    TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 0));
            }

            SpawnWater();
        }

        else if (Deck.activeCards[card - 1].cardName == "Shatter")
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(Resources.Load("Prefabs/SpellFX/ShatterLine"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.identity);
            SpawnWater();
        }

        else if (Deck.activeCards[card - 1].cardName == "Spout")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Spout_Water"), Move_Cross.crossPos, Quaternion.identity);
            SpawnWater();
        }
        else if (Deck.activeCards[card - 1].cardName == "Tidal")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Tidal"), TestCharController.player.transform);
        }

        else
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/FireBall"), new Vector2(TestCharController.player.transform.position.x,
            TestCharController.player.transform.position.y), Quaternion.identity);

        }
    }

    //Cast Animation - Fire
    void SpawnFire()
    {
        if(TestCharController.player.GetComponent<TestCharController>().north)
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Cast_Fire"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 90));
        }
        else if (TestCharController.player.GetComponent<TestCharController>().east)
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Cast_Fire"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 0));
        }
        else if (TestCharController.player.GetComponent<TestCharController>().west)
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Cast_Fire"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 180));
        }
        else if (TestCharController.player.GetComponent<TestCharController>().south)
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Cast_Fire"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 270));
        }
    }

    //Cast Animation - Elec
    void SpawnElec()
    {
        if (TestCharController.player.GetComponent<TestCharController>().north)
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Elec_Cast"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 90));
        }
        else if (TestCharController.player.GetComponent<TestCharController>().east)
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Elec_Cast"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 0));
        }
        else if (TestCharController.player.GetComponent<TestCharController>().west)
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Elec_Cast"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 180));
        }
        else if (TestCharController.player.GetComponent<TestCharController>().south)
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Elec_Cast"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 270));
        }
    }
    //Cast Animation - Wind
    void SpawnWind()
    {
        if (TestCharController.player.GetComponent<TestCharController>().north)
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Cast_Wind"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 90));
        }
        else if (TestCharController.player.GetComponent<TestCharController>().east)
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Cast_Wind"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 0));
        }
        else if (TestCharController.player.GetComponent<TestCharController>().west)
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Cast_Wind"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 180));
        }
        else if (TestCharController.player.GetComponent<TestCharController>().south)
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Cast_Wind"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 270));
        }
    }

    //Cast Animation - Earth
    void SpawnEarth()
    {
        if (TestCharController.player.GetComponent<TestCharController>().north)
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Earth_Cast"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 90));
        }
        else if (TestCharController.player.GetComponent<TestCharController>().east)
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Earth_Cast"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 0));
        }
        else if (TestCharController.player.GetComponent<TestCharController>().west)
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Earth_Cast"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 180));
        }
        else if (TestCharController.player.GetComponent<TestCharController>().south)
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Earth_Cast"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 270));
        }
    }

    //Cast Animation - Water
    void SpawnWater()
    {
        if (TestCharController.player.GetComponent<TestCharController>().north)
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Cast_Water"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 90));
        }
        else if (TestCharController.player.GetComponent<TestCharController>().east)
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Cast_Water"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 0));
        }
        else if (TestCharController.player.GetComponent<TestCharController>().west)
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Cast_Water"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 180));
        }
        else if (TestCharController.player.GetComponent<TestCharController>().south)
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Cast_Water"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.Euler(0, 0, 270));
        }
    }

}
