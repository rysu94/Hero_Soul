using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue
{
    public string dialogueText;
    public string dialogueIMG;
    public string dialogueName;
    public float dialoguePitch;


    public Dialogue(string dia, string img, string name, float pitch)
    {
        dialogueText = dia;
        dialogueIMG = img;
        dialoguePitch = pitch;
        dialogueName = name;
    }


}
