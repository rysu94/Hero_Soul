using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShopProcessing : MonoBehaviour
{

    public GameObject buyPrompt;
    public Text itemAmountText;
    public static int itemAmount;
    Item[] shopkeeperInv = new Item[27];
    public int itemIndex;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //raycast to see if its above an item slot
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = Camera.main.transform.position.z;
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if(hit.collider != null && hit.collider.tag == "Destroy_Item_Yes")
        {
            if (Input.GetMouseButtonDown(0))
            {
                buyPrompt.SetActive(false);
            }
        }
        else if(hit.collider != null && hit.collider.tag == "Destroy_Item_No")
        {
            if (Input.GetMouseButtonDown(0))
            {
                buyPrompt.SetActive(false);
            }
        }
        else if(hit.collider != null && hit.collider.tag == "Destroy_Item_Add")
        {
            if(Input.GetMouseButtonDown(0) && itemAmount < shopkeeperInv[itemIndex].itemQuantity)
            {
                itemAmount++;
            }
        }
        else if(hit.collider != null && hit.collider.tag == "Destroy_Item_Sub")
        {
            if (Input.GetMouseButtonDown(0) && itemAmount > 1)
            {
                itemAmount--;
            }
        }
    }

    public void openPrompt(Item[] shopInv, int index)
    {
        buyPrompt.SetActive(true);
        itemAmount = 1;
        shopkeeperInv = shopInv;
        itemIndex = index;
    }
}
