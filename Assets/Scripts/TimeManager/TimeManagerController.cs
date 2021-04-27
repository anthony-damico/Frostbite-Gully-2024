using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManagerController : MonoBehaviour
{

    //Singleton Pattern prevents mutiple instances of the class. In this case the the TimeSystem
    #region Singleton

    public static TimeManagerController instance;

    private void Awake()
    {
        if (instance != null) //If the instance is not null, that means an instance of the TimeSystem is active
        {
            Debug.LogWarning("More then once instance of the Time System has been found");
            return;
        }
        instance = this;
    }

    #endregion

    public float minuteIntervals = 2f; //This is how long to wait before adding Minutes
    public int tenMinutes = 1; //This is the first number in a minute such as 00, 10, 20, 30, 40, 50
    public int minutes = 1; //This is the second number in a minute such as 00, 01, 02, 03, 04, 05, 06, 07, 08, 09
    public int hours = 23; //This is the hours on the clock. There are 24 hours in a day
    public int days = 1; //This is the day of the week on the clock. There are 7 days
    public int date = 29; //This is the day number on the clock. There are 30/31 days in a season
    public int seasons = 4; //This is the season number on the clock. There are 4 seasons in a year
    public int years = 1; //This is the year number on the clock
    public int AMPM = 1; //This will define whether is is AM (0) or PM (1)
    public TimeManagerSerialization timeManagerScriptableObject;

    public Seasons MySeason
    {
        get
        {
            //If the slot is emply OR the count of the max stack size is less then the max count then this slot is free to have the item added to it
            if (seasons == 1)
            {
                return Seasons.spring; //This means the slot is empty. Meaning the slot is not full allowing items to be stacks
            }
            else if(seasons == 2)
            {
                return Seasons.summer;
            }
            else if (seasons == 3)
            {
                return Seasons.autumn;
            }
            else if(seasons == 4)
            {
                return Seasons.winter;
            }

            return Seasons.spring;
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(checkMinutes(minuteIntervals));
        LoadTime();
    }

    // Update is called once per frame
    void Update()
    {
        SaveTime();
    }

    private IEnumerator checkMinutes(float minuteIntervals)
    {
        while(true)
        {
            yield return new WaitForSeconds(minuteIntervals);
            minutes = minutes + 1;

            UpdateTime();
        }
    }

    public void UpdateTime()
    {
        if (minutes > 9)
        {
            tenMinutes = tenMinutes + 1;
            minutes = 0;
        }

        if (tenMinutes > 5)
        {
            hours = hours + 1;
            tenMinutes = 0;
        }

        if (hours > 23) //0 is 12am
        {
            days = days + 1;
            date = date + 1;
            hours = 0;
        }

        if (days > 7)
        {
            days = 1;
            }

        if (date > 30)
        {
            seasons = seasons + 1;
            date = 1;
        }

        if (seasons > 4)
        {
            years = years + 1;
            seasons = 1;
        }

        if (hours >= 12 && hours <= 23) //This is the difference between 12pm and 11pm
        {
            AMPM = 1; //This means PM
        }
        else
        {
            AMPM = 0; //This means AM
        }
    }

    public void SaveTime()
    {
        timeManagerScriptableObject.tenMinutes = tenMinutes;
        timeManagerScriptableObject.minutes = minutes;
        timeManagerScriptableObject.hours = hours;
        timeManagerScriptableObject.days = days;
        timeManagerScriptableObject.date = date;
        timeManagerScriptableObject.seasons = seasons;
        timeManagerScriptableObject.years = years;
        timeManagerScriptableObject.AMPM = AMPM;
    }

    public void LoadTime()
    {
        tenMinutes = timeManagerScriptableObject.tenMinutes;
        minutes = timeManagerScriptableObject.minutes;
        hours = timeManagerScriptableObject.hours;
        days = timeManagerScriptableObject.days;
        date = timeManagerScriptableObject.date;
        seasons = timeManagerScriptableObject.seasons;
        years = timeManagerScriptableObject.years;
        AMPM = timeManagerScriptableObject.AMPM;
    }

}



public enum Seasons
{
    spring,
    summer,
    autumn,
    winter
}