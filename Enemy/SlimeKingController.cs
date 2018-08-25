using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/*
Hero Project Slime King Class
By: Ryan Su

This class holds the unique behaviors of the slime king enemy, including animations and AI

*/

public class SlimeKingController : Monster
{
    public GameObject slimeParent;
    public GameObject slimePrefab;
    public GameObject cameraObject;
    public GameObject bossBarFrame;
    public GameObject bossBar;

    public GameObject stateManager;

    public Text bossBarText;

    public float bossBarWidth = 201.4f;
    public float monsterMaxHealth = 1750;

    public Animator slimeAnim;

    public Vector2 defaultDir = new Vector2(1, 0);
    public Vector2 newDir;

    public AudioClip normalMusic;


    //Slime King Sfx 
    public AudioClip collapseNoise;
    public AudioClip groundPoundNoise;
    public AudioClip whistleNoise;

    public Coroutine behaviorRoutine;

    //spawn slime prefabs
    public GameObject spawnSlimePrefabUp;
    public GameObject spawnSlimePrefabDown;

    //size shrinking variable
    public bool phase1 = false;
    public bool phase2 = false;
    public bool phase3 = false;

    //Is the slime king rolling?
    public bool isRolling = false;
    public bool wallHit = false;
    public bool rollingUp = false;
    public bool rollingDown = false;

    public GameObject bossWipe;
    public AudioClip bossMusic;

    public int jumpCount = 0;

    //Charge Bar
    public float chargeMult;
    public GameObject chargeBar, chargeFrame;

    public GameObject treasure;

    //Hero Soul Spawned?
    bool soulSpawned = false;

    // Use this for initialization
    void Start()
    {
        boss = true;

        monsterHealth = 1750;
        monsterMaxHealth = 1750;
        contactDamage = 30;

        player = GameObject.FindGameObjectWithTag("Player");
        slimeAnim = GetComponent<Animator>();
        monsterSprite = GetComponent<SpriteRenderer>();
        monsterRB = GetComponent<Rigidbody2D>();

        //Slime King Loot Table

        //Arcane Copper Ring
        monsterDrops.Add(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[13]);
        monsterDropChance.Add(10);
        //Enduring Copper Ring
        monsterDrops.Add(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[14]);
        monsterDropChance.Add(10);
        //Pendent of Endurance
        monsterDrops.Add(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[33]);
        monsterDropChance.Add(10);
        //Pendent of Int
        monsterDrops.Add(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[34]);
        monsterDropChance.Add(10);


        //Arcana [fire, water, earth, air, life]
        arcanaDrop.Add(0);
        arcanaDrop.Add(30);
        arcanaDrop.Add(0);
        arcanaDrop.Add(5);
        arcanaDrop.Add(5);

        //experience drop
        experienceDrop = 150;

        colNoise = GetComponent<AudioSource>().clip;

        StartCoroutine(StartRoutine());
        
        

        
    }

    // Update is called once per frame
    void Update()
    {
        float healthPercent = (float)monsterHealth / (float)monsterMaxHealth;
        var xpBarRect = bossBar.transform as RectTransform;
        xpBarRect.sizeDelta = new Vector2(bossBarWidth * healthPercent, xpBarRect.sizeDelta.y);

        if(monsterHealth <= 0 && !GameController.paused)
        {
            bossBarFrame.SetActive(false);
            GameObject.Find("BGM").GetComponent<AudioSource>().clip = normalMusic;
            GameObject.Find("BGM").GetComponent<AudioSource>().Play();
        }
        //75% size change
        if(monsterHealth <= (monsterMaxHealth*.75f) && !phase1 && !phase2 && !phase3 && !isRolling)
        {
            phase1 = true;
            StartCoroutine(RolloutRoutine(3));
        }
        //50% size change
        else if(monsterHealth <= (monsterMaxHealth * .5f) && !phase2 && !phase3 && !isRolling)
        {
            phase2 = true;
            phase1 = false;
            StartCoroutine(RolloutRoutine(6));
        }
        //25% size change
        else if(monsterHealth <= (monsterMaxHealth * .25f) && !phase3 && !isRolling)
        {
            phase3 = true;
            phase2  = false;
            StartCoroutine(RolloutRoutine(9));
        }

        if(monsterHealth < 0)
        {
            LevelDatabase.forestOfBeginning.isBoss = false;
            FlagController.korosClear = true;
        }

        if(unstoppable)
        {
            GetComponent<CircleCollider2D>().enabled = false;
        }
        else
        {
            GetComponent<CircleCollider2D>().enabled = true;
        }

        if(monsterHealth <= 0)
        {
            treasure.SetActive(true);
            treasure.transform.Find("Light_Beam").GetComponent<Animator>().Play("Mosaic_Light_Spawn");
            if(!soulSpawned)
            {
                Instantiate(Resources.Load("Prefabs/Soul/Hero_Soul_Selector"), transform.position, Quaternion.identity);
                soulSpawned = true;
            }

            
        }

    }
    
    /* Slime King's Behaviors
    1. Jump Towards player
    2. Rollout Routine
    3. Ground Pound
    */

    //Slime King Behavior Controller
    IEnumerator SlimeKingBehavior()
    {
        while(monsterHealth > 0)
        {
            print(GetComponent<Rigidbody2D>().velocity);
            //Check if the object is paused
            while (GameController.paused)
            {
                yield return null;
            }
            
            //Time between actions
            monsterRB.constraints = ~RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
            yield return new WaitForSeconds(1.5f);
            monsterRB.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
            
            //Decide what action to do
            if(!isRolling)
            {
                if (jumpCount == 10)
                {
                    //Charge Bar
                    if (phase1)
                    {
                        chargeFrame.transform.localScale = new Vector2(.375f, .25f);
                    }
                    else if (phase2)
                    {
                        chargeFrame.transform.localScale = new Vector2(.5f, .33f);
                    }
                    else if (phase3)
                    {
                        chargeFrame.transform.localScale = new Vector2(.75f, .5f);
                    }
                    else
                    {
                        chargeFrame.transform.localScale = new Vector2(.3f, .2f);
                    }

                    chargeFrame.SetActive(true);
                    chargeMult = 0;

                    for(int i = 0; i < 100; i++)
                    {
                        //Change the charge bar
                        float scale = chargeMult / 100f;
                        chargeBar.transform.localScale = new Vector2(scale, 1f);
                        float offset = -.17f + (.17f * scale);
                        chargeBar.transform.localPosition = new Vector2(offset, 0);
                        chargeMult += 1;
                        yield return new WaitForSeconds(.01f);
                    }
                    chargeFrame.SetActive(false);

                    StartCoroutine(GroundPound());
                    yield return StartCoroutine(ShakeScreen(.35f)); 
                    jumpCount = 0;
                }
                else
                {
                    MoveRoutine();
                    jumpCount++;
                }
            }
        }
    }

    //Shakes the screen
    IEnumerator ShakeScreen(float duration)
    {
        //Check if the object is paused
        while (GameController.paused)
        {
            monsterRB.velocity = new Vector2(0, 0);
            yield return null;
        }

        GetComponent<AudioSource>().clip = groundPoundNoise;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(.5f);

        if(phase1)
        {
            stateManager.GetComponent<StateManager>().AddState(3, 7, 1, false);
        }
        else if(phase2)
        {
            stateManager.GetComponent<StateManager>().AddState(2, 7, 1, false);
        }
        else if (phase2)
        {
            stateManager.GetComponent<StateManager>().AddState(1, 7, 1, false);
        }
        else
        {
            stateManager.GetComponent<StateManager>().AddState(4, 7, 1, false);
        }

        float shakeDuration = duration;
        Vector2 originalPos = cameraObject.transform.position;
        while(shakeDuration > 0)
        {
            shakeDuration -= .01f;
            Vector2 newPos = new Vector2(originalPos.x + (Random.insideUnitCircle.x * .04f), originalPos.y);
            cameraObject.transform.position = new Vector3(newPos.x, newPos.y, -10);
            yield return new WaitForSeconds(.01f);
        }
        yield return new WaitForSeconds(.01f);
        cameraObject.transform.position = new Vector3(originalPos.x, originalPos.y, -10);
    }

    //Shakes the screen no stun
    IEnumerator ShakeScreenNS(float duration)
    {
        GetComponent<AudioSource>().clip = groundPoundNoise;
        GetComponent<AudioSource>().Play();
        float shakeDuration = duration;
        Vector2 originalPos = cameraObject.transform.position;
        while (shakeDuration > 0)
        {
            shakeDuration -= .01f;
            Vector2 newPos = new Vector2(originalPos.x + (Random.insideUnitCircle.x * .04f), originalPos.y);
            cameraObject.transform.position = new Vector3(newPos.x, newPos.y, -10);
            yield return new WaitForSeconds(.01f);
        }
        yield return new WaitForSeconds(.01f);
        cameraObject.transform.position = new Vector3(originalPos.x, originalPos.y, -10);
    }

    //RollOut
    IEnumerator RolloutRoutine(int rolls)
    {

        //Check if the object is paused
        while (GameController.paused)
        {
            yield return null;
        }


        isRolling = true;
        unstoppable = true;
        invulnerable = true;

        GameObject.Find("Ability_Frame").GetComponent<Animator>().Play("Ability_Enter");
        GameObject.Find("Ability_Frame").GetComponent<AudioSource>().Play();
        GameObject.Find("Ability_Frame").transform.Find("Ability_Name").GetComponent<Text>().text = "Royal Rollout";

        //Show the player the boss is about to roll
        slimeAnim.Play("Slime_Roll_Down");
        //Charge Bar
        if (phase1)
        {
            chargeFrame.transform.localScale = new Vector2(.3f, .2f);           
        }
        else if (phase2)
        {
            chargeFrame.transform.localScale = new Vector2(.375f, .25f);        
        }
        else if (phase3)
        {
            chargeFrame.transform.localScale = new Vector2(.5f, .33f);
        }
        else
        {
            
        }

        chargeFrame.SetActive(true);
        chargeMult = 0;

        for (int i = 0; i < 100; i++)
        {
            //Change the charge bar
            float scale = chargeMult / 100f;
            chargeBar.transform.localScale = new Vector2(scale, 1f);
            float offset = -.17f + (.17f * scale);
            chargeBar.transform.localPosition = new Vector2(offset, 0);
            chargeMult += 1;
            yield return new WaitForSeconds(.015f);
        }
        chargeFrame.SetActive(false);

        //Check if the object is paused
        while (GameController.paused)
        {
            yield return null;
        }

        //Execute rolls
        for (int i = 1; i <= rolls; i++)
        {
            //Check if the object is paused
            while (GameController.paused)
            {
                yield return null;
            }

            //determine roll direction, up/down
            Vector2 rollDirection;
            //Down
            if (i % 2 > 0)
            {
                if(Vector2.Angle(player.transform.position, new Vector2(0, -1)) < 65)
                {
                    rollDirection = (player.transform.position - transform.position).normalized * GetRollSpeedFactor();
                    if(rollDirection.y > 0)
                    {
                        rollDirection.y *= -1;
                    }
                }
                else
                {
                    rollDirection = GetRollDirection(235, 305).normalized * GetRollSpeedFactor();
                }
                slimeAnim.Play("Slime_Roll_Down");
                monsterRB.velocity = new Vector2(0, 0);
                rollingDown = true;
                rollingUp = false;
            }
            else
            {
                if (Vector2.Angle(player.transform.position, new Vector2(0, 1)) < 65)
                {
                    rollDirection = (player.transform.position - transform.position).normalized * GetRollSpeedFactor();
                    if (rollDirection.y < 0)
                    {
                        rollDirection.y *= -1;
                    }
                }
                else
                {
                    rollDirection = GetRollDirection(65, 125).normalized * GetRollSpeedFactor();
                }
                slimeAnim.Play("Slime_Roll_Up");
                monsterRB.velocity = new Vector2(0, 0);
                rollingUp = true;
                rollingDown = false;
            }

            for(int j = 0; j < 100; j++)
            {

                //Check if the object is paused
                while (GameController.paused)
                {
                    monsterRB.velocity = new Vector2(0, 0);
                    yield return null;
                }


                monsterRB.velocity = rollDirection.normalized * GetRollSpeedFactor();
                yield return new WaitForSeconds(.1f);

                //check if the boss hits the left/right boundaries

                //right wall
                if (transform.position.x > 2.15)
                {
                    if(rollingUp)
                    {
                        StartCoroutine(ShakeScreenNS(.15f));
                        rollDirection = GetRollDirection(145, 170).normalized;
                    }
                    else
                    {
                        StartCoroutine(ShakeScreenNS(.15f));
                        rollDirection = GetRollDirection(235, 260).normalized;
                    }
                }
                //left wall
                else if(transform.position.x < -2.25)
                {
                    if(rollingUp)
                    {
                        StartCoroutine(ShakeScreenNS(.15f));
                        rollDirection = GetRollDirection(55, 80).normalized;
                    }
                    else
                    {
                        StartCoroutine(ShakeScreenNS(.15f));
                        rollDirection = GetRollDirection(325, 350).normalized;
                    }
                }


                //check if the boss hits the up/down boundaries
                if(phase1 && ((transform.localPosition.y < -1.9f && rollingDown) || (transform.localPosition.y > 0.25f && rollingUp)))
                {
                    monsterRB.velocity = new Vector2(0, 0);
                    StartCoroutine(ShakeScreenNS(.15f));
                    break;
                }
                else if(phase2 && ((transform.localPosition.y < -2f && rollingDown) || (transform.localPosition.y > 0.28f && rollingUp)))
                {
                    monsterRB.velocity = new Vector2(0, 0);
                    StartCoroutine(ShakeScreenNS(.15f));
                    break;
                }
                else if(phase3 && ((transform.localPosition.y < -2f && rollingDown) || (transform.localPosition.y > 0.35f && rollingUp)))
                {
                    monsterRB.velocity = new Vector2(0, 0);
                    StartCoroutine(ShakeScreenNS(.15f));
                    break;
                }
            }
        }

        //adjust size & damage
        if(phase1)
        {
            transform.localScale = new Vector2(4, 4);
            contactDamage = 20;
            rollingUp = false;
            rollingDown = false;
            slimeAnim.Play("Slime_Hurt_1");
            if(transform.localPosition.y < 0)
            {
                //print("DOWN");
                Instantiate(spawnSlimePrefabDown, new Vector2(transform.localPosition.x, transform.localPosition.y + .5f), Quaternion.identity);
                LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].enemyCount += 5;
            }
            else
            {
                //print("UP");
                Instantiate(spawnSlimePrefabUp, new Vector2(transform.localPosition.x, transform.localPosition.y + .5f), Quaternion.identity);
                LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].enemyCount += 5;
            }
            //CheckBounds();
            yield return new WaitForSeconds(2f);
        }
        else if(phase2)
        {
            transform.localScale = new Vector2(3, 3);
            contactDamage = 10;
            rollingUp = false;
            rollingDown = false;
            slimeAnim.Play("Slime_Hurt_1");
            if (transform.localPosition.y < 0)
            {
                Instantiate(spawnSlimePrefabDown, new Vector2(transform.localPosition.x, transform.localPosition.y + .5f), Quaternion.identity);
                LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].enemyCount += 5;
            }
            else
            {
                Instantiate(spawnSlimePrefabUp, new Vector2(transform.localPosition.x, transform.localPosition.y + .5f), Quaternion.identity);
                LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].enemyCount += 5;
            }
            //CheckBounds();
            yield return new WaitForSeconds(2f);
        }
        else if(phase3)
        {
            transform.localScale = new Vector2(2, 2);
            contactDamage = 5;
            rollingUp = false;
            rollingDown = false;
            slimeAnim.Play("Slime_Hurt_1");
            if (transform.localPosition.y < 0)
            {
                Instantiate(spawnSlimePrefabDown, new Vector2(transform.localPosition.x, transform.localPosition.y + .5f), Quaternion.identity);
                LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].enemyCount += 5;
            }
            else
            {
                Instantiate(spawnSlimePrefabUp, new Vector2(transform.localPosition.x, transform.localPosition.y + .5f), Quaternion.identity);
                LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].enemyCount += 5;
            }
            //CheckBounds();
            yield return new WaitForSeconds(2f);
        }
        unstoppable = false;
        invulnerable = false;
        isRolling = false;
    }

    Vector2 GetRollDirection(float angle1, float angle2)
    {
        Vector2 rollDirection = new Vector2(0, 0);
        float randomAngle = Random.Range(angle1, angle2);
        float radians = randomAngle * (Mathf.PI / 180);
        rollDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;
        return rollDirection;
    }

    Vector2 GetRollDirectionNoRandom(float angle1)
    {
        Vector2 rollDirection = new Vector2(0, 0);
        float radians = angle1 * (Mathf.PI / 180);
        rollDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;
        return rollDirection;
    }

    Vector2 GetRollDirectionNew(bool rollingUp)
    {
        Vector2 rollDirection = new Vector2(0, 0);
        if(rollingUp)
        {
            Vector2 newDir = new Vector2(Random.Range(-2.15f, 2.15f),1.2f);
            Vector2 bossPos = new Vector2(transform.position.x, transform.position.y);
            rollDirection = (bossPos - newDir).normalized;
        }
        else
        {
            Vector2 newDir = new Vector2(Random.Range(-2.15f, 2.15f), -1.4f);
            Vector2 bossPos = new Vector2(transform.position.x, transform.position.y);
        }
        return rollDirection;
    }

    float GetAnglePlayer()
    {
        float returnAngle = 0;

        returnAngle = Vector2.Angle(transform.position, TestCharController.player.position);

        return returnAngle;
    }


    //This helper function returns an integer that will modify the rollout speed
    float GetRollSpeedFactor()
    {
        float returnInt = 1;
        if(phase1)
        {
            returnInt = 1.5f;
        }
        else if(phase2)
        {
            returnInt = 2f;
        }
        else if(phase3)
        {
            returnInt = 2.5f;
        }
        return returnInt;
    }

    //Ground pound
    IEnumerator GroundPound()
    {
        //Check if the object is paused
        while (GameController.paused)
        {
            monsterRB.velocity = new Vector2(0, 0);
            yield return null;
        }
        GameObject.Find("Ability_Frame").GetComponent<Animator>().Play("Ability_Enter");
        GameObject.Find("Ability_Frame").GetComponent<AudioSource>().Play();
        GameObject.Find("Ability_Frame").transform.Find("Ability_Name").GetComponent<Text>().text = "Kingly Crash";

        slimeAnim.Play("Slime_GPound");
        yield return new WaitForSeconds(1f);
    }

    //Move the Slime King
    void MoveRoutine()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        //Decide which direction to go

        //Random direction
        if (distance >= 4.5f)
        {
            int tempInt = Random.Range(1, 5);
            if (tempInt == 1)
            {
                newDir = new Vector2(1, 0);
                monsterRB.velocity = newDir;
            }
            else if (tempInt == 2)
            {
                newDir = new Vector2(-1, 0);
                monsterRB.velocity = newDir;
            }
            else if (tempInt == 3)
            {
                newDir = new Vector2(0, 1);
                monsterRB.velocity = newDir;
            }
            else if (tempInt == 4)
            {
                newDir = new Vector2(0, -1);
                monsterRB.velocity = newDir * 10;
            }
        }
        //Towards player
        else
        {
            newDir = (player.transform.position - transform.position).normalized;
            monsterRB.velocity = newDir;
        }

        float angle = Vector3.Angle(newDir, defaultDir);
        if (angle <= 90 || angle > 270)
        {
            slimeAnim.Play("Slime_Jump_2");
        }
        else
        {
            slimeAnim.Play("Slime_Jump_1");
        }

    }

    void CheckBounds()
    {
        if(transform.position.y > 1.30)
        {
            transform.position = new Vector2(transform.position.x, 1.3f);
        }
        else if(transform.position.y < -.89)
        {
            transform.position = new Vector2(transform.position.x, -0.89f);
        }
    }

    IEnumerator StartRoutine()
    {
        yield return new WaitForEndOfFrame();
        treasure.SetActive(false);
        bossBarText.text = "Slime King";
        bossBarFrame.SetActive(true);
        bossBar.SetActive(true);
        GameObject.Find("BGM").GetComponent<AudioSource>().Stop();

        
        TestCharController.inDialogue = true;
        GetComponent<AudioSource>().clip = whistleNoise;
        GetComponent<AudioSource>().Play();
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        transform.Find("Crown").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        transform.Find("Shadow").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(4.25f);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        transform.Find("Crown").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        transform.Find("Shadow").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        yield return StartCoroutine(ShakeScreenNS(.75f));
        TestCharController.inDialogue = false;
        GetComponent<Animator>().Play("Slime_Idle");
        bossWipe.SetActive(true);
        TestCharController.inDialogue = true;
        yield return new WaitForSeconds(2f);
        Camera.main.orthographicSize = 1.828043f;
        Camera.main.transform.position = new Vector3(0, 0, -10);
        TestCharController.inDialogue = false;
        behaviorRoutine = StartCoroutine(SlimeKingBehavior());
        bossWipe.SetActive(false);
        GameObject.Find("BGM").GetComponent<AudioSource>().clip = bossMusic;
        GameObject.Find("BGM").GetComponent<AudioSource>().Play();
    }

}





   



