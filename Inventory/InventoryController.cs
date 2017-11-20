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
    public bool invMenuToggle = false;

    //stash
    public GameObject stash;

    //treasure
    public GameObject treasure;

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
                StartCoroutine(CloseInv()); 
            }
        }

        //Inventory Slot
        else if(hit.collider != null && hit.collider.tag == "Inv_Slot")
        {
            if((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && hit.collider.gameObject.GetComponent<Slot>().itemID != 0 && invMenuToggle)
            {
                invMenu.SetActive(false);
                invMenuToggle = false;
            }

            else if(Input.GetMouseButtonDown(1) && hit.collider.gameObject.GetComponent<Slot>().itemID != 0 && !invMenuToggle)
            {
                invMenu.SetActive(true);
                invMenu.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 5));
                invMenu.transform.position = new Vector3(invMenu.transform.position.x - .35f, invMenu.transform.position.y - .15f, invMenu.transform.position.z);
                invMenuToggle = true;

                //Send information to the inv menu
                invMenu.GetComponent<InvMenu>().slotNumber = hit.collider.GetComponent<Slot>().slotNumber;
                invMenu.GetComponent<InvMenu>().slotImage = hit.collider.GetComponent<Slot>().slotImage;
                invMenu.GetComponent<InvMenu>().itemAmount = hit.collider.GetComponent<Slot>().itemAmount;
                invMenu.GetComponent<InvMenu>().itemID = hit.collider.GetComponent<Slot>().itemID;
                invMenu.GetComponent<InvMenu>().itemType = hit.collider.GetComponent<Slot>().itemType;
            }
        }
        
        if ((Input.GetKeyDown(KeyCode.I) || (Input.GetKeyDown(KeyCode.Tab))) && !TestCharController.inDialogue)
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
    }
    public void OpenInv()
    {
        inInv = true;
        Sprite tempSprite = Resources.Load<Sprite>("Inventory/Inventory");
        InventoryController.invImage.sprite = tempSprite;
        invToggle = true;
        inv.SetActive(true);
        UpdateInventory();
        playerGold.text = InventoryManager.playerGold.ToString();
        UpdateEquip();
    }
    public void CloseInventory()
    {
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
    }


}
