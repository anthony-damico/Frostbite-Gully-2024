using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotScriptV2 : MonoBehaviour
{
    //Future Anthony, If your method/Variable is using a get/set, it is a property. If you declare a public bool <Name> {} with no get or set, it is a method.
    //Local Variables to this script will be prefixed with an underscore. EG _item
    //Properties to this script will be prefixed as with "my" eg myItem

    //Variables
    [SerializeField] private ObservableStack<Item> _items = new ObservableStack<Item>(); //ObservableStack is an event that keeps track of each stack on a slot
    [SerializeField] private Image _icon; //This is the image that sits on the slot
    [SerializeField] private Text _stackSizeText; //This is mapped in the UI against the slot prefab

    //Properties
    //This getter property will allow other classes and methods to access the "items" on the Obserablestack _items via the variable known as slotItems
    public ObservableStack<Item> slotItems
    {
        get
        {
            return _items;
        }

    }

    //This getter property will return true if the _item stack on a slot is 0. This means the slot is empty. If it returns false, the slot is not empty.
    public bool isEmpty
    {
        get
        {
            return slotItems.Count == 0; //If the count is 0, this means the slot is empty becasue there are no items in the stack on the slot. this will return true
        }
    }

    //This getter property is called when we want to check (Peek) at the current item on the slot. We check the item on the slot and pass it to the property "MyItem" which we can then do extra logic
    //such as checking the Items properties/metadata such as its name and max stack size (located in item.cs)
    public Item MyItem
    {
        get
        {
            if (!isEmpty) //If this slot is not empty. Return the item in the slot. If there is no item, return NULL
            {
                return slotItems.Peek();
            }

            return null;
        }
    }


    //Unity Methods
    //Awake is used as we need to always be listening to the item stack to see if something changes (For now this is disabled until i get placeInEmptySlot working
    //private void Awake()
    //{
    //
    //    //The Below are events being called from the ObservableStack Class
    //    slotItems.OnPop += new UpdateStackEvent(UpdateSlot); //Whenever Pop() is called, this the ObservableStack will be listening and will call UpdateSlot, which will UpdateStackSize
    //    slotItems.OnPush += new UpdateStackEvent(UpdateSlot); //Whenever Push() is called, this the ObservableStack will be listening and will call UpdateSlot, which will UpdateStackSize
    //    slotItems.OnClear += new UpdateStackEvent(UpdateSlot); //Whenever Clear() is called, this the ObservableStack will be listening and will call UpdateSlot, which will UpdateStackSize
    //
    //}

    //Methods

    //Check that a slot is empty

    //Add Item to Slot
    //This will return true when an item has been added to the slot
    public bool AddItemToSlot(Item item)
    {
        slotItems.Push(item); //This adds the item to the stack _items
        _icon.sprite = item.MyIcon; //This sets the icon on the slot equal to the icon from scriptable object Item
        _icon.color = Color.white; //set the icon UI image to white when an item has been added (This is to remove the alpha that hides the image allowing the image allocated in the previous line to display)
        item.MyInventorySlot = this; //This is how an item knows what slot it is on. 
        return true;
    }


    //This function will try to stack the item on the slot currently being interacted with. If it adds the item to the stack, it returns true. If it can't stack, it returns false
    public bool AddItemToStack(Item item)
    {
        //First Check, if the slot is not empty. We are only interested in in stacking on non-empty slots (if isEmpty returns false, the slot is empty)
        //Second Check, we are checking the item we are trying to add to the slot against the item already on the slot (MyItem Property contains all teh information about the item on the slot)  
        //Third Check, Check the count of items on the stack against how many items of the same type can be stacked. This is determined myStackSize on the Item.cs (myStackSize is a property created from _StackSize)
        if (isEmpty == false && item.name == MyItem.name && slotItems.Count < MyItem.MyStackSize)
        {
            slotItems.Push(item);
            item.MyInventorySlot = this; //This tells the current slot what item is on it
            Debug.Log(slotItems.Count);
            return true; //When returned true, means we can stack the item
        }

        return false; //When returned false, means we can't stack the item
    }

    //Remove Item from Slot

    //Move Item From Slot
}
