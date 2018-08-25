using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Explore_Koros_Listener : MonoBehaviour
{
    public static int bossCount = 1;

    public int roomBoss = 0;

    public static bool questComplete = false;

	// Use this for initialization
	void Start ()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemy.GetComponent<SlimeKingController>())
            {
                roomBoss++;
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(!questComplete)
        {
            if(bossCount > 0)
            {
                int tempInt = 0;
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    if (enemy.GetComponent<SlimeKingController>())
                    {
                        tempInt++;
                    }
                }

                if(tempInt < roomBoss)
                {
                    bossCount -= (roomBoss - tempInt);
                    if(bossCount <= 0)
                    {
                        GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
                        tempObj.GetComponent<Text>().text = "<color=yellow>Explore Koros</color>: Dungeon boss killed!";
                        questComplete = true;
                        GameObject.Find("QuestManager").GetComponent<QuestManager>().PrintSystemMessage("Quest Complete!\n<color=yellow>-Explore Koros</color>");

                        for(int i = 0; i < QuestManager.activeMainQuests.Count; i++)
                        {
                            if(QuestManager.activeMainQuests[i].questName == "Explore Koros")
                            {
                                QuestManager.activeMainQuests[i].DistributeReward();
                            }
                        }

                        GameObject.Find("QuestManager").GetComponent<QuestManager>().RemoveQuest("Explore Koros");
                        
                    }
                }
            }

            GameObject.Find("QuestManager").GetComponent<QuestManager>().UpdateQuestList();
        }	
	}
}
