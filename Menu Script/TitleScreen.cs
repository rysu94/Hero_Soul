using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public bool active = false;
    public GameObject BGM;
    public GameObject anyKey, lights, circles, menu, flash;

    public Button play, quit;
    public List<GameObject> menuItems = new List<GameObject>();

    
    public bool cursorBuffer = false;
    public int index;
    public GameObject cursor, cursorMenu;

	// Use this for initialization
	void Start ()
    {
        if (GameController.xbox360Enabled())
        {
            anyKey.GetComponent<Text>().text = "Press A Button";
        }

        play.onClick.AddListener(PlayGame);
        menuItems.Add(play.gameObject);
        quit.onClick.AddListener(QuitGame);
        menuItems.Add(quit.gameObject);
        index = -1;
        StartCoroutine(PlayRoutine());
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(GameController.xbox360Enabled())
        {
            UpdateCursor();
            if (InputManager.A_Button() && active && index == -1)
            {
                GetComponent<Animator>().Play("Title_Cover_Wipe");
                GetComponent<AudioSource>().Play();
                anyKey.SetActive(false);
                StartCoroutine(ShowMenu());
                active = false;
                
            }

            else if (InputManager.A_Button() && !active && index > -1)
            {
                switch(index)
                {
                    default:
                        break;
                    case 0:
                        PlayGame();
                        break;
                    case 1:
                        QuitGame();
                        break;

                }
            }

            if(InputManager.MainVertical() > 0 && !cursorBuffer)
            {
                index--;
                if(index < 0)
                {
                    index = 1;
                }
                cursorBuffer = true;
                cursorMenu.GetComponent<AudioSource>().Play();
                StartCoroutine(CursorBuffer());
            }
            else if(InputManager.MainVertical() < 0 && !cursorBuffer)
            {
                index++;
                if (index > 1)
                {
                    index = 0;
                }
                cursorBuffer = true;
                cursorMenu.GetComponent<AudioSource>().Play();
                StartCoroutine(CursorBuffer());
            }

        }
        else
        {
            if(Input.anyKeyDown && active)
            {
                GetComponent<Animator>().Play("Title_Cover_Wipe");
                GetComponent<AudioSource>().Play();
                anyKey.SetActive(false);
                StartCoroutine(ShowMenu());
                active = false;
            }
        }

	}

    IEnumerator PlayRoutine()
    {
        yield return new WaitForSeconds(5.5f);
        anyKey.SetActive(true);
        active = true;
        BGM.SetActive(true);
        flash.SetActive(true);
    }

    IEnumerator ShowMenu()
    {
        yield return new WaitForSeconds(2f);
        lights.SetActive(true);
        circles.SetActive(true);
        menu.SetActive(true);
        index = 0;
    }

    void PlayGame()
    {
        SceneManager.LoadScene("Char_Select");
    }

    void QuitGame()
    {
        Application.Quit();   
    }

    IEnumerator CursorBuffer()
    {
        yield return new WaitForSeconds(.25f);
        cursorBuffer = false;
    }

    void UpdateCursor()
    {
        if(index > -1)
        {
            Destroy(cursor);
            cursor = Instantiate(Resources.Load("Prefabs/Inventory/Inv_Cursor_Title"), menuItems[index].transform) as GameObject;
        }
    }
}
