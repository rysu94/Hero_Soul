using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Bloom : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(BloomRoutine());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator BloomRoutine()
    {
        yield return new WaitForSeconds(.5f);

        foreach (GameObject state in GameObject.FindGameObjectsWithTag("Player_State"))
        {
            if (state.GetComponent<StateController>().stateID == 1)
            {
                GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
                tempObj.GetComponent<Text>().text = "Removed <color=red>Poison</color>.";
                StateManager.playerStates.RemoveAt(state.GetComponent<StateController>().stateIndex);
                state.GetComponent<StateController>().DestroyIcon();
                Destroy(state);
                int index = StateManager.playerStates.Count;
                foreach (GameObject newState in GameObject.FindGameObjectsWithTag("Player_State"))
                {
                    newState.GetComponent<StateController>().stateIndex = index;
                    index--;
                }
            }
        }
    }
}
