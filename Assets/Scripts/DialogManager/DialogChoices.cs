using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Choice
{
    [TextArea(1, 4)]
    public string text;

    public Dialog nextDialog;
}

[CreateAssetMenu(fileName = "New Dialog Choices", menuName = "Dialog Options/Dialog Choices")]
public class DialogChoices : ScriptableObject
{
    [TextArea(1, 4)]
    public string headerText; //I might remove this. THis is the text that MIGHT show above a dialog choice branch

    public Choice[] choices;

}
