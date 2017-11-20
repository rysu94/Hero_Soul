using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BreakBar : MonoBehaviour
{
    float barLength = 126f;
    public GameObject levelOneBar;
    public GameObject levelTwoBar;
    public GameObject levelThreeBar;

    public int charge1;
    public int charge2;
    public int charge3;

    public Text breakLevel;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetCharge();
        GetLevel();
        var lvOneRect = levelOneBar.transform as RectTransform;
        var lvTwoRect = levelTwoBar.transform as RectTransform;
        var lvThreeRect = levelThreeBar.transform as RectTransform;
        lvOneRect.sizeDelta = new Vector2(barLength * (charge1/100f), lvOneRect.sizeDelta.y);
        lvTwoRect.sizeDelta = new Vector2(barLength * (charge2 / 100f), lvTwoRect.sizeDelta.y);
        lvThreeRect.sizeDelta = new Vector2(barLength * (charge3 / 100f), lvThreeRect.sizeDelta.y);
    }

    void GetCharge()
    {
        if(PlayerStats.breakMeter <= 100)
        {
            charge1 = PlayerStats.breakMeter;
            charge2 = 0;
            charge3 = 0;
        }
        else if(PlayerStats.breakMeter > 100 && PlayerStats.breakMeter <= 200)
        {
            charge1 = 100;
            charge2 = PlayerStats.breakMeter - 100;
            charge3 = 0;
        }
        else if(PlayerStats.breakMeter > 200 && PlayerStats.breakMeter <= 300)
        {
            charge1 = 100;
            charge2 = 100;
            charge3 = PlayerStats.breakMeter - 200;
        }
        else
        {
            charge1 = 0;
            charge2 = 0;
            charge3 = 0;
        }
    }

    void GetLevel()
    {
        if(charge1 == 100)
        {
            breakLevel.text = "1";
        }
        if(charge2 == 100)
        {
            breakLevel.text = "2";
        }
        if(charge3 == 100)
        {
            breakLevel.text = "3";
        }
        if(charge1 < 100)
        {
            breakLevel.text = "0";
        }
    }
        
}
