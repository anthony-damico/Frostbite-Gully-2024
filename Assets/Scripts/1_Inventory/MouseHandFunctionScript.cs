using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class MouseHandFunctionScript : BaseHandFunctionScript
{

    //Singleton Pattern prevents mutiple instances of the class. In this case the HandFunction
    #region Singleton

    public static MouseHandFunctionScript instance;

    private void Awake()
    {
        if (instance != null) //If the instance is not null, that means an instance of the MouseHandFunctionScript is active
        {
            Debug.LogWarning("More then once instance of the Hand Function has been found");
            return;
        }
        instance = this;
    }

    #endregion


    //Variables
    [SerializeField] private FbGInputControllerV1 inputSytem;

    //Properties


    //Unity Methods

    private void Start()
    {
        inputSytem = GetComponent<FbGInputControllerV1>();
    }
    void Update()
    {
        //This moves the _icon (HandIcon) to the position of the mouse
        HandIcon.transform.position = Input.mousePosition + _offset; //ScreenToViewPointer Logic from Here: https://forum.unity.com/threads/mouse-position-with-new-input-system.829248/

        DeleteItem();
    }


    //Methods
    public override void DeleteItem()
    {
        
        //If the right mouse button is clicked
        //AND the mouse is not over the top of the current gameobject
        //AND MyMoveable has an item attached to it
        if (inputSytem.RightClick.WasPressed && EventSystem.current.IsPointerOverGameObject() == false && instance.MyMoveable != null)
        {
            base.DeleteItem();
            if (MyMoveable is Item && InventoryScriptV2.instance.fromSlot != null)
            {
                (MyMoveable as Item).MyInventorySlot.ClearSlot(); 
            }
            
            DropMoveable();
            
            InventoryScriptV2.instance.fromSlot = null;

        }

    }

}
