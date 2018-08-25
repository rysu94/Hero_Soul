using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Mosaic_Pedastal : MonoBehaviour
{
    public GameObject interactText;
    public GameObject interactPrefab;

    public bool textMade = false;

    public GameObject lightBeam, mosaicTile;

    public int mosaicID;

    // Use this for initialization
    void Start ()
    {
		if(LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].isOpened)
        {
            mosaicTile.SetActive(false);
            lightBeam.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        float distance = Vector3.Distance(transform.position, TestCharController.player.transform.position);
        //print(distance);
        if (distance < .35 && !textMade && !LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].isOpened)
        {
            if (GameController.xbox360Enabled())
            {
                interactText = Instantiate(Resources.Load("Prefabs/Interact_XBox") as GameObject, new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity);
            }
            else
            {
                interactText = Instantiate(Resources.Load("Prefabs/Interact") as GameObject, new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity);
            }
            textMade = true;
        }
        else if (distance >= .35)
        {
            Destroy(interactText);
            textMade = false;
        }

        //If interact is pressed
        if (distance < .5 && !LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].isOpened && (InputManager.A_Button() || Input.GetKeyDown(KeyCode.F)))
        {
            LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].isOpened = true;
            GetComponent<AudioSource>().Play();
            StartCoroutine(RemoveMosaic());
        }
    }
     IEnumerator RemoveMosaic()
    {
        lightBeam.GetComponent<Animator>().Play("Mosaic_Light_Dim");
        yield return new WaitForSeconds(1f);

        switch(mosaicID)
        {
            default:
                break;
            //Koros
            case 1:
                List<int> validTile = new List<int>();
                for(int i = 0; i < Mosaic_Manager.korosMosaic.Length; i++)
                {
                    if(!Mosaic_Manager.korosMosaic[i])
                    {
                        validTile.Add(i);
                    }
                }
                int tempInt = Random.Range(0, validTile.Count);
                Mosaic_Manager.korosMosaic[validTile[tempInt]] = true;
                GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
                tempObj.GetComponent<Text>().text = "Obtained a <color=yellow>Mosaic Piece</color>!";
                break;
        }

        Destroy(interactText);
        textMade = false;
        mosaicTile.SetActive(false);
        lightBeam.SetActive(false);
    }
}
