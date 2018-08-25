using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ExitToTitle : MonoBehaviour
{

    public Button returnButton;

	// Use this for initialization
	void Start ()
    {
        returnButton.onClick.AddListener(ReturntoTitle);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //For the Demo~
    public void ReturntoTitle()
    {
        //Clear the inventory
        for(int i = 0; i < InventoryManager.playerInventory.Length; i++)
        {
            InventoryManager.playerInventory[i] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
        }
        //Set the the starting Inv
        for(int i = 0; i < InventoryManager.playerEquipment.Length; i++)
        {
            InventoryManager.playerEquipment[i] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[0];
        }
        InventoryManager.playerEquipment[0] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[38];
        InventoryManager.playerEquipment[3] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[9];
        InventoryManager.playerEquipment[5] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[10];

        Soul_Manager.unlockedHeros.Clear();
        for(int i = 0; i < Soul_Manager.playerSelectedSkills.Length; i++)
        {
            Soul_Manager.playerSelectedSkills[i] = -1;
        }
        Soul_Manager.playerTalentTree.Clear();
        Soul_Manager.currentTier = 0;

        PlayerStats.ResetStats();
        PotionController.healthPotionCurrent = PotionController.healthPotionMax;
        PotionController.manaPotionCurrent = PotionController.manaPotionMax;

        InventoryManager.playerGold = 3000;
        PlayerStats.playerEXP = 0;

        Destroy(GameObject.Find("BGM"));
        //Go to Title screen
        SceneManager.LoadScene("Title Screen");
    }
}
