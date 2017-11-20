using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayButton : MonoBehaviour
{

    public Button playButton;

	// Use this for initialization
	void Start ()
    {
        playButton = GetComponent<Button>();
        playButton.onClick.AddListener(PlayGame);

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void PlayGame()
    {
        SceneManager.LoadScene("Forest_1_Start");
    }
}
