using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BossTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("Forest_1_Boss");
            LevelCreator.playerStartX = 0;
            LevelCreator.playerStartY = -1.3f;
            LevelCreator.startTag = "Up";
            Town_Event_5.start = true;
            TestCharController.arcanaEnabled = true;
        }
	}
}
