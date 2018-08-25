using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifespanHide : MonoBehaviour
{

    public float lifespan;
    public bool active = false;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!active)
        {
            StartCoroutine(StartDecay());
        }
        
    }

    IEnumerator StartDecay()
    {
        active = true;
        yield return new WaitForSeconds(lifespan);
        active = false;
        gameObject.SetActive(false);
    }

    void OnMouseDown()
    {
        active = false;
        gameObject.SetActive(false);
    }
}
