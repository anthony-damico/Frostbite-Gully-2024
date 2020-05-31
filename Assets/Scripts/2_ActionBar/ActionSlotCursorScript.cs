using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEditor;
public class ActionSlotCursorScript : MonoBehaviour
{

    private EventSystem eventSystem;

    // Start is called before the first frame update
    void Start()
    {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = eventSystem.currentSelectedGameObject.transform.position;
    }
}
