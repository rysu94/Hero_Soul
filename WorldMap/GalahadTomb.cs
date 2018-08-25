using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GalahadTomb : MonoBehaviour
{

    public GameObject arrow;
    public AudioSource selectNoise;
    public AudioClip townBGM;
    public GameObject anchor;

    public GameObject weissAnchor;

    public Sprite loadSprite;

    public bool isOver = false;

    public Image info;
    public Text infoText;
    public Text inforDesc;

    public Button enterButton;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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
            arrow.transform.position = weissAnchor.transform.position;
            selectNoise.Play();
            info.gameObject.SetActive(true);
            info.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 5));
            info.transform.position = new Vector3(info.transform.position.x + 1.15f, info.transform.position.y - .35f, info.transform.position.z);
            infoText.text = "Galahad's Tomb";
            inforDesc.text = "The lost and forgotton tomb of the great Galahad.";
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
        SceneLoader.loadedScene = "Tomb_1_Start";
        SceneLoader.loadedBGM = townBGM;
        SceneLoader.loadSprite = loadSprite;
        GameObject.Find("BGM").GetComponent<AudioSource>().clip = townBGM;
        GameObject.Find("BGM").GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("LoadScreen");

        LevelCreator.newLevel = true;
        LevelCreator.playerStartX = 0;
        LevelCreator.playerStartY = -1.3f;
        LevelCreator.startTag = "Up";
        CameraController.lockCamera = false;

    }
}
