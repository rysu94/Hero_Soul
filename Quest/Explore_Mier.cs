using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explore_Mier : MonoBehaviour
{

    public static bool questAccepted = false;
    public GameObject quest;

    // Use this for initialization
    void Start()
    {
        if (!questAccepted)
        {
            StartCoroutine(AddQuest());
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator AddQuest()
    {
        yield return new WaitForSeconds(1f);
        questAccepted = true;
        quest.GetComponent<QuestManager>().AddQuest(QuestDatabase.quest1);
    }
}
