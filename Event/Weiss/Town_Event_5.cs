using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town_Event_5 : MonoBehaviour
{
    public static bool start;

    public GameObject dialogueController;
    public GameObject dialogueIMG;

    public GameObject wipe;

    public GameObject enemy;

    public GameObject fog;

    public float volume;
    public AudioClip bossBGM;

    public GameObject bossWipe;

    public GameObject laugh;

    public GameObject boss;

    public GameObject player;

    public GameObject orb;

    // Use this for initialization
    void Start ()
    {
		if(start)
        {
            orb.SetActive(true);
            enemy.SetActive(true);
            boss.SetActive(true);
            player.transform.position = new Vector2(0,-1.3f);
            player.GetComponent<Animator>().Play("TestUpIdle");
            wipe.SetActive(true);
            fog.SetActive(true);
            StartCoroutine(TalkRoutine());
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator FadeOutRoutine()
    {
        volume = GameObject.Find("BGM").GetComponent<AudioSource>().volume;
        while (volume > 0)
        {
            volume -= .01f;
            GameObject.Find("BGM").GetComponent<AudioSource>().volume = volume;
            yield return new WaitForSeconds(.1f);
        }
    }

    IEnumerator TalkRoutine()
    {
        StartCoroutine(FadeOutRoutine());
        dialogueController.SetActive(true);
        dialogueIMG.SetActive(true);
        boss.GetComponent<Animator>().Play("M_NPC_Up_Idle");
        dialogueController.GetComponent<DialogueController>().Clear();
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Kukuku... Now this is unexpected. It appears I have an guest.", "Count/Count Beaumont_thigh up", "Count Beaumont", 0.9f));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("What's going on here? Is that life Arcana?", "Cecilia/Cecilia Grey_thigh_2", "Cecilia", 1f));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Bing bong, correct♥!", "Count/Count Beaumont_thigh up", "Count Beaumont", 0.9f));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("But how did you get so much? Y-you... it can't be...", "Cecilia/Cecilia Grey_thigh_2", "Cecilia", 1f));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("*Sigh* Monsters only can give so much life arcana, yet people are overflowing with life.", "Count/Count Beaumont_thigh up", "Count Beaumont", 0.9f));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("So you're the one that let the monsters into town, and hurt all those people!", "Cecilia/Cecilia Grey_thigh_3", "Cecilia", 1f));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Correct♥! Allow me to introduce myself, I am Count Beaumont travelling artist, and welcome to my dazzling art exhibition!", "Count/Count Beaumont_thigh up", "Count Beaumont", 0.9f));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Art? Innocent people are getting hurt! How can you call that art?", "Cecilia/Cecilia Grey_thigh_3", "Cecilia", 1f));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Kukuku... Suffering... Fear... Anger... THIS IS MY ART! And YOU, overflowing with life will make the perfect Piece de Resistance! Say, what is your name so I can savor this moment.", "Count/Count Beaumont_thigh up", "Count Beaumont", 0.9f));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Cecilia Grey, junior member of the adventurer guild! I'm going to stop you no matter what!", "Cecilia/Cecilia Grey_thigh_3", "Cecilia", 1f));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Kukuku... Your determination is DELIGHTFUL! Let the dance of combat BEGIN!", "Count/Count Beaumont_thigh up", "Count Beaumont", 0.9f));
        dialogueController.GetComponent<DialogueController>().StartDialogue();

        while (!dialogueController.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }

        TestCharController.inDialogue = true;
        laugh.SetActive(true);
        yield return new WaitForSeconds(1f);
        GameObject.Find("BGM").GetComponent<AudioSource>().clip = bossBGM;
        GameObject.Find("BGM").GetComponent<AudioSource>().volume = .1f;
        GameObject.Find("BGM").GetComponent<AudioSource>().Play();
        bossWipe.SetActive(true);

        yield return new WaitForSeconds(2f);

        for (float i = 0; i < .25f; i += .05f)
        {
            boss.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, (1f - (i * 4)));
            yield return new WaitForSeconds(.05f);
        }
        TestCharController.inDialogue = false;
        boss.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        boss.transform.position = new Vector2(0, 0.875f);
        boss.GetComponent<Animator>().Play("M_NPC_Down_Idle");
        CountController.start = true;
    }
}
