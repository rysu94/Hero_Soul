using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class PotionSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int potionID;

    public string potionName;
    public string potionDesc;
    public string potionQuantity;
    public string imgName;

    public GameObject slotTooltip;

    public Text potionNameText;
    public Text potionDescText;
    public Text potionQuantityText;

    public AudioSource noise;
    public GameObject dragSlot;

    // Use this for initialization
    void Start ()
    {
        noise = GameObject.Find("GenNoise").GetComponent<AudioSource>();
        GetTooptip();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = Camera.main.transform.position.z;
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit.collider != null && hit.collider.tag == "Potion_Slot")
        {
            slotTooltip.SetActive(true);
            GetTooptip();
            potionNameText.text = hit.collider.gameObject.GetComponent<PotionSlot>().potionName;
            potionDescText.text = hit.collider.gameObject.GetComponent<PotionSlot>().potionDesc;
            potionQuantityText.text = hit.collider.gameObject.GetComponent<PotionSlot>().potionQuantity;
        }
        else
        {
            slotTooltip.SetActive(false);
        }
    }

    void GetTooptip()
    {
        switch (potionID)
        {
            default:
                potionName = "";
                potionDesc = "";
                potionQuantity = "";
                imgName = "";
                break;
            case 1:
                potionName = "Health Potion";
                potionDesc = "Use to heal for " + PotionController.healthPotionAmount + ".";
                potionQuantity = "Quantity: " + PotionController.healthPotionCurrent + "/" + PotionController.healthPotionMax;
                imgName = "Heal_Potion";
                break;
            case 2:
                potionName = "Mana Potion";
                potionDesc = "Use to regenerate " + PotionController.manaPotionAmount + " mana.";
                potionQuantity = "Quantity: " + PotionController.manaPotionCurrent + "/" + PotionController.manaPotionMax;
                imgName = "Mana_Potion";
                break;
            case 3:
                potionName = "Stamina Potion";
                potionDesc = "Use to regenerate " + PotionController.staminaPotionAmount + " stamina.";
                potionQuantity = "Quantity: " + PotionController.staminaPotionCurrent + "/" + PotionController.staminaPotionMax;
                imgName = "Stamina_Potion";
                break;
            case 4:
                potionName = "Stoneskin Potion";
                potionDesc = "Use to gain " + (PotionController.stonePotionAmount * 10) + " armor for 30 seconds.";
                potionQuantity = "Quantity: " + PotionController.stonePotionCurrent + "/" + PotionController.stonePotionMax;
                imgName = "StoneskinPotion";
                break;
            case 5:
                potionName = "Strength Potion";
                potionDesc = "Use to gain " + (PotionController.strengthPotionAmount * 10) + " strength for 30 seconds.";
                potionQuantity = "Quantity: " + PotionController.strengthPotionCurrent + "/" + PotionController.strengthPotionMax;
                imgName = "strPotion_Potion";
                break;
            case 6:
                potionName = "Alacrity Potion";
                potionDesc = "Use to gain " + (PotionController.alacrityPotionAmount * 5) + " dexterity and movement speed for 30 seconds.";
                potionQuantity = "Quantity: " + PotionController.alacrityPotionCurrent + "/" + PotionController.alacrityPotionMax;
                imgName = "AlacrityPotion";
                break;
            case 7:
                potionName = "Intellect Potion";
                potionDesc = "Use to gain " + (PotionController.intellectPotionAmount * 10) + " intelligence for 30 seconds.";
                potionQuantity = "Quantity: " + PotionController.intellectPotionCurrent + "/" + PotionController.intellectPotionMax;
                imgName = "intPotion";
                break;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        noise.Play();
        dragSlot = Instantiate(Resources.Load("Prefabs/Inventory/Drag_Item")) as GameObject;
        dragSlot.transform.SetParent(GameObject.Find("HUD").transform, false);
        dragSlot.GetComponent<Image>().sprite = Resources.Load<Sprite>("Potion/" + imgName);
        dragSlot.GetComponentInChildren<Text>().text = "";
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragSlot.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 5));
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        noise.Play();
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = Camera.main.transform.position.z;
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit.collider != null && (hit.collider.tag == "Potion_Slot1" || hit.collider.tag == "Potion_Slot2"))
        {
            if (hit.collider.tag == "Potion_Slot1")
            {
                PotionController.slot1Active = potionID;
            }
            else if (hit.collider.tag == "Potion_Slot2")
            {
                PotionController.slot2Active = potionID;
            }

        }

        Destroy(dragSlot);
    }
}
