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
    public int inventorySizeLimit = 36; //This is the max size of the inventory
    public List<Item> _items = new List<Item>(); //This create a a list against the inventory GameObject that will accept the scripable object "Item"
    public List<SlotScriptV2> _slots = new List<SlotScriptV2>(); //This is a list of all slots in the inventory. The slots are linked to this list via the unity inspector


    //Properties


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


    private bool PlaceInStackSlot(Item item)
    {
        foreach (SlotScriptV2 slot in _slots)
        {
            if (slot.AddItemToStack(item))
            {
                return true;
            }
        }

        return false;
    }

}