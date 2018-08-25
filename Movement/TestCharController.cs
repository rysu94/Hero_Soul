using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TestCharController : MonoBehaviour
{

    //Character Identity
    //This variable keeps track of which character to spawn as, 1 = Cecilia, 2 = Leon, 3 = Risette, 4 = Sparrow
    public static int charID = 0;


    //===========================================
    //             Character Managment
    //===========================================
    public static Transform player;
    public Rigidbody2D playerRB;
    public SpriteRenderer playerSprite;

    public float sprintVel = 1;
    public static Vector2 playerVel = new Vector2(0,-3); 
    
    public float stamNum = 1;
    public Image stamBar;
    public static bool isSprinting = false;

    //Character's Weapon
    public GameObject lightWep;
    public GameObject weapon;
    public AudioSource weaponSound;

    public static bool isSwinging = false;
    public static Vector3 wepDir;

    //Character facing
    public bool north;
    public bool south;
    public bool east;
    public bool west;

    //Diagonal Movement Controls

    // 1 = north, 2 = south, 3 = east, 4 = west
    public static int mainDirection = 0;
    public static int secondaryDirection = 0;

    //Player Face
    public Image face;
    public Image defaultFace;
    public Image attackFace;
    public Image hurtFace;

    //Manages whether the player got hit
    public static bool isHit = false;

    //Dashing Variables
    public bool isSliding = false;
    public ParticleSystem dustCloud;
    public GameObject dust;
    public static float chargeMult;

    //Swiftness Buff
    public static float swiftnessModfier = 0;

    //===========================================


    //===========================================
    //             Player Sound
    //===========================================
    //player attack noise
    public static AudioClip[] attackNoise;
    public static AudioSource playerAttack;

    //player hurt noise
    public static AudioClip[] hurtNoise;
    public static AudioClip[] damageNoise;

    //player cast noise
    public static AudioClip[] castNoise;

    //===========================================


    //===========================================
    //             HUD Management
    //===========================================
    //Minimap Management
    public static GameObject miniMap;
    public static bool miniMapToggle = true;

    //player stamina UI
    public GameObject staminaGauge;
    public Text staminaNum;

    //Stamina Bar
    public float defaultWidth = 73.5f;
    public float stamIncrement;

    //Player health UI
    public GameObject healthGauge;
    public Text healthNum;

    //Health Bar
    public float healthDefaultWidth = 159.2f;
    public float hpIncrement;

    //Mana Gauge
    public List<GameObject> manaFrames = new List<GameObject>();
    public List<GameObject> manaOrbs = new List<GameObject>();

    //Hud States
    public static bool inTreasure = false;
    public static bool inDialogue = false;
    public static bool controller = false;

    //Armor Bar UI
    public GameObject armorGuage;

    //Shield Bar UI
    public GameObject shieldGuage;


    //===========================================


    //===========================================
    //             Attack Management
    //===========================================
    //Manages whether player is performing a heavy attack
    public static bool isHeavy = false;

    //Combo Counter
    public static int comboCount;
    //Combo Counter multiplier
    public static float comboMultiplier;

    //Special Attack Processing
    float startTime = 0;
    bool rightClickBuffer = false;
    public bool charging = false;
    public bool isCharging = false;
    float redColor = 1;
    public bool chargeBuffer = false;

    //Cecilia Special
    public GameObject chargeFrame;
    public GameObject chargeBar;

    //Internal Combo Counter for light attacks
    public int internalComboCounter;
    //Internal Combo buffer, to reset the internal combo
    Coroutine internalBuffer;

    public SpriteRenderer weaponSprite;

    public static bool attackEnabled;

    //===========================================


    //Invulnerability frames
    public static bool invuln = false;
    public bool invulnRoutine = false;

    //Break Invuln
    public static bool breakInvuln = false;

    //Flag that allows player to use aracana
    public static bool arcanaEnabled = true;

    //Button Prompts
    public GameObject leftClickPrompt;
    public GameObject rightClickPrompt;
    public GameObject middleClickPrompt;
    public GameObject lTriggerPrompt;

    //Break flags
    public static bool startBreak = false;
    public static bool breakHit = false;
    public GameObject breakTargetSingle;

    public static bool harrierBreak = false;
    public bool breakBuffer = false;

    //Slow
    public static float slowModifier = 1;

    //---Comapnion---
    //0 none
    //1 Cecilia
    //2 Leon
    //3 Risette
    //4 Sparrow
    public static int companionID = 0;


    void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {
        player = GetComponent<Transform>();
        playerRB = GetComponent<Rigidbody2D>();
        weaponSound = weapon.GetComponent<AudioSource>();
        playerAttack = player.GetComponent<AudioSource>();
        playerSprite = player.GetComponent<SpriteRenderer>();

        //face = GameObject.Find("Player_HUD").transform.Find("Top_Left").transform.Find("FacePlate").transform.Find("CharMask").transform.Find("CharPic").GetComponent<Image>();


        miniMap = GameObject.Find("MiniMap");

        attackNoise = new AudioClip[6];
        attackNoise[0] = (AudioClip)Resources.Load("Sound/Cecilia/Female_Attack_01_Rina");
        attackNoise[1] = (AudioClip)Resources.Load("Sound/Cecilia/Female_Attack_02_Rina");
        attackNoise[2] = (AudioClip)Resources.Load("Sound/Cecilia/Female_Attack_03_Rina");
        attackNoise[3] = (AudioClip)Resources.Load("Sound/Cecilia/Female_Attack_04_Rina");
        attackNoise[4] = (AudioClip)Resources.Load("Sound/Cecilia/Female_Attack_05_Rina");
        attackNoise[5] = (AudioClip)Resources.Load("Sound/Cecilia/Female_Attack_06_Rina");

        hurtNoise = new AudioClip[7];
        hurtNoise[0] = (AudioClip)Resources.Load("Sound/Cecilia/Female_Hurt_01_Rina");
        hurtNoise[1] = (AudioClip)Resources.Load("Sound/Cecilia/Female_Hurt_02_Rina");
        hurtNoise[2] = (AudioClip)Resources.Load("Sound/Cecilia/Female_Hurt_03_Rina");
        hurtNoise[3] = (AudioClip)Resources.Load("Sound/Cecilia/Female_Hurt_04_Rina");
        hurtNoise[4] = (AudioClip)Resources.Load("Sound/Cecilia/Female_Hurt_05_Rina");
        hurtNoise[5] = (AudioClip)Resources.Load("Sound/Cecilia/Female_Hurt_06_Rina");
        hurtNoise[6] = (AudioClip)Resources.Load("Sound/Cecilia/Female_Hurt_07_Rina");

        damageNoise = new AudioClip[2];
        damageNoise[0] = (AudioClip)Resources.Load("Sound/Damage4");
        damageNoise[1] = (AudioClip)Resources.Load("Sound/Damage5");

        castNoise = new AudioClip[2];
        castNoise[0] = (AudioClip)Resources.Load("Sound/Cecilia/Female_ChargeUp_05_Rina");
        castNoise[1] = (AudioClip)Resources.Load("Sound/Cecilia/Female_ChargeUp_06_Rina");

        //Add the frames to the mana frame list
        for (int i = 1; i <= 12; i++)
        {
            manaFrames.Add(GameObject.Find("ManaOrbFrame_" + i));
            manaOrbs.Add(GameObject.Find("ManaOrb" + i));
        }
        
        //initialize the mana frames
        for(int i = 0; i< manaFrames.Count; i++)
        {
            manaFrames[i].SetActive(false);
        }
        //initialize the mana orbs
        for(int i = 0; i < manaOrbs.Count; i++)
        {
            manaOrbs[i].SetActive(false);
        }
        
        if(LevelCreator.startTag == "Up")
        {
            north = true;
            west = false;
            east = false;
            south = false;
            mainDirection = 1;
        }
        else if(LevelCreator.startTag == "Left")
        {
            west = true;
            north = false;
            east = false;
            south = false;
            mainDirection = 4;
        }
        else if(LevelCreator.startTag == "Right")
        {
            east = true;
            west = false;
            north = false;
            south = false;
            mainDirection = 3;
        }
        else if(LevelCreator.startTag == "Down")
        {
            south = true;
            west = false;
            east = false;
            north = false;
            mainDirection = 2;
        }

        //Reset the charge multiplier
        chargeMult = 0;

        healthGauge = GameObject.Find("HPBar");
        healthNum = GameObject.Find("HealthNum").GetComponent<Text>();
        armorGuage = GameObject.Find("ArmorBar");
        shieldGuage = GameObject.Find("ShieldBar");
        stamBar = GameObject.Find("StamBar").GetComponent<Image>();
        staminaGauge = GameObject.Find("StamBar");
        staminaNum = GameObject.Find("StamNum").GetComponent<Text>();
        face = GameObject.Find("CharPic_Real").GetComponent<Image>();


        PlayerStats.currentArmor = PlayerStats.armAmount;
        PlayerStats.currentShield = PlayerStats.shieldAmount;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.V))
        {
            PlayerStats.breakMeter = 100;
        }

        /*
        if (Mathf.Abs(InputManager.CursorJoystick().magnitude) > 0)
        {
            HardwareCursor.SimulateController(Input.GetAxis("J_CursorHorizontal"), Input.GetAxis("J_CursorVertical"), 8);
        }

        
        if(InputManager.A_Button() && (PotionController.hoverPotion || inDialogue || inTreasure))
        {
            controller = true;
            HardwareCursor.LeftClick();
            StartCoroutine(ControllerBuffer());
        }
        if(InputManager.B_Button() && (PotionController.hoverPotion || inDialogue || inTreasure))
        {
            controller = true;
            HardwareCursor.RightClick();
            StartCoroutine(ControllerBuffer());
        }
        */

        if (!isSwinging && !isHit && !InventoryController.inInv && !isSliding && !inDialogue && !inTreasure)
        {
            //Reset Player face
            face.sprite = defaultFace.sprite;

            float chargeModifier = 1f;
            if(charging)
            {
                chargeModifier = .5f;
            }

            playerRB.velocity = (InputManager.MainJoystick() * 1.25f) * (1 + PlayerStats.speedAmount + swiftnessModfier) * chargeModifier * slowModifier;

            if((InputManager.MainHorizontal() == 1 && InputManager.MainVertical() == 1) ||
                (InputManager.MainHorizontal() == 1 && InputManager.MainVertical() == -1) ||
                (InputManager.MainHorizontal() == -1 && InputManager.MainVertical() == 1) ||
                (InputManager.MainHorizontal() == -1 && InputManager.MainVertical() == -1))
            {
                playerRB.velocity = ((InputManager.MainJoystick() * 1.25f) * (1 + PlayerStats.speedAmount + swiftnessModfier) * chargeModifier) * .75f * slowModifier;
            } 


            //print(InputManager.MainJoystick());
            //Determine Animation (Keyboard)
            //if(InputManager.MainHorizontal() > 0 && InputManager.MainVertical() == 0)

            //Determine Animation (Controller)

            //Up/Down
            if(Mathf.Abs(InputManager.MainVertical()) > Mathf.Abs(InputManager.MainHorizontal()))
            {
                if(InputManager.MainVertical() > 0 && InputManager.MainVertical() <= .75f)
                {
                    player.GetComponent<Animator>().Play("TestUpWalk");
                    north = true;
                    west = false;
                    east = false;
                    south = false;
                }
                else if(InputManager.MainVertical() > .75f && InputManager.MainVertical() <= 1f)
                {
                    player.GetComponent<Animator>().Play("Sprint_Up");
                    north = true;
                    west = false;
                    east = false;
                    south = false;
                }
                else if(InputManager.MainVertical() < 0 && InputManager.MainVertical() >= -.75f)
                {
                    player.GetComponent<Animator>().Play("TestWalkDown");
                    north = false;
                    west = false;
                    east = false;
                    south = true;
                }
                else if(InputManager.MainVertical() < -.75f && InputManager.MainVertical() >= -1f)
                {
                    player.GetComponent<Animator>().Play("Sprint_Down");
                    north = false;
                    west = false;
                    east = false;
                    south = true;
                }
            }

            //Left/Right
            else if (Mathf.Abs(InputManager.MainVertical()) < Mathf.Abs(InputManager.MainHorizontal()))
            {
                if (InputManager.MainHorizontal() > 0 && InputManager.MainHorizontal() <= .75f)
                {
                    player.GetComponent<Animator>().Play("TestRightWalk");
                    north = false;
                    west = false;
                    east = true;
                    south = false;
                }
                else if (InputManager.MainHorizontal() > .75f && InputManager.MainHorizontal() <= 1f)
                {
                    player.GetComponent<Animator>().Play("Sprint_Right");
                    north = false;
                    west = false;
                    east = true;
                    south = false;
                }
                else if (InputManager.MainHorizontal() < 0 && InputManager.MainHorizontal() >= -.75f)
                {
                    player.GetComponent<Animator>().Play("TestLeftWalk");
                    north = false;
                    west = true;
                    east = false;
                    south = false;
                }
                else if (InputManager.MainHorizontal() < -.75f && InputManager.MainHorizontal() >= -1f)
                {
                    player.GetComponent<Animator>().Play("Sprint_Left");
                    north = false;
                    west = true;
                    east = false;
                    south = false;
                }
            }

            else if(Mathf.Abs(InputManager.MainHorizontal()) == Mathf.Abs(InputManager.MainVertical()) && 
                Mathf.Abs(InputManager.MainHorizontal()) > 0 && Mathf.Abs(InputManager.MainVertical()) > 0)
            {
                if(InputManager.MainHorizontal() > 0)
                {
                    player.GetComponent<Animator>().Play("Sprint_Right");
                    north = false;
                    west = false;
                    east = true;
                    south = false;
                }
                else if(InputManager.MainHorizontal() < 0)
                {
                    player.GetComponent<Animator>().Play("Sprint_Left");
                    north = false;
                    west = true;
                    east = false;
                    south = false;
                }
            }

            else if (InputManager.MainHorizontal() == 0 && InputManager.MainVertical() == 0)
            {
                if(north)
                {
                    player.GetComponent<Animator>().Play("TestUpIdle");
                }
                else if(south)
                {
                    player.GetComponent<Animator>().Play("TestDownIdle");
                }
                else if(west)
                {
                    player.GetComponent<Animator>().Play("TestLeftIdle");
                }
                else
                {
                    player.GetComponent<Animator>().Play("TestRightIdle");
                }
            }

            //Sprinting
            if (Input.GetKey(KeyCode.LeftShift) && !isSprinting)
            {
                isSprinting = true;
            }
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                isSprinting = false;
            }

            //Invuln
            if(invuln && !invulnRoutine)
            {
                invulnRoutine = true;
                StartCoroutine(IFrameRoutine());
            }

            //Dashing
            if((Input.GetKeyDown(KeyCode.Space) || InputManager.J_Space()) && PlayerStats.stamina >= 25 && !charging)
            {
                PlayerStats.stamina -= 25;
                if (north)
                {
                    player.GetComponent<Animator>().Play("Dash_Up");
                    playerRB.velocity = new Vector2(0, 6);
                    //playerRB.velocity = new Vector2(0, 7);
                    StartCoroutine(SlideRoutine());    
                }
                else if(south)
                {
                    player.GetComponent<Animator>().Play("Dash_Down");
                    playerRB.velocity = new Vector2(0, -6);
                    //playerRB.velocity = new Vector2(0, -7);
                    StartCoroutine(SlideRoutine());
                }
                else if(east)
                {
                    player.GetComponent<Animator>().Play("Dash_Right");
                    playerRB.velocity = new Vector2(6, 0);
                    //playerRB.velocity = new Vector2(7, 0);
                    StartCoroutine(SlideRoutine());
                }
                else if(west)
                {
                    player.GetComponent<Animator>().Play("Dash_Left");
                    playerRB.velocity = new Vector2(-6, 0);
                    //playerRB.velocity = new Vector2(-7, 0);
                    StartCoroutine(SlideRoutine());
                }
            }

        }

        /*
        //Toggles the minimap portion of the HUD
        if(Input.GetKeyDown(KeyCode.M))
        {
            if(miniMapToggle)
            {
                miniMapToggle = false;
                miniMap.SetActive(false);
                
            }
            else if(!miniMapToggle)
            {
                miniMapToggle = true;
                miniMap.SetActive(true);
            }
        }
        */

        //Left Mouse Click, light attack processing
        if ((Input.GetMouseButton(0) || InputManager.J_Trigger() < 0) && !InventoryController.inInv && !inTreasure && !inDialogue && !PotionController.hoverPotion && attackEnabled && !controller)
        {
            if(!isSwinging && PlayerStats.stamina >= 15)
            {
                if(InventoryManager.playerEquipment[0].itemID != 0 && InventoryManager.playerEquipment[0] != null)
                {
                    weaponSprite.sprite = Resources.Load<Sprite>("Item/" + InventoryManager.playerEquipment[0].itemIconName);       
                }
                else
                {
                    weaponSprite.sprite = Resources.Load<Sprite>("Item/W_Spear001");
                }

                StartCoroutine(WeaponSwingRoutine());
            }
            
        }

        //Right Mouse Click, Special attack charge attack
        if ((Input.GetMouseButton(1) || InputManager.J_Trigger() > 0)  && !InventoryController.inInv && !inTreasure && !inDialogue && !PotionController.hoverPotion && !charging && !chargeBuffer && attackEnabled && !controller && PlayerStats.breakMeter < 100)
        {
            chargeFrame.SetActive(true);
            chargeBar.transform.localScale = new Vector2(0, 1);
            chargeBar.transform.localPosition = new Vector2(-.17f, 0);

            charging = true;
            chargeMult = 0;
            chargeBuffer = true;

            /*
            if (InventoryManager.playerEquipment[0].itemID != 0 && InventoryManager.playerEquipment[0] != null)
            {
                weaponSprite.sprite = Resources.Load<Sprite>("Item/" + InventoryManager.playerEquipment[0].itemIconName);
            }
            else
            {
                weaponSprite.sprite = Resources.Load<Sprite>("Item/W_Spear001");
            }
            */

            StartCoroutine(ChargeWeaponRoutine());
        }

  
        if ((!Input.GetMouseButton(1) && InputManager.J_Trigger() <= 0) && charging && chargeBuffer)
        {
            charging = false;
            /*
            if (!isSwinging && PlayerStats.stamina >= 25)
            {
                StartCoroutine(HeavyWeaponSwingRoutine());
            }
            */
        }
        

        //Play the hurt face sprite when player is hit
        if (isHit)
        {
            face.sprite = hurtFace.sprite;
        }

        //updates the player's stamina
        UpdateStamina();

        //updates the player's health
        UpdateHealth();

        //updates the player's mana
        UpdateMana();


    }

    public void SetPlayerDirection()
    {
        if (LevelCreator.startTag == "Up")
        {
            north = true;
            west = false;
            east = false;
            south = false;
            mainDirection = 1;
        }
        else if (LevelCreator.startTag == "Left")
        {
            west = true;
            north = false;
            east = false;
            south = false;
            mainDirection = 4;
        }
        else if (LevelCreator.startTag == "Right")
        {
            east = true;
            west = false;
            north = false;
            south = false;
            mainDirection = 3;
        }
        else if (LevelCreator.startTag == "Down")
        {
            south = true;
            west = false;
            east = false;
            north = false;
            mainDirection = 2;
        }
    }



    //The Time buffer between heavy attack and charge
    IEnumerator RightClickBuffer()
    {
        float scaleX = (chargeBar.transform.localScale.x / 20f);
        float scale = chargeMult / 3f;
        float offset = -.17f - chargeBar.transform.localPosition.x;
        float offsetIncrement = offset / 20f;

        for (int i = 0; i < 20; i++)
        {
            chargeBar.transform.localScale = new Vector2(chargeBar.transform.localScale.x - scaleX, 1f);
            chargeBar.transform.localPosition = new Vector2(chargeBar.transform.localPosition.x + offsetIncrement, 0);
            yield return new WaitForSeconds(.025f);
        }
        charging = false;
        chargeBuffer = false;
        chargeFrame.SetActive(false);
    }


    //Handles the Spear Charge Action
    IEnumerator ChargeWeaponRoutine()
    {
        float velocity = 0;
        redColor = 1;
        isCharging = true;
        while (PlayerStats.stamina > 0 && charging)
        {
            chargeMult += .025f;

            //Cap the damage multiplier
            if (chargeMult > 3)
            {
                chargeMult = 3;
            }

            //Change the charge bar
            float scale = chargeMult / 3f;
            chargeBar.transform.localScale = new Vector2(scale, 1f);
            float offset = -.17f + (.17f * scale);
            chargeBar.transform.localPosition = new Vector2(offset, 0);

            playerSprite.color = new Color(1f, redColor, redColor);
            redColor -= .005f;
            PlayerStats.stamina--;

            velocity += 0.15f;
            //Cap the velocity
            if (velocity > 15)
            {
                velocity = 15;
            }
            yield return new WaitForSeconds(.01f);
        }



        charging = false;
        isSliding = true;
        isHeavy = true;
        lightWep.SetActive(true);
        AudioClip attack = Resources.Load("Sound/Cecilia/WeaponSwing1") as AudioClip;
        lightWep.GetComponent<AudioSource>().clip = attack;
        lightWep.GetComponent<AudioSource>().Play();
        if (north)
        {
            lightWep.transform.rotation = Quaternion.Euler(0, 0, 90);
            if (chargeMult < 1.5)
            {
                player.GetComponent<Animator>().Play("Special_Weak_Up");
            }
            else
            {
                player.GetComponent<Animator>().Play("Special_Strong_Up");
            }
            weapon.transform.eulerAngles = new Vector3(0, 0, 135);
            playerRB.velocity = new Vector2(0, velocity);
        }
        else if (south)
        {
            lightWep.transform.rotation = Quaternion.Euler(0, 0, 270);
            if (chargeMult < 1.5)
            {
                player.GetComponent<Animator>().Play("Special_Weak_Down");
            }
            else
            {
                player.GetComponent<Animator>().Play("Special_Strong_Down");
            }
            weapon.transform.eulerAngles = new Vector3(0, 0, 315);
            playerRB.velocity = new Vector2(0, -velocity);
        }
        else if (east)
        {
            lightWep.transform.rotation = Quaternion.Euler(0, 0, 0);
            if (chargeMult < 1.5)
            {
                player.GetComponent<Animator>().Play("Special_Weak_Right");
            }
            else
            {
                player.GetComponent<Animator>().Play("Special_Strong_Right");
            }
            weapon.transform.eulerAngles = new Vector3(0, 0, 45);
            playerRB.velocity = new Vector2(velocity, 0);
        }
        else if (west)
        {
            lightWep.transform.rotation = Quaternion.Euler(0, 0, 180);
            if (chargeMult < 1.5)
            {
                player.GetComponent<Animator>().Play("Special_Weak_Left");
            }
            else
            {
                player.GetComponent<Animator>().Play("Special_Strong_Left");
            }
            weapon.transform.eulerAngles = new Vector3(0, 0, 225);
            playerRB.velocity = new Vector2(-velocity, 0);
        }
        //weapon.SetActive(true);
        face.sprite = attackFace.sprite;
        weaponSound.Play();
        PlayAttackNoise();
        dust.SetActive(true);
        yield return new WaitForSeconds(.5f);
        playerSprite.color = new Color(1f, 1f, 1f);
        isHeavy = false;
        //weapon.SetActive(false);
        isSliding = false;
        dust.SetActive(false);
        isCharging = false;
        chargeMult = 0;
        chargeBuffer = true;
        StartCoroutine(RightClickBuffer());
        lightWep.SetActive(false);
        if (north)
        {
            player.GetComponent<Animator>().Play("TestUpIdle");
        }
        else if(south)
        {
            player.GetComponent<Animator>().Play("TestDownIdle");
        }
        else if(west)
        {
            player.GetComponent<Animator>().Play("TestLeftIdle");
        }
        else if(east)
        {
            player.GetComponent<Animator>().Play("TestRightIdle");
        }
        
    }
    //===================================================
    //            Light Attack Processing
    //===================================================

    //Set the start location of the attack
    void SetWepDir()
    {
        if(north)
        {
            wepDir = new Vector3(0, 0, 180);
            player.GetComponent<Animator>().Play("TestUpIdle");
        }
        else if(south)
        {
            wepDir = new Vector3(0, 0, 0);
            player.GetComponent<Animator>().Play("TestDownIdle");
        }
        else if(west)
        {
            wepDir = new Vector3(0, 0, 270);
            player.GetComponent<Animator>().Play("TestLeftIdle");
        }
        else if(east)
        {
            wepDir = new Vector3(0, 0, 90);
            player.GetComponent<Animator>().Play("TestRightIdle");
        }
    }

    //Handles the first part of the light atack combo: A wide sweep ~90 degrees in front of player
    IEnumerator WeaponSwingRoutine()
    {
        if(internalBuffer != null)
        {
            StopCoroutine(internalBuffer);
        }

        isSwinging = true;
        face.sprite = attackFace.sprite;
        if(north)
        {
            playerRB.velocity = (new Vector2(0, 1)) * .55f;
        }
        else if(south)
        {
            playerRB.velocity = (new Vector2(0, -1)) * .55f;
        }
        else if(east)
        {
            playerRB.velocity = (new Vector2(1, 0)) * .55f;
        }
        else if(west)
        {
            playerRB.velocity = (new Vector2(-1, 0)) * .55f;
        }
        PlayAttackNoise();
        PlayerStats.UseStam(15);

        //If on 1st combo, right sweep
        if(internalComboCounter == 0)
        {
            if (north)
            {
                player.GetComponent<Animator>().Play("Test_Jab_Up");
                lightWep.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (south)
            {
                player.GetComponent<Animator>().Play("Test_Jab_Down");
                lightWep.transform.rotation = Quaternion.Euler(0, 0, 270);
            }
            else if (west)
            {
                player.GetComponent<Animator>().Play("Test_Jab_Left");
                lightWep.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            else if (east)
            {
                player.GetComponent<Animator>().Play("Test_Jab_Right");
                lightWep.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            yield return new WaitForSeconds(.1f);
            lightWep.SetActive(true);
            AudioClip attack = Resources.Load("Sound/Cecilia/WeaponSwing1") as AudioClip;
            lightWep.GetComponent<AudioSource>().clip = attack;
            lightWep.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(.2f);
            lightWep.SetActive(false);
            internalComboCounter++;
        }
        //If on 2nd combo, left sweep
        else if(internalComboCounter == 1)
        {
            weapon.transform.eulerAngles = new Vector3(0, 0, weapon.transform.eulerAngles.z - 90);

            if (north)
            {
                player.GetComponent<Animator>().Play("Test_Slash1_Up");
                lightWep.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (south)
            {
                player.GetComponent<Animator>().Play("Test_Slash1_Down");
                lightWep.transform.rotation = Quaternion.Euler(0, 0, 270);
            }
            else if (west)
            {
                player.GetComponent<Animator>().Play("Test_Slash1_Left");
                lightWep.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            else if (east)
            {
                player.GetComponent<Animator>().Play("Test_Slash1_Right");
                lightWep.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            yield return new WaitForSeconds(.1f);
            lightWep.SetActive(true);
            AudioClip attack = Resources.Load("Sound/Cecilia/WeaponSwing2") as AudioClip;
            lightWep.GetComponent<AudioSource>().clip = attack;
            lightWep.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(.2f);
            lightWep.SetActive(false);
            internalComboCounter++;
            
        }
        //If on 3rd combo, jab
        else if(internalComboCounter == 2)
        {
            weapon.transform.eulerAngles = new Vector3(0, 0, wepDir.z - 45);
            if (north)
            {
                player.GetComponent<Animator>().Play("Test_Slash2_Up");
                lightWep.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (south)
            {
                player.GetComponent<Animator>().Play("Test_Slash2_Down");
                lightWep.transform.rotation = Quaternion.Euler(0, 0, 270);
            }
            else if (west)
            {
                player.GetComponent<Animator>().Play("Test_Slash2_Left");
                lightWep.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            else if (east)
            {
                player.GetComponent<Animator>().Play("Test_Slash2_Right");
                lightWep.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            yield return new WaitForSeconds(.1f);
            lightWep.SetActive(true);
            AudioClip attack = Resources.Load("Sound/Cecilia/WeaponSwing3") as AudioClip;
            lightWep.GetComponent<AudioSource>().clip = attack;
            lightWep.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(.2f);
            lightWep.SetActive(false);
            internalComboCounter = 0;
        }

        yield return new WaitForSeconds(.05f);
        internalBuffer = StartCoroutine(LightComboBuffer());
        isSwinging = false;   
    }

    IEnumerator LightComboBuffer()
    {
        yield return new WaitForSeconds(.5f);
        internalComboCounter = 0;
    }

    //===================================================
    //            Heavy Attack Processing (discontinued)
    //===================================================

    //Handles the Heavy weapon attack action
    IEnumerator HeavyWeaponSwingRoutine()
    {
        internalComboCounter = 0;
        isSwinging = true;
        isHeavy = true;
        face.sprite = attackFace.sprite;
        playerRB.velocity = (playerVel.normalized) * .25f;
        weapon.gameObject.SetActive(true);
        weaponSound.Play();
        PlayAttackNoise();
        PlayerStats.UseStam(25);
        for (int i = 0; i < 450; i += 15)
        {
            weapon.transform.eulerAngles = new Vector3(0, 0, weapon.transform.eulerAngles.z - 15);
            yield return new WaitForSeconds(.01f);

        }
        weapon.gameObject.SetActive(false);
        //weapon.transform.eulerAngles = new Vector3(0, 0, weapon.transform.eulerAngles.z + 45);
        yield return new WaitForSeconds(.1f);

        weapon.transform.eulerAngles = wepDir;
        isHeavy = false;
        isSwinging = false;
    }

    void PlayAttackNoise()
    {
        playerAttack.clip = attackNoise[Random.Range(0, attackNoise.Length)];
        playerAttack.Play();
    }


    //Updates the Stamina Bar bar on the HUD, resizing and changing the number
    void UpdateStamina()
    {
        staminaNum.text = PlayerStats.stamina.ToString();
        var stamRect = staminaGauge.transform as RectTransform;
        stamIncrement = defaultWidth / PlayerStats.maxStamina;
        float newWidth = PlayerStats.stamina * stamIncrement;
        stamRect.sizeDelta = new Vector2(newWidth, stamRect.sizeDelta.y);
    }

    //Updates the Health Bar bar on the HUD, resizing and changing the number
    void UpdateHealth()
    {
        //Display Shield
        if(PlayerStats.currentShield > 0)
        {
            healthNum.text = PlayerStats.currentShield.ToString();
        }
        else if(PlayerStats.currentArmor > 0)
        {
            healthNum.text = PlayerStats.currentArmor.ToString();
        }
        else
        {
            healthNum.text = PlayerStats.health.ToString();
        }

        var hpRect = healthGauge.transform as RectTransform;
        var armRect = armorGuage.transform as RectTransform;
        var shieldRect = shieldGuage.transform as RectTransform;

        hpIncrement = healthDefaultWidth / PlayerStats.maxHealth;
        float newWidth = PlayerStats.health * hpIncrement;
        hpRect.sizeDelta = new Vector2(newWidth, hpRect.sizeDelta.y);

        hpIncrement = healthDefaultWidth / 300;
        float newWidth2 = PlayerStats.currentArmor * hpIncrement;
        armRect.sizeDelta = new Vector2(newWidth2, armRect.sizeDelta.y);

        hpIncrement = healthDefaultWidth / 300;
        float newWidth3 = PlayerStats.currentShield * hpIncrement;
        shieldRect.sizeDelta = new Vector2(newWidth3, shieldRect.sizeDelta.y);


    }

    //Updates the mana orbs on the HUD
    void UpdateMana()
    {

        for (int i = 0; i < 12; i++)
        {
            GameObject tempObj = manaFrames[i];
            tempObj.SetActive(false);
            manaOrbs[i].SetActive(false);
        }
        //check how many frames should be generated
        int numOrbs = (PlayerStats.wisdom + PlayerStats.bonusWIS + PlayerStats.wisTalent) / 5;
        if(PlayerStats.mana > PlayerStats.maxMana)
        {
            PlayerStats.mana = PlayerStats.maxMana;
        }

        if(numOrbs > 12)
        {
            numOrbs = 12;
        }
        for(int i = 0; i < numOrbs; i++)
        {
            GameObject tempObj = manaFrames[i];
            tempObj.SetActive(true);
        }

        //check how many orbs should be generated
        for(int i = 0; i < PlayerStats.mana; i++)
        {
            GameObject tempObj = manaOrbs[i];
            tempObj.SetActive(true);
        }

        for(int i = PlayerStats.mana; i < PlayerStats.maxMana; i++)
        {
            GameObject tempObj = manaOrbs[i];
            tempObj.SetActive(false);
        }


    }

    //Player Dash
    IEnumerator SlideRoutine()
    {
        isSliding = true;
        dust.SetActive(true);
        yield return new WaitForSeconds(.5f);
        dust.SetActive(false);
        isSliding = false;

        if(north)
        {
            player.GetComponent<Animator>().Play("Sprint_Up");
        }
        else if(south)
        {
            player.GetComponent<Animator>().Play("Sprint_Down");
        }
        else if(east)
        {
            player.GetComponent<Animator>().Play("Sprint_Right");
        }
        else
        {
            player.GetComponent<Animator>().Play("Sprint_Left");
        }
    }

    //Player Invuln Frame
    public IEnumerator IFrameRoutine()
    {
        float time = 1.5f;
        bool flash = false;
        invuln = true;
        while(time > 0)
        {

            yield return new WaitForSeconds(.1f);
            if(!flash)
            {
                player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .25f);
                flash = true;
            }
            else
            {
                player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .75f);
                flash = false;
            }
            time -= .1f;
        }
        invuln = false;
        invulnRoutine = false;
        player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
        isHit = false;
        isSliding = false;
    }

    /*=========================================================================
                             Cecilia Break Skills
    ===========================================================================*/

    //Tier 1 Cecilia Skill: Harrier Dash
    //Charge in a direction, if it connects unlocks button mash sequence to deal damage. Time frozen?
    public IEnumerator HarrierDash(AudioClip bgmClip, float time)
    {
        startBreak = true;
        harrierBreak = true;
        GameObject.Find("HarrierDash").GetComponent<AudioSource>().Play();
        if (north)
        {
            player.GetComponent<Animator>().Play("Start_Dash_Up");
        }
        else if (south)
        {
            player.GetComponent<Animator>().Play("Start_Dash_Down");
        }
        else if (east)
        {
            player.GetComponent<Animator>().Play("Start_Dash_Right");
        }
        else if (west)
        {
            player.GetComponent<Animator>().Play("Start_Dash_Left");
        }
        yield return new WaitForSeconds(2f);
        PlayAttackNoise();
        //Set player velocity to zero
        playerRB.velocity = new Vector2(0, 0);
        lightWep.SetActive(true);
        GameObject.Find("BreakCharge").GetComponent<AudioSource>().Play();

        /*
        for (int i = 0; i < 450; i += 15)
        {
            weapon.transform.eulerAngles = new Vector3(0, 0, weapon.transform.eulerAngles.z - 15);
            yield return new WaitForSeconds(.01f);
        }
        */
        dust.SetActive(true);
        //Determine Dash Direction
        if (north)
        {
            player.GetComponent<Animator>().Play("Harrier_Up");
            lightWep.transform.eulerAngles = new Vector3(0, 0, 90);
            playerRB.velocity = new Vector2(0, 15);
        }
        else if(south)
        {
            player.GetComponent<Animator>().Play("Harrier_Down");
            lightWep.transform.eulerAngles = new Vector3(0, 0, 270);
            playerRB.velocity = new Vector2(0, -15);
        }
        else if(east)
        {
            player.GetComponent<Animator>().Play("Harrier_Right");
            lightWep.transform.eulerAngles = new Vector3(0, 0, 0);
            playerRB.velocity = new Vector2(15, 0);
        }
        else if(west)
        {
            player.GetComponent<Animator>().Play("Harrier_Left");
            lightWep.transform.eulerAngles = new Vector3(0, 0, 180);
            playerRB.velocity = new Vector2(-15, 0);
        }

        while(playerRB.velocity.magnitude > 0)
        {
            yield return new WaitForSeconds(.1f);
        }

        //At the end of the dash, if an enemy is hit execute the prompt routine
        if(breakHit)
        {
            dust.SetActive(false);
            yield return StartCoroutine(HarrierHitRoutine());
        }

        //Jump Back!
        
        //Determine Reverse Dash Direction
        if (north)
        {
            player.GetComponent<Animator>().Play("TestUpIdle");
            playerRB.velocity = new Vector2(0, -7);
        }
        else if (south)
        {
            player.GetComponent<Animator>().Play("TestDownIdle");
            playerRB.velocity = new Vector2(0, 7);
        }
        else if (east)
        {
            player.GetComponent<Animator>().Play("TestRightIdle");
            playerRB.velocity = new Vector2(-7, 0);
        }
        else if (west)
        {
            player.GetComponent<Animator>().Play("TestLeftIdle");
            playerRB.velocity = new Vector2(7, 0);
        }
        dust.SetActive(false);
        lightWep.SetActive(false);
        yield return new WaitForSeconds(1f);

        float volume = .1f;
        while (volume > 0)
        {
            volume -= .005f;
            GameObject.Find("BGM").GetComponent<AudioSource>().volume = volume;
            yield return new WaitForSeconds(.1f);
        }
        GameObject.Find("BGM").GetComponent<AudioSource>().clip = bgmClip;
        GameObject.Find("BGM").GetComponent<AudioSource>().Play();
        GameObject.Find("BGM").GetComponent<AudioSource>().volume = .1f;
        GameObject.Find("BGM").GetComponent<AudioSource>().time = time;

        startBreak = false;
        breakHit = false;
        inDialogue = false;
        breakInvuln = false;
        GameController.paused = false;
        harrierBreak = false;
    }

    IEnumerator HarrierHitRoutine()
    {
        bool bufferSwitch = false;
        GetComponent<BoxCollider2D>().enabled = false;
        if(GameController.xbox360_Enabled)
        {
            lTriggerPrompt.SetActive(false);
        }
        else
        {
            leftClickPrompt.SetActive(true);
        }
        
        for(float i= 0; i < 3; i+=.1f)
        {
            if(breakTargetSingle == null)
            {
                break;
            }
            if((Input.GetMouseButton(0) || InputManager.J_Trigger() < 0) && !bufferSwitch && !breakBuffer)
            {
                StartCoroutine(HitBuffer());
                GameObject.Find("DamageNoise").GetComponent<AudioSource>().Play();
                bufferSwitch = true;
                if(breakTargetSingle != null)
                {
                    int wepDamage = DamageManager.WeaponDamage(breakTargetSingle);
                    breakTargetSingle.GetComponent<Monster>().DamageMonster(wepDamage);
                }
                if (north)
                {
                    playerRB.velocity = new Vector2(0, .05f);
                    Instantiate(Resources.Load<GameObject>("Prefabs/Hit/Pirece_Anim"), transform.position, Quaternion.Euler(0, 0, 90));
                }
                else if(south)
                {
                    playerRB.velocity = new Vector2(0, -.05f);
                    Instantiate(Resources.Load<GameObject>("Prefabs/Hit/Pirece_Anim"), transform.position, Quaternion.Euler(0, 0, 270));
                }
                else if(east)
                {
                    playerRB.velocity = new Vector2(.05f, 0);
                    Instantiate(Resources.Load<GameObject>("Prefabs/Hit/Pirece_Anim"), transform.position, Quaternion.Euler(0, 0, 0));
                }
                else if(west)
                {
                    playerRB.velocity = new Vector2(-.05f, 0);
                    Instantiate(Resources.Load<GameObject>("Prefabs/Hit/Pirece_Anim"), transform.position, Quaternion.Euler(0, 0, 180));
                }
            }
            yield return new WaitForSeconds(.1f);
            bufferSwitch = false;
        }
        leftClickPrompt.SetActive(false);
        lTriggerPrompt.SetActive(false);
        GetComponent<BoxCollider2D>().enabled = true;

    }

    IEnumerator HitBuffer()
    {
        breakBuffer = true;
        yield return new WaitForSeconds(.2f);
        breakBuffer = false;
    }

    //Tier 1 Cecilia Skill: Reload?





    //Stop player from sliding though the wall
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    IEnumerator ControllerBuffer()
    {
        yield return new WaitForSeconds(.25f);
        controller = false;
    }



}
