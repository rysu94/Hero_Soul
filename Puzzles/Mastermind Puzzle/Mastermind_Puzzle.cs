using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mastermind_Puzzle : MonoBehaviour
{
    public GameObject interactText;
    public bool textMade = false;

    public static int heldFruit = 0;
    public GameObject fruit;

    public List<int> combination = new List<int>();
    public int[] playerCombination = new int[3];
    public GameObject holder1, holder2, holder3;
    public GameObject arc1, arc2, arc3;

    public bool cleared = false;

	// Use this for initialization
	void Start ()
    {
        //Create the mastermind Combination
        CreateCombination();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float distance = Vector3.Distance(holder2.transform.position, TestCharController.player.transform.position);
        //print(distance);
        if (distance < .5 && !textMade)
        {
            if (GameController.xbox360Enabled())
            {
                interactText = Instantiate(Resources.Load("Prefabs/Interact_XBox") as GameObject, new Vector2(holder2.transform.position.x + .35f, holder2.transform.position.y + .15f), Quaternion.identity);
            }
            else
            {
                interactText = Instantiate(Resources.Load("Prefabs/Interact") as GameObject, new Vector2(holder2.transform.position.x + .35f, holder2.transform.position.y + .15f), Quaternion.identity);
            }
            textMade = true;
        }
        else if (distance >= .5)
        {
            Destroy(interactText);
            textMade = false;
        }

        CheckFruit();
        

        //If interact is pressed
        if(distance < .5 && (InputManager.A_Button() || Input.GetKeyDown(KeyCode.F)))
        {
            PlaceFruit();
        }
    }

    //Returns the closest holder to the player
    GameObject GetHolder()
    {
        float dist1 = Vector2.Distance(TestCharController.player.transform.position, holder1.transform.position);
        float dist2 = Vector2.Distance(TestCharController.player.transform.position, holder2.transform.position);
        float dist3 = Vector2.Distance(TestCharController.player.transform.position, holder3.transform.position);

        if(dist1 <= dist2 && dist1 <= dist3)
        {
            return holder1;
        }
        else if(dist2 <= dist1 && dist2 <= dist3)
        {
            return holder2;
        }
        else if(dist3 <= dist1 && dist3 <= dist2)
        {
            return holder3;
        }

        return null;
    }

    void SetPlayerCombination(GameObject holder, int val)
    {
        if(holder == holder1)
        {
            if(playerCombination[0] != 0)
            {
                heldFruit = playerCombination[0];
            }
            else
            {
                heldFruit = 0;
            }
            playerCombination[0] = val;
        }
        else if(holder == holder2)
        {
            if (playerCombination[1] != 0)
            {
                heldFruit = playerCombination[1];
            }
            else
            {
                heldFruit = 0;
            }
            playerCombination[1] = val;
        }
        else if(holder == holder3)
        {
            if (playerCombination[2] != 0)
            {
                heldFruit = playerCombination[2];
            }
            else
            {
                heldFruit = 0;
            }
            playerCombination[2] = val;
        }
    }

    void ReturnFruit(GameObject holder)
    {
        if (holder == holder1)
        {
            heldFruit = playerCombination[0];
        }
        else if (holder == holder2)
        {
            heldFruit = playerCombination[1];
        }
        else if (holder == holder3)
        {
            heldFruit = playerCombination[2];
        }
    }

    void PlaceFruit()
    {
        if(!cleared)
        {
            switch (heldFruit)
            {
                default:
                    GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                    GetHolder().GetComponent<Animator>().Play("Mastermind_Holder_Idle");
                    ReturnFruit(GetHolder());
                    SetPlayerCombination(GetHolder(), 0);
                    break;
                //Cherry
                case 1:
                    GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                    GetHolder().GetComponent<Animator>().Play("Mastermind_Holder_Cherry");
                    SetPlayerCombination(GetHolder(), 1);
                    break;
                //Orange
                case 2:
                    GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                    GetHolder().GetComponent<Animator>().Play("Mastermind_Holder_Orange");
                    SetPlayerCombination(GetHolder(), 2);
                    break;
                //Grape
                case 3:
                    GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                    GetHolder().GetComponent<Animator>().Play("Mastermind_Holder_Grape");
                    SetPlayerCombination(GetHolder(), 3);
                    break;
            }
            CheckCombination();

        }
        else
        {
            heldFruit = 0;
        }
    }

    void CreateCombination()
    {
        //Set the player combination
        playerCombination[0] = 0;
        playerCombination[1] = 0;
        playerCombination[2] = 0;

        //First Number
        combination.Add(Random.Range(1, 4));

        //Second Number
        combination.Add(Random.Range(1, 4));

        //Third Number
        combination.Add(Random.Range(1, 4));
    }

    //Check the mastermind combination
    void CheckCombination()
    {
        CheckHolder();

        //Check the node color
        if (playerCombination[0] == combination[0])
        {
            arc1.GetComponent<Animator>().Play("Mastermind_Node_Blue");
        }
        else
        {
            arc1.GetComponent<Animator>().Play("Mastermind_Node_Red");
        }
        
        if(playerCombination[1] == combination[1])
        {
            arc2.GetComponent<Animator>().Play("Mastermind_Node_Blue");
        }
        else
        {
            arc2.GetComponent<Animator>().Play("Mastermind_Node_Red");
        }

        if(playerCombination[2] == combination[2])
        {
            arc3.GetComponent<Animator>().Play("Mastermind_Node_Blue");
        }
        else
        {
            arc3.GetComponent<Animator>().Play("Mastermind_Node_Red");
        }


        if(playerCombination[0] == combination[0] && playerCombination[1] == combination[1] && playerCombination[2] == combination[2])
        {
            //Updates the enemy count in the current room, if there are no more enemies open doors
            
            LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].enemyCount--;
            if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].enemyCount <= 0 && !LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].roomClear)
            {
                LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].roomClear = true;
                GetComponent<AudioSource>().Play();
                cleared = true;
                heldFruit = 0;
            }
        }
    }

    void CheckHolder()
    {
        //Holder 1
        switch (playerCombination[0])
        {
            default:
                holder1.GetComponent<Animator>().Play("Mastermind_Holder_Idle");
                break;
            case 1:
                holder1.GetComponent<Animator>().Play("Mastermind_Holder_Cherry");
                break;
            case 2:
                holder1.GetComponent<Animator>().Play("Mastermind_Holder_Orange");
                break;
            case 3:
                holder1.GetComponent<Animator>().Play("Mastermind_Holder_Grape");
                break;
        }

        //Holder 2
        switch (playerCombination[1])
        {
            default:
                holder2.GetComponent<Animator>().Play("Mastermind_Holder_Idle");
                break;
            case 1:
                holder2.GetComponent<Animator>().Play("Mastermind_Holder_Cherry");
                break;
            case 2:
                holder2.GetComponent<Animator>().Play("Mastermind_Holder_Orange");
                break;
            case 3:
                holder2.GetComponent<Animator>().Play("Mastermind_Holder_Grape");
                break;
        }

        //Holder 3
        switch (playerCombination[2])
        {
            default:
                holder3.GetComponent<Animator>().Play("Mastermind_Holder_Idle");
                break;
            case 1:
                holder3.GetComponent<Animator>().Play("Mastermind_Holder_Cherry");
                break;
            case 2:
                holder3.GetComponent<Animator>().Play("Mastermind_Holder_Orange");
                break;
            case 3:
                holder3.GetComponent<Animator>().Play("Mastermind_Holder_Grape");
                break;
        }
    }

    void CheckFruit()
    {
        switch(heldFruit)
        {
            default:
                if (fruit != null)
                    Destroy(fruit);
                break;
            //Cherry
            case 1:
                if(fruit != null)
                    Destroy(fruit);
                fruit = Instantiate(Resources.Load("Prefabs/Puzzles/Mastermind/CherrySlab") as GameObject, TestCharController.player.transform);
                break;
            //Orange
            case 2:
                if (fruit != null)
                    Destroy(fruit);
                fruit = Instantiate(Resources.Load("Prefabs/Puzzles/Mastermind/OrangeSlab") as GameObject, TestCharController.player.transform);
                break;
            //Grape
            case 3:
                if (fruit != null)
                    Destroy(fruit);
                fruit = Instantiate(Resources.Load("Prefabs/Puzzles/Mastermind/GrapeSlab") as GameObject, TestCharController.player.transform);
                break;
        }
    }
}
