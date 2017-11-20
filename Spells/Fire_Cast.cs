using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Cast : MonoBehaviour
{

    public Animator fireCast;

    void Awake()
    {
        fireCast = GetComponent<Animator>();
    }
	// Use this for initialization
	void Start ()
    {
        StartCoroutine(DeathRoutine());
        fireCast.Play("Explode_2");
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
