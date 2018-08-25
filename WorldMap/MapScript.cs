using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapScript : MonoBehaviour
{
    public static int transition = 1;
    public GameObject wipeScreen;
    public GameObject icon;
    public GameObject cameraObj;

    //Koros
    public Sprite forestloadSprite;
    public AudioClip forestBGM;

    //Camp1
    public Sprite campSprite;
    public AudioClip campBGM;

    //Mier
    public Sprite mierSprite;
    public AudioClip mierBGM;

    //Port
    public Sprite portSprite;
    public AudioClip portBGM;

    //Tomb
    public Sprite tombSprite;
    public AudioClip tombBGM;

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(DetermineAction());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator DetermineAction()
    {
        yield return new WaitForSeconds(.1f);
        wipeScreen.GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(.5f);
        switch(transition)
        {
            default:
                break;
            //Koros
            case 1:
                icon.GetComponent<Animator>().Play("Weiss_Koros");
                yield return new WaitForSeconds(4f);
                wipeScreen.GetComponent<Animator>().Play("FadeOut");
                cameraObj.GetComponent<Animator>().Play("Camera_Koros");
                yield return new WaitForSeconds(3f);

                SceneLoader.loadedScene = "Forest_1_Start";
                SceneLoader.loadSprite = forestloadSprite;
                SceneLoader.loadedBGM = forestBGM;
                GameObject.Find("BGM").GetComponent<AudioSource>().clip = forestBGM;
                GameObject.Find("BGM").GetComponent<AudioSource>().Play();
                SceneManager.LoadScene("LoadScreen");

                LevelCreator.newLevel = true;
                LevelCreator.playerStartX = 0;
                LevelCreator.playerStartY = -1.3f;
                LevelCreator.startTag = "Up";
                CameraController.lockCamera = false;
                break;

            //Camp 1
            case 2:
                icon.GetComponent<Animator>().Play("Koros_Camp1");
                yield return new WaitForSeconds(4f);
                wipeScreen.GetComponent<Animator>().Play("FadeOut");
                cameraObj.GetComponent<Animator>().Play("Camera_Camp1");
                yield return new WaitForSeconds(3f);

                SceneLoader.loadedScene = "Camp_1";
                SceneLoader.loadSprite = campSprite;
                SceneLoader.loadedBGM = campBGM;
                GameObject.Find("BGM").GetComponent<AudioSource>().clip = campBGM;
                GameObject.Find("BGM").GetComponent<AudioSource>().Play();
                SceneManager.LoadScene("LoadScreen");

                LevelCreator.newLevel = true;
                LevelCreator.playerStartX = 0;
                LevelCreator.playerStartY = 1.3f;
                LevelCreator.startTag = "Down";
                CameraController.lockCamera = false;
                break;

            //Mier
            case 3:
                icon.GetComponent<Animator>().Play("Camp1_Mier");
                yield return new WaitForSeconds(4f);
                wipeScreen.GetComponent<Animator>().Play("FadeOut");
                cameraObj.GetComponent<Animator>().Play("Camera_Camp1");
                yield return new WaitForSeconds(3f);

                SceneLoader.loadedScene = "Camp_1";
                SceneLoader.loadSprite = campSprite;
                SceneLoader.loadedBGM = campBGM;
                GameObject.Find("BGM").GetComponent<AudioSource>().clip = campBGM;
                GameObject.Find("BGM").GetComponent<AudioSource>().Play();
                SceneManager.LoadScene("LoadScreen");

                LevelCreator.newLevel = true;
                LevelCreator.playerStartX = 0;
                LevelCreator.playerStartY = 1.3f;
                LevelCreator.startTag = "Down";
                CameraController.lockCamera = false;
                break;
        }
    }
}
