using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Character", menuName = "Dialog Options/Character")]
public class Character : ScriptableObject
{
    public string characterName;        //The characters name that will appear when dialog is active
    public Sprite characterPortrait;    //The characters portrait that will appear when dialog is active

}
