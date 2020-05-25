using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class SlotScriptV2 : MonoBehaviour
{
    //Future Anthony, If your method/Variable is using a get/set, it is a property. If you declare a public bool <Name> {} with no get or set, it is a method.
    //Local Variables to this script will be prefixed with an underscore. EG _item
    //Properties to this script will be prefixed as with "my" eg myItem

    //Variables
    [SerializeField] private Stack<Item> _items = new Stack<Item>(); //Stack is an event that keeps track of each stack on a slot
    [SerializeField] private Image _icon; //This is the image that sits on the slot
    [SerializeField] private Text _stackSizeText; //This is mapped in the UI against the slot prefab

    //Properties

    //This getter property will allow other classes and methods to change the icon on a slot
    public Image MySlotIcon
    {
        get { return _icon; }
        set { _icon = value; }
    }

    //This getter property will allow other classes and methods to access the "items" on the Obserablestack _items via the variable known as slotItems
    public Stack<Item> slotItems
    {
        get { return _items; }

    }

    //This getter property will return true if the _item stack on a slot is 0. This means the slot is empty. If it returns false, the slot is not empty.
    public bool isEmpty
    {
        get { return slotItems.Count == 0; } //If the count is 0, this means the slot is empty becasue there are no items in the stack on the slot. this will return true
    }


    //This getter property will check if the slot is empity (using isEmpty Property) OR it will check if the slot item is less then the items max stack count. 
    //It will return false if either of these crietreas are true. THis is important when trying to Merge 2 stacks of items together
    public bool isFull
    {
        get
        {
            //If the slot is emply OR the count of the max stack size is less then the max count then this slot is free to have the item added to it
            if (isEmpty || slotItemCount < MyItem.MyStackSize)
            {
                return false; //This means the slot is empty. Meaning the slot is not full allowing items to be stacks
            }

            return true;
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

    public int slotItemCount
    {
        get { return slotItems.Count; }
    }


    //Unity Methods

    //Methods
  
    //Add Item to Slot
    //This will return true when an item has been added to the slot
    public bool AddItemToSlot(Item item)
    {
        slotItems.Push(item); //This adds the item to the stack _items
        _icon.sprite = item.MyIcon; //This sets the icon on the slot equal to the icon from scriptable object Item
        _icon.color = Color.white; //set the icon UI image to white when an item has been added (This is to remove the alpha that hides the image allowing the image allocated in the previous line to display)
        updateStackCount(); //Update the Text UI Element on the slot to represent the number of items on the slot
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
            updateStackCount(); //Update the Text UI Element on the slot to represent the number of items on the slot
            return true; //When returned true, means we can stack the item
        }

        return false; //When returned false, means we can't stack the item
    }

    //Use Items (Is called from the slot UI Button as a UnityEvent)
    public void UseItem()
    {
        if(isEmpty == false)
        {
            MyItem.UseItem();
        }
        
    }

    //RemoveItem is called whenever an item needs to be removed from a slot. EG it has been used or trashed from the inventory. This is called from the item.cs script when the slotscripv2.UseItem Method is called
    public void RemoveItem(Item item)
    {
        if (!isEmpty) //If the slot is not empty. Remove the item from the inventory using Pop(); This is a function to remove items from the stack
        {
            slotItems.Pop();
            updateStackCount();
        }
    }

    //Clear Stack to Empty. Clear() is a function of a stack
    public void ClearSlot()
    {
        if (slotItemCount > 0) //If there is a minimum of 1 item on the slot
        {
            slotItems.Clear(); //Clear the slot using the stack function Clear()
            updateStackCount(); // Update the stack count to reflect the changes
        }
    }

    //Move Item
    public void MouseMoveItem()
    {
        //Check one, Make sure there is nothing in the hand (mouse). This is determined by the variable _fromSlot in the inventoryScript
        //Check two is to make sure the slot being clicked on is not empty
        if (InventoryScriptV2.instance.fromSlot == null && !isEmpty) //if we don't have something to move
        {
            MouseHandFunctionScript.instance.TakeMoveable(MyItem as IMoveable); //An item can only be moved if it is IMoveable
            InventoryScriptV2.instance.fromSlot = this; //"this" is whatever was just clicked on
        }
    }

    //This method will be used to put an item back into the inventory
    public bool PutItemBackOnSlot()
    {
        if (InventoryScriptV2.instance.fromSlot == this) //This means i have clicked on the same slot that i am trying to move from
        {
            InventoryScriptV2.instance.fromSlot.MySlotIcon.color = Color.white; //this sets the colour of the slot back to normal
            return true;
        }

        return false;
    }

    //This method is used when moving an item to another slot. If you move the item from a hand to a slot of the same item type, it will try to merge them together
    public bool MergeItems(SlotScript fromHand) //fromHand is the item in the handfunction
    {

        if (isEmpty)
        {
            return false; //If false, there is no need to merge items
        }

        //The item in the hand matches the item on the slot
        //AND there is enough room on the slot to merge
        if (fromHand.MyItem.GetType() == MyItem.GetType() && !isFull)  
        {
            int freeSlots = MyItem.MyStackSize - slotItemCount; //this determines how many free slots do we have in the stack

            //this for loop has to run for how many free slots in the stack are avalible
            for (int i = 0; i < freeSlots; i++)
            {
                AddItemToSlot(fromHand.MyItems.Pop());
            }

            return true; //return true if successful
        }

        return false; //if unsuccessful

    }






    public void updateStackCount()
    {
        if(slotItemCount >= 1)
        {
            _stackSizeText.text = slotItemCount.ToString(); //Set the text UI Element on the Slot equal to the count if Item in the stack on the slot
            _stackSizeText.color = Color.white; //If the colour of the text is hidden due to alpha being set to none  or not white, set it to white so it is now visiable
            MySlotIcon.color = Color.white; //If the colour of the Icon is hidden due to alpha being set to none or not white, set it to white so it is now visiable
        }
        else if (slotItemCount <= 0)
        {
            _stackSizeText.color = new Color(0, 0, 0, 0); //This sets the text on the slot to have no colour and no alpha which makes it invisible. Items with a stacksize of 0 do not need a stacksize since there is no item on the slot
            MySlotIcon.color = new Color(0, 0, 0, 0); //This sets the text on the slot to have no colour and no alpha which makes it invisible. Items with a stacksize of 0 do not need a stacksize since there is no item on the slot
        }
    }
}



// This class is inside the TestClass so it could access its private fields
// this custom editor will show up on any object with TestScript attached to it
// you don't need (and can't) attach this class to a gameobject
[CustomEditor(typeof(SlotScriptV2))]
public class StackPreview : Editor
{
    public override void OnInspectorGUI()
    {

        // get the target script as TestScript and get the stack from it
        var ts = (SlotScriptV2)target;
        var stack = ts.slotItems;

        // some styling for the header, this is optional
        var bold = new GUIStyle();
        bold.fontStyle = FontStyle.Bold;
        GUILayout.Label("Items in my stack", bold);

        // add a label for each item, you can add more properties
        // you can even access components inside each item and display them
        // for example if every item had a sprite we could easily show it 
        foreach (var item in stack)
        {
            GUILayout.Label(item.name);
        }
    }
}
