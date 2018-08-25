using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Transfer_Generic : MonoBehaviour
{
    //The Canvas object prefab for the interact text
    public GameObject interactTextPrefab;
    public GameObject interactText;
    public bool textMade = false;

    public string sceneName;
    public float xCoord;
    public float yCoord;
    public string direction;

    public AudioClip normal;

    public GameObject door;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float distance = Vector3.Distance(transform.position, TestCharController.player.transform.position);
        //print(distance);
        if (distance < .25 && !textMade)
        {
            if (GameController.xbox360Enabled())
            {
                interactText = Instantiate(Resources.Load("Prefabs/Interact_XBox"), new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity) as GameObject;
            }
            else
            {
                interactText = Instantiate(interactTextPrefab, new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity);
            }
            textMade = true;
        }
        else if (distance >= .25)
        {
            Destroy(interactText);
            textMade = false;
        }

        if (distance < .25 && InputManager.A_Button())
        {
            StartCoroutine(DoorRoutine());
        }
    }

    IEnumerator DoorRoutine()
    {
        door.GetComponent<Animator>().Play("Door_Open");
        door.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(.25f);
        GameObject.Find("BGM").GetComponent<AudioSource>().clip = normal;
        GameObject.Find("BGM").GetComponent<AudioSource>().Play();
        GameObject.Find("BGM").GetComponent<AudioSource>().volume = .1f;
        LevelCreator.playerStartX = xCoord;
        LevelCreator.playerStartY = yCoord;
        LevelCreator.startTag = direction;
        SceneManager.LoadScene(sceneName);
    }
}
