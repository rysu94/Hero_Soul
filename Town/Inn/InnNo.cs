using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnNo : MonoBehaviour
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
            if (hit.collider != null && hit.collider.tag == "Shop_No" && !isClicked)
            {
                shopDialogue.GetComponent<ShopDialogue>().Clear();
                shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("How can I help you?", "NPC/NPC_Shopkeeper", "Shopkeeper", 0.9f));
                shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
                shopChoices.SetActive(true);
                restChoices.SetActive(false);
            }
        }
    }
}
