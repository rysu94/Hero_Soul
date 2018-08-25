using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Card_Interface : MonoBehaviour
{
    public static bool starterPack = false;
    public static bool canClick = false;
    public bool deckLoaded = false;

    public GameObject cardSelected;
    public GameObject[] cardSprites = new GameObject[8];
    public Text[] cardNames = new Text[8];
    public Text[] cardQuant = new Text[8];
    public Text deckNum;
    public static int currentDeckCard = 0;
    public int minCardID = 1;
    public int maxCardID = 8;

    public GameObject cardUINoise;
    public GameObject cardUIRareNoise;
    public GameObject cardUIRareChargeNoise;
    public GameObject cardUIErrorNoise;

    public GameObject cardSlabPrefab;
    public Transform deckGrid;

    public List<int> deckListID = new List<int>();

    public List<GameObject> slabList = new List<GameObject>();


    //Booster Pack
    public Button openBooster;
    public GameObject openBoosterFrame;
    public Image[] boosterIMG = new Image[5];
    public bool openingBooster = false;
    public bool doneOpening = false;
    public int[] chosenCards = new int[5];

    public Button resetButton;
    public Button doneButton;
    public Button craftingBack;
    public Button craftButton;
    public Button toggleButton;

    //Tooltip
    public GameObject tooltip;

    //System Message
    public GameObject system;

    //Arcana Numbers
    public Text fireArcana;
    public Text waterArcana;
    public Text lifeArcana;
    public Text earthArcana;
    public Text airArcana;

    public GameObject craftingInterface;
    public int numCrafts;
    public int craftID;

    public GameObject blankCard;
    public GameObject firePrefab;
    public GameObject waterPrefab;
    public GameObject lifePrefab;
    public GameObject earthPrefab;
    public GameObject airPrefab;
    public int[] arcanaCost = new int[5];
    public int[] arcanaDisenchant = new int[5];
    public GameObject craftedCard;
    public GameObject grats;

    //Craft Toggle
    public bool disenchantToggle = false;

    //Tutorial Message
    public GameObject tutMessage;
    public Text tutText;

    //Previous Scene
    public static string sceneName;
    public static float returnX = -2.3f;
    public static float returnY = 0;
    public static string startTag = "Up";

    //Xbox Controller Tabs
    public static int spellTabNum = 0;

    //RuneForging
    public GameObject runeForge;
    public GameObject runeFade;

    //Card Cost
    public GameObject[] cardCost = new GameObject[8];
    public GameObject[] cardCostSmall = new GameObject[8];

    // Use this for initialization
    void Start()
    {
        if (starterPack)
        {
            openBoosterFrame.SetActive(false);
        }
        openBooster.onClick.AddListener(StarterPack);
        resetButton.onClick.AddListener(ResetDeck);
        doneButton.onClick.AddListener(DoneDeck);
        craftingBack.onClick.AddListener(CloseCrafting);
        craftButton.onClick.AddListener(Craft);
        toggleButton.onClick.AddListener(ToggleCraft);

        //Change the mouse cursor
        Cursor.SetCursor((Texture2D)Resources.Load("Cursor"), Vector2.zero, CursorMode.Auto);

        spellTabNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        LoadPlayerDeck();
        CheckSlabList();
        CheckHoverCard();
        CheckSpellTab();
        CheckDeckNum();
        UpdateArcana();
        CheckCrafting();
        GameObject.Find("Total_Cards").GetComponent<Text>().text = "Total Cards: \n" + GetTotalCardCount();

        if(!TutorialDatabase.tut6 && currentDeckCard > 0)
        {
            TutorialDatabase.tut6 = true;
            tutMessage.GetComponent<LifespanHide>().active = false;
            tutMessage.SetActive(false);
            tutText.text = "Deck Building Basics:\nTo remove cards from your deck, you can either use [LMB] to remove single cards or click the \"Reset\" button to clear all cards.";
            tutMessage.SetActive(true);
        }

        if(!TutorialDatabase.tut7 && currentDeckCard == 15)
        {
            TutorialDatabase.tut7 = true;
            tutMessage.GetComponent<LifespanHide>().active = false;
            tutMessage.SetActive(false);
            tutText.text = "Deck Building Basics:\nWhen you are done building you click the \"Done\" button to exit deck building.";
            tutMessage.SetActive(true);
        }

    }

    void ToggleCraft()
    {
        if(true)
        {
            GetComponent<AudioSource>().Play();
            if (!disenchantToggle)
            {
                toggleButton.gameObject.transform.Find("Text").GetComponent<Text>().text = "Disenchant";
                system.transform.Find("Text").GetComponent<Text>().text = "Disenchant Mode Enabled";
                system.SetActive(true);
                disenchantToggle = true;
            }
            else if(disenchantToggle)
            {
                toggleButton.gameObject.transform.Find("Text").GetComponent<Text>().text = "Crafting";
                system.transform.Find("Text").GetComponent<Text>().text = "Crafting Mode Enabled";
                system.SetActive(true);
                disenchantToggle = false;
            }
        }

    }

    void Disenchant()
    {
        GetComponent<AudioSource>().Play();
        craftingInterface.SetActive(false);
        craftedCard.SetActive(true);

        StartCoroutine(DisenchantCard());

    }

    void Craft()
    {
        if(!disenchantToggle)
        {
            if (InventoryManager.fireArcana >= arcanaCost[0] && InventoryManager.waterArcana >= arcanaCost[1] &&
                InventoryManager.lifeArcana >= arcanaCost[2] && InventoryManager.earthArcana >= arcanaCost[3] &&
                InventoryManager.windArcana >= arcanaCost[4] && numCrafts > 0)
            {
                GetComponent<AudioSource>().Play();
                blankCard.SetActive(true);
                craftedCard.SetActive(true);
                craftingInterface.SetActive(false);

                StartCoroutine(GenerateFire(arcanaCost[0], true));
                StartCoroutine(GenerateWater(arcanaCost[1], true));
                StartCoroutine(GenerateLife(arcanaCost[2], true));
                StartCoroutine(GenerateEarth(arcanaCost[3], true));
                StartCoroutine(GenerateAir(arcanaCost[4], true));
                StartCoroutine(RevealCraftedCard());

            }
            else
            {
                cardUIErrorNoise.GetComponent<AudioSource>().Play();
                system.transform.Find("Text").GetComponent<Text>().text = "Not Enough Arcana!";
                system.SetActive(true);
            }
        }
        else if(disenchantToggle)
        {
            Disenchant();
        }



    }

    IEnumerator DisenchantCard()
    {
        grats.SetActive(true);
        grats.transform.Find("Congrats").gameObject.GetComponent<Text>().text = "Disenchanting...";
        grats.transform.Find("Text").gameObject.SetActive(false);
        craftedCard.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cards/Arcana_" + GetComponent<CardDatabase>().FindCard(craftID).cardName);
        cardUIRareChargeNoise.GetComponent<AudioSource>().Play();
        for (float i = 1f; i > 0; i -= .01f)
        {
            craftedCard.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(0.03f);
        }
        cardUIRareNoise.GetComponent<AudioSource>().Play();
        StartCoroutine(GenerateFire(arcanaDisenchant[0], false));
        StartCoroutine(GenerateWater(arcanaDisenchant[1], false));
        StartCoroutine(GenerateLife(arcanaDisenchant[2], false));
        StartCoroutine(GenerateEarth(arcanaDisenchant[3], false));
        StartCoroutine(GenerateAir(arcanaDisenchant[4], false));
        craftedCard.SetActive(false);
        grats.transform.Find("Text").gameObject.SetActive(true);
        grats.transform.Find("Congrats").gameObject.GetComponent<Text>().text = "Congratulations";
        grats.transform.Find("Text").GetComponent<Text>().text = GetComponent<CardDatabase>().FindCard(craftID).cardName + " x" + numCrafts + " disenchanted!";
        yield return new WaitForSeconds(4f);
        grats.SetActive(false);
        craftedCard.SetActive(false);
        blankCard.SetActive(false);
        canClick = true;
        GetComponent<CardDatabase>().FindCard(craftID).cardQuant -= numCrafts;
    }


    IEnumerator RevealCraftedCard()
    {
        float interval = 0.0118f;
        grats.SetActive(true);
        //grats.transform.Find("Congrats").gameObject.SetActive(false);
        grats.transform.Find("Congrats").gameObject.GetComponent<Text>().text = "Crafting...";
        grats.transform.Find("Text").gameObject.SetActive(false);
        craftedCard.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cards/Arcana_Blank");
        for (float i = 0; i < 255; i++)
        {
            craftedCard.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i/255f);
            yield return new WaitForSeconds(interval);
        }
        cardUIRareChargeNoise.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(2.5f);

        craftedCard.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cards/NewSpell/Arcana_" + GetComponent<CardDatabase>().FindCard(craftID).cardName);

        cardUIRareNoise.GetComponent<AudioSource>().Play();
        craftedCard.transform.localScale = new Vector2(7, 7);
        grats.transform.Find("Congrats").gameObject.GetComponent<Text>().text = "Congratulations";
        grats.transform.Find("Text").gameObject.SetActive(true);
        grats.transform.Find("Text").GetComponent<Text>().text = "You crafted " + GetComponent<CardDatabase>().FindCard(craftID).cardName + " x" + numCrafts + "!";
        for (float i = 7; i > 5; i-=.05f)
        {
            craftedCard.transform.localScale = new Vector2(i, i);
            yield return new WaitForSeconds(.025f);
        }

        yield return new WaitForSeconds(3f);
        grats.SetActive(false);
        craftedCard.SetActive(false);
        blankCard.SetActive(false);
        canClick = true;
        GetComponent<CardDatabase>().FindCard(craftID).cardQuant += numCrafts;
    }

    IEnumerator GenerateFire(int fireArcana, bool isCrafting)
    {
        float interval = 3f / fireArcana;
        for(float i = 0; i < fireArcana; i++)
        {
            Vector2 pos = Random.insideUnitCircle * 5 + new Vector2(-2.96f, 0);
            if(isCrafting)
            {
                Vector2 velocity = new Vector2(-2.96f / 3f, 0) - pos / 3;
                Instantiate(firePrefab, pos, Quaternion.identity).GetComponent<Rigidbody2D>().velocity = velocity;
                InventoryManager.fireArcana--;
                yield return new WaitForSeconds(interval);
            }
            else if(!isCrafting)
            {
                Vector2 velocity = pos / 3 - new Vector2(-2.96f / 3f, 0);
                Instantiate(firePrefab, new Vector2(-2.96f, 0), Quaternion.identity).GetComponent<Rigidbody2D>().velocity = velocity;
                InventoryManager.fireArcana++;
            }

            
            
        }
    }

    IEnumerator GenerateWater(int waterArcana, bool isCrafting)
    {
        float interval = 3f / waterArcana;
        for (float i = 0; i < waterArcana; i++)
        {
            Vector2 pos = Random.insideUnitCircle * 5 + new Vector2(-2.96f, 0);
            if(isCrafting)
            {
                Vector2 velocity = new Vector2(-2.96f / 3f, 0) - pos / 3;
                Instantiate(waterPrefab, pos, Quaternion.identity).GetComponent<Rigidbody2D>().velocity = velocity;
                InventoryManager.waterArcana--;
                yield return new WaitForSeconds(interval);
            }
            else if(!isCrafting)
            {
                Vector2 velocity = pos / 3 - new Vector2(-2.96f / 3f, 0);
                Instantiate(waterPrefab, new Vector2(-2.96f, 0), Quaternion.identity).GetComponent<Rigidbody2D>().velocity = velocity;
                InventoryManager.waterArcana++;
            }

            
            
        }
    }

    IEnumerator GenerateLife(int lifeArcana, bool isCrafting)
    {
        float interval = 3f / lifeArcana;
        for (float i = 0; i < lifeArcana; i++)
        {
            Vector2 pos = Random.insideUnitCircle * 5 + new Vector2(-2.96f, 0);
            if (isCrafting)
            {
                Vector2 velocity = new Vector2(-2.96f / 3f, 0) - pos / 3;
                Instantiate(lifePrefab, pos, Quaternion.identity).GetComponent<Rigidbody2D>().velocity = velocity;
                InventoryManager.lifeArcana--;
                yield return new WaitForSeconds(interval);
            }
            else if (!isCrafting)
            {
                Vector2 velocity = pos / 3 - new Vector2(-2.96f / 3f, 0);
                Instantiate(lifePrefab, new Vector2(-2.96f, 0), Quaternion.identity).GetComponent<Rigidbody2D>().velocity = velocity;
                InventoryManager.lifeArcana++;
            }    
        }
    }

    IEnumerator GenerateEarth(int earthArcana, bool isCrafting)
    {
        float interval = 3f / earthArcana;
        for (float i = 0; i < earthArcana; i++)
        {
            Vector2 pos = Random.insideUnitCircle * 5 + new Vector2(-2.96f, 0);
            if (isCrafting)
            {
                Vector2 velocity = new Vector2(-2.96f / 3f, 0) - pos / 3;
                Instantiate(earthPrefab, pos, Quaternion.identity).GetComponent<Rigidbody2D>().velocity = velocity;
                InventoryManager.earthArcana--;
                yield return new WaitForSeconds(interval);
            }
            else if (!isCrafting)
            {
                Vector2 velocity = pos / 3 - new Vector2(-2.96f / 3f, 0);
                Instantiate(earthPrefab, new Vector2(-2.96f, 0), Quaternion.identity).GetComponent<Rigidbody2D>().velocity = velocity;
                InventoryManager.earthArcana++;
            }
        }
    }

    IEnumerator GenerateAir(int airArcana, bool isCrafting)
    {
        float interval = 3f / airArcana;
        for (float i = 0; i < airArcana; i++)
        {
            Vector2 pos = Random.insideUnitCircle * 5 + new Vector2(-2.96f, 0);
            if (isCrafting)
            {
                Vector2 velocity = new Vector2(-2.96f / 3f, 0) - pos / 3;
                Instantiate(airPrefab, pos, Quaternion.identity).GetComponent<Rigidbody2D>().velocity = velocity;
                InventoryManager.windArcana--;
                yield return new WaitForSeconds(interval);
            }
            else if (!isCrafting)
            {
                Vector2 velocity = pos / 3 - new Vector2(-2.96f / 3f, 0);
                Instantiate(airPrefab, new Vector2(-2.96f, 0), Quaternion.identity).GetComponent<Rigidbody2D>().velocity = velocity;
                InventoryManager.windArcana++;
            }
        }
    }




    void CloseCrafting()
    {
        GetComponent<AudioSource>().Play();
        craftingInterface.SetActive(false);
        canClick = true;
    }

    int FindCardInDeck(int id)
    {
        int returnInt = 0;

        for(int i = 0; i < Deck.playerDeck.Length; i++)
        {
            if(Deck.playerDeck[i].cardID == id)
            {
                returnInt++;
            }
        }

        for(int j = 0; j < slabList.Count; j++)
        {
            if(slabList[j].GetComponent<Slab>().cardID == id)
            {
                returnInt += slabList[j].GetComponent<Slab>().cardQuant;
            }
        }

        return returnInt;
    }

    void CheckCrafting()
    {
        //Cards Owned
        if(!disenchantToggle)
        {
            craftingInterface.transform.Find("Card_Owned").GetComponent<Text>().text = "<color=#00ff00ff>Owned</color>: " + (GetComponent<CardDatabase>().FindCard(craftID).cardQuant + FindCardInDeck(craftID)) + "/5";
            
            //Update Cost numbers
            craftingInterface.transform.Find("Fire_Arcana").transform.Find("Text").GetComponent<Text>().text = (GetComponent<CardDatabase>().FindCard(craftID).fireArcanaCost * numCrafts).ToString();
            craftingInterface.transform.Find("Water_Arcana").transform.Find("Text").GetComponent<Text>().text = (GetComponent<CardDatabase>().FindCard(craftID).waterArcanaCost * numCrafts).ToString();
            craftingInterface.transform.Find("Life_Arcana").transform.Find("Text").GetComponent<Text>().text = (GetComponent<CardDatabase>().FindCard(craftID).lifeArcanaCost * numCrafts).ToString();
            craftingInterface.transform.Find("Earth_Arcana").transform.Find("Text").GetComponent<Text>().text = (GetComponent<CardDatabase>().FindCard(craftID).earthArcanaCost * numCrafts).ToString();
            craftingInterface.transform.Find("Air_Arcana").transform.Find("Text").GetComponent<Text>().text = (GetComponent<CardDatabase>().FindCard(craftID).windArcanaCost * numCrafts).ToString();
        }
        else if(disenchantToggle)
        {
            craftingInterface.transform.Find("Card_Owned").GetComponent<Text>().text = "<color=#00ff00ff>In Stock</color>: " + (GetComponent<CardDatabase>().FindCard(craftID).cardQuant);
            craftingInterface.transform.Find("Fire_Arcana").transform.Find("Text").GetComponent<Text>().text = ((GetComponent<CardDatabase>().FindCard(craftID).fireArcanaCost/2) * numCrafts).ToString();
            craftingInterface.transform.Find("Water_Arcana").transform.Find("Text").GetComponent<Text>().text = ((GetComponent<CardDatabase>().FindCard(craftID).waterArcanaCost/2) * numCrafts).ToString();
            craftingInterface.transform.Find("Life_Arcana").transform.Find("Text").GetComponent<Text>().text = ((GetComponent<CardDatabase>().FindCard(craftID).lifeArcanaCost/2) * numCrafts).ToString();
            craftingInterface.transform.Find("Earth_Arcana").transform.Find("Text").GetComponent<Text>().text = ((GetComponent<CardDatabase>().FindCard(craftID).earthArcanaCost/2) * numCrafts).ToString();
            craftingInterface.transform.Find("Air_Arcana").transform.Find("Text").GetComponent<Text>().text = ((GetComponent<CardDatabase>().FindCard(craftID).windArcanaCost/2) * numCrafts).ToString();
        }
        

        //Card  Name
        craftingInterface.transform.Find("Card_Name").GetComponent<Text>().text = GetComponent<CardDatabase>().FindCard(craftID).cardName;

        //Crafted Cards
        craftingInterface.transform.Find("Crafting_Frame").transform.Find("Craft_Quant").GetComponent<Text>().text = numCrafts.ToString();


        arcanaCost[0] = GetComponent<CardDatabase>().FindCard(craftID).fireArcanaCost * numCrafts;
        arcanaCost[1] = GetComponent<CardDatabase>().FindCard(craftID).waterArcanaCost * numCrafts;
        arcanaCost[2] = GetComponent<CardDatabase>().FindCard(craftID).lifeArcanaCost * numCrafts;
        arcanaCost[3] = GetComponent<CardDatabase>().FindCard(craftID).earthArcanaCost * numCrafts;
        arcanaCost[4] = GetComponent<CardDatabase>().FindCard(craftID).windArcanaCost * numCrafts;

        arcanaDisenchant[0] = (GetComponent<CardDatabase>().FindCard(craftID).fireArcanaCost / 2) * numCrafts;
        arcanaDisenchant[1] = (GetComponent<CardDatabase>().FindCard(craftID).waterArcanaCost / 2) * numCrafts;
        arcanaDisenchant[2] = (GetComponent<CardDatabase>().FindCard(craftID).lifeArcanaCost / 2) * numCrafts;
        arcanaDisenchant[3] = (GetComponent<CardDatabase>().FindCard(craftID).earthArcanaCost / 2) * numCrafts;
        arcanaDisenchant[4] = (GetComponent<CardDatabase>().FindCard(craftID).windArcanaCost / 2) * numCrafts;

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = Camera.main.transform.position.z;
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit.collider != null)
        {
            if(hit.collider.tag == "Split_Item_Add" && !disenchantToggle)
            {
                if(Input.GetMouseButtonDown(0) && GetComponent<CardDatabase>().FindCard(craftID).cardQuant + FindCardInDeck(craftID) + numCrafts < 5)
                {
                    GetComponent<AudioSource>().Play();
                    numCrafts++;
                }
                else if(Input.GetMouseButtonDown(0))
                {
                    cardUIErrorNoise.GetComponent<AudioSource>().Play();
                    system.transform.Find("Text").GetComponent<Text>().text = "You can't craft anymore of this card!";
                    system.SetActive(true);
                }
            }

            if (hit.collider.tag == "Split_Item_Sub" && !disenchantToggle)
            {
                if (Input.GetMouseButtonDown(0) && numCrafts > 0)
                {
                    GetComponent<AudioSource>().Play();
                    numCrafts--;
                }
                else if(Input.GetMouseButtonDown(0))
                {
                    cardUIErrorNoise.GetComponent<AudioSource>().Play();
                    system.transform.Find("Text").GetComponent<Text>().text = "You can't have any less of this card!";
                    system.SetActive(true);
                }
            }

            if(hit.collider.tag == "Split_Item_Add" && disenchantToggle)
            {
                if(Input.GetMouseButtonDown(0) && numCrafts < GetComponent<CardDatabase>().FindCard(craftID).cardQuant && GetTotalCardCount() - numCrafts > 15)
                {
                    GetComponent<AudioSource>().Play();
                    numCrafts++;
                }
                else if(Input.GetMouseButtonDown(0))
                {
                    cardUIErrorNoise.GetComponent<AudioSource>().Play();
                    system.transform.Find("Text").GetComponent<Text>().text = "You can't disenchant any more of this card!";
                    system.SetActive(true);
                }
            }

            if(hit.collider.tag == "Split_Item_Sub" && disenchantToggle)
            {
                if(Input.GetMouseButtonDown(0) && numCrafts > 0)
                {
                    GetComponent<AudioSource>().Play();
                    numCrafts--;
                }
                else if(Input.GetMouseButtonDown(0))
                {
                    cardUIErrorNoise.GetComponent<AudioSource>().Play();
                    system.transform.Find("Text").GetComponent<Text>().text = "You can't disenchant any less of this card!";
                    system.SetActive(true);
                }
            }

        }
    }


    void LoadPlayerDeck()
    {
        Deck.InitDeck();
        if(!deckLoaded)
        {
            deckLoaded = true;
            for (int i = 0; i < Deck.playerDeck.Length; i++)
            {
                CreateSlab(Deck.playerDeck[i].cardID);
            }
        }
    }

    void UpdateArcana()
    {
        fireArcana.text = InventoryManager.fireArcana.ToString();
        waterArcana.text = InventoryManager.waterArcana.ToString();
        lifeArcana.text = InventoryManager.lifeArcana.ToString();
        earthArcana.text = InventoryManager.earthArcana.ToString();
        airArcana.text = InventoryManager.windArcana.ToString();
    }

    void DoneDeck()
    {
        if (currentDeckCard >= 15 && canClick)
        {
            TestCharController.inDialogue = false;
            cardUIRareNoise.GetComponent<AudioSource>().Play();
            int deckIndex = 0;
            for (int i = 0; i < slabList.Count; i++)
            {
                for (int j = 0; j < slabList[i].GetComponent<Slab>().cardQuant; j++)
                {
                    Deck.playerDeck[deckIndex] = GetComponent<CardDatabase>().FindCard(slabList[i].GetComponent<Slab>().cardID);
                    deckIndex++;
                }
            }
            LevelCreator.playerStartX = returnX;
            LevelCreator.playerStartY = returnY;
            LevelCreator.startTag = startTag;
            SceneManager.LoadScene(sceneName);

            //Add a new objective
            for (int i = 0; i < QuestManager.activeMainQuests.Count; i++)
            {
                if (QuestManager.activeMainQuests[i].questName == "It's Magic")
                {
                    QuestManager.activeMainQuests[i].objComplete[1] = true;
                }
            }

        }
        else if (canClick)
        {
            cardUIErrorNoise.GetComponent<AudioSource>().Play();
            system.transform.Find("Text").GetComponent<Text>().text = "You don't have enough cards in your deck!";
            system.SetActive(true);
        }

    }

    void ResetDeck()
    {
        if(canClick)
        {
            GetComponent<AudioSource>().Play();
            for (int i = slabList.Count - 1; i >= 0; i--)
            {
                currentDeckCard = 0;
                GameObject tempObj = slabList[i];

                GetComponent<CardDatabase>().FindCard(tempObj.GetComponent<Slab>().cardID).cardQuant += tempObj.GetComponent<Slab>().cardQuant;

                slabList.RemoveAt(i);
                Destroy(tempObj);
            }
        }

    }

    void CheckDeckNum()
    {
        deckNum.text = currentDeckCard + "/" + Deck.deckSize;
        if (currentDeckCard < 15)
        {
            deckNum.color = new Color(255, 0, 0);
        }
        else
        {
            deckNum.color = new Color(255, 255, 255);
        }
    }

    void CheckSlabList()
    {
        for(int i = 0; i < slabList.Count; i++)
        {
            if(slabList[i].GetComponent<Slab>().cardID == 0)
            {
                GameObject tempObj = slabList[i];
                slabList.RemoveAt(i);
                Destroy(tempObj);
            }
        }
    }

    void CreateSlab(int id)
    {
        bool slabFound = false;
        //Check if slab already exists
        for(int i = 0; i < slabList.Count; i++)
        {
            if(slabList[i].GetComponent<Slab>().cardID == id && !slabFound)
            {
                slabFound = true;
                slabList[i].GetComponent<Slab>().AddQuant();
                slabList[i].transform.Find("Card_Quant").GetComponent<Text>().text = "x" + (slabList[i].GetComponent<Slab>().cardQuant);
                currentDeckCard++; 
            }
        }

        if(!slabFound && id != 0)
        {
            currentDeckCard++;
            GameObject tempObj = Instantiate(cardSlabPrefab, deckGrid);
            tempObj.GetComponent<Slab>().cardID = id;
            tempObj.GetComponent<Slab>().cardQuant = 1;
            tempObj.GetComponent<Slab>().manaCost = GetComponent<CardDatabase>().FindCard(id).manaCost;
            tempObj.GetComponent<Slab>().cardName = GetComponent<CardDatabase>().FindCard(id).cardName;
            tempObj.GetComponent<Slab>().cardRarity = GetComponent<CardDatabase>().FindCard(id).cardRarity;
            tempObj.transform.Find("Card_Name").GetComponent<Text>().text = GetComponent<CardDatabase>().FindCard(id).cardName;
            tempObj.transform.Find("RarityOrb").GetComponent<Image>().color = GetOrbColor(GetComponent<CardDatabase>().FindCard(id).cardRarity);
            tempObj.transform.Find("MaskBG").transform.Find("CardMask").transform.Find("Card_Img").GetComponent<Image>().sprite = Resources.Load<Sprite>("Cards/Arcana_" + GetComponent<CardDatabase>().FindCard(id).cardName);
            slabList.Add(tempObj);
        }

    }

    Color32 GetOrbColor(string rarity)
    {
        if(rarity == "Common")
        {
            return new Color32(255, 255, 255, 255);
        }
        else if(rarity == "Uncommon")
        {
            return new Color32(0, 255, 0, 255);
        }
        else if(rarity == "Rare")
        {
            return new Color32(0, 140, 255, 255);
        }
        else if(rarity == "Epic")
        {
            return new Color32(188, 0, 255, 255);
        }
        else if(rarity == "Legend")
        {
            return new Color32(255, 164, 0, 255);
        }
        return new Color32(0, 0, 0, 255);
    }

    void StarterPack()
    {
        GetComponent<AudioSource>().Play();
        starterPack = true;
        if(!openingBooster)
        {
            openBoosterFrame.GetComponent<Animator>().Play("OpenBooster");
            StartCoroutine(ShowBoosterCards());
        }
        else if(doneOpening)
        {
            RenderCards(minCardID, maxCardID);
            openingBooster = false;
            openBoosterFrame.SetActive(false);
            canClick = true;
            if (!TutorialDatabase.tut5)
            {
                tutText.text = "Deck Building Basics:\nTo add cards to your deck, use the [LMB] on cards you have discovered. You can browse your collection by clicking on the tabs above.";
                tutMessage.SetActive(true);
                TutorialDatabase.tut5 = true;
            }
        }
    }

    IEnumerator ShowBoosterCards()
    {
        openingBooster = true;
        GetStarterCards();
        yield return new WaitForSeconds(2f);
        for(int i = 0; i < boosterIMG.Length; i++)
        {
            if(i < 4)
            {
                yield return new WaitForSeconds(.75f);
                cardUINoise.GetComponent<AudioSource>().Play();
            }
            else
            {
                yield return new WaitForSeconds(1f);
                cardUIRareChargeNoise.GetComponent<AudioSource>().Play();
                yield return new WaitForSeconds(1f);
                cardUIRareNoise.GetComponent<AudioSource>().Play();
            }
            
            boosterIMG[i].sprite = Resources.Load<Sprite>("Cards/NewSpell/Arcana_" + GetComponent<CardDatabase>().FindCard(chosenCards[i]).cardName);
            boosterIMG[i].gameObject.transform.Find("Card_Quant").GetComponent<Text>().text = "x3";
            boosterIMG[i].gameObject.transform.Find("Card_Name").GetComponent<Text>().text = GetComponent<CardDatabase>().FindCard(chosenCards[i]).cardName;
            GetComponent<CardDatabase>().FindCard(chosenCards[i]).cardQuant += 3;
            InventoryManager.playerSpellbook[chosenCards[i]] = true;

            boosterIMG[i].gameObject.GetComponent<StarterCard>().id = chosenCards[i];

            for(int j = 0; j < GetComponent<Spell_Database>().FindSpell(chosenCards[i]).spellCost; j++)
            {
                Instantiate(Resources.Load("Cards/NewSpell/Cost_Crystal_Full"), boosterIMG[i].gameObject.transform.Find("Mana_Frame_Small"));
            }


        }

        doneOpening = true;

        openBooster.gameObject.GetComponentInChildren<Text>().text = "Close";
    }

    void GetStarterCards()
    {
        int tempInt = 0;
        //1 2 9 10 17 18 25 26 33 34
        List<int> commonID = new List<int>();
        commonID.Add(1);
        commonID.Add(2);
        commonID.Add(9);
        commonID.Add(10);
        commonID.Add(17);
        commonID.Add(18);
        commonID.Add(25);
        commonID.Add(26);
        commonID.Add(33);
        commonID.Add(34);

        for(int i = 0; i < 4; i++)
        {
            tempInt = Random.Range(0, commonID.Count);
            chosenCards[i] = commonID[tempInt];
            commonID.RemoveAt(tempInt);
        }

        //3 4 11 12 19 20 27 28 35 36
        List<int> uncommonID = new List<int>();
        uncommonID.Add(3);
        uncommonID.Add(4);
        uncommonID.Add(11);
        uncommonID.Add(12);
        uncommonID.Add(19);
        uncommonID.Add(20);
        uncommonID.Add(27);
        uncommonID.Add(28);
        uncommonID.Add(35);
        uncommonID.Add(36);

        tempInt = Random.Range(0, uncommonID.Count);
        chosenCards[4] = uncommonID[tempInt];
    }

    void RenderCards(int min, int max)
    {
        //Remove all spell crystals
        foreach (GameObject crystal in GameObject.FindGameObjectsWithTag("Spell_Crystal"))
        {
            Destroy(crystal);
        }

        for (int i = 0; i < cardSprites.Length; i++)
        {
            //cardSprites[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cards/Arcana_" + GetComponent<CardDatabase>().FindCard(min + i).cardName);
            //cardNames[i].text = GetComponent<CardDatabase>().FindCard(min + i).cardName;
            if (InventoryManager.playerSpellbook[min+i])
            {
                cardSprites[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cards/NewSpell/Arcana_" + GetComponent<CardDatabase>().FindCard(min + i).cardName);
                cardNames[i].text = GetComponent<CardDatabase>().FindCard(min + i).cardName;
                cardQuant[i].text = "x" + GetComponent<CardDatabase>().FindCard(min + i).cardQuant.ToString();

                //Create Spell Crystals
                for(int j = 0; j < GetComponent<Spell_Database>().FindSpell(min+i).spellCost; j++)
                {
                    if(GetComponent<Spell_Database>().FindSpell(min + i).spellCost > 3)
                    {
                        Instantiate(Resources.Load("Cards/NewSpell/Cost_Crystal_Full"), cardCost[i].transform);
                    }
                    else
                    {
                        Instantiate(Resources.Load("Cards/NewSpell/Cost_Crystal_Full"), cardCostSmall[i].transform);
                    }
                }



            }
            else
            {
                cardSprites[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cards/Arcana_Back");
                cardNames[i].text = "???";
                cardQuant[i].text = "";
            }
            
        }


    }


    void CheckSpellTab()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = Camera.main.transform.position.z;
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        if(hit.collider != null && Input.GetMouseButtonDown(0) && canClick)
        {
            if(hit.collider.tag == "SpellTab_Fire")
            {
                GetComponent<AudioSource>().Play();
                spellTabNum = 0;
                minCardID = 1;
                maxCardID = 8;
                RenderCards(minCardID, maxCardID);
            }
            else if(hit.collider.tag == "SpellTab_Earth")
            {
                GetComponent<AudioSource>().Play();
                spellTabNum = 1;
                minCardID = 9;
                maxCardID = 16;
                RenderCards(minCardID, maxCardID);
            }
            else if (hit.collider.tag == "SpellTab_Life")
            {
                GetComponent<AudioSource>().Play();
                spellTabNum = 2;
                minCardID = 17;
                maxCardID = 24;
                RenderCards(minCardID, maxCardID);
            }
            else if (hit.collider.tag == "SpellTab_Water")
            {
                GetComponent<AudioSource>().Play();
                spellTabNum = 3;
                minCardID = 25;
                maxCardID = 32;
                RenderCards(minCardID, maxCardID);
            }
            else if (hit.collider.tag == "SpellTab_Wind")
            {
                GetComponent<AudioSource>().Play();
                spellTabNum = 4;
                minCardID = 33;
                maxCardID = 40;
                RenderCards(minCardID, maxCardID);
            }
        }
        if (spellTabNum == 0)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Inventory/Spell_1");
            minCardID = 1;
            maxCardID = 8;
        }
        else if (spellTabNum == 1)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Inventory/Spell_2");
            minCardID = 9;
            maxCardID = 16;
        }
        else if (spellTabNum == 2)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Inventory/Spell_3");
            minCardID = 17;
            maxCardID = 24;
        }
        else if (spellTabNum == 3)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Inventory/Spell_4");
            minCardID = 25;
            maxCardID = 32;
        }
        else if (spellTabNum == 4)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Inventory/Spell_5");
            minCardID = 33;
            maxCardID = 40;
        }
    }

    void GetToolTip(int id)
    {
        //Tooltip
        tooltip.SetActive(true);
        Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tooltip.transform.position = new Vector3(newPos.x, newPos.y, 0);

        tooltip.transform.Find("Tooltip_Frame").transform.Find("Card_Name").GetComponent<Text>().text = GetComponent<CardDatabase>().FindCard(id).cardName;
        string eleType = "";
        string desc = "";
        GetComponent<CardTooltipDatabase>().FindCardInfo(GetComponent<CardDatabase>().FindCard(id).cardName, ref eleType, ref desc);

        tooltip.transform.Find("Tooltip_Frame").transform.Find("Element_Type").GetComponent<Text>().text = eleType;
        tooltip.transform.Find("Tooltip_Frame").transform.Find("Card_Desc").GetComponent<Text>().text = desc;

        //Card Type Image
        tooltip.transform.Find("Tooltip_Frame").transform.Find("Card_Type_IMG").GetComponent<Image>().sprite = Resources.Load<Sprite>("Cards/Icon/" + eleType);


        //Clear any generated sockets
        foreach (GameObject socket in GameObject.FindGameObjectsWithTag("Inv_Garbage"))
        {
            Destroy(socket);
        }

        //Rune Images
        for (int i = 0; i < GetComponent<Rune_Database>().FindRune(id).runes.Count; i++)
        {
            if (GetComponent<Rune_Database>().FindRune(id).equippedRunes[i] > -1)
            {
                if (GetComponent<Rune_Database>().FindRune(id).runes[i] == 1)
                {
                    GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/DeckBuilder/Tooltip_Rune/Red_Tooltip_Rune"), tooltip.transform.Find("Socketed_Runes").transform);
                    tempObj.transform.Find("Rune").GetComponent<Image>().sprite = Rune_Database.redSocketData[GetComponent<Rune_Database>().FindRune(id).equippedRunes[i]].transform.Find("Rune_IMG").GetComponent<Image>().sprite;
                }
                else
                {
                    GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/DeckBuilder/Tooltip_Rune/Blue_Tooltip_Rune"), tooltip.transform.Find("Socketed_Runes").transform);
                }

            }
            else
            {
                if (GetComponent<Rune_Database>().FindRune(id).runeUnlocks[i])
                {
                    if (GetComponent<Rune_Database>().FindRune(id).runes[i] == 1)
                    {
                        GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/DeckBuilder/Tooltip_Rune/Red_Tooltip_Rune"), tooltip.transform.Find("Socketed_Runes").transform);
                        tempObj.transform.Find("Rune").gameObject.SetActive(false);
                    }
                    else
                    {
                        GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/DeckBuilder/Tooltip_Rune/Blue_Tooltip_Rune"), tooltip.transform.Find("Socketed_Runes").transform);
                        tempObj.transform.Find("Rune").gameObject.SetActive(false);
                    }
                }
                else
                {
                    if (GetComponent<Rune_Database>().FindRune(id).runes[i] == 1)
                    {
                        GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/DeckBuilder/Tooltip_Rune/Red_Tooltip_Rune_Locked"), tooltip.transform.Find("Socketed_Runes").transform);
                    }
                    else
                    {
                        GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/DeckBuilder/Tooltip_Rune/Blue_Tooltip_Rune_Locked"), tooltip.transform.Find("Socketed_Runes").transform);
                    }
                }
            }
        }
    }

    void CheckHoverCard()
    {
        tooltip.SetActive(false);

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = Camera.main.transform.position.z;
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        if (hit.collider != null && canClick)
        {
            if (cardSelected != null)
            {
                cardSelected.SetActive(false);
            }

            if (hit.collider.tag == "Card_1")
            {
                cardSelected = hit.collider.gameObject.transform.Find("ArcanaSelected").gameObject;
                cardSelected.SetActive(true);
                if(Input.GetMouseButtonDown(0) && InventoryManager.playerSpellbook[minCardID] && GetComponent<CardDatabase>().FindCard(minCardID).cardQuant > 0)
                {
                    if(currentDeckCard < Deck.deckSize)
                    {
                        cardUINoise.GetComponent<AudioSource>().Play();
                        GetComponent<CardDatabase>().FindCard(minCardID).cardQuant--;
                        CreateSlab(minCardID);
                    }
                    else
                    {
                        cardUIErrorNoise.GetComponent<AudioSource>().Play();
                        system.transform.Find("Text").GetComponent<Text>().text = "Deck is full!";
                        system.SetActive(true);
                    }
                    
                }
                else if(Input.GetMouseButtonDown(0) && !InventoryManager.playerSpellbook[minCardID])
                {
                    cardUIErrorNoise.GetComponent<AudioSource>().Play();
                    system.transform.Find("Text").GetComponent<Text>().text = "Card not discovered yet!";
                    system.SetActive(true);
                }

                else if (Input.GetMouseButtonDown(0) && InventoryManager.playerSpellbook[minCardID] && GetComponent<CardDatabase>().FindCard(minCardID).cardQuant == 0)
                {
                    cardUIErrorNoise.GetComponent<AudioSource>().Play();
                    system.transform.Find("Text").GetComponent<Text>().text = "You don't have any more copies of that card!";
                    system.SetActive(true);
                }

                //Shoud a tooltip render?
                if (InventoryManager.playerSpellbook[minCardID])
                {
                    GetToolTip(minCardID);



                }


                //Card Crafting
                OpenCrafting(minCardID);

                //Runeforge
                OpenRuneForge(minCardID);

            }
            else if (hit.collider.tag == "Card_2")
            {
                cardSelected = hit.collider.gameObject.transform.Find("ArcanaSelected").gameObject;
                cardSelected.SetActive(true);
                if (Input.GetMouseButtonDown(0) && InventoryManager.playerSpellbook[minCardID+1] && GetComponent<CardDatabase>().FindCard(minCardID + 1).cardQuant > 0)
                {

                    if (currentDeckCard < Deck.deckSize)
                    {
                        cardUINoise.GetComponent<AudioSource>().Play();
                        GetComponent<CardDatabase>().FindCard(minCardID + 1).cardQuant--;
                        CreateSlab(minCardID+1);
                    }
                    else
                    {
                        cardUIErrorNoise.GetComponent<AudioSource>().Play();
                        system.transform.Find("Text").GetComponent<Text>().text = "Deck is full!";
                        system.SetActive(true);
                    }
                }
                else if (Input.GetMouseButtonDown(0) && !InventoryManager.playerSpellbook[minCardID+1])
                {
                    cardUIErrorNoise.GetComponent<AudioSource>().Play();
                    system.transform.Find("Text").GetComponent<Text>().text = "Card not discovered yet!";
                    system.SetActive(true);
                }

                else if (Input.GetMouseButtonDown(0) && InventoryManager.playerSpellbook[minCardID+1] && GetComponent<CardDatabase>().FindCard(minCardID+1).cardQuant == 0)
                {
                    cardUIErrorNoise.GetComponent<AudioSource>().Play();
                    system.transform.Find("Text").GetComponent<Text>().text = "You don't have any more copies of that card!";
                    system.SetActive(true);
                }

                //Shoud a tooltip render?
                if (InventoryManager.playerSpellbook[minCardID+1])
                {
                    GetToolTip(minCardID+1);
                }

                //Card Crafting
                OpenCrafting(minCardID+1);

                //Runeforge
                OpenRuneForge(minCardID+1);

            }
            else if (hit.collider.tag == "Card_3")
            {
                cardSelected = hit.collider.gameObject.transform.Find("ArcanaSelected").gameObject;
                cardSelected.SetActive(true);
                if (Input.GetMouseButtonDown(0) && InventoryManager.playerSpellbook[minCardID+2] && GetComponent<CardDatabase>().FindCard(minCardID + 2).cardQuant > 0)
                {
                    if (currentDeckCard < Deck.deckSize)
                    {
                        cardUINoise.GetComponent<AudioSource>().Play();
                        GetComponent<CardDatabase>().FindCard(minCardID + 2).cardQuant--;
                        CreateSlab(minCardID+2);
                    }
                    else
                    {
                        cardUIErrorNoise.GetComponent<AudioSource>().Play();
                        system.transform.Find("Text").GetComponent<Text>().text = "Deck is full!";
                        system.SetActive(true);
                    }
                }
                else if (Input.GetMouseButtonDown(0) && !InventoryManager.playerSpellbook[minCardID+2])
                {
                    cardUIErrorNoise.GetComponent<AudioSource>().Play();
                    system.transform.Find("Text").GetComponent<Text>().text = "Card not discovered yet!";
                    system.SetActive(true);
                }

                else if (Input.GetMouseButtonDown(0) && InventoryManager.playerSpellbook[minCardID+2] && GetComponent<CardDatabase>().FindCard(minCardID+2).cardQuant == 0)
                {
                    cardUIErrorNoise.GetComponent<AudioSource>().Play();
                    system.transform.Find("Text").GetComponent<Text>().text = "You don't have any more copies of that card!";
                    system.SetActive(true);
                }

                //Shoud a tooltip render?
                if (InventoryManager.playerSpellbook[minCardID+2])
                {
                    GetToolTip(minCardID+2);
                }

                //Card Crafting
                OpenCrafting(minCardID+2);

                //Runeforge
                OpenRuneForge(minCardID+2);

            }
            else if (hit.collider.tag == "Card_4")
            {
                cardSelected = hit.collider.gameObject.transform.Find("ArcanaSelected").gameObject;
                cardSelected.SetActive(true);
                if (Input.GetMouseButtonDown(0) && InventoryManager.playerSpellbook[minCardID+3] && GetComponent<CardDatabase>().FindCard(minCardID + 3).cardQuant > 0)
                {

                    if (currentDeckCard < Deck.deckSize)
                    {
                        cardUINoise.GetComponent<AudioSource>().Play();
                        GetComponent<CardDatabase>().FindCard(minCardID + 3).cardQuant--;
                        CreateSlab(minCardID+3);
                    }
                    else
                    {
                        cardUIErrorNoise.GetComponent<AudioSource>().Play();
                        system.transform.Find("Text").GetComponent<Text>().text = "Deck is full!";
                        system.SetActive(true);
                    }
                }
                else if (Input.GetMouseButtonDown(0) && !InventoryManager.playerSpellbook[minCardID+3])
                {
                    cardUIErrorNoise.GetComponent<AudioSource>().Play();
                    system.transform.Find("Text").GetComponent<Text>().text = "Card not discovered yet!";
                    system.SetActive(true);
                }

                else if (Input.GetMouseButtonDown(0) && InventoryManager.playerSpellbook[minCardID+3] && GetComponent<CardDatabase>().FindCard(minCardID+3).cardQuant == 0)
                {
                    cardUIErrorNoise.GetComponent<AudioSource>().Play();
                    system.transform.Find("Text").GetComponent<Text>().text = "You don't have any more copies of that card!";
                    system.SetActive(true);
                }

                //Shoud a tooltip render?
                if (InventoryManager.playerSpellbook[minCardID+3])
                {
                    GetToolTip(minCardID+3);
                }

                //Card Crafting
                OpenCrafting(minCardID+3);

                //Runeforge
                OpenRuneForge(minCardID + 3);

            }
            else if (hit.collider.tag == "Card_5")
            {
                cardSelected = hit.collider.gameObject.transform.Find("ArcanaSelected").gameObject;
                cardSelected.SetActive(true);
                if (Input.GetMouseButtonDown(0) && InventoryManager.playerSpellbook[minCardID+4] && GetComponent<CardDatabase>().FindCard(minCardID + 4).cardQuant > 0)
                {

                    if (currentDeckCard < Deck.deckSize)
                    {
                        cardUINoise.GetComponent<AudioSource>().Play();
                        GetComponent<CardDatabase>().FindCard(minCardID + 4).cardQuant--;
                        CreateSlab(minCardID+4);
                    }
                    else
                    {
                        cardUIErrorNoise.GetComponent<AudioSource>().Play();
                        system.transform.Find("Text").GetComponent<Text>().text = "Deck is full!";
                        system.SetActive(true);
                    }
                }
                else if (Input.GetMouseButtonDown(0) && !InventoryManager.playerSpellbook[minCardID+4])
                {
                    cardUIErrorNoise.GetComponent<AudioSource>().Play();
                    system.transform.Find("Text").GetComponent<Text>().text = "Card not discovered yet!";
                    system.SetActive(true);
                }

                else if (Input.GetMouseButtonDown(0) && InventoryManager.playerSpellbook[minCardID+4] && GetComponent<CardDatabase>().FindCard(minCardID+4).cardQuant == 0)
                {
                    cardUIErrorNoise.GetComponent<AudioSource>().Play();
                    system.transform.Find("Text").GetComponent<Text>().text = "You don't have any more copies of that card!";
                    system.SetActive(true);
                }

                //Shoud a tooltip render?
                if (InventoryManager.playerSpellbook[minCardID+4])
                {
                    GetToolTip(minCardID+4);
                }

                //Card Crafting
                OpenCrafting(minCardID+4);

                //Runeforge
                OpenRuneForge(minCardID+4);

            }
            else if (hit.collider.tag == "Card_6")
            {
                cardSelected = hit.collider.gameObject.transform.Find("ArcanaSelected").gameObject;
                cardSelected.SetActive(true);
                if (Input.GetMouseButtonDown(0) && InventoryManager.playerSpellbook[minCardID+5] && GetComponent<CardDatabase>().FindCard(minCardID + 5).cardQuant > 0)
                {

                    if (currentDeckCard < Deck.deckSize)
                    {
                        cardUINoise.GetComponent<AudioSource>().Play();
                        GetComponent<CardDatabase>().FindCard(minCardID + 5).cardQuant--;
                        CreateSlab(minCardID+5);
                    }
                    else
                    {
                        cardUIErrorNoise.GetComponent<AudioSource>().Play();
                        system.transform.Find("Text").GetComponent<Text>().text = "Deck is full!";
                        system.SetActive(true);
                    }
                }
                else if (Input.GetMouseButtonDown(0) && !InventoryManager.playerSpellbook[minCardID+5])
                {
                    cardUIErrorNoise.GetComponent<AudioSource>().Play();
                    system.transform.Find("Text").GetComponent<Text>().text = "Card not discovered yet!";
                    system.SetActive(true);
                }

                else if (Input.GetMouseButtonDown(0) && InventoryManager.playerSpellbook[minCardID+5] && GetComponent<CardDatabase>().FindCard(minCardID+5).cardQuant == 0)
                {
                    cardUIErrorNoise.GetComponent<AudioSource>().Play();
                    system.transform.Find("Text").GetComponent<Text>().text = "You don't have any more copies of that card!";
                    system.SetActive(true);
                }

                //Shoud a tooltip render?
                if (InventoryManager.playerSpellbook[minCardID+5])
                {
                    GetToolTip(minCardID+5);
                }

                //Card Crafting
                OpenCrafting(minCardID+5);

                //Runeforge
                OpenRuneForge(minCardID+5);

            }
            else if (hit.collider.tag == "Card_7")
            {
                cardSelected = hit.collider.gameObject.transform.Find("ArcanaSelected").gameObject;
                cardSelected.SetActive(true);
                if (Input.GetMouseButtonDown(0) && InventoryManager.playerSpellbook[minCardID+6] && GetComponent<CardDatabase>().FindCard(minCardID + 6).cardQuant > 0)
                {

                    if (currentDeckCard < Deck.deckSize)
                    {
                        cardUINoise.GetComponent<AudioSource>().Play();
                        GetComponent<CardDatabase>().FindCard(minCardID + 6).cardQuant--;
                        CreateSlab(minCardID+6);
                    }
                    else
                    {
                        cardUIErrorNoise.GetComponent<AudioSource>().Play();
                        system.transform.Find("Text").GetComponent<Text>().text = "Deck is full!";
                        system.SetActive(true);
                    }
                }
                else if (Input.GetMouseButtonDown(0) && !InventoryManager.playerSpellbook[minCardID+6])
                {
                    cardUIErrorNoise.GetComponent<AudioSource>().Play();
                    system.transform.Find("Text").GetComponent<Text>().text = "Card not discovered yet!";
                    system.SetActive(true);
                }

                else if (Input.GetMouseButtonDown(0) && InventoryManager.playerSpellbook[minCardID+6] && GetComponent<CardDatabase>().FindCard(minCardID+6).cardQuant == 0)
                {
                    cardUIErrorNoise.GetComponent<AudioSource>().Play();
                    system.transform.Find("Text").GetComponent<Text>().text = "You don't have any more copies of that card!";
                    system.SetActive(true);
                }

                //Shoud a tooltip render?
                if (InventoryManager.playerSpellbook[minCardID+6])
                {
                    GetToolTip(minCardID+6);
                }

                //Card Crafting
                OpenCrafting(minCardID+6);

                //Runeforge
                OpenRuneForge(minCardID+6);
            }
            else if (hit.collider.tag == "Card_8")
            {
                cardSelected = hit.collider.gameObject.transform.Find("ArcanaSelected").gameObject;
                cardSelected.SetActive(true);
                if (Input.GetMouseButtonDown(0) && InventoryManager.playerSpellbook[minCardID+7] && GetComponent<CardDatabase>().FindCard(minCardID + 7).cardQuant > 0)
                {

                    if (currentDeckCard < Deck.deckSize)
                    {
                        cardUINoise.GetComponent<AudioSource>().Play();
                        GetComponent<CardDatabase>().FindCard(minCardID + 7).cardQuant--;
                        CreateSlab(minCardID+7);
                    }
                    else
                    {
                        cardUIErrorNoise.GetComponent<AudioSource>().Play();
                        system.transform.Find("Text").GetComponent<Text>().text = "Deck is full!";
                        system.SetActive(true);
                    }
                }
                else if (Input.GetMouseButtonDown(0) && !InventoryManager.playerSpellbook[minCardID+7])
                {
                    cardUIErrorNoise.GetComponent<AudioSource>().Play();
                    system.transform.Find("Text").GetComponent<Text>().text = "Card not discovered yet!";
                    system.SetActive(true);
                }

                else if (Input.GetMouseButtonDown(0) && InventoryManager.playerSpellbook[minCardID+7] && GetComponent<CardDatabase>().FindCard(minCardID+7).cardQuant == 0)
                {
                    cardUIErrorNoise.GetComponent<AudioSource>().Play();
                    system.transform.Find("Text").GetComponent<Text>().text = "You don't have any more copies of that card!";
                    system.SetActive(true);
                }

                //Shoud a tooltip render?
                if (InventoryManager.playerSpellbook[minCardID+7])
                {
                    GetToolTip(minCardID+7);
                }

                //Card Crafting
                OpenCrafting(minCardID+7);

                //Runeforge
                OpenRuneForge(minCardID+7);
            }
        }             
        else
        {
            if (cardSelected != null)
            {
                cardSelected.SetActive(false);
            }
        }
    }

    void OpenCrafting(int id)
    {
        if(!disenchantToggle)
        {
            if (Input.GetMouseButtonDown(1) && InventoryManager.playerSpellbook[id] && GetComponent<CardDatabase>().FindCard(id).cardQuant < 5)
            {
                GetComponent<AudioSource>().Play();
                craftingInterface.SetActive(true);
                craftingInterface.transform.Find("Text").GetComponent<Text>().text = "Card Crafting";
                craftingInterface.transform.Find("Arcana_Costs").transform.Find("Text").GetComponent<Text>().text = "Cost:";
                craftingInterface.transform.Find("Crafting_Frame").transform.Find("Text").GetComponent<Text>().text = "How many cards would you like to craft?";
                craftingInterface.transform.Find("Card_Img").GetComponent<Image>().sprite = Resources.Load<Sprite>("Cards/NewSpell/Arcana_" + GetComponent<CardDatabase>().FindCard(id).cardName);
                craftingInterface.transform.Find("Card_Name").GetComponent<Text>().text = GetComponent<CardDatabase>().FindCard(id).cardName;
                craftButton.transform.Find("Text").GetComponent<Text>().text = "Craft!";
                canClick = false;
                numCrafts = 0;
                craftID = id;

                foreach(GameObject crystal in GameObject.FindGameObjectsWithTag("Shop_Rest"))
                {
                    Destroy(crystal);
                }

                //Create Mana Crystals
                for(int i = 0; i < GetComponent<Spell_Database>().FindSpell(id).spellCost; i++)
                {
                    if(GetComponent<Spell_Database>().FindSpell(id).spellCost > 3)
                    {
                        Instantiate(Resources.Load("Cards/NewSpell/Cost_Crystal_Full_Craft"), craftingInterface.transform.Find("Card_Img").Find("Mana_Frame").transform);                      
                    }
                    else
                    {
                        Instantiate(Resources.Load("Cards/NewSpell/Cost_Crystal_Full_Craft"), craftingInterface.transform.Find("Card_Img").Find("Mana_Frame_Small").transform);
                    }
                }

            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (!InventoryManager.playerSpellbook[id])
                {
                    cardUIErrorNoise.GetComponent<AudioSource>().Play();
                    system.transform.Find("Text").GetComponent<Text>().text = "Card not discovered yet!";
                    system.SetActive(true);
                }
                else if (GetComponent<CardDatabase>().FindCard(id).cardQuant == 5)
                {
                    cardUIErrorNoise.GetComponent<AudioSource>().Play();
                    system.transform.Find("Text").GetComponent<Text>().text = "You can't craft anymore of this card!";
                    system.SetActive(true);
                }
            }
        }
        else if(disenchantToggle)
        {
            if(Input.GetMouseButtonDown(1) && InventoryManager.playerSpellbook[id] && GetComponent<CardDatabase>().FindCard(id).cardQuant > 0 && GetTotalCardCount() > 15)
            {
                GetComponent<AudioSource>().Play();
                craftingInterface.SetActive(true);
                craftingInterface.transform.Find("Text").GetComponent<Text>().text = "Card Disenchanting";
                craftingInterface.transform.Find("Arcana_Costs").transform.Find("Text").GetComponent<Text>().text = "Disenchant amount:";
                craftingInterface.transform.Find("Crafting_Frame").transform.Find("Text").GetComponent<Text>().text = "How many cards would you like to disenchant?";
                craftingInterface.transform.Find("Card_Img").GetComponent<Image>().sprite = Resources.Load<Sprite>("Cards/NewSpell/Arcana_" + GetComponent<CardDatabase>().FindCard(id).cardName);
                craftingInterface.transform.Find("Card_Name").GetComponent<Text>().text = GetComponent<CardDatabase>().FindCard(id).cardName;
                craftButton.transform.Find("Text").GetComponent<Text>().text = "Disenchant!";
                canClick = false;
                numCrafts = 0;
                craftID = id;

                foreach (GameObject crystal in GameObject.FindGameObjectsWithTag("Shop_Rest"))
                {
                    Destroy(crystal);
                }

                //Create Mana Crystals
                for (int i = 0; i < GetComponent<Spell_Database>().FindSpell(id).spellCost; i++)
                {
                    if (GetComponent<Spell_Database>().FindSpell(id).spellCost > 3)
                    {
                        Instantiate(Resources.Load("Cards/NewSpell/Cost_Crystal_Full_Craft"), craftingInterface.transform.Find("Card_Img").Find("Mana_Frame").transform);
                    }
                    else
                    {
                        Instantiate(Resources.Load("Cards/NewSpell/Cost_Crystal_Full_Craft"), craftingInterface.transform.Find("Card_Img").Find("Mana_Frame_Small").transform);
                    }
                }
            }
            else if(Input.GetMouseButtonDown(1))
            {
                if(!InventoryManager.playerSpellbook[id])
                {
                    cardUIErrorNoise.GetComponent<AudioSource>().Play();
                    system.transform.Find("Text").GetComponent<Text>().text = "Card not discovered yet!";
                    system.SetActive(true);
                }
                else if(GetComponent<CardDatabase>().FindCard(id).cardQuant == 0)
                {
                    cardUIErrorNoise.GetComponent<AudioSource>().Play();
                    system.transform.Find("Text").GetComponent<Text>().text = "You don't have that card in stock to disenchant!";
                    system.SetActive(true);
                }
                else if(GetTotalCardCount() <= 15)
                {
                    cardUIErrorNoise.GetComponent<AudioSource>().Play();
                    system.transform.Find("Text").GetComponent<Text>().text = "You must have more than 15 cards to disenchant!";
                    system.SetActive(true);
                }

            }
        }
    }

    //Gets the total number fo cards owned
    int GetTotalCardCount()
    {
        int total = 0;

        //Count how many cards are in the deck builder
        for(int i = 0; i < slabList.Count; i++)
        {
            total += slabList[i].GetComponent<Slab>().cardQuant;
        }

        //Count how many cards are in player's stock
        for(int j = 0; j < GetComponent<CardDatabase>().cardData.Count; j++)
        {
            total += GetComponent<CardDatabase>().cardData[j].cardQuant;
        }
        //print(total);
        return total;
    }

    public void OpenRuneForge(int cardID)
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!runeForge.activeInHierarchy && InventoryManager.playerSpellbook[cardID])
            {
                runeForge.GetComponent<Rune_Manager>().cardID = cardID;
                runeFade.SetActive(true);
                runeForge.SetActive(true);
                runeForge.GetComponent<Rune_Manager>().GenerateSockets(cardID);
                runeForge.GetComponent<Rune_Manager>().UpdateStat();
            }
            else
            {
                cardUIErrorNoise.GetComponent<AudioSource>().Play();
                system.SetActive(false);
                system.transform.Find("Text").GetComponent<Text>().text = "Card not discovered yet!";
                system.SetActive(true);
            }
            
        }


    }
}
