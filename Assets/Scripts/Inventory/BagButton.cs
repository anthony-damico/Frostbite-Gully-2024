using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BagButton : MonoBehaviour, IPointerClickHandler
{

    private Bag _bag; //If it exist or is not null, then that means a bag is equipped to the bag button

    [SerializeField]
    private Sprite full, empty; //This is the graphics for the bag button based if there is a bag equipped or not

    public Bag MyBag
    {
        get
        {
            return _bag;
        }

        set
        {
            if (value != null)
            {
                GetComponent<Image>().sprite = full;
            }
            else
            {
                GetComponent<Image>().sprite = empty;
            }

            _bag = value;
        }
    }


    //This is triggered if we click on a specific bag button
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Input.GetKey(KeyCode.LeftShift)) //If user holds down left shift while left clicking, the bag will be unequipped
            {
                HandFunctionScript.instance.TakeMoveable(MyBag); //Take the bag that has been equipped and attach it to the hand function
            }
            else if (_bag != null) //If we left click, open the bag accordingly
            {
                //BagScript.instance.OpenClose();
                _bag.MyBagScript.OpenClose(); //This opens and closes the bag when you click on Bag button. What is confusing is why it has to go through "MyBagScript"
            }
        }
    }

    //This function will be responsible for removing the bag from the bag button
    //It will also take all the items in a bag that is being removed and will add them to another bag if there is enough free space
    public void RemoveBag()
    {
        InventoryScript.instance.RemoveBag(MyBag); //Removes the bag from the inventory
        MyBag.MyBagButton = null; //Makes sure the button is not bound to a bag

        foreach (Item item in MyBag.MyBagScript.GetItems()) //Get all the items
        {
            InventoryScript.instance.AddItem(item); //Readd the items to the inventory after the bag has been dequiped
        }

        MyBag = null; //this makes sure the next bag can be added to the bagbutton. Makes sure the bag does have a reference to the bag button anymore
    }
}
