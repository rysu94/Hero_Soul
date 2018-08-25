using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    public GameObject alcConfirm;
    public Button yes;
    public Button no;

    public GameObject shopDialogue;
    public GameObject alcOptions;

    public AudioSource click;

    public static bool inUpgrade = false;

    public int potionID;

    //Heal potion
    public GameObject healPotencyBar;
    public GameObject healQuantityBar;
    public Text healPotencyText;
    public Text healQuantityText;

    //Mana Potion
    public GameObject manaPotencyBar;
    public GameObject manaQuantityBar;
    public Text manaPotencyText;
    public Text manaQuantityText;

    //Stamina Potion
    public GameObject stamPotencyBar;
    public GameObject stamQuantityBar;
    public Text stamPotencyText;
    public Text stamQuantityText;
    public GameObject stamUnlock;

    //Stoneskin Potion
    public GameObject stonePotencyBar;
    public GameObject stoneQuantityBar;
    public Text stonePotencyText;
    public Text stoneQuantityText;
    public GameObject stoneUnlock;

    //Strength Potion
    public GameObject strengthPotencyBar;
    public GameObject strengthQuantityBar;
    public Text strengthPotencyText;
    public Text strengthQuantityText;
    public GameObject strengthUnlock;

    //Alacrity Potion
    public GameObject alacrityPotencyBar;
    public GameObject alacrityQuantityBar;
    public Text alacrityPotencyText;
    public Text alacrityQuantityText;
    public GameObject alacrityUnlock;

    //Intellect Potion
    public GameObject intellectPotencyBar;
    public GameObject intellectQuantityBar;
    public Text intellectPotencyText;
    public Text intellectQuantityText;
    public GameObject intellectUnlock;

    //tooltip
    public GameObject tooltip;

    //Tooltip text
    public Text tooltipDesc;
    public GameObject materialCostPanel;
    public GameObject materialPrefab;

    //Buttons
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    public GameObject button6;
    public GameObject button7;
    public GameObject button8;
    public GameObject button9;
    public GameObject button10;
    public GameObject button11;
    public GameObject button12;
    public GameObject button13;
    public GameObject button14;

    public bool potionAdd = false;

    public Text gold;
    public Button back;
    public GameObject alcHUD;

    public Button refill, nextPage, prevPage;
    public GameObject pageObj, potionSelect, upgrade1, upgrade2;
    public int page;
    public Text pageText;

    public GameObject playerInv, playerInvPanel;
    public List<GameObject> slotList = new List<GameObject>();

    // Use this for initialization
    void Start ()
    {
        CheckUnlocks();
        back.onClick.AddListener(backButton);
        refill.onClick.AddListener(RefillPotions);
        nextPage.onClick.AddListener(UpPage);
        prevPage.onClick.AddListener(DownPage);

        page = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        gold.text = InventoryManager.playerGold.ToString();

        UpdatePotions();
        UpdateAdd();

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = Camera.main.transform.position.z;
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit.collider != null && hit.collider.tag == "Potion_Add" && hit.collider.gameObject.GetComponent<PotionUpgrade>().unlocked)
        {
            hit.collider.gameObject.GetComponent<PotionUpgrade>().UpdateCosts();
            tooltip.SetActive(true);
            tooltipDesc.text = hit.collider.gameObject.GetComponent<PotionUpgrade>().tooltip;
        }
        else if(hit.collider != null && hit.collider.tag == "Alc_Equip" && Input.GetMouseButtonDown(0))
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            GetComponent<Image>().sprite = Resources.Load<Sprite>("Inventory/ShopInv");
            potionSelect.SetActive(true);
            upgrade1.SetActive(false);
            upgrade2.SetActive(false);
            pageObj.SetActive(false);
            playerInv.SetActive(false);
        }
        else if(hit.collider != null && hit.collider.tag == "Alc_Upgrage" && Input.GetMouseButtonDown(0))
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            GetComponent<Image>().sprite = Resources.Load<Sprite>("Inventory/ShopInv2");
            potionSelect.SetActive(false);
            pageObj.SetActive(true);
            UpdatePage();
            playerInv.SetActive(true);
            UpdateInv();
        }
        else
        {
            tooltip.SetActive(false);
        }


        if (Input.GetMouseButtonDown(0) && !inUpgrade)
        {

            //Unlock Stamina Potions
            if (hit.collider != null && hit.collider.tag == "PotionUnlock")
            {
                click.Play();
                shopDialogue.SetActive(true);
                shopDialogue.GetComponent<ShopDialogue>().Clear();
                shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Interested in the stamina potion? This potion will regenerate your stamina when used. I'll teach you the recipe for it for 500 Gold. How's that sound?", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo",0.9f, -1));
                shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
                shopDialogue.GetComponent<ShopDialogue>().waitingDecision = true;
                alcConfirm.SetActive(true);
                alcOptions.SetActive(false);
                inUpgrade = true;
                potionAdd = false;
                potionID = 3;
            }
            //Unlocks stoneskin
            else if (hit.collider != null && hit.collider.tag == "PotionUnlock2")
            {
                click.Play();
                shopDialogue.SetActive(true);
                shopDialogue.GetComponent<ShopDialogue>().Clear();
                shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Interested in the stoneskin potion? This potion will grant you bonus defense when used. I'll teach you the recipe for it for 1000 Gold. How's that sound?", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo",0.9f, -1));
                shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
                shopDialogue.GetComponent<ShopDialogue>().waitingDecision = true;
                alcConfirm.SetActive(true);
                alcOptions.SetActive(false);
                inUpgrade = true;
                potionAdd = false;
                potionID = 4;
            }
            //unlock strength
            else if (hit.collider != null && hit.collider.tag == "PotionUnlock3")
            {
                click.Play();
                shopDialogue.SetActive(true);
                shopDialogue.GetComponent<ShopDialogue>().Clear();
                shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Interested in the strength potion? This potion will grant you bonus strength when used. I'll teach you the recipe for it for 750 Gold. How's that sound?", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo",0.9f, -1));
                shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
                shopDialogue.GetComponent<ShopDialogue>().waitingDecision = true;
                alcConfirm.SetActive(true);
                alcOptions.SetActive(false);
                inUpgrade = true;
                potionAdd = false;
                potionID = 5;
            }
            //unlock alacrity
            else if (hit.collider != null && hit.collider.tag == "PotionUnlock4")
            {
                click.Play();
                shopDialogue.SetActive(true);
                shopDialogue.GetComponent<ShopDialogue>().Clear();
                shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Interested in the alacrity potion? This potion will grant you bonus dexterity and movement speed when used. I'll teach you the recipe for it for 1000 Gold. How's that sound?", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo",0.9f, -1));
                shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
                shopDialogue.GetComponent<ShopDialogue>().waitingDecision = true;
                alcConfirm.SetActive(true);
                alcOptions.SetActive(false);
                inUpgrade = true;
                potionAdd = false;
                potionID = 6;
            }
            else if(hit.collider != null && hit.collider.tag == "PotionUnlock5")
            {
                click.Play();
                shopDialogue.SetActive(true);
                shopDialogue.GetComponent<ShopDialogue>().Clear();
                shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Interested in the intellect potion? This potion will grant you bonus intelligence when used. I'll teach you the recipe for it for 750 Gold. How's that sound?", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo",0.9f, -1));
                shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
                shopDialogue.GetComponent<ShopDialogue>().waitingDecision = true;
                alcConfirm.SetActive(true);
                alcOptions.SetActive(false);
                inUpgrade = true;
                potionAdd = false;
                potionID = 7;
            }

            else if(hit.collider != null && hit.collider.tag == "Potion_Add" && hit.collider.gameObject.GetComponent<PotionUpgrade>().unlocked)
            {
                click.Play();
                shopDialogue.SetActive(true);
                hit.collider.gameObject.GetComponent<Animator>().Play("Button");
                if (hit.collider.gameObject.GetComponent<PotionUpgrade>().upgradeID == 1)
                {
                    shopDialogue.GetComponent<ShopDialogue>().Clear();
                    shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Would you like to upgrade the potency of your healing potion?", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo", 0.9f, -1));
                    shopDialogue.GetComponent<ShopDialogue>().StartDialogue();                  
                }
                else if (hit.collider.gameObject.GetComponent<PotionUpgrade>().upgradeID == 2)
                {
                    shopDialogue.GetComponent<ShopDialogue>().Clear();
                    shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Would you like to upgrade your healing potion capacity?", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo", 0.9f, -1));
                    shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
                }
                else if (hit.collider.gameObject.GetComponent<PotionUpgrade>().upgradeID == 3)
                {
                    shopDialogue.GetComponent<ShopDialogue>().Clear();
                    shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Would you like to upgrade the potency of you mana potion?", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo",0.9f, -1));
                    shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
                }
                else if (hit.collider.gameObject.GetComponent<PotionUpgrade>().upgradeID == 4)
                {
                    shopDialogue.GetComponent<ShopDialogue>().Clear();
                    shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Would you like to upgrade your mana potion capacity?", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo",0.9f, -1));
                    shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
                }
                else if (hit.collider.gameObject.GetComponent<PotionUpgrade>().upgradeID == 5)
                {
                    shopDialogue.GetComponent<ShopDialogue>().Clear();
                    shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Would you like to upgrade the potency of your stamina potions?", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo",0.9f, -1));
                    shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
                }
                else if (hit.collider.gameObject.GetComponent<PotionUpgrade>().upgradeID == 6)
                {
                    shopDialogue.GetComponent<ShopDialogue>().Clear();
                    shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Would you like to upgrade your stamina potion capacity?", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo",0.9f, -1));
                    shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
                }
                else if (hit.collider.gameObject.GetComponent<PotionUpgrade>().upgradeID == 7)
                {
                    shopDialogue.GetComponent<ShopDialogue>().Clear();
                    shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Would you like to upgrade the potency of your stoneskin potion?", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo",0.9f, -1));
                    shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
                }
                else if (hit.collider.gameObject.GetComponent<PotionUpgrade>().upgradeID == 8)
                {
                    shopDialogue.GetComponent<ShopDialogue>().Clear();
                    shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Would you like to upgrade your stoneskin potion capacity?", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo",0.9f, -1));
                    shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
                }
                else if (hit.collider.gameObject.GetComponent<PotionUpgrade>().upgradeID == 9)
                {
                    shopDialogue.GetComponent<ShopDialogue>().Clear();
                    shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Would you like to upgrade the potency of your strength potion?", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo",0.9f, -1));
                    shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
                }
                else if (hit.collider.gameObject.GetComponent<PotionUpgrade>().upgradeID == 10)
                {
                    shopDialogue.GetComponent<ShopDialogue>().Clear();
                    shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Would you like to upgrade your strength potion capacity?", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo",0.9f, -1));
                    shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
                }
                else if (hit.collider.gameObject.GetComponent<PotionUpgrade>().upgradeID == 11)
                {
                    shopDialogue.GetComponent<ShopDialogue>().Clear();
                    shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Would you like to upgrade the potency of your alacrity potion?", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo",0.9f, -1));
                    shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
                }
                else if (hit.collider.gameObject.GetComponent<PotionUpgrade>().upgradeID == 12)
                {
                    shopDialogue.GetComponent<ShopDialogue>().Clear();
                    shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Would you like to upgrade your alacrity potion capacity?", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo",0.9f, -1));
                    shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
                }
                else if (hit.collider.gameObject.GetComponent<PotionUpgrade>().upgradeID == 13)
                {
                    shopDialogue.GetComponent<ShopDialogue>().Clear();
                    shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Would you like to upgrade the potency of your intellect potion?", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo",0.9f, -1));
                    shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
                }
                else if (hit.collider.gameObject.GetComponent<PotionUpgrade>().upgradeID == 14)
                {
                    shopDialogue.GetComponent<ShopDialogue>().Clear();
                    shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Would you like to upgrade your intellect potion capacity?", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo",0.9f, -1));
                    shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
                }
                alcConfirm.SetActive(true);
                alcOptions.SetActive(false);
                inUpgrade = true;
                potionAdd = true;
                potionID = hit.collider.gameObject.GetComponent<PotionUpgrade>().upgradeID;
            }
        }

        //Potion unlock confirm/deny
        else if(Input.GetMouseButtonDown(0) && inUpgrade)
        {
            if(hit.collider != null && hit.collider.tag == "Shop_Yes" && !potionAdd)
            {
                click.Play();
                shopDialogue.GetComponent<ShopDialogue>().waitingDecision = false;
                GameObject.Find("PurchaseNoise").GetComponent<AudioSource>().Play();
                //stam potion
                if (potionID == 3)
                {
                    shopDialogue.GetComponent<ShopDialogue>().Clear();
                    shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Thank you for your purchase!", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo",0.9f, -1));
                    shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Can I help you with anything else?", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo",0.9f, -1));
                    shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
                    PotionController.staminaPotionAmount = 50;
                    PotionController.staminaPotionMax = 1;
                    PotionController.staminaPotionCurrent = 1;
                    button1.GetComponent<PotionUpgrade>().unlocked = true;
                    button2.GetComponent<PotionUpgrade>().unlocked = true;
                }
                //stone potion
                else if(potionID == 4)
                {
                    shopDialogue.GetComponent<ShopDialogue>().Clear();
                    shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Thank you for your purchase!", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo", 0.9f, -1));
                    shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Can I help you with anything else?", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo",0.9f, -1));
                    shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
                    PotionController.stonePotionAmount = 1;
                    PotionController.stonePotionMax = 1;
                    PotionController.stonePotionCurrent = 1;
                    button3.GetComponent<PotionUpgrade>().unlocked = true;
                    button4.GetComponent<PotionUpgrade>().unlocked = true;
                }
                //str potion
                else if(potionID == 5)
                {
                    shopDialogue.GetComponent<ShopDialogue>().Clear();
                    shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Thank you for your purchase!", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo",0.9f, -1));
                    shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Can I help you with anything else?", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo",0.9f, -1));
                    shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
                    PotionController.strengthPotionAmount = 1;
                    PotionController.strengthPotionMax = 1;
                    PotionController.strengthPotionCurrent = 1;
                    button5.GetComponent<PotionUpgrade>().unlocked = true;
                    button6.GetComponent<PotionUpgrade>().unlocked = true;
                }
                //ala potion
                else if(potionID == 6)
                {
                    shopDialogue.GetComponent<ShopDialogue>().Clear();
                    shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Thank you for your purchase!", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo",0.9f, -1));
                    shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Can I help you with anything else?", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo",0.9f, -1));
                    shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
                    PotionController.alacrityPotionAmount = 1;
                    PotionController.alacrityPotionMax = 1;
                    PotionController.alacrityPotionCurrent = 1;
                    button7.GetComponent<PotionUpgrade>().unlocked = true;
                    button8.GetComponent<PotionUpgrade>().unlocked = true;
                }
                //int potion
                else if(potionID == 7)
                {
                    shopDialogue.GetComponent<ShopDialogue>().Clear();
                    shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Thank you for your purchase!", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo",0.9f, -1));
                    shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Can I help you with anything else?", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo",0.9f, -1));
                    shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
                    PotionController.intellectPotionAmount = 1;
                    PotionController.intellectPotionMax = 1;
                    PotionController.intellectPotionCurrent = 1;
                    button9.GetComponent<PotionUpgrade>().unlocked = true;
                    button10.GetComponent<PotionUpgrade>().unlocked = true;
                }
                alcConfirm.SetActive(false);
                //alcOptions.SetActive(true);
                inUpgrade = false;
                CheckUnlocks();
            }
            else if(hit.collider != null && hit.collider.tag == "Shop_No")
            {
                click.Play();
                shopDialogue.GetComponent<ShopDialogue>().waitingDecision = false;
                shopDialogue.GetComponent<ShopDialogue>().Clear();
                shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Can I help you with anything else?", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo", 0.9f,-1));
                shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
                alcConfirm.SetActive(false);
                //alcOptions.SetActive(true);
                inUpgrade = false;
                potionAdd = false;
            }

            else if(hit.collider != null && hit.collider.tag == "Shop_Yes" && potionAdd)
            {
                click.Play();

                //heal potency
                if(potionID == 1)
                {
                    PotionController.healthPotionAmount += 50;
                }
                //heal quant
                else if(potionID == 2)
                {
                    PotionController.healthPotionMax += 1;
                }
                //mana potency
                else if(potionID == 3)
                {
                    PotionController.manaPotionAmount += 1;
                }
                //mana quant
                else if(potionID == 4)
                {
                    PotionController.manaPotionMax += 1;
                }
                //stam pot
                else if(potionID == 5)
                {
                    PotionController.staminaPotionAmount += 50;
                }
                //stam quant
                else if(potionID == 6)
                {
                    PotionController.staminaPotionMax += 1;
                }
                //stone pot
                else if(potionID == 7)
                {
                    PotionController.stonePotionAmount += 1;
                }
                //stone quant
                else if(potionID == 8)
                {
                    PotionController.stonePotionMax += 1;
                }
                //str pot
                else if(potionID == 9)
                {
                    PotionController.strengthPotionAmount += 1;
                }
                //str quant
                else if(potionID == 10)
                {
                    PotionController.strengthPotionMax += 1;
                }
                //ala pot
                else if(potionID == 11)
                {
                    PotionController.alacrityPotionAmount += 1;
                }
                //ala quant
                else if(potionID == 12)
                {
                    PotionController.alacrityPotionMax += 1;
                }
                //int pot
                else if(potionID == 13)
                {
                    PotionController.intellectPotionAmount += 1;
                }
                //int quant
                else if(potionID == 14)
                {
                    PotionController.intellectPotionMax += 1;
                }

                shopDialogue.GetComponent<ShopDialogue>().Clear();
                shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Your potion has been upgraded.", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo",0.9f, -1));
                shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Can I help you with anything else?", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo",0.9f, -1));
                shopDialogue.GetComponent<ShopDialogue>().StartDialogue();

                UpdatePotions();
                alcConfirm.SetActive(false);
                //alcOptions.SetActive(true);
                inUpgrade = false;
                potionAdd = false;
            }

        }
    }

    //checks which potions have been unlocked
    void CheckUnlocks()
    {
        //Check the unlocks
        if (PotionController.staminaPotionMax > 0)
        {
            stamUnlock.SetActive(false);
        }
        if (PotionController.stonePotionMax > 0)
        {
            stoneUnlock.SetActive(false);
        }
        if (PotionController.strengthPotionMax > 0)
        {
            strengthUnlock.SetActive(false);
        }
        if (PotionController.alacrityPotionMax > 0)
        {
            alacrityUnlock.SetActive(false);
        }
        if (PotionController.intellectPotionMax > 0)
        {
            intellectUnlock.SetActive(false);
        }
    }

    //updates the numbers and bars of each potion
    void UpdatePotions()
    {
        RectTransform barRect;
        float rectPercent;

        //health potion
        healPotencyText.text = PotionController.healthPotionAmount.ToString() + "/300";
        barRect = healPotencyBar.transform as RectTransform;
        rectPercent = (float)PotionController.healthPotionAmount / 300f;
        barRect.sizeDelta = new Vector2(125.17f * rectPercent, barRect.sizeDelta.y);

        healQuantityText.text = PotionController.healthPotionMax.ToString() + "/10";
        barRect = healQuantityBar.transform as RectTransform;
        rectPercent = (float)PotionController.healthPotionMax / 10f;
        barRect.sizeDelta = new Vector2(125.17f * rectPercent, barRect.sizeDelta.y);

        //mana potion
        manaPotencyText.text = PotionController.manaPotionAmount.ToString() + "/12";
        barRect = manaPotencyBar.transform as RectTransform;
        rectPercent = (float)PotionController.manaPotionAmount / 12f;
        barRect.sizeDelta = new Vector2(125.17f * rectPercent, barRect.sizeDelta.y);

        manaQuantityText.text = PotionController.manaPotionMax.ToString() + "/10";
        barRect = manaQuantityBar.transform as RectTransform;
        rectPercent = (float)PotionController.manaPotionMax / 10f;
        barRect.sizeDelta = new Vector2(125.17f * rectPercent, barRect.sizeDelta.y);

        //stam potion
        stamPotencyText.text = PotionController.staminaPotionAmount.ToString() + "/300";
        barRect = stamPotencyBar.transform as RectTransform;
        rectPercent = (float)PotionController.staminaPotionAmount / 300f;
        barRect.sizeDelta = new Vector2(125.17f * rectPercent, barRect.sizeDelta.y);

        stamQuantityText.text = PotionController.staminaPotionMax.ToString() + "/10";
        barRect = stamQuantityBar.transform as RectTransform;
        rectPercent = (float)PotionController.staminaPotionMax / 10f;
        barRect.sizeDelta = new Vector2(125.17f * rectPercent, barRect.sizeDelta.y);

        //stoneskin
        stonePotencyText.text = (PotionController.stonePotionAmount * 10).ToString() + "/50";
        barRect = stonePotencyBar.transform as RectTransform;
        rectPercent = ((float)PotionController.stonePotionAmount * 10) / 50f;
        barRect.sizeDelta = new Vector2(125.17f * rectPercent, barRect.sizeDelta.y);

        stoneQuantityText.text = PotionController.stonePotionMax.ToString() + "/5";
        barRect = stoneQuantityBar.transform as RectTransform;
        rectPercent = (float)PotionController.stonePotionMax / 5f;
        barRect.sizeDelta = new Vector2(125.17f * rectPercent, barRect.sizeDelta.y);

        //strength
        strengthPotencyText.text = (PotionController.strengthPotionAmount * 10).ToString() + "/50";
        barRect = strengthPotencyBar.transform as RectTransform;
        rectPercent = ((float)PotionController.strengthPotionAmount * 10) / 50f;
        barRect.sizeDelta = new Vector2(125.17f * rectPercent, barRect.sizeDelta.y);

        strengthQuantityText.text = PotionController.strengthPotionMax.ToString() + "/5";
        barRect = strengthQuantityBar.transform as RectTransform;
        rectPercent = (float)PotionController.strengthPotionMax / 5f;
        barRect.sizeDelta = new Vector2(125.17f * rectPercent, barRect.sizeDelta.y);

        //alacrity
        alacrityPotencyText.text = (PotionController.alacrityPotionAmount * 5).ToString() + "/25";
        barRect = alacrityPotencyBar.transform as RectTransform;
        rectPercent = ((float)PotionController.alacrityPotionAmount * 5) / 25f;
        barRect.sizeDelta = new Vector2(125.17f * rectPercent, barRect.sizeDelta.y);

        alacrityQuantityText.text = PotionController.alacrityPotionMax.ToString() + "/5";
        barRect = alacrityQuantityBar.transform as RectTransform;
        rectPercent = (float)PotionController.alacrityPotionMax / 5f;
        barRect.sizeDelta = new Vector2(125.17f * rectPercent, barRect.sizeDelta.y);

        //intellect
        intellectPotencyText.text = (PotionController.intellectPotionAmount * 10).ToString() + "/50";
        barRect = intellectPotencyBar.transform as RectTransform;
        rectPercent = ((float)PotionController.intellectPotionAmount * 10) / 50f;
        barRect.sizeDelta = new Vector2(125.17f * rectPercent, barRect.sizeDelta.y);

        intellectQuantityText.text = PotionController.intellectPotionMax.ToString() + "/5";
        barRect = intellectQuantityBar.transform as RectTransform;
        rectPercent = (float)PotionController.intellectPotionMax / 5f;
        barRect.sizeDelta = new Vector2(125.17f * rectPercent, barRect.sizeDelta.y);
    }

    void UpdateAdd()
    {
        if (PotionController.healthPotionAmount >= 300)
        {
            button11.SetActive(false);
        }
        if (PotionController.healthPotionMax >= 10)
        {
            button12.SetActive(false);
        }
        if (PotionController.manaPotionAmount >= 12)
        {
            button13.SetActive(false);
        }
        if(PotionController.manaPotionMax >= 10)
        {
            button14.SetActive(false);
        }
        if(PotionController.staminaPotionAmount >= 300)
        {
            button1.SetActive(false);
        }
        if(PotionController.staminaPotionMax >= 10)
        {
            button2.SetActive(false);
        }
        if(PotionController.stonePotionAmount >= 5)
        {
            button3.SetActive(false);
        }
        if(PotionController.stonePotionMax >= 5)
        {
            button4.SetActive(false);
        }
        if(PotionController.strengthPotionAmount >= 5)
        {
            button5.SetActive(false);
        }
        if(PotionController.strengthPotionMax >= 5)
        {
            button6.SetActive(false);
        }
        if(PotionController.alacrityPotionAmount >= 5)
        {
            button7.SetActive(false);
        }
        if(PotionController.alacrityPotionMax >= 5)
        {
            button8.SetActive(false);
        }
        if(PotionController.intellectPotionAmount >= 5)
        {
            button9.SetActive(false);
        }
        if(PotionController.intellectPotionMax >= 5)
        {
            button10.SetActive(false);
        }
    }
    void backButton()
    {
        GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
        GameObject.Find("WipeScreen").GetComponent<Animator>().Play("FadeIn");
        TestCharController.inDialogue = false;
        playerInv.SetActive(false);
        alcHUD.SetActive(false);
    }

    void RefillPotions()
    {
        if(!inUpgrade)
        {
            GameObject.Find("PotionNoise").GetComponent<AudioSource>().Play();
            shopDialogue.SetActive(true);
            shopDialogue.GetComponent<ShopDialogue>().Clear();
            shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Your potions have been refilled.", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo", 0.9f, -1));
            shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
            PotionController.healthPotionCurrent = PotionController.healthPotionMax;
            PotionController.manaPotionCurrent = PotionController.manaPotionMax;
            PotionController.staminaPotionCurrent = PotionController.staminaPotionMax;
            PotionController.stonePotionCurrent = PotionController.stonePotionMax;
            PotionController.strengthPotionCurrent = PotionController.strengthPotionMax;
            PotionController.alacrityPotionCurrent = PotionController.alacrityPotionMax;
            PotionController.intellectPotionCurrent = PotionController.intellectPotionMax;
        }
    }

    void UpPage()
    {
        page++;
        if(page > 1)
        {
            page = 1;
            GameObject.Find("ErrorNoise").GetComponent<AudioSource>().Play();
        }
        else
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
        }
        UpdatePage();
    }

    void DownPage()
    {
        page--;
        if(page < 0)
        {
            page = 0;
            GameObject.Find("ErrorNoise").GetComponent<AudioSource>().Play();
        }
        else
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
        }
        UpdatePage();
    }

    void UpdatePage()
    {
        pageText.text = "Page " + (page+1);
        if (page == 0)
        {
            upgrade1.SetActive(true);
            upgrade2.SetActive(false);
        }
        else
        {
            upgrade1.SetActive(false);
            upgrade2.SetActive(true);
        }
    }

    void UpdateInv()
    {
        for(int i = 0; i < slotList.Count; i++)
        {
            Destroy(slotList[i]);
        }
        for(int i = 0; i < InventoryManager.playerInventory.Length; i++)
        {
            GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Inventory/Alc_Slot"), playerInvPanel.transform);
            tempObj.transform.Find("Item_IMG").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + InventoryManager.playerInventory[i].itemIconName);
            if(InventoryManager.playerInventory[i].itemQuantity > 0)
            {
                tempObj.transform.Find("Item_Num").GetComponent<Text>().text = InventoryManager.playerInventory[i].itemQuantity.ToString();
            }
            else
            {
                tempObj.transform.Find("Item_Num").GetComponent<Text>().text = "";
            }
            slotList.Add(tempObj);         
        }
    }
}
