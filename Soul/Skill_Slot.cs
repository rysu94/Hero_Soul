using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Skill_Slot : MonoBehaviour
{
    public int slotIndex;
    public GameObject skillSelected;

    public string talentName;
    public string talentDesc;

	// Use this for initialization
	void Start ()
    {
        GetComponent<Button>().onClick.AddListener(SkillSelect);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void SkillSelect()
    {
        if(slotIndex >= 0 && slotIndex < 3)
        {
            Soul_Manager.playerSelectedSkills[0] = slotIndex;
        }
        else if(slotIndex >= 3 && slotIndex < 6)
        {
            Soul_Manager.playerSelectedSkills[1] = slotIndex;
        }
        else if(slotIndex >=6 && slotIndex < 9)
        {
            Soul_Manager.playerSelectedSkills[2] = slotIndex;
        }
        else if(slotIndex >= 9 && slotIndex < 12)
        {
            Soul_Manager.playerSelectedSkills[3] = slotIndex;
        }
        else if(slotIndex >= 12 && slotIndex < 15)
        {
            Soul_Manager.playerSelectedSkills[4] = slotIndex;
        }
        else if(slotIndex >= 15 && slotIndex < 18)
        {
            Soul_Manager.playerSelectedSkills[5] = slotIndex;
        }
        UpdateSelected();
    }

    void UpdateSelected()
    {
        GameObject.Find("SelectNoise").GetComponent<AudioSource>().Play();
        foreach (GameObject skill in GameObject.FindGameObjectsWithTag("Soul_Button"))
        {
            skill.GetComponent<Skill_Slot>().skillSelected.SetActive(false);
        }

        foreach (GameObject skill in GameObject.FindGameObjectsWithTag("Soul_Button"))
        {
            for(int i = 0; i < Soul_Manager.playerSelectedSkills.Length; i++)
            {
                if(skill.GetComponent<Skill_Slot>().slotIndex == Soul_Manager.playerSelectedSkills[i])
                {
                    skill.GetComponent<Skill_Slot>().skillSelected.SetActive(true);
                }
            }
        }

    }
}
