using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weight : MonoBehaviour
{
    public int weightAmount = 0;

    public void ChangeWeight(int newWeight)
    {
        weightAmount = newWeight;
    }

}
