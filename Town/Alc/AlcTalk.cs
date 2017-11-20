using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlcTalk : MonoBehaviour
{

    public AudioSource click;
    public Animator buttonAnim;

    public GameObject shopHUD;
    public GameObject shopDialogue;

    public bool isClicked = false;

    // Update is called once per frame
    void Update()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = Camera.main.transform.position.z;
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (Input.GetMouseButtonDown(0))
        {
            if (hit.collider != null && hit.collider.tag == "Shop_Talk" && !isClicked)
            {
                buttonAnim.Play("Button");
                click.Play();
                shopDialogue.GetComponent<ShopDialogue>().Clear();
                shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Are you new to Alchemy? If you are, you should learn. Alchemy is a truly useful thing to learn. It allows you to create and upgrade potions.", "NPC/NPC_Shopkeeper", "Shopkeeper",0.9f));
                shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Anyways, how can I help you?", "NPC/NPC_Shopkeeper", "Shopkeeper",0.9f));
                shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
            }
        }
    }

}
