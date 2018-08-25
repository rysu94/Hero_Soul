using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScreenEffects : MonoBehaviour
{
    public Image injured;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (PlayerStats.health < (int)(PlayerStats.maxHealth * .25f))
        {
            injured.gameObject.SetActive(true);
        }
        else
        {
            injured.gameObject.SetActive(false);
        }
    }
}
