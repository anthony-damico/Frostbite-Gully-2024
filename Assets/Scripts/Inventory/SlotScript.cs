using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//local private variables will be named as _variablename
//properties returning values from a private variable will be name MyVariableName

public class SlotScript : MonoBehaviour, IPointerClickHandler, IClickable
{

    //A Stack is simillar to an array or a list. However stack has some functions that allow you too add to the stack and review the stack easily
    //private Stack<Item> _items = new Stack<Item>(); //Old Code for reference
    //We no longer have to use the standard stack becasue we created a new ObseravleStack that inherits from stack

    [SerializeField]
    private ObservableStack<Item> _items = new ObservableStack<Item>();

    [SerializeField]
    private Image _icon; //This is the image that sits on the slot

    [SerializeField]
    private Text stackSizeText; //This is mapped in the UI against the slot prefab

    //This will be a reference to the bag this slot belongs to
    public BagScript MyBag { get; set; }

    //This will return true if the _item count is 0. This means the slot is empty
    public bool isEmpty
    {
        get
        {
            return MyItems.Count == 0; //If the count is 0, this means the slot is empty becasue there are no items in the stack on the slot. this will return true
        }
    }

    //isFull will check if the type of item on the slot has a max out stack or if there is room left on the stack
    public bool isFull
    {
        get
        {
            //If the slot is emply OR the count of the max stack size is less then the max count then this slot is free to have the item added to it
            if(isEmpty || MyCount < MyItem.MyStackSize)
            {
                return false; //This means the slot is empty. Meaning the slot is not full allowing items to be stacks
            }

            return true;
        }
    }



    //This is a property
    public Item MyItem
    {
        get
        {
            if (!isEmpty) //If this slot is not empty. Return the item in the slot. If there is no item, return NULL
            {
                return MyItems.Peek();
            }

            return null;
        }
    }

    //This will get return the _icon from the slot whenever a slot is clicked. The _icon can also be set if the _icon has changed
    public Image MyIcon
    {
        get
        {
            return _icon;
        }
        set
        {
            _icon = value;
        }
    }

    //This will return the count of items on a slot allowing the process that are referrecing MySlot to know if there is an item on the slot or not
    public int MyCount
    {
        get
        {
            {return MyItems.Count; }
        }
    }

    //Whenever the stackSizeText needs to be updated, it can be accessed through the property MyStackText
    public Text MyStackText
    {
        get
        {
            return stackSizeText;
        }
    }

    //This property will allow other classes and methods to access the stack _items via MyItems
    public ObservableStack<Item> MyItems
    {
        get
        {
           return _items;
        }

    }



    //This will return true when an item has been added to the slot
    public bool AddItem(Item item)
    {
        MyItems.Push(item); //This adds the item to the stack _items
        _icon.sprite = item.MyIcon; //This sets the icon on the slot equal to the icon from scriptable object Item
        _icon.color = Color.white; //set the icon UI image to white when an item has been added (This is to remove the alpha that hides the image allowing the image allocated in the previous line to display)
        item.MySlot = this; //This is how an item knows what slot it is on. 
        return true;
    }

    //RemoveItem is called whenever an item needs to be removed from a slot. EG it has been used or trashed from the inventory
    public void RemoveItem(Item item)
    {
        if(!isEmpty) //If the slot is not empty. Remove the item from the inventory using Pop(); This is a function to remove items from the stack
        {
            MyItems.Pop();
        }
    }

    //is this calling itself??
    public void Clear()
    {
        if (MyItems.Count > 0)
        {
            MyItems.Clear();
        }
    }

    //This is activated when when the user clicks on the slot. This is what triggers the process to start using an item
    public void OnPointerClick(PointerEventData eventData)
    {

        //The below function will be used for Moving an item
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //Check one, Make sure there is nothing in the hand. This is determined by the variable _fromSlot in the inventoryScript
            //Check two is to make sure the slot being clicked on is not empty
            if(InventoryScript.instance.fromSlot == null && !isEmpty) //if we don't have something to move
            {
                HandFunctionScript.instance.TakeMoveable(MyItem as IMoveable); //An item can only be moved if it is IMoveable
                InventoryScript.instance.fromSlot = this; //"this" is whatever was just clicked on
            }

            //If i click on the slot and the fromslot is empty, and the slot im clicking on is empty, and the item on the hand script is a BAG, the try to unequip the bag
            else if(InventoryScript.instance.fromSlot == null && isEmpty == true && (HandFunctionScript.instance.MyMoveable is Bag)) 
            {
                Bag bag = (Bag)HandFunctionScript.instance.MyMoveable; //This creates a Bag as a bag is on the handscript


                //2 checks occur here
                //Check 1 cheks to make sure when you try to dequip a bag, you can't put the bag within itself
                //Check 2 checks the How many empty slots there are
                if (bag.MyBagScript != MyBag && InventoryScript.instance.MyEmptySlotCount - bag.Slots > 0) 
                {
                    AddItem(bag); //Adds the bag back to the inventory
                    bag.MyBagButton.RemoveBag(); //This will remove the bag from the bag button
                    HandFunctionScript.instance.DropMoveable(); //This removes the item from the hand
                }



            }
            else if (InventoryScript.instance.fromSlot != null) //If we have something to move
            {

                //First you just want to try and put the item back (if you clicked on the same slot
                //the second check is to try and merge
                //The third check you want to do is to try and swap the items on the slots
                //The fouth check is put the item on a slot
                if(PutItemBack() == true || MergeItems(InventoryScript.instance.fromSlot) || SwapItems(InventoryScript.instance.fromSlot) || AddItems(InventoryScript.instance.fromSlot.MyItems))
                {
                    HandFunctionScript.instance.DropMoveable();
                    InventoryScript.instance.fromSlot = null; //this resets the fromSlot to allow the user to pick up another item
                }

            }

        }

            //The below function will be used for Using an Item
        if (eventData.button == PointerEventData.InputButton.Right) //This checks when the user right clicks on a slot
        {
            UseItem();
        }
    }

    //This methold will use the item.
    //It is important to note that not all items can be "Used", Therefore we will only use items that are considered useable by using the interface IUseable
    public void UseItem()
    {

        if(MyItem is IUseable)
        {
            (MyItem as IUseable).Use();
        }

        
    }

    //Function that returns a bool.
    //This function is going to check each slot. It will first check to see if a slot exists that contains the same item.
    //It will then try to stack the item on this slot. If it can't stack the item becasue the slot is full, it will not add the item to the stack
    public bool StackItem(Item item)
    {
        //3 checks occur here
        //First Check if the slot is not empty. We are only interested in in stacking on non-empty slots
        //Second Check, ae we trying to add the same item or a different item
        //Third Check, Check the count of items on the stack against how many items of the same type can be stacked. This is determined my stackSize on the Item.cs (A property is created from this called MyStackSize)
        if (!isEmpty && item.name == MyItem.name && MyItems.Count < MyItem.MyStackSize) 
        {
            MyItems.Push(item);
            item.MySlot = this;
            return true; //When returned true, means we can stack the item
        }

        return false; //When returned false, means we can't stack the item

    }


    //This function is going to be called everytime something regarding items in the inventory changes. Whether it be a new item added to the invetory, or an item used or removed from the inventory
    private void UpdateSlot()
    {
        InventoryScript.instance.UpdateStackSize(this);
    }


    //This method will be used to put an item back into the inventory
    public bool PutItemBack()
    {
        if(InventoryScript.instance.fromSlot == this) //This means i have clicked on the same slot that i am trying to move from
        {
            InventoryScript.instance.fromSlot.MyIcon.color = Color.white; //this sets the colour of the slot back to normal
            return true;
        }

        return false;

    }

    //This will be used for swapping items between slots
    public bool SwapItems(SlotScript from)
    {
        //If the slot we are clicking on is empty (true)
        if(isEmpty == true)
        {
            return false; //This means the slot is empty and we don't have to do anything else
        }
        
        //If the item moving is differnt to the item being moved to will swap
        //OR if the count of item is MAX (eg 5 corns on the slot vs 2 corns in the hand) then swap the item as the slot has maxed stack
        if(from.MyItem.GetType() != MyItem.GetType() || from.MyCount+MyCount > MyItem.MyStackSize)
        {

            //Make a  copy of all items we need to swap from A (slot A for example). This is like a temp stack to not loose the items in the inventory
            ObservableStack<Item> tmpFrom = new ObservableStack<Item>(from.MyItems);

            //Clear all the items from slot A
            from.MyItems.Clear();

            //Take all items from slot B and copy them into Slot A
            from.AddItems(MyItems);

            //clear slot B
            MyItems.Clear();

            //Move items from Slot A copy to slot B
            AddItems(tmpFrom);

            //Return true becuase the swap was successful
            return true;
        }

        //return false if the swap was not possible
        return false;
    }

    //This function will take an item and place it into a different slot in the inventory
    //This function will also check if you can add an item to an existing stack
    public bool AddItems(ObservableStack<Item> newItems)
    {
        //If the slot is emply OR the item being moved matches the item being moved to then complete the code within the {}
        if(isEmpty == true || newItems.Peek().GetType() == MyItem.GetType())
        {
            int count = newItems.Count; //this is used for the loop below. This checks the count on all the newItems

            for (int i = 0; i < count; i++)
            {
                //we are checking if the slot is full or not (To see if we can stack)
                if(isFull == true)
                {
                    return false; //this means we can't proceed with adding the item to slot or slotstack
                }

                //else
                AddItem(newItems.Pop());
            }

            return true; //if we managed to move the items, return true

        }

        return false; //If we are unable to move the items, return false

    }

    public bool MergeItems(SlotScript from) //from is the item in the hand
    {

        if(isEmpty)
        {
            return false; //If false, there is no need to merge items
        }

        //The item in the hand matches the item on the slot
        //AND there is enough room on the slot to merge
        if(from.MyItem.GetType() == MyItem.GetType() && !isFull)
        {
            int freeSlots = MyItem.MyStackSize - MyCount; //this determines how many free slots do we have in the stack

            //this for loop has to run for how many free slots in the stack are avalible
            for (int i = 0; i < freeSlots; i++)
            {
                AddItem(from.MyItems.Pop());
            }

            return true; //return true if successful
        }

        return false; //if unsuccessful

    }


    //Awake is used as we need to always be listening to the item stack to see if something changes
    private void Awake()
    {

        //The Below are events being called from the ObservableStack Class
        MyItems.OnPop += new UpdateStackEvent(UpdateSlot); //Whenever Pop() is called, this the ObservableStack will be listening and will call UpdateSlot, which will UpdateStackSize
        MyItems.OnPush += new UpdateStackEvent(UpdateSlot); //Whenever Push() is called, this the ObservableStack will be listening and will call UpdateSlot, which will UpdateStackSize
        MyItems.OnClear += new UpdateStackEvent(UpdateSlot); //Whenever Clear() is called, this the ObservableStack will be listening and will call UpdateSlot, which will UpdateStackSize

    }

}
