using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class ShopSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int slotNumber;
    public Sprite slotImage;
    public int itemAmount;
    public int itemID;
    public string itemType;
    public GameObject dragSlot;
    public GameObject slotImageObj;
    public GameObject slotTextObj;
    public AudioSource noise;

    //ID = 1 : Weiss Alc Shop
    //ID = 2 : Weiss Wep Shop
    //ID - 3 : Weiss Magic Shop
    public int shopID;

    void Awake()
    {
        noise = GameObject.Find("GenNoise").GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //Begin mouse drag
    //1. raycast to check if mouse is on top of a slot
    //2. get item data on the slot 
    //3. instantiate the Drag Image prefab
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemID > 0 && !InventoryManager.inShop)
        {
            noise.Play();
            dragSlot = Instantiate(Resources.Load("Prefabs/Inventory/Drag_Item")) as GameObject;
            dragSlot.transform.SetParent(GameObject.Find("HUD").transform, false);
            dragSlot.GetComponent<Image>().sprite = slotImage;
            if (GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].stackable)
            {
                dragSlot.GetComponentInChildren<Text>().text = itemAmount.ToString();
            }
            else if (!GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].stackable)
            {
                dragSlot.GetComponentInChildren<Text>().text = "";
            }


            slotImageObj = transform.Find("Item_IMG").gameObject;
            slotImageObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/No_Item");
            slotTextObj = transform.Find("Item_Num").gameObject;
            slotTextObj.GetComponent<Text>().text = "";

        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (itemID > 0 && !InventoryManager.inShop)
        {
            dragSlot.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 5));
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (itemID > 0 && !InventoryManager.inShop)
        {
            noise.Play();
            //raycast to see if its above an item slot
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPoint.z = Camera.main.transform.position.z;
            Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider != null && hit.collider.tag == "Inv_Slot")
            {
                GameObject.Find("AlcShopButton").GetComponent<AlcShop>().OpenPrompt(slotNumber, true);
                Destroy(dragSlot);
            }
            else
            {
                Destroy(dragSlot);
                slotImageObj.GetComponent<Image>().sprite = slotImage;
                if (GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].stackable)
                {
                    slotTextObj.GetComponent<Text>().text = itemAmount.ToString();
                }
                else if (!GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[itemID].stackable)
                {
                    slotTextObj.GetComponent<Text>().text = "";
                }
            }
        }
    }
}
