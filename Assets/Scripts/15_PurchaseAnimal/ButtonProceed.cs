using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonProceed : MonoBehaviour
{

    [SerializeField]
    private TMP_Text inputValue;

    private GameObject go;

    void Update()
    {
    }


    public void buttonPressed()
    {
        Debug.Log("Button was pressed on " + this.name);
        Debug.Log("Input Field value: " + inputValue.text);
    }



}
