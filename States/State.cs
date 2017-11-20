using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class State
{
    //These manage the time left on states
    public int stateTime;
    //The ID of the state
    public int stateID;
    //How many stacks of the state are there
    public int stackSize;

    //Manages time between state ticks
    public int stateTick;

    //is the state stackable
    public bool isStackable;

    /*
    State Database

    ID:
    0 = Default
    1 = Poison
    2 = Defense
    3 = Strength
    4 = Alacrity
    5 = Intellect
    6 = Swiftness


    */

    //State Ctor
    public State(int time, int id, int stack, bool stackable)
    {
        stateTime = time;
        stateID = id;
        stackSize = stack;
        isStackable = stackable;
    }

    public State()
    {
        stateTime = 0;
        stateID = 0;
        stackSize = 0;
    }


}
