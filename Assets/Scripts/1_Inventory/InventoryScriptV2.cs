using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScriptV2 : MonoBehaviour
{

    //Singleton Pattern prevents mutiple instances of the class. In this case the inventory
    #region Singleton

    public static InventoryScriptV2 instance;

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


    //This is for Debugging
    [SerializeField]
    public int inventorySizeLimit = 36; //This is the max size of the inventory
    public List<Item> _items = new List<Item>(); //This create a a list against the inventory GameObject that will accept the scripable object "Item"
    public List<SlotScriptV2> _slots = new List<SlotScriptV2>(); //This is a list of all slots in the inventory. The slots are linked to this list via the unity inspector
    
    //Add Items
    public void AddItemToInventory(Item item)
    {
        _items.Add(item); //Add is a build in function of the List Type. It will Add whatever Object is specified as long as the Object Type matches the list type
    }

    //Remove Items
    public void RemoveItemFromInventory(Item item)
    {
        _items.Remove(item); //Remove is a build in function of the List Type. It will remove whatever Object is specified as long as the Object Type matches the list type
    }

    //Move Items

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
