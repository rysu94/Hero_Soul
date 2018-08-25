using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class QuestMenu : MonoBehaviour
{
    public Button closeButton;
    public Button closeButtonSpell;
    public Button closeButtonBest;
    public GameObject blackMask;

    public bool padActive = false;
    public GameObject newQuest;


    // Use this for initialization
    void Start ()
    {
        closeButton.onClick.AddListener(CloseMenu);
        closeButtonSpell.onClick.AddListener(CloseSpell);
        closeButtonBest.onClick.AddListener(CloseBest);
	}

	
	// Update is called once per frame
	void Update ()
    {
        //Controller Support
		if(GameController.xbox360Enabled() && !padActive && !InventoryController.inInv)
        {
            if(Mathf.Abs(InputManager.J_DPadHorizontal()) > Mathf.Abs(InputManager.J_DPadVertical()))
            {
                if (InputManager.J_DPadHorizontal() < 0)
                {
                    GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                    padActive = true;
                    StartCoroutine(PadBuffer());
                    CloseMenu();
                }
                else if (InputManager.J_DPadHorizontal() > 0)
                {
                    GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                    padActive = true;
                    StartCoroutine(PadBuffer());
                    CloseBest();
                }
            }

            else if(Mathf.Abs(InputManager.J_DPadHorizontal()) < Mathf.Abs(InputManager.J_DPadVertical()))
            {
                if (InputManager.J_DPadVertical() > 0)
                {
                    GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                    padActive = true;
                    StartCoroutine(PadBuffer());
                    InventoryController.inSpellbook = false;
                    CloseSpell();
                    
                }
            }
        }
	}

    void CloseMenu()
    {
        GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
        gameObject.SetActive(false);
        TestCharController.inDialogue = false;
        blackMask.SetActive(false);
        GameController.paused = false;
        InventoryController.inSpellbook = false;
        newQuest.SetActive(false);
        QuestManager.questActive = false;
    }

    void CloseSpell()
    {
        GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
        gameObject.SetActive(false);
        TestCharController.inDialogue = false;
        blackMask.SetActive(false);
        GameController.paused = false;
        InventoryController.inSpellbook = false;
    }

    void CloseBest()
    {
        GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
        gameObject.SetActive(false);
        TestCharController.inDialogue = false;
        blackMask.SetActive(false);
        GameController.paused = false;
        InventoryController.inSpellbook = false;
    }

    IEnumerator PadBuffer()
    {
        yield return new WaitForSeconds(.5f);
        padActive = false;
    }
}
