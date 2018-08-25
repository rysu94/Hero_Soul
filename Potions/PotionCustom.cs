using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PotionCustom : MonoBehaviour
{
    public Image healthPotion;
    public Text slot1;
    public Image manaPotion;
    public Text slot2;
    public Image staminaPotion;
    public Text slot3;
    public Image stoneskinPotion;
    public Text slot4;
    public Image strengthPotion;
    public Text slot5;
    public Image alacrityPotion;
    public Text slot6;
    public Image intellectPotion;
    public Text slot7;

    public Image slot1IMG;
    public Text slot1Quant;
    public Image slot2IMG;
    public Text slot2Quant;


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateList();
        UpdateActive();    
    }

    void UpdateActive()
    {
        switch (PotionController.slot1Active)
        {
            default:
                break;
            case 1:
                slot1IMG.sprite = Resources.Load<Sprite>("Potion/Heal_Potion");
                slot1Quant.text = PotionController.healthPotionCurrent + "/" + PotionController.healthPotionMax;
                break;
            case 2:
                slot1IMG.sprite = Resources.Load<Sprite>("Potion/Mana_Potion");
                slot1Quant.text = PotionController.manaPotionCurrent + "/" + PotionController.manaPotionMax;
                break;
            case 3:
                slot1IMG.sprite = Resources.Load<Sprite>("Potion/Stamina_Potion");
                slot1Quant.text = PotionController.staminaPotionCurrent + "/" + PotionController.staminaPotionMax;
                break;
            case 4:
                slot1IMG.sprite = Resources.Load<Sprite>("Potion/StoneskinPotion");
                slot1Quant.text = PotionController.stonePotionCurrent + "/" + PotionController.stonePotionMax;
                break;
            case 5:
                slot1IMG.sprite = Resources.Load<Sprite>("Potion/strPotion_Potion");
                slot1Quant.text = PotionController.strengthPotionCurrent + "/" + PotionController.strengthPotionMax;
                break;
            case 6:
                slot1IMG.sprite = Resources.Load<Sprite>("Potion/AlacrityPotion");
                slot1Quant.text = PotionController.alacrityPotionCurrent + "/" + PotionController.alacrityPotionMax;
                break;
            case 7:
                slot1IMG.sprite = Resources.Load<Sprite>("Potion/intPotion");
                slot1Quant.text = PotionController.intellectPotionCurrent + "/" + PotionController.intellectPotionMax;
                break;
        }

        switch (PotionController.slot2Active)
        {
            default:
                break;
            case 1:
                slot2IMG.sprite = Resources.Load<Sprite>("Potion/Heal_Potion");
                slot2Quant.text = PotionController.healthPotionCurrent + "/" + PotionController.healthPotionMax;
                break;
            case 2:
                slot2IMG.sprite = Resources.Load<Sprite>("Potion/Mana_Potion");
                slot2Quant.text = PotionController.manaPotionCurrent + "/" + PotionController.manaPotionMax;
                break;
            case 3:
                slot2IMG.sprite = Resources.Load<Sprite>("Potion/Stamina_Potion");
                slot2Quant.text = PotionController.staminaPotionCurrent + "/" + PotionController.staminaPotionMax;
                break;
            case 4:
                slot2IMG.sprite = Resources.Load<Sprite>("Potion/StoneskinPotion");
                slot2Quant.text = PotionController.stonePotionCurrent + "/" + PotionController.stonePotionMax;
                break;
            case 5:
                slot2IMG.sprite = Resources.Load<Sprite>("Potion/strPotion_Potion");
                slot2Quant.text = PotionController.strengthPotionCurrent + "/" + PotionController.strengthPotionMax;
                break;
            case 6:
                slot2IMG.sprite = Resources.Load<Sprite>("Potion/AlacrityPotion");
                slot2Quant.text = PotionController.alacrityPotionCurrent + "/" + PotionController.alacrityPotionMax;
                break;
            case 7:
                slot2IMG.sprite = Resources.Load<Sprite>("Potion/intPotion");
                slot2Quant.text = PotionController.intellectPotionCurrent + "/" + PotionController.intellectPotionMax;
                break;
        }
    }

    void UpdateList()
    {
        if(PotionController.healthPotionMax <= 0)
        {
            healthPotion.gameObject.SetActive(false);
        }
        else if(PotionController.healthPotionMax > 0)
        {
            healthPotion.gameObject.SetActive(true);
            slot1.text = PotionController.healthPotionCurrent + "/" + PotionController.healthPotionMax;
        }

        if(PotionController.manaPotionMax <= 0)
        {
            manaPotion.gameObject.SetActive(false);
        }
        else if(PotionController.manaPotionMax > 0)
        {
            manaPotion.gameObject.SetActive(true);
            slot2.text = PotionController.manaPotionCurrent + "/" + PotionController.manaPotionMax;
        }

        if(PotionController.staminaPotionMax <= 0)
        {
            staminaPotion.gameObject.SetActive(false);
        }
        else if(PotionController.staminaPotionMax > 0)
        {
            staminaPotion.gameObject.SetActive(true);
            slot3.text = PotionController.staminaPotionCurrent + "/" + PotionController.staminaPotionMax;
        }

        if (PotionController.stonePotionMax <= 0)
        {
            stoneskinPotion.gameObject.SetActive(false);
        }
        else if (PotionController.stonePotionMax > 0)
        {
            stoneskinPotion.gameObject.SetActive(true);
            slot4.text = PotionController.stonePotionCurrent + "/" + PotionController.stonePotionMax;
        }

        if (PotionController.strengthPotionMax <= 0)
        {
            strengthPotion.gameObject.SetActive(false);
        }
        else if (PotionController.strengthPotionMax > 0)
        {
            strengthPotion.gameObject.SetActive(true);
            slot5.text = PotionController.strengthPotionCurrent + "/" + PotionController.strengthPotionMax;
        }

        if (PotionController.alacrityPotionMax <= 0)
        {
            alacrityPotion.gameObject.SetActive(false);
        }
        else if (PotionController.alacrityPotionMax > 0)
        {
            alacrityPotion.gameObject.SetActive(true);
            slot6.text = PotionController.alacrityPotionCurrent + "/" + PotionController.alacrityPotionMax;
        }

        if (PotionController.intellectPotionMax <= 0)
        {
            intellectPotion.gameObject.SetActive(false);
        }
        else if (PotionController.intellectPotionMax > 0)
        {
            intellectPotion.gameObject.SetActive(true);
            slot7.text = PotionController.intellectPotionCurrent + "/" + PotionController.intellectPotionMax;
        }
    }
}
