using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Quest_Slab : MonoBehaviour
{

    public Text questNameText;
    public Image questIMG;

    public string questName;
    public List<string> questObj = new List<string>();
    public List<bool> questComplete = new List<bool>();
    public string questDesc;

    public int xp;
    public int gold;
    public List<Item> questItems = new List<Item>();

    public int type;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

    }
}
