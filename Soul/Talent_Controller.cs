using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Talent_Controller : MonoBehaviour
{
    public GameObject talentFrame;


	// Use this for initialization
	void Start ()
    {
        UpdateTalents();
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    void UpdateTalents()
    {
        foreach (GameObject preview in GameObject.FindGameObjectsWithTag("Skill_Preview"))
        {
            Destroy(preview);
        }

        //Generate selected skills
        for (int i = 0; i < Soul_Manager.playerSelectedSkills.Length; i++)
        {
            if(Soul_Manager.playerSelectedSkills[i] >= 0)
            {
                GameObject tempObj = Instantiate(Resources.Load("Prefabs/Soul/Skill_Preview_Small"), talentFrame.transform) as GameObject;
                tempObj.transform.Find("Preview_IMG").GetComponent<Image>().sprite = Resources.Load<Sprite>("Soul/" + Soul_Manager.playerTalentTree[Soul_Manager.playerSelectedSkills[i]].talentIMG);
                tempObj.transform.Find("TalentName").GetComponent<Text>().text = Soul_Manager.playerTalentTree[Soul_Manager.playerSelectedSkills[i]].talentName;
                tempObj.transform.Find("TalentDesc").GetComponent<Text>().text = Soul_Manager.playerTalentTree[Soul_Manager.playerSelectedSkills[i]].talentDesc;
            }
        }

        //Generate blank skills
        int count = 0;
        foreach (GameObject preview in GameObject.FindGameObjectsWithTag("Skill_Preview"))
        {
            count++;
        }

        int numPreviews = 0;
        if (PlayerStats.playerLevel >= 0 && PlayerStats.playerLevel < 5)
        {
            numPreviews = 1;
        }
        else if (PlayerStats.playerLevel >= 5 && PlayerStats.playerLevel < 10)
        {
            numPreviews = 2;
        }
        else if(PlayerStats.playerLevel >= 10 && PlayerStats.playerLevel < 15)
        {
            numPreviews = 3;
        }
        else if(PlayerStats.playerLevel >= 15 && PlayerStats.playerLevel < 20)
        {
            numPreviews = 4;
        }
        else if(PlayerStats.playerLevel >= 20 && PlayerStats.playerLevel < 25)
        {
            numPreviews = 5;
        }
        else
        {
            numPreviews = 6;
        }


        for (int i = 0; i < (numPreviews - count); i++)
        {

            GameObject tempObj = Instantiate(Resources.Load("Prefabs/Soul/Skill_Preview_Small"), talentFrame.transform) as GameObject;
        }

    }
}
