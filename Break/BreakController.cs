using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BreakController : MonoBehaviour
{
    public bool skill1Enabled = false;
    public bool skill2Enabled = false;
    public bool skill3Enabled = false;

    public GameObject skill1Icon;
    public GameObject skill2Icon;
    public GameObject skill3Icon;

    public GameObject ceciliaWipe;
    public GameObject leonWipe;
    public GameObject risetteWipe;
    public GameObject sparrowWipe;

    public bool draining = false;

    public float volume;
    public float clipTime;
    public AudioClip bgmClip;
    public AudioClip breakClip;

    public GameObject breakFlash;
    public GameObject breakPrefab;
	// Use this for initialization
	void Start ()
    {
        CheckIcon();
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckSkill();
        CheckIcon();

		if((Input.GetMouseButton(1) || InputManager.J_Trigger() > 0) && skill1Enabled && !draining)
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            skill1Icon.GetComponent<Image>().color = new Color32(150, 150, 150, 255);
            StartCoroutine(DrainBreak(100));
        }
        else if(!Input.GetMouseButton(1) && skill1Enabled)
        {
            skill1Icon.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }

        if (Input.GetKeyDown(KeyCode.X) && skill2Enabled && !draining)
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            skill2Icon.GetComponent<Image>().color = new Color32(150, 150, 150, 255);
            StartCoroutine(DrainBreak(200));
        }
        else if(!Input.GetKey(KeyCode.X) && skill2Enabled)
        {
            skill2Icon.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }

        if (Input.GetKeyDown(KeyCode.C) && skill3Enabled && !draining)
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            skill3Icon.GetComponent<Image>().color = new Color32(150, 150, 150, 255);
            StartCoroutine(DrainBreak(300));
        }
        else if (!Input.GetKey(KeyCode.C) && skill3Enabled)
        {
            skill3Icon.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    void CheckSkill()
    {
        if (PlayerStats.breakMeter >= 100)
        {
            skill1Enabled = true;
        }
        else
        {
            skill1Enabled = false;
        }
        
        if(PlayerStats.breakMeter >= 200)
        {
            skill2Enabled = true;
        }
        else
        {
            skill2Enabled = false;
        }

        if(PlayerStats.breakMeter >= 300)
        {
            skill3Enabled = true;
        }
        else
        {
            skill3Enabled = false;
        }

    }

    void CheckIcon()
    {
        if (skill1Enabled)
        {
            skill1Icon.SetActive(true);
        }
        else
        {
            skill1Icon.SetActive(false);
        }

        if (skill2Enabled)
        {
            skill2Icon.SetActive(true);
        }
        else
        {
            skill2Icon.SetActive(false);
        }

        if (skill3Enabled)
        {
            skill3Icon.SetActive(true);
        }
        else
        {
            skill3Icon.SetActive(false);
        }
    }

    void PlayHeroWipe(int id)
    {
        switch(id)
        {
            default:
                break;
            case 1:
                ceciliaWipe.SetActive(true);
                break;
            case 2:
                leonWipe.SetActive(true);
                break;
            case 3:
                risetteWipe.SetActive(true);
                break;
            case 4:
                sparrowWipe.SetActive(true);
                break;
        }
    }

    void DisableHeroWipe()
    {
        ceciliaWipe.SetActive(false);
        leonWipe.SetActive(false);
        risetteWipe.SetActive(false);
        sparrowWipe.SetActive(false);
    }

    IEnumerator DrainBreak(int amount)
    {
        TestCharController.breakInvuln = true;
        TestCharController.inDialogue = true;

        GameController.paused = true;

        draining = true;
        PlayHeroWipe(1);

        bgmClip = GameObject.Find("BGM").GetComponent<AudioSource>().clip;



        volume = GameObject.Find("BGM").GetComponent<AudioSource>().volume;
        while (volume > 0)
        {
            volume -= .005f;
            GameObject.Find("BGM").GetComponent<AudioSource>().volume = volume;
            yield return new WaitForSeconds(.1f);
        }
        clipTime = GameObject.Find("BGM").GetComponent<AudioSource>().time;
        GameObject.Find("BGM").GetComponent<AudioSource>().clip = breakClip;
        GameObject.Find("BGM").GetComponent<AudioSource>().Play();
        GameObject.Find("BGM").GetComponent<AudioSource>().time = 0;
        GameObject.Find("BGM").GetComponent<AudioSource>().volume = .1f;

        //yield return new WaitForSeconds(2f);
        DisableHeroWipe();
        GetComponent<AudioSource>().Play();

        //Determine which break skill to use

        //Harrier Dash
        StartCoroutine(TestCharController.player.GetComponent<TestCharController>().HarrierDash(bgmClip, clipTime));
        
        for (int i = 0; i < amount; i++)
        {
            yield return new WaitForSeconds(.015f);
            PlayerStats.breakMeter--;
        }
        draining = false;
    }
}
