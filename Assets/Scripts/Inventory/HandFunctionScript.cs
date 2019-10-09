using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



//This script will be used to move items around the interface. 
//You will beable to click on the gameobject icon to add it to your "Hand" and move the icon around screen.
//You will then beable to place the icon somewhere else on the screen such as a action bar or another inventory slot

public class HandFunctionScript : MonoBehaviour
{

    //Singleton Pattern prevents mutiple instances of the class. In this case the inventory
    #region Singleton

    public static HandFunctionScript instance;

    private void Awake()
    {
        if (instance != null) //If the instance is not null, that means an instance of the inventory is active
        {
            Debug.LogWarning("More then once instance of the Hand Function has been found");
            return;
        }
        instance = this;
    }

    #endregion

    //This is the icon. It will be overwritten with whatever icon the gameobject has
    [SerializeField]
    private Image _icon; //This is a reference to the Image in the editor

    //This is the image offset. It is used to offset the image against the mouse pointer so the image does not sit over the top of the mouse pointer
    public Vector3 _offset;

    //THis is a constructor to the Imoveable interface.
    public IMoveable MyMoveable { get; set; }
    

    // Start is called before the first frame update
    void Start()
    {
        _icon = GetComponent<Image>(); //This completes the reference to the Image in the editor and allows me to access all the image components via _icon
        _offset.x = -45;
        _offset.y = 40;
    }

    // Update is called once per frame
    void Update()
    {
        //This moves the _icon (HandIcon) to the position of the mouse
        _icon.transform.position = Input.mousePosition + _offset;

        DeleteItem();
    }

    //TakeMoveable will add the gameobject to the players hand.
    public void TakeMoveable(IMoveable moveable)
    {
        this.MyMoveable = moveable;
        _icon.sprite = moveable.MyIcon; //moveable.MyIcon is the icon that is set via any other script that has access to the IMoveable interface.
        _icon.color = Color.white; //Make the Image Visable if hidden

    }

    //PutMoveable will let the player put the gameobject in the players hand somewhere else
    public IMoveable PutMoveable()
    {
        IMoveable temp = MyMoveable;
        MyMoveable = null; //Clear the MyMovebale variable of anything set to it
        _icon.color = new Color(0, 0, 0, 0); //set the icon to invisible
        return temp; //return the MyMoveable
    }


    public void DropMoveable()
    {
        MyMoveable = null; //Clear the MyMovebale variable of anything set to it
        _icon.color = new Color(0, 0, 0, 0); //set the icon to invisible
    }

    public void DeleteItem()
    {
        //If the left mouse button is clicked
        //AND the mouse is not over the top of the current gameobject
        //AND MyMoveable has an item attached to it
        if(Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() && instance.MyMoveable != null)
        {

            if(MyMoveable is Item && InventoryScript.instance.fromSlot != null)
            {
                (MyMoveable as Item).MySlot.Clear();
            }

            DropMoveable();

            InventoryScript.instance.fromSlot = null;

        }
    }

}
