using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Vector2 originalPos;


    float shakeDuration = 0f;
    float shakeMagnitude = .7f;
    float decreaseFactor = 1f;

	// Use this for initialization
	void Start ()
    {
        originalPos = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(shakeDuration > 0)
        {
            transform.position = originalPos * shakeMagnitude;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            transform.position = originalPos;
        }
	}

    public void EnableShake(float duration)
    {
        shakeDuration = duration;
        originalPos = transform.position;
    }
}
