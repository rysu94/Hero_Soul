using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Transfer_Room : MonoBehaviour
{
    public string sceneName;
    public float xCoord, yCoord;
    public string direction;

    public bool changeRoom = false;
    public int newX, newY;

    public bool newLevel = false;
    public AudioClip newBGM; 

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (newLevel)
            {
                LevelCreator.newLevel = true;
                GameObject.Find("BGM").GetComponent<AudioSource>().clip = newBGM;
                GameObject.Find("BGM").GetComponent<AudioSource>().Play();
            }
            SceneManager.LoadScene(sceneName);
            LevelCreator.playerStartX = xCoord;
            LevelCreator.playerStartY = yCoord;
            LevelCreator.startTag = direction;

            if(changeRoom)
            {
                LevelCreator.playerCurrentX = newX;
                LevelCreator.playerCurrentY = newY;
            }



        }

    }
}
