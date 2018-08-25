using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Lakeside : MonoBehaviour {

    public GameObject arrow;
    public AudioSource selectNoise;
    public AudioClip trialsBGM;
    public GameObject anchor;

    public GameObject trialsAnchor;

    public Sprite loadSprite;

    public bool isOver = false;

    public Image info;
    public Text infoText;
    public Text inforDesc;

    public Button enterButton;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isOver)
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPoint.z = Camera.main.transform.position.z;
            Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider == null || hit.collider.tag != "Inv_Menu")
            {
                if (isOver && Input.GetMouseButtonDown(0))
                {
                    isOver = false;
                    arrow.transform.position = anchor.transform.position;
                    selectNoise.Play();
                    info.gameObject.SetActive(false);
                    CameraController.lockCamera = false;
                    enterButton.onClick.RemoveListener(EnterScene);
                }
            }
        }
    }

    void OnMouseOver()
    {
        if (!isOver && Input.GetMouseButtonDown(0) && !CameraController.lockCamera)
        {
            arrow.transform.position = trialsAnchor.transform.position;
            selectNoise.Play();
            info.gameObject.SetActive(true);
            info.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 5));
            info.transform.position = new Vector3(info.transform.position.x + 1.15f, info.transform.position.y - .35f, info.transform.position.z);
            infoText.text = "Weiss Lakeside";
            inforDesc.text = "The Adventurer Guild holds their annual tryouts at a lake near the town of Weiss. Do you have what it takes to make it?";
            CameraController.lockCamera = true;
            StartCoroutine(ClickBuffer());
            enterButton.onClick.AddListener(EnterScene);
        }
    }

    IEnumerator ClickBuffer()
    {
        yield return new WaitForSeconds(.1f);
        isOver = true;
    }

    void EnterScene()
    {
        SceneLoader.loadedScene = "Training_1";
        SceneLoader.loadedBGM = trialsBGM;
        SceneLoader.loadSprite = loadSprite;
        GameObject.Find("BGM").GetComponent<AudioSource>().clip = trialsBGM;
        GameObject.Find("BGM").GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("LoadScreen");

        LevelCreator.newLevel = true;
        LevelCreator.playerStartX = -2.3f;
        LevelCreator.playerStartY = 0;
        LevelCreator.startTag = "Right";
        CameraController.lockCamera = false;

    }

}
