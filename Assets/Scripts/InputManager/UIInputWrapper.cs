using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEditor;


//A: Grab All items on a slot
//X: Grab 1 item on a slot


public class UIInputWrapper : MonoBehaviour, IPointerClickHandler
{

    public UnityEvent leftClick;
    public UnityEvent middleClick;
    public UnityEvent rightClick;
    public UnityEvent submit;
    public UnityEvent buttonEast;
    public UnityEvent cancel;

    private EventSystem eventSystem;
    public FbGInputControllerV1 inputSystem;
    
    

    public void Start()
    {
        
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        inputSystem = GetComponent<FbGInputControllerV1>(); //Complete a reference to the Arugula Input System Wrapper: http://angryarugula.com/unity/Arugula_InputSystem.unitypackage
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            leftClick.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Middle)
            middleClick.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Right)
            rightClick.Invoke();
    }



    public void Update()
    {

        if (eventSystem.currentSelectedGameObject == this.gameObject)
        {
            if(inputSystem.Submit.WasPressed)
            {
                submit.Invoke();
            }
            else if (inputSystem.ButtonEast.WasPressed)
            {
                buttonEast.Invoke();
            }
        }
       // if(playerInput.submit)


            //if (Input.GetButton("Submit"))
            //{
            //    submit.Invoke();
            //}
            //else if (Input.GetButton("Cancel"))
            //{
            //    cancel.Invoke();
            //}
    }   //
}
