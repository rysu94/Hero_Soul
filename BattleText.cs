using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BattleText : MonoBehaviour
{

    public RectTransform battleText;

    public bool isScrolling = false;
    public int timer;

    public GameObject enemy;
    public int damage;

    public float vel = .035f;
    public float sideVel;
    public float trans = 1;

	// Use this for initialization
	void Start ()
    {
        battleText = GetComponent<RectTransform>();

        int tempInt = Random.Range(0, 2);
        if(tempInt == 0)
        {
            sideVel = .0025f;
        }
        else if(tempInt == 1)
        {
            sideVel = -.0025f;
        }
        StartCoroutine(ScrollTextRoutine());
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    IEnumerator ScrollTextRoutine()
    {
        while(true)
        {
            battleText.transform.Translate(Vector2.up * vel);
            battleText.transform.Translate(Vector2.right * sideVel);
            vel -= .002f;
            trans -= .01f;
            GetComponent<Text>().color = new Color(GetComponent<Text>().color.r, GetComponent<Text>().color.g, GetComponent<Text>().color.b, trans);
            timer++;
            yield return new WaitForSeconds(.01f);
            if (timer == 32)
            {
                Destroy(this.gameObject);
            }
        }

    }
}
