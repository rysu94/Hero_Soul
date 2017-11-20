using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BattleText : MonoBehaviour
{

    public RectTransform battleText;

    public bool isScrolling = false;
    public int timer;

	// Use this for initialization
	void Start ()
    {
        battleText = GetComponent<RectTransform>();     
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!isScrolling)
        {
            StartCoroutine(ScrollTextRoutine());
        }
	}

    IEnumerator ScrollTextRoutine()
    {
        isScrolling = true;
        battleText.transform.Translate(Vector2.up *.015f);
        timer++;
        yield return new WaitForSeconds(.01f);
        if(timer == 16)
        {
            Destroy(this.gameObject);
        }
        isScrolling = false;
    }
}
