using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedInteract : MonoBehaviour
{
    //The Canvas object prefab for the interact text
    public GameObject interactTextPrefab;
    public GameObject interactText;
    public bool textMade = false;


    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float distance = Vector3.Distance(transform.position, TestCharController.player.transform.position);
        //print(distance);
        if (distance < .5 && !textMade)
        {
            interactText = Instantiate(interactTextPrefab, new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity);
            textMade = true;
        }
        else if (distance >= .5)
        {
            Destroy(interactText);
            textMade = false;
        }
    }
}
