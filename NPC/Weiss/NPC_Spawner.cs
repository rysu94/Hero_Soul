using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Spawner : MonoBehaviour
{
    public string gender, npcID;
    public int id, pos;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(SpawnNPC());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnNPC()
    {
        while(true)
        {
            
            int tempInt = Random.Range(0,2);
            if(tempInt == 0)
            {
                gender = "M";
                id = Random.Range(1, 6);
                if(id == 1)
                {
                    npcID = "";
                }
                else
                {
                    npcID = id.ToString();
                }
                
            }
            else
            {
                gender = "F";
                id = Random.Range(1, 6);
                if (id == 1)
                {
                    npcID = "";
                }
                else
                {
                    npcID = id.ToString();
                }
            }
            GameObject tempObj = Instantiate(Resources.Load("Prefabs/NPC/" + gender + "_NPC_" + id), transform.position, Quaternion.identity) as GameObject;
            tempObj.AddComponent<NPC>();
            tempObj.GetComponent<NPC>().move = true;
            tempObj.GetComponent<NPC>().gender = gender;
            tempObj.GetComponent<NPC>().NPC_ID = npcID;
           
            tempObj.GetComponent<NPC>().defaultPos = pos;
            yield return new WaitForSeconds(.5f);
            tempObj.GetComponent<NPC>().Move();
            if(pos == 1)
            {
                transform.position = new Vector2(Random.Range(-.5f, .5f), -1.903f);
            }
            else if(pos == 2)
            {
                transform.position = new Vector2(Random.Range(-.5f, .5f), 1.903f);
            }
            else if(pos == 3)
            {
                transform.position = new Vector2(2.791f, Random.Range(0.074f, -.502f));
            }
            else if (pos == 4)
            {
                transform.position = new Vector2(-2.791f, Random.Range(0.074f, -.502f));
            }

            yield return new WaitForSeconds(Random.Range(10,25));
        }
    }
}
