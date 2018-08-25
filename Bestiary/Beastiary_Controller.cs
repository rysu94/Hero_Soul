using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class Beastiary_Controller : MonoBehaviour
{
    public static int pageNumber = 0;
    public static int minPageNumber = 0;
    public static int maxPageNumber = 2;
    public Button nextPage;
    public Button backPage;
    public Text pageText;


    public Button[] beastiaryButtons = new Button[15];

    public GameObject beastPage;

    public Image background;
    public Image foreground;
    public Image monsterSprite;

    public Text monsterName;
    public Text monsterHealth;
    public Text monsterWeak;
    public Text monsterDrops;
    public Text monsterArcana;
    public Text monsterDesc;

    public Button beastPageClose;

    //Controller Support
    public int selectX, selectY;
    public int selectIndex;

    public GameObject selectObj;
    public bool padActive = false;

	// Use this for initialization
	void Start ()
    {
        GetComponent<Bestiary_Database>().InitDB();
		for(int i = 0; i < 15; i++)
        {
            if(Bestiary_Database.monsterDB[(15 * pageNumber) + i].monsterName != "Empty" && Bestiary_Database.monsterDB[(15 * pageNumber) + i].discovered)
            {
                beastiaryButtons[i].transform.Find("MonsterName").GetComponent<Text>().text = Bestiary_Database.monsterDB[(15 * pageNumber) + i].monsterName;
                beastiaryButtons[i].transform.Find("Monster_IMG").GetComponent<Image>().sprite = Resources.Load<Sprite>("Bestiary/Enemies/" + 
                    Bestiary_Database.monsterDB[(15 * pageNumber) + i].monsterSprite);                   
            }
            else
            {
                beastiaryButtons[i].transform.Find("MonsterName").GetComponent<Text>().text = "???";
                beastiaryButtons[i].transform.Find("Monster_IMG").GetComponent<Image>().sprite = Resources.Load<Sprite>("Bestiary/Enemies/None");
            }
            beastiaryButtons[i].transform.Find("MonsterID").GetComponent<Text>().text = ((15 * pageNumber) + i + 1).ToString();
            beastiaryButtons[i].GetComponent<Bestiary_Slab>().monsterID = (15 * pageNumber) + i;
            beastiaryButtons[i].onClick.AddListener(OpenMonsterPage);
        }
        beastPageClose.onClick.AddListener(CloseMonsterPage);
        nextPage.onClick.AddListener(NextPage);
        backPage.onClick.AddListener(BackPage);
	}

    // Update is called once per frame
    void Update()
    {
        //Controller Support
        if(GameController.xbox360Enabled())
        {
            ChangePage();
            ScrollEntry();
            SelectEntry();
        }

    }

    void OpenMonsterPage()
    {
        //print(EventSystem.current.currentSelectedGameObject.GetComponent<Bestiary_Slab>().monsterID);
        GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
        int index = EventSystem.current.currentSelectedGameObject.GetComponent<Bestiary_Slab>().monsterID;

        if(Bestiary_Database.monsterDB[index].discovered)
        {
            beastPage.SetActive(true);
            background.sprite = Resources.Load<Sprite>("Bestiary/" + Bestiary_Database.monsterDB[index].background);
            foreground.sprite = Resources.Load<Sprite>("Bestiary/" + Bestiary_Database.monsterDB[index].foreground);
            monsterSprite.sprite = Resources.Load<Sprite>("Bestiary/Enemies/" + Bestiary_Database.monsterDB[index].monsterSprite);

            monsterName.text = Bestiary_Database.monsterDB[index].monsterName;
            monsterHealth.text = "<color=yellow>Health: </color>" + Bestiary_Database.monsterDB[index].monsterHealth;
            monsterWeak.text = "<color=yellow>Weakness: </color>" + Bestiary_Database.monsterDB[index].monsterWeakness;
            monsterDrops.text = "<color=yellow>Item Drops:</color>\n" + Bestiary_Database.monsterDB[index].monsterDrop;
            monsterArcana.text = "<color=yellow>Arcana Drops:</color>\n" + Bestiary_Database.monsterDB[index].monsterArcana;

            monsterDesc.text = Bestiary_Database.monsterDB[index].monsterDesc;
        }
        else
        {
            CloseMonsterPage();
        }

    }

    void CloseMonsterPage()
    {
        GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
        beastPage.SetActive(false);
    }

    void NextPage()
    {
        if(pageNumber < 2)
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            pageNumber++;
            pageText.text = "Page " + (pageNumber+1);
            UpdatePage();
        }
        else
        {
            GameObject.Find("ErrorNoise").GetComponent<AudioSource>().Play();
        }
    }

    void BackPage()
    {
        if(pageNumber > 0)
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            pageNumber--;
            pageText.text = "Page " + (pageNumber+1);
            UpdatePage();
        }
        else
        {
            GameObject.Find("ErrorNoise").GetComponent<AudioSource>().Play();
        }
    }

    public void UpdatePage()
    {
        for (int i = 0; i < 15; i++)
        {
            if (Bestiary_Database.monsterDB[(15 * pageNumber) + i].monsterName != "Empty" && Bestiary_Database.monsterDB[(15 * pageNumber) + i].discovered)
            {
                beastiaryButtons[i].transform.Find("MonsterName").GetComponent<Text>().text = Bestiary_Database.monsterDB[(15 * pageNumber) + i].monsterName;
                beastiaryButtons[i].transform.Find("Monster_IMG").GetComponent<Image>().sprite = Resources.Load<Sprite>("Bestiary/Enemies/" +
                    Bestiary_Database.monsterDB[(15 * pageNumber) + i].monsterSprite);
            }
            else
            {
                beastiaryButtons[i].transform.Find("MonsterName").GetComponent<Text>().text = "???";
                beastiaryButtons[i].transform.Find("Monster_IMG").GetComponent<Image>().sprite = Resources.Load<Sprite>("Bestiary/Enemies/None");
            }
            beastiaryButtons[i].transform.Find("MonsterID").GetComponent<Text>().text = ((15 * pageNumber) + i + 1).ToString();
            beastiaryButtons[i].GetComponent<Bestiary_Slab>().monsterID = (15 * pageNumber) + i;
            beastiaryButtons[i].onClick.AddListener(OpenMonsterPage);
        }
    }

    //Controller Support
    public void ChangePage()
    {
        if(InputManager.R_Bumper())
        {
            NextPage();
            selectX = 0;
            selectY = 0;
        }
        else if(InputManager.L_Bumper())
        {
            BackPage();
            selectX = 0;
            selectY = 0;
        }
    }

    public void ScrollEntry()
    {
        if(Mathf.Abs(InputManager.MainHorizontal()) > Mathf.Abs(InputManager.MainVertical()) && !padActive)
        {
            if(InputManager.MainHorizontal() > .5f)
            {
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                selectX++;
                if(selectX > 4)
                {
                    selectX = 0;
                }
                padActive = true;
                StartCoroutine(PadBuffer());
            }
            else if(InputManager.MainHorizontal() < -.5f)
            {
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                selectX--;
                if(selectX < 0)
                {
                    selectX = 4;
                }
                padActive = true;
                StartCoroutine(PadBuffer());
            }
        }
        else if(Mathf.Abs(InputManager.MainVertical()) > Mathf.Abs(InputManager.MainHorizontal()) && !padActive)
        {
            if(InputManager.MainVertical() < -.5f)
            {
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                selectY++;
                if(selectY > 2)
                {
                    selectY = 0;
                }
                padActive = true;
                StartCoroutine(PadBuffer());
            }
            else if(InputManager.MainVertical() > .5f)
            {
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                selectY--;
                if(selectY < 0)
                {
                    selectY = 2;
                }
                padActive = true;
                StartCoroutine(PadBuffer());
            }
        }

        Destroy(selectObj);
        selectIndex = (selectY * 5) + selectX;
        selectObj = Instantiate(Resources.Load("Prefabs/Inventory/Best_Select"), beastiaryButtons[selectIndex].gameObject.transform) as GameObject;
    }

    public void SelectEntry()
    {
        if(InputManager.A_Button())
        {
            //print(EventSystem.current.currentSelectedGameObject.GetComponent<Bestiary_Slab>().monsterID);
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            int index = selectIndex;

            if (Bestiary_Database.monsterDB[index].discovered)
            {
                beastPage.SetActive(true);
                background.sprite = Resources.Load<Sprite>("Bestiary/" + Bestiary_Database.monsterDB[index].background);
                foreground.sprite = Resources.Load<Sprite>("Bestiary/" + Bestiary_Database.monsterDB[index].foreground);
                monsterSprite.sprite = Resources.Load<Sprite>("Bestiary/Enemies/" + Bestiary_Database.monsterDB[index].monsterSprite);

                monsterName.text = Bestiary_Database.monsterDB[index].monsterName;
                monsterHealth.text = "<color=yellow>Health: </color>" + Bestiary_Database.monsterDB[index].monsterHealth;
                monsterWeak.text = "<color=yellow>Weakness: </color>" + Bestiary_Database.monsterDB[index].monsterWeakness;
                monsterDrops.text = "<color=yellow>Item Drops:</color>\n" + Bestiary_Database.monsterDB[index].monsterDrop;
                monsterArcana.text = "<color=yellow>Arcana Drops:</color>\n" + Bestiary_Database.monsterDB[index].monsterArcana;

                monsterDesc.text = Bestiary_Database.monsterDB[index].monsterDesc;
            }
            else
            {
                CloseMonsterPage();
            }
        }
    }

    IEnumerator PadBuffer()
    {
        yield return new WaitForSeconds(.25f);
        padActive = false;
    }

}
