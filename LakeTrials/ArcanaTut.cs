using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ArcanaTut : MonoBehaviour {

    public GameObject tutMessage;
    public Text tutText;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!TutorialDatabase.tut9 && TutorialDatabase.tut8 && collision.gameObject.tag == "Player")
        {
            TutorialDatabase.tut9 = true;
            tutMessage.GetComponent<LifespanHide>().active = false;
            tutMessage.SetActive(false);
            tutText.text = "Arcana Basics:\nScroll up and down the middle mouse wheel or use [C] and [V] to select an Arcana Card. To use your selected card either press [MMB] or [Q].";
            tutMessage.SetActive(true);
        }
        Destroy(gameObject);
    }
}
