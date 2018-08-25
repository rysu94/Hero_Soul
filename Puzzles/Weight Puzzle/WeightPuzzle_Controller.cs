using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WeightPuzzle_Controller : MonoBehaviour
{

    //The number of weights to spawn
    public int numWeights;

    //The list of objects that holds the weight gameobjects
    public List<GameObject> weightList = new List<GameObject>();

	// Use this for initialization
	void Start ()
    {
        //Generate the number of weights
        numWeights = Random.Range(3, 6);

        //Instantiate the weights

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
