using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHandFunctionScript : MonoBehaviour
{


    //Script Variables
    [SerializeField] private Image _icon; //This is a reference to the Image in the editor. This is the icon. It will be overwritten with whatever icon the gameobject has
    [SerializeField] public Vector3 _offset; //This is the image offset. It is used to offset the image against the mouse pointer so the image does not sit over the top of the mouse pointer
    [SerializeField] public IMoveable MyMoveable { get; set; } //THis is a constructor to the Imoveable interface.


    //Properties
    public Image HandIcon { get { return _icon; } set { _icon = value; } }

    //Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        //HandIcon = GetComponent<Image>(); //This completes the reference to the Image in the editor and allows me to access all the image components via _icon
        //_offset.x = -45; //Offset the image against the
        //_offset.y = 40;
    }

    //TakeMoveable will add the gameobject to the players hand.
    public void TakeMoveable(IMoveable moveable)
    {
        this.MyMoveable = moveable;
        HandIcon.sprite = moveable.MyIcon; //moveable.MyIcon is the icon that is set via any other script that has access to the IMoveable interface.
        HandIcon.color = Color.white; //Make the Image Visable if hidden

    }

    //PutMoveable will let the player put the gameobject in the players hand somewhere else
    public IMoveable PutMoveable()
    {
        IMoveable temp = MyMoveable;
        MyMoveable = null; //Clear the MyMovebale variable of anything set to it
        HandIcon.color = new Color(0, 0, 0, 0); //set the icon to invisible
        return temp; //return the MyMoveable
    }


    //DropMoveable resets the handfunction so it can accept a new moveable
    public void DropMoveable()
    {
        MyMoveable = null; //Clear the MyMovebale variable of anything set to it
        HandIcon.color = new Color(0, 0, 0, 0); //set the icon to invisible
    }

    //Delete an item from the inventory. This will be overwitten in inhertied methods as a mouse delete will be different to a controller/keyboard delete
    public virtual void DeleteItem()
    {
        //Debug.Log("Deleted this moveable object");
    }
}