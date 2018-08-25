using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    

    public GameObject questMenu;

    //Main Quests
    public static List<Quest> activeMainQuests = new List<Quest>();

    //Side Quests
    public static List<Quest> activeSideQuests = new List<Quest>();

    public GameObject systemMessage;
    public Text systemText;

    public GameObject newQuest;
    public static bool questActive = false;

    // Use this for initialization
    void Start ()
    {
        SpawnQuestListeners();
        if(questActive)
        {
            newQuest.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void AddQuest(Quest quest)
    {
        //Check if it's a main quest 
        if(quest.questType == 0)
        {
            bool questExists = false;
            //check if quest exists
            for(int i = 0; i < activeMainQuests.Count; i++)
            {
                if(activeMainQuests[i].questName == quest.questName)
                {
                    questExists = true;
                }
            }

            //Check if you are max quests
            if(activeMainQuests.Count < 3 && !questExists)
            {
                activeMainQuests.Add(quest);
                systemMessage.SetActive(true);
                systemMessage.GetComponent<AudioSource>().Play();
                systemText.text = "Quest Accepted!\n<color=yellow>- " + quest.questName + "</color>";
                questMenu.SetActive(true);
                questMenu.GetComponent<QuestController>().UpdateQuests();
                questMenu.SetActive(false);
                SpawnQuestListeners();
                newQuest.SetActive(true);
                questActive = true;
            }
            else if(activeMainQuests.Count >= 3)
            {
                systemMessage.SetActive(true);
                GameObject.Find("ErrorNoise").GetComponent<AudioSource>().Play();
                systemText.text = "You can't accept any more main quests!";
            }
            else if(questExists)
            {
                systemMessage.SetActive(true);
                GameObject.Find("ErrorNoise").GetComponent<AudioSource>().Play();
                systemText.text = "You have already accepted this Quest!";
            }
            
        }
        //Check if it's a side quest
        else
        {
            bool questExists = false;
            //check if quest exists
            for (int i = 0; i < activeSideQuests.Count; i++)
            {
                if (activeSideQuests[i].questName == quest.questName)
                {
                    questExists = true;
                }
            }

            //Check if you are max side quests
            if (activeSideQuests.Count < 3 && !questExists)
            {
                activeSideQuests.Add(quest);
                systemMessage.SetActive(true);
                systemMessage.GetComponent<AudioSource>().Play();
                systemText.text = "Quest Accepted!\n<color=yellow>- " + quest.questName + "</color>";
                questMenu.SetActive(true);
                questMenu.GetComponent<QuestController>().UpdateQuests();
                questMenu.SetActive(false);
                SpawnQuestListeners();
                newQuest.SetActive(true);
                questActive = true;
            }
            else if(activeSideQuests.Count >= 3)
            {
                systemMessage.SetActive(true);
                GameObject.Find("ErrorNoise").GetComponent<AudioSource>().Play();
                systemText.text = "You can't accept any more side quests!";
            }
            else if(questExists)
            {
                systemMessage.SetActive(true);
                GameObject.Find("ErrorNoise").GetComponent<AudioSource>().Play();
                systemText.text = "You have already accepted this Quest!";
            }
        }
    }

    public void RemoveQuest(string name)
    {
        
        //Remove quest if in main
        for(int i = activeMainQuests.Count - 1; i >= 0; i--)
        {
            if(activeMainQuests[i].questName == name)
            {

                activeMainQuests.RemoveAt(i);
                questMenu.SetActive(true);
                questMenu.GetComponent<QuestController>().UpdateQuests();
                if(QuestController.displayQuest == i+1)
                {
                    QuestController.displayQuest = 0;
                } 
                questMenu.GetComponent<QuestController>().SetDisplay();
                questMenu.SetActive(false);
               
            }
        }

        //Remove quest if in side
        for(int j = activeSideQuests.Count - 1; j >= 0; j--)
        {
            if(activeSideQuests[j].questName == name)
            {

                activeSideQuests.RemoveAt(j);
                questMenu.SetActive(true);
                questMenu.GetComponent<QuestController>().UpdateQuests();
                if (QuestController.displayQuest == j+4)
                {
                    QuestController.displayQuest = 0;
                }
                questMenu.GetComponent<QuestController>().SetDisplay();
                questMenu.SetActive(false);

            }
        }
    }

    public void SpawnQuestListeners()
    {
        foreach(GameObject listener in GameObject.FindGameObjectsWithTag("Quest_Listener"))
        {
            Destroy(listener);
        }

        for(int i = 0; i < activeMainQuests.Count; i++)
        {
            CheckListener(activeMainQuests[i].questName);
        }

        for(int i = 0; i < activeSideQuests.Count; i++)
        {
            CheckListener(activeSideQuests[i].questName);
        }

    }

    public void CheckListener(string name)
    {
        switch(name)
        {
            default:
                break;
            case "Explore Koros":
                Instantiate(Resources.Load("Prefabs/Quest/Quest_Listeners/Explore_Koros"), new Vector2(0, 0), Quaternion.identity);
                break;
            case "Slime Hunter I":
                Instantiate(Resources.Load("Prefabs/Quest/Quest_Listeners/Slime_Hunter"), new Vector2(0, 0), Quaternion.identity);
                break;
            case "Card Collector I":
                Instantiate(Resources.Load("Prefabs/Quest/Quest_Listeners/Card_Collector"), new Vector2(0, 0), Quaternion.identity);
                break;
            case "Masky Hunter I":
                Instantiate(Resources.Load("Prefabs/Quest/Quest_Listeners/Masky_Hunter"), new Vector2(0, 0), Quaternion.identity);
                break;
            case "Stocking Up I":
                Instantiate(Resources.Load("Prefabs/Quest/Quest_Listeners/Stocking_Up"), new Vector2(0, 0), Quaternion.identity);
                break;
        }
    }

    public void PrintSystemMessage(string message)
    {
        systemMessage.SetActive(true);
        systemMessage.GetComponent<AudioSource>().Play();
        systemText.text = message;
        questActive = true;
        newQuest.SetActive(true);
    }

    public void UpdateQuestList()
    {
        if(!questMenu.activeInHierarchy)
        {
            questMenu.SetActive(true);
            questMenu.GetComponent<QuestController>().UpdateQuests();
            questMenu.GetComponent<QuestController>().SetDisplay();
            questMenu.SetActive(false);
        }

    }



}
