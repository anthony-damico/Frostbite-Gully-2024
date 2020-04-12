using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterUnityEventReceiver : MonoBehaviour
{
    //this is manually added to the Unity Event list in PlayerInput.
    //It must be public or it will not show up in the drop down.
    //This will only be called if PlayerInput is set to Unity Events mode.
    public void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("UnityEvent OnAttack " + context.phase);
    }
}
