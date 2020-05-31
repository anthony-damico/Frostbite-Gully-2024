using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBarScript : MonoBehaviour
{

    //Variables
    [SerializeField] private SlotScriptV2 _inventorySlot; //This will be the slot that ActionSlot is linked to. For now, this will be mapped through the editor
    [SerializeField] private Image _icon; //This is the image that sits on the slot
    [SerializeField] private Text _stackSizeText; //This is mapped in the UI against the slot prefab


    //Properties
    //This getter property will allow other classes and methods to change the icon on a Actionslot
    public SlotScriptV2 MyActionInventorySlot
    {
        get { return _inventorySlot; }
        set { _inventorySlot = value; }
    }

    //This getter property will allow other classes and methods to change the icon on a Actionslot
    public Image MyActionSlotIcon
    {
        get { return _icon; }
        set { _icon = value; }
    }

    //This getter property will allow other classes and methods to change the text stacksize on a Actionslot
    public Text MyActionStackSizeText
    {
        get { return _stackSizeText; }
        set { _stackSizeText = value; }
    }

    //Unity Methods

    // Update is called once per frame
    void Update()
    {
        if (MyActionInventorySlot.slotItemCount >= 1) //If there is an item on the inventroy slot, exceute the code
        {
            MyActionSlotIcon.sprite = MyActionInventorySlot.MySlotIcon.sprite; //Set the actionSlot icon the same as the inventory slot icon
            MyActionStackSizeText.text = MyActionInventorySlot.MySlotStackSizeText.text; //Set the actionSlot text the same as the inventory slot text

            MyActionSlotIcon.color = Color.white; //Make the icon visable
            MyActionStackSizeText.color = Color.white; //Make the text visable
        }
        else if (MyActionInventorySlot.slotItemCount <= 0)
        {
            MyActionSlotIcon.color = new Color(0, 0, 0, 0);
            MyActionStackSizeText.color = new Color(0, 0, 0, 0);
        }


    }


    //Methods
    public void UseItemFromActionSlot()
    {
        MyActionInventorySlot.UseItem();
    }

}
