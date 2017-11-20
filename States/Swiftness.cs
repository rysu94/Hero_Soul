using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swiftness : StateController
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine(StateDecayRoutine());
        StartCoroutine(SwiftTickRoutine());
    }

    IEnumerator SwiftTickRoutine()
    {
        TestCharController.swiftnessModfier = .25f;
        while (stateTime > 0)
        {
            yield return new WaitForSeconds(1f);
        }
        TestCharController.swiftnessModfier = 0;
    }
}
