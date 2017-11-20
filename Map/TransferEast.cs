using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TransferEast : MonoBehaviour
{

    string roomName;

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
            //sets room to explored if it isn't already
            if (!LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].isExplored)
            {
                LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].isExplored = true;
            }
            LevelCreator.playerCurrentY += 1;
            roomName = LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].roomName;
            SceneManager.LoadScene(roomName);
            LevelCreator.playerStartX = -2.3f;
            LevelCreator.playerStartY = 0;
            LevelCreator.startTag = "Right";
        }
    }

}
