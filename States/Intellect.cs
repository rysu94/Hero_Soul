using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intellect : StateController
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine(StateDecayRoutine());
        StartCoroutine(IntellectTickRoutine());
    }

    IEnumerator IntellectTickRoutine()
    {
        PlayerStats.intelBuffBonus = (PotionController.intellectPotionAmount * 10);
        while (stateTime > 0)
        {
            yield return new WaitForSeconds(1f);
        }
        PlayerStats.intelBuffBonus = 0;
    }

}
