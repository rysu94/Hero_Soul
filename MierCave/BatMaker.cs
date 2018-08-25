using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMaker : MonoBehaviour
{
    public bool goingRight = false;
    float count = 0;

	// Use this for initialization
	void Start ()
    {
        Instantiate(Resources.Load("Prefabs/Cave_Tiles/Exclaim"), transform.position, Quaternion.identity);
        StartCoroutine(SpawnBats());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator SpawnBats()
    {
        yield return new WaitForSeconds(5f);
        GetComponent<AudioSource>().Play();
        while(count <= 5)
        {
            if(goingRight)
            {
                GameObject tempObj = Instantiate(Resources.Load("Prefabs/Cave_Tiles/BatSwarm"), new Vector2(transform.position.x, transform.position.y + Random.Range(-.25f, .25f)), Quaternion.identity) as GameObject;
                tempObj.GetComponent<Rigidbody2D>().velocity = new Vector2(-1.25f, 0);            
            }
            else
            {
                GameObject tempObj = Instantiate(Resources.Load("Prefabs/Cave_Tiles/BatSwarm"), new Vector2(transform.position.x, transform.position.y + Random.Range(-.25f, .25f)), Quaternion.identity) as GameObject;
                tempObj.GetComponent<Rigidbody2D>().velocity = new Vector2(1.25f, 0);
                tempObj.GetComponent<Animator>().Play("BatSwarm2");
            }
            float tempTime = Random.Range(.1f, .25f);
            yield return new WaitForSeconds(tempTime);
            count += tempTime;
        }
        yield return new WaitForSeconds(3.5f);
    }
}
