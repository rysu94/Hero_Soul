﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class ForestOfBeginning : MonoBehaviour
{

    public GameObject arrow;
    public AudioSource selectNoise;
    public AudioClip forestBGM;
    public GameObject anchor;
    public GameObject forestAnchor;

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
        if(isOver)
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
            StartCoroutine(ClickBuffer());
            arrow.transform.position = forestAnchor.transform.position;
            selectNoise.Play();
            info.gameObject.SetActive(true);
            info.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 5));
            info.transform.position = new Vector3(info.transform.position.x + 1.15f, info.transform.position.y, info.transform.position.z);
            infoText.text = "Koros Forest";
            inforDesc.text = "The Koros forest is small forest on the road to the Alterian Capital. Though it is small, many people still go missing traversing it.";
            CameraController.lockCamera = true;
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
        SceneLoader.loadedScene = "Forest_1_Start";
        SceneLoader.loadSprite = loadSprite;
        SceneLoader.loadedBGM = forestBGM;
        GameObject.Find("BGM").GetComponent<AudioSource>().clip = forestBGM;
        GameObject.Find("BGM").GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("LoadScreen");

        LevelCreator.newLevel = true;
        LevelCreator.playerStartX = 0;
        LevelCreator.playerStartY = -1.3f;
        LevelCreator.startTag = "Up";
        CameraController.lockCamera = false;
    }
}
