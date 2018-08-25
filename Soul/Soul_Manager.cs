using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul_Manager : MonoBehaviour
{
    public static bool init = false;

    public static bool soulEnabled = false;

    //Stores the index of the skill selected on that tier
    public static int[] playerSelectedSkills = new int[6];

    //Stores the talents on player skill tree
    public static List<Talent> playerTalentTree = new List<Talent>();

    //The current hero selected: 0 = None, 1 = Berserker, 2 = Vanguard, 3 = Assassin, 4 = Ranger, 5 = Mage, 6 = Sage
    public static int heroSelected = 0;

    //The current skill tier the player is on
    public static int currentTier = 0;

    //Stores the ID of the break skill the player has chosen
    public static int playerBreakSkill;

    //Stores which Heros the player has unlocked
    public static List<int> unlockedHeros = new List<int>();

	// Use this for initialization
	void Start ()
    {
		if(!init)
        {
            init = true;
            for(int i = 0; i < 6; i++)
            {
                playerSelectedSkills[i] = -1;
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //Adds 3 more talents based on the hero selected and tier
    public void LevelUp(int tier)
    {
        switch(heroSelected)
        {
            default:
                break;
            case 1: //Berserker
                for(int i = 0; i < 3; i++)
                {
                    playerTalentTree.Add(Soul_Database.talentBerserk[i + (tier*3)]);
                }
                playerSelectedSkills[currentTier] = tier * 3; 
                break;
            case 2: //Vanguard
                for (int i = 0; i < 3; i++)
                {
                    playerTalentTree.Add(Soul_Database.talentVanguard[i + (tier*3)]);
                }
                playerSelectedSkills[currentTier] = tier * 3;
                break;
            case 3: //Assassin
                for (int i = 0; i < 3; i++)
                {
                    playerTalentTree.Add(Soul_Database.talentAssassin[i + (tier*3)]);
                }
                playerSelectedSkills[currentTier] = tier * 3;
                break;
            case 4: //Ranger
                for (int i = 0; i < 3; i++)
                {
                    playerTalentTree.Add(Soul_Database.talentRanger[i + (tier*3)]);
                }
                playerSelectedSkills[currentTier] = tier * 3;
                break;
            case 5: //Mage
                for (int i = 0; i < 3; i++)
                {
                    playerTalentTree.Add(Soul_Database.talentMage[i + (tier*3)]);
                }
                playerSelectedSkills[currentTier] = tier * 3;
                break;
            case 6: //Sage
                for (int i = 0; i < 3; i++)
                {
                    playerTalentTree.Add(Soul_Database.talentSage[i + (tier*3)]);
                }
                playerSelectedSkills[currentTier] = tier * 3;
                break;
        }
    }


}
