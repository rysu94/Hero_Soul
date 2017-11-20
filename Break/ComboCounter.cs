using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ComboCounter : MonoBehaviour
{
    public Text comboText;
    public Text comboTimerText;

    public Coroutine redShift;
    public Coroutine comboBuffer;

    public bool comboActive;
    public float comboTime;
	// Use this for initialization
	void Start ()
    {
        PlayerStats.comboCount = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void AddCombo()
    {
        //If the player has a combo add break meter, the larger the combo the more break
        if(PlayerStats.comboCount > 0 && !TestCharController.startBreak)
        {
            PlayerStats.breakMeter++;
            if(PlayerStats.comboCount > 10)
            {
                PlayerStats.breakMeter += 2;
                if(PlayerStats.comboCount > 30)
                {
                    PlayerStats.breakMeter += 3;
                }
            }
        }
        
        PlayerStats.comboCount++;
        comboText.gameObject.SetActive(true);
        comboText.text = PlayerStats.comboCount + " hit combo!";
        comboTime = 3;
        if(!comboActive)
        {
            StartCoroutine(ComboBuffer());
        }
        
    }


    IEnumerator ComboBuffer()
    {
        comboActive = true;
        while(comboTime > 0)
        {
            comboTimerText.text = comboTime.ToString("F1");
            yield return new WaitForSeconds(.1f);
            comboTime-=.1f;
        }
        PlayerStats.comboCount = 0;
        comboActive = false;
        comboText.gameObject.SetActive(false);
    }
}
