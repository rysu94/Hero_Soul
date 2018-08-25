using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon_Controller : MonoBehaviour
{
    public List<GameObject> anchorList = new List<GameObject>();
    public GameObject icon;
    public int indexer;
    public bool buffer = false;


	// Use this for initialization
	void Start ()
    {
        indexer = 0;
        icon.transform.position = anchorList[indexer].transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKey(KeyCode.A) && !buffer && indexer > 0)
        {
            StartCoroutine(Buffer());
            indexer--;
            GetComponent<AudioSource>().Play();
            StartCoroutine(MoveIcon());
        }
        else if(Input.GetKey(KeyCode.D) && !buffer && indexer < anchorList.Count-1)
        {
            StartCoroutine(Buffer());
            indexer++;
            GetComponent<AudioSource>().Play();
            StartCoroutine(MoveIcon());
        }
	}

    IEnumerator Buffer()
    {
        buffer = true;
        yield return new WaitForSeconds(1f);
        buffer = false;
    }

    IEnumerator MoveIcon()
    {
        Vector2 currentPos = icon.transform.position;
        Vector2 anchorPos = anchorList[indexer].transform.position;
        Vector2 directionVec = (anchorPos - currentPos) / 50;

        for(int i = 0; i < 50; i++)
        {
            icon.transform.position = (Vector2)icon.transform.position + directionVec;
            yield return new WaitForSeconds(.01f);
        }
    }
}
