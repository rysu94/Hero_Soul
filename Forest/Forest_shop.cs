using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Forest_shop : MonoBehaviour
{
    public GameObject interactText;
    public GameObject interactPrefab;
    public bool textMade = false;

    public GameObject shopHud;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float distance = Vector2.Distance(new Vector2(transform.localPosition.x, transform.localPosition.y), TestCharController.player.transform.position);
        //print(distance);
        if (distance < .5 && !textMade)
        {
            if(GameController.xbox360Enabled())
            {
                interactText = Instantiate(Resources.Load("Prefabs/Interact_XBox") as GameObject, new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity);
            }
            else
            {
                interactText = Instantiate(interactPrefab, new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity);
            }         
            textMade = true;
        }
        else if (distance >= .5)
        {
            Destroy(interactText);
            textMade = false;
        }

        if (distance < .5 && InputManager.A_Button() && !TestCharController.inDialogue && !InventoryController.inInv)
        {
            GetComponent<Shop_Database>().initDB();

            TestCharController.inDialogue = true;
            //Read the special Items
            Shop_Database.saleList[3] = LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].specialShopInv;
            /*
            for(int i = 0; i < LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].specialShopInv.Count; i++)
            {
                print(LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].specialShopInv[i] + " ");
            }
            */
            Shop_Controller.shopIndex = 3;
            shopHud.SetActive(true);
            GameObject.Find("ShopController").GetComponent<Shop_Controller>().UpdateShop();
        }
    }

}
