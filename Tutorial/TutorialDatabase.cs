using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDatabase : MonoBehaviour
{
    //Training 1 Basic Movement
    public static bool tut1 = false;

    //Training 2 Equip Weapon
    public static bool tut2 = false;
    public static bool tut2_A = false;
    public static bool tut3 = false;
    public static bool tut3_A = false;

    //Training 2 Attack Tutorial
    public static bool tut4 = false;

    //Training 2 DeckBuilding Tutorial
    public static bool tut5 = false;
    public static bool tut6 = false;
    public static bool tut7 = false;

    //Training 2 Arcana
    public static bool tut8 = false;
    public static bool tut9 = false;

    //Training 3 Dodge
    public static bool tut10 = false;



    /*
    1. Player stash
    2. Inn Bed
    3. Hero Soul
    4. Basic Combat
    5. Arcana Combat
    6. Equip Items

    */
    public List<string> tutorialList = new List<string>();

    void Awake()
    {
        //Player Stash
        tutorialList.Add("Everytime you visit an Inn you will have access to your stash, a treasure chest located in every Inn room. You can use your stash to store items or withdraw stores items. To interact with the stash press [F] when you are near it.");
        //Inn Bed
        tutorialList.Add("Sleeping the bed at the Inn will retore your health and mana to max. Additionally, sleeping will allow you to save your game as well as access your Hero Soul.");
        //Hero Soul
        tutorialList.Add("Congratulations you've obtained the Hero Soul! The Hero Soul allows you to gain special talents as you level up. You can access your Hero Soul in any Inn.");
        //Basic Combat
        tutorialList.Add("By default, [LMB] initiates a light attack while [RMB] initiates your character's special attack. Both of these attacks use stamina.");
        //Arcana Combat
        tutorialList.Add("Some enemies cannot be hurt by normal attacks and must be damaged using Arcana. By default, [Q] or [MMB] uses the current Arcana Card you have selected, which is shown in the bottom left on your HUD. To toggle between cards you can either use the mouse wheel or the [C] and [V] keys.");
        //Equipping Items
        tutorialList.Add("Some items can be worn or equipped by your character, such as Weapons and Armor. To open your equipment menu, first open your inventory by pressing [I]. Then click on the armor tab on the left of the inventory panel. Drag and drop the items you wish to equip.");


    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
