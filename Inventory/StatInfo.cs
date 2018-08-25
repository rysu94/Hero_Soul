using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatInfo : MonoBehaviour
{
    public string header = "Stats:";
    public string desc = "";

    void Start()
    {
        desc = "<color=orange>DEF: - DMG Taken.</color><color=red>\nSTR: + Physical damage.</color>\n<color=lime>DEX: + Crit Chance.</color><color=yellow>\nEND: + Stamina.</color>\n<color=white>VIT: + Health.</color><color=blue>\nINT: + Magic Damage.</color>\n<color=lightblue>WIS: + Mana.</color>";
    }

}
