using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class ControllerHandFunctionScript : BaseHandFunctionScript
{
    //Singleton Pattern prevents mutiple instances of the class. In this case the HandFunction
    #region Singleton

    public static ControllerHandFunctionScript instance;

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
    [SerializeField] private GameObject deleteButton;
    [SerializeField] private GameObject firstSlotButton;

    //Properties


    //Unity Methods

    private void Start()
    {
        inputSytem = GetComponent<FbGInputControllerV1>();
    }
    void Update()
    {


        if (EventSystem.current.currentSelectedGameObject != null && instance.MyMoveable != null) //If the evenysystem has a selected object, run the code
        {
            HandIcon.transform.position = EventSystem.current.currentSelectedGameObject.transform.position + _offset;
            deleteButton.SetActive(true);
        }
        else if (instance.MyMoveable == null)
        {
            deleteButton.SetActive(false);
        }

        if (EventSystem.current.currentSelectedGameObject == deleteButton && inputSytem.ButtonEast.WasPressed && instance.MyMoveable != null)
        {
            DeleteItem();
        }
    }


    //Methods
    public override void DeleteItem()
    {

        //If the right mouse button is clicked
        //AND the mouse is not over the top of the current gameobject
        //AND MyMoveable has an item attached to it
        base.DeleteItem();
        if (MyMoveable is Item && InventoryScriptV2.instance.fromSlot != null)
        {
            (MyMoveable as Item).MyInventorySlot.ClearSlot();
        }

        DropMoveable();

        deleteButton.SetActive(false);

        EventSystem.current.SetSelectedGameObject(firstSlotButton);

        InventoryScriptV2.instance.fromSlot = null;

    }

}
