using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnLeave : MonoBehaviour
{
    public AudioSource click;
    public Animator buttonAnim;

    public GameObject shopHUD;
    public GameObject shopDialogue;

    public GameObject wipeScreen;

    public bool isClicked = false;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = Camera.main.transform.position.z;
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if(Input.GetMouseButtonDown(0))
        {
            if (hit.collider != null && hit.collider.tag == "Shop_Leave" && !isClicked)
            {
                StartCoroutine(LeaveRoutine());
            }
        }
    }

    IEnumerator LeaveRoutine()
    {
        isClicked = true;
        buttonAnim.Play("Button");
        click.Play();
        shopDialogue.SetActive(true);
        shopDialogue.GetComponent<ShopDialogue>().Clear();
        shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Come again!", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Shopkeeper", "Shopkeeper", 0.9f, -1));
        shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
        yield return new WaitForSeconds(1.5f);
        wipeScreen.GetComponent<Animator>().Play("FadeIn");
        shopHUD.SetActive(false);
        TestCharController.inDialogue = false;
        isClicked = false;
        TestCharController.player.transform.position = new Vector2(1.416f, .469f);
        TestCharController.player.GetComponent<Animator>().Play("TestDownIdle");
    }
}
