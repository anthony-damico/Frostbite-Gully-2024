using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{

    //Singleton Pattern prevents mutiple instances of the class. In this case the inventory
    #region Singleton

    public static InventoryScript instance;

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

    private List<Bag> _bags = new List<Bag>(); //This creates a list of bags

    [SerializeField]
    private BagButton[] _bagButtons; //This is used for assigning a new bag whenever we equip a bag

    //This is for Debugging
    [SerializeField]
    public Item[] _items; //This create an array against the inventory GameObject that will accept the scripable obekjct "Item"


    //This is a reference to the slotscript so i can access the properties of the slot
    private SlotScript _fromSlot; //This is the slot the item is coming from

    // Start is called before the first frame update
    void Start()
    {
        //The below is used as a debug to add a bag item to the inventory
        Bag bag = (Bag)Instantiate(_items[0]); //This initizes the Item in slot 0 in the item array
        bag.Initialize(20); //This calls the Initialie method which is used to allocate the bag slot size. Refer to Bag.cs for more details
        bag.Use(); //This uses the bag to equip all the slots from the bag
    }

    private void Update()
    {
        //Controlled in DebugManager.cs as of 28/09/2019
        /*
        if (Input.GetKeyDown(KeyCode.J))
        {
            //The below is used as a debug to add a bag item to the inventory
            Bag bag = (Bag)Instantiate(_items[0]); //This initizes the Item in slot 0 in the item array
            bag.Initialize(20); //This calls the Initialie method which is used to allocate the bag slot size. Refer to Bag.cs for more details
            bag.Use(); //This uses the bag to equip all the slots from the bag
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            //The below is used as a debug to add a bag item to the inventory
            Bag bag = (Bag)Instantiate(_items[2]); //This initizes the Item in slot 0 in the item array
            bag.Initialize(bag.Slots); //This calls the Initialie method which is used to allocate the bag slot size. Refer to Bag.cs for more details
            AddItem(bag);

            Equipment tool = (Equipment)Instantiate(_items[3]); //This initizes the Item in slot 3 in the item array
            AddItem(tool);

            tool = (Equipment)Instantiate(_items[4]); //This initizes the Item in slot 4 in the item array
            AddItem(tool);

            tool = (Equipment)Instantiate(_items[5]); //This initizes the Item in slot 5 in the item array
            AddItem(tool);

            tool = (Equipment)Instantiate(_items[6]); //This initizes the Item in slot 6 in the item array
            AddItem(tool);

        }
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            //The below is used as a debug to add a bag item to the inventory
            Corn corn = (Corn)Instantiate(_items[1]); //This initizes the Item in slot 0 in the item array
            AddItem(corn);
        }
        */

        if (Input.GetKeyDown(KeyCode.B))
        {
            OpenClose(); //Open\Close all bags in the inventory
        }

    }


    //The fromSlot will always be the item that is being carried by the hand. This function will grey out the Slot if the item is being moved
    public SlotScript fromSlot
    {
        get
        {
            return _fromSlot;
        }

        set
        {
            _fromSlot = value;
            if(value != null) //If an item has been clicked, the _fromSlot is considered in use
            {
                _fromSlot.MyIcon.color = Color.grey; //set the slot/icon colour to grey
            }
        }

    }

    //This bool preform a check to see if the list _bag has 5 bags in it. It will return true until 5 bags have been added to the _bag List
    public bool CanAddBag
    {
        get { return _bags.Count < 5; } //this will retunr true while there are < 5 bags in the _bags list
    }


    //This will put the count together from all the bags. This MyEmptySlotCount will count the BAGS not the slots as the slots have been counted in the slotscript
    public int MyEmptySlotCount
    {
        get
        {
            int count = 0;

            foreach (Bag bag in _bags)
            {
                count += bag.MyBagScript.MyEmptySlotCount; //How this works. If there are 5 slots free in bag 1 and 3 slots free in bag 2, add a total count of 8 to count
            }

            return count; //Return the total empty slot count across all bags
        }
    }


    //From Memory, this adds a bag to the _bags list and tells the bag button that a new bag has been added
    public void AddBag(Bag bag)
    {
        foreach (BagButton bagButton in _bagButtons)
        {
            if(bagButton.MyBag == null)
            {
                bagButton.MyBag = bag; //When this line of code is execute, the code under MyBag property MyBag located in BagButton is execute. This is how we know which icon to use
                _bags.Add(bag); //This adds a bag to the bags list
                bag.MyBagButton = bagButton; //this gives the bag a reference to the button it is sitting on
                break; //This returns from the IF statement to prevent the code running on all buttons
            }
        }
    }

    //This function will call PlaceInStack or PlaceInEmpty depending on what kind of item is being added
    //Check 1: Does the item have a StackSize greater then 0
    //Check 2: PlaceInStack function checks to see if the item is allowed to be placed in a stack. If the reuslt is true, this is allowed. If the result is true, just return from the function.
    //Check 3: If the item could not be placed in a stack because the result was false, place the item in an empty slot
    public void AddItem(Item item)
    {
        if(item.MyStackSize > 0)
        {
            if(PlaceInStack(item) == true)
            {
                return;
            }
        }

        PlaceInEmpty(item);
    }


    /*    public bool AddItem(Item item)
    {
        if (item.MyStackSize <= 0)
        {
            PlaceInEmpty(item);
            return;
        }  
        else
        {
            return PlaceInStack(item);
        }
                                   
    }
    */

    //This function will place the item in an empty slot if it can't stack it for any reason
    //This functon just look at every bag and the bag itself knows if it can add the item
    private void PlaceInEmpty(Item item)
    {
        foreach (Bag bag in _bags)
        {
            if(bag.MyBagScript.AddItem(item)) //If it is possible to add the item, then return as it was successful
            {
                return;
            }
        }

    }


    //This function will return a bool to determine if it is possible to stack an option
    private bool PlaceInStack(Item item) //This is the item we are going to try and stack
    {
        foreach (Bag bag in _bags) //We run through each bag in _bags. Whenever a Bag is found, it is referred to as "bag" within the foreach loop 
        {
            foreach (SlotScript slots in bag.MyBagScript.MySlots) //We can now run through each slot on the bag to see if it is possible to stack on the slot or not (Moving forward). If a slot is found. it is referred to as "slots"
            {
                //This will try to stack on every slot, on every bag. If the functons manages to stack, it will return true.
                if(slots.StackItem(item))
                {
                    return true;
                }
            }

        }

        //If we were unable to stack the item, return false
        return false;
    }


    //This Might be stored in a UIManager in the future
    public void OpenClose()
    {
        //Everytime a bag is found in the _bags list, refer to it as x
        bool closedBag = _bags.Find(x => !x.MyBagScript.IsOpen); //This line checks to see if any bags are closed. If any bags are closed, closedBag is set to true

        //If closedBag == true, then open all closed bags
        //If closedBag == false, then close all open bags
        foreach(Bag bag in _bags)
        {
            if (bag.MyBagScript.IsOpen != closedBag)
            {
                bag.MyBagScript.OpenClose();
            }
        }

    }


    //This would be better on a UIManager
    //This is used to update the stacksize
    public void UpdateStackSize(IClickable clickable)
    {

        //THe stackSize needs to update if there is 1 or more item
        if(clickable.MyCount >= 1)
        {
            clickable.MyStackText.text = clickable.MyCount.ToString(); //This sets the text on the slot equal to to the count value in MyCount
            clickable.MyStackText.color = Color.white; //If the colour of the text is hidden or not white, set it to white
            clickable.MyIcon.color = Color.white;
        }

        if(clickable.MyCount == 0) //IF there are no more items on the clickable item/slot
        {
            clickable.MyStackText.color = new Color(0, 0, 0, 0); //This sets the text on the slot to have no colour / be invisible. Items with a stacksize of 0 do not need a stacksize
            clickable.MyIcon.color = new Color(0, 0, 0, 0); //This sets the icon on the slot to have no colour / be invisable
        }
    }

    //THe remove bag function is called from the BagButton.cs when it is click
    public void RemoveBag(Bag bag)
    {
        _bags.Remove(bag); //Remove is a function in the item.cs
        Destroy(bag.MyBagScript.gameObject); //This will destroy the gameObject Bag that has been clicked on. However we will make an exact copy of this object in the inventory

    }

}
