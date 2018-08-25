using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TransferWest : MonoBehaviour
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
        //Save items on the ground
        LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].roomItems.Clear();
        foreach (GameObject item in FindObjectsOfType<GameObject>())
        {
            if (item.GetComponent<CollectItem>())
            {
                LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].roomItems.Add(new WorldItem(
                    item.transform.position, item.GetComponent<CollectItem>().itemID, GameObject.Find("InventoryController").GetComponent<ItemDatabase>().FindItem(item.GetComponent<CollectItem>().itemID).itemIconName));
            }
        }

        //Save Destructibles
        LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].roomDestructibles.Clear();
        foreach (GameObject destruct in FindObjectsOfType<GameObject>())
        {
            if (destruct.GetComponent<Destruct_Scatter>())
            {
                LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].roomDestructibles.Add(
                    new WorldDestruct(destruct.transform.position, destruct.GetComponent<Destruct_Scatter>().hitPoints, destruct));
            }
        }

        if (other.gameObject.tag == "Player")
        {

            //sets room to explored if it isn't already
            if (!LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].isExplored)
            {
                LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].isExplored = true;
            }

            LevelCreator.playerCurrentY -= 1;
            roomName = LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].roomName;
            SceneManager.LoadScene(roomName);
            LevelCreator.playerStartX = 2.3f;
            LevelCreator.playerStartY = 0;
            LevelCreator.startTag = "Left";
        }   
    }
}
