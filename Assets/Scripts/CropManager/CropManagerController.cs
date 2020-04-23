 using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
The Purpose of the Script is to control the state of the crop prebab. 
Such as
- What kind of Crop is being grown
- Is the season correct for the crop to be grown
- Has the crop been watered


- Things to test:
swoop7_1109/17/2019
that function references that particular class's transform.
hmmmm
not sure how that would work, the hit doesn't know which components or scripts it has
    // raycast hit
   transform.    // transform of what the raycast hit
   gameObject.    // gameObject of what the raycast hit
   GetComponent<CropManager>().    // get the CropManager script
   SpriteChange(stage2watered);   // Now I have access to this method
hmmm
i wonder if
     
   
 // raycast 
     GetComponent<CropManager>().     // get the CropManager script
     SpriteChange(stage2watered);     // Now I have access to this method
would work

*/
public class CropManagerController : MonoBehaviour
{

    TimeManagerController timeManagerController; //Creates a reference back to the TimeManagerControllerScript to get access to the different time, day and season variables
    EquipmentManager equipmentManager; //Creates a reference to the EquipmentManager so i can see what tool the player has equipped

    //Universal Sprites
    public Sprite tilledTile; //This is the graphic for a tile that has been hoed
    public Sprite tilledWateredTile; //This is the graphic for a tile that has been hoed and watered
    public Sprite seedTile; //This is the graphic for a tile that has seeds on it
    public Sprite seedWateredTile; //This is the graphic for a tile that has seeds on it and watered

    //Growth Logic Variables. The Below variables can be defined within the Method. I have added these here for ease of visability and will remove them
    [SerializeField]
    private bool isTilled; //Checks to see if the land has been Tilled. This means you can plant

    [SerializeField]
    private bool isPlanted; //This flag means a seed has been planted

    [SerializeField]
    private bool isWatered; //Checks to see if the crop has been watered

    [SerializeField]
    public bool isEmpty; //Checks if a plot is empty or not. For now, a plot is considered empty if there is no seed on it

    public bool canGrow; //Can grow is a flag to determine whether the plant can grow by day
    public int currentGrowth; //This will have 1 added to it at 6am (timemanager). When DaysPassed equals growthAge, the plant is Grown
    public int currentrepeatableGrowths; //This is how many times the crop has grown

    public bool isStage1; //Is the plant in stage1
    public bool isStage2; //Is the plant in stage2
    public bool isStage3; //Is the plant in stage3
    public bool isStageRegrow; //Is the plant in regrow

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    //The below may not be needed if a smarter way to identify crops can be found. They have been Serialzied to show them in the Editor for DEBUG Purposes

    [SerializeField]
    Sprite stage1;           //The animation of first stage of the Plant/Crop

    [SerializeField]
    Sprite stage1watered;    //The watered animation first stage of the Plant/Crop

    [SerializeField]
    Sprite stage2;           //The animation of Second stage of the Plant/Crop

    [SerializeField]
    Sprite stage2watered;    //The watered animation Second stage of the Plant/Crop

    [SerializeField]
    Sprite stage3;           //The animation of Third stage of the Plant/Crop

    [SerializeField]
    Sprite stage3watered;    //The watered animation Third stage of the Plant/Crop

    [SerializeField]
    int growthAge;           //This will be how long the plant will take to grow. EG 4 Game Days

    [SerializeField]
    int stage1Age;           //This is the  age the plant will change  to stage1 graphics

    [SerializeField]
    int stage2Age;           //This is the  age the plant will change  to stage2 graphics

    [SerializeField]
    int stage3Age;           //This is the  age the plant will change  to stage3 graphics

    [SerializeField]
    int repeatableGrowths;   //This is how many time the plant can be regrown

    [SerializeField]
    int daysToRegrow;        //This will allow a plant to regrow faster if it has a repeatable growth

    [SerializeField]
    int harvestableAmount;   //This will be how many items / crops will be given once the growth is complete. EG Carrot would give 1 carrot whereas Corn might give 3 corn

    [SerializeField]
    int plantSeason;         //This is the season the plant can be grown in

    [SerializeField]
    Item item;               //This is the item a seed will return
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////


    // Start is called before the first frame update
    void Start()
    {
        timeManagerController = TimeManagerController.instance; //Completes the reference back to the TimeManagerController.cs script using the singleton
        equipmentManager = EquipmentManager.instance; //Completes the reference back to the EquipmentManager.cs script using the singleton

        isStage1 = false; 
        isStage2 = false; 
        isStage3 = false;
        isStageRegrow = false;
        isTilled = false;
        isPlanted = false;
        isWatered = false;
        currentGrowth = 1;
        isEmpty = true;
}

    // Update is called once per frame
    void Update()
    {
        //Step 5: Grow Plant: Increase Growth age of plant for every day passed
        if (timeManagerController.hours == 6 && timeManagerController.tenMinutes == 0 && timeManagerController.minutes == 0) //This means 6am and the start of a new day
        {
            GrowPlant();
        }
        else
        {
            canGrow = true; //This is alwasy true until GrowPlant() triggers at 6am, where it will turn to false until it is no longer 6am
        }
    }

    //This will do the farm work
    public void DoFarmWork() //The raycast is passed in from the PlayerMovement.cs script
    {
        //Import Debug Step
        Debug.Log("♫ Working on the Farm on all day, everybody sing the farming song ♫" + " " + this.name); //Do Planting Stuff

        //Step 1: Clear Rubbish
        //Step 2: till tile (hoe tile)
        //Step 3: Plant Seed
        //Step 4: Water tile and/or seed
        //Step 5: Grow Plant: Increase Growth age of plant for every day passed
        //Step 6: Harvest crop
        //Step 7: Regrow crop (if applicable)


        //Step 2: Hoe Tile
        if(equipmentManager.currentEquipment.equipmentType == ToolType.hoe && isTilled == false) //Checks to see of the ToolType "Hoe" is equipped and the land is not yet tilled
        {
            HoeTite(); //If a hoe is equip, hoe the land
        }

        //Step 3: Plant Seed
        if(equipmentManager.currentEquipment.equipmentType == ToolType.seed && isTilled == true && isPlanted == false) //Check if the land has been hoed and if a seed is Equip
        {
            PlantSeed();
        }

        //Step 4: Water tile and/or seed
        if (equipmentManager.currentEquipment.equipmentType == ToolType.wateringCan)
        {
            AddWateredState();
        }

        //Step 6: Harvest crop
        if(isStage3 == true)
        {
            HarvestCrop();
        }
    }

    //This will change the graphic to "tilledTile"
    void HoeTite() //Each Planting Function (Hoe, Seed, Water etc will need to take a RaycastHit2D so that the raycast projected from the player knows what it has hit and what it is doing
    {
            //Till Land and tell the plot it is ready to accept a seed
            Debug.Log("Land has been tilled on " + this.name);
            this.transform.GetComponent<SpriteRenderer>().sprite = tilledTile; //Change the tile graphic to the tilled sprite
            isTilled = true; //The ground has been tiled and will now accept a seed
    }

    //Plant seed and prevent the plot being overwritten with another seed by setting isPlanted to True
    void PlantSeed()
    {
        GetSeedInfo(); //This will get the type of seed being planted from the currently equipped seed
        Debug.Log(equipmentManager.currentEquipment.name + "  has been planted on " + this.name);
        transform.GetComponent<SpriteRenderer>().sprite = seedTile; //Change the tile graphic to the seed sprite
        isPlanted = true; //Seed has been planted, plot can't be overwritten (hopefully)
        isEmpty = false;

        //This is called here, just in case a tile has already been watered when a seed is planted. Will only trigger if isWatered == True
        if (isWatered == true)
        {
            AddWateredState(); 
        }
    }
    
    //Water the crop
    void AddWateredState()
    {

        //Change the tile graphic to "tilledWateredTite" if it has been tilled, does not have a seed and is not yet watered
        if (isTilled == true && isPlanted == false && isWatered == false && stage1 == false && stage2 == false && stage3 == false && isStageRegrow == false)
        {
            Debug.Log("Watered the tilled land on " + this.name);
            transform.GetComponent<SpriteRenderer>().sprite = tilledWateredTile;
        }

        //Change the graphic to "seedWateredTile" if the tile has been tilled and watered
        if (isTilled == true && isPlanted == true && isWatered == true && stage1 == false && stage2 == false && stage3 == false && isStageRegrow == false)
        {
            Debug.Log("Seed is already watered on " + this.name);
            transform.GetComponent<SpriteRenderer>().sprite = seedWateredTile;
        }

        //Change the graphic to "seedWateredTile" if the tile has a seed on it
        if (isPlanted == true && isWatered == false)
        {
            Debug.Log("Watered the seed on " + this.name);
            transform.GetComponent<SpriteRenderer>().sprite = seedWateredTile;
        }

        //Check if the plant is in stage1 and set the watered graphic to stage1watered
        if (isPlanted == true && isStage1 == true && isWatered == false)
        {
            Debug.Log("Staged 1 watered successful on" + this.name);
            transform.GetComponent<SpriteRenderer>().sprite = stage1watered;
        }
        
        //Check if the plant is in stage2 and set the watered graphic to stage2watered
        if (isPlanted == true && isStage2 == true && isWatered == false)
        {
            Debug.Log("Staged 2 watered successful on " + this.name);
            transform.GetComponent<SpriteRenderer>().sprite = stage2watered;
        }
        
        //Check if the plant is in stage3 and set the watered graphic to stage3watered
        if (isPlanted == true && isStage3 == true && isWatered == false)
        {
            Debug.Log("Staged 3 watered successful on " + this.name);
            transform.GetComponent<SpriteRenderer>().sprite = stage3watered;
        }

        //Check if the plant is in stageRegrow and set the watered graphic to stage2watered (This is temp)
        if (isPlanted == true && isStageRegrow == true && isWatered == false)
        {
            Debug.Log("Staged 2 watered successful on " + this.name);
            transform.GetComponent<SpriteRenderer>().sprite = stage2watered;
        }

        isWatered = true;

    }

    //Remove "watered" state on crops. This is used at the start of a new day (6am onwards)
    void RemoveWateredState()
    {

        if(isWatered == true)
        {
            //Change the tile graphic to "tilledTite" if it has been tilled, does not have a seed and is watered
            if (isTilled == true && isPlanted == false)
            {
                Debug.Log("tilled land is no longer watered on " + name);
                SpriteChange(tilledTile); //transform.GetComponent<SpriteRenderer>().sprite = tilledTile;
                return;
            }

            //Change the graphic to "seedTile" if the tile has a seed
            if (isTilled == true && isPlanted == true && isStageRegrow == false)
            {
                Debug.Log("Seed is no longer watered on " + name);
                SpriteChange(seedTile); //transform.GetComponent<SpriteRenderer>().sprite = seedTile;
                return;
            }

            //Check if the plant is in stage1 has been watered graphic to stage1
            if (isPlanted == true && isStage1 == true)
            {
                Debug.Log("Staged 1 is no longer watered on " + name);
                SpriteChange(stage1); //transform.GetComponent<SpriteRenderer>().sprite = stage1;
            }

            //Check if the plant is in stage2 has been watered graphic to stage2
            if (isPlanted == true && isStage2 == true)
            {
                Debug.Log("Staged 2 is no longer watered on " + name);
                SpriteChange(stage2); //transform.GetComponent<SpriteRenderer>().sprite = stage2;
            }

            //Check if the plant is in stage3 has been watered graphic to stage3
            if (isPlanted == true && isStage3 == true)
            {
                Debug.Log("Staged 3 is no longer watered on " + name);
                SpriteChange(stage3); //transform.GetComponent<SpriteRenderer>().sprite = stage3;
            }

            //Check if the plant is in stageregrow has been watered graphic to stage2
            if (isPlanted == true && isStageRegrow == true)
            {
                Debug.Log("Staged regrow is no longer watered on " + name);
                SpriteChange(stage2); //transform.GetComponent<SpriteRenderer>().sprite = stage2;
            }

            return;
        }
    }

    //Plant needs to grow
    void GrowPlant()
    {
        //Check if the plant has been watered
        //If it has been watered, add 1 to the life of the crop
        if(isWatered == true)
        {
            CheckTime();
        }

        if(isStageRegrow == false)
        {
            //Check the age of crop and move from seed to stage1
            if (currentGrowth >= stage1Age && currentGrowth < stage2Age && isPlanted == true)
            {
                Debug.Log("Set isStage1 to true on " + name);
                SpriteChange(stage1); //transform.GetComponent<SpriteRenderer>().sprite = stage1;
                isStage1 = true;
                isStage2 = false;
                isStage3 = false;
            }

            //Check the age of crop and move from stage1 to stage2
            if (currentGrowth >= stage2Age && currentGrowth < growthAge && isPlanted == true)
            {
                Debug.Log("Set isStage2 to true on " + name);
                SpriteChange(stage2); //transform.GetComponent<SpriteRenderer>().sprite = stage2;
                isStage1 = false;
                isStage2 = true;
                isStage3 = false;
            }

            //Check the age of crop and move fromm stage2 to stage3. This means the crop will be ready to harvest
            if (currentGrowth >= growthAge && isPlanted == true)
            {
                Debug.Log("Set isStage3 to true on " + name);
                SpriteChange(stage3); //transform.GetComponent<SpriteRenderer>().sprite = stage3;
                isStage1 = false;
                isStage2 = false;
                isStage3 = true;
            }
        }
        else
        {
            //Check the age of crop and move fromm stage2 to stage3. This means the crop will be ready to harvest
            if (currentGrowth >= daysToRegrow && isPlanted == true)
            {
                Debug.Log("Set isStage3 for regrow to true on " + name);
                SpriteChange(stage3); // transform.GetComponent<SpriteRenderer>().sprite = stage3;
                isStage1 = false;
                isStage2 = false;
                isStage3 = true;
                //isStageRegrow = true;
            }
        }

        



        //harvest if applicable
        //Repeat growth if needed
        //Start new  day
    }


    //To prevent DaysPassed getting more then 1 added to it when the time equals 6am, this proceedure will wait 3seconds before adding 1 to DaysPassed
    private void CheckTime()
    {
        if (canGrow == true) //This means 6am
        {
            StartCoroutine(waitForTime(3.0f)); //This will call the untitlity function and will wait 3 Seconds

            //currentGrowth will only occur if the plot has a seed on it
            if (isPlanted == true)
            {
                currentGrowth = currentGrowth + 1; //Add one 
            }
            
            RemoveWateredState(); //Sets the graphics to there unwatered state
            isWatered = false; //Set the crop to no longer be watered to allow it to be watered again
            canGrow = false; //This means the plot has progress and can be set to false
        }
    }

    private void HarvestCrop()
    {
        for (int i = 0; i < harvestableAmount; i++)
        {
            InventoryScript.instance.AddItem(item);
        }

        if(currentrepeatableGrowths < repeatableGrowths)
        {
            Debug.Log("Set isStage2 to true on " + name);
            SpriteChange(stage2); //transform.GetComponent<SpriteRenderer>().sprite = stage2;
            isStage1 = false;
            isStage2 = false;
            isStage3 = false;
            isStageRegrow = true;
            currentGrowth = 1;
            currentrepeatableGrowths = currentrepeatableGrowths + 1;
        }
        else
        {
            ResetPlotInfo(); //Reset the plot to empty
            ResetSeedInfo(); //Reset the seed information against the plot
        }
        

    }

    //This will get all the seed information
    public void GetSeedInfo()
    {
        //Get the graphics and information from the seed
        stage1 = equipmentManager.currentEquipment.stage1;
        stage1watered = equipmentManager.currentEquipment.stage1watered;
        stage2 = equipmentManager.currentEquipment.stage2;
        stage2watered = equipmentManager.currentEquipment.stage2watered;
        stage3 = equipmentManager.currentEquipment.stage3;
        stage3watered = equipmentManager.currentEquipment.stage3watered;
        growthAge = equipmentManager.currentEquipment.growthAge;
        stage1Age = equipmentManager.currentEquipment.stage1Age;
        stage2Age = equipmentManager.currentEquipment.stage2Age;
        stage3Age = equipmentManager.currentEquipment.stage3Age;
        repeatableGrowths = equipmentManager.currentEquipment.repeatableGrowths;
        daysToRegrow = equipmentManager.currentEquipment.daysToRegrow;
        harvestableAmount = equipmentManager.currentEquipment.harvestableAmount;
        plantSeason = equipmentManager.currentEquipment.plantSeason;
        item = equipmentManager.currentEquipment.item;
    }

    void ResetSeedInfo()
    {
        //Reset the seed information on the plot
        stage1 = null;
        stage1watered = null;
        stage2 = null;
        stage2watered = null;
        stage3 = null;
        stage3watered = null;
        growthAge = 0;
        stage1Age = 0;
        stage2Age = 0;
        stage3Age = 0;
        repeatableGrowths = 0;
        daysToRegrow = 0;
        harvestableAmount = 0;
        plantSeason = 0;
        item = null;
    }

    void ResetPlotInfo()
    {
        isStage1 = false;
        isStage2 = false;
        isStage3 = false;
        isStageRegrow = false;
        isTilled = true; //Leave the land tilled for easy re-planting
        isPlanted = false;
        isWatered = false;
        currentGrowth = 1;
        SpriteChange(tilledTile); //old line of code: transform.GetComponent<SpriteRenderer>().sprite = tilledTile;
    }


    //Return the plotname based on the input
    void WhatsMyName(int i, int j)
    {
        Debug.Log("CropManagerController Check: GO Name: " + GridScript._2DtileArray[i][j].name + " is at position in the array: " + i + " " + j);
    }

    //This function is used to changed the sprite of the plot. At this time, the function only has 1 line of code, but my thoughts are if i decide to play an
    //animation when a plant grows, i will beable to add it here easily
    public void SpriteChange(Sprite sprite)
    {
        transform.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    //This is a untitity function, it ia used to wait a certain amount of seconds
    public IEnumerator waitForTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("ToolHighlight"))
        {
            //collision.IsTouchingLayers(layerMask: 1 << 10)
            if (collision.IsTouchingLayers(layerMask: 1 << LayerMask.NameToLayer("BigResource"))) //BigResource Layer
            {
                Debug.Log("Hit " + this.name + " which is a Big Resource");
            }

            else if (collision.IsTouchingLayers(layerMask: 1 << LayerMask.NameToLayer("SmallResource"))) //SmallResource Layer
            {
                Debug.Log("Hit " + this.name + " which is Small Resource");
            }

            else if (collision.IsTouchingLayers(layerMask: 1 << LayerMask.NameToLayer("Plot"))) //Plot Layer
            {
                Debug.Log("Hit " + this.name + " which is a plot");
                DoFarmWork();
            }
        }
    }

}

