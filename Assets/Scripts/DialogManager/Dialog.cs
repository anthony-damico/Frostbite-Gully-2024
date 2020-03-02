using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Line
{
    public Character character;

    [TextArea(1, 4)]
    public string text;
}

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog Options/Dialog")]
public class Dialog : ScriptableObject
{
    public Line[] lines;
    public Dialog nextDialog;
}

