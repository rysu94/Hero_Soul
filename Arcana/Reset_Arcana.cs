using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset_Arcana : MonoBehaviour {

    public GameObject interactText;
    public GameObject interactPrefab;
    public bool textMade = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Vector2.Distance(new Vector2(transform.localPosition.x, transform.localPosition.y), TestCharController.player.transform.position);
        //print(distance);
        if (distance < .5 && !textMade)
        {
            if (GameController.xbox360Enabled())
            {
                interactText = Instantiate(Resources.Load("Prefabs/Interact_XBox"), new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity) as GameObject;
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

        if (distance < .5 && InputManager.A_Button() && !TestCharController.inDialogue)
        {
            Deck.ResetDeck();
        }
    }
}
