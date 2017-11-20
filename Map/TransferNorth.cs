using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TransferNorth : MonoBehaviour
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
        if(other.gameObject.tag == "Player")
        {
         
            //sets room to explored if it isn't already
            if (!LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].isExplored)
            {
                LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].isExplored = true;
            }
            LevelCreator.playerCurrentX -= 1;
            roomName = LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].roomName;
            SceneManager.LoadScene(roomName);            
            LevelCreator.playerStartX = 0;
            LevelCreator.playerStartY = -1.3f;
            LevelCreator.startTag = "Up";
            
        }
    }
}
