using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MenuBackground : MonoBehaviour
{
    [SerializeField] public bool menuOpen = false;
    public CanvasGroup menuCanvasGroup; //This is used to Hide and Show the UI element. The Canvas Group is located on the Bag Prefab This is a reference
    private FbGInputControllerV1 inputSystem;
    private EventSystem eventSystem;

    [SerializeField] private CanvasGroup actionBarCanvasGroup; //Currently, this will just be the first ActionSlot. I will try to make the code remember the most recent ActionSlot selected
    [SerializeField] private GameObject actionBarSlot; //Currently, this will just be the first ActionSlot. I will try to make the code remember the most recent ActionSlot selected
    [SerializeField] private GameObject inventorySlot; //Currently, this will just be the first Inventory. I will try to make the code remember the most recent InventorySlot selected

    private void Start()
    {
        menuCanvasGroup = GetComponent<CanvasGroup>(); //This completes the reference to the canvasgroup allow me to access any canvas functions via the variable canvasGroup
        inputSystem = GetComponent<FbGInputControllerV1>(); //Complete a reference to the Arugula Input System Wrapper: http://angryarugula.com/unity/Arugula_InputSystem.unitypackage
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    private void Update()
    {
        //Toggle the menu on/off
        if(inputSystem.ButtonNorth.WasPressed)
        {
            //If button is pressed and the menu is open, set the menuOpen bool to false to close the menu (and vice versa to open)
            if (menuOpen == true)
            {
                menuOpen = false;
                OpenMenu(inventorySlot); //When the menu is open, select the inventroy slot set in the "inventorySlot" variable  in the inspector
            }
            else
            {
                menuOpen = true;
                CloseMenu(actionBarSlot); //When the menu is closed, select the acton slot set in the "actionSlot" variable  in the inspector
            }
        }       
    }

    //This function is used to open and close the Menu Canvas
    public void OpenClose()
    {
        menuCanvasGroup.alpha = menuCanvasGroup.alpha > 0 ? 0 : 1; //If the canvas is hidden, set the alpha to 1, if the canvas is showing, set the alpha to 0. This is like an IF statement

        menuCanvasGroup.blocksRaycasts = menuCanvasGroup.blocksRaycasts == true ? false : true; //If the blockRaycasts is set to true, then set to false, if false, then set to true
    }

    public void OpenMenu(GameObject selectObject)
    {
        //Display the game menu
        menuCanvasGroup.alpha = 1; //Make the menu Visable
        menuCanvasGroup.blocksRaycasts = true;
        menuCanvasGroup.interactable = true; //Enable the interactable compoent of the canvas group so the event system can interact
        eventSystem.SetSelectedGameObject(selectObject); //Set the eventSystem to the required selected object

        //Hide the action bar
        actionBarCanvasGroup.alpha = 0; //Make the menu Visable
        actionBarCanvasGroup.blocksRaycasts = false;
        actionBarCanvasGroup.interactable = false; //Enable the interactable compoent of the canvas group so the event system can interact
    }

    public void CloseMenu(GameObject selectObject)
    {
        //Hide the game menu
        menuCanvasGroup.alpha = 0; //Make the menu Visable
        menuCanvasGroup.blocksRaycasts = false;
        menuCanvasGroup.interactable = false; //Enable the interactable compoent of the canvas group so the event system can interact
        eventSystem.SetSelectedGameObject(selectObject); //Set the eventSystem to the required selected object

        //Display the action bar
        actionBarCanvasGroup.alpha = 1; //Make the menu Visable
        actionBarCanvasGroup.blocksRaycasts = true;
        actionBarCanvasGroup.interactable = true; //Enable the interactable compoent of the canvas group so the event system can interact
    }


}
