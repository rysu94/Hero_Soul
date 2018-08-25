using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Guild_Controller : MonoBehaviour
{
    public GameObject questPanel, questDescPanel, guildHUD;
    public Text questName, questDesc;
    public Button confirm, done, close;
    public static int questIndex;

    //1 = Wiess, 
    public static int guildID = 1;

	// Use this for initialization
	void Start ()
    {
        done.onClick.AddListener(closeHUD);
        confirm.onClick.AddListener(confirmQuest);

        UpdateQuests();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void closeHUD()
    {
        questName.text = "";
        questDesc.text = "";
        GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
        guildHUD.SetActive(false);
        confirm.gameObject.SetActive(false);
        TestCharController.inDialogue = false;
    }

    void confirmQuest()
    {
        
        if(QuestManager.activeSideQuests.Count < 3)
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            switch (guildID)
            {
                default:
                    break;
                case 1:
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().AddQuest(Guild_Manager.GuildQuestWeiss[questIndex]);
                    Guild_Manager.GuildQuestWeiss.RemoveAt(questIndex);
                    questName.text = "";
                    questDesc.text = "";
                    confirm.gameObject.SetActive(false);
                    break;
            }
            UpdateQuests();
        }
        else
        {
            GameObject.Find("ErrorNoise").GetComponent<AudioSource>().Play();
        }

    }

    public void UpdateQuests()
    {
        foreach (GameObject reward in GameObject.FindGameObjectsWithTag("Guild_Quest"))
        {
            Destroy(reward);
        }

        switch(guildID)
        {
            default:
                break;
            case 1:
                for(int i = 0; i < Guild_Manager.GuildQuestWeiss.Count; i++)
                {
                    GameObject tempObj = Instantiate(Resources.Load("Prefabs/Guild/Guild_Quest"), questPanel.transform) as GameObject;
                    tempObj.GetComponent<Guild_Quest>().questName = Guild_Manager.GuildQuestWeiss[i].questName;
                    tempObj.GetComponent<Guild_Quest>().questDesc = Guild_Manager.GuildQuestWeiss[i].questDesc;
                    tempObj.transform.Find("QuestImage").GetComponent<Image>().sprite = Resources.Load<Sprite>("Guild/" + Guild_Manager.GuildQuestWeiss[i].questIMG);
                    tempObj.GetComponent<Guild_Quest>().index = i;
                }



                break;
        }

        
    }
}
