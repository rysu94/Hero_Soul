using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PotionCustom : MonoBehaviour
{
    public Image healthPotion;
    public Image manaPotion;
    public Image staminaPotion;
    public Image stoneskinPotion;
    public Image strengthPotion;
    public Image alacrityPotion;
    public Image intellectPotion;


    public Image slot1IMG;
    public Image slot2IMG;


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
                break;
            case 2:
                slot1IMG.sprite = Resources.Load<Sprite>("Potion/Mana_Potion");
                break;
            case 3:
                slot1IMG.sprite = Resources.Load<Sprite>("Potion/Stamina_Potion");
                break;
            case 4:
                slot1IMG.sprite = Resources.Load<Sprite>("Potion/StoneskinPotion");
                break;
            case 5:
                slot1IMG.sprite = Resources.Load<Sprite>("Potion/strPotion_Potion");
                break;
            case 6:
                slot1IMG.sprite = Resources.Load<Sprite>("Potion/AlacrityPotion");
                break;
            case 7:
                slot1IMG.sprite = Resources.Load<Sprite>("Potion/intPotion");
                break;
        }

        switch (PotionController.slot2Active)
        {
            default:
                break;
            case 1:
                slot2IMG.sprite = Resources.Load<Sprite>("Potion/Heal_Potion");
                break;
            case 2:
                slot2IMG.sprite = Resources.Load<Sprite>("Potion/Mana_Potion");
                break;
            case 3:
                slot2IMG.sprite = Resources.Load<Sprite>("Potion/Stamina_Potion");
                break;
            case 4:
                slot2IMG.sprite = Resources.Load<Sprite>("Potion/StoneskinPotion");
                break;
            case 5:
                slot2IMG.sprite = Resources.Load<Sprite>("Potion/strPotion_Potion");
                break;
            case 6:
                slot2IMG.sprite = Resources.Load<Sprite>("Potion/AlacrityPotion");
                break;
            case 7:
                slot2IMG.sprite = Resources.Load<Sprite>("Potion/intPotion");
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
        }

        if(PotionController.manaPotionMax <= 0)
        {
            manaPotion.gameObject.SetActive(false);
        }
        else if(PotionController.manaPotionMax > 0)
        {
            manaPotion.gameObject.SetActive(true);
        }

        if(PotionController.staminaPotionMax <= 0)
        {
            staminaPotion.gameObject.SetActive(false);
        }
        else if(PotionController.staminaPotionMax > 0)
        {
            staminaPotion.gameObject.SetActive(true);
        }

        if (PotionController.stonePotionMax <= 0)
        {
            stoneskinPotion.gameObject.SetActive(false);
        }
        else if (PotionController.stonePotionMax > 0)
        {
            stoneskinPotion.gameObject.SetActive(true);
        }

        if (PotionController.strengthPotionMax <= 0)
        {
            strengthPotion.gameObject.SetActive(false);
        }
        else if (PotionController.strengthPotionMax > 0)
        {
            strengthPotion.gameObject.SetActive(true);
        }

        if (PotionController.alacrityPotionMax <= 0)
        {
            alacrityPotion.gameObject.SetActive(false);
        }
        else if (PotionController.alacrityPotionMax > 0)
        {
            alacrityPotion.gameObject.SetActive(true);
        }

        if (PotionController.intellectPotionMax <= 0)
        {
            intellectPotion.gameObject.SetActive(false);
        }
        else if (PotionController.staminaPotionMax > 0)
        {
            intellectPotion.gameObject.SetActive(true);
        }
    }
}
