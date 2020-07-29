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

    //This getter property will allow other classes and methods to change the icon on a slot
    public Text MySlotStackSizeText
    {
        get { return _stackSizeText; }
        set { _stackSizeText = value; }
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
        InventoryScriptV2.instance.updateStackCount(this); //Update the Text UI Element on the slot to represent the number of items on the slot
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
            InventoryScriptV2.instance.updateStackCount(this); //Update the Text UI Element on the slot to represent the number of items on the slot
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
            InventoryScriptV2.instance.updateStackCount(this);
        }
    }

    //Clear Stack to Empty. Clear() is a function of a stack
    public void ClearSlot()
    {
        if (slotItemCount > 0) //If there is a minimum of 1 item on the slot
        {
            slotItems.Clear(); //Clear the slot using the stack function Clear()
            InventoryScriptV2.instance.updateStackCount(this); // Update the stack count to reflect the changes
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
        else if (InventoryScriptV2.instance.fromSlot != null) //If we have something to move
        {
            //First you just want to try and put the item back (if you clicked on the same slot
            //the second check is to try and merge
            //The third check you want to do is to try and swap the items on the slots
            //The fouth check is put the item on a slot
            if (PutItemBackOnSlot() == true || MergeItems(InventoryScriptV2.instance.fromSlot) || SwapItems(InventoryScriptV2.instance.fromSlot) || AddItemsToNewSlot(InventoryScriptV2.instance.fromSlot.slotItems))
            {
                MouseHandFunctionScript.instance.DropMoveable();
                InventoryScriptV2.instance.updateStackCount(this);
                InventoryScriptV2.instance.updateStackCount(InventoryScriptV2.instance.fromSlot);
                InventoryScriptV2.instance.fromSlot = null; //this resets the fromSlot to allow the user to pick up another item
            }

        }

    }


    //Move Item
    public void ControllerMoveItem()
    {
        //Check one, Make sure there is nothing in the hand (mouse). This is determined by the variable _fromSlot in the inventoryScript
        //Check two is to make sure the slot being clicked on is not empty
        if (InventoryScriptV2.instance.fromSlot == null && !isEmpty) //if we don't have something to move
        {
            ControllerHandFunctionScript.instance.TakeMoveable(MyItem as IMoveable); //An item can only be moved if it is IMoveable
            InventoryScriptV2.instance.fromSlot = this; //"this" is whatever was just clicked on
        }
        else if (InventoryScriptV2.instance.fromSlot != null) //If we have something to move
        {
            //First you just want to try and put the item back (if you clicked on the same slot
            //the second check is to try and merge
            //The third check you want to do is to try and swap the items on the slots
            //The fouth check is put the item on a slot
            if (PutItemBackOnSlot() == true || MergeItems(InventoryScriptV2.instance.fromSlot) || SwapItems(InventoryScriptV2.instance.fromSlot) || AddItemsToNewSlot(InventoryScriptV2.instance.fromSlot.slotItems))
            {
                ControllerHandFunctionScript.instance.DropMoveable();
                InventoryScriptV2.instance.updateStackCount(this);
                InventoryScriptV2.instance.updateStackCount(InventoryScriptV2.instance.fromSlot);
                InventoryScriptV2.instance.fromSlot = null; //this resets the fromSlot to allow the user to pick up another item
            }

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
    public bool MergeItems(SlotScriptV2 fromHand) //fromHand is the item in the handfunction
    {

        if (isEmpty) //If isEmpty is True
        {
            return false; //Return false, there is no need to merge items
        }

        //The item in the hand matches the item on the slot
        //AND there is enough room on the slot to merge
        if (fromHand.MyItem.GetType() == MyItem.GetType() && !isFull)  
        {
            int freeSlots = MyItem.MyStackSize - slotItemCount; //this determines how many free slots do we have in the stack

            //this for loop has to run for how many free slots in the stack are avalible
            for (int i = 0; i < freeSlots; i++)
            {
                AddItemToSlot(fromHand.slotItems.Pop());
            }

            return true; //return true if successful added merge the item with a stack
        }

        return false; //if unsuccessful, return false. This will allow the next check to happen in the MouseMoveItem() method or ControllerMoveItem() method
    }

    //This will be used for swapping items between slots
    public bool SwapItems(SlotScriptV2 fromSlot)
    {
        //If the slot we are clicking on is empty (true)
        if (isEmpty == true)
        {
            return false; //This means the slot is empty and we don't have to do anything else
        }

        //If the item moving is differnt to the item on the slot, we will swap the place of the 2 items
        //OR if the count of item is MAX (eg 5 corns on the slot vs 2 corns in the hand) then swap the item as the slot has maxed stack
        if (fromSlot.MyItem.GetType() != MyItem.GetType() || fromSlot.slotItemCount + slotItemCount > MyItem.MyStackSize)
        {

            //Make a copy of all items we need to swap from Slot A/Selected slot into a temporary slot so we don't lose the data/items associated to the slot
            Stack<Item> tmpFrom = new Stack<Item>(fromSlot.slotItems);

            //Now that we have a refernce to the data from Slot A/Selected Slot, Clear all the data/items to make it empty
            fromSlot.slotItems.Clear();

            //Take all items from slot B and copy them into Slot A
            fromSlot.AddItemsToNewSlot(slotItems);

            //clear slot B
            slotItems.Clear();

            //Move items from temporaary stack and copy to slot B
            AddItemsToNewSlot(tmpFrom);

            //Return true becuase the swap was successful
            return true;
        }

        //return false if the swap was not possible
        return false;
    }

    //This function works beside the SwapItems method amd can be used to move datas/items to a new empty slot
    //This function will take an item and place it into a different slot in the inventory
    //This function will also check if you can add an item to an existing stack
    public bool AddItemsToNewSlot(Stack<Item> newItems)
    {
        //If the slot is emply OR the item being moved matches the item being moved to then complete the code within the {}
        if (isEmpty == true || newItems.Peek().GetType() == MyItem.GetType())
        {
            int count = newItems.Count; //this is used for the loop below. This checks the count on all the newItems (The item from the stack that is passed through to it. EG the temporary stack)

            for (int i = 0; i < count; i++)
            {
                //we are checking if the slot is full or not (To see if we can stack)
                if (isFull == true)
                {
                    return false; //this means we can't proceed with adding the item to slot or slotstack
                }

                //else
                AddItemToSlot(newItems.Pop());
            }

            return true; //if we managed to move the items, return true

        }

        return false; //If we are unable to move the items, return false
    }
}
