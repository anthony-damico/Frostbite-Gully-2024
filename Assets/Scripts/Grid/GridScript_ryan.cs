using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript_ryan : MonoBehaviour
{

    public GameObject parentGO; // Parent Gameobejct holding all the other children gameObjects

    [SerializeField]
    List<GameObject> tileArray = new List<GameObject>();

    [SerializeField]
    List<List<GameObject>> _2DtileArray = new List<List<GameObject>>();


    bool gameStart;

    // Start is called before the first frame update
    void Start()
    {
        gameStart = true;

        if (gameStart == true)
        {
            #region Ryan's Modifications

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
                if(!prevTransform)
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

            #endregion
            //foreach (GameObject tile in GameObject.FindGameObjectsWithTag("plot"))
            //{
            //    int number = 0;
            //    string tempName = "plot_" + number;
            //    tile.gameObject.name = tempName;
            //    tileArray.Add(tile);
            //    number++;
            //}

            //PopulateNestedList();

            gameStart = false;
        }   
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
    }


    void PopulateNestedList()
    {
        for (int i = 0; i < 2 /*int.MaxValue*/; i++)
        {
            _2DtileArray[i].Add(tileArray[i].GetComponent<GameObject>());

            //for (int j = 0; j < _2DtileArray[i].length; j++)
            //{
            //    Debug.Log("row: " + i + "column: " + j);
            //}
        }
    }

}
