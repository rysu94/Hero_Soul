using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InteractTextController : MonoBehaviour
{

    public Text interactText;
    public Color textColor;

	// Use this for initialization
	void Start ()
    {
        interactText = GetComponent<Text>();
        textColor = interactText.color;
	}
	
	// Update is called once per frame
	void Update ()
    {
        int distance = (int)Vector2.Distance(Camera.main.WorldToScreenPoint(transform.position), GameObject.FindGameObjectWithTag("Player").transform.position);

        if(distance < 1108)
        {
            textColor.a = 255;
        }
        else if(distance >= 1108)
        {
            textColor.a = 0;
        }
        print(distance);

	}
}
