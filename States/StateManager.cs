using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    //List of the states the player has
    public static List<State> playerStates = new List<State>();

    //The parent object
    public GameObject stateFrame;

    //Default State Prefab
    public GameObject stateIcon;

    //Poison State Prefab
    public GameObject poisonIcon;

    //Stoneskin state prefab
    public GameObject stoneIcon;

    //strength state prefab
    public GameObject strengthIcon;

    //alacrity state prefabs
    public GameObject dexterityIcon;
    public GameObject swiftIcon;

    //intellect state prefab
    public GameObject intellectIcon;

    //Dazed state prefab
    public GameObject dazedIcon;


	// Use this for initialization
	void Start ()
    {
        UpdateStates();
	}
	
	// Update is called once per frame
	void Update ()
    {
		//Test Add state to character
        /*
        if(Input.GetKeyDown(KeyCode.Y))
        {
            AddState(25, 1, 1);
            UpdateStates();
        }
        */
	}

    //Adds a state to the player's state list and instantiate the icon
    public void AddState(int duration, int id, int stack, bool stackable)
    {
        //Search if state already exists in the list
        if(playerStates.Count > 0)
        {
            for (int i = 0; i < playerStates.Count; i++)
            {
                //if one is found and it can be stacked, stack
                if(id == playerStates[i].stateID && playerStates[i].isStackable)
                {
                    playerStates[i].stackSize += stack;
                    playerStates[i].stateTime = duration;
                    playerStates[i].stateTick = 0;
                    UpdateStates();
                    return;
                }
                //If it is found but not stackable, add to timer
                else if(id == playerStates[i].stateID && !playerStates[i].isStackable)
                {
                    playerStates[i].stateTime += duration;
                    playerStates[i].stateTick = 0;
                    UpdateStates();
                    return;
                }
            }
        }
        //If state is not found, add it to the list and instantiate it

        //Add state to the list
        playerStates.Add(new State(duration, id, stack, stackable));

        //Instantiate the state icon
        GameObject tempObj = GetState(id);
        Instantiate(tempObj).transform.SetParent(stateFrame.transform, false);

        //Send data the instantiated game object
        tempObj.GetComponent<StateController>().stateTime = duration;
        tempObj.GetComponent<StateController>().stateID = id;
        tempObj.GetComponent<StateController>().stateIndex = playerStates.Count - 1;
        tempObj.GetComponent<StateController>().stackSize = stack;
        tempObj.GetComponent<StateController>().stateTick = 0;
    }

    //Returns the prefab associated with the state id
    public GameObject GetState(int id)
    {
        GameObject returnState = stateIcon;

        //default
        if (id == 0)
        {
            returnState = stateIcon;
        }
        //Poison
        else if (id == 1)
        {
            returnState = poisonIcon;
        }
        //Stoneskin
        else if (id == 2)
        {
            returnState = stoneIcon;
        }
        //Strength
        else if (id == 3)
        {
            returnState = strengthIcon;
        }
        //Alacrity
        else if (id == 4)
        {
            returnState = dexterityIcon;
        }
        else if (id == 5)
        {
            returnState = intellectIcon;
        }
        else if(id == 6)
        {
            returnState = swiftIcon;
        }
        else if(id == 7)
        {
            returnState = dazedIcon;
        }
        return returnState;
    }

    public void UpdateStates()
    {
        //Remove all states objects
        foreach(Transform child in stateFrame.transform)
        {
            Destroy(child.gameObject);
        }

        //Instantiate the states
        if (playerStates.Count > 0)
        {
            for (int i = 0; i < playerStates.Count; i++)
            {
                GameObject tempObj = Instantiate(GetState(playerStates[i].stateID));
                tempObj.transform.SetParent(stateFrame.transform, false);
                //Send data the instantiated game object
                tempObj.GetComponent<StateController>().stateTime = playerStates[i].stateTime;
                tempObj.GetComponent<StateController>().stateID = playerStates[i].stateID;
                tempObj.GetComponent<StateController>().stateIndex = i;
                tempObj.GetComponent<StateController>().stackSize = playerStates[i].stackSize;
                tempObj.GetComponent<StateController>().stateTick = playerStates[i].stateTick;
            }
        }
    }
}
