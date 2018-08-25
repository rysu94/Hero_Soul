using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Card_Collector_Listener : MonoBehaviour
{

    public static int cardCount;
    public int currentCount = 0;

    public static bool questComplete = false;

	// Use this for initialization
	void Start ()
    {
        cardCount = 0;
		for(int i = 0; i < InventoryManager.playerSpellbook.Length; i++)
        {
            if(InventoryManager.playerSpellbook[i])
            {
                cardCount++;
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(cardCount >= 10 && !questComplete)
        {
            questComplete = true;
            GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
            tempObj.GetComponent<Text>().text = "<color=yellow>Card Collector I</color>: 10/10 cards discoverd!";
            GameObject.Find("QuestManager").GetComponent<QuestManager>().PrintSystemMessage("New Objective added!\n<color=yellow>-Card Collector I</color>");

            //Add a new objective
            for (int i = 0; i < QuestManager.activeSideQuests.Count; i++)
            {
                if (QuestManager.activeSideQuests[i].questName == "Card Collector I")
                {
                    QuestManager.activeSideQuests[i].objComplete[0] = true;
                    QuestManager.activeSideQuests[i].questObj.Add("Return to Fiona.");
                    QuestManager.activeSideQuests[i].objComplete.Add(false);
                }
            }
        }

        currentCount = 0;
        for(int i = 0; i < InventoryManager.playerSpellbook.Length; i++)
        {
            if(InventoryManager.playerSpellbook[i])
            {
                currentCount++;
            }
        }

        if(!questComplete && (currentCount > cardCount))
        {
            GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
            tempObj.GetComponent<Text>().text = "<color=yellow>Card Collector I</color>: " + currentCount + "/10 cards discoverd!";
            cardCount = currentCount;
            UpdateCardCount();
        }

        
	}

    void UpdateCardCount()
    {
       
        for (int i = 0; i < QuestManager.activeSideQuests.Count; i++)
        {
            if (QuestManager.activeSideQuests[i].questName == "Card Collector I")
            {
                currentCount = 0;
                for (int j = 0; j < InventoryManager.playerSpellbook.Length; j++)
                {
                    if (InventoryManager.playerSpellbook[j])
                    {
                        currentCount++;
                    }
                }
                QuestManager.activeSideQuests[i].questObj[0] = "Discover " + currentCount + "/10 unique arcana.";
            }
        }
    }
}
