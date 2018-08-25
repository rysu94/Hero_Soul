using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HeroSoul_Interface : MonoBehaviour
{
    //The hero soul interface on the HUD
    public GameObject heroInterface;

    //This transform is where the panel 
    public Transform heroPanel;

    //Tool tip for on mouse over
    public GameObject mouseTooltip;

    bool inBuffer = false;

    //Controller control variables
    public int selectedIndex = 0;
    public List<GameObject> heroFrames = new List<GameObject>();
    public bool controlBuffer = false;
    public GameObject controllerTooltip;
    public GameObject[] controllerTooltips = new GameObject[3];

	// Use this for initialization
	void Start ()
    {
        inBuffer = false;
        heroFrames.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        //Controller Enabled
        if(GameController.xbox360Enabled() && !controlBuffer && heroInterface.activeInHierarchy)
        {
            //Left
            if(InputManager.MainHorizontal() < 0)
            {
                selectedIndex--;
                if(selectedIndex < 0)
                {
                    selectedIndex = heroFrames.Count - 1;
                }

                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                StartCoroutine(ControllerBuffer());
            }
            //Right
            else if(InputManager.MainHorizontal() > 0)
            {
                selectedIndex++;
                if (selectedIndex > heroFrames.Count - 1)
                {
                    selectedIndex = 0;
                }

                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                StartCoroutine(ControllerBuffer());
            }

            //Update the Selection
            for(int i = 0; i < heroFrames.Count; i++)
            {
                if(i == selectedIndex && !inBuffer)
                {
                    heroFrames[i].transform.Find("Controller_Selected").GetComponent<Animator>().Play("Soul_Frame_Selected");
                    heroFrames[i].transform.Find("A_Button").GetComponent<Animator>().Play("A_Button");
                    controllerTooltip.SetActive(true);
                    controllerTooltip.transform.position = heroFrames[i].transform.position;

                    List<GameObject> talents = new List<GameObject>();
                    foreach(Transform talent in heroFrames[i].transform.Find("Talent_Frame"))
                    {
                        talents.Add(talent.gameObject);
                    }

                    controllerTooltips[0].transform.Find("TalentName").GetComponent<Text>().text = talents[0].GetComponent<HUD_Talent_Tooptip>().skillName;
                    controllerTooltips[0].transform.Find("TalentDesc").GetComponent<Text>().text = talents[0].GetComponent<HUD_Talent_Tooptip>().skillDesc;
                    controllerTooltips[1].transform.Find("TalentName").GetComponent<Text>().text = talents[1].GetComponent<HUD_Talent_Tooptip>().skillName;
                    controllerTooltips[1].transform.Find("TalentDesc").GetComponent<Text>().text = talents[1].GetComponent<HUD_Talent_Tooptip>().skillDesc;
                    controllerTooltips[2].transform.Find("TalentName").GetComponent<Text>().text = talents[2].GetComponent<HUD_Talent_Tooptip>().skillName;
                    controllerTooltips[2].transform.Find("TalentDesc").GetComponent<Text>().text = talents[2].GetComponent<HUD_Talent_Tooptip>().skillDesc;

                }
                else
                {
                    heroFrames[i].transform.Find("Controller_Selected").GetComponent<Animator>().Play("Soul_Frame_Selected_Off");
                    heroFrames[i].transform.Find("A_Button").GetComponent<Animator>().Play("A_Button_Hide");
                }

                //If a hero is selected
                if(InputManager.A_Button())
                {
                    AddUnlocked();
                    controllerTooltip.SetActive(false);
                }
            }

        }

        if(heroInterface.activeInHierarchy)
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPoint.z = Camera.main.transform.position.z;
            Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
            if (hit.collider != null && hit.collider.tag == "Skill_Preview")
            {
                mouseTooltip.gameObject.SetActive(true);
                mouseTooltip.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 5));
                mouseTooltip.transform.position = new Vector3(mouseTooltip.transform.position.x + .75f, mouseTooltip.transform.position.y - .35f, mouseTooltip.transform.position.z);

                //Find talent name
                GameObject tempObj = mouseTooltip.transform.Find("TalentName").gameObject;
                Text tempText = tempObj.GetComponent<Text>();
                tempText.text = hit.collider.gameObject.GetComponent<HUD_Talent_Tooptip>().skillName;

                //Find talent Desc
                GameObject tempObj2 = mouseTooltip.transform.Find("TalentDesc").gameObject;
                Text tempText2 = tempObj2.GetComponent<Text>();
                tempText2.text = hit.collider.gameObject.GetComponent<HUD_Talent_Tooptip>().skillDesc;
            }
            else
            {
                mouseTooltip.gameObject.SetActive(false);
            }
        }

    }

    public void ShowInteface()
    {
        heroInterface.SetActive(true);
        GenerateHeros();
    }

    //Generate the 3 hero panels to pick from
    public void GenerateHeros()
    {
        //Generate the list of possible hero ids
        List<int> tempHeroList = new List<int>();
        for(int i = 0; i < 6; i++)
        {
            if(!Soul_Manager.unlockedHeros.Contains(i))
            {
                tempHeroList.Add(i);
            }
        }

        //print(tempHeroList.Count);

        //Make a list of 3 hero ids, if possible
        List<int> generatedHeros = new List<int>();
        if(tempHeroList.Count >= 3)
        {
            for (int i = 0; i < 3; i++)
            {
                int tempInt = Random.Range(0, tempHeroList.Count);
                generatedHeros.Add(tempHeroList[tempInt]);
                tempHeroList.RemoveAt(tempInt);
            }
        }
        else
        {
            for (int i = 0; i < tempHeroList.Count+1; i++)
            {
                int tempInt = Random.Range(0, tempHeroList.Count);
                generatedHeros.Add(tempHeroList[tempInt]);
                tempHeroList.RemoveAt(tempInt);
            }
        }


        //Clear the hero panel
        heroInterface.SetActive(true);
        foreach (Transform hero in heroPanel.transform)
        {
            Destroy(hero.gameObject);
        }

        //Create hero frames on the hero panel
        for(int i = 0; i < generatedHeros.Count; i++)
        {
            //print(generatedHeros[i]);
            CreateHero(generatedHeros[i]);
        }

        //If controller enabled, fill the frame list
        if(GameController.xbox360Enabled())
        {
            heroFrames.Clear();
            foreach(GameObject frame in GameObject.FindGameObjectsWithTag("Hero_Frame"))
            {
                heroFrames.Add(frame);
            }
        }
    }

    public void CreateHero(int heroID)
    {
        //Create the a hero on the hero panel
        GameObject hero = (Instantiate(Resources.Load<GameObject>("Prefabs/Soul/Hero_Frame"), heroPanel.transform));

        //The hero image 
        Image heroPortrait = hero.transform.Find("Foreground").transform.Find("Mask").transform.Find("Hero_Portrait").gameObject.GetComponent<Image>();
        heroPortrait.sprite = GetHeroPortrait(heroID);
        
        //The text for the hero's class
        Text heroClassText = hero.transform.Find("HeroClass").gameObject.GetComponent<Text>();
        heroClassText.text = GetClassText(heroID);

        //The hero soul class icon
        Image heroIcon = hero.transform.Find("Soul_Frame").transform.Find("Soul_Icon").gameObject.GetComponent<Image>();
        heroIcon.sprite = GetHeroIcon(heroID);

        //Hero Talent Frame
        GameObject talentFrame = hero.transform.Find("Talent_Frame").gameObject;
        foreach(Transform talent in talentFrame.transform)
        {
            Destroy(talent.gameObject);
        }
        GetHeroTalents(heroID, talentFrame.transform);

        //SBreak Skill Icon
        Image sBreakFrame = hero.transform.Find("S-Break Frame").gameObject.GetComponent<Image>();
        sBreakFrame.sprite = GetBreakSkill(heroID);

        //The confirm button
        Button confirmButton = hero.transform.Find("Button").gameObject.GetComponent<Button>();
        confirmButton.GetComponent<HUD_Talent_Tooptip>().heroID = heroID;
        confirmButton.onClick.AddListener(AddUnlocked);
    }

    public Sprite GetBreakSkill(int heroID)
    {
        Sprite skill = Resources.Load<Sprite>("Soul/Break/Skyfall");

        return skill;
    }

    public void GetHeroTalents(int heroID, Transform talentFrame)
    {
        switch(heroID)
        {
            default:
                break;
            case 0:
                //Talent 1 - Deck Size Increase
                GameObject talentBer1 = Instantiate(Resources.Load<GameObject>("Prefabs/Soul/Talent"), talentFrame);
                talentBer1.GetComponent<HUD_Talent_Tooptip>().skillName = "Arcana Mastery";
                talentBer1.GetComponent<HUD_Talent_Tooptip>().skillDesc = "Increase you maximum deck size by 5.";
                //Talent 2 - Leech
                GameObject talentBer2 = Instantiate(Resources.Load<GameObject>("Prefabs/Soul/Talent"), talentFrame);
                talentBer2.GetComponent<HUD_Talent_Tooptip>().skillName = "Unique Talent: Leech";
                talentBer2.GetComponent<HUD_Talent_Tooptip>().skillDesc = "Adds an chance to leech health when attacking.";
                talentBer2.GetComponent<Image>().sprite = Resources.Load<Sprite>("Soul/Leech+1");
                //Talent 3 - Berserk
                GameObject talentBer3 = Instantiate(Resources.Load<GameObject>("Prefabs/Soul/Talent"), talentFrame);
                talentBer3.GetComponent<HUD_Talent_Tooptip>().skillName = "Unique Talent: Berserk";
                talentBer3.GetComponent<HUD_Talent_Tooptip>().skillDesc = "Increases damage done when below 25% health.";
                talentBer3.GetComponent<Image>().sprite = Resources.Load<Sprite>("Soul/Berserk+25");
                break;
            case 1:
                //Talent 1 - Deck Size Increase
                GameObject talentVan1 = Instantiate(Resources.Load<GameObject>("Prefabs/Soul/Talent"), talentFrame);
                talentVan1.GetComponent<HUD_Talent_Tooptip>().skillName = "Arcana Mastery";
                talentVan1.GetComponent<HUD_Talent_Tooptip>().skillDesc = "Increase you maximum deck size by 5.";
                //Talent 2 - Def Up
                GameObject talentVan2 = Instantiate(Resources.Load<GameObject>("Prefabs/Soul/Talent"), talentFrame);
                talentVan2.GetComponent<HUD_Talent_Tooptip>().skillName = "Unique Talent: Increased Def";
                talentVan2.GetComponent<HUD_Talent_Tooptip>().skillDesc = "Adds bonus armor.";
                talentVan2.GetComponent<Image>().sprite = Resources.Load<Sprite>("Soul/DefUp+5");
                //Talent 3 - Armor
                GameObject talentVan3 = Instantiate(Resources.Load<GameObject>("Prefabs/Soul/Talent"), talentFrame);
                talentVan3.GetComponent<HUD_Talent_Tooptip>().skillName = "Unique Talent: Armor";
                talentVan3.GetComponent<HUD_Talent_Tooptip>().skillDesc = "Start each level with bonus armor.";
                talentVan3.GetComponent<Image>().sprite = Resources.Load<Sprite>("Soul/Arm+50");
                break;
            case 2:
                //Talent 1 - Deck Size Increase
                GameObject talentAss1 = Instantiate(Resources.Load<GameObject>("Prefabs/Soul/Talent"), talentFrame);
                talentAss1.GetComponent<HUD_Talent_Tooptip>().skillName = "Arcana Mastery";
                talentAss1.GetComponent<HUD_Talent_Tooptip>().skillDesc = "Increase you maximum deck size by 5.";
                //Talent 2 - Assassinate
                GameObject talentAss2 = Instantiate(Resources.Load<GameObject>("Prefabs/Soul/Talent"), talentFrame);
                talentAss2.GetComponent<HUD_Talent_Tooptip>().skillName = "Unique Talent: Assassinate";
                talentAss2.GetComponent<HUD_Talent_Tooptip>().skillDesc = "Adds chance on hit to instantly kill lesser enemies.";
                talentAss2.GetComponent<Image>().sprite = Resources.Load<Sprite>("Soul/Assn+5");
                //Talent 3 - Plunder
                GameObject talentAss3 = Instantiate(Resources.Load<GameObject>("Prefabs/Soul/Talent"), talentFrame);
                talentAss3.GetComponent<HUD_Talent_Tooptip>().skillName = "Unique Talent: Plunder";
                talentAss3.GetComponent<HUD_Talent_Tooptip>().skillDesc = "Adds chance to steal gold when attacking.";
                talentAss3.GetComponent<Image>().sprite = Resources.Load<Sprite>("Soul/Plunder+1");
                break;
            case 3:
                //Talent 1 - Deck Size Increase
                GameObject talentRan1 = Instantiate(Resources.Load<GameObject>("Prefabs/Soul/Talent"), talentFrame);
                talentRan1.GetComponent<HUD_Talent_Tooptip>().skillName = "Arcana Mastery";
                talentRan1.GetComponent<HUD_Talent_Tooptip>().skillDesc = "Increase you maximum deck size by 5.";
                //Talent 2 - Twin Strike
                GameObject talentRan2 = Instantiate(Resources.Load<GameObject>("Prefabs/Soul/Talent"), talentFrame);
                talentRan2.GetComponent<HUD_Talent_Tooptip>().skillName = "Unique Talent: Twin Strike";
                talentRan2.GetComponent<HUD_Talent_Tooptip>().skillDesc = "Adds chance to add strike twice when attacking.";
                talentRan2.GetComponent<Image>().sprite = Resources.Load<Sprite>("Soul/Twin+5");
                //Talent 3 - Precision
                GameObject talentRan3 = Instantiate(Resources.Load<GameObject>("Prefabs/Soul/Talent"), talentFrame);
                talentRan3.GetComponent<HUD_Talent_Tooptip>().skillName = "Unique Talent: Precision";
                talentRan3.GetComponent<HUD_Talent_Tooptip>().skillDesc = "Increases critical strike damage.";
                talentRan3.GetComponent<Image>().sprite = Resources.Load<Sprite>("Soul/Prec+25");
                break;
            case 4:
                //Talent 1 - Deck Size Increase
                GameObject talentMag1 = Instantiate(Resources.Load<GameObject>("Prefabs/Soul/Talent"), talentFrame);
                talentMag1.GetComponent<HUD_Talent_Tooptip>().skillName = "Arcana Mastery";
                talentMag1.GetComponent<HUD_Talent_Tooptip>().skillDesc = "Increase you maximum deck size by 5.";
                //Talent 2 - Collector
                GameObject talentMag2 = Instantiate(Resources.Load<GameObject>("Prefabs/Soul/Talent"), talentFrame);
                talentMag2.GetComponent<HUD_Talent_Tooptip>().skillName = "Unique Talent: Collector";
                talentMag2.GetComponent<HUD_Talent_Tooptip>().skillDesc = "Increase the amount of Arcana dropped when killing enemies.";
                talentMag2.GetComponent<Image>().sprite = Resources.Load<Sprite>("Soul/Collector+1");
                //Talent 3 - Shield
                GameObject talentMag3 = Instantiate(Resources.Load<GameObject>("Prefabs/Soul/Talent"), talentFrame);
                talentMag3.GetComponent<HUD_Talent_Tooptip>().skillName = "Unique Talent: Shield";
                talentMag3.GetComponent<HUD_Talent_Tooptip>().skillDesc = "Start each level with bonus Shield.";
                talentMag3.GetComponent<Image>().sprite = Resources.Load<Sprite>("Soul/Shield+25");
                break;
            case 5:
                //Talent 1 - Deck Size Increase
                GameObject talentSag1 = Instantiate(Resources.Load<GameObject>("Prefabs/Soul/Talent"), talentFrame);
                talentSag1.GetComponent<HUD_Talent_Tooptip>().skillName = "Arcana Mastery";
                talentSag1.GetComponent<HUD_Talent_Tooptip>().skillDesc = "Increase you maximum deck size by 5.";
                //Talent 2 - Alchemy
                GameObject talentSag2 = Instantiate(Resources.Load<GameObject>("Prefabs/Soul/Talent"), talentFrame);
                talentSag2.GetComponent<HUD_Talent_Tooptip>().skillName = "Unique Talent: Alchemy";
                talentSag2.GetComponent<HUD_Talent_Tooptip>().skillDesc = "Increases the number of potions you can carry.";
                talentSag2.GetComponent<Image>().sprite = Resources.Load<Sprite>("Soul/Alc+1");
                //Talent 3 - Shield
                GameObject talentSag3 = Instantiate(Resources.Load<GameObject>("Prefabs/Soul/Talent"), talentFrame);
                talentSag3.GetComponent<HUD_Talent_Tooptip>().skillName = "Unique Talent: Shield";
                talentSag3.GetComponent<HUD_Talent_Tooptip>().skillDesc = "Start each level with bonus Shield.";
                talentSag3.GetComponent<Image>().sprite = Resources.Load<Sprite>("Soul/Shield+25");
                break;
        }
    }

    //Get the hero icon using the id
    public Sprite GetHeroIcon(int heroID)
    {
        Sprite icon = Resources.Load<Sprite>("Soul/HeroPortaits/W_Axe008");
        switch(heroID)
        {
            default:
                break;
            case 0:
                icon = Resources.Load<Sprite>("Soul/HeroPortaits/W_Axe008");
                break;
            case 1:
                icon = Resources.Load<Sprite>("Soul/HeroPortaits/E_Metal09");
                break;
            case 2:
                icon = Resources.Load<Sprite>("Soul/HeroPortaits/W_Throw002");
                break;
            case 3:
                icon = Resources.Load<Sprite>("Soul/HeroPortaits/W_Bow13");
                break;
            case 4:
                icon = Resources.Load<Sprite>("Soul/HeroPortaits/W_Book06");
                break;
            case 5:
                icon = Resources.Load<Sprite>("Soul/HeroPortaits/W_Book03");
                break;
        }

        return icon;
    }

    //Get the hero class text
    public string GetClassText(int heroID)
    {
        string heroClass = "Null";
        switch(heroID)
        {
            default:
                break;
            case 0:
                heroClass = "Berserker";
                break;
            case 1:
                heroClass = "Vanguard";
                break;
            case 2:
                heroClass = "Assassin";
                break;
            case 3:
                heroClass = "Ranger";
                break;
            case 4:
                heroClass = "Mage";
                break;
            case 5:
                heroClass = "Sage";
                break;
        }
        return heroClass;
    }

    //Returns the image for the Hero using the id
    public Sprite GetHeroPortrait(int heroID)
    {
        Sprite heroSprite = Resources.Load<Sprite>("Soul/HeroPortaits/Berserker_Iskar");
        switch(heroID)
        {
            default:
                break;
            case 0:
                heroSprite = Resources.Load<Sprite>("Soul/HeroPortaits/Berserker_Iskar");
                break;
            case 1:
                heroSprite = Resources.Load<Sprite>("Soul/HeroPortaits/Hero_02");
                break;
            case 2:
                heroSprite = Resources.Load<Sprite>("Soul/HeroPortaits/Hero_03");
                break;
            case 3:
                heroSprite = Resources.Load<Sprite>("Soul/HeroPortaits/Hero_04");
                break;
            case 4:
                heroSprite = Resources.Load<Sprite>("Soul/HeroPortaits/Hero_05");
                break;
            case 5:
                heroSprite = Resources.Load<Sprite>("Soul/HeroPortaits/Hero_06");
                break;
        }

        return heroSprite;
    }


    public void AddUnlocked()
    {
        if(!inBuffer)
        {
            //Add the hero id to the list of unlocked heros

            if(GameController.xbox360Enabled())
            {
                Soul_Manager.unlockedHeros.Add(heroFrames[selectedIndex].transform.Find("Button").GetComponent<HUD_Talent_Tooptip>().heroID);
            }
            else
            {
                Soul_Manager.unlockedHeros.Add(EventSystem.current.currentSelectedGameObject.GetComponent<HUD_Talent_Tooptip>().heroID);
            }
            
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            StartCoroutine(CloseBuffer());
        }
    }

    IEnumerator CloseBuffer()
    {
        inBuffer = true;
        GameObject.Find("WipeScreen").GetComponent<Animator>().Play("FadeOut");
        yield return new WaitForSeconds(4f);
        heroInterface.SetActive(false);
        TestCharController.inDialogue = false;
        Soul_Controller.sceneName = SceneManager.GetActiveScene().name;
        Soul_Controller.playerX = TestCharController.player.transform.position.x;
        Soul_Controller.playerY = TestCharController.player.transform.position.y;
        if(TestCharController.player.GetComponent<TestCharController>().north)
        {
            Soul_Controller.startTag = "Up";
        }
        else if(TestCharController.player.GetComponent<TestCharController>().south)
        {
            Soul_Controller.startTag = "Down";
        }
        else if(TestCharController.player.GetComponent<TestCharController>().west)
        {
            Soul_Controller.startTag = "Left";
        }
        else if(TestCharController.player.GetComponent<TestCharController>().east)
        {
            Soul_Controller.startTag = "Right";
        }
        SceneManager.LoadScene("HeroSoul");
        
    }

    IEnumerator ControllerBuffer()
    {
        controlBuffer = true;
        yield return new WaitForSeconds(.5f);
        controlBuffer = false;
    }
}
