using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class Dialogue_Choice : MonoBehaviour
{
    bool active = false;
    public List<GameObject> choice = new List<GameObject>();
    public int padY = 0;

    GameObject cursor;
    public GameObject main;

	// Use this for initialization
	void Start ()
    {
        active = false;
        if (GameController.xbox360Enabled())
            UpdateCursor();

	}
	
	// Update is called once per frame
	void Update ()
    {
		if(GameController.xbox360Enabled())
        {
            if(InputManager.MainVertical() < 0 && !active)
            {
                active = true;
                padY++;
                if (padY > choice.Count - 1)
                {
                    padY = 0;
                }
                UpdateCursor();
                StartCoroutine(padBuffer());
            }
            else if(InputManager.MainVertical() > 0 && !active)
            {
                active = true;
                padY--;
                if (padY < 0)
                {
                    padY = choice.Count - 1;
                }
                UpdateCursor();
                StartCoroutine(padBuffer());
            }

            if(InputManager.A_Button())
            {
                //GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                active = false;
                var pointer = new PointerEventData(EventSystem.current);
                ExecuteEvents.Execute(choice[padY].gameObject, pointer, ExecuteEvents.pointerClickHandler);
            }
        }
	}

    public void UpdateCursor()
    {
        Destroy(cursor);
        GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
        switch(padY)
        {
            default:
                break;
            case 0:
                cursor = Instantiate(Resources.Load<GameObject>("Prefabs/Inventory/Inv_Cursor_ShopBuy"), choice[0].transform);
                break;
            case 1:
                cursor = Instantiate(Resources.Load<GameObject>("Prefabs/Inventory/Inv_Cursor_ShopBuy"), choice[1].transform);
                break;
            case 2:
                cursor = Instantiate(Resources.Load<GameObject>("Prefabs/Inventory/Inv_Cursor_ShopBuy"), choice[2].transform);
                break;
        }
    }

    IEnumerator padBuffer()
    {
        yield return new WaitForSeconds(.5f);
        active = false;
    }
}
