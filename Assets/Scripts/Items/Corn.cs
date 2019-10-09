using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Corn", menuName = "Items/Corn", order = 1)]
public class Corn : Item, IUseable
{
    public void Use()
    {

        //Corn will do more, such as heal the player
        Debug.Log("The Item " + name + " has been used");

        //Remove the item once used. This comes from the Parent Script "Item"
        Remove();
    }

}
