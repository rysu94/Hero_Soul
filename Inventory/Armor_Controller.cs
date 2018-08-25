using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Armor_Controller : MonoBehaviour
{
    public AudioSource click;

    public GameObject treasure;
    public GameObject stash;

    void Awake()
    {

    }


	// Use this for initialization
	void Start ()
    {
        click = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = Camera.main.transform.position.z;
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if(hit.collider != null && hit.collider.tag == "Armor_Tab")
        {
            if (Input.GetMouseButtonDown(0) && InventoryController.equipToggle && !InventoryController.inSpellbook)
            {
                click.Play();
                InventoryController.equip.gameObject.SetActive(false);
                Sprite tempSprite = Resources.Load<Sprite>("Inventory/Inventory");
                InventoryController.invImage.sprite = tempSprite;
                InventoryController.equipToggle = false;

            }

            else if (Input.GetMouseButtonDown(0) && !InventoryController.equipToggle && !InventoryController.inSpellbook)
            {
                InventoryController.equip.gameObject.SetActive(true);
                click.Play();
                Sprite tempSprite = Resources.Load<Sprite>("Inventory/Inv_2");
                InventoryController.invImage.sprite = tempSprite;
                InventoryController.equipToggle = true;
                InventoryController.deckToggle = false;
                InventoryController.deck.gameObject.SetActive(false);
                InventoryController.statToggle = false;
                InventoryController.stats.gameObject.SetActive(false);
                treasure.SetActive(false);
                stash.SetActive(false);

                //Armor Tutorial
                if (!TutorialDatabase.tut2_A && TutorialDatabase.tut2)
                {
                    GameObject.Find("Equip_Tut").GetComponent<EquipmentTut>().EquipTut();
                }
            }
        }

        if(InputManager.J_DPadHorizontal() < 0 && InventoryController.equipToggle && !InventoryController.invHUDMade && !InventoryController.padActive)
        {
            click.Play();
            InventoryController.equip.gameObject.SetActive(false);
            Sprite tempSprite = Resources.Load<Sprite>("Inventory/Inventory");
            InventoryController.invImage.sprite = tempSprite;
            InventoryController.equipToggle = false;
            InventoryController.selectTab = 0;
            InventoryController.padX = 0;
            InventoryController.padActive = true;
            StartCoroutine(InputBuffer());
        }
        else if(InputManager.J_DPadHorizontal() < 0 && !InventoryController.equipToggle && !InventoryController.invHUDMade && !InventoryController.padActive)
        {
            InventoryController.equip.gameObject.SetActive(true);
            click.Play();
            Sprite tempSprite = Resources.Load<Sprite>("Inventory/Inv_2");
            InventoryController.invImage.sprite = tempSprite;
            InventoryController.equipToggle = true;
            InventoryController.deckToggle = false;
            InventoryController.deck.gameObject.SetActive(false);
            InventoryController.statToggle = false;
            InventoryController.stats.gameObject.SetActive(false);
            treasure.SetActive(false);
            stash.SetActive(false);
            InventoryController.selectTab = 0;
            InventoryController.padX = 0;
            InventoryController.padActive = true;
            StartCoroutine(InputBuffer());
        }


    }

    IEnumerator InputBuffer()
    {
        yield return new WaitForSeconds(.5f);
        InventoryController.padActive = false;
    }
}
