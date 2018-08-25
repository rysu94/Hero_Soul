using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path_Nodes : MonoBehaviour
{
    public List<GameObject> nodeList = new List<GameObject>();
    public GameObject[] objList;

    // Use this for initialization
    void Start ()
    {
        UpdateNodes();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //Check if the node is within a collider
    public void CheckNodeValid(GameObject node)
    {
        node.GetComponent<Node>().valid = true;
        //node.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);

        objList = GameObject.FindGameObjectsWithTag("Respawn");
        foreach (GameObject obj in objList)
        {
            //Determine Bounds of the collider
            if (obj.GetComponent<Collider2D>().OverlapPoint(node.transform.position) && !obj.GetComponent<Collider2D>().isTrigger)
            {
                node.GetComponent<Node>().valid = false;
                //node.GetComponent<SpriteRenderer>().color = new Color(.5f, .5f, .5f);
                break;
            }            
        }
    }

    //Return the node that is closest to the position given
    public GameObject FindClosestNode(Vector2 pos, GameObject currentNode)
    {
        //UpdateNodes();
        GameObject foundNode = nodeList[0];
        float distance = 100;

        for(int i = 0; i < nodeList.Count; i++)
        {
            if(Vector2.Distance(nodeList[i].transform.position, pos) < distance && nodeList[i].GetComponent<Node>().valid /*&& 
                nodeList[i] != currentNode*/)
            {
                foundNode = nodeList[i];
                distance = Vector2.Distance(nodeList[i].transform.position, pos);
            }
        }

        return foundNode;
    }

    //Returns a vector pointing to the nearest node
    public Vector2 Pathfind(GameObject origin, GameObject end, GameObject companion, ref GameObject currentNode, ref GameObject lastNode)
    {
        //end.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f);
        /*
        if (Vector2.Distance(companion.transform.position, origin.transform.position) > .18f)
        {
            origin.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f);
            print("Moving to origin " + origin);
            return (origin.transform.position - companion.transform.position).normalized;
        }
        */
        //else
        //{
        float distance;
        if(currentNode != null)
        {
            distance = Vector2.Distance(companion.transform.position, currentNode.transform.position);
            if(distance > .05f)
            {
                return (currentNode.transform.position - companion.transform.position).normalized;
            }
        }

        //If companions is close enough to the current node, find a new one
        GameObject closestNode = origin;
        distance = 100;
        //Check N
        if (origin.GetComponent<Node>().nodeLinks[0] != null && origin.GetComponent<Node>().valid && origin.GetComponent<Node>().nodeLinks[0] != lastNode &&
                Vector2.Distance(origin.GetComponent<Node>().nodeLinks[0].transform.position, end.transform.position) < distance)
        {
            distance = Vector2.Distance(origin.GetComponent<Node>().nodeLinks[0].transform.position, end.transform.position);
            closestNode = origin.GetComponent<Node>().nodeLinks[0];
        }
        //Check NE
        if (origin.GetComponent<Node>().nodeLinks[1] != null && origin.GetComponent<Node>().valid && origin.GetComponent<Node>().nodeLinks[1] != lastNode &&
            Vector2.Distance(origin.GetComponent<Node>().nodeLinks[1].transform.position, end.transform.position) < distance)
        {
            distance = Vector2.Distance(origin.GetComponent<Node>().nodeLinks[1].transform.position, end.transform.position);
            closestNode = origin.GetComponent<Node>().nodeLinks[1];
        }
        //Check E
        if (origin.GetComponent<Node>().nodeLinks[2] != null && origin.GetComponent<Node>().valid && origin.GetComponent<Node>().nodeLinks[2] != lastNode &&
            Vector2.Distance(origin.GetComponent<Node>().nodeLinks[2].transform.position, end.transform.position) < distance)
        {
            distance = Vector2.Distance(origin.GetComponent<Node>().nodeLinks[2].transform.position, end.transform.position);
            closestNode = origin.GetComponent<Node>().nodeLinks[2];
        }
        //Check SE
        if (origin.GetComponent<Node>().nodeLinks[3] != null && origin.GetComponent<Node>().valid && origin.GetComponent<Node>().nodeLinks[3] != lastNode &&
            Vector2.Distance(origin.GetComponent<Node>().nodeLinks[3].transform.position, end.transform.position) < distance)
        {
            distance = Vector2.Distance(origin.GetComponent<Node>().nodeLinks[3].transform.position, end.transform.position);
            closestNode = origin.GetComponent<Node>().nodeLinks[3];
        }
        //Check S
        if (origin.GetComponent<Node>().nodeLinks[4] != null && origin.GetComponent<Node>().valid && origin.GetComponent<Node>().nodeLinks[4] != lastNode &&
            Vector2.Distance(origin.GetComponent<Node>().nodeLinks[4].transform.position, end.transform.position) < distance)
        {
            distance = Vector2.Distance(origin.GetComponent<Node>().nodeLinks[4].transform.position, end.transform.position);
            closestNode = origin.GetComponent<Node>().nodeLinks[4];
        }
        //Check SW
        if (origin.GetComponent<Node>().nodeLinks[5] != null && origin.GetComponent<Node>().valid && origin.GetComponent<Node>().nodeLinks[5] != lastNode &&
            Vector2.Distance(origin.GetComponent<Node>().nodeLinks[5].transform.position, end.transform.position) < distance)
        {
            distance = Vector2.Distance(origin.GetComponent<Node>().nodeLinks[5].transform.position, end.transform.position);
            closestNode = origin.GetComponent<Node>().nodeLinks[5];
        }
        //Check W
        if (origin.GetComponent<Node>().nodeLinks[6] != null && origin.GetComponent<Node>().valid && origin.GetComponent<Node>().nodeLinks[6] != lastNode &&
            Vector2.Distance(origin.GetComponent<Node>().nodeLinks[6].transform.position, end.transform.position) < distance)
        {
            distance = Vector2.Distance(origin.GetComponent<Node>().nodeLinks[6].transform.position, end.transform.position);
            closestNode = origin.GetComponent<Node>().nodeLinks[6];
        }
        //Check NW
        if (origin.GetComponent<Node>().nodeLinks[7] != null && origin.GetComponent<Node>().valid && origin.GetComponent<Node>().nodeLinks[7] != lastNode &&
            Vector2.Distance(origin.GetComponent<Node>().nodeLinks[7].transform.position, end.transform.position) < distance)
        {
            distance = Vector2.Distance(origin.GetComponent<Node>().nodeLinks[7].transform.position, end.transform.position);
            closestNode = origin.GetComponent<Node>().nodeLinks[7];
        }

        currentNode = closestNode;
        lastNode = origin;

        //closestNode.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f);

        //print("Moving to " + closestNode);
        return (closestNode.transform.position - origin.transform.position).normalized;
        //}
    }

    //Connects Nodes for available paths
    public void UpdateNodes()
    {
        //Check if the node is valid
        for (int i = 0; i < nodeList.Count; i++)
        {
            CheckNodeValid(nodeList[i]);
            if (nodeList[i].GetComponent<Node>().valid)
            {
                //print(0);
                //Raycast each of the cardinal direction
                Vector2 worldPoint = nodeList[i].transform.position;
                //worldPoint.z = 0;
                //print(worldPoint);
                //North
                RaycastHit2D[] hitN = Physics2D.LinecastAll(worldPoint, new Vector2(worldPoint.x, worldPoint.y + .48f));

                for (int j = 0; j < hitN.Length; j++)
                {
                    if (hitN[j].collider != null && hitN[j].collider != nodeList[i].GetComponent<Collider2D>() && hitN[j].collider.tag != "Companion")
                    {
                        if (!hitN[j].collider.isTrigger)
                        {
                            Debug.DrawLine(worldPoint, hitN[j].point, Color.red, 10);
                            break;
                        }
                        else if (hitN[j].collider.tag == "Node")
                        {
                            nodeList[i].GetComponent<Node>().nodeLinks[0] = hitN[j].collider.gameObject;
                            Debug.DrawLine(worldPoint, hitN[j].transform.position, Color.green, 10);
                            break;
                        }
                    }
                }

                //Northeast
                RaycastHit2D[] hitNE = Physics2D.LinecastAll(worldPoint, new Vector2(worldPoint.x + .12f, worldPoint.y + .24f));

                for (int j = 0; j < hitNE.Length; j++)
                {
                    if (hitNE[j].collider != null && hitNE[j].collider != nodeList[i].GetComponent<Collider2D>() && hitNE[j].collider.tag != "Companion")
                    {
                        if (!hitNE[j].collider.isTrigger)
                        {
                            Debug.DrawLine(worldPoint, hitNE[j].point, Color.red, 10);
                            break;
                        }
                        else if (hitNE[j].collider.tag == "Node")
                        {
                            nodeList[i].GetComponent<Node>().nodeLinks[1] = hitNE[j].collider.gameObject;
                            Debug.DrawLine(worldPoint, hitNE[j].transform.position, Color.green, 10);
                            break;
                        }
                    }
                }

                //East
                RaycastHit2D[] hitE = Physics2D.LinecastAll(worldPoint, new Vector2(worldPoint.x + .24f, worldPoint.y));

                for (int j = 0; j < hitE.Length; j++)
                {
                    if (hitE[j].collider != null && hitE[j].collider != nodeList[i].GetComponent<Collider2D>() && hitE[j].collider.tag != "Companion")
                    {
                        if (!hitE[j].collider.isTrigger)
                        {
                            Debug.DrawLine(worldPoint, hitE[j].point, Color.red, 10);
                            break;
                        }
                        else if (hitE[j].collider.tag == "Node")
                        {
                            nodeList[i].GetComponent<Node>().nodeLinks[2] = hitE[j].collider.gameObject;
                            Debug.DrawLine(worldPoint, hitE[j].transform.position, Color.green, 10);
                            break;
                        }
                    }
                }


                //Southeast
                RaycastHit2D[] hitSE = Physics2D.LinecastAll(worldPoint, new Vector2(worldPoint.x + .12f, worldPoint.y - .24f));

                for (int j = 0; j < hitSE.Length; j++)
                {
                    if (hitSE[j].transform != null && hitSE[j].collider != nodeList[i].GetComponent<Collider2D>() && hitSE[j].collider.tag != "Companion")
                    {
                        if (!hitSE[j].collider.isTrigger)
                        {
                            Debug.DrawLine(worldPoint, hitSE[j].point, Color.red, 10);
                            break;
                        }

                        else if (hitSE[j].collider.tag == "Node")
                        {
                            nodeList[i].GetComponent<Node>().nodeLinks[3] = hitSE[j].collider.gameObject;
                            Debug.DrawLine(worldPoint, hitSE[j].transform.position, Color.green, 10);
                            break;
                        }
                    }                   
                }


                //South
                RaycastHit2D[] hitS = Physics2D.LinecastAll(worldPoint, new Vector2(worldPoint.x, worldPoint.y - .48f));

                for (int j = 0; j < hitS.Length; j++)
                {
                    if (hitS[j].transform != null && hitS[j].collider != nodeList[i].GetComponent<Collider2D>() && hitS[j].collider.tag != "Companion")
                    {
                        if (!hitS[j].collider.isTrigger)
                        {
                            Debug.DrawLine(worldPoint, hitS[j].point, Color.red, 10);
                            break;
                        }
                        else if (hitS[j].transform.tag == "Node")
                        {
                            nodeList[i].GetComponent<Node>().nodeLinks[4] = hitS[j].collider.gameObject;
                            Debug.DrawLine(worldPoint, hitS[j].transform.position, Color.green, 10);
                            break;
                        }
                    }

                }

                //Southwest
                RaycastHit2D[] hitSW = Physics2D.LinecastAll(worldPoint, new Vector2(worldPoint.x - .12f, worldPoint.y - .24f));

                for (int j = 0; j < hitSW.Length; j++)
                {
                    if (hitSW[j].collider != null && hitSW[j].collider != nodeList[i].GetComponent<Collider2D>() && hitSW[j].collider.tag != "Companion")
                    {
                        if (!hitSW[j].collider.isTrigger)
                        {
                            Debug.DrawLine(worldPoint, hitSW[j].point, Color.red, 10);
                            break;
                        }
                        else if (hitSW[j].collider.tag == "Node")
                        {
                            nodeList[i].GetComponent<Node>().nodeLinks[5] = hitSW[j].collider.gameObject;
                            Debug.DrawLine(worldPoint, hitSW[j].transform.position, Color.green, 10);
                            break;
                        }
                    }
                }

                //West
                RaycastHit2D[] hitW = Physics2D.LinecastAll(worldPoint, new Vector2(worldPoint.x - .24f, worldPoint.y));

                for (int j = 0; j < hitW.Length; j++)
                {
                    if (hitW[j].collider != null && hitW[j].collider != nodeList[i].GetComponent<Collider2D>() && hitW[j].collider.tag != "Companion")
                    {
                        if (!hitW[j].collider.isTrigger)
                        {
                            Debug.DrawLine(worldPoint, hitW[j].point, Color.red, 10);
                            break;
                        }
                        else if (hitW[j].collider.tag == "Node")
                        {
                            nodeList[i].GetComponent<Node>().nodeLinks[6] = hitW[j].collider.gameObject;
                            Debug.DrawLine(worldPoint, hitW[j].transform.position, Color.green, 10);
                            break;
                        }
                    }
                }

                //Northwest
                RaycastHit2D[] hitNW = Physics2D.LinecastAll(worldPoint, new Vector2(worldPoint.x - .12f, worldPoint.y + .24f));

                for (int j = 0; j < hitNW.Length; j++)
                {
                    if (hitNW[j].collider != null && hitNW[j].collider != nodeList[i].GetComponent<Collider2D>() && hitNW[j].collider.tag != "Companion")
                    {
                        if (!hitNW[j].collider.isTrigger)
                        {
                            Debug.DrawLine(worldPoint, hitNW[j].point, Color.red, 10);
                            break;
                        }
                        else if (hitNW[j].collider.tag == "Node")
                        {
                            nodeList[i].GetComponent<Node>().nodeLinks[7] = hitNW[j].collider.gameObject;
                            Debug.DrawLine(worldPoint, hitNW[j].transform.position, Color.green, 10);
                            break;
                        }
                    }
                }

                //Check if the node has any links
                bool isValid = false;
                for (int j = 0; j < nodeList[i].GetComponent<Node>().nodeLinks.Length; j++)
                {
                    if (nodeList[i].GetComponent<Node>().nodeLinks[j] != null)
                    {
                        isValid = true;
                        break;
                    }
                }
                if (!isValid)
                {
                    nodeList[i].GetComponent<Node>().valid = false;
                    //nodeList[i].GetComponent<SpriteRenderer>().color = new Color(.5f, .5f, .5f);
                }

            }
        }
            
    }
}
