﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InnRest : MonoBehaviour
{
    public AudioSource click;
    public Animator buttonAnim;

    public GameObject shopHUD;
    public GameObject shopDialogue;

    public GameObject wipeScreen;

    public bool isClicked = false;

    public GameObject shopChoices;
    public GameObject restChoices;

	// Update is called once per frame
	void Update ()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = Camera.main.transform.position.z;
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (Input.GetMouseButtonDown(0))
        {
            if (hit.collider != null && hit.collider.tag == "Shop_Rest" && !isClicked)
            {
                /*
                click.Play();
                shopDialogue.GetComponent<ShopDialogue>().Clear();
                shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("It's 15 Gold to stay the night. Is that okay? ", "NPC/NPC_Shopkeeper", "Shopkeeper",0.9f));
                shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
                shopChoices.SetActive(false);
                restChoices.SetActive(true);
                */
                StartCoroutine(RestRoutine());
            }
        }
    }

    IEnumerator RestRoutine()
    {
        isClicked = true;
        click.Play();
        buttonAnim.Play("Button");
        shopDialogue.GetComponent<ShopDialogue>().Clear();
        shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Excellent choice, right this way.", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Shopkeeper", "Shopkeeper", 0.9f, -1));
        shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
        yield return new WaitForSeconds(1.5f);
        wipeScreen.GetComponent<Animator>().Play("FadeOut");

        for(int i = 0; i < 10; i++)
        {
            GameObject.Find("BGM").GetComponent<AudioSource>().volume -= .01f;
            yield return new WaitForSeconds(.3f);
        }

        //yield return new WaitForSeconds(3f);

        TestCharController.inDialogue = false;
        SceneManager.LoadScene("Town_Inn");
        LevelCreator.playerStartX = 0;
        LevelCreator.playerStartY = -0.8f;
        LevelCreator.startTag = "Up";
        
    }
}
