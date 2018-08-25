using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StateController : MonoBehaviour
{
    //These manage the time left on states
    public int stateTime = 25;
    public Text stateTimeText;

    //Image of the state
    public Image stateImage;
    public Color stateColor;
    public bool isBlinking = false;

    //Imported data from the the state list
    public int stateID;
    public int stateIndex;
    public int stackSize;
    public Text stackText;
    public int stateTick;

    public GameObject tempObj;

    public string stateName;
    public string stateType;
    public string stateDesc;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(StateDecayRoutine());        
    }

    void Update()
    {
        //Update the stack size of the state
        if (stackSize > 1)
        {
            stackText.text = "x" + stackSize;
        }
        else
        {
            stackText.text = "";
        }
        UpdateTooltip();
    }

    public void UpdateTooltip()
    {
        if(stateName == "Alacrity")
        {
            stateType = "Buff x" + stackSize;
            stateDesc = "Increases your dexterity by <color=lime>" + PotionController.alacrityPotionAmount * 5 + "</color>.";
        }
        else if(stateName == "Stoneskin")
        {
            stateType = "Buff x" + stackSize;
            stateDesc = "Increases your armor by <color=lime>" + PotionController.stonePotionAmount * 10 + "</color>.";
        }
        else if (stateName == "Enfire")
        {
            stateType = "Buff x" + stackSize;
            stateDesc = "Increases your strength by <color=lime>" + (int)(PlayerStats.strength * (0.25f * stackSize)) + "</color>(25% Base).";
        }
        else if (stateName == "Enliven")
        {
            stateType = "Buff x" + stackSize;
            stateDesc = "Regenerate <color=lime>" + (stackSize * 5) + "</color> stamina every 3 seconds.";
        }
        else if (stateName == "Enstone")
        {
            stateType = "Buff x" + stackSize;
            stateDesc = "Increase your armor by <color=lime>" + (int)(PlayerStats.defense * (0.5f * stackSize)) + "</color>(50% Base).";
        }
        else if (stateName == "Enwater")
        {
            stateType = "Buff x" + stackSize;
            stateDesc = "Regenerate 1 mana every <color=lime>" + (8 - stackSize) + "</color> seconds.";
        }
        else if (stateName == "Intellect Up")
        {
            stateType = "Buff x" + stackSize;
            stateDesc = "Increase your intellect by <color=lime>" + (PotionController.intellectPotionAmount * 10) + "</color>.";
        }
        else if (stateName == "Poison")
        {
            stateType = "Debuff x" + stackSize;
            stateDesc = "Deals <color=red>" + (5 * stackSize) + "</color> damage every 5 seconds.";
        }
        else if (stateName == "Strength Up")
        {
            stateType = "Buff x" + stackSize;
            stateDesc = "Increase your strength by <color=lime>" + (PotionController.strengthPotionAmount * 10) + "</color>.";
        }
        else if (stateName == "Swiftness")
        {
            stateDesc = "Increase your movement speed by <color=lime>25%</color>.";
        }


    }

    //Manages the remaining duration of the state
    public IEnumerator StateDecayRoutine()
    {
        stateTimeText.text = stateTime.ToString() + "s";
        while (stateTime > 0)
        {
            //Check if the object is paused
            while (GameController.paused)
            {
                yield return null;
            }

            yield return new WaitForSeconds(1f);
            stateTime--;
            //Find state's position in the player states list
            for(int i = 0; i < StateManager.playerStates.Count; i++)
            {
                if(StateManager.playerStates[i].stateID == stateID)
                {
                    stateIndex = i;
                }
            }
            StateManager.playerStates[stateIndex].stateTime--;
            stateTimeText.text = stateTime.ToString() + "s";
            if (stateTime <= 10)
            {
                stateColor = stateImage.color;
                if (!isBlinking)
                {
                    StartCoroutine(StateBlinkRoutine());
                }
            }
        }
        yield return new WaitForSeconds(.25f);
        StateManager.playerStates.RemoveAt(stateIndex);

        //Update the other state positions
        if(StateManager.playerStates.Count > 0)
        {
            foreach (Transform state in GameObject.Find("States").transform)
            {
                for(int i = 0; i < StateManager.playerStates.Count; i++)
                {
                    if(state.gameObject.GetComponent<StateController>().stateID == StateManager.playerStates[i].stateID)
                    {
                        state.gameObject.GetComponent<StateController>().stateIndex = i;
                    }
                }
            }
        }

        Destroy(this.gameObject);
    }

    public IEnumerator StateBlinkRoutine()
    {
        isBlinking = true;
        while (isBlinking)
        {
            for (float i = 1f; i > 0; i -= .1f)
            {
                yield return new WaitForSeconds(.05f);
                stateColor = new Color(1f, 1f, 1f, i);
                stateImage.color = stateColor;
            }

            for (float i = 0f; i < 1; i += .1f)
            {
                yield return new WaitForSeconds(.05f);
                stateColor = new Color(1f, 1f, 1f, i);
                stateImage.color = stateColor;
            }
        }

    }

    public void DestroyIcon()
    {
        Destroy(tempObj);
    }
}
