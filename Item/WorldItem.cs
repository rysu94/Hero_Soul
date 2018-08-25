using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour
{
    public Vector2 itemPos;
    public int itemID;
    public string itemName;

    public void SpawnItem()
    {
        GameObject tempObj = Instantiate(Resources.Load("Prefabs/Items/" + itemName), itemPos, Quaternion.identity) as GameObject;
    }

    public WorldItem(Vector2 pos, int id, string name)
    {
        itemPos = pos;
        itemID = id;
        itemName = name;
    }
}
