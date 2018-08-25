using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnTalk : MonoBehaviour
{
    public AudioSource click;
    public Animator buttonAnim;

    public GameObject shopHUD;
    public GameObject shopDialogue;

    public bool isClicked = false;

	// Update is called once per frame
	void Update ()
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
                shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("You look new. I may not look it, but I used to be an adventurer like you. Let me give you a tip, make sure to periodically rest at inns to remove fatigue. Besides you'll be well rested afterwords. Take care of your body that's what I always say.", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Shopkeeper", "Shopkeeper", 0.9f, -1));
                shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Anyways, how can I help you?", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Shopkeeper", "Shopkeeper",0.9f, -1));
                shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
            }
        }
    }
}
