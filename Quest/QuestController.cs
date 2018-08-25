using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public static int displayQuest = 0;

    public GameObject mainQuestPanel;
    public GameObject sideQuestPanel;
    public GameObject rewardPanel;

    public Text questName;
    public Text questObj1;
    public Text questObj2;
    public Text questDesc;
    public Text questRewards;

    //Controller Support
    public List<GameObject> questMainList = new List<GameObject>();
    public List<GameObject> questSideList = new List<GameObject>();
    public GameObject selectSlab;
    public int selectIndex = 0;
    public bool mainQuest = false;
    public bool padActive = false;

	// Use this for initialization
	void Start ()
    {
        UpdateQuests();
        SetDisplay();
        mainQuest = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = Camera.main.transform.position.z;
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if(hit.collider != null && hit.collider.tag == "Quest")
        {
            if(Input.GetMouseButtonDown(0))
            {
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                questName.text = hit.collider.gameObject.GetComponent<Quest_Slab>().questName;
                questDesc.text = hit.collider.gameObject.GetComponent<Quest_Slab>().questDesc;

                //set active display
                for (int i = 0; i < QuestManager.activeMainQuests.Count; i++)
                {
                    if(hit.collider.gameObject.GetComponent<Quest_Slab>().questName == QuestManager.activeMainQuests[i].questName)
                    {
                        displayQuest = i + 1;
                    }
                }
                for(int i = 0; i < QuestManager.activeSideQuests.Count; i++)
                {
                    if(hit.collider.gameObject.GetComponent<Quest_Slab>().questName == QuestManager.activeSideQuests[i].questName)
                    {
                        displayQuest = i + 4;
                    }
                }

                //Quest Obj
                if(hit.collider.gameObject.GetComponent<Quest_Slab>().questObj.Count <= 3)
                {
                    questObj1.text = "";
                    for(int i = 0; i < hit.collider.gameObject.GetComponent<Quest_Slab>().questObj.Count; i++)
                    {
                        //Check if quest is complete
                        if(hit.collider.gameObject.GetComponent<Quest_Slab>().questComplete[i])
                        {
                            questObj1.text += "<color=white>(x) </color>" + hit.collider.GetComponent<Quest_Slab>().questObj[i] + "\n";
                        }
                        else
                        {
                            questObj1.text += "<color=white>( ) </color>" + hit.collider.GetComponent<Quest_Slab>().questObj[i] + "\n";
                        }
                        
                    }
                }
                else if(hit.collider.gameObject.GetComponent<Quest_Slab>().questObj.Count > 3)
                {
                    questObj1.text = "";
                    for (int i = 0; i < 3; i++)
                    {
                        //Check if quest is complete
                        if (hit.collider.gameObject.GetComponent<Quest_Slab>().questComplete[i])
                        {
                            questObj1.text += "<color=white>(x) </color>" + hit.collider.GetComponent<Quest_Slab>().questObj[i] + "\n";
                        }
                        else
                        {
                            questObj1.text += "<color=white>( ) </color>" + hit.collider.GetComponent<Quest_Slab>().questObj[i] + "\n";
                        }
                    }
                    questObj2.text = "";
                    for(int j = 3; j < hit.collider.GetComponent<Quest_Slab>().questObj.Count; j++)
                    {
                        //Check if quest is complete
                        if (hit.collider.gameObject.GetComponent<Quest_Slab>().questComplete[j])
                        {
                            questObj2.text += "<color=white>(x) </color>" + hit.collider.GetComponent<Quest_Slab>().questObj[j] + "\n";
                        }
                        else
                        {
                            questObj2.text += "<color=white>( ) </color>" + hit.collider.GetComponent<Quest_Slab>().questObj[j] + "\n";
                        }
                    }
                }
                questRewards.text = "Rewards:\n";
                
                //Clear the reward panel
                foreach(GameObject reward in GameObject.FindGameObjectsWithTag("Quest_Reward"))
                {
                    Destroy(reward);
                }

                //check XP
                if(hit.collider.GetComponent<Quest_Slab>().xp > 0)
                {
                    GameObject tempObj = Instantiate(Resources.Load("Prefabs/Quest/Reward_Slot"), rewardPanel.transform) as GameObject;
                    tempObj.transform.Find("Reward_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/XP_Icon");
                    tempObj.GetComponent<Reward_Slab>().desc = "\n" + hit.collider.GetComponent<Quest_Slab>().xp.ToString() + " xp.";
                }
                //check Gold
                if(hit.collider.GetComponent<Quest_Slab>().gold > 0)
                {
                    GameObject tempObj = Instantiate(Resources.Load("Prefabs/Quest/Reward_Slot"), rewardPanel.transform) as GameObject;
                    tempObj.transform.Find("Reward_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/I_GoldCoin");
                    tempObj.GetComponent<Reward_Slab>().desc = "\n" + hit.collider.GetComponent<Quest_Slab>().gold.ToString() + " gold.";
                }
               
                //check item
                for(int i = 0; i < hit.collider.gameObject.GetComponent<Quest_Slab>().questItems.Count; i++)
                {
                    GameObject tempObj = Instantiate(Resources.Load("Prefabs/Quest/Reward_Slot"), rewardPanel.transform) as GameObject;
                    tempObj.transform.Find("Reward_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + hit.collider.gameObject.GetComponent<Quest_Slab>().questItems[i].itemIconName);
                    tempObj.GetComponent<Reward_Slab>().desc = "\n" + hit.collider.GetComponent<Quest_Slab>().questItems[i].itemName;
                }
            }
        }


        //Controller Support
        if(GameController.xbox360Enabled())
        {
            UpdateSelection();
            SelectQuest();
            DisplayQuest();
        }



    }

    public void UpdateQuests()
    {
        foreach(GameObject slab in GameObject.FindGameObjectsWithTag("Quest"))
        {
            Destroy(slab);
        }

        GameObject tempObj;
        //Generate main quests
        for(int i = 0; i < QuestManager.activeMainQuests.Count; i++)
        {
            tempObj = Instantiate(Resources.Load("Prefabs/Quest/Quest_Slab"), mainQuestPanel.transform) as GameObject;
            tempObj.GetComponent<Quest_Slab>().questNameText.text = QuestManager.activeMainQuests[i].questName;
            tempObj.GetComponent<Quest_Slab>().questIMG.sprite = Resources.Load<Sprite>("Quest/" + QuestManager.activeMainQuests[i].questIMG);
            tempObj.GetComponent<Quest_Slab>().questName = QuestManager.activeMainQuests[i].questName;
            tempObj.GetComponent<Quest_Slab>().questDesc = QuestManager.activeMainQuests[i].questDesc;
            tempObj.GetComponent<Quest_Slab>().questObj = QuestManager.activeMainQuests[i].questObj;
            tempObj.GetComponent<Quest_Slab>().xp = QuestManager.activeMainQuests[i].questXP;
            tempObj.GetComponent<Quest_Slab>().gold = QuestManager.activeMainQuests[i].questGold;
            tempObj.GetComponent<Quest_Slab>().questItems = QuestManager.activeMainQuests[i].questRewards;
            tempObj.GetComponent<Quest_Slab>().questComplete = QuestManager.activeMainQuests[i].objComplete;
            tempObj.GetComponent<Quest_Slab>().type = QuestManager.activeMainQuests[i].questType;
        }

        //Generate side Quests
        for(int j = 0; j < QuestManager.activeSideQuests.Count; j++)
        {
            tempObj = Instantiate(Resources.Load("Prefabs/Quest/Quest_Slab"), sideQuestPanel.transform) as GameObject;
            tempObj.GetComponent<Quest_Slab>().questNameText.text = QuestManager.activeSideQuests[j].questName;
            tempObj.GetComponent<Quest_Slab>().questIMG.sprite = Resources.Load<Sprite>("Quest/" + QuestManager.activeSideQuests[j].questIMG);
            tempObj.GetComponent<Quest_Slab>().questName = QuestManager.activeSideQuests[j].questName;
            tempObj.GetComponent<Quest_Slab>().questDesc = QuestManager.activeSideQuests[j].questDesc;
            tempObj.GetComponent<Quest_Slab>().questObj = QuestManager.activeSideQuests[j].questObj;
            tempObj.GetComponent<Quest_Slab>().xp = QuestManager.activeSideQuests[j].questXP;
            tempObj.GetComponent<Quest_Slab>().gold = QuestManager.activeSideQuests[j].questGold;
            tempObj.GetComponent<Quest_Slab>().questItems = QuestManager.activeSideQuests[j].questRewards;
            tempObj.GetComponent<Quest_Slab>().questComplete = QuestManager.activeSideQuests[j].objComplete;
            tempObj.GetComponent<Quest_Slab>().type = QuestManager.activeSideQuests[j].questType;
        }

        //Update Controller Variables
        selectIndex = 0;


    }

    public void SetDisplay()
    {
        if (displayQuest > 0)
        {
            if (displayQuest <= 3)
            {
                questName.text = QuestManager.activeMainQuests[displayQuest - 1].questName;
                questDesc.text = QuestManager.activeMainQuests[displayQuest - 1].questDesc;
                if (QuestManager.activeMainQuests[displayQuest - 1].questObj.Count <= 3)
                {
                    questObj1.text = "";
                    for (int i = 0; i < QuestManager.activeMainQuests[displayQuest - 1].questObj.Count; i++)
                    {
                        //Check if quest is complete
                        if(QuestManager.activeMainQuests[displayQuest-1].objComplete[i])
                        {
                            questObj1.text += "<color=white>(x) </color>" + QuestManager.activeMainQuests[displayQuest - 1].questObj[i] + "\n";
                        }
                        else
                        {
                            questObj1.text += "<color=white>( ) </color>" + QuestManager.activeMainQuests[displayQuest - 1].questObj[i] + "\n";
                        }      
                    }
                }
                else if (QuestManager.activeMainQuests[displayQuest - 1].questObj.Count > 3)
                {
                    questObj1.text = "";
                    for (int i = 0; i < 3; i++)
                    {
                        //Check if quest is complete
                        if (QuestManager.activeMainQuests[displayQuest -1].objComplete[i])
                        {
                            questObj1.text += "<color=white>(x) </color>" + QuestManager.activeMainQuests[displayQuest - 1].questObj[i] + "\n";
                        }
                        else
                        {
                            questObj1.text += "<color=white>( ) </color>" + QuestManager.activeMainQuests[displayQuest - 1].questObj[i] + "\n";
                        }
                    }
                    questObj2.text = "";
                    for (int j = 3; j < QuestManager.activeMainQuests[displayQuest - 1].questObj.Count; j++)
                    {
                        //Check if quest is complete
                        if (QuestManager.activeMainQuests[displayQuest - 1].objComplete[j])
                        {
                            questObj2.text += "<color=white>(x) </color>" + QuestManager.activeMainQuests[displayQuest - 1].questObj[j] + "\n";
                        }
                        else
                        {
                            questObj2.text += "<color=white>( ) </color>" + QuestManager.activeMainQuests[displayQuest - 1].questObj[j] + "\n";
                        }
                    }
                }
            }
            else
            {
                questName.text = QuestManager.activeSideQuests[displayQuest - 4].questName;
                questDesc.text = QuestManager.activeSideQuests[displayQuest - 4].questDesc;
                if (QuestManager.activeSideQuests[displayQuest - 4].questObj.Count <= 3)
                {
                    questObj1.text = "";
                    for (int i = 0; i < QuestManager.activeSideQuests[displayQuest - 4].questObj.Count; i++)
                    {
                        //Check if quest is complete
                        if (QuestManager.activeSideQuests[displayQuest - 4].objComplete[i])
                        {
                            questObj1.text += "<color=white>(x) </color>" + QuestManager.activeSideQuests[displayQuest - 4].questObj[i] + "\n";
                        }
                        else
                        {
                            questObj1.text += "<color=white>( ) </color>" + QuestManager.activeSideQuests[displayQuest - 4].questObj[i] + "\n";
                        }
                    }
                }
                else if (QuestManager.activeSideQuests[displayQuest - 4].questObj.Count > 3)
                {
                    questObj1.text = "";
                    for (int i = 0; i < 3; i++)
                    {
                        //Check if quest is complete
                        if (QuestManager.activeSideQuests[displayQuest - 4].objComplete[i])
                        {
                            questObj1.text += "<color=white>(x) </color>" + QuestManager.activeSideQuests[displayQuest - 4].questObj[i] + "\n";
                        }
                        else
                        {
                            questObj1.text += "<color=white>( ) </color>" + QuestManager.activeSideQuests[displayQuest - 4].questObj[i] + "\n";
                        }
                    }
                    questObj2.text = "";
                    for (int j = 3; j < QuestManager.activeSideQuests[displayQuest - 4].questObj.Count; j++)
                    {
                        //Check if quest is complete
                        if (QuestManager.activeSideQuests[displayQuest - 4].objComplete[j])
                        {
                            questObj2.text += "<color=white>(x) </color>" + QuestManager.activeSideQuests[displayQuest - 4].questObj[j] + "\n";
                        }
                        else
                        {
                            questObj2.text += "<color=white>( ) </color>" + QuestManager.activeSideQuests[displayQuest - 4].questObj[j] + "\n";
                        }
                    }
                }
            }

            questRewards.text = "Rewards:\n";
            //Clear the reward panel
            foreach (GameObject reward in GameObject.FindGameObjectsWithTag("Quest_Reward"))
            {
                Destroy(reward);
            }

            if (displayQuest <= 3)
            {
                //check XP
                if (QuestManager.activeMainQuests[displayQuest - 1].questXP > 0)
                {
                    GameObject tempObj = Instantiate(Resources.Load("Prefabs/Quest/Reward_Slot"), rewardPanel.transform) as GameObject;
                    tempObj.transform.Find("Reward_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/XP_Icon");
                    tempObj.GetComponent<Reward_Slab>().desc = "\n" + QuestManager.activeMainQuests[displayQuest - 1].questXP.ToString() + " xp.";
                }
                //check Gold
                if (QuestManager.activeMainQuests[displayQuest - 1].questGold > 0)
                {
                    GameObject tempObj = Instantiate(Resources.Load("Prefabs/Quest/Reward_Slot"), rewardPanel.transform) as GameObject;
                    tempObj.transform.Find("Reward_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/I_GoldCoin");
                    tempObj.GetComponent<Reward_Slab>().desc = "\n" + QuestManager.activeMainQuests[displayQuest - 1].questGold.ToString() + " gold.";
                }

                //check item
                for (int i = 0; i < QuestManager.activeMainQuests[displayQuest - 1].questRewards.Count; i++)
                {
                    GameObject tempObj = Instantiate(Resources.Load("Prefabs/Quest/Reward_Slot"), rewardPanel.transform) as GameObject;
                    tempObj.transform.Find("Reward_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + QuestManager.activeMainQuests[displayQuest - 1].questRewards[i].itemIconName);
                    tempObj.GetComponent<Reward_Slab>().desc = "\n" + QuestManager.activeMainQuests[displayQuest - 1].questRewards[i].itemName;
                }
            }
            else
            {
                //check XP
                if (QuestManager.activeSideQuests[displayQuest - 4].questXP > 0)
                {
                    GameObject tempObj = Instantiate(Resources.Load("Prefabs/Quest/Reward_Slot"), rewardPanel.transform) as GameObject;
                    tempObj.transform.Find("Reward_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/XP_Icon");
                    tempObj.GetComponent<Reward_Slab>().desc = "\n" + QuestManager.activeSideQuests[displayQuest - 4].questXP.ToString() + " xp.";
                }
                //check Gold
                if (QuestManager.activeSideQuests[displayQuest - 4].questGold > 0)
                {
                    GameObject tempObj = Instantiate(Resources.Load("Prefabs/Quest/Reward_Slot"), rewardPanel.transform) as GameObject;
                    tempObj.transform.Find("Reward_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/I_GoldCoin");
                    tempObj.GetComponent<Reward_Slab>().desc = "\n" + QuestManager.activeSideQuests[displayQuest - 4].questGold.ToString() + " gold.";
                }

                //check item
                for (int i = 0; i < QuestManager.activeSideQuests[displayQuest - 4].questRewards.Count; i++)
                {
                    GameObject tempObj = Instantiate(Resources.Load("Prefabs/Quest/Reward_Slot"), rewardPanel.transform) as GameObject;
                    tempObj.transform.Find("Reward_Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + QuestManager.activeSideQuests[displayQuest - 4].questRewards[i].itemIconName);
                    tempObj.GetComponent<Reward_Slab>().desc = "\n" + QuestManager.activeSideQuests[displayQuest - 4].questRewards[i].itemName;
                }
            }
        }
        else if(displayQuest == 0)
        {
            questName.text = "";
            questDesc.text = "";
            questObj1.text = "";
            questObj2.text = "";
            questRewards.text = "";

            //Clear the reward panel
            foreach (GameObject reward in GameObject.FindGameObjectsWithTag("Quest_Reward"))
            {
                Destroy(reward);
            }

        }
    }

    //Controller Support

    public void UpdateSelection()
    {
        questMainList.Clear();
        questSideList.Clear();
        foreach (GameObject questSlab in GameObject.FindGameObjectsWithTag("Quest"))
        {
            //Main Quests
            if (questSlab.GetComponent<Quest_Slab>().type == 0)
            {
                questMainList.Add(questSlab);
            }
            //Side Quests
            if(questSlab.GetComponent<Quest_Slab>().type == 1)
            {
                questSideList.Add(questSlab);
            }  
        }
    }

    public void SelectQuest()
    {
        //print(InputManager.MainVertical());
        if (InputManager.L_Bumper() || InputManager.R_Bumper())
        {
            if (!mainQuest && questMainList.Count > 0)
            {
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                mainQuest = true;
                selectIndex = 0;
            }
            else if(mainQuest && questSideList.Count > 0)
            {
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                mainQuest = false;
                selectIndex = 0;
            }
        }
        else if (InputManager.MainVertical() > .5f && !padActive)
        {
            if (mainQuest && questMainList.Count > 0)
            {
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                padActive = true;
                selectIndex--;
                if (selectIndex < 0)
                {
                    selectIndex = questMainList.Count - 1;
                }
                StartCoroutine(PadBuffer());
            }
            else if (!mainQuest && questSideList.Count > 0)
            {
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                padActive = true;
                selectIndex--;
                if (selectIndex < 0)
                {
                    selectIndex = questSideList.Count - 1;
                }
                StartCoroutine(PadBuffer());
            }
        }
        else if(InputManager.MainVertical() < -.5f && !padActive)
        {
            if(mainQuest && questMainList.Count > 0)
            {
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                padActive = true;
                selectIndex++;
                if(selectIndex > questMainList.Count - 1)
                {
                    selectIndex = 0;
                }
                StartCoroutine(PadBuffer());
            }
            else if(!mainQuest && questSideList.Count > 0)
            {
                GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                padActive = true;
                selectIndex++;
                if(selectIndex > questSideList.Count - 1)
                {
                    selectIndex = 0;
                }
                StartCoroutine(PadBuffer());
            }           
        }

        Destroy(selectSlab);

        //Display Selected Quest
        if (questMainList.Count > 0 && mainQuest)
        {
            selectSlab = Instantiate(Resources.Load("Prefabs/Quest/QuestSelect"), questMainList[selectIndex].transform) as GameObject;
        }
        else if (questSideList.Count > 0 && !mainQuest)
        {
            selectSlab = Instantiate(Resources.Load("Prefabs/Quest/QuestSelect"), questSideList[selectIndex].transform) as GameObject;
        }
    }

    public void DisplayQuest()
    {
        if (InputManager.A_Button() && (questMainList.Count > 0 || questSideList.Count > 0))
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            displayQuest = selectIndex + 1;
            if(!mainQuest)
            {
                displayQuest += 3;
            }
            SetDisplay();
        }
    }


    IEnumerator PadBuffer()
    {
        yield return new WaitForSeconds(.25f);
        padActive = false;
    }



}
