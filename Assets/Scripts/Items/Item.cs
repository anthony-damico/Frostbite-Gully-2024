using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//This script will be the blueprint for Items

    //local private variables will be named as _variablename
    //properties returning values from a private variable will be name MyVariableName

public abstract class Item : ScriptableObject, IMoveable
{

    //new public string name = "New Item";    //by default every gameobject has a name, by adding "new" in front of public, it allows you to override the default
    
    [SerializeField]
    private Sprite _icon;              //This will be the sprite used as the icon for the item

    [SerializeField]
    private int _stackSize; //This will determine if a item can stack. If it is a 0 or 1, the item is not stackable

    //An item needs a reference to the slot it is sitting on. This is how the item knows what slot it is sitting on
    private SlotScript _slot; //Will be depricated

    private SlotScriptV2 _InventorySlot; //An item needs a reference to the slot it is sitting on. This is how the item knows what slot it is sitting on

    public Sprite MyIcon
    {
        get
        {
            return _icon;
        }
    }

    //This allows other class to GET the stackSize which is private in the Item Class
    public int MyStackSize
    {
        get
        {
            return _stackSize;
        }

    }


    //This property will be depricated
    //A Slot needs a GET and SET. We need to beable to get the Item in the Slot, but we also need to beable to see a new item to a slot. This is how the item knows what slot it is sitting on
    public SlotScript MySlot
    {
        get
        {
            return _slot;
        }
        set
        {
            _slot = value;
        }
    }


    //A Slot needs a GET and SET. We need to beable to get the Item in the Slot, but we also need to beable to see a new item to a slot. This is how the item knows what slot it is sitting on
    public SlotScriptV2 MyInventorySlot
    {
        get
        {
            return _InventorySlot;
        }
        set
        {
            _InventorySlot = value;
        }
    }


    public virtual void UseItem()
    {

    }

    //Whenever an Item is used, this remove function will allow the item to remove itself from the inventory. Any Items that inherit from this script will have this functionality
    public void Remove()
    {
        if(MySlot != null) //If an item exists on the slot
        {
            MySlot.RemoveItem(this); //This is this item or gameobject (As in Item.cs)
        }
    }


}
