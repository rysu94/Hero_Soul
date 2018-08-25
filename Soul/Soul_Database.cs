using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul_Database : MonoBehaviour
{
    public static bool init = false;

    public static List<Talent> talentDB = new List<Talent>();

    public static List<Talent> talentBerserk = new List<Talent>();
    public static List<Talent> talentVanguard = new List<Talent>();
    public static List<Talent> talentAssassin = new List<Talent>();
    public static List<Talent> talentRanger = new List<Talent>();
    public static List<Talent> talentMage = new List<Talent>();
    public static List<Talent> talentSage = new List<Talent>();

    // Use this for initialization
    void Start ()
    {
		if(!init)
        {
            InitTalents();
            init = true;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    void InitTalents()
    {
        init = true;
        //string name, int str, int dex, int end, int intel, int wis, int vit, int def, int leech, int plund, 
        //int alc, int collect, int ass, int hunt, int eva, int arm, int shield, int berserk,  int deck

        //+5 str  0
        talentDB.Add(new Talent("+5 Strength", "StrUp+5", 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase strength by 5."));
        //+10 str  1
        talentDB.Add(new Talent("+10 Strength", "StrUp+10", 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase strength by 10."));
        //+15 str  2
        talentDB.Add(new Talent("+15 Strength", "StrUp+15", 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase strength by 15."));
        //+25 str  3
        talentDB.Add(new Talent("+25 Strength", "StrUp+25",25, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase strength by 25."));

        //+5 Crit  4
        talentDB.Add(new Talent("+5% Critical", "DexUp+5",0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase critical strike by 5%."));
        //+10 Crit  5
        talentDB.Add(new Talent("+10% Critical", "DexUp+10", 0, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase critical strike by 10%."));
        //+15 Crit  6
        talentDB.Add(new Talent("+15% Critical", "DexUp+15", 0, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase critical strike by 15%"));

        //+5 End  7
        talentDB.Add(new Talent("+5 Endurance", "EndUp+5", 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase endurance by 5."));
        //+10 End  8
        talentDB.Add(new Talent("+10 Endurance", "EndUp+10", 0, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase endurance by 10."));
        //+15 End  9
        talentDB.Add(new Talent("+15 Endurance", "EndUp+15", 0, 0, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase endurance by 15."));
        //+25 End  10
        talentDB.Add(new Talent("+25 Endurance", "EndUp+25", 0, 0, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase endurance by 25."));

        //+5 Int  11
        talentDB.Add(new Talent("+5 Intelligence", "IntUp+5", 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase Intelligence by 5."));
        //+10 Int  12
        talentDB.Add(new Talent("+10 Intelligence", "IntUp+10", 0, 0, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase Intelligence by 10."));
        //+15 Int  13
        talentDB.Add(new Talent("+15 Intelligence", "IntUp+15", 0, 0, 0, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase Intelligence by 15."));
        //+25 Int  14
        talentDB.Add(new Talent("+25 Intelligence", "IntUp+25", 0, 0, 0, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase Intelligence by 25."));

        //+5 Wis  15
        talentDB.Add(new Talent("+5 Wisdom", "Wis+5", 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase Wisdom by 5."));
        //+10 Wis  16
        talentDB.Add(new Talent("+10 Wisdom", "Wis+10", 0, 0, 0, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase Wisdom by 10."));
        //+15 Wis  17
        talentDB.Add(new Talent("+15 Wisdom", "Wis+15", 0, 0, 0, 0, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase Wisdom by 15."));
        //+25 Wis  18
        talentDB.Add(new Talent("+25 Wisdom", "Wis+25", 0, 0, 0, 0, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase Wisdom by 25."));

        //+5 Vit 19
        talentDB.Add(new Talent("+5 Vitality", "Vit+5", 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase Vitality by 5."));
        //+10 Vit 20
        talentDB.Add(new Talent("+10 Vitality", "Vit+10", 0, 0, 0, 0, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase Vitality by 10."));
        //+15 Vit 21
        talentDB.Add(new Talent("+15 Vitality", "Vit+15", 0, 0, 0, 0, 0, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase Vitality by 15."));
        //+25 Vit 22
        talentDB.Add(new Talent("+25 Vitality", "Vit+25", 0, 0, 0, 0, 0, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase Vitality by 25."));

        //+5 Def 23
        talentDB.Add(new Talent("+5 Defense", "DefUp+5", 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase Defense by 5."));
        //+10 Def 24
        talentDB.Add(new Talent("+10 Defense", "DefUp+10", 0, 0, 0, 0, 0, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase Defense by 10."));
        //+15 Def 25
        talentDB.Add(new Talent("+15 Defense", "DefUp+15", 0, 0, 0, 0, 0, 0, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase Defense by 15."));

        //+10% Leech 26
        talentDB.Add(new Talent("+10% Leech", "Leech+1", 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase chance to leech 10 Health by 10%."));
        //+20% Leech 27
        talentDB.Add(new Talent("+20% Leech", "Leech+2", 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase chance to leech 10 Health by 20%."));
        //+30% Leech 28
        talentDB.Add(new Talent("+30% Leech", "Leech+3", 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase chance to leech 10 Health by 30%."));

        //+10% Plunder 29
        talentDB.Add(new Talent("+10% Plunder", "Plunder+1", 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase chance to plunder gold on kill by 10%."));
        //+20% Plunder 30
        talentDB.Add(new Talent("+20% Plunder", "Plunder+2", 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase chance to plunder gold on kill by 20%."));
        //+30% Plunder 31
        talentDB.Add(new Talent("+30% Plunder", "Plunder+3", 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase chance to plunder gold on kill by 30%."));

        //Alc +1 32
        talentDB.Add(new Talent("Alchemy +1", "Alc+1", 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increases chance to drop additional crafting materials by 10%."));
        //Alc +2 33
        talentDB.Add(new Talent("Alchemy +2", "Alc+2", 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increases chance to drop additional crafting materials by 20%."));
        //Alc +3 34
        talentDB.Add(new Talent("Alchemy +3", "Alc+3", 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increase chance to drop additional crafting materials by 30%."));

        //Collector +1 35
        talentDB.Add(new Talent("Collector +1", "Collector+1", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increases dropped Arcana by 1."));
        //Collector +2 36
        talentDB.Add(new Talent("Collector +2", "Collector+2", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increases dropped Arcana by 2."));
        //Collector +3 37
        talentDB.Add(new Talent("Collector +3", "Collector+3", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increases dropeed Arcana by 3."));

        //Assassin +5% 38
        talentDB.Add(new Talent("Assassin +5%", "Assn+5", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increases chance to instantly execute weaker enemies by 5%."));
        //Assassin +10% 39
        talentDB.Add(new Talent("Assassin +10%", "Assn+10", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increases chance to instantly execute weaker enemies by 10%."));
        //Assassin +15% 40
        talentDB.Add(new Talent("Assassin +15%", "Assn+15", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15, 0, 0, 0, 0, 0, 0, 0, 0,
            "Increases chance to instantly execute weaker enemies by 15%."));

        //Precision +5% 41
        talentDB.Add(new Talent("Precision +25%", "Prec+25", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 25, 0, 0, 0, 0, 0, 0, 0,
            "Increase critical strike multiplier by 25% (additive)."));
        //Precision +10% 42
        talentDB.Add(new Talent("Precision +50%", "Prec+50", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 50, 0, 0, 0, 0, 0, 0, 0,
            "Increases critical strike multipier by 50% (additive)."));
        //Precision +15% 43
        talentDB.Add(new Talent("Precision +75%", "Prec+75", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 75, 0, 0, 0, 0, 0, 0, 0,
            "Increase critical strike multiplier by 75% (additive)."));

        //Evasion +5% 44
        talentDB.Add(new Talent("Evasion +5%", "Eva+5", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0,
            "Increases chance to evade attack by 5%."));
        //Evasion +10% 45
        talentDB.Add(new Talent("Evasion +10%", "Eva+10", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 0, 0, 0, 0, 0, 0,
            "Increases chance to evade attack by 10%."));
        //Evasion +15% 46
        talentDB.Add(new Talent("Evasion +15%", "Eva+15", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15, 0, 0, 0, 0, 0, 0,
            "Increases chance to evade attack by 15%."));

        //Armor +50 47
        talentDB.Add(new Talent("Armor +50", "Arm+50", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 50, 0, 0, 0, 0, 0,
            "Increases armor shield granted per room by 50."));
        //Armor +100 48
        talentDB.Add(new Talent("Armor +100", "Arm+100", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0,
            "Increases armor shield granted per room by 100."));
        //Armor +150 49
        talentDB.Add(new Talent("Armor +150", "Arm+150", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 150, 0, 0, 0, 0, 0,
            "Increases armor shield granted per room by 150."));

        //Shield +25 50
        talentDB.Add(new Talent("Shield +25", "Shield+25", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 25, 0, 0, 0, 0,
            "Increases shield granted per room by 25."));
        //Shield +50 51
        talentDB.Add(new Talent("Shield +50", "Shield+50", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 50, 0, 0, 0, 0,
            "Increases shield granted per room by 50."));
        //Shield +75 52
        talentDB.Add(new Talent("Shield +75", "Shield+75", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 75, 0, 0, 0, 0,
            "Increases shield granted per room by 75."));

        //Berserk +25% 53
        talentDB.Add(new Talent("Berserk +25%", "Berserk+25", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 25, 0, 0, 0,
            "Increases damage when below 25% max health by 25%."));
        //Berserk +50% 54
        talentDB.Add(new Talent("Berserk +50%", "Berserk+50", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 50, 0, 0, 0,
            "Increases damage when below 25% max health by 50%."));
        //Berserk +75% 55
        talentDB.Add(new Talent("Berserk +75%", "Berserk+75", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 75, 0, 0, 0,
            "Increases damage when below 25% max health by 75%."));

        //Deck +5 56
        talentDB.Add(new Talent("Deck +5", "Deck+5", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0,
            "Increases your maximum Arcana Deck capacity by 5."));
        //Deck +10 57
        talentDB.Add(new Talent("Deck +10", "Deck+10", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 0, 0,
            "Increases your maximum Arcana Deck capacity by 10."));

        //Speed + 10% 58
        talentDB.Add(new Talent("Speed +10%", "Speed+10", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 0,
            "Increase base movement speed by 10%."));

        //Twin + 5% 59
        talentDB.Add(new Talent("Twin Strike +5%", "Twin+5", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5,
            "Increase chance to attack the enemy a 2nd time for 50% damage by 5%."));
        //Twin + 10% 60
        talentDB.Add(new Talent("Twin Strike +10%", "Twin+10", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10,
            "Increase chance to attack the enemy a 2nd time for 50% damage by 10%."));
        //Twin + 15% 61
        talentDB.Add(new Talent("Twin Strike +15%", "Twin+15", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15,
            "Increase chance to attack the enemy a 2nd time for 50% damage by 15%."));



        //Init Berserker Talents
        //Tier 1
        talentBerserk.Add(talentDB[0]);
        talentBerserk.Add(talentDB[7]);
        talentBerserk.Add(talentDB[19]);

        //Tier 2
        talentBerserk.Add(talentDB[1]);
        talentBerserk.Add(talentDB[20]);
        talentBerserk.Add(talentDB[26]);

        //Tier 3
        talentBerserk.Add(talentDB[1]);
        talentBerserk.Add(talentDB[8]);
        talentBerserk.Add(talentDB[53]);

        //Tier 4
        talentBerserk.Add(talentDB[2]);
        talentBerserk.Add(talentDB[21]);
        talentBerserk.Add(talentDB[27]);

        //Tier 5
        talentBerserk.Add(talentDB[2]);
        talentBerserk.Add(talentDB[9]);
        talentBerserk.Add(talentDB[54]);

        //Tier 6
        talentBerserk.Add(talentDB[3]);
        talentBerserk.Add(talentDB[28]);
        talentBerserk.Add(talentDB[55]);


        //Init Vanguard Talents
        //Tier 1
        talentVanguard.Add(talentDB[0]);
        talentVanguard.Add(talentDB[23]);
        talentVanguard.Add(talentDB[7]);

        //Tier2
        talentVanguard.Add(talentDB[0]);
        talentVanguard.Add(talentDB[20]);
        talentVanguard.Add(talentDB[47]);

        //Tier 3
        talentVanguard.Add(talentDB[1]);
        talentVanguard.Add(talentDB[24]);
        talentVanguard.Add(talentDB[8]);

        //Tier 4
        talentVanguard.Add(talentDB[1]);
        talentVanguard.Add(talentDB[21]);
        talentVanguard.Add(talentDB[48]);

        //Tier 5
        talentVanguard.Add(talentDB[1]);
        talentVanguard.Add(talentDB[25]);
        talentVanguard.Add(talentDB[9]);

        //Tier 6
        talentVanguard.Add(talentDB[2]);
        talentVanguard.Add(talentDB[22]);
        talentVanguard.Add(talentDB[49]);

        //Init Assassin Talents
        //Tier 1
        talentAssassin.Add(talentDB[0]);
        talentAssassin.Add(talentDB[4]);
        talentAssassin.Add(talentDB[29]);

        //Tier 2
        talentAssassin.Add(talentDB[0]);
        talentAssassin.Add(talentDB[38]);
        talentAssassin.Add(talentDB[44]);

        //Tier 3
        talentAssassin.Add(talentDB[1]);
        talentAssassin.Add(talentDB[5]);
        talentAssassin.Add(talentDB[30]);

        //Tier 4
        talentAssassin.Add(talentDB[1]);
        talentAssassin.Add(talentDB[39]);
        talentAssassin.Add(talentDB[58]);

        //Tier 5
        talentAssassin.Add(talentDB[2]);
        talentAssassin.Add(talentDB[6]);
        talentAssassin.Add(talentDB[31]);

        //Tier 6
        talentAssassin.Add(talentDB[2]);
        talentAssassin.Add(talentDB[40]);
        talentAssassin.Add(talentDB[45]);

        //Init Ranger Talents
        //Tier 1
        talentRanger.Add(talentDB[0]);
        talentRanger.Add(talentDB[59]);
        talentRanger.Add(talentDB[4]);

        //Tier 2
        talentRanger.Add(talentDB[0]);
        talentRanger.Add(talentDB[41]);
        talentRanger.Add(talentDB[44]);

        //Tier 3
        talentRanger.Add(talentDB[0]);
        talentRanger.Add(talentDB[60]);
        talentRanger.Add(talentDB[5]);

        //Tier 4
        talentRanger.Add(talentDB[1]);
        talentRanger.Add(talentDB[42]);
        talentRanger.Add(talentDB[45]);

        //Tier 5
        talentRanger.Add(talentDB[1]);
        talentRanger.Add(talentDB[61]);
        talentRanger.Add(talentDB[58]);

        //Tier 6
        talentRanger.Add(talentDB[1]);
        talentRanger.Add(talentDB[43]);
        talentRanger.Add(talentDB[46]);

        //Init Mage Talents
        //Tier 1
        talentMage.Add(talentDB[11]);
        talentMage.Add(talentDB[15]);
        talentMage.Add(talentDB[35]);

        //Tier 2
        talentMage.Add(talentDB[12]);
        talentMage.Add(talentDB[50]);
        talentMage.Add(talentDB[56]);

        //Tier 3
        talentMage.Add(talentDB[12]);
        talentMage.Add(talentDB[16]);
        talentMage.Add(talentDB[36]);

        //Tier 4
        talentMage.Add(talentDB[13]);
        talentMage.Add(talentDB[51]);
        talentMage.Add(talentDB[56]);

        //Tier 5
        talentMage.Add(talentDB[13]);
        talentMage.Add(talentDB[17]);
        talentMage.Add(talentDB[37]);

        //Tier 6
        talentMage.Add(talentDB[14]);
        talentMage.Add(talentDB[52]);
        talentMage.Add(talentDB[57]);

        //Init Safe Talents
        //Tier 1
        talentSage.Add(talentDB[11]);
        talentSage.Add(talentDB[50]);
        talentSage.Add(talentDB[32]);

        //Tier 2
        talentSage.Add(talentDB[11]);
        talentSage.Add(talentDB[15]);
        talentSage.Add(talentDB[56]);

        //Tier 3
        talentSage.Add(talentDB[12]);
        talentSage.Add(talentDB[51]);
        talentSage.Add(talentDB[33]);

        //Tier 4
        talentSage.Add(talentDB[12]);
        talentSage.Add(talentDB[15]);
        talentSage.Add(talentDB[56]);

        //Tier 5
        talentSage.Add(talentDB[13]);
        talentSage.Add(talentDB[52]);
        talentSage.Add(talentDB[34]);

        //Tier 6
        talentSage.Add(talentDB[13]);
        talentSage.Add(talentDB[16]);
        talentSage.Add(talentDB[57]);
    }
}
