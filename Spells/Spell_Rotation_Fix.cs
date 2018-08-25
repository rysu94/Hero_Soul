using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Rotation_Fix : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.identity;
	}
}
