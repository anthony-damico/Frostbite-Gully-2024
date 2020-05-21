using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Corn", menuName = "Items/Corn", order = 1)]
public class Corn : Item, IUseable
{
    public void Use() //This has been depricated as I no longer use the IUsable Interface
    {

        //Corn will do more, such as heal the player
        Debug.Log("The Item " + name + " has been used");

        //Remove the item once used. This comes from the Parent Script "Item"
        Remove();
    }



    public override void UseItem()
    {
        base.UseItem();
        //Corn will do more, such as heal the player
        Debug.Log("The Item " + name + " has been used");

        //Remove the item once used. This comes from the Parent Script "Item"
        Remove();
    }

}
