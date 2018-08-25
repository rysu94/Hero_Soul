using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/*
Hero Project Damage Manager
By: Ryan Su

This caculates the damage players deal


*/

public class DamageManager : MonoBehaviour
{
    public bool isPhysical;
    public bool isSpell;
    public bool isPlayer;

    public static int spellBase;

    public static int totalHits;

    void Awake()
    {

    }

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public static void CreateText(string text, GameObject enemy)
    {
        GameObject tempObj = Instantiate(Resources.Load("Prefabs/PlayerBattleText")) as GameObject;
        tempObj.transform.SetParent(GameObject.Find("HUD").transform, false);
        RectTransform tempRect = tempObj.GetComponent<RectTransform>();
        tempRect.position = new Vector2(enemy.transform.position.x, enemy.transform.position.y);
        tempObj.GetComponent<Text>().text = text;
        tempObj.GetComponent<Text>().color = Color.white;
    }

    public static void PlayerDamage(int damage, GameObject player, bool pure)
    {
        //Reset the combo counter
        GameObject.Find("ComboController").GetComponent<ComboCounter>().comboTime = 0;

        if ((!TestCharController.invuln && !TestCharController.breakInvuln) || pure)
        {
            int adjustedDamage = 0;
            //Check if the damage is pure
            if (!pure)
            {
                //adds variance based on level
                int randomDamage = damage + Random.Range(-PlayerStats.playerLevel, PlayerStats.playerLevel);
                //Get adjusted damage formula
                adjustedDamage = (randomDamage - (PlayerStats.defense + PlayerStats.defenseBuffBonus + PlayerStats.enstoneBonus + PlayerStats.defTalent));
                if (adjustedDamage < 0)
                {
                    adjustedDamage = 0;
                }
            }
            else
            {
                adjustedDamage = damage;
            }


            GameObject tempObj = Instantiate(Resources.Load("Prefabs/PlayerBattleText")) as GameObject;
            tempObj.transform.SetParent(GameObject.Find("HUD").transform, false);
            RectTransform tempRect = tempObj.GetComponent<RectTransform>();

            tempRect.position = new Vector2(player.transform.position.x, player.transform.position.y);

            if (adjustedDamage > 0)
            {
                tempObj.GetComponent<Text>().text = ((int)adjustedDamage).ToString();
            }
            else
            {
                tempObj.GetComponent<Text>().text = "Resist";
                tempObj.GetComponent<Text>().color = Color.white;
            }

            int evadeChance = Random.Range(0, 100);
            if (evadeChance < PlayerStats.evaChance && !pure)
            {
                tempObj.GetComponent<Text>().text = "Miss!";
                tempObj.GetComponent<Text>().color = Color.white;
                adjustedDamage = 0;
            }

            //Check if there is a shield
            if(PlayerStats.currentShield > 0 && !pure)
            {
                PlayerStats.currentShield -= adjustedDamage;
            }

            //Check if there is armor
            else if(PlayerStats.currentArmor > 0 && !pure)
            {
                PlayerStats.currentArmor -= adjustedDamage;
            }
            else
            {
                PlayerStats.health -= adjustedDamage;
                TestCharController.playerAttack.clip = TestCharController.hurtNoise[Random.Range(0, TestCharController.hurtNoise.Length)];
                TestCharController.playerAttack.Play();
            }

            if (!pure && adjustedDamage > 0)
            {
                TestCharController.invuln = true;
                //Add break meter
                PlayerStats.breakMeter += 5;
            }
            
        }
    }

    public static int WeaponDamage(GameObject enemy)
    {
        //Calculates the Weapon Damage
        float playerDamage = 0;
        float critChance = Random.Range(1, 101);
        float critMultiplier = 1;
        float chargeMultiplier = 1;

        //Check if it's a crit
        if (critChance < ((PlayerStats.dexterity + PlayerStats.bonusDEX + PlayerStats.dexTalent)/ 2))
        {
            critMultiplier = 2 + (PlayerStats.precAmount/100f);
        }

        //Get the Player Weapon
        int weaponDamage = Random.Range(1, 4);
        if (InventoryManager.playerEquipment[0].itemID != 0)
        {
            weaponDamage = Random.Range(InventoryManager.playerEquipment[0].GetMinDMG(), InventoryManager.playerEquipment[0].GetMaxDMG() + 1);
        }
        else
        {
            weaponDamage = 1;
        }

        

        //check if it's a heavy attack
        if (TestCharController.isHeavy)
        {
            chargeMultiplier += TestCharController.chargeMult;
        }

        playerDamage = (((weaponDamage * chargeMultiplier) * Random.Range(1, 1.5f))) * (1 + ((float)(PlayerStats.strength + PlayerStats.bonusSTR + 
            PlayerStats.strengthBuffBonus + PlayerStats.enfireBuffBonus + PlayerStats.strTalent)/25f)) * critMultiplier;

        //Break Modifier (Harrier)
        if (TestCharController.harrierBreak)
        {
            //playerDamage = playerDamage;
        }

        //Create Battle Text
        GameObject tempObj = Instantiate(Resources.Load("Prefabs/BattleText")) as GameObject;
        tempObj.transform.SetParent(GameObject.Find("HUD").transform, false);
        RectTransform tempRect = tempObj.GetComponent<RectTransform>();

        tempRect.position= new Vector2(enemy.transform.position.x, enemy.transform.position.y);

        if (critMultiplier == 1)
        {   
            tempObj.GetComponent<Text>().text = ((int)playerDamage).ToString();


            //Check assassin
            int assassinChance = Random.Range(0, 100);
            if(assassinChance < PlayerStats.assChance && !enemy.GetComponent<Monster>().boss)
            {
                tempObj.GetComponent<Text>().text = "<color=red>9999</color>";
                playerDamage = 9999;
                Instantiate(Resources.Load("Prefabs/Soul/Assassinate"), enemy.transform);
            }

            //Berserk?
            if (PlayerStats.health <= (PlayerStats.maxHealth * 0.25) && PlayerStats.berserkAmount > 0)
            {
                playerDamage *= (PlayerStats.berserkAmount / 100f);
                tempObj.GetComponent<Text>().text = "<color=red>" + ((int)playerDamage).ToString() + "</color>";
            }

        }
        else if(critMultiplier > 1)
        {
            tempObj.GetComponent<Text>().text = ((int)playerDamage).ToString() + "!";
            tempObj.GetComponent<Text>().fontSize = 32;

            //Check assassin
            int assassinChance = Random.Range(0, 100);
            if (assassinChance < PlayerStats.assChance && !enemy.GetComponent<Monster>().boss)
            {
                tempObj.GetComponent<Text>().text = "<color=red>9999</color>";
                playerDamage = 9999;
            }

            //Berserk?
            if (PlayerStats.health <= (PlayerStats.maxHealth * 0.25) && PlayerStats.berserkAmount > 0)
            {
                playerDamage *= (PlayerStats.berserkAmount / 100f);
                tempObj.GetComponent<Text>().text = "<color=red>" + ((int)playerDamage).ToString() + "!</color>";
            }

        }

        //If the damage is resisted
        if(enemy.GetComponent<Monster>().physicalResist)
        {
            playerDamage = 0;
            tempObj.GetComponent<Text>().text = "Resist";
        }

        //If the damage is resisted
        if (enemy.GetComponent<Monster>().invulnerable)
        {
            playerDamage = 0;
            tempObj.GetComponent<Text>().text = "Invuln";
        }

        //combo counter
        if (playerDamage > 0)
        {
            GameObject.Find("ComboController").GetComponent<ComboCounter>().AddCombo();
        }

        //Leech Chance
        int leechChance = Random.Range(0, 100);
        if(leechChance < (PlayerStats.leechChance * 10))
        {
            PlayerStats.health += 10;
            tempObj.transform.Find("SubText").GetComponent<Text>().text += "\n<color=lime>Leech +10</color>";
            if (PlayerStats.health > PlayerStats.maxHealth)
            {
                PlayerStats.health = PlayerStats.maxHealth;
            }
        }

        //Plunder Chance
        if (enemy.GetComponent<Monster>().monsterHealth - playerDamage <= 0)
        {
            int plunderChance = Random.Range(0, 100);
            if (plunderChance < PlayerStats.plundChance * 10)
            {
                int gold = Random.Range(15, 50);
                InventoryManager.playerGold += gold;
                tempObj.transform.Find("SubText").GetComponent<Text>().text += "\n<color=yellow>Plunder +" + gold + "</color>";
            }
        }

        //Twin Chance
        int twinChance = Random.Range(0, 100);
        if(twinChance < (PlayerStats.twinChance))
        {
            int twinDamage = (int)(playerDamage / 2);
            tempObj.transform.Find("SubText").GetComponent<Text>().text += "\n<color=white>Twin " + twinDamage + "</color>";
            playerDamage += twinDamage;
        }

        return (int)playerDamage;
    }

    public static int MagicDamage(GameObject enemy, int spell)
    {
        //Calculates the Spell Damage
        float playerDamage = 0;

        playerDamage = (spell * (1 + (PlayerStats.intelligence + PlayerStats.bonusINT + PlayerStats.intelBuffBonus + PlayerStats.intTalent)/50f)) * Random.Range(0.8f, 1.2f);

        bool textFound = false;
        //Check if battle text exists
        foreach (GameObject text in GameObject.FindGameObjectsWithTag("Battle_Text"))
        {
            if (enemy.GetComponent<Monster>().invulnerable)
            {
                playerDamage = 0;
                text.GetComponent<Text>().text = "Invuln";
            }
            else if (text.GetComponent<BattleText>().enemy == enemy)
            {
                if(text.GetComponent<RectTransform>().localScale.x < 2)
                {
                    text.GetComponent<RectTransform>().localScale = new Vector3(text.GetComponent<RectTransform>().localScale.x + .1f, text.GetComponent<RectTransform>().localScale.y + .1f, 1);
                }
                text.GetComponent<Text>().text = (text.GetComponent<BattleText>().damage + (int)playerDamage).ToString();
                text.GetComponent<BattleText>().damage += (int)playerDamage;
                textFound = true;
            }
        }

        if(!textFound)
        {
            GameObject tempObj = Instantiate(Resources.Load("Prefabs/SpellText")) as GameObject;
            tempObj.transform.SetParent(GameObject.Find("HUD").transform, false);
            tempObj.GetComponent<BattleText>().enemy = enemy;

            if (enemy.GetComponent<Monster>().invulnerable)
            {
                playerDamage = 0;
                tempObj.GetComponent<Text>().text = "Invuln";
            }
            else
            {
                //print((int)playerDamage);
                tempObj.GetComponent<BattleText>().damage += (int)playerDamage;
                tempObj.GetComponent<Text>().text = ((int)playerDamage).ToString();
            }

            RectTransform tempRect = tempObj.GetComponent<RectTransform>();
            tempRect.position = new Vector2(enemy.transform.position.x, enemy.transform.position.y);
        }        
        return (int)playerDamage;
    }

}
