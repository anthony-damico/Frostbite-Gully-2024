using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterEventReceiver : MonoBehaviour
{
    PlayerInput input;

    private void Start()
    {
        input = GetComponent<PlayerInput>();

        //All action events from PlayerInput, this will only fire if the PlayerInput mode is set to C# Events
        input.onActionTriggered += OnActionTriggered;

        //if you know what you're looking for already,  these will fire regardless of the PlayerInput mode.
        //The catch here is that if you switch action maps... :(
        input.currentActionMap["Attack"].started += HandleOnAttack;
        input.currentActionMap["Attack"].performed += HandleOnAttack;
        input.currentActionMap["Attack"].canceled += HandleOnAttack;
    }

    private void HandleOnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("InputAction Event " + context.phase + " " + context.interaction);
    }

    private void OnActionTriggered(InputAction.CallbackContext context)
    {
        Debug.Log("C# Event " + context.action.name + " " + context.phase + " " + context.interaction);
    }
}
