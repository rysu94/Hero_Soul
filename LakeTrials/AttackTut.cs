using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AttackTut : MonoBehaviour
{
    public GameObject tutMessage;
    public Text tutText;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!TutorialDatabase.tut4 && collision.gameObject.tag == "Player")
        {
            TutorialDatabase.tut4 = true;
            tutMessage.GetComponent<LifespanHide>().active = false;
            tutMessage.SetActive(false);
            tutText.text = "Combat Basics:\nYou can execute a light attack by pressing [LMB]. Using your light attack uses stamina which can be seen in the top left.";
            tutMessage.SetActive(true);
        }
        Destroy(gameObject);
    }
}
