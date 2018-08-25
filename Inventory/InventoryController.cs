using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static bool inInv = false;

    //Player Inventory
    public static GameObject inv;
    public static bool invToggle = false;
    public AudioSource invSound;
    public static Image invImage;

    //Player Equipment
    public static GameObject equip;
    public static bool equipToggle = false;

    //Player Deck
    public static GameObject deck;
    public static bool deckToggle = false;

    //Player Stats
    public static GameObject stats;
    public static bool statToggle = false;

    //Get the Inv Panel
    public GameObject slotPanel;
    public Text useText;

    //Inventory slot prefab
    public GameObject invSlot;


    //Slot List
    public List<GameObject> slotList = new List<GameObject>();

    //Equip List
    public GameObject[] equipList = new GameObject[7];

    //Inventory Capacity
    public static int invCapacity = 36;

    //Player Gold
    public Text playerGold;

    public GameObject spellBook;
    public static bool inSpellbook = false;

    //Item Menu
    public GameObject invMenu;
    public static bool invMenuToggle = false;

    //stash
    public GameObject stash;

    //treasure/arcana tab
    public GameObject treasure;
    public Button closeButton;
    public GameObject talent;

    //Gamepad Controls
    public static int padX, padY;
    public static bool padActive = false;

    public static int selectedIndex;
    public GameObject selectedSlot;

    public static int selectTab = 0;
    public static int openTab = 0;

    public GameObject helpPanel;

    public static bool invHUDMade = false;
    public int invHUDselection;
    public GameObject invHUDSlot;

    void Awake()
    {
        inv = GameObject.Find("Inventory");
        invImage = inv.GetComponent<Image>();
        invSound = GetComponent<AudioSource>();
        equip = GameObject.Find("Equipment");
        deck = GameObject.Find("Arcana_Deck");
        stats = GameObject.Find("Skill_Tree");
        slotPanel = GameObject.Find("Slot Panel");

    }

    // Use this for initialization
    void Start ()
    {
        if(Soul_Manager.soulEnabled)
        {
            talent.SetActive(true);
        }

        //Close Button
        closeButton.onClick.AddListener(CloseInventory);
        
        //Generate the slots
        for (int i = 0; i < invCapacity; i++)
        {
            slotList.Add(Instantiate(invSlot));
            slotList[i].transform.SetParent(GameObject.Find("Slot Panel").transform, false);
            slotList[i].GetComponent<Slot>().slotNumber = i;
        }
        
        //Fill the Equip Array
        for(int i = 0; i < equipList.Length; i++)
        {
            equipList[i].GetComponent<EquipSlot>().slotImage = Resources.Load<Sprite>("Item/" + InventoryManager.playerEquipment[i].itemIconName);
        }
        

        //checks the toggle of the inventory
        if (!invToggle)
        {
            inv.gameObject.SetActive(false);
            equip.gameObject.SetActive(false);
            deck.gameObject.SetActive(false);
            stats.gameObject.SetActive(false);
            spellBook.SetActive(false);
            InventoryController.inSpellbook = false;

        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = Camera.main.transform.position.z;
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if(hit.collider != null && hit.collider.tag == "Close_Button")
        {
            if(Input.GetMouseButtonDown(0))
            {
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                StartCoroutine(CloseInv()); 
            }
        }

        //Inventory Slot
        else if(hit.collider != null && hit.collider.tag == "Inv_Slot")
        {
            if((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && invMenuToggle && !InvMenu.choiceHover && !InvMenu.inMenu)
            {
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                invMenu.SetActive(false);
                invMenuToggle = false;
            }

            else if(Input.GetMouseButtonDown(1) && hit.collider.gameObject.GetComponent<Slot>().itemID != 0 && !invMenuToggle && hit.collider.tag == "Inv_Slot")
            {
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                invMenu.SetActive(true);
                invMenu.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 5));
                invMenu.transform.position = new Vector3(invMenu.transform.position.x - .35f, invMenu.transform.position.y - .25f, invMenu.transform.position.z);
                invMenuToggle = true;

                //Change inv menu 1st option
                if(hit.collider.gameObject.GetComponent<Slot>().itemType == "Weapon" || hit.collider.gameObject.GetComponent<Slot>().itemType == "Head" ||
                    hit.collider.gameObject.GetComponent<Slot>().itemType == "Neck" || hit.collider.gameObject.GetComponent<Slot>().itemType == "Body" ||
                    hit.collider.gameObject.GetComponent<Slot>().itemType == "Ring" || hit.collider.gameObject.GetComponent<Slot>().itemType == "Feet")
                {
                    //Equip
                    invMenu.GetComponent<InvMenu>().optionID = 1;
                    useText.text = "Equip";
                }

                else if(hit.collider.gameObject.GetComponent<Slot>().GetComponent<Slot>().itemType == "Consumable")
                {
                    //Use
                    invMenu.GetComponent<InvMenu>().optionID = 2;
                    useText.text = "Use";
                }

                else if(hit.collider.gameObject.GetComponent<Slot>().GetComponent<Slot>().itemType == "Crafting Material" || hit.collider.gameObject.GetComponent<Slot>().GetComponent<Slot>().itemType == "Treasure")
                {
                    //Inspect
                    invMenu.GetComponent<InvMenu>().optionID = 3;
                    useText.text = "Inspect";
                }

                //Send information to the inv menu
                invMenu.GetComponent<InvMenu>().slotNumber = hit.collider.GetComponent<Slot>().slotNumber;
                invMenu.GetComponent<InvMenu>().slotImage = hit.collider.GetComponent<Slot>().slotImage;
                invMenu.GetComponent<InvMenu>().itemAmount = hit.collider.GetComponent<Slot>().itemAmount;
                invMenu.GetComponent<InvMenu>().itemID = hit.collider.GetComponent<Slot>().itemID;
                invMenu.GetComponent<InvMenu>().itemType = hit.collider.GetComponent<Slot>().itemType;
            }

        }
        
        if ((Input.GetKeyDown(KeyCode.I) || InputManager.J_Tab()) && !TestCharController.inDialogue && !TestCharController.inTreasure)
        {
            if (invToggle)
            {
                CloseInventory();
            }
            else if (!invToggle)
            {
                OpenInv();
            }
        }

        if(inInv && GameController.xbox360Enabled() && !InvMenu.inMenu && !TestCharController.inTreasure)
        {
            ToggleTabs();
            UpdatePadSelect();
            ToggleHelp();
            SelectItem();
        }

        


    }
    public void OpenInv()
    {
        selectTab = 0;
        padX = 0;
        padY = 0;
        inInv = true;
        Sprite tempSprite = Resources.Load<Sprite>("Inventory/Inventory");
        InventoryController.invImage.sprite = tempSprite;
        //InventoryController.equipToggle = true;
        //equip.SetActive(true);
        invToggle = true;
        inv.SetActive(true);
        UpdateInventory();
        playerGold.text = InventoryManager.playerGold.ToString();
        UpdateEquip();
        helpPanel.SetActive(true);
    }
    public void CloseInventory()
    {
        GameObject.Find("EquipNoise").GetComponent<AudioSource>().Play();
        inInv = false;
        invToggle = false;
        inv.SetActive(false);
        equip.SetActive(false);
        deck.SetActive(false);
        stats.SetActive(false);
        spellBook.SetActive(false);
        equipToggle = false;
        statToggle = false;
        deckToggle = false;
        InventoryController.inSpellbook = false;
        stash.SetActive(false);
        treasure.SetActive(false);
        invMenu.SetActive(false);
        invMenuToggle = false;
        helpPanel.SetActive(false);
        invHUDMade = false;
    }


    public int GetItemID()
    {
        return 0;
    }

    public void UpdateInventory()
    {
        for(int i = 0; i < invCapacity; i++)
        {
            GameObject tempObj = slotList[i].transform.Find("Item_IMG").gameObject;
            Image tempInvImage = tempObj.GetComponent<Image>();
            Sprite tempSprite = Resources.Load<Sprite>("Item/" + InventoryManager.playerInventory[i].itemIconName);
            tempInvImage.sprite = tempSprite;
            slotList[i].GetComponent<Slot>().slotImage = tempSprite;

            slotList[i].GetComponent<Slot>().itemID = InventoryManager.playerInventory[i].itemID;

            GameObject tempTextObj = slotList[i].transform.Find("Item_Num").gameObject;
            Text tempText = tempTextObj.GetComponent<Text>();
            
            //Find out how many of something player has
            int tempInt = InventoryManager.playerInventory[i].itemQuantity;
            if (tempInt > 0 && InventoryManager.playerInventory[i].stackable)
            {
                tempText.text = tempInt.ToString();
                slotList[i].GetComponent<Slot>().itemAmount = tempInt;
            }
            else if(tempInt > 0 && !InventoryManager.playerInventory[i].stackable)
            {
                tempText.text = "";
                slotList[i].GetComponent<Slot>().itemAmount = tempInt;
            }
            else if (tempInt == 0)
            {
                tempText.text = "";
                slotList[i].GetComponent<Slot>().itemAmount = 0;
            }
            else if (tempInt < 0)
            {
                tempText.text = "ERROR";
                slotList[i].GetComponent<Slot>().itemAmount = -1;
            }

            //item type
            slotList[i].GetComponent<Slot>().itemType = InventoryManager.playerInventory[i].itemType;
        }
    }

    public void UpdateEquip()
    {
        for (int i = 0; i < equipList.Length; i++)
        {
            GameObject tempObj = equipList[i].transform.Find("Item_IMG").gameObject;
            Image tempInvImage = tempObj.GetComponent<Image>();
            Sprite tempSprite = Resources.Load<Sprite>("Item/" + InventoryManager.playerEquipment[i].itemIconName);
            tempInvImage.sprite = tempSprite;

            equipList[i].GetComponent<EquipSlot>().slotImage = tempSprite;
            equipList[i].GetComponent<EquipSlot>().itemID = InventoryManager.playerEquipment[i].itemID;
            equipList[i].GetComponent<EquipSlot>().itemType = InventoryManager.playerEquipment[i].itemType;
        }
    }

   
    IEnumerator CloseInv()
    {
        yield return new WaitForSeconds(.1f);
        inInv = false;
        invToggle = false;
        inv.SetActive(false);
        equip.SetActive(false);
        deck.SetActive(false);
        stats.SetActive(false);
        spellBook.SetActive(false);
        equipToggle = false;
        statToggle = false;
        deckToggle = false;
        InventoryController.inSpellbook = false;
        helpPanel.SetActive(false);
        invHUDMade = false;

    }

    public void ToggleTabs()
    {
        if((InputManager.L_Bumper() || InputManager.R_Bumper()) && !invHUDMade)
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            padX = 0;
            padY = 0;
            if (selectTab == 0)
            {
                if(equip.activeInHierarchy)
                {
                    selectTab = 1;
                }
                else if(deck.activeInHierarchy)
                {
                    selectTab = 2;
                }
                else if(stats.activeInHierarchy)
                {
                    selectTab = 3;
                }
                print(selectTab);
            }
            else
            {
                selectTab = 0;
            }
        }
    }

    public void UpdatePadSelect()
    {
        if(selectTab == 0 && !invHUDMade && !InvMenu.inMenu)
        {
            if (InputManager.MainHorizontal() > .5f && !padActive)
            {
                padActive = true;
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                padX++;
                if (padX > 5)
                {
                    padX = 0;
                }
                StartCoroutine(PadBuffer());
            }
            else if (InputManager.MainHorizontal() < -.5f && !padActive)
            {
                padActive = true;
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                padX--;
                if (padX < 0)
                {
                    padX = 5;
                }
                StartCoroutine(PadBuffer());
            }
            else if (InputManager.MainVertical() > .5f && !padActive)
            {
                padActive = true;
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                padY--;
                if (padY < 0)
                {
                    padY = 5;
                }
                StartCoroutine(PadBuffer());
            }
            else if (InputManager.MainVertical() < -.5f && !padActive)
            {
                padActive = true;
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                padY++;
                if (padY > 5)
                {
                    padY = 0;
                }
                StartCoroutine(PadBuffer());
            }

            //Display Selection
            Destroy(selectedSlot);

            //Adjusted index of the selected slot
            selectedIndex = (padY * 6) + padX;

            selectedSlot = Instantiate(Resources.Load("Prefabs/Inventory/Slot_Select"), slotList[selectedIndex].transform) as GameObject;
        }

        //Armor
        else if(selectTab == 1)
        {
            if((InputManager.MainHorizontal() > .5f || InputManager.MainVertical() < -.5f) && !padActive)
            {
                padActive = true;
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                padX++;
                if(padX > 6)
                {
                    padX = 0;
                }
                StartCoroutine(PadBuffer());
            }
            else if((InputManager.MainHorizontal() < -.5f || InputManager.MainVertical() > .5f) && !padActive)
            {
                padActive = true;
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                padX--;
                if(padX < 0)
                {
                    padX = 6;
                }
                StartCoroutine(PadBuffer());
            }

            //Display Selection
            Destroy(selectedSlot);

            //Adjusted index of the selected slot
            selectedIndex = padX;

            selectedSlot = Instantiate(Resources.Load("Prefabs/Inventory/Slot_Select"), equipList[selectedIndex].transform) as GameObject;
        }
        //Arcana
        else if(selectTab == 2)
        {
            if ((InputManager.MainHorizontal() > .5f || InputManager.MainVertical() > .5f) && !padActive)
            {
                padActive = true;
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                padX--;
                if (padX < 0)
                {
                    padX = ArcanaDeck_Manager.cardSlabList.Count - 1;
                }
                StartCoroutine(PadBuffer());
            }
            else if ((InputManager.MainHorizontal() < -.5f || InputManager.MainVertical() < -.5f) && !padActive)
            {
                padActive = true;
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                padX++;
                if (padX > ArcanaDeck_Manager.cardSlabList.Count-1)
                {
                    padX = 0;
                }
                StartCoroutine(PadBuffer());
            }

            //Display Selection
            Destroy(selectedSlot);

            //Adjusted index of the selected slot
            selectedIndex = padX;

            selectedSlot = Instantiate(Resources.Load("Prefabs/Inventory/Card_Select"), ArcanaDeck_Manager.cardSlabList[selectedIndex].transform) as GameObject;
        }
        //Stats
        else if(selectTab == 3)
        {

        }
    }

    IEnumerator PadBuffer()
    {
        yield return new WaitForSeconds(.25f);
        padActive = false;
    }

    void ToggleHelp()
    {
        if(InputManager.J_Back())
        {
            if(helpPanel.activeInHierarchy)
            {
                helpPanel.SetActive(false);
            }
            else
            {
                helpPanel.SetActive(true);
            }
        }
    }

    void SelectItem()
    {
        if(InputManager.A_Button())
        {
            //Inventory 1st State Item, Open Inv menu
            if(!invHUDMade && selectTab == 0 && InventoryManager.playerInventory[selectedIndex].itemID != 0)
            {
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                //Inv Menu made state
                invHUDMade = true;
                MakeInvHUD();
                invHUDselection = 0;
            }

            
            //Inventory 2nd State Item, Select Inv menu
            else if(invHUDMade && selectTab == 0)
            {
                if(invHUDselection == 0)
                {
                    invMenu.GetComponent<InvMenu>().PadEquip();
                    invMenu.GetComponent<InvMenu>().PadInspect();
                }
                else if(invHUDselection == 1)
                {
                    invMenu.GetComponent<InvMenu>().PadSplit();
                    //StartCoroutine(DestroyHUD());
                }
                else if(invHUDselection == 2)
                {
                    invMenu.GetComponent<InvMenu>().PadDestroy();
                    //StartCoroutine(DestroyHUD());
                }
                
                invHUDMade = false;
            }

            else if(!invHUDMade && selectTab == 1 && InventoryManager.playerEquipment[selectedIndex].itemID != 0)
            {
                //Check if there is a empty space in the inv
                bool spaceFound = false;
                for(int i = 0; i < InventoryManager.playerEquipment.Length; i++)
                {
                    if(InventoryManager.playerInventory[i].itemID == 0)
                    {
                        GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                        spaceFound = true;
                        InventoryManager.playerInventory[i] = InventoryManager.playerEquipment[selectedIndex];
                        InventoryManager.playerEquipment[selectedIndex] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
                        break;
                    }
                }
                if(!spaceFound)
                {
                    GameObject.Find("ErrorNoise").GetComponent<AudioSource>().Play();
                }
                UpdateEquip();
                UpdateInventory();
            }
        }

        if(InputManager.B_Button() && invHUDMade)
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            StartCoroutine(DestroyHUD());

        }
        else if(InputManager.MainVertical() < -.5f && invHUDMade && !padActive)
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            padActive = true;
            invHUDselection++;
            if(invHUDselection > 2)
            {
                invHUDselection = 0;
            }
            StartCoroutine(PadBuffer());
        }
        else if (InputManager.MainVertical() > .5f && invHUDMade && !padActive)
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            padActive = true;
            invHUDselection--;
            if(invHUDselection < 0)
            {
                invHUDselection = 2;
            }
            StartCoroutine(PadBuffer());
        }

        if(invHUDMade)
        {
            Destroy(invHUDSlot);
            invHUDSlot = Instantiate(Resources.Load("Prefabs/Inventory/Inv_Select"), invMenu.transform) as GameObject;
            switch(invHUDselection)
            {
                default:
                    break;
                case 0:
                    Instantiate(Resources.Load("Prefabs/Inventory/Inv_Cursor"), invHUDSlot.transform.Find("Option_1").transform);
                    break;
                case 1:
                    Instantiate(Resources.Load("Prefabs/Inventory/Inv_Cursor"), invHUDSlot.transform.Find("Option_2").transform);
                    break;
                case 2:
                    Instantiate(Resources.Load("Prefabs/Inventory/Inv_Cursor"), invHUDSlot.transform.Find("Option_3").transform);
                    break;
            }
        }


    }

    IEnumerator DestroyHUD()
    {
        yield return new WaitForSeconds(.1f);
        invMenu.SetActive(false);
        invHUDMade = false;
    }

    void MakeInvHUD()
    {
        GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
        invMenu.SetActive(true);
        invMenu.transform.position = slotList[selectedIndex].transform.position;
        invMenu.transform.position = new Vector3(invMenu.transform.position.x - .35f, invMenu.transform.position.y - .25f, invMenu.transform.position.z);
        invMenuToggle = true;

        //Change inv menu 1st option
        if (slotList[selectedIndex].GetComponent<Slot>().itemType == "Weapon" || slotList[selectedIndex].GetComponent<Slot>().itemType == "Head" ||
            slotList[selectedIndex].GetComponent<Slot>().itemType == "Neck" || slotList[selectedIndex].GetComponent<Slot>().itemType == "Body" ||
            slotList[selectedIndex].GetComponent<Slot>().itemType == "Ring" || slotList[selectedIndex].GetComponent<Slot>().itemType == "Feet")
        {
            //Equip
            invMenu.GetComponent<InvMenu>().optionID = 1;
            useText.text = "Equip";
        }

        else if (slotList[selectedIndex].GetComponent<Slot>().GetComponent<Slot>().itemType == "Consumable")
        {
            //Use
            invMenu.GetComponent<InvMenu>().optionID = 2;
            useText.text = "Use";
        }

        else if (slotList[selectedIndex].GetComponent<Slot>().itemType == "Crafting Material" || slotList[selectedIndex].GetComponent<Slot>().itemType == "Treasure")
        {
            //Inspect
            invMenu.GetComponent<InvMenu>().optionID = 3;
            useText.text = "Inspect";
        }

        //Send information to the inv menu
        invMenu.GetComponent<InvMenu>().slotNumber = slotList[selectedIndex].GetComponent<Slot>().slotNumber;
        invMenu.GetComponent<InvMenu>().slotImage = slotList[selectedIndex].GetComponent<Slot>().slotImage;
        invMenu.GetComponent<InvMenu>().itemAmount = slotList[selectedIndex].GetComponent<Slot>().itemAmount;
        invMenu.GetComponent<InvMenu>().itemID = slotList[selectedIndex].GetComponent<Slot>().itemID;
        invMenu.GetComponent<InvMenu>().itemType = slotList[selectedIndex].GetComponent<Slot>().itemType;
    }



}
