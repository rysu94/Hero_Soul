using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BossBarTransparent : MonoBehaviour
{

    public Image bossBar;
    public Image bossBarMask;
    public Image bossBarBack;
    public Image skull;

    public Color bossBarAlpha;

	// Use this for initialization
	void Start ()
    {
        bossBarAlpha = bossBar.color;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            bossBarAlpha.a = .25f;
            bossBar.color = bossBarAlpha;
            bossBarMask.color = bossBarAlpha;
            bossBarBack.color = bossBarAlpha;
            skull.color = bossBarAlpha;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            bossBarAlpha.a = 1f;
            bossBar.color = bossBarAlpha;
            bossBarMask.color = bossBarAlpha;
            bossBarBack.color = bossBarAlpha;
            skull.color = bossBarAlpha;
        }
    }
}
