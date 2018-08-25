using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GuildBoard : MonoBehaviour
{
    public GameObject interactText;
    public GameObject interactPrefab;
    public bool textMade = false;

    public GameObject guildHUD;


    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float distance = Vector2.Distance(new Vector2(transform.localPosition.x, transform.localPosition.y), TestCharController.player.transform.position);
        //print(distance);
        if (distance < .35 && !textMade)
        {
            interactText = Instantiate(interactPrefab, new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity);
            textMade = true;
        }
        else if (distance >= .35)
        {
            Destroy(interactText);
            textMade = false;
        }

        if (distance < .35 && Input.GetKeyDown(KeyCode.F) && !TestCharController.inDialogue)
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            guildHUD.SetActive(true);
            TestCharController.inDialogue = true;
            guildHUD.transform.Find("GuildController").GetComponent<Guild_Controller>().UpdateQuests();
        }
    }
}
