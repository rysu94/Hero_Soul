using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Shop_Controller : MonoBehaviour
{
    //0 = wep, 1 = magic, 2 = alc, 3 = dungeon
    public static int shopIndex = 0;
    public Image sellIMG;
    // 1= buying  2= buyback
    public static int shopMode = 1;
    public Image shopkeeperIMG;

    public GameObject buyPanel;
    public List<GameObject> buyList = new List<GameObject>();
    public GameObject sellPanel;
    public List<GameObject> sellList = new List<GameObject>();
    public GameObject salePanel;
    public List<GameObject> saleList = new List<GameObject>();

    public List<GameObject> buybackList = new List<GameObject>();

    public Text gold;

    public GameObject tooltip;

    public Button backButton;
    public GameObject dialogueFade;

    public GameObject buyback1;
    public GameObject buyback2;
    public Text buybackText;

    public int page = 0;
    public Text pageNum;
    public Button upButton, downButton;

    public GameObject sellMult, sellMultFrame;

    //GamePad Controls
    public static bool padActive = false;

    public static int mainPadX, mainPadY; //This is for the buy menu
    public static int salePadX; //This is for the sale menu
    public static int sidePadX, sidePadY; //This is for the player inv
    public static int tabPadY; //This is for the side tabs

    public static int selectedIndex;
    public GameObject selectedSlot;

    public GameObject interactCursor;

    // Use this for initialization
    void Start ()
    {
        backButton.onClick.AddListener(DoneButton);
        upButton.onClick.AddListener(NextPage);
        downButton.onClick.AddListener(BackPage);

        page = 0;

        //Is the controller enabled?
        if (GameController.xbox360Enabled())
        {
            mainPadX = -1;
            mainPadY = -1;
            sidePadX = -1;
            sidePadY = -1;
            salePadX = -1;
            tabPadY = -1;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Update player gold
        gold.text = InventoryManager.playerGold.ToString();

        UpdatePage();
        UpdateShopkeeper();

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = Camera.main.transform.position.z;
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        //Is the controller enabled?
        if(GameController.xbox360Enabled() && !sellMult.activeInHierarchy)
        {
            //Create the shop cursor
            if(interactCursor == null)
            {
                interactCursor = Instantiate(Resources.Load("Prefabs/Inventory/Inv_Cursor"), transform) as GameObject;
            }

            if(InputManager.L_Bumper())
            {
                mainPadX = -1;
                mainPadY = -1;
                sidePadX = -1;
                sidePadY = -1;
                salePadX = -1;
                tabPadY = -1;
                BackPage();
            }

            if(InputManager.R_Bumper())
            {
                mainPadX = -1;
                mainPadY = -1;
                sidePadX = -1;
                sidePadY = -1;
                salePadX = -1;
                tabPadY = -1;
                NextPage();
            }

            //Select Item
            if(InputManager.A_Button())
            {
                if(selectedSlot != null)
                {
                    if(mainPadX > -1)
                    {
                        selectedSlot.GetComponent<Shop_Slab>().BuyItem();
                    }
                    else if(salePadX > -1)
                    {
                        selectedSlot.GetComponent<Shop_Slab>().BuyItem();
                    }
                    else if(sidePadX > -1)
                    {
                        selectedSlot.GetComponent<Sell_Slot>().SellItem();
                    }
                }
                else if (tabPadY == 0)
                {
                    GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                    shopMode = 1;
                    page = 0;
                    UpdateShop();
                }
                else if (tabPadY == 1)
                {
                    GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                    shopMode = 2;
                    page = 0;
                    UpdateShop();
                }
            }
            //Close shop
            if(InputManager.B_Button())
            {
                tooltip.SetActive(false);
                DoneButton();
            }

            SetCursorPos();
            DisplayTooltip(); 
                      
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider != null && hit.collider.tag == "Buy_Tab")
                {
                    GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                    shopMode = 1;
                    page = 0;
                    UpdateShop();
                }
                else if (hit.collider != null && hit.collider.tag == "BuyBack_Tab")
                {
                    GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                    shopMode = 2;
                    page = 0;
                    UpdateShop();
                }
            }
        }

        //Tooltip
        if (hit.collider != null && hit.collider.tag == "Shop_Slab")
        {
            tooltip.SetActive(true);
            tooltip.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 5));
            tooltip.transform.position = new Vector3(tooltip.transform.position.x + 1f, tooltip.transform.position.y - .5f, tooltip.transform.position.z);
            //Find item name
            GameObject tempObj = tooltip.transform.Find("Item_Name").gameObject;
            Text tempText = tempObj.GetComponent<Text>();
            tempText.text = hit.collider.gameObject.GetComponent<Shop_Slab>().itemName;
            //Find item desc
            tempObj = tooltip.transform.Find("Item_Desc").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = hit.collider.gameObject.GetComponent<Shop_Slab>().itemDesc;
            //Find item type
            tempObj = tooltip.transform.Find("Item_Type").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = hit.collider.gameObject.GetComponent<Shop_Slab>().itemType;
            //Find item sell
            tempObj = tooltip.transform.Find("Item_Sell1").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = "";
            tempObj = tooltip.transform.Find("Item_Sell2").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = "";
        }
        else if(hit.collider != null && hit.collider.tag == "Sell_Slot" && hit.collider.GetComponent<Sell_Slot>().id != 0)
        {
            tooltip.SetActive(true);
            tooltip.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 5));
            tooltip.transform.position = new Vector3(tooltip.transform.position.x - 1.35f, tooltip.transform.position.y - .5f, tooltip.transform.position.z);

            //Find item name
            GameObject tempObj = tooltip.transform.Find("Item_Name").gameObject;
            Text tempText = tempObj.GetComponent<Text>();
            tempText.text = hit.collider.gameObject.GetComponent<Sell_Slot>().itemName;
            //Find item desc
            tempObj = tooltip.transform.Find("Item_Desc").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = hit.collider.gameObject.GetComponent<Sell_Slot>().itemDesc;
            //Find item type
            tempObj = tooltip.transform.Find("Item_Type").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = hit.collider.gameObject.GetComponent<Sell_Slot>().itemType;
            //Find item sell
            tempObj = tooltip.transform.Find("Item_Sell1").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = "Sells for ";
            tempObj = tooltip.transform.Find("Item_Sell2").gameObject;
            tempText = tempObj.GetComponent<Text>();
            tempText.text = hit.collider.gameObject.GetComponent<Sell_Slot>().sell.ToString() + " gold";
        }
        else
        {
            if(!GameController.xbox360Enabled())
                tooltip.SetActive(false);
        }
    }

    //Controller Support
    public void DisplayTooltip()
    {
        if((mainPadX > -1 || salePadX > -1) && selectedSlot != null)
        {      
            if(selectedSlot.GetComponent<Shop_Slab>())
            {
                tooltip.SetActive(true);
                tooltip.transform.position = new Vector3(selectedSlot.transform.position.x - 1.75f, selectedSlot.transform.position.y - .6f, tooltip.transform.position.z);
                //Find item name
                GameObject tempObj = tooltip.transform.Find("Item_Name").gameObject;
                Text tempText = tempObj.GetComponent<Text>();
                tempText.text = selectedSlot.GetComponent<Shop_Slab>().itemName;
                //Find item desc
                tempObj = tooltip.transform.Find("Item_Desc").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = selectedSlot.GetComponent<Shop_Slab>().itemDesc;
                //Find item type
                tempObj = tooltip.transform.Find("Item_Type").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = selectedSlot.GetComponent<Shop_Slab>().itemType;
                //Find item sell
                tempObj = tooltip.transform.Find("Item_Sell1").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = "";
                tempObj = tooltip.transform.Find("Item_Sell2").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = "";
            }
        }
        
        else if(sidePadX > -1 && selectedSlot != null)
        {
            if(selectedSlot.GetComponent<Sell_Slot>() && selectedSlot.GetComponent<Sell_Slot>().id != 0)
            {
                tooltip.SetActive(true);
                tooltip.transform.position = new Vector3(selectedSlot.transform.position.x - 1.75f, selectedSlot.transform.position.y - .6f, tooltip.transform.position.z);

                //Find item name
                GameObject tempObj = tooltip.transform.Find("Item_Name").gameObject;
                Text tempText = tempObj.GetComponent<Text>();
                tempText.text = selectedSlot.GetComponent<Sell_Slot>().itemName;
                //Find item desc
                tempObj = tooltip.transform.Find("Item_Desc").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = selectedSlot.GetComponent<Sell_Slot>().itemDesc;
                //Find item type
                tempObj = tooltip.transform.Find("Item_Type").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = selectedSlot.GetComponent<Sell_Slot>().itemType;
                //Find item sell
                tempObj = tooltip.transform.Find("Item_Sell1").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = "Sells for ";
                tempObj = tooltip.transform.Find("Item_Sell2").gameObject;
                tempText = tempObj.GetComponent<Text>();
                tempText.text = selectedSlot.GetComponent<Sell_Slot>().sell.ToString() + " gold";
            }

        }
        
    }

    public void SetCursorPos()
    {
        //Initial setting?
        if(mainPadX == -1 && sidePadX == -1 && salePadX == -1 && tabPadY == -1)
        {
            if(buyList.Count > 0)
            {
                mainPadX = 0;
                mainPadY = 0;
            }   
            else if(saleList.Count > 0)
            {
                salePadX = 0;
            } 
            else if(sellList.Count > 0)
            {
                sidePadX = 0;
                sidePadY = 0;
            }
        }

        //Main?
        if(mainPadX > -1)
        {
            //Determine where to place the cursor
            selectedIndex = (mainPadY * 2) + mainPadX;

            if(shopMode == 1 && buyList.Count > 0)
            {
                if (selectedIndex >= buyList.Count)
                {
                    selectedIndex = buyList.Count - 1;
                }
                Destroy(interactCursor);
                interactCursor = Instantiate(Resources.Load("Prefabs/Inventory/Inv_Cursor_ShopBuy"), buyList[selectedIndex].transform) as GameObject;
                selectedSlot = buyList[selectedIndex];
            }
            else if(shopMode == 2 && buybackList.Count > 0)
            {
                if (selectedIndex >= buybackList.Count)
                {
                    selectedIndex = buybackList.Count - 1;
                }
                Destroy(interactCursor);
                interactCursor = Instantiate(Resources.Load("Prefabs/Inventory/Inv_Cursor_ShopBuy"), buybackList[selectedIndex].transform) as GameObject;
                selectedSlot = buybackList[selectedIndex];
            }



            if (InputManager.MainHorizontal() > .5f && !padActive)
            {
                padActive = true;
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                mainPadX++;
                if (mainPadX > 1)
                {
                    mainPadX = -1;
                    sidePadX = 0;
                    sidePadY = 0;
                    mainPadY = -1;
                }
                StartCoroutine(PadBuffer());
            }
            else if (InputManager.MainHorizontal() < -.5f && !padActive)
            {
                padActive = true;
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                mainPadX--;
                if (mainPadX < 0)
                {
                    mainPadX = -1;
                    mainPadY = -1;
                    tabPadY = 0;
                }
                StartCoroutine(PadBuffer());
            }
            else if (InputManager.MainVertical() > .5f && !padActive)
            {
                padActive = true;
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                mainPadY--;
                if (mainPadY < 0 && shopMode == 1)
                {
                    mainPadY = -1;
                    salePadX = mainPadX;
                    mainPadX = -1;
                }
                else if(mainPadY < 0 && shopMode == 2)
                {
                    mainPadY = buybackList.Count/2;
                }
                StartCoroutine(PadBuffer());
            }
            else if (InputManager.MainVertical() < -.5f && !padActive)
            {
                padActive = true;
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                mainPadY++;
                if (mainPadY > buyList.Count/2 && shopMode == 1)
                {
                    mainPadY = -1;
                    salePadX = mainPadX;
                    mainPadX = -1;
                }
                else if (mainPadY > buybackList.Count / 2 && shopMode == 2)
                {
                    mainPadY = 0;
                }
                StartCoroutine(PadBuffer());
            }
        }
        //Side?
        else if(sidePadX > -1)
        {
            //Determine where to place the cursor
            selectedIndex = (sidePadY * 6) + sidePadX;
            Destroy(interactCursor);
            interactCursor = Instantiate(Resources.Load("Prefabs/Inventory/Inv_Cursor_ShopSell"), sellList[selectedIndex].transform) as GameObject;
            selectedSlot = sellList[selectedIndex];

            if (InputManager.MainHorizontal() > .5f && !padActive)
            {
                padActive = true;
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                sidePadX++;
                if (sidePadX > 5)
                {
                    sidePadX = -1;    
                    sidePadY = -1;
                    tabPadY = 0;
                }
                StartCoroutine(PadBuffer());
            }
            else if (InputManager.MainHorizontal() < -.5f && !padActive)
            {
                padActive = true;
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                sidePadX--;
                if (sidePadX < 0)
                {
                    sidePadX = -1;
                    sidePadY = -1;
                    if(shopMode == 1)
                    {
                        if (buyList.Count > 0)
                        {
                            mainPadX = 0;
                            mainPadY = 0;
                        }
                        else
                        {
                            salePadX = 0;
                        }
                    }
                    else if(shopMode == 2)
                    {
                        if (buybackList.Count > 0)
                        {
                            mainPadX = 0;
                            mainPadY = 0;
                        }
                        else
                        {
                            tabPadY = 0;
                        }
                    }

                    
                }
                StartCoroutine(PadBuffer());
            }
            else if (InputManager.MainVertical() > .5f && !padActive)
            {
                padActive = true;
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                sidePadY--;
                if (sidePadY < 0)
                {
                    sidePadY = 5;
                }
                StartCoroutine(PadBuffer());
            }
            else if (InputManager.MainVertical() < -.5f && !padActive)
            {
                padActive = true;
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                sidePadY++;
                if (sidePadY > 5)
                {
                    sidePadY = 0;
                }
                StartCoroutine(PadBuffer());
            }
        }
        //Sale?
        else if (salePadX > -1 && shopMode == 1)
        {
            //Determine where to place the cursor
            if(salePadX == 0 && saleList.Count > 0)
            {
                Destroy(interactCursor);
                interactCursor = Instantiate(Resources.Load("Prefabs/Inventory/Inv_Cursor_ShopBuy"), saleList[0].transform) as GameObject;
                selectedSlot = saleList[0];
            }
            else if(salePadX == 1 && saleList.Count > 1)
            {
                Destroy(interactCursor);
                interactCursor = Instantiate(Resources.Load("Prefabs/Inventory/Inv_Cursor_ShopBuy"), saleList[1].transform) as GameObject;
                selectedSlot = saleList[1];
            }
            else if(salePadX == 0 && saleList.Count == 0)
            {
                Destroy(interactCursor);
                interactCursor = Instantiate(Resources.Load("Prefabs/Inventory/Inv_Cursor_ShopBuy"), buyback2.transform) as GameObject;
                selectedSlot = null;
            }
            else if (salePadX == 1 && saleList.Count >= 0)
            {
                Destroy(interactCursor);
                interactCursor = Instantiate(Resources.Load("Prefabs/Inventory/Inv_Cursor_ShopBuy"), buyback1.transform) as GameObject;
                selectedSlot = null;
            }

            if (InputManager.MainHorizontal() > .5f && !padActive)
            {
                padActive = true;
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                salePadX++;
                if (salePadX > 1)
                {
                    salePadX = -1;
                    sidePadX = 0;
                    sidePadY = 0;
                }
                StartCoroutine(PadBuffer());
            }
            else if (InputManager.MainHorizontal() < -.5f && !padActive)
            {
                padActive = true;
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                salePadX--;
                if (salePadX < 0)
                {
                    salePadX = -1;
                    tabPadY = 0;
                }
                StartCoroutine(PadBuffer());
            }
            else if (InputManager.MainVertical() > .5f && !padActive)
            {
                padActive = true;
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                if(buyList.Count > 0)
                {
                    mainPadX = salePadX;
                    mainPadY = 3;
                    salePadX = -1;
                }            
                StartCoroutine(PadBuffer());
            }
            else if (InputManager.MainVertical() < -.5f && !padActive)
            {
                padActive = true;
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                if(buyList.Count > 0)
                {
                    mainPadX = salePadX;
                    mainPadY = 0;
                    salePadX = -1;
                }
                StartCoroutine(PadBuffer());
            }
        }
        //Tab?
        else if(tabPadY > -1)
        {
            selectedSlot = null;
            //Determine where to place the cursor
            if (tabPadY == 0)
            {
                Destroy(interactCursor);
                interactCursor = Instantiate(Resources.Load("Prefabs/Inventory/Inv_Cursor_ShopTab"), GameObject.Find("BuyTab").transform) as GameObject;
            }
            else
            {
                Destroy(interactCursor);
                interactCursor = Instantiate(Resources.Load("Prefabs/Inventory/Inv_Cursor_ShopTab"), GameObject.Find("Buyback").transform) as GameObject;
            }

            if (InputManager.MainHorizontal() > .5f && !padActive)
            {
                padActive = true;
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                if(shopMode == 1)
                {
                    if (buyList.Count > 0)
                    {
                        mainPadX = 0;
                        mainPadY = 0;
                    }
                    else
                    {
                        salePadX = 0;
                    }
                    tabPadY = -1;
                }
                else if(shopMode == 2)
                {
                    if(buybackList.Count > 0)
                    {
                        mainPadX = 0;
                        mainPadY = 0;
                    }
                    else
                    {
                        sidePadX = 0;
                        sidePadY = 0;
                    }
                }

                StartCoroutine(PadBuffer());
            }
            else if (InputManager.MainHorizontal() < -.5f && !padActive)
            {
                padActive = true;
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                sidePadX = 0;
                sidePadY = 0;
                tabPadY = -1;
                StartCoroutine(PadBuffer());
            }
            else if (InputManager.MainVertical() > .5f && !padActive)
            {
                padActive = true;
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                tabPadY--;
                if (tabPadY < 0)
                {
                    tabPadY = 1;
                }
                StartCoroutine(PadBuffer());
            }
            else if (InputManager.MainVertical() < -.5f && !padActive)
            {
                padActive = true;
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                tabPadY++;
                if (tabPadY > 1)
                {
                    tabPadY = 0;
                }
                StartCoroutine(PadBuffer());
            }
        }
    }

    IEnumerator PadBuffer()
    {
        yield return new WaitForSeconds(.25f);
        padActive = false;
    }




    public void UpdateShop()
    {
        GetComponent<Shop_Database>().initDB();

        //Display correct shop inv image & generate the slabs
        if(shopMode == 1)
        {
            sellIMG.sprite = Resources.Load<Sprite>("Shop/ShopInv");

            buybackText.gameObject.SetActive(true);
            buyback1.SetActive(true);
            buyback2.SetActive(true);

            foreach (GameObject slab in GameObject.FindGameObjectsWithTag("Shop_Slab"))
            {
                Destroy(slab);
            }


            //Determine the max index
            int modifier = Shop_Database.shopList[shopIndex].Count - (page * 8);           
            int limit;
            if(modifier >= 8)
            {
                limit = (page * 8) + 8;
            }
            else
            {
                limit = (page * 8) + modifier;
            }

            buyList.Clear();
            //Normal Sales
            for (int i = (page * 8); i < limit; i++)
            {
                GameObject tempObj = Instantiate(Resources.Load("Prefabs/Shop/Shop_Slab"), buyPanel.transform) as GameObject;
                tempObj.GetComponent<Shop_Slab>().itemName = GetComponent<ItemDatabase>().FindItem(Shop_Database.shopList[shopIndex][i]).itemName;
                tempObj.GetComponent<Shop_Slab>().itemType = GetComponent<ItemDatabase>().FindItem(Shop_Database.shopList[shopIndex][i]).itemType;
                tempObj.GetComponent<Shop_Slab>().itemDesc = GetComponent<ItemDatabase>().FindItem(Shop_Database.shopList[shopIndex][i]).itemDescription;
                tempObj.GetComponent<Shop_Slab>().sell = GetComponent<ItemDatabase>().FindItem(Shop_Database.shopList[shopIndex][i]).itemSellCost * 2;
                tempObj.GetComponent<Shop_Slab>().id = GetComponent<ItemDatabase>().FindItem(Shop_Database.shopList[shopIndex][i]).itemID;
                tempObj.transform.Find("Button").transform.Find("Item_Name").GetComponent<Text>().text = tempObj.GetComponent<Shop_Slab>().itemName;
                tempObj.transform.Find("Button").transform.Find("Gold").transform.Find("Gold_Text").GetComponent<Text>().text = tempObj.GetComponent<Shop_Slab>().sell.ToString();
                tempObj.transform.Find("Item_Frame").transform.Find("Item_IMG").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + GetComponent<ItemDatabase>().FindItem(Shop_Database.shopList[shopIndex][i]).itemIconName);
                buyList.Add(tempObj);
            }

            saleList.Clear();
            //Special Sale!
            for (int i = 0; i < Shop_Database.saleList[shopIndex].Count; i++)
            {
                GameObject tempObj = Instantiate(Resources.Load("Prefabs/Shop/Shop_Slab"), salePanel.transform) as GameObject;
                tempObj.GetComponent<Shop_Slab>().itemName = GetComponent<ItemDatabase>().FindItem(Shop_Database.saleList[shopIndex][i]).itemName;
                tempObj.GetComponent<Shop_Slab>().itemType = GetComponent<ItemDatabase>().FindItem(Shop_Database.saleList[shopIndex][i]).itemType;
                tempObj.GetComponent<Shop_Slab>().itemDesc = GetComponent<ItemDatabase>().FindItem(Shop_Database.saleList[shopIndex][i]).itemDescription;
                tempObj.GetComponent<Shop_Slab>().sell = GetComponent<ItemDatabase>().FindItem(Shop_Database.saleList[shopIndex][i]).itemSellCost * 2;
                tempObj.GetComponent<Shop_Slab>().id = GetComponent<ItemDatabase>().FindItem(Shop_Database.saleList[shopIndex][i]).itemID;
                tempObj.GetComponent<Shop_Slab>().sale = true;
                tempObj.GetComponent<Shop_Slab>().index = i;
                tempObj.transform.Find("Button").transform.Find("Item_Name").GetComponent<Text>().text = tempObj.GetComponent<Shop_Slab>().itemName;
                tempObj.transform.Find("Button").transform.Find("Gold").transform.Find("Gold_Text").GetComponent<Text>().text = tempObj.GetComponent<Shop_Slab>().sell.ToString();
                tempObj.transform.Find("Item_Frame").transform.Find("Item_IMG").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + GetComponent<ItemDatabase>().FindItem(Shop_Database.saleList[shopIndex][i]).itemIconName);
                saleList.Add(tempObj);
            }

            foreach (GameObject slot in GameObject.FindGameObjectsWithTag("Sell_Slot"))
            {
                Destroy(slot);
            }

            sellList.Clear();
            for(int i = 0; i < InventoryManager.playerInventory.Length; i++)
            {
                GameObject tempObj = Instantiate(Resources.Load("Prefabs/Shop/Sell_Slot"), sellPanel.transform) as GameObject;
                tempObj.GetComponent<Sell_Slot>().itemName = InventoryManager.playerInventory[i].itemName;
                tempObj.GetComponent<Sell_Slot>().itemType = InventoryManager.playerInventory[i].itemType;
                tempObj.GetComponent<Sell_Slot>().itemDesc = InventoryManager.playerInventory[i].itemDescription;
                tempObj.GetComponent<Sell_Slot>().sell = InventoryManager.playerInventory[i].itemSellCost;
                tempObj.GetComponent<Sell_Slot>().quant = InventoryManager.playerInventory[i].itemQuantity;
                tempObj.GetComponent<Sell_Slot>().id = InventoryManager.playerInventory[i].itemID;
                tempObj.GetComponent<Sell_Slot>().slot = i;
                tempObj.GetComponent<Button>().onClick.AddListener(tempObj.GetComponent<Sell_Slot>().SellItem);

                tempObj.transform.Find("Item_IMG").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + InventoryManager.playerInventory[i].itemIconName);
                if(tempObj.GetComponent<Sell_Slot>().quant > 0 && InventoryManager.playerInventory[i].stackable)
                {
                    tempObj.transform.Find("Item_Num").GetComponent<Text>().text = tempObj.GetComponent<Sell_Slot>().quant.ToString();
                }
                else
                {
                    tempObj.transform.Find("Item_Num").GetComponent<Text>().text = "";
                }

                sellList.Add(tempObj);
                
            }


        }
        else
        {
            sellIMG.sprite = Resources.Load<Sprite>("Shop/ShopInv2");

            buybackText.gameObject.SetActive(false);
            buyback1.SetActive(false);
            buyback2.SetActive(false);

            foreach (GameObject slab in GameObject.FindGameObjectsWithTag("Shop_Slab"))
            {
                Destroy(slab);
            }


            //Determine the max index
            int modifier = Shop_Database.buybackList.Count - (page * 12);
            int limit;
            if (modifier >= 12)
            {
                limit = (page * 12) + 12;
            }
            else
            {
                limit = (page * 12) + modifier;
            }

            buybackList.Clear();
            for (int i = (page * 12); i < limit; i++)
            {
                GameObject tempObj = Instantiate(Resources.Load("Prefabs/Shop/Shop_Slab"), buyPanel.transform) as GameObject;
                tempObj.GetComponent<Shop_Slab>().itemName = Shop_Database.buybackList[i].itemName;
                tempObj.GetComponent<Shop_Slab>().itemType = Shop_Database.buybackList[i].itemType;
                tempObj.GetComponent<Shop_Slab>().itemDesc = Shop_Database.buybackList[i].itemDescription;
                tempObj.GetComponent<Shop_Slab>().sell = Shop_Database.buybackList[i].itemSellCost;
                tempObj.GetComponent<Shop_Slab>().id = Shop_Database.buybackList[i].itemID;
                tempObj.GetComponent<Shop_Slab>().quant = Shop_Database.buybackList[i].itemQuantity;
                tempObj.GetComponent<Shop_Slab>().index = i;
                tempObj.GetComponent<Shop_Slab>().buyback = true;
                tempObj.transform.Find("Button").transform.Find("Item_Name").GetComponent<Text>().text = tempObj.GetComponent<Shop_Slab>().itemName;
                if(tempObj.GetComponent<Shop_Slab>().quant > 0)
                {
                    tempObj.transform.Find("Button").transform.Find("Gold").transform.Find("Gold_Text").GetComponent<Text>().text = (tempObj.GetComponent<Shop_Slab>().sell * tempObj.GetComponent<Shop_Slab>().quant).ToString();
                }
                else
                {
                    tempObj.transform.Find("Button").transform.Find("Gold").transform.Find("Gold_Text").GetComponent<Text>().text = tempObj.GetComponent<Shop_Slab>().sell.ToString();
                }
                
                tempObj.transform.Find("Item_Frame").transform.Find("Item_IMG").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + Shop_Database.buybackList[i].itemIconName);
                buybackList.Add(tempObj);
            }

            foreach (GameObject slot in GameObject.FindGameObjectsWithTag("Sell_Slot"))
            {
                Destroy(slot);
            }

            sellList.Clear();
            for (int i = 0; i < InventoryManager.playerInventory.Length; i++)
            {
                GameObject tempObj = Instantiate(Resources.Load("Prefabs/Shop/Sell_Slot"), sellPanel.transform) as GameObject;
                tempObj.GetComponent<Sell_Slot>().itemName = InventoryManager.playerInventory[i].itemName;
                tempObj.GetComponent<Sell_Slot>().itemType = InventoryManager.playerInventory[i].itemType;
                tempObj.GetComponent<Sell_Slot>().itemDesc = InventoryManager.playerInventory[i].itemDescription;
                tempObj.GetComponent<Sell_Slot>().sell = InventoryManager.playerInventory[i].itemSellCost;
                tempObj.GetComponent<Sell_Slot>().quant = InventoryManager.playerInventory[i].itemQuantity;
                tempObj.GetComponent<Sell_Slot>().id = InventoryManager.playerInventory[i].itemID;
                tempObj.GetComponent<Sell_Slot>().slot = i;
                tempObj.GetComponent<Button>().onClick.AddListener(tempObj.GetComponent<Sell_Slot>().SellItem);

                tempObj.transform.Find("Item_IMG").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + InventoryManager.playerInventory[i].itemIconName);
                if (tempObj.GetComponent<Sell_Slot>().quant > 0 && InventoryManager.playerInventory[i].stackable &&
                    (tempObj.GetComponent<Sell_Slot>().itemType == "Treasure" || tempObj.GetComponent<Sell_Slot>().itemType == "Crafting Material"))
                {
                    tempObj.transform.Find("Item_Num").GetComponent<Text>().text = tempObj.GetComponent<Sell_Slot>().quant.ToString();
                }
                else
                {
                    tempObj.transform.Find("Item_Num").GetComponent<Text>().text = "";
                }
                sellList.Add(tempObj);
            }
        }
        
    }

    void DoneButton()
    {
        padActive = false;
        GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
        GameObject.Find("Shop_HUD").SetActive(false);
        TestCharController.inDialogue = false;
        dialogueFade.SetActive(false);
    }

    void UpdatePage()
    {
        pageNum.text = "Page " + (page + 1);
    }

    void NextPage()
    {
        //is there another page?
        if (Shop_Database.shopList[shopIndex].Count > (page * 8) + 8 && shopMode == 1)
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            page++;
            UpdateShop();
        }
        else if(Shop_Database.buybackList.Count > (page * 12) + 12 && shopMode == 2)
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            page++;
            UpdateShop();
        }
        else
        {
            GameObject.Find("ErrorNoise").GetComponent<AudioSource>().Play();
        }
    }

    void BackPage()
    {
        if(page > 0)
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            page--;
            UpdateShop();
        }
        else
        {
            GameObject.Find("ErrorNoise").GetComponent<AudioSource>().Play();
        }
    }

    void UpdateShopkeeper()
    {
        if(shopIndex == 0)
        {
            shopkeeperIMG.sprite = Resources.Load<Sprite>("Faces/NPC/NPC_Shopkeeper");
        }
        else if(shopIndex == 1)
        {
            shopkeeperIMG.sprite = Resources.Load<Sprite>("Faces/NPC/NPC_Magic");
        }
        else if(shopIndex == 2)
        {
            shopkeeperIMG.sprite = Resources.Load<Sprite>("Faces/NPC/NPC_Alc");
        }
        else if(shopIndex == 3)
        {
            shopkeeperIMG.sprite = Resources.Load<Sprite>("Faces/NPC/NPC_Shopkeeper");
        }

    }

    public void ShowMult(int itemID, int quant, string prompt, bool buying)
    {
        sellMult.SetActive(true);
        sellMultFrame.GetComponent<Sell_Mult_Controller>().id = itemID;
        sellMultFrame.GetComponent<Sell_Mult_Controller>().itemQuant = quant;
        sellMultFrame.GetComponent<Sell_Mult_Controller>().prompt.text = "How many would you like to " + prompt + "<color=yellow> (" + GetComponent<ItemDatabase>().FindItem(itemID).itemSellCost + " gold)</color>";
        sellMultFrame.GetComponent<Sell_Mult_Controller>().buying = buying;
        sellMultFrame.GetComponent<Sell_Mult_Controller>().itemNum.text = "1";
        Sell_Mult_Controller.sellQuant = 1;
        Sell_Mult_Controller.buyQuant = 1;
    }

    


}
