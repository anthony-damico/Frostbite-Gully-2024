using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{

    //03/09/2019: Credit to Ryan Davis (r.m.davis90@gmail.com) for assisting with the jagged list/nested list to gather all plot objects

   //The purpose of this script is to manage objects on the grid. I am not sure if i need another script as a "Map Manager" or if this grid script is enough to act as a map manager

    public GameObject parentGO; // Parent Gameobejct holding all the other children gameObjects. This is the ground Object in the Unity Hierachy

    [SerializeField]
    List<GameObject> tileArray = new List<GameObject>();

    //This is public temporarilly until i figure out how to get; set; correctlthy
    //[SerializeField]
    public static List<List<GameObject>> _2DtileArray = new List<List<GameObject>>(); //Static keyword makes this variable a Member of the class, not of any particular instance.

    [SerializeField]
    Transform BigRockPrefab; //The Prefab being spawned

    [SerializeField]
    Transform BigLogPrefab; //The Prefab being spawned

    [SerializeField]
    Transform SmallRockPrefab; //The Prefab being spawned

    [SerializeField]
    Transform SmallLogPrefab; //The Prefab being spawned


    public bool gameStart;

    [SerializeField]
    int listI;              //This is for Debug Purposes used with WhatsMyName() function

    [SerializeField]
    int ListJ;              //This is for Debug Purposes used with WhatsMyName() function

    // Start is called before the first frame update
    void Start()
    {
        gameStart = true;

        //cropManagerController = GetComponent<CropManagerController>(); //Completes the reference to the CropManagerController. If a function or variable from the CropManagerController script/class needs to be called, it can be called via the variable cropManagerController
    }



    // Update is called once per frame
    void Update()
    {
        if (gameStart == true)
        {
            gameStart = false; //Run immediatley to prevent the PopulateNestedList() running a second time
            PopulateNestedList();

        }

        //This is for debug Purposes to see if i can return the correct plots
        if (Input.GetKeyDown(KeyCode.P)) //P for Plot
        {
            WhatsMyName(listI, ListJ); //listI and listJ can be chnaged in the unity editor to return different plots Hopefully
        }

        //This is for debug Purposes to see if i can return the correct plots
        if (Input.GetKeyDown(KeyCode.Keypad1)) //1
        {
            GenerateResources();
        }

    }
  


    //Populate Nested List code provide by Ryan. Anthony to Learn and understand code to modify as needed
    void PopulateNestedList()
    {
        // gameObject.Transform holds all the useful methods (because each GameObject _HAS_TO_HAVE_ a transform
        // (so it's better to put them there) 

        Transform prevTransform = null; // Set it to null for our for loop check

        // As long as there are still children under the parent gameobject...
        for (int i = 0; i < parentGO.transform.childCount; i++)
        {
            // Rename the gameObject
            parentGO.transform.GetChild(i).gameObject.name = "plot_" + i;

            // Add it to our 1D List
            tileArray.Add(parentGO.transform.GetChild(i).gameObject);
        }

        // Create a temp list for the following for loop
        List<GameObject> tempColumns = new List<GameObject>();


        // The maximum width the grid can be is the number of children (i.e. 1 x n)
        for (int i = 0; i < parentGO.transform.childCount; i++)
        {
            // Check if the prevTransform has been assigned yet
            if (!prevTransform)
                prevTransform = tileArray[i].transform; // if not, assign it to the first child


            // Check if the position of the current child we are checking is above the previous one
            if (tileArray[i].transform.position.y > prevTransform.position.y + 0.5f) // needed to add this stupid +0.5 because floating points are the devil
            {
                _2DtileArray.Add(new List<GameObject>(tempColumns)); // Create a new list and pass it into the 2D list
                tempColumns.Clear(); // Clear the tempColumn otherwise it will keep being added to
                prevTransform = tileArray[i].transform; // Have to set this otherwise we get stuck in a loop
            }

            tempColumns.Add(tileArray[i]); // Add it to our temp List
            prevTransform = tileArray[i].transform; // Set the current transform to the previous
        }

        // One last addition to the list, otherwise the final iteration is left out
        _2DtileArray.Add(new List<GameObject>(tempColumns));


        // Output the array to the debug console
        Debug.Log("Size of _2DtileArray: " + _2DtileArray.Count);
        for (int i = 0; i < _2DtileArray.Count; i++)
        {
            for (int j = 0; j < _2DtileArray[i].Count; j++)
            {
                Debug.Log("GO Name: " + _2DtileArray[i][j].name + " is at position in the array: " + i + " " + j);
            }
        }
    }


    //The below function is used to generate resources AKA wood, stone, grass/weeds
    void GenerateResources()
    {

        //Step 1: Select the plot
        //Step 2: Check the plot is empty
        //Step 3: Determine if a 1x1 or 2x2 resource is generated
        //Step 3a: Check the plot to the in a 2x2 is empty
        //Step 4: Choose the resource to generate
        //Step 5: Generate
        //Step 6: ???
        //Step 7: Profit

        int MagicNumber = 5; //The Magic Number is what the random number needs to match to "select the plot" that will be spawned with a resource

        SpawnBigResource(MagicNumber);

        SpawnSmallResource(MagicNumber);




    }


    void SpawnBigResource(int MagicNumber)
    {
        //Spawn a 2x2
        for (int i = 0; i < _2DtileArray.Count - 1; i++) //The _2DtileArray.Count -1 ensures the top row is not chosen when spawning 2x2 objects
        {
            for (int j = 0; j < _2DtileArray[i].Count - 1; j++) //The _2DtileArray.Count -1 ensures the last column is not chosen when spawning 2x2 objects
            {
                int rnd = Random.Range(0, 10); //Generate a number between 0 and 9. 10 is ingored as the max number is inclusive (This is a limitation of the unity engine)
                int posi = i + 1; //Get the current i value from the outer loop and add 1
                int posj = j + 1; //Get the current j value from the inner loop and add 1

                if (MagicNumber == rnd)
                {
                    Debug.Log("Expected Magic number is " + MagicNumber + ". Rnd is " + rnd + ".  GO Name: " + _2DtileArray[i][j].name + " has been selected");

                    if (_2DtileArray[i][j].GetComponent<CropManagerController>().isEmpty == true && //If the plot select is EG: 0, 0
                        _2DtileArray[posi][j].GetComponent<CropManagerController>().isEmpty == true && //And if the plot directly above is empty EG 1, 0
                        _2DtileArray[i][posj].GetComponent<CropManagerController>().isEmpty == true && //And if the plot to the right is empty EG 0, 1
                        _2DtileArray[posi][posj].GetComponent<CropManagerController>().isEmpty == true) //And if the plot diagonally is emplty EG 1, 1 then deploy a 2x2 resource
                    {
                        Debug.Log("GO Name: " + _2DtileArray[i][j].name + " is empty");
                        Debug.Log("GO Name: " + _2DtileArray[posi][j].name + " is empty");
                        Debug.Log("GO Name: " + _2DtileArray[i][posj].name + " is empty");
                        Debug.Log("GO Name: " + _2DtileArray[posi][posj].name + " is empty and should be the difference");


                        Vector3 spawnPosition = _2DtileArray[i][j].transform.position;
                        spawnPosition += Vector3.right * 0.5f;

                        int rndResource = Random.Range(0, 2); //Generate a number between 0 and 1. 2 is ingored as the max number is inclusive (This is a limitation of the unity engine)

                        if (rndResource == 0) //Spawn a BigRock if rndResource = 0
                        {
                            var plotResource = Instantiate(BigRockPrefab, spawnPosition, Quaternion.identity); //Instantiate\Load the BigRock into the Game Scene
                            plotResource.GetComponent<BigRockScript>().GetPlotInfoFromGridScript(_2DtileArray[i][j], _2DtileArray[posi][j], _2DtileArray[i][posj], _2DtileArray[posi][posj]);
                        }
                        else if (rndResource == 1) //Spawn a BigLog if rndResource = 1
                        {
                            var plotResource = Instantiate(BigLogPrefab, spawnPosition, Quaternion.identity); //Instantiate\Load the BigRock into the Game Scene
                            plotResource.GetComponent<BigLogScript>().GetPlotInfoFromGridScript(_2DtileArray[i][j], _2DtileArray[posi][j], _2DtileArray[i][posj], _2DtileArray[posi][posj]);
                        }



                        _2DtileArray[i][j].GetComponent<CropManagerController>().isEmpty = false;
                        _2DtileArray[posi][j].GetComponent<CropManagerController>().isEmpty = false;
                        _2DtileArray[i][posj].GetComponent<CropManagerController>().isEmpty = false;
                        _2DtileArray[posi][posj].GetComponent<CropManagerController>().isEmpty = false;

                        rndResource = -1; //set random to -1 so a new random number can be picked

                    }
                    else
                    {
                        Debug.Log("GO Name: " + _2DtileArray[i][j].name + " has been something on it already");
                    }

                }
                else
                {
                    Debug.Log("Expected Magic number is " + MagicNumber + ". Rnd is " + rnd + ".  GO Name: " + _2DtileArray[i][j].name + " is ignored");
                }

                rnd = -1; //set random to -1 so a new random number can be picked
            }
        }
    }

    void SpawnSmallResource(int MagicNumber)
    {
        //Spawn a 1x1
        for (int i = 0; i < _2DtileArray.Count; i++)
        {
            for (int j = 0; j < _2DtileArray[i].Count; j++)
            {
                int rnd = Random.Range(0, 10); //Generate a number between 0 and 9. 10 is ingored as the max number is inclusive (This is a limitation of the unity engine)

                if (MagicNumber == rnd)
                {
                    Debug.Log("Expected Magic number is " + MagicNumber + ". Rnd is " + rnd + ".  GO Name: " + _2DtileArray[i][j].name + " has been selected");

                    if (_2DtileArray[i][j].GetComponent<CropManagerController>().isEmpty == true) //If the plot select is EG: 0, 0 then deploy a 1x1 resource
                    {
                        Debug.Log("GO Name: " + _2DtileArray[i][j].name + " is empty");


                        Vector3 spawnPosition = _2DtileArray[i][j].transform.position;
                        spawnPosition += Vector3.down * 0.5f;

                        int rndResource = Random.Range(0, 2); //Generate a number between 0 and 1. 2 is ingored as the max number is inclusive (This is a limitation of the unity engine)

                        if (rndResource == 0) //Spawn a BigRock if rndResource = 0
                        {
                            var plotResource = Instantiate(SmallRockPrefab, spawnPosition, Quaternion.identity); //Instantiate\Load the BigRock into the Game Scene
                            plotResource.GetComponent<SmallRockScript>().GetPlotInfoFromGridScript(_2DtileArray[i][j]);
                        }
                        else if (rndResource == 1) //Spawn a BigLog if rndResource = 1
                        {
                            var plotResource = Instantiate(SmallLogPrefab, spawnPosition, Quaternion.identity); //Instantiate\Load the BigRock into the Game Scene
                            plotResource.GetComponent<SmallLogScript>().GetPlotInfoFromGridScript(_2DtileArray[i][j]);
                        }



                        _2DtileArray[i][j].GetComponent<CropManagerController>().isEmpty = false;

                        rndResource = -1; //set random to -1 so a new random number can be picked

                    }
                    else
                    {
                        Debug.Log("GO Name: " + _2DtileArray[i][j].name + " has been something on it already");
                    }

                }
                else
                {
                    Debug.Log("Expected Magic number is " + MagicNumber + ". Rnd is " + rnd + ".  GO Name: " + _2DtileArray[i][j].name + " is ignored");
                }

                rnd = -1; //set random to -1 so a new random number can be picked
            }
        }
    }

    //Return the plotname based on the input
    void WhatsMyName(int i, int j)
    {
        Debug.Log("GridScript Check: GO Name: " + _2DtileArray[i][j].name + " is at position in the array: " + i + " " + j);
    }

}