using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static float time = 0;
    public Text timer;

    public static bool started = false;
    public static Coroutine timerRoutine;

    public GameObject finish;
    public static bool finished = false;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    IEnumerator AddTime()
    {
        time = 10;
        while(!finished && time > 0)
        {
            yield return new WaitForSeconds(.1f);
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            time -= .1f;
            timer.text = time.ToString("F1");
        }
        timer.text = "";
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(!started && collision.gameObject.tag == "Player")
        {
            started = true;
            finished = false;
            timerRoutine = StartCoroutine(AddTime());
        }

    }
}
