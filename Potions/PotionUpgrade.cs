﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PotionUpgrade : MonoBehaviour
{
    public int upgradeID;

    public GameObject potionCostPanel;
    public GameObject costPrefab;

    public string tooltip;
    public Text tooltipText;

    public bool unlocked = false;

    void Update()
    {
        //UpdateCosts();
    }

    public void UpdateCosts()
    {
        //update the tooltip
        switch (upgradeID)
        {
            default:
                tooltip = "Error";
                break;
            //Heal Potency
            case 1:
                tooltip = "Increase healing potion potency to <color=yellow>" + (PotionController.healthPotionAmount + 50) + "</color>";
                if(PotionController.healthPotionAmount > 300)
                {
                    tooltip = "Max Level";
                }
                GenerateMaterials(upgradeID, PotionController.healthPotionAmount);
                break;
            //heal quantity
            case 2:
                tooltip = "Increase healing potion capacity to <color=yellow>" + (PotionController.healthPotionMax + 1) + "</color>";
                if(PotionController.healthPotionMax > 10)
                {
                    tooltip = "Max Level";
                }
                GenerateMaterials(upgradeID, PotionController.healthPotionMax);
                break;
            //mana potency
            case 3:
                tooltip = "Increase mana potion potency to " + (PotionController.manaPotionAmount + 1);
                if(PotionController.manaPotionAmount > 12)
                {
                    tooltip = "Max Level";
                }
                break;
            //mana quantity
            case 4:
                tooltip = "Increase mana potion capacity to " + (PotionController.manaPotionMax + 1);
                if(PotionController.manaPotionMax > 10)
                {
                    tooltip = "Max Level";
                }
                break;
            //stamina potency
            case 5:
                tooltip = "Increase stamina potion potency to " + (PotionController.staminaPotionAmount + 50);
                if(PotionController.staminaPotionAmount > 300)
                {
                    tooltip = "Max Level";
                }
                break;
            //stamina quantity
            case 6:
                tooltip = "Increase stamina potion quantity to " + (PotionController.staminaPotionMax + 1);
                if(PotionController.staminaPotionMax > 10)
                {
                    tooltip = "Max Level";
                }
                break;
            //stoneskin potency
            case 7:
                tooltip = "Increase stoneskin potion potency to " + ((PotionController.stonePotionAmount * 10) + 10);
                if(PotionController.stonePotionAmount > 50)
                {
                    tooltip = "Max Level";
                }
                break;
            //stoneskin quantity
            case 8:
                tooltip = "Increase stoneskin potion capacity to " + (PotionController.stonePotionMax + 1);
                if(PotionController.stonePotionMax > 5)
                {
                    tooltip = "Max Level";
                }
                break;
            //strength potency
            case 9:
                tooltip = "Increase strength potion potency to " + ((PotionController.strengthPotionAmount * 10) + 10);
                if(PotionController.strengthPotionAmount > 50)
                {
                    tooltip = "Max Level";
                }
                break;
            //strength quantity
            case 10:
                tooltip = "Increase strength potion capacity to " + (PotionController.strengthPotionMax + 1);
                if(PotionController.strengthPotionMax > 5)
                {
                    tooltip = "Max Level";
                }
                break;
            //alacrity potency
            case 11:
                tooltip = "Increase alacrity potion potency to " + ((PotionController.alacrityPotionAmount * 5) + 5);
                if(PotionController.alacrityPotionAmount > 25)
                {
                    tooltip = "Max Level";
                }
                break;
            //alacrity quantity
            case 12:
                tooltip = "Increase alacrity potion capacity to " + (PotionController.alacrityPotionMax + 1);
                if(PotionController.alacrityPotionMax > 5)
                {
                    tooltip = "Max Level";
                }
                break;
            //intellect potency
            case 13:
                tooltip = "Increase intellect potion potency to " + ((PotionController.intellectPotionAmount * 10) + 10);
                if(PotionController.intellectPotionAmount > 50)
                {
                    tooltip = "Max Level";
                }
                break;
            //intellect quantity
            case 14:
                tooltip = "Increase intellect potion capacity to " + (PotionController.intellectPotionMax + 1);
                if(PotionController.intellectPotionMax > 5)
                {
                    tooltip = "Max Level";
                }
                break;

        }
    }

    void GenerateMaterials(int id, int amount)
    {  
        int[] mat = new int[8];
        switch (id)
        {
            default:
                break;
            case 1:
                int level = amount / 50;
                mat = PotionDatabase.healthPot[level - 1];  
                break;
            case 2:
                mat = PotionDatabase.healthQuant[amount - 3];
                break;
                
        }

        foreach (GameObject material in GameObject.FindGameObjectsWithTag("Alc_Mat"))
        {
            Destroy(material);
        }

        for (int i = 0; i < mat.Length; i+=2)
        {
            GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Inventory/PotionMaterial"), potionCostPanel.transform);
            tempObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + potionCostPanel.GetComponent<ItemDatabase>().FindItem(mat[i]).itemIconName);
            if(mat[i + 1] > 0)
            {
                tempObj.transform.Find("QuantText").GetComponent<Text>().text = mat[i + 1].ToString();
            }
            else
            {
                tempObj.transform.Find("QuantText").GetComponent<Text>().text = "";
            }
            
        }
    }


}
