using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TabButton : MonoBehaviour
{

    public string buttonName = "Enter Button Name"; //Set the Name in the inspector. Will be replaced with an Imange in the future
    public Text buttonText; //Mapped in the edit, will be updated to reflect the name of the button
    public string headerName = "Enter Header Text";
    public TextMeshProUGUI headerText; //Mapped via the start method

    public TabGroup tabGroup; //This is a reference to the parent tabGroup gameObject set in the inspector

    //255 255 255 255 is the default colour
    //106 106 106 255 is a darker colour to show the button has been selected
    public Image background;
    public Color32 idleColour = new Color32(255, 255, 255, 255); // Set the colour back to default
    public Color32 selectedColour = new Color32(106, 106, 106, 255); // Set the colour back to default

    // Start is called before the first frame update
    void Start()
    {
        headerText = GameObject.Find("HeaderText").GetComponent<TextMeshProUGUI>();
        tabGroup = GameObject.Find("TabGroup").GetComponent<TabGroup>(); //Create a referenece to the TabGroup GameObject
        background = GetComponent<Image>(); //Complete a reference to the image compoent of this button
        tabGroup.Subscribe(this); //Add this button to the tabButton list in the TabGroup class

        buttonText.text = buttonName; //Requried here on game load to ensure a heading is generated in the menu
        headerText.text = headerName; //Requried here on game load to ensure a heading is generated in the menu

    }

    // Update is called once per frame
    void Update()
    {

    }

    //The below 3 methods are used to change the state of the tabButton whenever a tabButton is interacted with
    public void OnTabEnter(TabButton button)
    {
        tabGroup.ResetTabs();
    }

    public void OnTabExit(TabButton button)
    {
        tabGroup.ResetTabs();

    }

    public void OnTabSelected(TabButton button)
    {
        tabGroup.ResetTabs();
        button.background.color = selectedColour;
        buttonText.text = buttonName;
        headerText.text = headerName;
        int index = button.transform.GetSiblingIndex(); //Get the index of this gameObject

        for (int i = 0; i < tabGroup.gameObjectsToSwap.Count; i++)
        {
            if(i == index)
            {
                tabGroup.gameObjectsToSwap[i].SetActive(true);
            }
            else
            {
                tabGroup.gameObjectsToSwap[i].SetActive(false);
            }
        }

    }

}
