using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBat : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        StartCoroutine(SpawnBatIndicator());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator SpawnBatIndicator()
    {
        while(true)
        {
            int tempInt = Random.Range(0, 2);
            //Spawn on the Right
            if(tempInt == 0)
            {
                float spawnLoc = Random.Range(-1.236f, 0.893f);
                Instantiate(Resources.Load("Prefabs/Cave_Tiles/BatSwarmIndicator_L"), new Vector2(2.577f, spawnLoc), Quaternion.identity);
                GameObject tempObj = Instantiate(Resources.Load("Prefabs/Cave_Tiles/BatMaker"), new Vector2(2.577f - .25f, spawnLoc), Quaternion.identity) as GameObject;
                tempObj.GetComponent<BatMaker>().goingRight = true;
            }
            //Spawn on the Left
            else
            {
                float spawnLoc = Random.Range(-1.236f, 0.893f);
                Instantiate(Resources.Load("Prefabs/Cave_Tiles/BatSwarmIndicator_R"), new Vector2(-2.587f, spawnLoc), Quaternion.identity);
                GameObject tempObj = Instantiate(Resources.Load("Prefabs/Cave_Tiles/BatMaker"), new Vector2(-2.587f + .25f, spawnLoc), Quaternion.identity) as GameObject;
            }
            yield return new WaitForSeconds(15f);
        }
        
    }
}
