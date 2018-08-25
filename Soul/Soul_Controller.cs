using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Soul_Controller : MonoBehaviour
{
    public GameObject skillPanel;
    public GameObject previewPanel;

    public Text[] levelText = new Text[6];
    public Text currentLevel;
    public Text previewTier;

    public Button[] heroButtons = new Button[6];

    public Text heroName;
    public Image heroIMG;

    public Button skillConfirmButton;

    public GameObject systemMessage;
    public Text systemText;

    public GameObject tooltip;
    public Text tooltipName;
    public Text tooltipDesc;

    public Button backButton;

    public static AudioClip bgm;
    public static string backScene;
    public static float x, y;

    //Return Scene Information
    public static string sceneName;
    public static float playerX, playerY;
    public static string startTag;

    //Hero selection panel
    public GameObject heroSelectPanel, heroPreviewPanel;
    public GameObject[] tierSelect = new GameObject[6];
    public Button[] tierButtons = new Button[6];
    public int selectedTier = 0;

	// Use this for initialization
	void Start ()
    {
        Cursor.SetCursor((Texture2D)Resources.Load("Cursor"), Vector2.zero, CursorMode.Auto);

        heroButtons[0].onClick.AddListener(SelectBerserker);
        heroButtons[1].onClick.AddListener(SelectVanguard);
        heroButtons[2].onClick.AddListener(SelectAssassin);
        heroButtons[3].onClick.AddListener(SelectRanger);
        heroButtons[4].onClick.AddListener(SelectMage);
        heroButtons[5].onClick.AddListener(SelectSage);

        CheckHeros();

        skillConfirmButton.onClick.AddListener(ConfirmSkill);

        backButton.onClick.AddListener(ReturnScene);

        UpdateTree();
        UpdateSelections();
	}
	
	// Update is called once per frame
	void Update ()
    {
        ShowLevels();

        //Tooltips
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = Camera.main.transform.position.z;
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if(hit.collider!= null && hit.collider.tag == "Skill_Preview")
        {
            tooltip.SetActive(true);
            tooltipName.text = hit.collider.gameObject.GetComponent<Preview_Slot>().talentName;
            tooltipDesc.text = hit.collider.gameObject.GetComponent<Preview_Slot>().talentDesc;
            tooltip.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 5));
            tooltip.transform.position = new Vector3(tooltip.transform.position.x + 2f, tooltip.transform.position.y - .5f, tooltip.transform.position.z);
        }
        else if(hit.collider != null && hit.collider.tag == "Soul_Button")
        {
            tooltip.SetActive(true);
            tooltipName.text = hit.collider.gameObject.GetComponent<Skill_Slot>().talentName;
            tooltipDesc.text = hit.collider.gameObject.GetComponent<Skill_Slot>().talentDesc;
            tooltip.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 5));
            tooltip.transform.position = new Vector3(tooltip.transform.position.x + 2f, tooltip.transform.position.y - .5f, tooltip.transform.position.z);
        }
        else
        {
            tooltip.SetActive(false);
        }


    }

    void ReturnScene()
    {
        UpdateTalentBonus();

        //GameObject.Find("BGM").GetComponent<AudioSource>().clip = bgm;
        //GameObject.Find("BGM").GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(sceneName);
        LevelCreator.playerStartX = playerX;
        LevelCreator.playerStartY = playerY;
        LevelCreator.startTag = startTag;
        Soul_Manager.soulEnabled = true;

    }


    //Select Hero Soul
    void SelectBerserker()
    {
        Soul_Manager.heroSelected = 1;
        ResetHeroSelection();
        UpdateSkillPreview();
        heroIMG.sprite = Resources.Load<Sprite>("Soul/HeroPortaits/Berserker_Iskar");
    }
    void SelectVanguard()
    {
        Soul_Manager.heroSelected = 2;
        ResetHeroSelection();
        UpdateSkillPreview();
        heroIMG.sprite = Resources.Load<Sprite>("Soul/HeroPortaits/Hero_02");
    }
    void SelectAssassin()
    {
        Soul_Manager.heroSelected = 3;
        ResetHeroSelection();
        UpdateSkillPreview();
        heroIMG.sprite = Resources.Load<Sprite>("Soul/HeroPortaits/Hero_03");
    }
    void SelectRanger()
    {
        Soul_Manager.heroSelected = 4;
        ResetHeroSelection();
        UpdateSkillPreview();
        heroIMG.sprite = Resources.Load<Sprite>("Soul/HeroPortaits/Hero_04");
    }
    void SelectMage()
    {
        Soul_Manager.heroSelected = 5;
        ResetHeroSelection();
        UpdateSkillPreview();
        heroIMG.sprite = Resources.Load<Sprite>("Soul/HeroPortaits/Hero_05");
    }
    void SelectSage()
    {
        Soul_Manager.heroSelected = 6;
        ResetHeroSelection();
        UpdateSkillPreview();
        heroIMG.sprite = Resources.Load<Sprite>("Soul/HeroPortaits/Hero_06");
    }
    void ResetHeroSelection()
    {
        GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
        for(int i = 0; i < heroButtons.Length; i++)
        {
            heroButtons[i].gameObject.GetComponent<Soul_Slot>().heroSelected.SetActive(false);
        }

        for (int i = 0; i < heroButtons.Length; i++)
        {
            if(heroButtons[i].gameObject.GetComponent<Soul_Slot>().heroID == Soul_Manager.heroSelected)
            {
                heroButtons[i].gameObject.GetComponent<Soul_Slot>().heroSelected.SetActive(true);
                heroName.text = heroButtons[i].gameObject.GetComponent<Soul_Slot>().heroName;
            }
        }
    }

    string GetHero()
    {
        string hero;
        switch(Soul_Manager.heroSelected)
        {
            default:
                hero = "BBBB";
                break;
            case 1:
                hero = "Berserker";
                break;
            case 2:
                hero = "Vanguard";
                break;
            case 3:
                hero = "Assassin";
                break;
            case 4:
                hero = "Ranger";
                break;
            case 5:
                hero = "Mage";
                break;
            case 6:
                hero = "Sage";
                    break;
        }

        return hero;
    }

    Sprite GetButtonImage()
    {
        Sprite hero;
        switch (Soul_Manager.heroSelected)
        {
            default:
                hero = Resources.Load<Sprite>("Soul/HeroPortaits/BerserkerButton");
                break;
            case 1:
                hero = Resources.Load<Sprite>("Soul/HeroPortaits/BerserkerButton");
                break;
            case 2:
                hero = Resources.Load<Sprite>("Soul/HeroPortaits/VanguardButton");
                break;
            case 3:
                hero = Resources.Load<Sprite>("Soul/HeroPortaits/AssassinButton");
                break;
            case 4:
                hero = Resources.Load<Sprite>("Soul/HeroPortaits/RangerButton");
                break;
            case 5:
                hero = Resources.Load<Sprite>("Soul/HeroPortaits/MageButton");
                break;
            case 6:
                hero = Resources.Load<Sprite>("Soul/HeroPortaits/SageButton");
                break;
        }

        return hero;
    }

    void ConfirmSkill()
    {
        //Check if skills can be added
        if(Soul_Manager.currentTier == 0 && PlayerStats.playerLevel >= 0 && Soul_Manager.playerTalentTree.Count < 3 && Soul_Manager.heroSelected > 0)
        {
            GameObject.Find("UnlockNoise").GetComponent<AudioSource>().Play();
            GameObject.Find("HeroController").GetComponent<Soul_Manager>().LevelUp(Soul_Manager.currentTier);
            Soul_Manager.currentTier++;
            UpdateTree();
            systemMessage.SetActive(true);
            systemText.text = GetHero() + " Tier " + Soul_Manager.currentTier + " Talents Unlocked!";
            UpdateSkillPreview();
            UpdateSelections();
            tierButtons[0].GetComponent<Image>().sprite = GetButtonImage();
        }
        else if(Soul_Manager.currentTier == 1 && PlayerStats.playerLevel >= 5 && Soul_Manager.playerTalentTree.Count < 6 && Soul_Manager.heroSelected > 0)
        {
            GameObject.Find("UnlockNoise").GetComponent<AudioSource>().Play();
            GameObject.Find("HeroController").GetComponent<Soul_Manager>().LevelUp(Soul_Manager.currentTier);
            Soul_Manager.currentTier++;
            UpdateTree();
            systemMessage.SetActive(true);
            systemText.text = GetHero() + " Tier " + Soul_Manager.currentTier + " Talents Unlocked!";
            UpdateSkillPreview();
            UpdateSelections();
            tierButtons[1].GetComponent<Image>().sprite = GetButtonImage();
        }
        else if (Soul_Manager.currentTier == 2 && PlayerStats.playerLevel >= 10 && Soul_Manager.playerTalentTree.Count < 9 && Soul_Manager.heroSelected > 0)
        {
            GameObject.Find("UnlockNoise").GetComponent<AudioSource>().Play();
            GameObject.Find("HeroController").GetComponent<Soul_Manager>().LevelUp(Soul_Manager.currentTier);
            Soul_Manager.currentTier++;
            UpdateTree();
            systemMessage.SetActive(true);
            systemText.text = GetHero() + " Tier " + Soul_Manager.currentTier + " Talents Unlocked!";
            UpdateSkillPreview();
            UpdateSelections();
            tierButtons[2].GetComponent<Image>().sprite = GetButtonImage();
        }
        else if (Soul_Manager.currentTier == 3 && PlayerStats.playerLevel >= 15 && Soul_Manager.playerTalentTree.Count < 12 && Soul_Manager.heroSelected > 0)
        {
            GameObject.Find("UnlockNoise").GetComponent<AudioSource>().Play();
            GameObject.Find("HeroController").GetComponent<Soul_Manager>().LevelUp(Soul_Manager.currentTier);
            Soul_Manager.currentTier++;
            UpdateTree();
            systemMessage.SetActive(true);
            systemText.text = GetHero() + " Tier " + Soul_Manager.currentTier + " Talents Unlocked!";
            UpdateSkillPreview();
            UpdateSelections();
            tierButtons[3].GetComponent<Image>().sprite = GetButtonImage();
        }
        else if (Soul_Manager.currentTier == 4 && PlayerStats.playerLevel >= 20 && Soul_Manager.playerTalentTree.Count < 15 && Soul_Manager.heroSelected > 0)
        {
            GameObject.Find("UnlockNoise").GetComponent<AudioSource>().Play();
            GameObject.Find("HeroController").GetComponent<Soul_Manager>().LevelUp(Soul_Manager.currentTier);
            Soul_Manager.currentTier++;
            UpdateTree();
            systemMessage.SetActive(true);
            systemText.text = GetHero() + " Tier " + Soul_Manager.currentTier + " Talents Unlocked!";
            UpdateSkillPreview();
            UpdateSelections();
            tierButtons[4].GetComponent<Image>().sprite = GetButtonImage();
        }
        else if (Soul_Manager.currentTier == 5 && PlayerStats.playerLevel >= 25 && Soul_Manager.playerTalentTree.Count < 18 && Soul_Manager.heroSelected > 0)
        {
            GameObject.Find("UnlockNoise").GetComponent<AudioSource>().Play();
            GameObject.Find("HeroController").GetComponent<Soul_Manager>().LevelUp(Soul_Manager.currentTier);
            
            UpdateTree();
            
            systemMessage.SetActive(true);
            systemText.text = GetHero() + " Tier " + (Soul_Manager.currentTier + 1) + " Talents Unlocked!";
            UpdateSkillPreview();
            UpdateSelections();
            tierButtons[5].GetComponent<Image>().sprite = GetButtonImage();

            //Soul_Manager.currentTier++;
        }
        else if(Soul_Manager.heroSelected == 0)
        {
            GameObject.Find("ErrorNoise").GetComponent<AudioSource>().Play();
            systemMessage.SetActive(true);
            systemText.text = "No heroic soul selected!";
        }
        else if(Soul_Manager.playerTalentTree.Count == 18)
        {
            GameObject.Find("ErrorNoise").GetComponent<AudioSource>().Play();
            systemMessage.SetActive(true);
            systemText.text = "You are at maximum talent level!";
        }
        else
        {
            GameObject.Find("ErrorNoise").GetComponent<AudioSource>().Play();
            systemMessage.SetActive(true);
            systemText.text = "Level not high enough! Level up to unlock more talents!";
        }
        

    }


    //Update the Skill Preview
    void UpdateSkillPreview()
    {
        previewTier.text = "Tier " + (Soul_Manager.currentTier + 1) + " Preview:";

        //Clear the current skill preview
        foreach (GameObject preview in GameObject.FindGameObjectsWithTag("Skill_Preview"))
        {
            Destroy(preview);
        }

        int startIndex = Soul_Manager.currentTier * 3; 

        for(int i = 0; i < 3; i++)
        {
            GameObject tempObj = Instantiate(Resources.Load("Prefabs/Soul/Skill_Preview"), previewPanel.transform) as GameObject;
            //Berserker
            if(Soul_Manager.heroSelected == 1)
            {
                tempObj.transform.Find("Preview_IMG").GetComponent<Image>().sprite = Resources.Load<Sprite>("Soul/" + Soul_Database.talentBerserk[startIndex+i].talentIMG);
                tempObj.GetComponent<Preview_Slot>().talentName = Soul_Database.talentBerserk[startIndex + i].talentName;
                tempObj.GetComponent<Preview_Slot>().talentDesc = Soul_Database.talentBerserk[startIndex + i].talentDesc;
            }
            //Vanguard
            else if(Soul_Manager.heroSelected == 2)
            {
                tempObj.transform.Find("Preview_IMG").GetComponent<Image>().sprite = Resources.Load<Sprite>("Soul/" + Soul_Database.talentVanguard[startIndex + i].talentIMG);
                tempObj.GetComponent<Preview_Slot>().talentName = Soul_Database.talentVanguard[startIndex + i].talentName;
                tempObj.GetComponent<Preview_Slot>().talentDesc = Soul_Database.talentVanguard[startIndex + i].talentDesc;
            }
            //Assassin
            else if (Soul_Manager.heroSelected == 3)
            {
                tempObj.transform.Find("Preview_IMG").GetComponent<Image>().sprite = Resources.Load<Sprite>("Soul/" + Soul_Database.talentAssassin[startIndex + i].talentIMG);
                tempObj.GetComponent<Preview_Slot>().talentName = Soul_Database.talentAssassin[startIndex + i].talentName;
                tempObj.GetComponent<Preview_Slot>().talentDesc = Soul_Database.talentAssassin[startIndex + i].talentDesc;
            }
            //Ranger
            else if (Soul_Manager.heroSelected == 4)
            {
                tempObj.transform.Find("Preview_IMG").GetComponent<Image>().sprite = Resources.Load<Sprite>("Soul/" + Soul_Database.talentRanger[startIndex + i].talentIMG);
                tempObj.GetComponent<Preview_Slot>().talentName = Soul_Database.talentRanger[startIndex + i].talentName;
                tempObj.GetComponent<Preview_Slot>().talentDesc = Soul_Database.talentRanger[startIndex + i].talentDesc;
            }
            //Mage
            else if (Soul_Manager.heroSelected == 5)
            {
                tempObj.transform.Find("Preview_IMG").GetComponent<Image>().sprite = Resources.Load<Sprite>("Soul/" + Soul_Database.talentMage[startIndex + i].talentIMG);
                tempObj.GetComponent<Preview_Slot>().talentName = Soul_Database.talentMage[startIndex + i].talentName;
                tempObj.GetComponent<Preview_Slot>().talentDesc = Soul_Database.talentMage[startIndex + i].talentDesc;
            }
            //Sage
            else if (Soul_Manager.heroSelected == 6)
            {
                tempObj.transform.Find("Preview_IMG").GetComponent<Image>().sprite = Resources.Load<Sprite>("Soul/" + Soul_Database.talentSage[startIndex + i].talentIMG);
                tempObj.GetComponent<Preview_Slot>().talentName = Soul_Database.talentSage[startIndex + i].talentName;
                tempObj.GetComponent<Preview_Slot>().talentDesc = Soul_Database.talentSage[startIndex + i].talentDesc;
            }
        }

    }

    //Updates the skill tree
    void UpdateTree()
    {
        //Clear the current skills in the tree
        foreach (GameObject skill in GameObject.FindGameObjectsWithTag("Soul_Button"))
        {
            Destroy(skill);
        }

        //Create the icons
        for (int i = 0; i < Soul_Manager.playerTalentTree.Count; i++)
        {
            GameObject tempObj =  Instantiate(Resources.Load("Prefabs/Soul/Skill_Icon"), skillPanel.transform) as GameObject;
            tempObj.transform.Find("Skill_IMG").GetComponent<Image>().sprite = Resources.Load<Sprite>("Soul/" + Soul_Manager.playerTalentTree[i].talentIMG);
            tempObj.GetComponent<Skill_Slot>().slotIndex = i;
            tempObj.GetComponent<Skill_Slot>().skillSelected = tempObj.transform.Find("Skill_Select").gameObject;
            tempObj.GetComponent<Skill_Slot>().skillSelected.SetActive(false);
            tempObj.GetComponent<Skill_Slot>().talentName = Soul_Manager.playerTalentTree[i].talentName;
            tempObj.GetComponent<Skill_Slot>().talentDesc = Soul_Manager.playerTalentTree[i].talentDesc;
        }

        //Turn on selected border

    }

    //Updates the selected skills
    public void UpdateSelections()
    {
        //Turn off all
        foreach (GameObject skill in GameObject.FindGameObjectsWithTag("Soul_Button"))
        {
            skill.GetComponent<Skill_Slot>().skillSelected.SetActive(false);
        }

        //Turn on the selected ones
        foreach (GameObject skill in GameObject.FindGameObjectsWithTag("Soul_Button"))
        {
            for(int i = 0; i < Soul_Manager.playerSelectedSkills.Length; i++)
            {
                if(Soul_Manager.playerSelectedSkills[i] == skill.GetComponent<Skill_Slot>().slotIndex)
                {
                    skill.GetComponent<Skill_Slot>().skillSelected.SetActive(true);
                }
            }
        }
    }

    public void ShowLevels()
    {
        levelText[0].gameObject.SetActive(true);
        currentLevel.text = "Current Level: " + (PlayerStats.playerLevel);

        if(PlayerStats.playerLevel >= 5)
        {
            levelText[1].gameObject.SetActive(true);
        }

        if (PlayerStats.playerLevel >= 10)
        {
            levelText[2].gameObject.SetActive(true);
        }

        if (PlayerStats.playerLevel >= 15)
        {
            levelText[3].gameObject.SetActive(true);
        }

        if (PlayerStats.playerLevel >= 20)
        {
            levelText[4].gameObject.SetActive(true);
        }

        if (PlayerStats.playerLevel >= 25)
        {
            levelText[5].gameObject.SetActive(true);
        }
    }

    void UpdateTalentBonus()
    {
        //Set all to 0
        PlayerStats.alcLevel = 0;
        PlayerStats.armAmount = 0;
        PlayerStats.assChance = 0;
        PlayerStats.berserkAmount = 0;
        PlayerStats.collectLevel = 0;
        PlayerStats.deckAdd = 0;
        PlayerStats.defTalent = 0;
        PlayerStats.dexTalent = 0;
        PlayerStats.endTalent = 0;
        PlayerStats.evaChance = 0;
        PlayerStats.precAmount = 0;
        PlayerStats.intTalent = 0;
        PlayerStats.leechChance = 0;
        PlayerStats.plundChance = 0;
        PlayerStats.shieldAmount = 0;
        PlayerStats.speedAmount = 0;
        PlayerStats.strTalent = 0;
        PlayerStats.vitTalent = 0;
        PlayerStats.wisTalent = 0;


        //Read the Player Talents
        for (int i = 0; i < Soul_Manager.playerSelectedSkills.Length; i++)
        {
            if (Soul_Manager.playerSelectedSkills[i] >= 0)
            {
                Talent tempTalent = Soul_Manager.playerTalentTree[Soul_Manager.playerSelectedSkills[i]];

                if (tempTalent.talentAlc > 0)
                {
                    PlayerStats.alcLevel += tempTalent.talentAlc;
                }
                if (tempTalent.talentArm > 0)
                {
                    PlayerStats.armAmount += tempTalent.talentArm;
                }
                if (tempTalent.talentAss > 0)
                {
                    PlayerStats.assChance += tempTalent.talentAss;
                }
                if (tempTalent.talentBerserk > 0)
                {
                    PlayerStats.berserkAmount += tempTalent.talentBerserk;
                }
                if (tempTalent.talentCollect > 0)
                {
                    PlayerStats.collectLevel += tempTalent.talentCollect;
                }
                if (tempTalent.talentDeck > 0)
                {
                    PlayerStats.deckAdd += tempTalent.talentDeck;
                }
                if (tempTalent.talentDef > 0)
                {
                    PlayerStats.defTalent += tempTalent.talentDef;
                }
                if (tempTalent.talentDex > 0)
                {
                    PlayerStats.dexTalent += tempTalent.talentDex;
                }
                if (tempTalent.talentEnd > 0)
                {
                    PlayerStats.endTalent += tempTalent.talentEnd;
                }
                if (tempTalent.talentEva > 0)
                {
                    PlayerStats.evaChance += tempTalent.talentEva;
                }
                if (tempTalent.talentHunt > 0)
                {
                    PlayerStats.precAmount += tempTalent.talentHunt;
                }
                if (tempTalent.talentInt > 0)
                {
                    PlayerStats.intTalent += tempTalent.talentInt;
                }
                if (tempTalent.talentLeech > 0)
                {
                    PlayerStats.leechChance += tempTalent.talentLeech;
                }
                if (tempTalent.talentPlund > 0)
                {
                    PlayerStats.plundChance += tempTalent.talentPlund;
                }
                if (tempTalent.talentShield > 0)
                {
                    PlayerStats.shieldAmount += tempTalent.talentShield;
                }
                if (tempTalent.talentSpeed > 0)
                {
                    PlayerStats.speedAmount += tempTalent.talentSpeed;
                }
                if (tempTalent.talentStr > 0)
                {
                    PlayerStats.strTalent += tempTalent.talentStr;
                }
                if (tempTalent.talentVit > 0)
                {
                    PlayerStats.vitTalent += tempTalent.talentVit;
                }
                if (tempTalent.talentWis > 0)
                {
                    PlayerStats.wisTalent += tempTalent.talentWis;
                }
                if(tempTalent.talentTwin > 0)
                {
                    PlayerStats.twinChance += tempTalent.talentTwin;
                }
            }

            Deck.deckSize = 15;
            Deck.deckSize += PlayerStats.deckAdd;
        }
    }

    void OpenHeroMenu()
    {
        heroSelectPanel.SetActive(true);
    }

    void CheckHeros()
    {
        for(int i = 0; i < 6; i++)
        {
            if(!Soul_Manager.unlockedHeros.Contains(i))
            {
                heroButtons[i].gameObject.SetActive(false);
            }

        }

    }


}
