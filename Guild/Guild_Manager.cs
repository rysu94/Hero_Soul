using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guild_Manager : MonoBehaviour
{
    public static bool init = false;

    public static List<Quest> GuildQuestWeiss = new List<Quest>();


	// Use this for initialization
	void Start ()
    {
		if(!init)
        {
            init = true;
            //Slime Hunter I
            GuildQuestWeiss.Add(QuestDatabase.testSideQuest1);
            GuildQuestWeiss.Add(QuestDatabase.side1);
            GuildQuestWeiss.Add(QuestDatabase.side2);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
