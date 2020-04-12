using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class DebugManagerScript : MonoBehaviour
{

    //References to Other Classes/Scripts via Singleton
    TimeManagerController timeManagerController; //Creates a reference back to the TimeManagerControllerScript to get access to the different time, day and season variables
    InventoryScript inventoryScript; //Create a referene back to the Inventory Script so i can add items to the inventory easily.


    public bool _active = false;

    public int xpos; //Set in the unity editor
    public int ypos; //Set in the unity editor

    private void Start()
    {
        timeManagerController = TimeManagerController.instance; //Completes the reference back to the TimeManagerController.cs script using the singleton
        inventoryScript = InventoryScript.instance; //Completes the reference back to the InventoryScript.cs script using the singleton
    }

    private void Update()
    {		
        //Open Debug Menu
        if (Input.GetKeyDown(KeyCode.T))
        {
			//if(_active == true ? false : true); 

            if(_active == true)
            {
                _active = false;
            }
            else
            {
                _active = true;
            }
        }
    }

    void OnGUI()
    {
        if (_active)
        {

            GUILayout.BeginArea(new Rect(xpos, ypos, 200, 200));
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();

            DisplayCheat("Close Cheat Menu", () => _active = false);
            DisplayCheat("Skip Day", () => DebugSkipTime());
            DisplayCheat("Add Items To Inventory", () => DebugAddItemsToInventory());
            DisplayCheat("Add Corn To Inventory", () => DebugAddCornToInventory());
            DisplayCheat("Create 20 Slot Bag", () => DebugCreate20SlotBag());

            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndArea();

        }
    }

    private void DisplayCheat(string cheatName, Action clickedCallback)
    {
        if (GUILayout.Button("Debug: " + cheatName))
        {
            clickedCallback();
        }
    }


    //Debugs
    //Skip a day in game time
    void DebugSkipTime()
    {
        timeManagerController.hours = 5;
        timeManagerController.tenMinutes = 5;
        timeManagerController.minutes = 5;
        timeManagerController.days++;
        timeManagerController.date++;
        timeManagerController.UpdateTime();
    }

    //Add items to inventory
    void DebugAddItemsToInventory()
    {
        //The below is used as a debug to add a bag item to the inventory
        Bag bag = (Bag)Instantiate(inventoryScript._items[2]); //This initizes the Item in slot 0 in the item array
        bag.Initialize(bag.Slots); //This calls the Initialie method which is used to allocate the bag slot size. Refer to Bag.cs for more details
        inventoryScript.AddItem(bag);

        Equipment tool = (Equipment)Instantiate(inventoryScript._items[3]); //This initizes the Item in slot 3 in the item array
        inventoryScript.AddItem(tool);

        tool = (Equipment)Instantiate(inventoryScript._items[4]); //This initizes the Item in slot 4 in the item array
        inventoryScript.AddItem(tool);

        tool = (Equipment)Instantiate(inventoryScript._items[5]); //This initizes the Item in slot 5 in the item array
        inventoryScript.AddItem(tool);

        tool = (Equipment)Instantiate(inventoryScript._items[6]); //This initizes the Item in slot 6 in the item array
        inventoryScript.AddItem(tool);

        tool = (Equipment)Instantiate(inventoryScript._items[7]); //This initizes the Item in slot 7 in the item array
        inventoryScript.AddItem(tool);

        tool = (Equipment)Instantiate(inventoryScript._items[8]); //This initizes the Item in slot 8 in the item array
        inventoryScript.AddItem(tool);

        tool = (Equipment)Instantiate(inventoryScript._items[9]); //This initizes the Item in slot 9 in the item array
        inventoryScript.AddItem(tool);

        tool = (Equipment)Instantiate(inventoryScript._items[10]); //This initizes the Item in slot 10 in the item array
        inventoryScript.AddItem(tool);
    }

    //Add Corn Item to Inventory
    void DebugAddCornToInventory()
    {
        //The below is used as a debug to add a bag item to the inventory
        Corn corn = (Corn)Instantiate(inventoryScript._items[1]); //This initizes the Item in slot 0 in the item array
        inventoryScript.AddItem(corn);
    }

    //Add a 20 Slot Bag
    void DebugCreate20SlotBag()
    {
        //The below is used as a debug to add a bag item to the inventory
        Bag bag = (Bag)Instantiate(inventoryScript._items[0]); //This initizes the Item in slot 0 in the item array
        bag.Initialize(20); //This calls the Initialie method which is used to allocate the bag slot size. Refer to Bag.cs for more details
        bag.Use(); //This uses the bag to equip all the slots from the bag
    }

}
