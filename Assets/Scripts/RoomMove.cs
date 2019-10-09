using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{

    //This is a reference to the CameraMovement.cs script
    private CameraMovement cam;

    //This is a reference to the CameraMovement.cs script
    //private screenFade screenFade;

    //These Variables will be used to define the new Min and Max Camera Positions after changing room
    public Vector2 newMaxPosition;
    public Vector2 newMinPosition;

    //The variable is used to determine how far to change the players x and y
    public Vector3 playerChange; //this is an offset

    //This boolan will determine whether location text is shown on screen or not (Location text mean the game area EG City, Forest, Lost Village)
    public bool needText;

    //This will be how each map is named correctly
    public string placeName;

    //This is a referce to the gameobject "Place Text"
    public GameObject text;

    //This is a reference to the actual text stored in the textbox with "Place Text"
    public Text placeText;

    // Start is called before the first frame update
    void Start()
    {
        //This finalises the referecne to the CameraMovement.cs script
        cam = Camera.main.GetComponent<CameraMovement>();
        //screenFade = GameObject.FindObjectOfType(typeof(screenFade)) as screenFade;


    }

    // Update is called once per frame
    void Update()
    {

    }

    //create new method and call the collider "other". Other Means Player
    private void OnTriggerEnter2D(Collider2D other)
    {

        //Checking to see if the player is in the trigger zone by using the Player Tag
        if (other.CompareTag("Player"))
        {

            //StartCoroutine(screenFade.FadeImage(true)); 

            //If player is in trigger zone, access camera and change the camera's location
            cam.minPosition.x = newMinPosition.x;
            cam.minPosition.y = newMinPosition.y;
            cam.maxPosition.x = newMaxPosition.x;
            cam.maxPosition.y = newMaxPosition.y;

            //Move the player into the new room
            other.transform.position += playerChange;

            //other.transform.position = other.transform.position + playerChange;

            if(needText == true)
            {
                //Call the Co-Routine placeNameCo() defined as a IEnumerator
                StartCoroutine(placeNameCo());
            }


        }

    }

    //IEnumerator is a method that runs in parrallel and allows you to have a specified wait tme
    private IEnumerator placeNameCo() //placeNameCo is the method name
    {
        text.SetActive(true); //Set the text game object to be active
        placeText.text = placeName;
        yield return new WaitForSeconds(4f); //this command will wait for 4 seconds
        text.SetActive(false); //Set the text game object to be inactive after the wait time has elasped
    }

}

