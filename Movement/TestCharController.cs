using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TestCharController : MonoBehaviour
{
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
    public GameObject weapon;
    public AudioSource weaponSound;

    public static bool isSwinging = false;
    public static Vector3 wepDir;

    //Character facing
    public bool north;
    public bool south;
    public bool east;
    public bool west;

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

    //The Empty Mana Orbs
    public GameObject manaFrame1;
    public GameObject manaFrame2;
    public GameObject manaFrame3;
    public GameObject manaFrame4;
    public GameObject manaFrame5;
    public GameObject manaFrame6;
    public GameObject manaFrame7;
    public GameObject manaFrame8;
    public GameObject manaFrame9;
    public GameObject manaFrame10;
    public GameObject manaFrame11;
    public GameObject manaFrame12;

    //The filled mana Orbs
    public GameObject manaOrb1;
    public GameObject manaOrb2;
    public GameObject manaOrb3;
    public GameObject manaOrb4;
    public GameObject manaOrb5;
    public GameObject manaOrb6;
    public GameObject manaOrb7;
    public GameObject manaOrb8;
    public GameObject manaOrb9;
    public GameObject manaOrb10;
    public GameObject manaOrb11;
    public GameObject manaOrb12;

    //Hud States
    public static bool inTreasure = false;
    public static bool inDialogue = false;

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


    //Break flags

    public static bool startBreak = false;
    public static bool breakHit = false;
    public GameObject breakTargetSingle;

    public static bool harrierBreak = false;


    // Use this for initialization
    void Start()
    {
        player = GetComponent<Transform>();
        playerRB = GetComponent<Rigidbody2D>();
        weaponSound = weapon.GetComponent<AudioSource>();
        playerAttack = player.GetComponent<AudioSource>();
        playerSprite = player.GetComponent<SpriteRenderer>();

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

        //Getting the mana Frame UI components
        manaFrame1 = GameObject.Find("ManaOrbFrame_1");
        manaFrame2 = GameObject.Find("ManaOrbFrame_2");
        manaFrame3 = GameObject.Find("ManaOrbFrame_3");
        manaFrame4 = GameObject.Find("ManaOrbFrame_4");
        manaFrame5 = GameObject.Find("ManaOrbFrame_5");
        manaFrame6 = GameObject.Find("ManaOrbFrame_6");
        manaFrame7 = GameObject.Find("ManaOrbFrame_7");
        manaFrame8 = GameObject.Find("ManaOrbFrame_8");
        manaFrame9 = GameObject.Find("ManaOrbFrame_9");
        manaFrame10 = GameObject.Find("ManaOrbFrame_10");
        manaFrame11 = GameObject.Find("ManaOrbFrame_11");
        manaFrame12 = GameObject.Find("ManaOrbFrame_12");

        //Add the frames to the mana frame list
        manaFrames.Add(manaFrame1);
        manaFrames.Add(manaFrame2);
        manaFrames.Add(manaFrame3);
        manaFrames.Add(manaFrame4);
        manaFrames.Add(manaFrame5);
        manaFrames.Add(manaFrame6);
        manaFrames.Add(manaFrame7);
        manaFrames.Add(manaFrame8);
        manaFrames.Add(manaFrame9);
        manaFrames.Add(manaFrame10);
        manaFrames.Add(manaFrame11);
        manaFrames.Add(manaFrame12);

        manaOrb1 = GameObject.Find("ManaOrb1");
        manaOrb2 = GameObject.Find("ManaOrb2");
        manaOrb3 = GameObject.Find("ManaOrb3");
        manaOrb4 = GameObject.Find("ManaOrb4");
        manaOrb5 = GameObject.Find("ManaOrb5");
        manaOrb6 = GameObject.Find("ManaOrb6");
        manaOrb7 = GameObject.Find("ManaOrb7");
        manaOrb8 = GameObject.Find("ManaOrb8");
        manaOrb9 = GameObject.Find("ManaOrb9");
        manaOrb10 = GameObject.Find("ManaOrb10");
        manaOrb11 = GameObject.Find("ManaOrb11");
        manaOrb12 = GameObject.Find("ManaOrb12");

        //Add the mana orbs to the orb list
        manaOrbs.Add(manaOrb1);
        manaOrbs.Add(manaOrb2);
        manaOrbs.Add(manaOrb3);
        manaOrbs.Add(manaOrb4);
        manaOrbs.Add(manaOrb5);
        manaOrbs.Add(manaOrb6);
        manaOrbs.Add(manaOrb7);
        manaOrbs.Add(manaOrb8);
        manaOrbs.Add(manaOrb9);
        manaOrbs.Add(manaOrb10);
        manaOrbs.Add(manaOrb11);
        manaOrbs.Add(manaOrb12);


        manaFrame1.gameObject.SetActive(false);
        manaFrame2.gameObject.SetActive(false);
        manaFrame3.gameObject.SetActive(false);
        manaFrame4.gameObject.SetActive(false);
        manaFrame5.gameObject.SetActive(false);
        manaFrame6.gameObject.SetActive(false);
        manaFrame7.gameObject.SetActive(false);
        manaFrame8.gameObject.SetActive(false);
        manaFrame9.gameObject.SetActive(false);
        manaFrame10.gameObject.SetActive(false);
        manaFrame11.gameObject.SetActive(false);
        manaFrame12.gameObject.SetActive(false);

        manaOrb1.gameObject.SetActive(false);
        manaOrb2.gameObject.SetActive(false);
        manaOrb3.gameObject.SetActive(false);
        manaOrb4.gameObject.SetActive(false);
        manaOrb5.gameObject.SetActive(false);
        manaOrb6.gameObject.SetActive(false);
        manaOrb7.gameObject.SetActive(false);
        manaOrb8.gameObject.SetActive(false);
        manaOrb9.gameObject.SetActive(false);
        manaOrb10.gameObject.SetActive(false);
        manaOrb11.gameObject.SetActive(false);
        manaOrb12.gameObject.SetActive(false);

        north = true;

        //Reset the charge multiplier
        chargeMult = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isSwinging && !isHit && !InventoryController.inInv && !isSliding && !inDialogue)
        {
            face.sprite = defaultFace.sprite;
            if (Input.GetKey(KeyCode.W))
            {
                playerRB.velocity = new Vector2(0, (.85f + swiftnessModfier));
                if(charging)
                {
                    playerRB.velocity = new Vector2(0, (.6f + swiftnessModfier));
                }
                else if(isSprinting)
                {
                    playerRB.velocity = new Vector2(0, (1.25f + swiftnessModfier));
                }
                playerVel = new Vector2(0, 3);

                north = true;
                south = false;
                east = false;
                west = false;

                if (Input.GetKeyDown(KeyCode.Space) && PlayerStats.stamina >= 25 && !charging)
                {
                    player.GetComponent<Animator>().Play("Dash_Up");
                    playerRB.velocity = new Vector2(0, 7);
                    StartCoroutine(SlideRoutine());
                    PlayerStats.stamina -= 25;
                }
                else if(Input.GetKey(KeyCode.LeftShift) && !isSprinting)
                {
                    isSprinting = true;
                }
            }
            else if (Input.GetKey(KeyCode.A))
            {
                playerRB.velocity = new Vector2((-.85f - swiftnessModfier), 0);
                if (charging)
                {
                    playerRB.velocity = new Vector2((-.6f - swiftnessModfier), 0);
                }
                else if (isSprinting)
                {
                    playerRB.velocity = new Vector2((-1.25f - swiftnessModfier), 0);
                }
                playerVel = new Vector2(-3, 0);

                north = false;
                south = false;
                east = false;
                west = true;

                if (Input.GetKeyDown(KeyCode.Space) && PlayerStats.stamina >= 25 && !charging)
                {
                    player.GetComponent<Animator>().Play("Dash_Left");
                    playerRB.velocity = new Vector2(-7, 0);
                    StartCoroutine(SlideRoutine());
                    PlayerStats.stamina -= 25;
                }
                else if (Input.GetKey(KeyCode.LeftShift) && !isSprinting)
                {
                    isSprinting = true;
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                playerRB.velocity = new Vector2((.85f + swiftnessModfier), 0);
                if (charging)
                {
                    playerRB.velocity = new Vector2((.6f + swiftnessModfier), 0);
                }
                else if (isSprinting)
                {
                    playerRB.velocity = new Vector2((1.25f + swiftnessModfier), 0);
                }
                playerVel = new Vector2(3, 0);

                north = false;
                south = false;
                east = true;
                west = false;

                if (Input.GetKeyDown(KeyCode.Space) && PlayerStats.stamina >= 25 && !charging)
                {
                    player.GetComponent<Animator>().Play("Dash_Right");
                    playerRB.velocity = new Vector2(7, 0);
                    StartCoroutine(SlideRoutine());
                    PlayerStats.stamina -= 25;
                }
                else if (Input.GetKey(KeyCode.LeftShift) && !isSprinting)
                {
                    isSprinting = true;
                }
            }
            else if (Input.GetKey(KeyCode.S))
            {
                playerRB.velocity = new Vector2(0, (-.85f - swiftnessModfier));
                if (charging)
                {
                    playerRB.velocity = new Vector2(0, (-.6f - swiftnessModfier));
                }
                else if (isSprinting)
                {
                    playerRB.velocity = new Vector2(0, (-1.25f - swiftnessModfier));
                }
                playerVel = new Vector2(0, -3);

                north = false;
                south = true;
                east = false;
                west = false;

                if (Input.GetKeyDown(KeyCode.Space) && PlayerStats.stamina >= 25 && !charging)
                {
                    player.GetComponent<Animator>().Play("Dash_Down");
                    playerRB.velocity = new Vector2(0, -7);
                    StartCoroutine(SlideRoutine());
                    PlayerStats.stamina -= 25;
                }
                else if (Input.GetKey(KeyCode.LeftShift) && !isSprinting)
                {
                    isSprinting = true;
                }
            }
            if (Input.GetKeyUp(KeyCode.W) && !isSliding)
            {
                //player.transform.Translate(new Vector2(0, 0));
                weapon.transform.eulerAngles = new Vector3(0, 0, 180);
                playerRB.velocity = new Vector2(0, 0);

            }
            if (Input.GetKeyUp(KeyCode.A) && !isSliding)
            {
                //player.transform.Translate(new Vector2(0, 0));
                weapon.transform.eulerAngles = new Vector3(0, 0, 270);
                playerRB.velocity = new Vector2(0, 0);

            }
            if (Input.GetKeyUp(KeyCode.D) && !isSliding)
            {
                // player.transform.Translate(new Vector2(0, 0));
                weapon.transform.eulerAngles = new Vector3(0, 0, 90);
                playerRB.velocity = new Vector2(0, 0);
            }
            if (Input.GetKeyUp(KeyCode.S) && !isSliding)
            {
                // player.transform.Translate(new Vector2(0, 0));
                weapon.transform.eulerAngles = new Vector3(0, 0, 0);
                playerRB.velocity = new Vector2(0, 0);
            }
            if(!Input.GetKey(KeyCode.LeftShift))
            {
                isSprinting = false;
            }

            if(invuln && !invulnRoutine)
            {
                invulnRoutine = true;
                StartCoroutine(IFrameRoutine());
            }


        }

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

        //Left Mouse Click, light attack processing
        if (Input.GetMouseButtonDown(0) && !InventoryController.inInv && !inTreasure && !inDialogue && !PotionController.hoverPotion)
        {
            if(!isSwinging && PlayerStats.stamina >= 10)
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
        if (Input.GetMouseButton(1) && !InventoryController.inInv && !inTreasure && !inDialogue && !PotionController.hoverPotion && !charging && !chargeBuffer)
        {
            chargeFrame.SetActive(true);
            chargeBar.transform.localScale = new Vector2(0, 1);
            chargeBar.transform.localPosition = new Vector2(-.17f, 0);

            charging = true;
            chargeMult = 0;
            chargeBuffer = true;

            if (InventoryManager.playerEquipment[0].itemID != 0 && InventoryManager.playerEquipment[0] != null)
            {
                weaponSprite.sprite = Resources.Load<Sprite>("Item/" + InventoryManager.playerEquipment[0].itemIconName);
            }
            else
            {
                weaponSprite.sprite = Resources.Load<Sprite>("Item/W_Spear001");
            }

            StartCoroutine(ChargeWeaponRoutine());
        }

  
        if (!Input.GetMouseButton(1) && charging && chargeBuffer)
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
            yield return new WaitForSeconds(.018f);
        }



        charging = false;
        isSliding = true;
        isHeavy = true;
        if (north)
        {
            player.GetComponent<Animator>().Play("Dash_Up");
            weapon.transform.eulerAngles = new Vector3(0, 0, 135);
            playerRB.velocity = new Vector2(0, velocity);
        }
        else if (south)
        {
            player.GetComponent<Animator>().Play("Dash_Down");
            weapon.transform.eulerAngles = new Vector3(0, 0, 315);
            playerRB.velocity = new Vector2(0, -velocity);
        }
        else if (east)
        {
            player.GetComponent<Animator>().Play("Dash_Right");
            weapon.transform.eulerAngles = new Vector3(0, 0, 45);
            playerRB.velocity = new Vector2(velocity, 0);
        }
        else if (west)
        {
            player.GetComponent<Animator>().Play("Dash_Left");
            weapon.transform.eulerAngles = new Vector3(0, 0, 225);
            playerRB.velocity = new Vector2(-velocity, 0);
        }
        weapon.SetActive(true);
        face.sprite = attackFace.sprite;
        weaponSound.Play();
        PlayAttackNoise();
        dust.SetActive(true);
        yield return new WaitForSeconds(.5f);
        playerSprite.color = new Color(1f, 1f, 1f);
        isHeavy = false;
        weapon.SetActive(false);
        isSliding = false;
        dust.SetActive(false);
        isCharging = false;

        chargeBuffer = true;
        StartCoroutine(RightClickBuffer());

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
        playerRB.velocity = (playerVel.normalized) * .25f;
        weapon.gameObject.SetActive(true);
        weaponSound.Play();
        PlayAttackNoise();
        SetWepDir();
        weapon.transform.eulerAngles = wepDir;
        PlayerStats.UseStam(10);

        //If on 1st combo, right sweep
        if(internalComboCounter == 0)
        {
            for (int i = 0; i <= 90; i += 10)
            {
                weapon.transform.eulerAngles = new Vector3(0, 0, weapon.transform.eulerAngles.z - 10);
                yield return new WaitForSeconds(.01f);
            }
            internalComboCounter++;
        }
        //If on 2nd combo, left sweep
        else if(internalComboCounter == 1)
        {
            weapon.transform.eulerAngles = new Vector3(0, 0, weapon.transform.eulerAngles.z - 90);
            for (int i = 0; i <= 90; i += 10)
            {
                weapon.transform.eulerAngles = new Vector3(0, 0, weapon.transform.eulerAngles.z + 10);
                yield return new WaitForSeconds(.01f);

            }
            internalComboCounter++;
            
        }
        //If on 3rd combo, jab
        else if(internalComboCounter == 2)
        {
            weapon.transform.eulerAngles = new Vector3(0, 0, wepDir.z - 45);
            //pull back
            if(north)
            {
                for (int i = 0; i < 5; i++)
                {
                    weapon.transform.position = new Vector2(player.transform.position.x, player.transform.position.y - 0.03f);
                    yield return new WaitForSeconds(.005f);
                }
                weapon.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 0.1f);
                yield return new WaitForSeconds(.1f);
                weapon.transform.position = new Vector2(player.transform.position.x, player.transform.position.y - 0.045f);
            }
            else if(south)
            {
                for (int i = 0; i < 5; i++)
                {
                    weapon.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 0.03f);
                    yield return new WaitForSeconds(.005f);
                }
                weapon.transform.position = new Vector2(player.transform.position.x, player.transform.position.y - 0.1f);
                yield return new WaitForSeconds(.1f);
                weapon.transform.position = new Vector2(player.transform.position.x, player.transform.position.y - 0.045f);
            }
            else if(east)
            {
                for (int i = 0; i < 5; i++)
                {
                    weapon.transform.position = new Vector2(player.transform.position.x - 0.07f, player.transform.position.y);
                    yield return new WaitForSeconds(.005f);
                }
                weapon.transform.position = new Vector2(player.transform.position.x + 0.1f, player.transform.position.y);
                yield return new WaitForSeconds(.1f);
                weapon.transform.position = new Vector2(player.transform.position.x, player.transform.position.y - 0.045f);
            }
            else if(west)
            {
                for (int i = 0; i < 5; i++)
                {
                    weapon.transform.position = new Vector2(player.transform.position.x + 0.07f, player.transform.position.y);
                    yield return new WaitForSeconds(.005f);
                }
                weapon.transform.position = new Vector2(player.transform.position.x - 0.1f, player.transform.position.y);
                yield return new WaitForSeconds(.1f);
                weapon.transform.position = new Vector2(player.transform.position.x, player.transform.position.y - 0.045f);
            }
            internalComboCounter = 0;
        }

        weapon.gameObject.SetActive(false);
        yield return new WaitForSeconds(.05f);
        weapon.transform.eulerAngles = wepDir;
        internalBuffer = StartCoroutine(LightComboBuffer());
        isSwinging = false;   
    }

    IEnumerator LightComboBuffer()
    {
        yield return new WaitForSeconds(2f);
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
        healthNum.text = PlayerStats.health.ToString();
        var hpRect = healthGauge.transform as RectTransform;
        hpIncrement = healthDefaultWidth / PlayerStats.maxHealth;
        float newWidth = PlayerStats.health * hpIncrement;
        hpRect.sizeDelta = new Vector2(newWidth, hpRect.sizeDelta.y);
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
        int numOrbs = (PlayerStats.wisdom + PlayerStats.bonusWIS) / 5;
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
            player.GetComponent<Animator>().Play("TestUpIdle");
        }
        else if(south)
        {
            player.GetComponent<Animator>().Play("TestDownIdle");
        }
        else if(east)
        {
            player.GetComponent<Animator>().Play("TestRightIdle");
        }
        else
        {
            player.GetComponent<Animator>().Play("TestLeftIdle");
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
        weapon.SetActive(true);
        GameObject.Find("BreakCharge").GetComponent<AudioSource>().Play();

        /*
        for (int i = 0; i < 450; i += 15)
        {
            weapon.transform.eulerAngles = new Vector3(0, 0, weapon.transform.eulerAngles.z - 15);
            yield return new WaitForSeconds(.01f);
        }
        */

        //Determine Dash Direction
        if (north)
        {
            player.GetComponent<Animator>().Play("Dash_Up");
            weapon.transform.eulerAngles = new Vector3(0, 0, 135);
            playerRB.velocity = new Vector2(0, 15);
        }
        else if(south)
        {
            player.GetComponent<Animator>().Play("Dash_Down");
            weapon.transform.eulerAngles = new Vector3(0, 0, 315);
            playerRB.velocity = new Vector2(0, -15);
        }
        else if(east)
        {
            player.GetComponent<Animator>().Play("Dash_Right");
            weapon.transform.eulerAngles = new Vector3(0, 0, 45);
            playerRB.velocity = new Vector2(15, 0);
        }
        else if(west)
        {
            player.GetComponent<Animator>().Play("Dash_Left");
            weapon.transform.eulerAngles = new Vector3(0, 0, 225);
            playerRB.velocity = new Vector2(-15, 0);
        }

        while(playerRB.velocity.magnitude > 0)
        {
            yield return new WaitForSeconds(.1f);
        }

        //At the end of the dash, if an enemy is hit execute the prompt routine
        if(breakHit)
        {
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
        weapon.SetActive(false);
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
        leftClickPrompt.SetActive(true);
        for(float i= 0; i < 3; i+=.1f)
        {
            if(breakTargetSingle == null)
            {
                break;
            }
            if(Input.GetMouseButton(0) && !bufferSwitch)
            {
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
                }
                else if(south)
                {
                    playerRB.velocity = new Vector2(0, -.05f);
                }
                else if(east)
                {
                    playerRB.velocity = new Vector2(.05f, 0);
                }
                else if(west)
                {
                    playerRB.velocity = new Vector2(-.05f, 0);
                }
            }
            yield return new WaitForSeconds(.1f);
            bufferSwitch = false;
        }
        leftClickPrompt.SetActive(false);
        GetComponent<BoxCollider2D>().enabled = true;

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




}
