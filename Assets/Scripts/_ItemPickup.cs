using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _ItemPickup : _Interactable
{

    public Item item;

    public override void Interact()
    {
        base.Interact();

        PickUp(); //Method used to pickup the item
    }

    void PickUp()
    {

        Debug.Log("Player Picked Up " + item.name);
        InventoryScript.instance.AddItem(item);
        //bool wasPickedUp = InventoryScript.instance.AddItem(item); //checks to see if the item is able to be picked up. If true, add the item to the inventory

        //if (wasPickedUp == true) //If the item was picked up, remove the game object from the scene
        //{
        //    Destroy(gameObject); //Remove the gameobject from the scene
        //}
        
    }

}
