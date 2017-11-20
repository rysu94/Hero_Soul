using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InnYes : MonoBehaviour
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
            if (hit.collider != null && hit.collider.tag == "Shop_Yes" && !isClicked)
            {
                if(InventoryManager.playerGold >= 0)
                {
                    StartCoroutine(RestRoutine());
                }
                else
                {
                    shopDialogue.GetComponent<ShopDialogue>().Clear();
                    shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Sorry you don't have enough Gold.", "NPC/NPC_Shopkeeper", "Shopkeeper", 0.9f));
                    shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
                    shopChoices.SetActive(true);
                    restChoices.SetActive(false);
                }
                
            }
        }
    }

    IEnumerator RestRoutine()
    {
        isClicked = true;
        click.Play();
        buttonAnim.Play("Button");
        shopDialogue.GetComponent<ShopDialogue>().Clear();
        shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Excellent choice, right this way.", "NPC/NPC_Shopkeeper", "Shopkeeper", 0.9f));
        shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
        yield return new WaitForSeconds(1.5f);
        wipeScreen.GetComponent<Animator>().Play("FadeOut");
        yield return new WaitForSeconds(3f);

        TestCharController.inDialogue = false;
        SceneManager.LoadScene("Town_Inn");
        LevelCreator.playerStartX = 0;
        LevelCreator.playerStartY = -0.8f;
        LevelCreator.startTag = "Up";
        /*
        isClicked = false;
        shopDialogue.GetComponent<ShopDialogue>().Clear();
        shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Good Morning, how can I help you?", "NPC/NPC_Shopkeeper", 0.9f));
        shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
        wipeScreen.GetComponent<Animator>().Play("FadeIn");
        shopChoices.SetActive(true);
        restChoices.SetActive(false);
        */
    }
}
