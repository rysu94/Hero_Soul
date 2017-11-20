﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlcUpgrade : MonoBehaviour
{
    public AudioSource click;
    public Animator buttonAnim;
    public GameObject alcConfirm;

    public GameObject shopHUD;
    public GameObject shopDialogue;

    public GameObject shopUpgrade;
    public GameObject shopCustom;
    public GameObject shopMenu;

    public bool isClicked = false;
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = Camera.main.transform.position.z;
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if(Input.GetMouseButtonDown(0))
        {
            if (hit.collider != null && hit.collider.tag == "Shop_Upgrade")
            {
                buttonAnim.Play("Button");
                click.Play();
                shopDialogue.GetComponent<ShopDialogue>().Clear();
                shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Good choice, what would you like to do?", "NPC/NPC_Shopkeeper", "Shopkeeper",0.9f));
                shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
                shopUpgrade.SetActive(true);
                shopCustom.SetActive(false);
                shopMenu.SetActive(false);
                alcConfirm.SetActive(false);
                PotionManager.inUpgrade = false;
            }
        }
    }
}
