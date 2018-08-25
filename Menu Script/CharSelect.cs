using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CharSelect : MonoBehaviour
{
    public Button ceciliaButton;
    public GameObject cecilia;
    public Button leonButton;
    public GameObject leon;
    public Button risetteButton;
    public GameObject risette;
    public Button sparrowButton;
    public GameObject sparrow;

    public string currentChar;
    public GameObject charObj;

    public GameObject info;
    public Text nameText;
    public Text wepText;
    public Text specText;
    public Text moveText;

    public Button confirmButton;

    public bool padActive = false;
    public int charX, charY;
    public int lastX, lastY;
    public GameObject cursor;

	// Use this for initialization
	void Start ()
    {
        if (GameController.xbox360Enabled())
        {
            charX = 0;
            charY = 0;
            UpdateCursor();
        }
        currentChar = "";
        ceciliaButton.onClick.AddListener(SelectCecilia);
        leonButton.onClick.AddListener(SelectLeon);
        risetteButton.onClick.AddListener(SelectRisette);
        sparrowButton.onClick.AddListener(SelectSparrow);
        confirmButton.onClick.AddListener(StartGame);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(GameController.xbox360Enabled())
        {
            if (InputManager.MainHorizontal() < 0 && !padActive && charX > -1)
            {
                GetComponent<AudioSource>().Play();
                charX--;
                if (charX < 0)
                {
                    charX = 1;
                }
                padActive = true;
                StartCoroutine(PadBuffer());
                UpdateCursor();
            }
            else if (InputManager.MainHorizontal() > 0 && !padActive && charX > -1)
            {
                GetComponent<AudioSource>().Play();
                charX++;
                if (charX > 1)
                {
                    charX = 0;
                }
                padActive = true;
                StartCoroutine(PadBuffer());
                UpdateCursor();
            }
            else if (InputManager.MainVertical() < 0 && !padActive && charY > -1)
            {
                GetComponent<AudioSource>().Play();
                charY--;
                if (charY < 0)
                {
                    charY = 1;
                }
                padActive = true;
                StartCoroutine(PadBuffer());
                UpdateCursor();
            }
            else if (InputManager.MainVertical() > 0 && !padActive && charY > -1)
            {
                GetComponent<AudioSource>().Play();
                charY++;
                if(charY > 1)
                {
                    charY = 0;
                }
                padActive = true;
                StartCoroutine(PadBuffer());
                UpdateCursor();
            }

            if(InputManager.A_Button())
            {
                //Cecilia
                if (charX == 0 && charY == 0)
                {
                    GetComponent<AudioSource>().Play();
                    SelectCecilia();
                    charX = -1;
                    charY = -1;
                    lastX = 0;
                    lastY = 0;
                    UpdateCursor();
                }
                //Leon
                else if (charX == 1 && charY == 0)
                {
                    GetComponent<AudioSource>().Play();
                    SelectLeon();
                    charX = -1;
                    charY = -1;
                    lastX = 1;
                    lastY = 0;
                    UpdateCursor();
                }
                //Risette
                else if (charX == 0 && charY == 1)
                {
                    GetComponent<AudioSource>().Play();
                    SelectRisette();
                    charX = -1;
                    charY = -1;
                    lastX = 0;
                    lastY = 1;
                    UpdateCursor();
                }
                //Sparrow
                else if (charX == 1 && charY == 1)
                {
                    GetComponent<AudioSource>().Play();
                    SelectSparrow();
                    charX = -1;
                    charY = -1;
                    lastX = 1;
                    lastY = 1;
                    UpdateCursor();
                }
                else if(charX == -1 && charY == -1)
                {
                    GetComponent<AudioSource>().Play();
                    StartGame();
                }
                
            }

            if(InputManager.B_Button() && charX == -1 && charY == -1)
            {
                GetComponent<AudioSource>().Play();
                charX = lastX;
                charY = lastY;
                UpdateCursor();
            }
        }
	}

    void UpdateCursor()
    {
        Destroy(cursor);
        //Cecilia
        if (charX == 0 && charY == 0)
        {
            cursor = Instantiate(Resources.Load<GameObject>("Prefabs/Inventory/Char_Select_Cursor"), ceciliaButton.gameObject.transform);
        }
        //Leon
        else if(charX == 1 && charY == 0)
        {
            cursor = Instantiate(Resources.Load<GameObject>("Prefabs/Inventory/Char_Select_Cursor"), leonButton.gameObject.transform);
        }
        //Risette
        else if (charX == 0 && charY == 1)
        {
            cursor = Instantiate(Resources.Load<GameObject>("Prefabs/Inventory/Char_Select_Cursor"), risetteButton.gameObject.transform);
        }
        //Sparrow
        else if (charX == 1 && charY == 1)
        {
            cursor = Instantiate(Resources.Load<GameObject>("Prefabs/Inventory/Char_Select_Cursor"), sparrowButton.gameObject.transform);
        }
        else if(charX == -1 && charY == -1)
        {
            cursor = Instantiate(Resources.Load<GameObject>("Prefabs/Inventory/Char_Confirm_Cursor"), confirmButton.gameObject.transform);
        }
    }

    IEnumerator PadBuffer()
    {
        yield return new WaitForSeconds(.5f);
        padActive = false;
    }

    void StartGame()
    {
        SceneLoader.loadedScene = "Forest_1_Start";
        //SceneLoader.loadedBGM = "";
        SceneLoader.loadSprite = Resources.Load<Sprite>("Loading/Forest_1");
        SceneManager.LoadScene("LoadScreen");

        LevelCreator.newLevel = true;
        LevelCreator.playerStartX = 0;
        LevelCreator.playerStartY = -1.3f;
        LevelCreator.startTag = "Up";
        CameraController.lockCamera = false;
    }

    void SelectCecilia()
    {
        if(currentChar != "" && currentChar != "Ceclia")
        {
            charObj.GetComponent<Animator>().Play(currentChar + "_ToBack");
        }
        info.SetActive(true);
        nameText.text = "Cecilia Grey";
        wepText.text = "<color=yellow>Weapon:</color> Spear";
        specText.text = "<color=yellow>Special Attack:</color> Spear Dash";
        moveText.text = "<color=yellow>Move Skill:</color> Dash";
        cecilia.GetComponent<Animator>().Play("Ceclia_ToFront");
        cecilia.GetComponent<AudioSource>().Play();
        currentChar = "Ceclia";
        charObj = cecilia;
    }

    void SelectLeon()
    {
        if (currentChar != "" && currentChar != "Leon")
        {
            charObj.GetComponent<Animator>().Play(currentChar + "_ToBack");
        }
        info.SetActive(true);
        nameText.text = "Leon Klein";
        wepText.text = "<color=yellow>Weapon:</color> Sword & Shield";
        specText.text = "<color=yellow>Special Attack:</color> Shield Block";
        moveText.text = "<color=yellow>Move Skill:</color> None";
        leon.GetComponent<Animator>().Play("Leon_ToFront");
        leon.GetComponent<AudioSource>().Play();
        currentChar = "Leon";
        charObj = leon;
    }

    void SelectRisette()
    {
        if (currentChar != "" && currentChar != "Risette")
        {
            charObj.GetComponent<Animator>().Play(currentChar + "_ToBack");
        }
        info.SetActive(true);
        wepText.text = "<color=yellow>Weapon:</color> Bow & Arrow";
        specText.text = "<color=yellow>Special Attack:</color> Powershot";
        moveText.text = "<color=yellow>Move Skill:</color> Dodge";
        nameText.text = "Risette Walker";
        risette.GetComponent<Animator>().Play("Risette_ToFront");
        risette.GetComponent<AudioSource>().Play();
        currentChar = "Risette";
        charObj = risette;
    }

    void SelectSparrow()
    {
        if (currentChar != "" && currentChar != "Sparrow")
        {
            charObj.GetComponent<Animator>().Play(currentChar + "_ToBack");
        }
        info.SetActive(true);
        nameText.text = "Sparrow";
        wepText.text = "<color=yellow>Weapon:</color> Dual Daggers";
        specText.text = "<color=yellow>Special Attack:</color> Throwing Daggers";
        moveText.text = "<color=yellow>Move Skill:</color> Roll";
        sparrow.GetComponent<Animator>().Play("Sparrow_ToFront");
        sparrow.GetComponent<AudioSource>().Play();
        currentChar = "Sparrow";
        charObj = sparrow;
    }
}
