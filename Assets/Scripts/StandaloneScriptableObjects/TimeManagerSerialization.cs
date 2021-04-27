using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TimeManagerSerialization", menuName = "SceneData/TimeManagerSerialization", order = 4)]
public class TimeManagerSerialization : ScriptableObject
{
    public int tenMinutes;          //This is the first number in a minute such as 00, 10, 20, 30, 40, 50
    public int minutes;             //This is the second number in a minute such as 00, 01, 02, 03, 04, 05, 06, 07, 08, 09
    public int hours;               //This is the hours on the clock. There are 24 hours in a day
    public int days;                //This is the day of the week on the clock. There are 7 days
    public int date;                //This is the day number on the clock. There are 30/31 days in a season
    public int seasons;             //This is the season number on the clock. There are 4 seasons in a year
    public int years;               //This is the year number on the clock
    public int AMPM;                //This will define whether is is AM (0) or PM (1)
}
