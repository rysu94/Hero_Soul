using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    //Blue Flag Sprite
    public Sprite clearFlag;


    //Weiss
    public GameObject weissFlag;
    public static bool weissClear = true;

    //Koros Forest
    public GameObject korosFlag;
    public static bool korosClear = false;

    //Mier Mines
    public GameObject mierFlag;
    public static bool mierClear = false;




	// Use this for initialization
	void Start ()
    {



		if(korosClear)
        {
            korosFlag.GetComponent<SpriteRenderer>().sprite = clearFlag;
            //korosFlag.GetComponent<Animator>().Play("Flag_Idle");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
