using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDestruct : MonoBehaviour
{
    //Position of the object, used for the ID of the object
    Vector2 pos;

    //Hit points of the object
    int hitPoints;

    //The shards of the object
    public List<Destructible> destructShards = new List<Destructible>();

    public WorldDestruct(Vector2 position, int hp, GameObject destructible)
    {
        pos = position;
        hitPoints = hp;

        GameObject objBase = destructible;
        
        for(int i = 0; i < objBase.GetComponent<Destruct_Scatter>().shards.Count; i++)
        {
            destructShards.Add(new Destructible(objBase.GetComponent<Destruct_Scatter>().shards[i].transform.position, 
                objBase.GetComponent<Destruct_Scatter>().shards[i].transform.rotation));
        }
    }

    public void AlignDestructible()
    {
        GameObject objBase = null;
        //Find the correct destructible
        foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            if(obj.GetComponent<Destruct_Scatter>() && (Vector2)obj.transform.position == pos)
            {
                objBase = obj;
                break;
            }
        }

        if(hitPoints <= 0)
        {
            objBase.GetComponent<BoxCollider2D>().enabled = false;
        }

        for(int i = 0; i < objBase.GetComponent<Destruct_Scatter>().shards.Count; i++)
        {
            objBase.GetComponent<Destruct_Scatter>().shards[i].transform.position = destructShards[i].pos;
            objBase.GetComponent<Destruct_Scatter>().shards[i].transform.rotation = destructShards[i].rot;
        }
    }

}
