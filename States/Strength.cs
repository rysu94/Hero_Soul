﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strength : StateController {

    // Use this for initialization
    void Start()
    {
        StartCoroutine(StateDecayRoutine());
        StartCoroutine(StrengthTickRoutine());
    }

    IEnumerator StrengthTickRoutine()
    {
        PlayerStats.strengthBuffBonus = (PotionController.strengthPotionAmount * 10);
        while (stateTime > 0)
        {
            //Check if the object is paused
            while (GameController.paused)
            {
                yield return null;
            }

            yield return new WaitForSeconds(1f);
        }
        PlayerStats.strengthBuffBonus = 0;
    }
}
