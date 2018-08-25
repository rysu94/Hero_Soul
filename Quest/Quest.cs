using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Quest: MonoBehaviour
{
    public string questName;
    public int questType;
    public string questIMG;
    public List<string> questObj = new List<string>();
    public List<bool> objComplete = new List<bool>();
    public string questDesc;

    //Quest rewards
    public List<Item> questRewards = new List<Item>();
    public int questXP;
    public int questGold;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public Quest(string name, int type, string img, string desc, List<string> obj, List<Item> items, int xp, int gold)
    {
        questName = name;

        questType = type;

        questIMG = img;
        questDesc = desc;

        questObj = obj;

        for (int i = 0; i < questObj.Count; i++)
        {
            objComplete.Add(false);
        }

        questRewards = items;
        questXP = xp;
        questGold = gold;
    }


    //Check if a quest is complete
    public bool CheckQuestComplete()
    {
        for(int i = 0; i < objComplete.Count; i++)
        {
            if(!objComplete[i])
            {
                return false;
            }
        }
        return true;
    }

    public void DistributeReward()
    {
        //print("Reward");
        //Distribute Gold
        InventoryManager.playerGold += questGold;
        
        if(questGold > 0)
        {
            GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
            tempObj.GetComponent<Text>().text = "Obtained <color=yellow>" + questGold + "</color> gold.";
        }


        //Distribute XP
        for(int i = 0; i < questXP; i++)
        {
            PlayerStats.GainXP(1);
        }

        if(questXP > 0)
        {
            GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
            tempObj.GetComponent<Text>().text = "Gained <color=lime>" + questXP + "</color> XP.";
        }
        


        //Spawn Items
        for (int i = 0; i < questRewards.Count; i++)
        {
            //Check if there is an empty inventory space
            bool spaceFound = false;
            GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
            tempObj.GetComponent<Text>().text = "Obtained " + questRewards[i].itemName + ".";
            for (int j = 0; j < InventoryManager.playerInventory.Length; j++)
            {
                if (InventoryManager.playerInventory[j].itemID == 0)
                {
                    InventoryManager.playerInventory[j] = questRewards[i];
                    spaceFound = true;
                    break;
                }
            }
            //if no space is found instantiate item
            if(!spaceFound)
            {
                Instantiate(Resources.Load("Prefabs/Items/" + questRewards[i].itemIconName), TestCharController.player.transform.position, Quaternion.identity);
            }
            
        }
       

    }
}
