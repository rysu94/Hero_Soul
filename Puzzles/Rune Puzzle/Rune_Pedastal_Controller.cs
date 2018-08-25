using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Rune_Pedastal_Controller : MonoBehaviour
{
    public GameObject interactText;
    public GameObject interactPrefab;
    public bool textMade = false;

    bool inbuffer = false;

    // Use this for initialization
    void Start ()
    {
        inbuffer = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        float distance = Vector2.Distance(new Vector2(transform.localPosition.x, transform.localPosition.y), TestCharController.player.transform.position);
        //print(distance);
        if (distance < .35 && !textMade)
        {
            if (GameController.xbox360Enabled())
            {
                interactText = Instantiate(Resources.Load("Prefabs/Interact_XBox"), new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity) as GameObject;
            }
            else
            {
                interactText = Instantiate(Resources.Load<GameObject>("Prefabs/Interact"), new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity);
            }
            textMade = true;
        }
        else if (distance >= .35)
        {
            Destroy(interactText);
            textMade = false;
        }
        //Activate the hero soul
        else if (InputManager.A_Button() && !inbuffer)
        {
            Destroy(interactText);
            StartCoroutine(OpenBuffer());
        }
    }

    IEnumerator OpenBuffer()
    {
        inbuffer = true;
        TestCharController.inDialogue = true;
        GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
        tempObj.GetComponent<Text>().text = "Obtained a <color=yellow>" + Rune_Database.socketData[LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].spawnRune].runeName + "</color>!";
        transform.Find("Discovered_Rune").GetComponent<Animator>().Play("Rune_Fade");
        yield return new WaitForSeconds(1.5f);
        textMade = false;
        TestCharController.inDialogue = false;
    }
}
