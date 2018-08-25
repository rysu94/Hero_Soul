using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue
{
    public string dialogueText;

    public string dialogueForeIMG;
    public string dialogueBackIMG;

    public string dialogueNPC;

    public string dialogueName;
    public float dialoguePitch;

    public int activeIMG;

    public IEnumerator dialogueAction;

    public Dialogue(string dia, string img, string img2, string img3, string name, float pitch, int active)
    {
        dialogueText = dia;

        dialogueForeIMG = img;
        dialogueBackIMG = img2;
        dialogueNPC = img3;

        dialoguePitch = pitch;
        dialogueName = name;

        activeIMG = active;
    }


}
