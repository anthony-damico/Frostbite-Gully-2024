using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tomato", menuName = "Items/Tomato", order = 1)]
public class Tomato : Item
{
    public override void UseItem()
    {
        base.UseItem();
        //Corn will do more, such as heal the player
        Debug.Log("The Item " + name + " has been used");

        //Remove the item once used. This comes from the Parent Script "Item"
        RemoveItem();
    }
}
