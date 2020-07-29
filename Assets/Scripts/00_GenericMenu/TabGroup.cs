using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{

    public List<TabButton> tabButtons; //List of buttons
    public Color32 idleColour = new Color32(255, 255, 255, 255); // Set the colour back to default
    public List<GameObject> gameObjectsToSwap;


    //I don't quite understand this Subscribe method yet?
    public void Subscribe(TabButton button)
    {
        if(tabButtons == null) //If the list of buttons is empty, create new list
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button); //else add the new tabbutton to the list
    }


    public void ResetTabs() //Every time a tab button is interacted with, we need to reset all buttons to there default colour state so the new tab button can be selected
    {

        foreach (TabButton button in tabButtons)
        {
            button.background.color = idleColour;
        }
    }


}
