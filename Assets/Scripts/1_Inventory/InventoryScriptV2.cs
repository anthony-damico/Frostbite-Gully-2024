using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScriptV2 : MonoBehaviour
{

    //Singleton Pattern prevents mutiple instances of the class. In this case the inventory
    #region Singleton

    public static InventoryScriptV2 instance;

    private void Awake()
    {
        if (instance != null) //If the instance is not null, that means an instance of the inventory is active
        {
            Debug.LogWarning("More then once instance of the inventory has been found");
            return;
        }
        instance = this;
    }

    #endregion


    //Variables
    //This is for Debugging
    [SerializeField]
    public int inventorySizeLimit = 36; //This is the max size of the inventory. This is probably not needed since each slot looks after itselfs
    public List<Item> _items = new List<Item>(); //This create a a list against the inventory GameObject that will accept the scripable object "Item"
    public List<SlotScriptV2> _slots = new List<SlotScriptV2>(); //This is a list of all slots in the inventory. The slots are linked to this list via the unity inspector
    private SlotScriptV2 _fromSlot; //This is used when moving an Item. When the player clicks on a slot, we can access the methods of the slot as needed. Is referenced in the property FromSlot


    //Properties

    //The fromSlot will always be the item that is being carried by the hand. This function will grey out the Slot if the item is being moved
    public SlotScriptV2 fromSlot
    {
        get { return _fromSlot; }
        
        set
        {
            _fromSlot = value;
            if (value != null) //If an item has been clicked, the _fromSlot is considered in use
            {
                _fromSlot.MySlotIcon.color = Color.grey; //set the slot/icon colour to grey
            }
        }
    }

    //Unity Methods


    //Methods

    /// <summary>
    /// Add an Item to the Inventory. Takes a parameter of "item"
    /// This might have to become a bool at a later date just incase i am adding items picked up off the ground
    /// </summary>
    /// <param name="item"></param>
    public void AddItemToInventory(Item item)
    {
        //Add to stack
        if (item.MyStackSize > 0)
        {
            if (PlaceInStackSlot(item) == true) //If placeInStack returns true, return from the method
            {
                return;
            }
        }

        //If can't add to stack, add to empty slot
        PlaceInEmptySlot(item);
    }

    //Remove Items
    public void RemoveItemFromInventory(Item item)
    {
        _items.Remove(item); //Remove is a build in function of the List Type. It will remove whatever Object is specified as long as the Object Type matches the list type
    }

    //Move Items


    //This method is called if the AddItemToInventory Method determines that an item needs to be added to a new empty slot.
    private bool PlaceInEmptySlot(Item item)
    {
        //Check each slot, to determine if an item can be added
        foreach (SlotScriptV2 slot in _slots)
        {
            Debug.Log("Debug " + slot.name); //Remove This later
            Debug.Log("Debug IsEmpty: " + slot.isEmpty); //Remove this later
            if (slot.isEmpty == true) //isEmpty is located on SlotScriptV2. If isEmpty is true, proceed to add the item to the slot
            {
                slot.AddItemToSlot(item); //If the slot is empty, add the item and return true if the item was added
                return true;
            }
        }

        return false; //If the above paths do not return a true, the we return false. In this case, after each slot is checked, if no slots return true, we return fasle as we could not add an Item
    }

    //This method is called if the AddItemToInventory Method determines that an item can be stacked  on to an exisiting slot. Return's True if the item was added to a existing slot successfully
    private bool PlaceInStackSlot(Item item)
    {
        foreach (SlotScriptV2 slot in _slots) //Check each slot in the inventory
        {
            if (slot.AddItemToStack(item)) //If AddItemToStack returns true, return from this method reporting TRUE
            {
                return true; //Return True if the item was added to Stack
            }
        }

        return false; //Return false if item was not added to stack
    }

}