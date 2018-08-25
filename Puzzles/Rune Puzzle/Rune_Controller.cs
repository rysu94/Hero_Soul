using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune_Controller : MonoBehaviour
{
    public GameObject glow, particles;

    public GameObject interactText;
    public bool textMade = false;

    public int runeIndex;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        float distance = Vector3.Distance(transform.position, TestCharController.player.transform.position);
        //print(distance);
        if (distance < .35 && !textMade && !GetComponentInParent<Rune_Puzzle>().cleared)
        {
            if (GameController.xbox360Enabled())
            {
                interactText = Instantiate(Resources.Load("Prefabs/Interact_XBox") as GameObject, new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity);
            }
            else
            {
                interactText = Instantiate(Resources.Load("Prefabs/Interact") as GameObject, new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity);
            }
            textMade = true;
        }
        else if (distance >= .35)
        {
            Destroy(interactText);
            textMade = false;
        }

        //If interact is pressed
        if (distance < .5 && (InputManager.A_Button() || Input.GetKeyDown(KeyCode.F)) && !GetComponentInParent<Rune_Puzzle>().cleared)
        {
            //GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            ActivateRune();
        }
    }

    public void DeactivateRune()
    {
        glow.SetActive(false);
        particles.SetActive(false);
    }

    public void ActivateRune()
    {
        if(GetComponentInParent<Rune_Puzzle>().runeCombo[GetComponentInParent<Rune_Puzzle>().currentIndex] == runeIndex)
        {
            GetComponent<AudioSource>().Play();
            glow.SetActive(true);
            particles.SetActive(true);
            GetComponentInParent<Rune_Puzzle>().currentIndex++;
        }
        else
        {
            GetComponentInParent<Rune_Puzzle>().DeactivateRunes();
            GetComponentInParent<Rune_Puzzle>().error.GetComponent<AudioSource>().Play();
        }
    }
}
