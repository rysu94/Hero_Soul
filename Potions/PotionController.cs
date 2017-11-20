using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PotionController : MonoBehaviour
{
    public static bool init = false;
    
    public AudioSource healthPotionNoise;
    public AudioSource manaPotionNoise;
    public AudioSource errorNoise;

    public Animator healthPotionAnim;
    public Animator manaPotionAnim;

    public Text healthPotionText;
    public Text manaPotionText;

    //Active Potions
    public static int slot1Active;
    public static int slot2Active;
    public Image slot1IMG;
    public Image slot2IMG;

    //Health Potion Parameters 0
    public static int healthPotionMax;
    public static int healthPotionCurrent;
    public static int healthPotionAmount;

    //Mana Potion Parameters 1
    public static int manaPotionMax;
    public static int manaPotionCurrent;
    public static int manaPotionAmount;

    //Stamina Potion Parameters 2
    public static int staminaPotionMax;
    public static int staminaPotionCurrent;
    public static int staminaPotionAmount;

    //Stoneskin Potion Parameters 3
    public static int stonePotionMax;
    public static int stonePotionCurrent;
    //stack size of the buff
    public static int stonePotionAmount;

    //Strength Potion 4
    public static int strengthPotionMax;
    public static int strengthPotionCurrent;
    //stack size of the buff
    public static int strengthPotionAmount;

    //Alacrity Potion 5
    public static int alacrityPotionMax;
    public static int alacrityPotionCurrent;
    //stack size of the buff
    public static int alacrityPotionAmount;

    //Intellect Potion 6
    public static int intellectPotionMax;
    public static int intellectPotionCurrent;
    //stack size of the buff
    public static int intellectPotionAmount;

    //Checks if player is hovering over a button
    public static bool hoverPotion;

    void Awake()
    {
        if(!init)
        {
            //Player starts with Healing potions and mana potions
            slot1Active = 1;
            slot2Active = 2;

            //heal Potion
            healthPotionAmount = 50;
            healthPotionMax = 3;
            healthPotionCurrent = 3;

            //mana Potion
            manaPotionAmount = 3;
            manaPotionCurrent = 1;
            manaPotionMax = 1;

            //stam potion
            staminaPotionAmount = 0;
            staminaPotionCurrent = 0;
            staminaPotionMax = 0;

            //stone potion
            stonePotionAmount = 0;
            stonePotionCurrent = 0;
            stonePotionMax = 0;

            //strength potion
            strengthPotionAmount = 0;
            strengthPotionCurrent = 0;
            strengthPotionMax = 0;

            //alacrity potion
            alacrityPotionAmount = 0;
            alacrityPotionCurrent = 0;
            alacrityPotionMax = 0;

            //intellect potion
            intellectPotionAmount = 0;
            intellectPotionCurrent = 0;
            intellectPotionMax = 0;

            init = true;
        }
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateActiveSlots();

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = Camera.main.transform.position.z;
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        //Check if mouse is over the button
        if(hit.collider != null && (hit.collider.tag == "Heal_Potion" || hit.collider.tag == "Mana_Potion"))
        {
            hoverPotion = true;
        }
        else
        {
            hoverPotion = false;
        }

        //Check if mouse button is pressed
        if (Input.GetMouseButtonDown(0) && hoverPotion && !TestCharController.inDialogue)
        {
            if(hit.collider.tag == "Heal_Potion")
            {
                UsePotion1(slot1Active);
            }
            else if(hit.collider.tag == "Mana_Potion")
            {
                UsePotion2(slot2Active);
            }
        }

        //Potion Slot 1
        if (Input.GetKeyDown(KeyCode.E))
        {
            UsePotion1(slot1Active);
        }
        //Potion Slot 2
        else if(Input.GetKeyDown(KeyCode.R))
        {
            UsePotion2(slot2Active);
        }	
	}

    //Heals Player
    void HealPlayer()
    {
        PlayerStats.health += healthPotionAmount;
        if(PlayerStats.health > PlayerStats.maxHealth)
        {
            PlayerStats.health = PlayerStats.maxHealth;
        }
    }

    //Regen player's mana
    void RegenMana()
    {
        PlayerStats.mana += manaPotionAmount;
        if(PlayerStats.mana > PlayerStats.maxMana)
        {
            PlayerStats.mana = PlayerStats.maxMana;
        }
    }

    //Regens player Stamina
    void RegenStamina()
    {
        PlayerStats.stamina += staminaPotionAmount;
        if(PlayerStats.stamina > PlayerStats.maxStamina)
        {
            PlayerStats.stamina = PlayerStats.maxStamina;
        }
    }

    //Add stoneskin
    void AddStoneskin()
    {
        GameObject.Find("States").GetComponent<StateManager>().AddState(15, 2, 1, false);
    }

    //Add strength
    void AddStrength()
    {
        GameObject.Find("States").GetComponent<StateManager>().AddState(15, 3, 1, false);
    }

    //Add alacrity
    void AddAlacrity()
    {
        GameObject.Find("States").GetComponent<StateManager>().AddState(15, 4, 1, false);
        GameObject.Find("States").GetComponent<StateManager>().AddState(15, 6, 1, false);
    }

    //Add intellect
    void AddIntellect()
    {
        GameObject.Find("States").GetComponent<StateManager>().AddState(15, 5, 1, false);
    }


    void UsePotion1(int potionID)
    {
        switch(potionID)
        {
            default:
                break;
            case 1:
                if (healthPotionCurrent > 0)
                {
                    healthPotionCurrent--;
                    healthPotionNoise.Play();
                    healthPotionAnim.Play("HealPotionButton");
                    HealPlayer();
                }
                else
                {
                    errorNoise.Play();
                    healthPotionAnim.Play("HealPotionError");
                }
                break;
            case 2:
                if (manaPotionCurrent > 0)
                {
                    manaPotionCurrent--;
                    healthPotionNoise.Play();
                    healthPotionAnim.Play("HealPotionButton");
                    RegenMana();
                }
                else
                {
                    errorNoise.Play();
                    healthPotionAnim.Play("HealPotionError");
                }
                break;
            case 3:
                if (staminaPotionCurrent > 0)
                {
                    staminaPotionCurrent--;
                    healthPotionNoise.Play();
                    healthPotionAnim.Play("HealPotionButton");
                    RegenStamina();
                }
                else
                {
                    errorNoise.Play();
                    healthPotionAnim.Play("HealPotionError");
                }
                break;
            case 4:
                if (stonePotionCurrent > 0)
                {
                    stonePotionCurrent--;
                    healthPotionNoise.Play();
                    healthPotionAnim.Play("HealPotionButton");
                    AddStoneskin();
                }
                else
                {
                    errorNoise.Play();
                    healthPotionAnim.Play("HealPotionError");
                }
                break;
            case 5:
                if (strengthPotionCurrent > 0)
                {
                    strengthPotionCurrent--;
                    healthPotionNoise.Play();
                    healthPotionAnim.Play("HealPotionButton");
                    AddStrength();
                }
                else
                {
                    errorNoise.Play();
                    healthPotionAnim.Play("HealPotionError");
                }
                break;
            case 6:
                if (alacrityPotionCurrent > 0)
                {
                    alacrityPotionCurrent--;
                    healthPotionNoise.Play();
                    healthPotionAnim.Play("HealPotionButton");
                    AddAlacrity();
                }
                else
                {
                    errorNoise.Play();
                    healthPotionAnim.Play("HealPotionError");
                }
                break;
            case 7:
                if (intellectPotionCurrent > 0)
                {
                    intellectPotionCurrent--;
                    healthPotionNoise.Play();
                    healthPotionAnim.Play("HealPotionButton");
                    AddIntellect();
                }
                else
                {
                    errorNoise.Play();
                    healthPotionAnim.Play("HealPotionError");
                }
                break;
        }
    }

    void UsePotion2(int potionID)
    {
        switch(potionID)
        {
            default:
                break;
            case 1:
                if (healthPotionCurrent > 0)
                {
                    healthPotionCurrent--;
                    manaPotionNoise.Play();
                    manaPotionAnim.Play("ManaPotionButton");
                    HealPlayer();
                }
                else
                {
                    errorNoise.Play();
                    manaPotionAnim.Play("ManaPotionError");
                }
                break;
            case 2:
                if (manaPotionCurrent > 0)
                {
                    manaPotionCurrent--;
                    manaPotionNoise.Play();
                    manaPotionAnim.Play("ManaPotionButton");
                    RegenMana();
                }
                else
                {
                    errorNoise.Play();
                    manaPotionAnim.Play("ManaPotionError");
                }
                break;
            case 3:
                if (staminaPotionCurrent > 0)
                {
                    staminaPotionCurrent--;
                    manaPotionNoise.Play();
                    manaPotionAnim.Play("ManaPotionButton");
                    RegenStamina();
                }
                else
                {
                    errorNoise.Play();
                    manaPotionAnim.Play("ManaPotionError");
                }
                break;
            case 4:
                if (stonePotionCurrent > 0)
                {
                    stonePotionCurrent--;
                    manaPotionNoise.Play();
                    manaPotionAnim.Play("ManaPotionButton");
                    AddStoneskin();
                }
                else
                {
                    errorNoise.Play();
                    manaPotionAnim.Play("ManaPotionError");
                }
                break;
            case 5:
                if (strengthPotionCurrent > 0)
                {
                    strengthPotionCurrent--;
                    manaPotionNoise.Play();
                    manaPotionAnim.Play("ManaPotionButton");
                    AddStrength();
                }
                else
                {
                    errorNoise.Play();
                    manaPotionAnim.Play("ManaPotionError");
                }
                break;
            case 6:
                if (alacrityPotionCurrent > 0)
                {
                    alacrityPotionCurrent--;
                    manaPotionNoise.Play();
                    manaPotionAnim.Play("ManaPotionButton");
                    AddAlacrity();
                }
                else
                {
                    errorNoise.Play();
                    manaPotionAnim.Play("ManaPotionError");
                }
                break;
            case 7:
                if (intellectPotionCurrent > 0)
                {
                    intellectPotionCurrent--;
                    manaPotionNoise.Play();
                    manaPotionAnim.Play("ManaPotionButton");
                    AddIntellect();
                }
                else
                {
                    errorNoise.Play();
                    manaPotionAnim.Play("ManaPotionError");
                }
                break;
        }
    }




    void UpdateActiveSlots()
    {
        switch (slot1Active)
        {
            default:
                break;
            case 1:
                slot1IMG.sprite = Resources.Load<Sprite>("Potion/Heal_Potion");
                healthPotionText.text = healthPotionCurrent.ToString() + "/" + healthPotionMax.ToString();
                break;
            case 2:
                slot1IMG.sprite = Resources.Load<Sprite>("Potion/Mana_Potion");
                healthPotionText.text = manaPotionCurrent.ToString() + "/" + manaPotionMax.ToString();
                break;
            case 3:
                slot1IMG.sprite = Resources.Load<Sprite>("Potion/Stamina_Potion");
                healthPotionText.text = staminaPotionCurrent.ToString() + "/" + staminaPotionMax.ToString();
                break;
            case 4:
                slot1IMG.sprite = Resources.Load<Sprite>("Potion/StoneskinPotion");
                healthPotionText.text = stonePotionCurrent.ToString() + "/" + stonePotionMax.ToString();
                break;
            case 5:
                slot1IMG.sprite = Resources.Load<Sprite>("Potion/strPotion_Potion");
                healthPotionText.text = strengthPotionCurrent.ToString() + "/" + strengthPotionMax.ToString();
                break;
            case 6:
                slot1IMG.sprite = Resources.Load<Sprite>("Potion/AlacrityPotion");
                healthPotionText.text = alacrityPotionCurrent.ToString() + "/" + alacrityPotionMax.ToString();
                break;
            case 7:
                slot1IMG.sprite = Resources.Load<Sprite>("Potion/intPotion");
                healthPotionText.text = intellectPotionCurrent.ToString() + "/" + intellectPotionMax.ToString();
                break;
        }

        switch (slot2Active)
        {
            default:
                break;
            case 1:
                slot2IMG.sprite = Resources.Load<Sprite>("Potion/Heal_Potion");
                manaPotionText.text = healthPotionCurrent.ToString() + "/" + healthPotionMax.ToString();
                break;
            case 2:
                slot2IMG.sprite = Resources.Load<Sprite>("Potion/Mana_Potion");
                manaPotionText.text = manaPotionCurrent.ToString() + "/" + manaPotionMax.ToString();
                break;
            case 3:
                slot2IMG.sprite = Resources.Load<Sprite>("Potion/Stamina_Potion");
                manaPotionText.text = staminaPotionCurrent.ToString() + "/" + staminaPotionMax.ToString();
                break;
            case 4:
                slot2IMG.sprite = Resources.Load<Sprite>("Potion/StoneskinPotion");
                manaPotionText.text = stonePotionCurrent.ToString() + "/" + stonePotionMax.ToString();
                break;
            case 5:
                slot2IMG.sprite = Resources.Load<Sprite>("Potion/strPotion_Potion");
                manaPotionText.text = strengthPotionCurrent.ToString() + "/" + strengthPotionMax.ToString();
                break;
            case 6:
                slot2IMG.sprite = Resources.Load<Sprite>("Potion/AlacrityPotion");
                manaPotionText.text = alacrityPotionCurrent.ToString() + "/" + alacrityPotionMax.ToString();
                break;
            case 7:
                slot2IMG.sprite = Resources.Load<Sprite>("Potion/intPotion");
                manaPotionText.text = intellectPotionCurrent.ToString() + "/" + intellectPotionMax.ToString();
                break;
        }
    }

}
