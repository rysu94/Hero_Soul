using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


/*
Hero Project PlayerStats Class
By: Ryan Su

This scripts stores the player's stats and has functions that affect them


*/

public class PlayerStats : MonoBehaviour
{
    private static PlayerStats playerStats = null;
    public static PlayerStats Instance
    {
        get { return playerStats; }
    }

    public static bool initDone = false;

    //The player's health
    public static int maxHealth;
    public static int health;

    //The player's mana
    public static int maxMana;
    public static int mana;
    bool manaSwitch;

    //The player's stamina
    public static int maxStamina;
    public static int stamina;
    public static int staminaRegenBonus = 0;
    bool stamSwitch = false;

    //Armor
    public static int currentArmor;

    //Shield
    public static int currentShield;

    //The Player's Stats 

    //Strength dictates melee damage
    public static int strength;
    public static int bonusSTR;
    public static int strGrowth;
    public static int strTalent;

    //Agility dictates crit chance & modifier
    public static int dexterity;
    public static bool isCrit = false;
    public static int bonusDEX;
    public static int dexGrowth;
    public static int dexTalent;

    //Dictates spell damage
    public static int intelligence;
    public static int bonusINT;
    public static int intGrowth;
    public static int intTalent;

    //Dictates max mana
    //Every 5 wisdom = another mana globe, capped at 12
    public static int wisdom;
    public static int bonusWIS;
    public static int wisGrowth;
    public static int enwaterBonus;
    public static int wisTalent;

    //Dictates max stamina
    //5 max stamina per point
    public static int endurance;
    public static int bonusEND;
    public static int endGrowth;
    public static int endTalent;

    //Dictates max health
    //5 max health per point
    public static int vitality;
    public static int bonusVIT;
    public static int vitGrowth;
    public static int vitTalent;

    //Player Defense
    public static int defense;
    public static int bonusDEF;
    public static int enstoneBonus;
    public static int defTalent;

    //Player Talent stuff
    public static int leechChance;
    public static int alcLevel;
    public static int armAmount;
    public static int assChance;
    public static int berserkAmount;
    public static int collectLevel;
    public static int deckAdd;
    public static int evaChance;
    public static int precAmount;
    public static int plundChance;
    public static int shieldAmount;
    public static int speedAmount;
    public static int twinChance;
   
    //Player Damage
    public static int playerDamage;

    //Mana Internal Counter
    public static int manaCounter;

    //ManaNoise
    public AudioSource manaNoise;

    //player level
    public static int playerLevel = 25;

    //Player Experience
    public static int playerEXP;
    public GameObject xpBar;
    public static float xpBarWidth = 87;
    public static AudioSource levelUpNoise;

    public GameObject xpBarHUD;
    public static float xpBarHudWidth = 500;
    public Text hudXP;

    //Buff Bonus stats
    public static int defenseBuffBonus = 0;
    public static int strengthBuffBonus = 0;
    public static int enfireBuffBonus = 0;

    public static int dexBuffBonus = 0;
    public static int intelBuffBonus = 0;

    //Player's Break Meter
    public static int breakMeter = 0;
    public static int comboCount = 0;
    public static int breakLevel = 1;


    void Awake()
    {
        playerStats = this;
        
    }

	// Use this for initialization
	void Start ()
    {
        xpBarHUD = GameObject.Find("EXP_Bar");
        if (!initDone)
        {
            //Player Level
            playerLevel = 1;
            LevelCurve.InitLevelCurve();

            //Adjust the XP Bar
            float xpPercentage = (float)playerEXP / (float)LevelCurve.levelCurve[playerLevel - 1];
            var xpBarRect = xpBar.transform as RectTransform;
            xpBarRect.sizeDelta = new Vector2(xpBarWidth * xpPercentage, xpBarRect.sizeDelta.y);

            //Adjust XP HUD Bar
            float xpPercentage2 = (float)playerEXP / (float)LevelCurve.levelCurve[playerLevel - 1];
            var xpBarRect2 = xpBarHUD.transform as RectTransform;
            xpBarRect2.sizeDelta = new Vector2(xpBarHudWidth * xpPercentage2, xpBarRect2.sizeDelta.y);
            //hud xp
            hudXP.text = PlayerStats.playerEXP.ToString() + "/" + LevelCurve.levelCurve[PlayerStats.playerLevel - 1].ToString() + "xp";

            //Player stats
            strength = 18;
            dexterity = 16;
            intelligence = 15;
            vitality = 20;
            endurance = 20;
            wisdom = 15;

            //Stat Growth per level
            strGrowth = 2;
            dexGrowth = 1;
            intGrowth = 1;
            wisGrowth = 1;
            endGrowth = 2;
            vitGrowth = 2;

            //starting stats
            maxHealth = 100;
            health = 100;
            maxMana = 6;
            mana = 3;
            maxStamina = 100;
            stamina = 100;
            initDone = true;
        }
        manaNoise = GameObject.Find("ManaNoise").GetComponent<AudioSource>();
        
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(!stamSwitch && stamina < maxStamina)
        {
            StartCoroutine(RegenStamRountine());
        }

        if(!manaSwitch && mana < maxMana)
        {
            StartCoroutine(RegenManaRoutine());
            if(manaCounter >= 100)
            {
                manaNoise.Play();
                mana++;
                manaCounter = 0;
            }
            
        }

        UpdateStats();
        ReadEquipment();

        //Adjust the XP Bar
        float xpPercentage = (float)playerEXP / (float)LevelCurve.levelCurve[playerLevel - 1];
        //print(playerEXP + "/" + LevelCurve.levelCurve[playerLevel - 1] + "=" + xpPercentage);
        var xpBarRect = xpBar.transform as RectTransform;
        xpBarRect.sizeDelta = new Vector2(xpBarWidth * xpPercentage, xpBarRect.sizeDelta.y);

        //Adjust XP HUD Bar
        float xpPercentage2 = (float)playerEXP / (float)LevelCurve.levelCurve[playerLevel - 1];
        var xpBarRect2 = xpBarHUD.transform as RectTransform;
        xpBarRect2.sizeDelta = new Vector2(xpBarHudWidth * xpPercentage, xpBarRect2.sizeDelta.y);
        //hud xp
        hudXP.text = PlayerStats.playerEXP.ToString() + "/" + LevelCurve.levelCurve[PlayerStats.playerLevel - 1].ToString() + "xp";

        //Check if break level
        CheckBreak();

    }

    public static void ResetStats()
    {
        //Player Level
        playerLevel = 1;

        //Player stats
        strength = 18;
        dexterity = 16;
        intelligence = 15;
        vitality = 20;
        endurance = 20;
        wisdom = 15;

        //Stat Growth per level
        strGrowth = 2;
        dexGrowth = 1;
        intGrowth = 1;
        wisGrowth = 1;
        endGrowth = 2;
        vitGrowth = 2;

        //starting stats
        maxHealth = 100;
        health = 100;
        maxMana = 6;
        mana = 3;
        maxStamina = 100;
        stamina = 100;
    }

    //This function handles casting spells
    public void Cast(int cost)
    {
        mana -= cost;
    }

    //This function handles using stamina
    public static void UseStam(int amount)
    {
        stamina -= amount;
    }

    IEnumerator RegenStamRountine()
    {
        stamSwitch = true;
        stamina ++;
        yield return new WaitForSeconds(.025f);
        stamSwitch = false;
    }

    IEnumerator RegenManaRoutine()
    {
        manaSwitch = true;
        yield return new WaitForSeconds(.05f);
        manaCounter++;
        manaSwitch = false;
    }

    static void UpdateStats()
    {
        maxHealth = 5 * (vitality + bonusVIT + vitTalent);
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        maxMana = (wisdom + bonusWIS + wisTalent) / 5;
        if(maxMana > 12)
        {
            maxMana = 12;
        }
        
        maxStamina = 5 * (endurance + bonusEND + endTalent);
        if(stamina > maxStamina)
        {
            stamina = maxStamina;
        }
    }


    //Update the bonus stats
    public void ReadEquipment()
    {
        bonusSTR = 0;
        bonusDEX = 0;
        bonusEND = 0;
        bonusVIT = 0;
        bonusINT = 0;
        bonusWIS = 0;
        defense = 0;
        
        //Read the armor slots
        for(int i = 0; i < InventoryManager.playerEquipment.Length; i++)
        {
            bonusSTR += InventoryManager.playerEquipment[i].GetSTR();
            bonusDEX += InventoryManager.playerEquipment[i].GetDEX();
            bonusEND += InventoryManager.playerEquipment[i].GetEND();
            bonusVIT += InventoryManager.playerEquipment[i].GetVIT();
            bonusINT += InventoryManager.playerEquipment[i].GetINT();
            bonusWIS += InventoryManager.playerEquipment[i].GetWIS();
            defense += InventoryManager.playerEquipment[i].GetDEF();
        }
    }

    public static void GainXP(int xp)
    {
        //Gain XP
        playerEXP += xp;
        //Check if the player leveled up
        if(playerEXP >= LevelCurve.levelCurve[playerLevel-1])
        {
            //reset player XP
            playerEXP -= LevelCurve.levelCurve[playerLevel - 1];
            //gain a level
            playerLevel++;
            levelUpNoise = GameObject.Find("LevelUp").GetComponent<AudioSource>();
            levelUpNoise.Play();

            //Gain stats
            strength += strGrowth;
            dexterity += dexGrowth;
            vitality += vitGrowth;
            endurance += endGrowth;
            wisdom += wisGrowth;
            intelligence += intGrowth;

            //Update stats
            UpdateStats();

            //Heal Health
            health = maxHealth;
            mana = maxMana;
            stamina = maxStamina;

            //Create Level Text
            GameObject tempObj = Instantiate(Resources.Load("Prefabs/BattleText")) as GameObject;
            tempObj.transform.SetParent(GameObject.Find("HUD").transform, false);
            RectTransform tempRect = tempObj.GetComponent<RectTransform>();
            tempObj.GetComponent<Text>().text = "Level Up!";
            tempObj.GetComponent<BattleText>().timer = -16;
            tempRect.position = new Vector2(TestCharController.player.transform.position.x, TestCharController.player.transform.position.y);
        }

    }

    //Caps the break meter and checks if break has been enabled
    public void CheckBreak()
    {
        if(breakLevel == 1 && breakMeter > 100)
        {
            breakMeter = 100;
        }
        else if(breakLevel == 2 && breakMeter > 200)
        {
            breakMeter = 200;
        }
        else if(breakLevel == 3 && breakMeter > 300)
        {
            breakMeter = 300;
        }
        else if(breakLevel == 0)
        {
            breakMeter = 0;
        }
    }









}
