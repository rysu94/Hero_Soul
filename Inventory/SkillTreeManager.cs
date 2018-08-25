using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{

    public Text charHP;
    public Text charMP;
    public Text charSTR;
    public Text charEND;
    public Text charStam;
    public Text charINT;
    public Text charDEX;
    public Text charWIS;
    public Text charVIT;
    public Text charDEF;
    public Text playerLevel;
    public Text playerXP;

    public Text critPerc;
    public Text bonusHP;
    public Text bonusEND;
    public Text bonusDMG;


	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        charHP.text = PlayerStats.health.ToString() + "/" + PlayerStats.maxHealth.ToString();
        charMP.text = PlayerStats.mana.ToString() + "/" + PlayerStats.maxMana.ToString();
        charStam.text = PlayerStats.stamina.ToString() + "/" + PlayerStats.maxStamina.ToString();

        charSTR.text = PlayerStats.strength.ToString();
        if((PlayerStats.bonusSTR + PlayerStats.strengthBuffBonus + PlayerStats.enfireBuffBonus + PlayerStats.strTalent) > 0)
        {
            charSTR.text = PlayerStats.strength.ToString() + "<color=#00ff00ff> + " + (PlayerStats.bonusSTR + PlayerStats.strengthBuffBonus + PlayerStats.enfireBuffBonus + PlayerStats.strTalent) + "</color>";
        }

        float bonusDamage = ((PlayerStats.strength + PlayerStats.bonusSTR + PlayerStats.strengthBuffBonus + PlayerStats.enfireBuffBonus + PlayerStats.strTalent) / 25f) * InventoryManager.playerEquipment[0].GetMinDMG();
        bonusDMG.text = "(" + Mathf.Floor(bonusDamage).ToString() + ")";

        charEND.text = PlayerStats.endurance.ToString();
        if((PlayerStats.bonusEND + PlayerStats.endTalent) > 0)
        {
            charEND.text = PlayerStats.endurance.ToString() + "<color=#00ff00ff> + " + (PlayerStats.bonusEND + PlayerStats.endTalent) + "</color>";
        }

        float bonusEndurance = ((PlayerStats.bonusEND + PlayerStats.endTalent) * 5);
        bonusEND.text = "(" + bonusEndurance.ToString() + ")";

        charINT.text = PlayerStats.intelligence.ToString();
        if ((PlayerStats.bonusINT + PlayerStats.intelBuffBonus + PlayerStats.intTalent) > 0)
        {
            charINT.text = PlayerStats.intelligence.ToString() + "<color=#00ff00ff> + " + (PlayerStats.bonusINT + PlayerStats.intelBuffBonus + PlayerStats.intTalent) + "</color>";
        }

        charWIS.text = PlayerStats.wisdom.ToString();
        if ((PlayerStats.bonusWIS + PlayerStats.wisTalent) > 0)
        {
            charWIS.text = PlayerStats.wisdom.ToString() + "<color=#00ff00ff> + " + (PlayerStats.bonusWIS + PlayerStats.wisTalent) + "</color>";
        }

        charDEX.text = PlayerStats.dexterity.ToString();
        if ((PlayerStats.bonusDEX + PlayerStats.dexBuffBonus + PlayerStats.dexTalent) > 0)
        { 
            charDEX.text = PlayerStats.dexterity.ToString() + "<color=#00ff00ff> + " + (PlayerStats.bonusDEX + PlayerStats.dexBuffBonus + PlayerStats.dexTalent) + "</color>";
        }

        float critChance = (PlayerStats.dexterity + PlayerStats.bonusDEX + PlayerStats.dexTalent) / 2;
        critPerc.text = "(" + critChance.ToString() + "%)";

        charVIT.text = PlayerStats.vitality.ToString();
        if ((PlayerStats.bonusVIT + PlayerStats.vitTalent) > 0)
        {
            charVIT.text = PlayerStats.vitality.ToString() + "<color=#00ff00ff> + " + (PlayerStats.bonusVIT + PlayerStats.vitTalent) + "</color>";
        }

        float bonusHealth = ((PlayerStats.bonusVIT + PlayerStats.vitTalent) * 5);
        bonusHP.text = "(" + bonusHealth.ToString() + ")";

        charDEF.text = PlayerStats.defense.ToString();
        //player defense
        if ((PlayerStats.defenseBuffBonus + PlayerStats.enstoneBonus + PlayerStats.defTalent) > 0)
        {
            charDEF.text = PlayerStats.defense.ToString() + "<color=#00ff00ff> + " + (PlayerStats.defenseBuffBonus + PlayerStats.enstoneBonus + PlayerStats.defTalent) + "</color>";
        }
        

        //player level
        playerLevel.text = "Level: " + PlayerStats.playerLevel;

        //player XP
        playerXP.text = PlayerStats.playerEXP.ToString() + "/" + LevelCurve.levelCurve[PlayerStats.playerLevel - 1].ToString() + "xp";


    }
}
