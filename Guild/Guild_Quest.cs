using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Guild_Quest : MonoBehaviour
{
    public string questName, questDesc;
    public Image questIMG;
    public int index;

	// Use this for initialization
	void Start ()
    {
        GetComponent<Button>().onClick.AddListener(UpdateQuest);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
     void UpdateQuest()
    {
        GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
        GameObject.Find("GuildController").GetComponent<Guild_Controller>().questDescPanel.transform.Find("Quest_Name").GetComponent<Text>().text = questName;
        GameObject.Find("GuildController").GetComponent<Guild_Controller>().questDescPanel.transform.Find("Quest Desc").GetComponent<Text>().text = questDesc;
        GameObject.Find("GuildController").GetComponent<Guild_Controller>().questDescPanel.transform.Find("Confirm_Button").gameObject.SetActive(true);
        Guild_Controller.questIndex = index;
    }
}
