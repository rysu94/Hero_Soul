using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Count_Cone_Move : MonoBehaviour
{
    //1 left
    //2 right
    //3 up
    //4 down
    public int moveID;

    public GameObject shadow;

    public GameObject telegraph;

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
            //left
            case 1:
                GetComponent<Animator>().Play("Count_Cone_L");
                break;
            //right
            case 2:
                GetComponent<Animator>().Play("Count_Cone_R");
                break;
            //up
            case 3:
                GetComponent<Animator>().Play("Count_Cone_U");
                break;
            //down
            case 4:
                GetComponent<Animator>().Play("Count_Cone_D");
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
