using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Stocking_Up_Listener : MonoBehaviour
{
    public int prevMush, prevCara, prevWing;
    public int redMushCount, hardCaraCount, batWingCount;
    public int questIndex;

    public static bool questCompleted = false;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(ListenerCheck());
    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    IEnumerator ListenerCheck()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            UpdateItemCount();
            CheckComplete();
        }
    }

    void CheckComplete()
    {
        int tempInt = 0;
        for(int i = 0; i < QuestManager.activeSideQuests[questIndex].objComplete.Count; i++)
        {
            if(QuestManager.activeSideQuests[questIndex].objComplete[i])
            {
                tempInt++;
            }
        }
        if(tempInt >= 3)
        {
            questCompleted = true;
            if(QuestManager.activeSideQuests[questIndex].questObj.Count < 4)
            {
                QuestManager.activeSideQuests[questIndex].questObj.Add("Return to Theo.");
                QuestManager.activeSideQuests[questIndex].objComplete.Add(false);
                GameObject.Find("QuestManager").GetComponent<QuestManager>().PrintSystemMessage("New Objective added!\n<color=yellow>-Stocking Up I</color>");
            }
        }
        else
        {
            questCompleted = false;
        }
        
    }


    void UpdateItemCount()
    {

        for (int i = 0; i < QuestManager.activeSideQuests.Count; i++)
        {
            if (QuestManager.activeSideQuests[i].questName == "Stocking Up I")
            {
                questIndex = i;
                break;
            }
        }

        prevMush = redMushCount;
        prevCara = hardCaraCount;
        prevWing = batWingCount;

        redMushCount = 0;
        hardCaraCount = 0;
        batWingCount = 0;
        for(int i = 0; i < InventoryManager.playerInventory.Length; i++)
        {
            if(InventoryManager.playerInventory[i].itemID == 7)
            {
                redMushCount += InventoryManager.playerInventory[i].itemQuantity;         
            }
            else if(InventoryManager.playerInventory[i].itemID == 37)
            {
                hardCaraCount += InventoryManager.playerInventory[i].itemQuantity;         
            }
            else if(InventoryManager.playerInventory[i].itemID == 6)
            {
                batWingCount += InventoryManager.playerInventory[i].itemQuantity;     
            }
        }

        if(redMushCount > 10)
        {
            redMushCount = 10;
        }
        if(hardCaraCount > 10)
        {
            hardCaraCount = 10;
        }
        if(batWingCount > 5)
        {
            batWingCount = 5;
        }
        QuestManager.activeSideQuests[questIndex].questObj[0] = "Gather " + redMushCount + "/10 Red Mushrooms.";
        QuestManager.activeSideQuests[questIndex].questObj[1] = "Gather " + hardCaraCount + "/10 Hard Carapaces.";
        QuestManager.activeSideQuests[questIndex].questObj[2] = "Gather " + batWingCount + "/5 Small Feathers.";


        if (redMushCount > prevMush)
        {
            GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
            tempObj.GetComponent<Text>().text = "<color=yellow>Stocking Up I</color>: " + redMushCount + "/10 Red Mushrooms collected!";
        }
        if(hardCaraCount > prevCara)
        {
            GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
            tempObj.GetComponent<Text>().text = "<color=yellow>Stocking Up I</color>: " + hardCaraCount + "/10 Hard Carapaces collected!";
        }
        if(batWingCount > prevWing)
        {
            GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
            tempObj.GetComponent<Text>().text = "<color=yellow>Stocking Up I</color>: " + batWingCount + "/5 Small Feathers collected!";
        }

        if(redMushCount >= 10)
        {
            QuestManager.activeSideQuests[questIndex].objComplete[0] = true;
        }
        else
        {
            QuestManager.activeSideQuests[questIndex].objComplete[0] = false;
        }
        if(hardCaraCount >= 10)
        {
            QuestManager.activeSideQuests[questIndex].objComplete[1] = true;
        }
        else
        {
            QuestManager.activeSideQuests[questIndex].objComplete[1] = false;
        }
        if(batWingCount >= 5)
        {
            QuestManager.activeSideQuests[questIndex].objComplete[2] = true;
        }
        else
        {
            QuestManager.activeSideQuests[questIndex].objComplete[2] = false;
        }

        GameObject.Find("QuestManager").GetComponent<QuestManager>().UpdateQuestList();
    }



}
