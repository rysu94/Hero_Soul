using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat_Controller : MonoBehaviour
{
    public AudioSource click;

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

        if(hit.collider != null && hit.collider.tag == "Skill_Tab")
        {
            if (Input.GetMouseButtonDown(0) && InventoryController.statToggle && !InventoryController.inSpellbook)
            {
                click.Play();
                InventoryController.stats.gameObject.SetActive(false);
                Sprite tempSprite = Resources.Load<Sprite>("Inventory/Inventory");
                InventoryController.invImage.sprite = tempSprite;
                InventoryController.statToggle = false;
            }

            else if (Input.GetMouseButtonDown(0) && !InventoryController.statToggle && !InventoryController.inSpellbook)
            {
                InventoryController.stats.gameObject.SetActive(true);
                click.Play();
                Sprite tempSprite = Resources.Load<Sprite>("Inventory/Inv_4");
                InventoryController.invImage.sprite = tempSprite;
                InventoryController.statToggle = true;
                InventoryController.equipToggle = false;
                InventoryController.equip.gameObject.SetActive(false);
                InventoryController.deckToggle = false;
                InventoryController.deck.gameObject.SetActive(false);
            }
        }

        if(InputManager.J_DPadHorizontal() > 0 && InventoryController.statToggle && !InventoryController.invHUDMade && !InventoryController.padActive)
        {
            click.Play();
            InventoryController.stats.gameObject.SetActive(false);
            Sprite tempSprite = Resources.Load<Sprite>("Inventory/Inventory");
            InventoryController.invImage.sprite = tempSprite;
            InventoryController.statToggle = false;
            InventoryController.selectTab = 0;
            InventoryController.padX = 0;
            InventoryController.padActive = true;
            StartCoroutine(InputBuffer());
        }
        else if(InputManager.J_DPadHorizontal() > 0 && !InventoryController.statToggle && !InventoryController.invHUDMade && !InventoryController.padActive)
        {
            InventoryController.stats.gameObject.SetActive(true);
            click.Play();
            Sprite tempSprite = Resources.Load<Sprite>("Inventory/Inv_4");
            InventoryController.invImage.sprite = tempSprite;
            InventoryController.statToggle = true;
            InventoryController.equipToggle = false;
            InventoryController.equip.gameObject.SetActive(false);
            InventoryController.deckToggle = false;
            InventoryController.deck.gameObject.SetActive(false);
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
