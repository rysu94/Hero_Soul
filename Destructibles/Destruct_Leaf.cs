using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruct_Leaf : MonoBehaviour {

    public List<GameObject> leafNode = new List<GameObject>();

    bool isTriggered = false;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Weapon" && !isTriggered)
        {
            int tempInt = Random.Range(0, leafNode.Count);
            Instantiate(Resources.Load<GameObject>("Prefabs/Hit/Wep_Hit_1"), transform.position, Quaternion.identity);
            Instantiate(Resources.Load<GameObject>("Prefabs/Destructibles/Leaf"), leafNode[tempInt].transform.position, Quaternion.identity);
            StartCoroutine(BufferShaker());
        }
    }

    IEnumerator BufferShaker()
    {
        isTriggered = true;
        Vector2 originPos = transform.position;

        for (int i = 0; i < 3; i++)
        {
            transform.position = originPos + new Vector2(Random.Range(-.005f, .005f), 0);
            yield return new WaitForSeconds(.1f);
        }
        transform.position = originPos;
        isTriggered = false;
    }
}
