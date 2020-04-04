using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class LightManager : MonoBehaviour
{
    TimeManagerController timeManagerController; //Creates a reference back to the TimeManagerControllerScript to get access to the different time varibales for the UI

    public Light2D globalLight;                        //Global 2d light as part of lightweight renderer 2D 
    public float globalLightIntensity;
    public float currentLerpTime;
    public float lerpTime = 60f;
    public Color32 colour;
    public Color32 fromColour;
    public Color32 toColour;

    float fromIntensity;    //This is the current GlobalLight Intensity that we want to start from
    float toIntensity;      //THis is the intensity we will lerp to
    bool lerpComplete;



    // Start is called before the first frame update
    void Start()
    {
        timeManagerController = TimeManagerController.instance;                                                         //Completes the reference back to the TimeManagerController.cs script
    }

    // Update is called once per frame
    void Update()
    {

        lerpLights();

    }


    public void lerpLights()
    {
        if (currentLerpTime == lerpTime) //If the currentLerpTime = lerpTime, this means a lerp has occured. This check shoudl reset the lerp and have it ready for the next lerp
        {
            StartCoroutine(waitForTime(3f)); //Wait 3 seconds before reseting the currentLerpTime. Its not elegant but it seems to work
        }

        if (timeManagerController.hours >= 4 && timeManagerController.hours < 5) //If time is between 4AM and 5AM
        {
            fromIntensity = 0.5f;
            toIntensity = 1.2f;
            fromColour = new Color32(255, 255, 255, 0); //Normal
            toColour = new Color32(225, 231, 26, 20); //Yellow

            runLerp(fromIntensity, toIntensity, fromColour, toColour);

        }

        if (timeManagerController.hours >= 8 && timeManagerController.hours < 9) //If time is between 8AM and 9AM
        {
            fromIntensity = 1.2f;
            toIntensity = 1.0f;
            fromColour = new Color32(225, 231, 26, 20); //Yellow
            toColour = new Color32(255, 255, 255, 0); //Normal

            runLerp(fromIntensity, toIntensity, fromColour, toColour);

        }

        if (timeManagerController.hours >= 16 && timeManagerController.hours < 17) //If time is between 4PM and 5PM
        {
            fromIntensity = 1.0f;
            toIntensity = 0.9f;
            fromColour = new Color32(255, 255, 255, 0); //Normal
            toColour = new Color32(255, 255, 255, 0); //Normal

            runLerp(fromIntensity, toIntensity, fromColour, toColour);

        }

        if (timeManagerController.hours >= 18 && timeManagerController.hours < 19) //If time is between 6PM and 7PM
        {
            fromIntensity = 0.9f;
            toIntensity = 0.8f;
            fromColour = new Color32(255, 255, 255, 0); //Normal
            toColour = new Color32(234, 176, 21, 20); //Orange

            runLerp(fromIntensity, toIntensity, fromColour, toColour);

        }

        if (timeManagerController.hours >= 20 && timeManagerController.hours < 21) //If time is between 8PM and 9PM
        {
            fromIntensity = 0.8f;
            toIntensity = 0.5f;
            fromColour = new Color32(234, 176, 21, 20); //Orange
            toColour = new Color32(255, 255, 255, 0); //Normal

            runLerp(fromIntensity, toIntensity, fromColour, toColour);

        }

        globalLight.color = colour;
        globalLight.intensity = globalLightIntensity;

    }


    void runLerp(float lerpFromIntensity, float lerpToIntensity, Color32 lerpFromColour, Color32 lerpToColour)
    {
        //Work out how to get game time / realtime to repalce time.deltatime on the current Lerp Time. How to make time.delta time releate to gametime not realtime
        currentLerpTime += Time.deltaTime * (1 / timeManagerController.minuteIntervals); //This allows the the lerp to honor the game time instread of changes per frame
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        //lerp!
        float perc = currentLerpTime / lerpTime;
        globalLightIntensity = Mathf.Lerp(fromIntensity, toIntensity, perc);
        colour = Color.Lerp(fromColour, toColour, perc);
    }

    //This is a untitity function, it ia used to wait a certain amount of seconds
    public IEnumerator waitForTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        currentLerpTime = 0; //This is to reset the current Lerp for the next time a lerp is needed
    }
}
