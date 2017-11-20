using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcanaDeck_Controller : MonoBehaviour
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

        if(hit.collider != null && hit.collider.tag == "Deck_Tab")
        {
            if (Input.GetMouseButtonDown(0) && InventoryController.deckToggle && !InventoryController.inSpellbook)
            {
                click.Play();
                InventoryController.deck.gameObject.SetActive(false);
                Sprite tempSprite = Resources.Load<Sprite>("Inventory/Inventory");
                InventoryController.invImage.sprite = tempSprite;
                InventoryController.deckToggle = false;
            }

            else if (Input.GetMouseButtonDown(0) && !InventoryController.deckToggle && !InventoryController.inSpellbook)
            {
                InventoryController.deck.gameObject.SetActive(true);
                click.Play();
                Sprite tempSprite = Resources.Load<Sprite>("Inventory/Inv_3");
                InventoryController.invImage.sprite = tempSprite;
                InventoryController.deckToggle = true;
                InventoryController.equipToggle = false;
                InventoryController.equip.gameObject.SetActive(false);
                InventoryController.statToggle = false;
                InventoryController.stats.gameObject.SetActive(false);
            }
        }
    }
}
