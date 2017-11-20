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

    //UI component that shows the arcana's image
    public Image activeCard1;
    public Image activeCard2;
    public Image activeCard3;


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

    public Animator card_1_Animator;
    public Animator card_2_Animator;
    public Animator card_3_Animator;

    public AudioSource card_1_Audio;
    public AudioSource card_2_Audio;
    public AudioSource card_3_Audio;

    public AudioSource playerCastAudio;

    public AudioSource errorNoise;

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
        activeCard1 = GameObject.Find("Card_1").GetComponent<Image>();
        activeCard2 = GameObject.Find("Card_2").GetComponent<Image>();
        activeCard3 = GameObject.Find("Card_3").GetComponent<Image>();

        errorNoise = GameObject.Find("ErrorNoise").GetComponent<AudioSource>();
        playerCastAudio = GameObject.Find("PlayerCastAudio").GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        if((Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetKeyDown(KeyCode.C)) && !isUsing && !InventoryController.inInv && !TestCharController.inDialogue && TestCharController.arcanaEnabled) //forward
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
        }
        else if((Input.GetAxis("Mouse ScrollWheel") < 0f || Input.GetKeyDown(KeyCode.V)) && !isUsing && !InventoryController.inInv && !TestCharController.inDialogue && TestCharController.arcanaEnabled) //backwards
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
        }

        GetCards();

        if(!init)
        {
            SetActiveCard();
            init = true;
        }
        

        if((Input.GetKeyDown(KeyCode.Q) || Input.GetMouseButtonDown(2)) && !InventoryController.inInv && !TestCharController.inDialogue && TestCharController.arcanaEnabled)
        {
            if(!isUsing)
            {
                manaSnapshot = PlayerStats.mana;
                StartCoroutine(UseCardRoutine());
                UseCard();
            }
            
        }


    }

    void GetCards()
    {
        activeCard1.sprite = Resources.Load<Sprite>(Deck.activeCards[0].cardAddress);
        activeCard2.sprite = Resources.Load<Sprite>(Deck.activeCards[1].cardAddress);
        activeCard3.sprite = Resources.Load<Sprite>(Deck.activeCards[2].cardAddress);
    }


    void SetActiveCard()
    {
        
        if (cardObjectNum == 1)
        {
            cardSelectFrame1.gameObject.SetActive(true);
            cardSelectFrame2.gameObject.SetActive(false);
            cardSelectFrame3.gameObject.SetActive(false);
            spellName.text = Deck.activeCards[0].cardName;
        }
        else if (cardObjectNum == 2)
        {
            cardSelectFrame1.gameObject.SetActive(false);
            cardSelectFrame2.gameObject.SetActive(true);
            cardSelectFrame3.gameObject.SetActive(false);
            spellName.text = Deck.activeCards[1].cardName;
        }
        else if (cardObjectNum == 3)
        {
            cardSelectFrame1.gameObject.SetActive(false);
            cardSelectFrame2.gameObject.SetActive(false);
            cardSelectFrame3.gameObject.SetActive(true);
            spellName.text = Deck.activeCards[2].cardName;
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
        }
        else if (cardObjectNum == 2 && PlayerStats.mana < Deck.activeCards[1].manaCost)
        {
            card_2_Animator.Play("Card_2_Error");
            errorNoise.Play();
        }
        else if (cardObjectNum == 3 && PlayerStats.mana < Deck.activeCards[2].manaCost)
        {
            card_3_Animator.Play("Card_3_Error");
            errorNoise.Play();
        }
    }

    IEnumerator UseCardRoutine()
    {
        isUsing = true;

        
        
        
        //Update the active card list
        if(manaSnapshot >= Deck.activeCards[cardObjectNum-1].manaCost)
        {
            //print(Deck.activeCards[cardObjectNum - 1].manaCost);

            SpawnCardEffect(cardObjectNum);
           
            playerCastAudio.clip = TestCharController.castNoise[Random.Range(0,2)];
            playerCastAudio.Play();

            yield return new WaitForSeconds(1f);
            Deck.UseCard(cardObjectNum);

        }
        card_1_Animator.Play("Card_1_Idle");
        card_2_Animator.Play("Card_2_Idle");
        card_3_Animator.Play("Card_3_Idle");
        SetActiveCard();

        isUsing = false;
    }

    void SpawnCardEffect(int card)
    {
        if (Deck.activeCards[card - 1].cardName == "Firebolt")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/FireBall"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.identity);

            //Cast Animation - Fire
            Instantiate(Resources.Load("Prefabs/SpellFX/Cast_Fire"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.Euler(TestCharController.wepDir.x, TestCharController.wepDir.y, TestCharController.wepDir.z - 90));

        }
        else if (Deck.activeCards[card - 1].cardName == "Embers")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Embers"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.identity);

            //Cast Animation - Fire
            Instantiate(Resources.Load("Prefabs/SpellFX/Cast_Fire"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.Euler(TestCharController.wepDir.x, TestCharController.wepDir.y, TestCharController.wepDir.z - 90));
        }
        else if (Deck.activeCards[card - 1].cardName == "Blaze")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Blaze"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.identity);

            //Cast Animation - Fire
            Instantiate(Resources.Load("Prefabs/SpellFX/Cast_Fire"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.Euler(TestCharController.wepDir.x, TestCharController.wepDir.y, TestCharController.wepDir.z - 90));
        }
        else if(Deck.activeCards[card - 1].cardName == "Stone")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Stone_Deploy"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.identity);
        }

        else if (Deck.activeCards[card - 1].cardName == "Boulder")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Boulder"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.identity);
        }

        else if (Deck.activeCards[card - 1].cardName == "Fire Spin")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/FireSpin"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.identity);
        }

        else if (Deck.activeCards[card - 1].cardName == "Magma")
        {
            Instantiate(Resources.Load("Prefabs/SpellFX/Magma"), new Vector2(TestCharController.player.transform.position.x,
                TestCharController.player.transform.position.y), Quaternion.identity);
        }
    }

}
