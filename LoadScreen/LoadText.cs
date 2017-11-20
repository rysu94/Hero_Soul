using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LoadText : MonoBehaviour
{
    public bool isDone = false;

    public Coroutine load;

	// Use this for initialization
	void Start ()
    {
        load = StartCoroutine(LoadingScreen());
	}

    void Update()
    {
        if(isDone)
        {
            StopCoroutine(load);
        }
    }

    IEnumerator LoadingScreen()
    {
        while(!isDone)
        {
            GetComponent<Text>().text = "Loading";
            yield return new WaitForSeconds(1f);
            GetComponent<Text>().text = "Loading.";
            yield return new WaitForSeconds(1f);
            GetComponent<Text>().text = "Loading..";
            yield return new WaitForSeconds(1f);
            GetComponent<Text>().text = "Loading...";
            yield return new WaitForSeconds(1f);
        }
    }
	

}
