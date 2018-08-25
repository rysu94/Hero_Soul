using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible
{
    //The position of the destructible shard
    public Vector2 pos;

    //The Z rotation of the shard
    public Quaternion rot;

    public Destructible(Vector2 position, Quaternion rotation)
    {
        pos = position;
        rot = rotation;
    }

}
