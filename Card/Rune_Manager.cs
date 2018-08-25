using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class Rune_Manager : MonoBehaviour
{
    public string loadedCard;
    public int loadedCost;


    public Text cardName;
    public Image cardImage;
    public GameObject runeFade;
    public GameObject runeFrame, unlockFrame, socketFrame;
    public GameObject runePanel;
    public int cardID;

    public Button confirm, yes, no;

    public Text runeTypeText, costText;

    public GameObject selectedSocket;

    //Tooltip
    public GameObject tooltip;

    //Red Stat Bar
    public GameObject redStatFrame;
    public GameObject damageBar, velocityBar, knockbackBar;

    //Blue Stat Bar
    public GameObject blueStatFrame;

    public GameObject runeTooltip;

    // Use this for initialization
    void Start ()
    {
        yes.onClick.AddListener(YesUnlock);
        no.onClick.AddListener(NoUnlock);

        confirm.onClick.AddListener(Confirm);

        GenerateSockets(cardID);
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateRunes();
        HoverCard();
        HoverRune();
	}

    void HoverRune()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = Camera.main.transform.position.z;
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Inv_Slot")
            {
                runeTooltip.SetActive(true);
                Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                runeTooltip.transform.position = new Vector3(newPos.x, newPos.y, 0);

                runeTooltip.transform.Find("Tooltip_Frame").transform.Find("Card_Name").GetComponent<Text>().text = GetComponent<CardDatabase>().FindCard(cardID).cardName;

                runeTooltip.transform.Find("Tooltip_Frame").transform.Find("Card_Name").GetComponent<Text>().text = hit.collider.gameObject.GetComponent<Rune_Info>().runeName;
                runeTooltip.transform.Find("Tooltip_Frame").transform.Find("Card_Desc").GetComponent<Text>().text = hit.collider.gameObject.GetComponent<Rune_Info>().runeDesc;
            }
            else
            {
                runeTooltip.SetActive(false);
            }
        }
    }

    void HoverCard()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = Camera.main.transform.position.z;
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        if (hit.collider != null)
        {
            if(hit.collider.tag == "Spell_Card")
            {
                tooltip.SetActive(true);
                Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                tooltip.transform.position = new Vector3(newPos.x, newPos.y, 0);

                tooltip.transform.Find("Tooltip_Frame").transform.Find("Card_Name").GetComponent<Text>().text = GetComponent<CardDatabase>().FindCard(cardID).cardName;
                string eleType = "";
                string desc = "";
                GetComponent<CardTooltipDatabase>().FindCardInfo(GetComponent<CardDatabase>().FindCard(cardID).cardName, ref eleType, ref desc);

                tooltip.transform.Find("Tooltip_Frame").transform.Find("Element_Type").GetComponent<Text>().text = eleType;
                tooltip.transform.Find("Tooltip_Frame").transform.Find("Card_Desc").GetComponent<Text>().text = desc;

                 //Card Type Image
                tooltip.transform.Find("Tooltip_Frame").transform.Find("Card_Type_IMG").GetComponent<Image>().sprite = Resources.Load<Sprite>("Cards/Icon/" + eleType);
                
                //Clear any generated sockets
                foreach (GameObject socket in GameObject.FindGameObjectsWithTag("Inv_Garbage"))
                {
                    Destroy(socket);
                }
                

                //Rune Images
                for (int i = 0; i < GetComponent<Rune_Database>().FindRune(cardID).runes.Count; i++)
                {
                    if (GetComponent<Rune_Database>().FindRune(cardID).equippedRunes[i] > -1)
                    {
                        if (GetComponent<Rune_Database>().FindRune(cardID).runes[i] == 1)
                        {
                            GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/DeckBuilder/Tooltip_Rune/Red_Tooltip_Rune"), tooltip.transform.Find("Socketed_Runes").transform);
                            tempObj.transform.Find("Rune").GetComponent<Image>().sprite = Rune_Database.redSocketData[GetComponent<Rune_Database>().FindRune(cardID).equippedRunes[i]].transform.Find("Rune_IMG").GetComponent<Image>().sprite;
                        }
                        else
                        {
                            GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/DeckBuilder/Tooltip_Rune/Blue_Tooltip_Rune"), tooltip.transform.Find("Socketed_Runes").transform);
                        }

                    }
                    else
                    {
                        if (GetComponent<Rune_Database>().FindRune(cardID).runeUnlocks[i])
                        {
                            if (GetComponent<Rune_Database>().FindRune(cardID).runes[i] == 1)
                            {
                                GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/DeckBuilder/Tooltip_Rune/Red_Tooltip_Rune"), tooltip.transform.Find("Socketed_Runes").transform);
                                tempObj.transform.Find("Rune").gameObject.SetActive(false);
                            }
                            else
                            {
                                GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/DeckBuilder/Tooltip_Rune/Blue_Tooltip_Rune"), tooltip.transform.Find("Socketed_Runes").transform);
                                tempObj.transform.Find("Rune").gameObject.SetActive(false);
                            }
                        }
                        else
                        {
                            if (GetComponent<Rune_Database>().FindRune(cardID).runes[i] == 1)
                            {
                                GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/DeckBuilder/Tooltip_Rune/Red_Tooltip_Rune_Locked"), tooltip.transform.Find("Socketed_Runes").transform);
                            }
                            else
                            {
                                GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/DeckBuilder/Tooltip_Rune/Blue_Tooltip_Rune_Locked"), tooltip.transform.Find("Socketed_Runes").transform);
                            }
                        }
                    }
                }

            }

            else
            {
                tooltip.SetActive(false);
            }
        }

    }

    public void UpdateStat()
    {
        Rune selectedRune = GetComponent<Rune_Database>().FindRune(cardID);

        if(selectedRune.cardType > 0)
        {
            //Offensive?
            if(selectedRune.cardType == 1)
            {
                redStatFrame.SetActive(true);

                //Damage Bar
                damageBar.transform.localScale = new Vector2(selectedRune.cardDamage/10f, 1f);
                //damageBar.transform.position = new Vector2(damageBar.transform.position.x + (.17f * (selectedRune.cardDamage / 10f)), damageBar.transform.position.y);

                //Velocity Bar
                velocityBar.transform.localScale = new Vector2(selectedRune.cardVelocity / 10f, 1f);
                //velocityBar.transform.position = new Vector2(velocityBar.transform.position.x + (.17f * (selectedRune.cardVelocity / 10f)), velocityBar.transform.position.y);

                //Knockback Bar
                knockbackBar.transform.localScale = new Vector2(selectedRune.cardKnockback / 10f, 1f);
                //knockbackBar.transform.position = new Vector2(knockbackBar.transform.position.x + (.17f * (selectedRune.cardKnockback / 10f)), knockbackBar.transform.position.y);
            }




        }



    }

    public void GenerateSockets(int id)
    {
        //Clear any generated sockets
        foreach (GameObject socket in GameObject.FindGameObjectsWithTag("SpellBook"))
        {
            Destroy(socket);
        }

        Rune selectedRune = GetComponent<Rune_Database>().FindRune(cardID);
        //Generate sockets based on the card selected
        for (int i = 0; i < selectedRune.runes.Count; i++)
        {
            if(selectedRune.runes[i] == 1)
            {
                GameObject tempObj =  Instantiate(Resources.Load<GameObject>("Prefabs/DeckBuilder/Red_Socket"), socketFrame.transform);
                //Is this socket unlocked?
                if (!selectedRune.runeUnlocks[i])
                {
                    tempObj.transform.Find("Unlock").gameObject.SetActive(true);
                    tempObj.transform.Find("Unlock").GetComponent<Button>().onClick.AddListener(UnlockRune);
                }
                else
                {
                    tempObj.transform.Find("Unlock").gameObject.SetActive(false);
                }
                //Is there something socketed?
                if (selectedRune.equippedRunes[i] >= 0)
                {
                    tempObj.transform.Find("Rune").gameObject.SetActive(true);
                    tempObj.transform.Find("Rune").GetComponent<Image>().sprite = Rune_Database.redSocketData[selectedRune.equippedRunes[i]].transform.Find("Rune_IMG").GetComponent<Image>().sprite;
                    tempObj.transform.Find("Rune").GetComponent<Button>().onClick.AddListener(ShowRunes);
                }
                else
                {
                    tempObj.transform.Find("Rune").gameObject.SetActive(false);
                    tempObj.transform.Find("Foreground").GetComponent<Button>().onClick.AddListener(ShowRunes);
                }

                //Feed in the socket info
                tempObj.GetComponent<Socket_Info>().socketID = i;
                tempObj.GetComponent<Socket_Info>().runeID = 0;
                tempObj.GetComponent<Socket_Info>().socketColor = 1;
            }
            else if(selectedRune.runes[i] == 2)
            {
                GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/DeckBuilder/Blue_Socket"), socketFrame.transform);
                //Is this socket unlocked?
                if (!selectedRune.runeUnlocks[i])
                {
                    tempObj.transform.Find("Unlock").gameObject.SetActive(true);
                    tempObj.transform.Find("Unlock").GetComponent<Button>().onClick.AddListener(UnlockRune);
                }
                else
                {
                    tempObj.transform.Find("Unlock").gameObject.SetActive(false);             
                }
                //Is there something socketed?
                if (selectedRune.equippedRunes[i] > 0)
                {
                    tempObj.transform.Find("Rune").gameObject.SetActive(true);
                    tempObj.transform.Find("Rune").GetComponent<Button>().onClick.AddListener(ShowRunes);
                }
                else
                {
                    tempObj.transform.Find("Rune").gameObject.SetActive(false);
                }

                tempObj.transform.Find("Foreground").GetComponent<Button>().onClick.AddListener(ShowRunes);

                //Feed in the socket info
                tempObj.GetComponent<Socket_Info>().socketID = i;
                tempObj.GetComponent<Socket_Info>().runeID = 0;
                tempObj.GetComponent<Socket_Info>().socketColor = 2;
            }         
        }
    }

    void UpdateRunes()
    {
        if(GetComponent<CardDatabase>().FindCard(cardID).cardName != loadedCard || GetComponent<Spell_Database>().FindSpell(cardID).spellCost != loadedCost)
        {
            loadedCard = GetComponent<CardDatabase>().FindCard(cardID).cardName;
            loadedCost = GetComponent<Spell_Database>().FindSpell(cardID).spellCost;

            cardName.text = GetComponent<CardDatabase>().FindCard(cardID).cardName;
            cardImage.sprite = Resources.Load<Sprite>("Cards/NewSpell/Arcana_" + GetComponent<CardDatabase>().FindCard(cardID).cardName);

            //Make mana crystals
            foreach(GameObject crystal in GameObject.FindGameObjectsWithTag("Shop_Rest"))
            {
                Destroy(crystal);
            }

            for(int i = 0; i < loadedCost; i++)
            {
                if(loadedCost > 3)
                {
                    Instantiate(Resources.Load("Cards/NewSpell/Cost_Crystal_Full_Craft"), cardImage.transform.Find("Mana_Frame").transform);
                }
                else
                {
                    Instantiate(Resources.Load("Cards/NewSpell/Cost_Crystal_Full_Craft"), cardImage.transform.Find("Mana_Frame_Small").transform);                 
                }
                
            }

        }

    }

    //Add rune to the selected socket
    void AddRune()
    {
        int id = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.GetComponent<Rune_Info>().runeID;

        GetComponent<Rune_Database>().FindRune(cardID).equippedRunes[selectedSocket.GetComponent<Socket_Info>().socketID] = id;

        selectedSocket.GetComponent<Socket_Info>().runeID = id;
        selectedSocket.transform.Find("Rune").gameObject.SetActive(true);
        selectedSocket.transform.Find("Rune").GetComponent<Image>().sprite = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite;
        runeFrame.SetActive(false);
    }

    //Show what runes can be socketed based on the slot clicked
    void ShowRunes()
    {
        runeFrame.SetActive(true);
        selectedSocket = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        //Clear any generated runes
        foreach (GameObject rune in GameObject.FindGameObjectsWithTag("Inv_Slot"))
        {
            Destroy(rune);
        }
        //Red?
        if (selectedSocket.GetComponent<Socket_Info>().socketColor == 1)
        {
            for (int i = 0; i < Rune_Database.unlockedRedRunes.Count; i++)
            {
                if (Rune_Database.unlockedRedRunes[i])
                {
                    GameObject tempObj = Instantiate(Rune_Database.redSocketData[i], runePanel.transform);
                    tempObj.GetComponent<Rune_Info>().runeID = i;
                    tempObj.GetComponent<Rune_Info>().runeName = Rune_Database.socketData[i].runeName;
                    tempObj.GetComponent<Rune_Info>().runeDesc = Rune_Database.socketData[i].runeDesc;
                    tempObj.transform.Find("Rune_IMG").GetComponent<Button>().onClick.AddListener(AddRune);
                }
            }
        }
        //Blue?
        else if (selectedSocket.GetComponent<Socket_Info>().socketColor == 2)
        {

        }


    }

    //Unlock the rune socket that is clicked
    void UnlockRune()
    {
        selectedSocket = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        unlockFrame.SetActive(true);
    }

    //If the confirm button is pressed
    void Confirm()
    {
        unlockFrame.SetActive(false);
        runeFrame.SetActive(false);
        runeFade.SetActive(false);
        gameObject.SetActive(false);
    }

    //If the yes button is pressed
    void YesUnlock()
    {
        //Update the cost of the unlock


        //Update the socket
        selectedSocket.transform.Find("Unlock").gameObject.SetActive(false);
        GetComponent<Rune_Database>().FindRune(cardID).runeUnlocks[selectedSocket.GetComponent<Socket_Info>().socketID] = true;

        unlockFrame.SetActive(false);
    }

    //If the no button is pressed
    void NoUnlock()
    {
        unlockFrame.SetActive(false);
    }
}
