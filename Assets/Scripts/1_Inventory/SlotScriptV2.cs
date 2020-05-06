using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotScriptV2 : MonoBehaviour
{
    //Variables
    [SerializeField] private ObservableStack<Item> _items = new ObservableStack<Item>(); //ObservableStack is an event that keeps track of each stack on a slot
    [SerializeField] private Image _icon; //This is the image that sits on the slot

    //Properties
    //This getter property will allow other classes and methods to access the Obserablestack _items via the variable known as slotItems
    public ObservableStack<Item> slotItems
    {
        get
        {
            return _items;
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


    //Add Item to Slot
    //This will return true when an item has been added to the slot
    public bool AddItem(Item item)
    {
        slotItems.Push(item); //This adds the item to the stack _items
        _icon.sprite = item.MyIcon; //This sets the icon on the slot equal to the icon from scriptable object Item
        _icon.color = Color.white; //set the icon UI image to white when an item has been added (This is to remove the alpha that hides the image allowing the image allocated in the previous line to display)
        item.MyInventorySlot = this; //This is how an item knows what slot it is on. 
        return true;
    }

    //Remove Item from Slot

    //Move Item From Slot
}
