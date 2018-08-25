using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Slime_Hunter_Listener : MonoBehaviour
{
    public static int slimeCount = 5;
    public static bool haveCrystal = false;

    public int roomSlime = 0;


    public static bool obj2 = false;
    public static bool questComplete = false;


	// Use this for initialization
	void Start ()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if(enemy.GetComponent<SlimeController>())
            {
                roomSlime++;
            }       
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!questComplete)
        {
            //Objective 1
            if (slimeCount > 0)
            {
                int tempInt = 0;
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    if (enemy.GetComponent<SlimeController>())
                    {
                        tempInt++;
                    }
                }

                if (tempInt < roomSlime)
                {
                    slimeCount -= (roomSlime - tempInt);

                    GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
                    tempObj.GetComponent<Text>().text = "<color=yellow>Slime Hunter I</color>:  " + (5-slimeCount) + "/5 slimes killed!";

                    roomSlime -= (roomSlime - tempInt);
                }
            }

            if (slimeCount <= 0)
            {
                for (int i = 0; i < QuestManager.activeSideQuests.Count; i++)
                {
                    if (QuestManager.activeSideQuests[i].questName == "Slime Hunter I")
                    {
                        QuestManager.activeSideQuests[i].objComplete[0] = true;
                    }
                }
            }

            //Objective 2
            for (int i = 0; i < QuestManager.activeSideQuests.Count; i++)
            {
                if (QuestManager.activeSideQuests[i].questName == "Slime Hunter I")
                {
                    QuestManager.activeSideQuests[i].objComplete[1] = checkItem();
                    if(checkItem() && !obj2)
                    {
                        obj2 = true;
                        GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
                        tempObj.GetComponent<Text>().text = "<color=yellow>Slime Hunter I</color>: G-Slime Crystal Aquired!";
                    }
                    if(!checkItem())
                    {
                        obj2 = false;
                    }
                }
            }
            GameObject.Find("QuestManager").GetComponent<QuestManager>().UpdateQuestList();
        }

        if (CheckCompletion() && !questComplete)
        {
            questComplete = true;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().PrintSystemMessage("New Objective added!\n<color=yellow>-Slime Hunter I</color>");
            //Add a new objective
            for (int i = 0; i < QuestManager.activeSideQuests.Count; i++)
            {
                if (QuestManager.activeSideQuests[i].questName == "Slime Hunter I")
                {
                    QuestManager.activeSideQuests[i].questObj.Add("Return the Guild Master.");
                    QuestManager.activeSideQuests[i].objComplete.Add(false);
                }
            }

        }

        UpdateObj1();


	}

    bool checkItem()
    {
        for(int i = 0; i < InventoryManager.playerInventory.Length; i++)
        {
            if(InventoryManager.playerInventory[i].itemID == 30)
            {
                return true;      
            }
        }
        return false;
    }

    bool CheckCompletion()
    {
        for (int i = 0; i < QuestManager.activeSideQuests.Count; i++)
        {
            if (QuestManager.activeSideQuests[i].questName == "Slime Hunter I")
            {
                if(QuestManager.activeSideQuests[i].objComplete[0] && QuestManager.activeSideQuests[i].objComplete[1])
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void UpdateSlime()
    {
        roomSlime = 0;
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemy.GetComponent<SlimeController>())
            {
                roomSlime++;
            }
        }
    }

    public void UpdateObj1()
    {
        for (int i = 0; i < QuestManager.activeSideQuests.Count; i++)
        {
            if (QuestManager.activeSideQuests[i].questName == "Slime Hunter I")
            {
                QuestManager.activeSideQuests[i].questObj[0] = "Defeat " + (5-slimeCount) + "/5 Green Slimes";
            }
        }
    }
}
