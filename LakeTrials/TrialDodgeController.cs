using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TrialDodgeController : MonoBehaviour
{
    public GameObject[] dummies = new GameObject[6];

    public Text hitText;
    public static int hitCount = 0;

    public Text startTimer;

    public float timer = 3;
    public Text timerText;

    public GameObject testRope;
    public GameObject dialogue;
    public GameObject dialogueImg;

    public Coroutine timerRoutine;
    public Coroutine shootRoutine;

    public bool failed = false;
    public bool passed = false;

    // Use this for initialization
    void Start ()
    {
        timer = 99;
	}
	
	// Update is called once per frame
	void Update ()
    {
        hitText.text = "";
		for(int i = 0; i < hitCount; i++)
        {
            if(i < 3)
            {
                hitText.text = hitText.text + "X";
            }
        }

        if(hitCount >= 3 && !failed)
        {
            failed = true;
            StartCoroutine(Failed());
            StopCoroutine(timerRoutine);
            StopCoroutine(shootRoutine);
        }

        if(timer <= 0 && !passed)
        {
            passed = true;
            StartCoroutine(Passed());
            StopCoroutine(timerRoutine);
            StopCoroutine(shootRoutine);
        }
	}

    public IEnumerator StartRoutine()
    {
        hitCount = 0;
        startTimer.gameObject.SetActive(true);
        startTimer.text = "Ready?";
        yield return new WaitForSeconds(1f);
        startTimer.text = "3";
        GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1f);
        startTimer.text = "2";
        GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1f);
        startTimer.text = "1";
        GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1f);
        startTimer.text = "Go!";
        GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1f);
        startTimer.text = "";
        timerRoutine = StartCoroutine(TimerRoutine());
        shootRoutine = StartCoroutine(ShootRoutine());
    }

    IEnumerator TimerRoutine()
    {
        hitText.gameObject.SetActive(true);
        timer = 30;
        timerText.gameObject.SetActive(true);
        while(timer > 0)
        {
            yield return new WaitForSeconds(.1f);
            timer -= .1f;
            timerText.text = timer.ToString("F1");
        }
        timerText.gameObject.SetActive(false);
    }

    IEnumerator ShootRoutine()
    {
        while(timer > 0)
        {
            yield return new WaitForSeconds(Random.Range(1, 2));
            Instantiate(Resources.Load("Prefabs/Enemies/EnemyAttack/Dummy_Missile"), dummies[Random.Range(0, dummies.Length)].transform.position, Quaternion.identity);
        }
    }

    IEnumerator Failed()
    {
        hitText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        GameObject.Find("FailNoise").GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(3f);
        dialogue.SetActive(true);
        dialogueImg.SetActive(true);
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("You got hit too many times, talk to me to try again.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Byron", .9f, -1));
        dialogue.GetComponent<DialogueController>().StartDialogue();
        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }
        testRope.SetActive(false);
    }

    IEnumerator Passed()
    {
        hitText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        GameObject.Find("PassNoise").GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(3f);
        dialogue.SetActive(true);
        dialogueImg.SetActive(true);
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Congratulations you pass. You can talk to me again if you want to try this trial again.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Byron", .9f, -1));
        dialogue.GetComponent<DialogueController>().StartDialogue();
        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }
        testRope.SetActive(false);
        Byron3.testPassed = true;
        GameObject.Find("RoomDone").GetComponent<AudioSource>().Play();
    }
}
