using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alacrity : StateController
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine(StateDecayRoutine());
        StartCoroutine(AlacrityTickRoutine());
    }

    IEnumerator AlacrityTickRoutine()
    {
        PlayerStats.dexBuffBonus = (PotionController.alacrityPotionAmount * 5);
        while (stateTime > 0)
        {
            yield return new WaitForSeconds(1f);
        }
        PlayerStats.dexBuffBonus = 0;
    }
}
