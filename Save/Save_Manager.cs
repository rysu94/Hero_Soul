using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Save_Manager : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void AutoSave()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/autosave_data.dat", FileMode.Open);

        GameData data = new GameData();

        SaveStats(ref data);
        SaveInventory(ref data);
        SaveEquipment(ref data);
        SaveArcanaDeck(ref data);
        SaveTalentTree(ref data);
        SaveSpellbook(ref data);

        bf.Serialize(file, data);
        file.Close();
    }

    public void AutoLoad()
    {
        if (File.Exists(Application.persistentDataPath + "/autosave_data.dat"))
        {
            //Open the autosave file
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/autosave_data.dat", FileMode.Open);
            GameData data = (GameData)bf.Deserialize(file);
            file.Close();
        }
    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/save_data.dat", FileMode.Open);
    }

    public void SavePlaytime(ref GameData data)
    {
        data.playtime += Time.time;
    }

    public void SaveStats(ref GameData data)
    {
        data.strength = PlayerStats.strength;
        data.strGrowth = PlayerStats.strGrowth;

        data.dexterity = PlayerStats.dexterity;
        data.dexGrowth = PlayerStats.dexGrowth;

        data.vitality = PlayerStats.vitality;
        data.vitGrowth = PlayerStats.vitGrowth;

        data.endurance = PlayerStats.endurance;
        data.endGrowth = PlayerStats.endGrowth;

        data.wisdom = PlayerStats.wisdom;
        data.wisdom = PlayerStats.wisGrowth;

        data.intelligence = PlayerStats.intelligence;
        data.intGrowth = PlayerStats.intGrowth;

        data.playerLevel = PlayerStats.playerLevel;
        data.playerExp = PlayerStats.playerEXP;

        data.breakMeter = PlayerStats.breakMeter;
        data.breakLevel = PlayerStats.breakLevel;
    }

    public void SaveInventory(ref GameData data)
    {
        data.playerInventory = InventoryManager.playerInventory;
        data.playerStash = InventoryManager.playerStash;
        data.playerGold = InventoryManager.playerGold;
    }

    public void SaveEquipment(ref GameData data)
    {
        data.playerEquipment = InventoryManager.playerEquipment;
    }

    public void SaveArcanaDeck(ref GameData data)
    {
        data.fireArcana = InventoryManager.fireArcana;
        data.earthArcana = InventoryManager.earthArcana;
        data.lifeArcana = InventoryManager.lifeArcana;
        data.waterArcana = InventoryManager.waterArcana;
        data.windArcana = InventoryManager.windArcana;
        data.deckSize = Deck.deckSize;
        data.playerDeck = Deck.playerDeck;
    }

    public void SaveTalentTree(ref GameData data)
    {
        data.playerTalentTree = Soul_Manager.playerTalentTree;
        data.playerSelectedSkill = Soul_Manager.playerSelectedSkills;
    }

    public void SaveSpellbook(ref GameData data)
    {
        data.playerSpellbook = InventoryManager.playerSpellbook;
    }
}
