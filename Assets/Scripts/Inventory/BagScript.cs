using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagScript : MonoBehaviour
{

    [SerializeField]
    private GameObject slotPrefab; //This will be the Slot Prefab

    private CanvasGroup canvasGroup; //This is used to Hide and Show the UI element. The Canvas Group is located on the Bag Prefab This is a reference

    //each bag needs to take own responibility of slots. This means each bag needs to have its own lists of slots
    private List<SlotScript> _slots = new List<SlotScript>();

    //This checks if the Bags are open or closed
    public bool IsOpen
    {
        get
        {
            return canvasGroup.alpha > 0; //If alpha is greater then 0, this means the bag is is open, therefore set to true
        }
    }

    //Other scripts need to beable to check what is on the slot. This public property will make this possible. We are only interested with what is on the slot
    public List<SlotScript> MySlots
    {
        get
        {
            return _slots;
        }
    }


    //This will count the empty slots each bag has
    public int MyEmptySlotCount
    {
        get
        {
            int count = 0;

            foreach (SlotScript slot in MySlots) //This will loop through through each slot to check if it is emply
            {
                if (slot.isEmpty) //If the slot is empty, add 1 to the count
                {
                    count++; //This means count = count + 1
                }
            }

            return count; //once all slots have been check, return the final count.
        }
    }


    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>(); //This completes the reference to the canvasgroup allow me to access any canvas functions via the variable canvasGroup
    }


    //This function will be responsible for return all the items in a bag. This will get the itmes from the slotscript from the obserablestack class/variable known as _items.
    //This is reponsible when you remove a bag. It will check and move the items to another bag (If there is enough free space defined in the the MyEmptySlotCount method
    public List<Item> GetItems()
    {

        List<Item> items = new List<Item>(); //This list is a temp reference of the list of items in a bag that is going to be dequipped

        foreach (SlotScript slot in _slots)
        {
            if(!slot.isEmpty) //If the slot isn't empty, this means there is an item on it
            {
                foreach (Item item in slot.MyItems) //We will check all the items from the item stack (MyItems)
                {
                    items.Add(item); //This adds the item to the temp list defined above called item if an item is found in a bag that is being removed
                }
            }
        }

        return items; //Return the temp list of items
    }

    //This will be the number of slot this script will create
    public void AddSlots(int slotCount)
    {
        for (int i = 0; i < slotCount; i++)
        {
            //Everytime we instantant a slot, we get component and return it to the slot variable defined before the '=' sign
            SlotScript slot = Instantiate(slotPrefab, transform).GetComponent<SlotScript>(); //This will create a slot at the required transform. In this case, the transform is the slot which is a child of the BagPrefab
            slot.MyBag = this; //This lets the slot know what bag it is assigned against. MyBag is defined in the SlotScript
            _slots.Add(slot);
        }
    }


    // The inventory has a AddItem function that is going to call the the bagscript additem and the bag is going to find an empty slot and add the item.
    // This means the inventoryscipt does not know anything about the slots, the bagscript will manage its own slots. 
    // The inventoryscript is just going to ask all the bags, can i add an item to you, if the bag responds and says yeah i have a free slot, then the item will be added to that slot and
    // the inventoryscript will stop asking the bags if there is a free slot
    public bool AddItem(Item item)
    {
        foreach(SlotScript slot in _slots)
        {
            if (slot.isEmpty) 
            {
                slot.AddItem(item); //If the slot is empty, add the item and return true if the item was added
                return true;
            }
        }

        return false;
    }

    //This function is used to open and close the Inventory Bags
    public void OpenClose()
    {
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1; //If the canvas is hidden, set the alpha to 1, if the canvas is showing, set the alpha to 0. This is like an IF statement

        canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true; //If the blockRaycasts is set to true, then set to false, if false, then set to true
    }

}
