using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public Sprite stage1;           //The animation of first stage of the Plant/Crop
    public Sprite stage1watered;    //The watered animation first stage of the Plant/Crop
    public Sprite stage2;           //The animation of Second stage of the Plant/Crop
    public Sprite stage2watered;    //The watered animation Second stage of the Plant/Crop
    public Sprite stage3;           //The animation of Third stage of the Plant/Crop
    public Sprite stage3watered;    //The watered animation Third stage of the Plant/Crop

    public int growthAge;           //This will be how long the plant will take to grow. EG 4 Game Days
    public int repeatableGrowths;   //This is how many time the plant can be regrown
    public int daysToRegrow;        //This will allow a plant to regrow faster if it has a repeatable growth
    public int harvestableAmount;   //This will be how many items / crops will be given once the growth is complete. EG Carrot would give 1 carrot whereas Corn might give 3 corn
    public int plantSeason;         //This is the season the plant can be grown in
}
