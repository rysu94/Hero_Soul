using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountLine_Move : MonoBehaviour
{
    //1 Up 2 Down 3 Left 4 Right
    public int moveID;
    public GameObject telegraph;
    public GameObject shadow;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(MoveRoutine());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator MoveRoutine()
    {
        yield return new WaitForSeconds(3f);
        telegraph.SetActive(false);
        switch(moveID)
        {
            default:
                break;
            case 1:
                GetComponent<Animator>().Play("Count_Dash_U");
                    break;
            case 2:
                GetComponent<Animator>().Play("Count_Dash_D");
                break;
            case 3:
                GetComponent<Animator>().Play("Count_Dash_L");
                break;
            case 4:
                GetComponent<Animator>().Play("Count_Dash_R");
                break;
        }
        yield return new WaitForSeconds(2.5f);
        for (float i = 0; i < .25f; i += .025f)
        {
            shadow.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, (1f - (i * 4)));
            yield return new WaitForSeconds(.05f);
        }
        Destroy(gameObject);
    }
}
