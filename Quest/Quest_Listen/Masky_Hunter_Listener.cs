using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Masky_Hunter_Listener : MonoBehaviour
{
    public static int maskyCount = 7;

    public int roomMasky = 0;
    public static bool questComplete = false;

    // Use this for initialization
    void Start ()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemy.GetComponent<Masky_Controller>())
            {
                roomMasky++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!questComplete)
        {
            //Objective 1
            if (maskyCount > 0)
            {
                int tempInt = 0;
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    if (enemy.GetComponent<Masky_Controller>())
                    {
                        tempInt++;
                    }
                }

                if (tempInt < roomMasky)
                {
                    maskyCount -= (roomMasky - tempInt);

                    GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
                    tempObj.GetComponent<Text>().text = "<color=yellow>Masky Hunter I</color>:  " + (7 - maskyCount) + "/7 Maskys killed!";

                    roomMasky -= (roomMasky - tempInt);
                }
            }

            if (maskyCount <= 0)
            {
                for (int i = 0; i < QuestManager.activeSideQuests.Count; i++)
                {
                    if (QuestManager.activeSideQuests[i].questName == "Masky Hunter I")
                    {
                        QuestManager.activeSideQuests[i].objComplete[0] = true;
                    }
                }
            }
        }

        if (CheckCompletion() && !questComplete)
        {
            questComplete = true;
            GameObject.Find("QuestManager").GetComponent<QuestManager>().PrintSystemMessage("New Objective added!\n<color=yellow>-Masky Hunter I</color>");
            //Add a new objective
            for (int i = 0; i < QuestManager.activeSideQuests.Count; i++)
            {
                if (QuestManager.activeSideQuests[i].questName == "Masky Hunter I")
                {
                    QuestManager.activeSideQuests[i].questObj.Add("Return the Guild Master.");
                    QuestManager.activeSideQuests[i].objComplete.Add(false);
                }
            }

        }

        UpdateObj();

    }

    bool CheckCompletion()
    {
        for (int i = 0; i < QuestManager.activeSideQuests.Count; i++)
        {
            if (QuestManager.activeSideQuests[i].questName == "Masky Hunter I")
            {
                if (QuestManager.activeSideQuests[i].objComplete[0])
                {
                    return true;
                }
            }
        }
        return false;
    }
    public void UpdateObj()
    {
        for (int i = 0; i < QuestManager.activeSideQuests.Count; i++)
        {
            if (QuestManager.activeSideQuests[i].questName == "Masky Hunter I")
            {
                QuestManager.activeSideQuests[i].questObj[0] = "Defeat " + (7 - maskyCount) + "/7 Maskys.";
            }
        }
    }
}
