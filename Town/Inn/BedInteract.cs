using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BedInteract : MonoBehaviour
{
    //The Canvas object prefab for the interact text
    public GameObject interactTextPrefab;
    public GameObject interactText;
    public bool textMade = false;

    public GameObject fadeOut;
    public bool fading = false;
    public AudioClip bgm;

    public string sceneName;
    public float playerX, playerY;
    public string startTag;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float distance = Vector3.Distance(transform.position, TestCharController.player.transform.position);
        //print(distance);
        if (distance < .5 && !textMade)
        {
            interactText = Instantiate(interactTextPrefab, new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity);
            textMade = true;
        }
        else if (distance >= .5)
        {
            Destroy(interactText);
            textMade = false;
        }
        if (distance < .5 && Input.GetKeyDown(KeyCode.F) && !fading)
        {
            StartCoroutine(HeroSoulFade());
        }

    }

    IEnumerator HeroSoulFade()
    {
        fading = true;
        TestCharController.inDialogue = true;
        fadeOut.GetComponent<Animator>().Play("FadeOut");
        yield return new WaitForSeconds(4f);

        Soul_Controller.bgm = GameObject.Find("BGM").GetComponent<AudioSource>().clip;

        GameObject.Find("BGM").GetComponent<AudioSource>().clip = bgm;
        GameObject.Find("BGM").GetComponent<AudioSource>().Play();

        SceneManager.LoadScene("HeroSoul");
        Soul_Controller.sceneName = sceneName;
        Soul_Controller.playerX = playerX;
        Soul_Controller.playerY = playerY;
        Soul_Controller.startTag = startTag;

        TestCharController.inDialogue = false;
    }
}
