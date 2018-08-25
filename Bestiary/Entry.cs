using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Entry : MonoBehaviour
{
    public string monsterName;
    public string monsterHealth;
    public string monsterWeakness;

    public string monsterDrop;
    public string monsterArcana;

    public string monsterDesc;

    public string background;
    public string foreground;
    public string monsterSprite;

    public bool discovered;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public Entry(string name, string health, string weak, string drop, string arcana, string desc, string sprite, string back, string fore, bool known)
    {
        monsterName = name;
        monsterHealth = health;
        monsterWeakness = weak;
        monsterDrop = drop;
        monsterArcana = arcana;
        monsterDesc = desc;

        background = back;
        foreground = fore;
        monsterSprite = sprite;

        discovered = known;
    }
}
