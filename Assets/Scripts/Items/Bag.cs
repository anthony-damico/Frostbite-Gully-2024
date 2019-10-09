using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bag",menuName ="Items/Bag",order =1)]
public class Bag : Item, IUseable
{
    [SerializeField]
    private int _slots; //This is the amount of slots a bag can have

    [SerializeField]
    private GameObject bagPrefab; //This is the BagPrefab in the Prefab folder


    public BagScript MyBagScript { get; set; } //This is a Property from the BagScript. This is because there is functionality in the BagScript that affects the Bag Item (AKA this script). This is a constructor

    public int Slots { get => _slots; }

    public BagButton MyBagButton { get; set; } //This is a Property from the BagButton script. This is because there is functionality in the BagButton that affects the Bag Item (AKA this script). This is a constructor

    public void Initialize(int slots)
    {
        _slots = slots;
    }

    public void Use()
    {
        if(InventoryScript.instance.CanAddBag == true) //This will use the Bag / Equip the bag as long as 5 or less bags are equipped
        {

            Remove();

            //This line of code will create a bag prefab using the logic defined in the BagScript (Such as creating the bag with X number of Slots)
            MyBagScript = Instantiate(bagPrefab, InventoryScript.instance.transform).GetComponent<BagScript>();
            MyBagScript.AddSlots(_slots);

            InventoryScript.instance.AddBag(this); //This will call the AddBag function from the inventory script if a BAG scriptable item is used
        }
        else
        {
            Debug.Log("Too Many Bags Equipped");
        }
    }

}
