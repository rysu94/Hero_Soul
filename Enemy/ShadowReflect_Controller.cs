using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShadowReflect_Controller : Monster
{
    //0 down 1 up 2 left 3 right
    public int direction = 0;
    public bool faded;

    public GameObject coneTelegraph, lineTelegraph, circleTelegraph;
    public GameObject attackNoise;

    public Text bossBarText;
    public GameObject bossBarFrame;
    public GameObject bossBar;

    public float bossBarWidth = 201.4f;
    public float monsterMaxHealth = 2100;

    public GameObject bossWipe;
    public AudioClip bossMusic;
    public AudioClip normalMusic;

    public List<GameObject> cloudList = new List<GameObject>();
    public List<GameObject> darkCrystal = new List<GameObject>();
    public List<GameObject> workers = new List<GameObject>();

    //Charge Bar
    public float chargeMult;
    public GameObject chargeBar, chargeFrame;


    //Boss Phases
    public int phaseNum = 0;

    // Use this for initialization
    void Start ()
    {
        monsterName = "Shadowy Reflection";
        boss = true;

        monsterHealth = 2100;
        contactDamage = 25;

        player = GameObject.FindGameObjectWithTag("Player");
        monsterSprite = GetComponent<SpriteRenderer>();
        monsterRB = GetComponent<Rigidbody2D>();

        //Hard Carapace
        monsterDrops.Add(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[37]);
        monsterDropChance.Add(333);

        //Arcana [fire, water, earth, air, life]
        arcanaDrop.Add(0);
        arcanaDrop.Add(0);
        arcanaDrop.Add(0);
        arcanaDrop.Add(0);
        arcanaDrop.Add(0);

        experienceDrop = 10;
        unstoppable = true;
        //invulnerable = true;
        StartCoroutine(StartRoutine());
    }
	
	// Update is called once per frame
	void Update ()
    {
        float healthPercent = (float)monsterHealth / (float)monsterMaxHealth;
        var xpBarRect = bossBar.transform as RectTransform;
        xpBarRect.sizeDelta = new Vector2(bossBarWidth * healthPercent, xpBarRect.sizeDelta.y);

        if (monsterHealth <= 0 && !GameController.paused)
        {
            bossBarFrame.SetActive(false);
            GameObject.Find("BGM").GetComponent<AudioSource>().clip = normalMusic;
            GameObject.Find("BGM").GetComponent<AudioSource>().Play();
        }

        if (unstoppable)
        {
            GetComponent<CircleCollider2D>().enabled = false;
        }
        else
        {
            GetComponent<CircleCollider2D>().enabled = true;
        }

        //Check health
        if (monsterHealth <= 1400 && phaseNum == 1)
        {
            phaseNum++;
        }
        else if(monsterHealth <= 700 && phaseNum == 4)
        {
            phaseNum++;
        }





    }

    IEnumerator BehaviorRoutine()
    {
        while(monsterHealth > 0)
        {

            //Check Phases

            //Spawn 1 cloud
            if (phaseNum == 0)
            {
                yield return StartCoroutine(ExecuteWorkersPhase1());
                phaseNum++;
                GameObject tempObj = Instantiate(Resources.Load("Prefabs/Enemies/Shadow_Cloud"), new Vector2(-2.316f, -0.315f), Quaternion.identity) as GameObject;
                Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/Cloud_Spawn"), tempObj.transform);
                cloudList.Add(tempObj);
            }


            // Teleport to Middle
            else if(phaseNum == 2)
            {
                invulnerable = true;
                if(!faded)
                {
                    yield return StartCoroutine(FadeOutRoutine());
                    yield return new WaitForSeconds(.5f);
                }
                yield return StartCoroutine(MoveMidRoutine());          
            }

            //Spawn 2 Clouds
            else if(phaseNum == 3)
            {
                cloudList.Clear();
                yield return StartCoroutine(ExecuteWorkersPhase2());
                phaseNum++;
                GameObject tempObj = Instantiate(Resources.Load("Prefabs/Enemies/Shadow_Cloud"), new Vector2(-1.57f, 1.173f), Quaternion.identity) as GameObject;
                Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/Cloud_Spawn"), tempObj.transform);
                cloudList.Add(tempObj);

                tempObj = Instantiate(Resources.Load("Prefabs/Enemies/Shadow_Cloud"), new Vector2(2.36f, -0.983f), Quaternion.identity) as GameObject;
                Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/Cloud_Spawn"), tempObj.transform);
                cloudList.Add(tempObj);

                faded = false;
                invulnerable = false;
            }
            //Teleport to middle
            else if(phaseNum == 5)
            {
                invulnerable = true;
                if (!faded)
                {
                    yield return StartCoroutine(FadeOutRoutine());
                    yield return new WaitForSeconds(.5f);
                }
                yield return StartCoroutine(MoveMidRoutine());
            }
            else if(phaseNum == 6)
            {
                cloudList.Clear();
                yield return StartCoroutine(ExecuteWorkersPhase3());
                phaseNum++;
                GameObject tempObj = Instantiate(Resources.Load("Prefabs/Enemies/Shadow_Cloud"), new Vector2(-2.324f, 0.454f), Quaternion.identity) as GameObject;
                Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/Cloud_Spawn"), tempObj.transform);
                cloudList.Add(tempObj);

                tempObj = Instantiate(Resources.Load("Prefabs/Enemies/Shadow_Cloud"), new Vector2(0.685f, 1.194f), Quaternion.identity) as GameObject;
                Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/Cloud_Spawn"), tempObj.transform);
                cloudList.Add(tempObj);

                tempObj = Instantiate(Resources.Load("Prefabs/Enemies/Shadow_Cloud"), new Vector2(2.345f, 0.489f), Quaternion.identity) as GameObject;
                Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/Cloud_Spawn"), tempObj.transform);
                cloudList.Add(tempObj);

                faded = false;
                invulnerable = false;
            }
            //Teleport to middle
            else if (phaseNum == 8)
            {
                invulnerable = true;
                if (!faded)
                {
                    yield return StartCoroutine(FadeOutRoutine());
                    yield return new WaitForSeconds(.5f);
                }
                yield return StartCoroutine(MoveMidRoutine());
            }

            //Determine distance of the player
            float distance = Vector2.Distance(transform.position, player.transform.position);
            //print(distance);

            if(distance > .55f)
            {
                monsterRB.constraints = ~RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
                Vector2 dir;
                if (!faded)
                {
                    dir = (player.transform.position - transform.position).normalized * 1.25f;
                }
                else
                {
                    dir = (player.transform.position - transform.position).normalized * .5f;
                    Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/Shadow_Puddle"), new Vector2(transform.position.x, transform.position.y - .2f), Quaternion.identity);
                }
                
                monsterRB.velocity = dir;

                //Determine Animation to Play
                if(Mathf.Abs(dir.x) > Mathf.Abs(dir.y) && !faded)
                {
                    if(dir.x > 0)
                    {
                        GetComponent<Animator>().Play("Shadow_Run_Right");           
                    }
                    else if(dir.x < 0)
                    {
                        GetComponent<Animator>().Play("Shadow_Run_Left");
                    }
                }
                else if(Mathf.Abs(dir.y) >= Mathf.Abs(dir.x) && !faded)
                {
                    if(dir.y > 0)
                    {
                        GetComponent<Animator>().Play("Shadow_Run_Up");
                    }
                    else if(dir.y < 0)
                    {                       
                        GetComponent<Animator>().Play("Shadow_Run_Down");
                    }
                }
            }
            else
            {
                monsterRB.velocity = new Vector2(0, 0);
                //Swing or Charge? 1 Swing  2 Charge
                int determinedAction = Random.Range(0, 3);

                //Determine Facing Angle
                Vector2 dir = (player.transform.position - transform.position).normalized;
                //print(dir);

                //Turn Left
                if(dir.x < -.7f && dir.y > -.7f && dir.y < .7f)
                {

                    //Unfade?
                    if(faded)
                    {
                        faded = false;
                        Unfade(2);
                        yield return new WaitForSeconds(.5f);
                    }

                    GetComponent<Animator>().Play("Shadow_Charge_Idle_Left");
                    direction = 2;
                    if (determinedAction == 0)
                    {
                        coneTelegraph.SetActive(true);
                        coneTelegraph.transform.rotation = Quaternion.Euler(0, 0, 270);
                        yield return new WaitForSeconds(1f);
                        GetComponent<Animator>().Play("Shadow_Jab_Left");
                        monsterRB.velocity = new Vector2(-.1f, 0);
                        yield return new WaitForSeconds(.3f);
                        monsterRB.velocity = new Vector2(-.1f, 0);
                        yield return new WaitForSeconds(.3f);
                        monsterRB.velocity = new Vector2(-.1f, 0);
                        yield return new WaitForSeconds(.3f);
                    }
                    else if(determinedAction == 1)
                    {
                        circleTelegraph.SetActive(true);
                        yield return new WaitForSeconds(1f);
                        GetComponent<Animator>().Play("Shadow_Spin");
                        yield return new WaitForSeconds(.25f);
                    }
                    else
                    {
                        lineTelegraph.SetActive(true);
                        lineTelegraph.transform.rotation = Quaternion.Euler(0, 0, 270);
                        yield return new WaitForSeconds(1f);
                        GetComponent<Animator>().Play("Shadow_Charge_Left");
                        monsterRB.velocity = new Vector2(-3, 0);
                        yield return new WaitForSeconds(.5f);
                    }             
                }
                //Turn Up
                else if(dir.x > -.7f && dir.x < .7f && dir.y > .7f)
                {
                    //Unfade?
                    if (faded)
                    {
                        faded = false;
                        Unfade(1);
                        yield return new WaitForSeconds(.5f);
                    }
                    GetComponent<Animator>().Play("Shadow_Charge_Idle_Up");
                    direction = 1;
                    if (determinedAction == 0)
                    {
                        coneTelegraph.SetActive(true);
                        coneTelegraph.transform.rotation = Quaternion.Euler(0, 0, 180);
                        yield return new WaitForSeconds(1f);
                        GetComponent<Animator>().Play("Shadow_Jab_Up");
                        monsterRB.velocity = new Vector2(0, .1f);
                        yield return new WaitForSeconds(.3f);
                        monsterRB.velocity = new Vector2(0, .1f);
                        yield return new WaitForSeconds(.3f);
                        monsterRB.velocity = new Vector2(0, .1f);
                        yield return new WaitForSeconds(.3f);
                    }
                    else if (determinedAction == 1)
                    {
                        circleTelegraph.SetActive(true);
                        yield return new WaitForSeconds(1f);
                        GetComponent<Animator>().Play("Shadow_Spin");
                        yield return new WaitForSeconds(.25f);
                    }
                    else
                    {
                        lineTelegraph.SetActive(true);
                        lineTelegraph.transform.rotation = Quaternion.Euler(0, 0, 180);
                        yield return new WaitForSeconds(1f);
                        GetComponent<Animator>().Play("Shadow_Charge_Up");
                        monsterRB.velocity = new Vector2(0, 3);
                        yield return new WaitForSeconds(.5f);    
                    }
                }
                //Turn Right
                else if(dir.x > .7f && dir.y > -.7f && dir.y < .7f)
                {
                    //Unfade?
                    if (faded)
                    {
                        faded = false;
                        Unfade(3);
                        yield return new WaitForSeconds(.5f);
                    }
                    GetComponent<Animator>().Play("Shadow_Charge_Idle_Right");
                    direction = 3;
                    if (determinedAction == 0)
                    {
                        coneTelegraph.SetActive(true);
                        coneTelegraph.transform.rotation = Quaternion.Euler(0, 0, 90);
                        yield return new WaitForSeconds(1f);
                        GetComponent<Animator>().Play("Shadow_Jab_Right");
                        monsterRB.velocity = new Vector2(.1f, 0);
                        yield return new WaitForSeconds(.3f);
                        monsterRB.velocity = new Vector2(.1f, 0);
                        yield return new WaitForSeconds(.3f);
                        monsterRB.velocity = new Vector2(.1f, 0);
                        yield return new WaitForSeconds(.3f);
                    }
                    else if (determinedAction == 1)
                    {
                        circleTelegraph.SetActive(true);
                        yield return new WaitForSeconds(1f);
                        GetComponent<Animator>().Play("Shadow_Spin");
                        yield return new WaitForSeconds(.25f);
                    }
                    else
                    {
                        lineTelegraph.SetActive(true);
                        lineTelegraph.transform.rotation = Quaternion.Euler(0, 0, 90);
                        yield return new WaitForSeconds(1f);
                        GetComponent<Animator>().Play("Shadow_Charge_Right");
                        monsterRB.velocity = new Vector2(3, 0);
                        yield return new WaitForSeconds(.5f);
                    }
                }
                //Turn Down
                else if(dir.x > -.7f && dir.x < .7f && dir.y < -.7f)
                {
                    //Unfade?
                    if (faded)
                    {
                        faded = false;
                        Unfade(0);
                        yield return new WaitForSeconds(.5f);
                    }
                    GetComponent<Animator>().Play("Shadow_Charge_Idle_Down");
                    direction = 0;
                    if (determinedAction == 0)
                    {
                        coneTelegraph.SetActive(true);
                        coneTelegraph.transform.rotation = Quaternion.Euler(0, 0, 0);
                        yield return new WaitForSeconds(1f);
                        GetComponent<Animator>().Play("Shadow_Jab_Down");
                        monsterRB.velocity = new Vector2(0, -.1f);
                        yield return new WaitForSeconds(.3f);
                        monsterRB.velocity = new Vector2(0, -.1f);
                        yield return new WaitForSeconds(.3f);
                        monsterRB.velocity = new Vector2(0, -.1f);
                        yield return new WaitForSeconds(.3f);
                    }
                    else if (determinedAction == 1)
                    {
                        circleTelegraph.SetActive(true);
                        yield return new WaitForSeconds(1f);
                        GetComponent<Animator>().Play("Shadow_Spin");
                        yield return new WaitForSeconds(.25f);
                    }
                    else
                    {
                        lineTelegraph.SetActive(true);
                        lineTelegraph.transform.rotation = Quaternion.Euler(0, 0, 0);
                        yield return new WaitForSeconds(1f);
                        GetComponent<Animator>().Play("Shadow_Charge_Down");
                        monsterRB.velocity = new Vector2(0, -3);
                        yield return new WaitForSeconds(.5f);
                    }
                }
                yield return new WaitForSeconds(.2f);

                distance = Vector2.Distance(transform.position, player.transform.position);
                if(distance > .55f)
                {
                    faded = true;
                    invulnerable = true;
                    switch (direction)
                    {
                        default:
                            break;
                        //down
                        case 0:
                            Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/ShadowStep"), transform);
                            GetComponent<Animator>().Play("Shadow_Fade");
                            yield return new WaitForSeconds(1f);
                            break;
                        //up
                        case 1:
                            Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/ShadowStep"), transform);
                            GetComponent<Animator>().Play("Shadow_Fade_Up");
                            yield return new WaitForSeconds(1f);
                            break;
                        //left
                        case 2:
                            Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/ShadowStep"), transform);
                            GetComponent<Animator>().Play("Shadow_Fade_Left");
                            yield return new WaitForSeconds(1f);
                            break;
                        //right
                        case 3:
                            Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/ShadowStep"), transform);
                            GetComponent<Animator>().Play("Shadow_Fade_Right");
                            yield return new WaitForSeconds(1f);
                            break;
                    }
                }
                
            }
            yield return new WaitForSeconds(.1f);

        }

    }

    IEnumerator FadeOutRoutine()
    {
        switch (direction)
        {
            default:
                break;
            //down
            case 0:
                Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/ShadowStep"), transform);
                GetComponent<Animator>().Play("Shadow_Fade");
                yield return new WaitForSeconds(1f);
                break;
            //up
            case 1:
                Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/ShadowStep"), transform);
                GetComponent<Animator>().Play("Shadow_Fade_Up");
                yield return new WaitForSeconds(1f);
                break;
            //left
            case 2:
                Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/ShadowStep"), transform);
                GetComponent<Animator>().Play("Shadow_Fade_Left");
                yield return new WaitForSeconds(1f);
                break;
            //right
            case 3:
                Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/ShadowStep"), transform);
                GetComponent<Animator>().Play("Shadow_Fade_Right");
                yield return new WaitForSeconds(1f);
                break;
        }
    }

    IEnumerator MoveMidRoutine()
    {
        faded = false;
        //Move to Center
        float dist = Vector2.Distance(transform.position, new Vector2(0, 0));
        while (dist > .25f)
        {
            dist = Vector2.Distance(transform.position, new Vector2(0, 0));
            monsterRB.velocity = (new Vector2(0, 0) - (Vector2)transform.position).normalized * .5f;
            yield return new WaitForSeconds(.1f);
        }
        monsterRB.velocity = new Vector2(0, 0);
        transform.position = new Vector2(0, 0);
        Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/ShadowStep"), transform);
        GetComponent<Animator>().Play("Shadow_Weak");
        yield return new WaitForSeconds(.25f);
        GameObject tempObj = Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/Dark_Shield"), transform) as GameObject;

        GameObject.Find("Ability_Frame").GetComponent<Animator>().Play("Ability_Enter");
        GameObject.Find("Ability_Frame").GetComponent<AudioSource>().Play();
        GameObject.Find("Ability_Frame").transform.Find("Ability_Name").GetComponent<Text>().text = "Forbidden Ritual";

        darkCrystal.Clear();
        darkCrystal.Add(Instantiate(Resources.Load("Prefabs/Enemies/Dark_Gem"), new Vector2(transform.position.x + 1f, transform.position.y), Quaternion.identity) as GameObject);
        darkCrystal.Add(Instantiate(Resources.Load("Prefabs/Enemies/Dark_Gem"), new Vector2(transform.position.x - 1f, transform.position.y), Quaternion.identity) as GameObject);
        darkCrystal.Add(Instantiate(Resources.Load("Prefabs/Enemies/Dark_Gem"), new Vector2(transform.position.x, transform.position.y + 1f), Quaternion.identity) as GameObject);
        LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].enemyCount += 3;

        for(int i = 0; i < darkCrystal.Count; i++)
        {
            Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/ShadowStep"), darkCrystal[i].transform);
        }

        yield return new WaitForSeconds(1f);

        chargeFrame.SetActive(true);
        chargeMult = 0;


        while (monsterHealth < monsterMaxHealth && chargeMult < 100)
        {

            //Change the charge bar
            float scale = chargeMult / 100f;
            chargeBar.transform.localScale = new Vector2(scale, 1f);
            float offset = -.17f + (.17f * scale);
            chargeBar.transform.localPosition = new Vector2(offset, 0);

            chargeMult += .25f * phaseNum;

            int count = 0;
            foreach(GameObject crystal in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if(crystal.GetComponent<DarkCrystal_Controller>())
                {
                    count++;
                }
            }
            if(count == 0)
            {
                break;
            }

            for (int i = 0; i < darkCrystal.Count; i++)
            {
                if(darkCrystal[i].GetComponent<DarkCrystal_Controller>().monsterHealth > 0)
                {
                    GameObject Orb = Instantiate(Resources.Load("Prefabs/Arcana/Dark_Arcana"), new Vector2(darkCrystal[i].transform.position.x + Random.Range(-.15f, .15f), darkCrystal[i].transform.position.y + Random.Range(-.15f, .15f)), Quaternion.identity) as GameObject;
                    Orb.GetComponent<Rigidbody2D>().velocity = (transform.position - Orb.transform.position).normalized * .65f;
                }
                
            }

            monsterHealth += 1 * phaseNum;
            yield return new WaitForSeconds(.1f);
        }

        direction = 0;
        chargeFrame.SetActive(false);
        for(int i = 0; i < cloudList.Count; i++)
        {
            Destroy(cloudList[i]);
        }

        for(int i = 0; i < darkCrystal.Count; i++)
        {
            Destroy(darkCrystal[i]);
        }
        faded = false;

        if(chargeMult >= 100)
        {
            Destroy(tempObj);

            GameObject.Find("Ability_Frame").GetComponent<Animator>().Play("Ability_Enter");
            GameObject.Find("Ability_Frame").GetComponent<AudioSource>().Play();
            GameObject.Find("Ability_Frame").transform.Find("Ability_Name").GetComponent<Text>().text = "Shadow Mimicry";

            yield return new WaitForSeconds(3f);
        }
        else
        {
            Instantiate(Resources.Load("Prefabs/WhiteBox"), new Vector2(0,0), Quaternion.identity);
            Destroy(tempObj);
            invulnerable = false;
            tempObj = Instantiate(Resources.Load("Prefabs/States/Dazed_Prefab"), transform) as GameObject;
            tempObj.AddComponent<Lifespan>().lifespan = 5;
            yield return new WaitForSeconds(5f);
            invulnerable = true;
        }

        
        phaseNum++;
    }

    IEnumerator ExecuteWorkersPhase1()
    {
        invulnerable = true;
        GameObject.Find("Ability_Frame").GetComponent<Animator>().Play("Ability_Enter");
        GameObject.Find("Ability_Frame").GetComponent<AudioSource>().Play();
        GameObject.Find("Ability_Frame").transform.Find("Ability_Name").GetComponent<Text>().text = "Cull the Weak";
        yield return StartCoroutine(FadeOutRoutine());
        yield return new WaitForSeconds(.25f);
        //Execute a worker
        
        transform.position = new Vector2(-1.975f, -0.252f);
        Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/ShadowStep"), transform);
        GetComponent<Animator>().Play("Shadow_Unfade_Left");
        yield return new WaitForSeconds(1f);
        GetComponent<Animator>().Play("Shadow_Slash2_Left");
        yield return new WaitForSeconds(.25f);
        Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/Blood"), workers[0].transform);
        yield return new WaitForSeconds(.5f);
        direction = 2;
        yield return StartCoroutine(FadeOutRoutine());
        yield return new WaitForSeconds(.25f);
        transform.position = new Vector2(0, 0);
        GetComponent<Animator>().Play("Shadow_Unfade_Down");
        Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/ShadowStep"), transform);
        yield return new WaitForSeconds(.25f);
        faded = false;
        invulnerable = false;
    }

    IEnumerator ExecuteWorkersPhase2()
    {
        GameObject.Find("Ability_Frame").GetComponent<Animator>().Play("Ability_Enter");
        GameObject.Find("Ability_Frame").GetComponent<AudioSource>().Play();
        GameObject.Find("Ability_Frame").transform.Find("Ability_Name").GetComponent<Text>().text = "Cull the Weak";
        yield return StartCoroutine(FadeOutRoutine());
        yield return new WaitForSeconds(.25f);
        //Execute a worker

        transform.position = new Vector2(-1.587f, 1.007f);
        Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/ShadowStep"), transform);
        GetComponent<Animator>().Play("Shadow_Unfade_Up");
        yield return new WaitForSeconds(1f);
        GetComponent<Animator>().Play("Shadow_Slash2_Up");
        yield return new WaitForSeconds(.25f);
        Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/Blood"), workers[1].transform);
        yield return new WaitForSeconds(.5f);

        transform.position = new Vector2(2.082f, -0.92f);
        GetComponent<Animator>().Play("Shadow_Slash2_Right");
        yield return new WaitForSeconds(.25f);
        Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/Blood"), workers[2].transform);
        yield return new WaitForSeconds(.5f);

        direction = 3;
        yield return StartCoroutine(FadeOutRoutine());
        yield return new WaitForSeconds(.25f);
        transform.position = new Vector2(0, 0);
        GetComponent<Animator>().Play("Shadow_Unfade_Down");
        Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/ShadowStep"), transform);
        yield return new WaitForSeconds(.25f);
        faded = false;
    }

    IEnumerator ExecuteWorkersPhase3()
    {
        GameObject.Find("Ability_Frame").GetComponent<Animator>().Play("Ability_Enter");
        GameObject.Find("Ability_Frame").GetComponent<AudioSource>().Play();
        GameObject.Find("Ability_Frame").transform.Find("Ability_Name").GetComponent<Text>().text = "Cull the Weak";
        yield return StartCoroutine(FadeOutRoutine());
        yield return new WaitForSeconds(.25f);
        //Execute a worker

        transform.position = new Vector2(-2.029f, 0.522f);
        Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/ShadowStep"), transform);
        GetComponent<Animator>().Play("Shadow_Unfade_Left");
        yield return new WaitForSeconds(1f);
        GetComponent<Animator>().Play("Shadow_Slash2_Left");
        yield return new WaitForSeconds(.25f);
        Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/Blood"), workers[3].transform);
        yield return new WaitForSeconds(.5f);

        transform.position = new Vector2(0.691f, 1.012f);
        GetComponent<Animator>().Play("Shadow_Slash2_Up");
        yield return new WaitForSeconds(.25f);
        Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/Blood"), workers[4].transform);
        yield return new WaitForSeconds(.5f);

        transform.position = new Vector2(2.098f, 0.549f);
        GetComponent<Animator>().Play("Shadow_Slash2_Right");
        yield return new WaitForSeconds(.25f);
        Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/Blood"), workers[5].transform);
        yield return new WaitForSeconds(.5f);

        direction = 3;
        yield return StartCoroutine(FadeOutRoutine());
        yield return new WaitForSeconds(.25f);
        transform.position = new Vector2(0, 0);
        GetComponent<Animator>().Play("Shadow_Unfade_Down");
        Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/ShadowStep"), transform);
        yield return new WaitForSeconds(.25f);
        faded = false;
    }

    public void Unfade(int dir)
    {
        Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/ShadowStep"), transform);
        invulnerable = false;
        switch (dir)
        {
            default:
                break;
            //Down
            case 0:
                GetComponent<Animator>().Play("Shadow_Unfade_Down");
                break;
            //Up
            case 1:
                GetComponent<Animator>().Play("Shadow_Unfade_Up");
                break;
            //Left
            case 2:
                GetComponent<Animator>().Play("Shadow_Unfade_Left");
                break;
            //Right
            case 3:
                GetComponent<Animator>().Play("Shadow_Unfade_Right");
                break;
        }
    }

    IEnumerator StartRoutine()
    {
        yield return new WaitForEndOfFrame();
        bossBarText.text = "Galahad's Shade";
        bossBarFrame.SetActive(true);
        bossBar.SetActive(true);
        GameObject.Find("BGM").GetComponent<AudioSource>().Stop();
        bossWipe.SetActive(true);
        TestCharController.inDialogue = true;
        yield return new WaitForSeconds(3.2f);
        TestCharController.inDialogue = false;
        StartCoroutine(BehaviorRoutine());
        bossWipe.SetActive(false);
        GameObject.Find("BGM").GetComponent<AudioSource>().clip = bossMusic;
        GameObject.Find("BGM").GetComponent<AudioSource>().Play();
    }


    //Overwrite death
    IEnumerator DeathRoutine()
    {
        //Destroy Clouds
        for (int i = 0; i < cloudList.Count; i++)
        {
            Destroy(cloudList[i]);
        }

        //Check if the object is paused
        while (GameController.paused)
        {
            monsterRB.velocity = new Vector2(0, 0);
            yield return null;
        }

        //Add Beastiary entry
        GameObject.Find("BeastiaryManager").GetComponent<Bestiary_Database>().AddEntry(monsterName);


        //Set the collapsing flag to true
        isCollapsing = true;

        //Ends the on-hit color change coroutine and return enemy back to base color
        StopCoroutine("ColorReturnRoutineEnemy");
        player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);

        //Disables the box collider the enemy has
        CircleCollider2D enemyHitbox = GetComponent<CircleCollider2D>();
        enemyHitbox.enabled = false;

        //If player is hit during the collapse, returns them back to normal
        TestCharController.isHit = false;

        //Fades out enemy over time
        float tempColor = 1;
        while (tempColor >= 0)
        {
            yield return new WaitForSeconds(.05f);
            tempColor -= .1f;
            monsterSprite.color = new Color(1f, 1f, 1f, tempColor);
        }

        //Updates the enemy count in the current room, if there are no more enemies open doors
        LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].enemyCount--;
        //print("Enemy ded" + LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].enemyCount);
        if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].enemyCount <= 0)
        {
            LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].roomClear = true;
            AudioSource tempAudio = GameObject.Find("RoomDone").GetComponent<AudioSource>();
            tempAudio.Play();
        }

        //Process Monster's Loot Drops
        ItemDrop();

        //Player Gain XP
        PlayerStats.GainXP(experienceDrop);

        //Disables the enemy
        gameObject.SetActive(false);
    }
}
