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
                adjustedDamage = (randomDamage - (PlayerStats.defense + PlayerStats.defenseBuffBonus));
                if (adjustedDamage < 0)
                {
                    adjustedDamage = 0;
                }
            }
            else
            {
                adjustedDamage = damage;
            }

            PlayerStats.health -= adjustedDamage;
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
        if (critChance < ((PlayerStats.dexterity + PlayerStats.bonusDEX + PlayerStats.defenseBuffBonus)/ 2))
        {
            critMultiplier = 2;
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

        playerDamage = (((weaponDamage * chargeMultiplier) * Random.Range(1, 1.5f))) * (1 + ((float)(PlayerStats.strength + PlayerStats.bonusSTR + PlayerStats.strengthBuffBonus)/50f)) * critMultiplier;

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

        }
        else if(critMultiplier == 2)
        {
            tempObj.GetComponent<Text>().text = ((int)playerDamage).ToString() + "!";
            tempObj.GetComponent<Text>().fontSize = 32;
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



        return (int)playerDamage;
    }

    public static int MagicDamage(float offset, GameObject enemy)
    {
        //Calculates the Spell Damage
        float playerDamage = 0;

        playerDamage = (spellBase + (PlayerStats.intelligence + PlayerStats.bonusINT + PlayerStats.intelBuffBonus) * 3) * Random.Range(0.8f, 1.2f);

        GameObject tempObj = Instantiate(Resources.Load("Prefabs/SpellText")) as GameObject;
        tempObj.transform.SetParent(GameObject.Find("HUD").transform, false);
        tempObj.GetComponent<Text>().text = ((int)playerDamage).ToString();
        RectTransform tempRect = tempObj.GetComponent<RectTransform>();

        tempRect.position = new Vector2(enemy.transform.position.x + offset, enemy.transform.position.y);
        
        return (int)playerDamage;
    }

}
