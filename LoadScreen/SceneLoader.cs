using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{

    public static string loadedScene;
    public static AudioClip loadedBGM;
    public static Sprite loadSprite;

    public Text loadText;
    public Image loadBG;


    public bool isDone = false;

    AsyncOperation ao;

    void Start()
    {
        StartCoroutine(LoadAsync());
        loadBG.GetComponent<Image>().sprite = loadSprite;
    }

    void Update()
    {
        if(isDone)
        {
            loadText.gameObject.GetComponent<LoadText>().isDone = true;
            loadText.text = "Press Space to Continue.";

            

            if (Input.GetKey(KeyCode.Space))
            {
                ao.allowSceneActivation = true;
            }
        }
    }

    IEnumerator LoadAsync()
    {
        yield return null;

        ao = SceneManager.LoadSceneAsync(loadedScene);
        ao.allowSceneActivation = false;

        while (ao.progress < .9f)
        {
            float progress = Mathf.Clamp01(ao.progress / 0.9f);
        }
        
        isDone = true;
        
        
        yield return null;
    }
}
