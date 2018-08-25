using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUD_Menu : MonoBehaviour
{
    public Button questButton;
    public GameObject questMenu;
    public Button spellButton;
    public GameObject spellMenu;
    public Button menuButton;
    public GameObject gameMenu;
    public Button bestButton;
    public GameObject bestMenu;

    public GameObject blackMask;

    public bool padActive = false;

    public GameObject newQuest;

    // Use this for initialization
    void Start ()
    {
        questButton.onClick.AddListener(OpenQuest);
        spellButton.onClick.AddListener(OpenSpell);
        menuButton.onClick.AddListener(OpenMenu);
        bestButton.onClick.AddListener(OpenBest);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMenu();
        }

        //Controller Support

        if(GameController.xbox360Enabled() && !padActive && !InventoryController.inInv)
        {
            //print(InputManager.J_DPadHorizontal() + " " + InputManager.J_DPadVertical());

            if(Mathf.Abs(InputManager.J_DPadHorizontal()) > Mathf.Abs(InputManager.J_DPadVertical()))
            {
                if (InputManager.J_DPadHorizontal() < 0)
                {
                    GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                    padActive = true;
                    StartCoroutine(PadBuffer());
                    OpenQuest();
                }
                else if (InputManager.J_DPadHorizontal() > 0)
                {
                    GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                    padActive = true;
                    StartCoroutine(PadBuffer());
                    OpenBest();
                }
            }

            if(Mathf.Abs(InputManager.J_DPadHorizontal()) < Mathf.Abs(InputManager.J_DPadVertical()))
            {
                if (InputManager.J_DPadVertical() > 0)
                {
                    GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                    padActive = true;
                    StartCoroutine(PadBuffer());
                    OpenSpell();      
                }
            }
        }



    }

    void OpenQuest()
    {
        GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
        InventoryController.inSpellbook = false;
        if (questMenu.activeInHierarchy)
        {
            TestCharController.inDialogue = false;
            GameController.paused = false;
            questMenu.SetActive(false);
            blackMask.SetActive(false);
            newQuest.SetActive(false);
            QuestManager.questActive = false;
        }
        else
        {
            blackMask.SetActive(true);
            TestCharController.inDialogue = true;
            GameController.paused = true;
            questMenu.SetActive(true);
            spellMenu.SetActive(false);
            gameMenu.SetActive(false);
            bestMenu.SetActive(false);

        }
        
    }

    void OpenSpell()
    {
        GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
        if (spellMenu.activeInHierarchy)
        {
            TestCharController.inDialogue = false;
            GameController.paused = false;
            spellMenu.SetActive(false);
            blackMask.SetActive(false);
            StartCoroutine(PadBuffer());
            InventoryController.inSpellbook = false;
        }
        else
        {
            blackMask.SetActive(true);
            TestCharController.inDialogue = true;
            GameController.paused = true;
            spellMenu.SetActive(true);
            questMenu.SetActive(false);
            gameMenu.SetActive(false);
            bestMenu.SetActive(false);
            InventoryController.inSpellbook = true;
        }
    }

    void OpenMenu()
    {
        GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
        InventoryController.inSpellbook = false;
        if (gameMenu.activeInHierarchy)
        {
            TestCharController.inDialogue = false;
            GameController.paused = false;
            gameMenu.SetActive(false);
            blackMask.SetActive(false);
        }
        else
        {
            blackMask.SetActive(true);
            TestCharController.inDialogue = true;
            GameController.paused = true;
            gameMenu.SetActive(true);
            questMenu.SetActive(false);
            spellMenu.SetActive(false);
            bestMenu.SetActive(false);
        }
    }

    void OpenBest()
    {
        GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
        if (bestMenu.activeInHierarchy)
        {
            TestCharController.inDialogue = false;
            GameController.paused = false;
            bestMenu.SetActive(false);
            blackMask.SetActive(false);
        }
        else
        {
            blackMask.SetActive(true);
            TestCharController.inDialogue = true;
            GameController.paused = true;
            bestMenu.SetActive(true);
            gameMenu.SetActive(false);
            questMenu.SetActive(false);
            spellMenu.SetActive(false);
        }
    }

    IEnumerator PadBuffer()
    {
        yield return new WaitForSeconds(.5f);
        padActive = false;
    }
}
