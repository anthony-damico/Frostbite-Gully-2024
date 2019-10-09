using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item, IUseable
{
    public int numberOfUses; //How many times the item can be used before it expires
    public bool isTool; //Defines whether the item is a tool so it can't expire / run out of uses
    public ToolType equipmentType; //What kind of Equipment is this


    /////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////
    //These may be removed
    public Sprite stage1;           //The animation of first stage of the Plant/Crop
    public Sprite stage1watered;    //The watered animation first stage of the Plant/Crop
    public Sprite stage2;           //The animation of Second stage of the Plant/Crop
    public Sprite stage2watered;    //The watered animation Second stage of the Plant/Crop
    public Sprite stage3;           //The animation of Third stage of the Plant/Crop
    public Sprite stage3watered;    //The watered animation Third stage of the Plant/Crop

    public int growthAge;           //This will be how long the plant will take to grow. EG 4 Game Days
    public int stage1Age;           //This is the  age the plant will change  to stage1 graphics
    public int stage2Age;           //This is the  age the plant will change  to stage2 graphics
    public int stage3Age;           //This is the  age the plant will change  to stage3 graphics
    public int repeatableGrowths;   //This is how many time the plant can be regrown
    public int daysToRegrow;        //This will allow a plant to regrow faster if it has a repeatable growth
    public int harvestableAmount;   //This will be how many items / crops will be given once the growth is complete. EG Carrot would give 1 carrot whereas Corn might give 3 corn
    public int plantSeason;         //This is the season the plant can be grown in

    public Item item;               //This is the item that the seed will give

    public AnimationClip clipDown;  //The Animation for a tool/items down position
    public AnimationClip clipLeft;  //The Animation for a tool/items left position
    public AnimationClip clipRight; //The Animation for a tool/items Right position
    public AnimationClip clipUp;    //The Animation for a tool/items up position
    /////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////

    public void Use()
    {
        Debug.Log("The tool " + name + " has been equipped.");
        EquipmentManager.instance.Equip(this); //Equip the Item that is click on in the inventroy
        //Remove From Inventory
    }

}


public enum ToolType
{
    hoe,
    wateringCan,
    seed,
    hammer,
    axe
}


