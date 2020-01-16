using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Line
{
    public Character character;

    [TextArea(4, 5)]
    public string text;
}

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog Options/Dialog")]
public class Dialog : ScriptableObject
{
    public Line[] lines;
}

