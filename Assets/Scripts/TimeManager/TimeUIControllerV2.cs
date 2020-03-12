using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.LWRP;

public class TimeUIControllerV2 : MonoBehaviour
{

    TimeManagerController timeManagerController; //Creates a reference back to the TimeManagerControllerScript to get access to the different time varibales for the UI
    PlayerStats playerStats;                    //This creates a areferce to the PlayerStats.cs script


    public Image imageHealthBar; //This is the HealthBar GameObject located under the TimeManager
    public Image imageHour; //This is the Hour GameObject located under the TimeManager
    public Image imageColonMinute; //The is the Colon-Minue Game object located under the TimeManager
    public Image imageDayName; //This is the DayName GameObject located under the TimeManager
    public Image imageDayNumber; //This is the DayNumber GameObject located under the TimeManager

    public Sprite zero; //This will be the sprite for 00
    public Sprite one; //This will be the sprite for 01
    public Sprite two; //This will be the sprite for 02
    public Sprite three; //This will be the sprite for 03
    public Sprite four; //This will be the sprite for 04
    public Sprite five; //This will be the sprite for 05
    public Sprite six; //This will be the sprite for 06
    public Sprite seven; //This will be the sprite for 07
    public Sprite eight; //This will be the sprite for 08
    public Sprite nine; //This will be the sprite for 09
    public Sprite ten; //This will be the sprite for 10
    public Sprite eleven; //This will be the sprite for 11
    public Sprite twelve; //This will be the sprite for 12
    public Sprite thirteen; //This will be the sprite for 13
    public Sprite fourteen; //This will be the sprite for 14
    public Sprite fifteen; //This will be the sprite for 15
    public Sprite sixteen; //This will be the sprite for 16
    public Sprite seventeen; //This will be the sprite for 17
    public Sprite eighteen; //This will be the sprite for 18
    public Sprite ninteen; //This will be the sprite for 19
    public Sprite twenty; //This will be the sprite for 20
    public Sprite twentyone; //This will be the sprite for 21
    public Sprite twentytwo; //This will be the sprite for 22
    public Sprite twentythree; //This will be the sprite for 23

    public Sprite colonZero; //This will be the sprite for :0
    public Sprite colonOne; //This will be the sprite for :1
    public Sprite colonTwo; //This will be the sprite for :2
    public Sprite colonThree; //This will be the sprite for :3
    public Sprite colonFour; //This will be the sprite for :4
    public Sprite colonFive; //This will be the sprite for :5
    public Sprite monday; //This will be the sprite for Monday
    public Sprite tuesday; //This will be the sprite for Tuesday
    public Sprite wednesday; //This will be the sprite for Wednesday
    public Sprite thursday; //This will be the sprite for Thursday
    public Sprite friday; //This will be the sprite for Friday
    public Sprite saturday; //This will be the sprite for Saturday
    public Sprite sunday; //This will be the sprite for Sunday
    public Sprite day1; //
    public Sprite day2; //
    public Sprite day3; //
    public Sprite day4; //
    public Sprite day5; //
    public Sprite day6; //
    public Sprite day7; //
    public Sprite day8; //
    public Sprite day9; //
    public Sprite day10; //
    public Sprite day11; //
    public Sprite day12; //
    public Sprite day13; //
    public Sprite day14; //
    public Sprite day15; //
    public Sprite day16; //
    public Sprite day17; //
    public Sprite day18; //
    public Sprite day19; //
    public Sprite day20; //
    public Sprite day21; //
    public Sprite day22; //
    public Sprite day23; //
    public Sprite day24; //
    public Sprite day25; //
    public Sprite day26; //
    public Sprite day27; //
    public Sprite day28; //
    public Sprite day29; //
    public Sprite day30; //
    public Sprite day31; //

    public Sprite hp000; // This is a sprite for the healthbar located under the TimeManager (0% Health)
    public Sprite hp001; // This is a sprite for the healthbar located under the TimeManager (1% Health)
    public Sprite hp002; // This is a sprite for the healthbar located under the TimeManager (2% Health)
    public Sprite hp003; // This is a sprite for the healthbar located under the TimeManager (3% Health)
    public Sprite hp004; // This is a sprite for the healthbar located under the TimeManager (4% Health)
    public Sprite hp005; // This is a sprite for the healthbar located under the TimeManager (5% Health)
    public Sprite hp010; // This is a sprite for the healthbar located under the TimeManager (10% Health)
    public Sprite hp015; // This is a sprite for the healthbar located under the TimeManager (15% Health)
    public Sprite hp020; // This is a sprite for the healthbar located under the TimeManager (20% Health)
    public Sprite hp025; // This is a sprite for the healthbar located under the TimeManager (25% Health)
    public Sprite hp030; // This is a sprite for the healthbar located under the TimeManager (30% Health)
    public Sprite hp035; // This is a sprite for the healthbar located under the TimeManager (35% Health)
    public Sprite hp040; // This is a sprite for the healthbar located under the TimeManager (40% Health)
    public Sprite hp045; // This is a sprite for the healthbar located under the TimeManager (45% Health)
    public Sprite hp050; // This is a sprite for the healthbar located under the TimeManager (50% Health)
    public Sprite hp055; // This is a sprite for the healthbar located under the TimeManager (55% Health)
    public Sprite hp060; // This is a sprite for the healthbar located under the TimeManager (60% Health)
    public Sprite hp065; // This is a sprite for the healthbar located under the TimeManager (65% Health)
    public Sprite hp070; // This is a sprite for the healthbar located under the TimeManager (70% Health)
    public Sprite hp075; // This is a sprite for the healthbar located under the TimeManager (75% Health)
    public Sprite hp080; // This is a sprite for the healthbar located under the TimeManager (80% Health)
    public Sprite hp085; // This is a sprite for the healthbar located under the TimeManager (85% Health)
    public Sprite hp090; // This is a sprite for the healthbar located under the TimeManager (90% Health)
    public Sprite hp095; // This is a sprite for the healthbar located under the TimeManager (95% Health)
    public Sprite hp100; // This is a sprite for the healthbar located under the TimeManager (100% Health)

    public float healthAsPercentage;                      //Players Health as a percentage
    public Light2D globalLight;                        //Global 2d light as part of lightweight renderer 2D 
    public float globalLightIntensity;




    // Start is called before the first frame update
    void Start()
    {
        timeManagerController = TimeManagerController.instance;                                                         //Completes the reference back to the TimeManagerController.cs script
        playerStats = GameObject.FindObjectOfType(typeof(PlayerStats)) as PlayerStats;                                  //This complete the reference to the PlayerStats.cs Script
    }

    // Update is called once per frame
    void Update()
    {
        UpdateImageColonMinute();
        UpdateImageHour();
        UpdateImageDayName();
        UpdateImageDayNumber();
        UpdateImageHealthBar();

        //lightComp.color = Color.blue;
        globalLight.intensity = globalLightIntensity;

    }

    void UpdateImageHealthBar()
    {

        healthAsPercentage = Mathf.Round((playerStats.PlayerCurrentHealth / playerStats.PlayerMaxHealth) * 100); //Converts player health to a percentage

        if (healthAsPercentage >= 100)
        {
            imageHealthBar.sprite = hp100;
        }

        if (Between(healthAsPercentage, 96, 100))
        {
            imageHealthBar.sprite = hp100;
        }

        if (Between(healthAsPercentage, 91, 95))
        {
            imageHealthBar.sprite = hp095;
        }

        if (Between(healthAsPercentage, 86, 90))
        {
            imageHealthBar.sprite = hp090;
        }
        
        if (Between(healthAsPercentage, 81, 85))
        {
            imageHealthBar.sprite = hp085;
        }
        
        if (Between(healthAsPercentage, 76, 80))
        {
            imageHealthBar.sprite = hp080;
        }
        
        if (Between(healthAsPercentage, 71, 75))
        {
            imageHealthBar.sprite = hp075;
        }
        
        if (Between(healthAsPercentage, 66, 70))
        {
            imageHealthBar.sprite = hp070;
        }
        
        if (Between(healthAsPercentage, 61, 65))
        {
            imageHealthBar.sprite = hp065;
        }
        
        if (Between(healthAsPercentage, 56, 60))
        {
            imageHealthBar.sprite = hp060;
        }
        
        if (Between(healthAsPercentage, 51, 55))
        {
            imageHealthBar.sprite = hp055;
        }
        
        if (Between(healthAsPercentage, 46, 50))
        {
            imageHealthBar.sprite = hp050;
        }
        
        if (Between(healthAsPercentage, 41, 45))
        {
            imageHealthBar.sprite = hp045;
        }
        
        if (Between(healthAsPercentage, 36, 40))
        {
            imageHealthBar.sprite = hp040;
        }
        
        if (Between(healthAsPercentage, 31, 35))
        {
            imageHealthBar.sprite = hp035;
        }
        
        if (Between(healthAsPercentage, 26, 30))
        {
            imageHealthBar.sprite = hp030;
        }
        
        if (Between(healthAsPercentage, 21, 25))
        {
            imageHealthBar.sprite = hp025;
        }
        
        if (Between(healthAsPercentage, 16, 20))
        {
            imageHealthBar.sprite = hp020;
        }
        
        if (Between(healthAsPercentage, 11, 15))
        {
            imageHealthBar.sprite = hp015;
        }
        
        if (Between(healthAsPercentage, 6, 10))
        {
            imageHealthBar.sprite = hp010;
        }
        
        if (healthAsPercentage == 5)
        {
            imageHealthBar.sprite = hp005;
        }
        
        if (healthAsPercentage == 4) 
        {
            imageHealthBar.sprite = hp004;
        }
        
        if (healthAsPercentage == 3)
        {
            imageHealthBar.sprite = hp003;
        }
        
        if (healthAsPercentage == 2)
        {
            imageHealthBar.sprite = hp002;
        }

        if (healthAsPercentage == 1)
        {
            imageHealthBar.sprite = hp001;
        }
        
        if (healthAsPercentage <= 0)
        {
            imageHealthBar.sprite = hp000;
        }

    }

    void UpdateImageDayName() //This is used to update GameObject/Image known as DayName located under TimeManager
    {
        //Switch is a nicer way of doing IF ELSE statements
        switch(timeManagerController.days) //This is the variable that the switch statement will use for each CASE statement
        {
            case 1:
                imageDayName.sprite = monday; //Set GameObject/Image known as DayName to have image monday
                break; //Break will jump out of this section of code between {}
            case 2:
                imageDayName.sprite = tuesday; //Set GameObject/Image known as DayName to have image tuesday
                break; //Break will jump out of this section of code between {}
            case 3:
                imageDayName.sprite = wednesday; //Set GameObject/Image known as DayName to have image wednesday
                break; //Break will jump out of this section of code between {}
            case 4:
                imageDayName.sprite = thursday; //Set GameObject/Image known as DayName to have image thursday
                break; //Break will jump out of this section of code between {}
            case 5:
                imageDayName.sprite = friday; //Set GameObject/Image known as DayName to have image friday
                break; //Break will jump out of this section of code between {}
            case 6:
                imageDayName.sprite = saturday; //Set GameObject/Image known as DayName to have image saturday
                break; //Break will jump out of this section of code between {}
            case 7:
                imageDayName.sprite = sunday; //Set GameObject/Image known as DayName to have image sunday
                break; //Break will jump out of this section of code between {}
        }
    }

    void UpdateImageHour() //This is used to update GameObject/Image known as DayName located under TimeManager
    {
        //Switch is a nicer way of doing IF ELSE statements
        switch (timeManagerController.hours) //This is the variable that the switch statement will use for each CASE statement
        {
            case 0:
                imageHour.sprite = twelve; //Set GameObject/Image known as ImageHour to have image One (12am)
                break; //Break will jump out of this section of code between {}
            case 1:
                imageHour.sprite = one; //Set GameObject/Image known as ImageHour to have image One (1am)
                break; //Break will jump out of this section of code between {}
            case 2:
                imageHour.sprite = two; //Set GameObject/Image known as ImageHour to have image One (2am)
                break; //Break will jump out of this section of code between {}
            case 3:
                imageHour.sprite = three; //Set GameObject/Image known as ImageHour to have image One (3am)
                break; //Break will jump out of this section of code between {}
            case 4:
                imageHour.sprite = four; //Set GameObject/Image known as ImageHour to have image One (4am)
                break; //Break will jump out of this section of code between {}
            case 5:
                imageHour.sprite = five; //Set GameObject/Image known as ImageHour to have image One (5am)
                break; //Break will jump out of this section of code between {}
            case 6:
                imageHour.sprite = six; //Set GameObject/Image known as ImageHour to have image One (6am)
                break; //Break will jump out of this section of code between {}
            case 7:
                imageHour.sprite = seven; //Set GameObject/Image known as ImageHour to have image One (7am)
                break; //Break will jump out of this section of code between {}
            case 8:
                imageHour.sprite = eight; //Set GameObject/Image known as ImageHour to have image One (8am)
                break; //Break will jump out of this section of code between {}
            case 9:
                imageHour.sprite = nine; //Set GameObject/Image known as ImageHour to have image One (9am)
                break; //Break will jump out of this section of code between {}
            case 10:
                imageHour.sprite = ten; //Set GameObject/Image known as ImageHour to have image One (10am)
                break; //Break will jump out of this section of code between {}
            case 11:
                imageHour.sprite = eleven; //Set GameObject/Image known as ImageHour to have image One (11am)
                break; //Break will jump out of this section of code between {}
            case 12:
                imageHour.sprite = twelve; //Set GameObject/Image known as ImageHour to have image One (12pm)
                break; //Break will jump out of this section of code between {}
            case 13:
                imageHour.sprite = one; //Set GameObject/Image known as ImageHour to have image One (1pm)
                break; //Break will jump out of this section of code between {}
            case 14:
                imageHour.sprite = two; //Set GameObject/Image known as ImageHour to have image One (2pm)
                break; //Break will jump out of this section of code between {}
            case 15:
                imageHour.sprite = three; //Set GameObject/Image known as ImageHour to have image One (3pm)
                break; //Break will jump out of this section of code between {}
            case 16:
                imageHour.sprite = four; //Set GameObject/Image known as ImageHour to have image One (4pm)
                break; //Break will jump out of this section of code between {}
            case 17:
                imageHour.sprite = five; //Set GameObject/Image known as ImageHour to have image One (5pm)
                break; //Break will jump out of this section of code between {}
            case 18:
                imageHour.sprite = six; //Set GameObject/Image known as ImageHour to have image One (6pm)
                break; //Break will jump out of this section of code between {}
            case 19:
                imageHour.sprite = seven; //Set GameObject/Image known as ImageHour to have image One (7pm)
                break; //Break will jump out of this section of code between {}
            case 20:
                imageHour.sprite = eight; //Set GameObject/Image known as ImageHour to have image One (8pm)
                break; //Break will jump out of this section of code between {}
            case 21:
                imageHour.sprite = nine; //Set GameObject/Image known as ImageHour to have image One (9pm)
                break; //Break will jump out of this section of code between {}
            case 22:
                imageHour.sprite = ten; //Set GameObject/Image known as ImageHour to have image One (10pm)
                break; //Break will jump out of this section of code between {}
            case 23:
                imageHour.sprite = eleven; //Set GameObject/Image known as ImageHour to have image One (11pm)
                break; //Break will jump out of this section of code between {}
        }
    }

    void UpdateImageColonMinute() //This is used to update GameObject/Image known as DayName located under TimeManager
    {
        //Switch is a nicer way of doing IF ELSE statements
        switch (timeManagerController.tenMinutes) //This is the variable that the switch statement will use for each CASE statement
        {
            case 0:
                imageColonMinute.sprite = colonZero; //Set GameObject/Image known as ColonMinute to have image colon-zero (:0)
                break; //Break will jump out of this section of code between {}
            case 1:
                imageColonMinute.sprite = colonOne; //Set GameObject/Image known as ColonMinute to have image One colon-one (:1)
                break; //Break will jump out of this section of code between {}
            case 2:
                imageColonMinute.sprite = colonTwo; //Set GameObject/Image known as ColonMinute to have image One colon-two (:2)
                break; //Break will jump out of this section of code between {}
            case 3:
                imageColonMinute.sprite = colonThree; //Set GameObject/Image known as ColonMinute to have image One colon-three (:3)
                break; //Break will jump out of this section of code between {}
            case 4:
                imageColonMinute.sprite = colonFour; //Set GameObject/Image known as ColonMinute to have image One colon-four (:4)
                break; //Break will jump out of this section of code between {}
            case 5:
                imageColonMinute.sprite = colonFive; //Set GameObject/Image known as ColonMinute to have image One colon-five (:5)
                break; //Break will jump out of this section of code between {}
        }
    }

    void UpdateImageDayNumber() //This is used to update GameObject/Image known as DayName located under TimeManager
    {
        //Switch is a nicer way of doing IF ELSE statements
        switch (timeManagerController.date) //This is the variable that the switch statement will use for each CASE statement
        {
            case 1:
                imageDayNumber.sprite = day1; //Set GameObject/Image known as imageDayNumber to have image day1 (1st)
                break; //Break will jump out of this section of code between {}
            case 2:
                imageDayNumber.sprite = day2; //Set GameObject/Image known as imageDayNumber to have image day2 (2nd)
                break; //Break will jump out of this section of code between {}
            case 3:
                imageDayNumber.sprite = day3; //Set GameObject/Image known as imageDayNumber to have image day3 (3rd)
                break; //Break will jump out of this section of code between {}
            case 4:
                imageDayNumber.sprite = day4; //Set GameObject/Image known as imageDayNumber to have image day4 (4th)
                break; //Break will jump out of this section of code between {}
            case 5:
                imageDayNumber.sprite = day5; //Set GameObject/Image known as imageDayNumber to have image day4 (4th)
                break; //Break will jump out of this section of code between {}
            case 6:
                imageDayNumber.sprite = day6; //Set GameObject/Image known as imageDayNumber to have image day4 (4th)
                break; //Break will jump out of this section of code between {}
            case 7:
                imageDayNumber.sprite = day7; //Set GameObject/Image known as imageDayNumber to have image day4 (4th)
                break; //Break will jump out of this section of code between {}
            case 8:
                imageDayNumber.sprite = day8; //Set GameObject/Image known as imageDayNumber to have image day4 (4th)
                break; //Break will jump out of this section of code between {}
            case 9:
                imageDayNumber.sprite = day9; //Set GameObject/Image known as imageDayNumber to have imageday4 (4th)
                break; //Break will jump out of this section of code between {}
            case 10:
                imageDayNumber.sprite = day10; //Set GameObject/Image known as imageDayNumber to have image day4 (4th)
                break; //Break will jump out of this section of code between {}
            case 11:
                imageDayNumber.sprite = day11; //Set GameObject/Image known as imageDayNumber to have imageday4 (4th)
                break; //Break will jump out of this section of code between {}
            case 12:
                imageDayNumber.sprite = day12; //Set GameObject/Image known as imageDayNumber to have imageday4 (4th)
                break; //Break will jump out of this section of code between {}
            case 13:
                imageDayNumber.sprite = day13; //Set GameObject/Image known as imageDayNumber to have image day4 (4th)
                break; //Break will jump out of this section of code between {}
            case 14:
                imageDayNumber.sprite = day14; //Set GameObject/Image known as imageDayNumber to have image day4 (4th)
                break; //Break will jump out of this section of code between {}
            case 15:
                imageDayNumber.sprite = day15; //Set GameObject/Image known as imageDayNumber to have image day4 (4th)
                break; //Break will jump out of this section of code between {}
            case 16:
                imageDayNumber.sprite = day16; //Set GameObject/Image known as imageDayNumber to have image day4 (4th)
                break; //Break will jump out of this section of code between {}
            case 17:
                imageDayNumber.sprite = day17; //Set GameObject/Image known as imageDayNumber to have image day4 (4th)
                break; //Break will jump out of this section of code between {}
            case 18:
                imageDayNumber.sprite = day18; //Set GameObject/Image known as imageDayNumber to have image day4 (4th)
                break; //Break will jump out of this section of code between {}
            case 19:
                imageDayNumber.sprite = day19; //Set GameObject/Image known as imageDayNumber to have image One (7pm)
                break; //Break will jump out of this section of code between {}
            case 20:
                imageDayNumber.sprite = day20; //Set GameObject/Image known as imageDayNumber to have image One (8pm)
                break; //Break will jump out of this section of code between {}
            case 21:
                imageDayNumber.sprite = day21; //Set GameObject/Image known as imageDayNumber to have image One (9pm)
                break; //Break will jump out of this section of code between {}
            case 22:
                imageDayNumber.sprite = day22; //Set GameObject/Image known as imageDayNumber to have image One (10pm)
                break; //Break will jump out of this section of code between {}
            case 23:
                imageDayNumber.sprite = day23; //Set GameObject/Image known as imageDayNumber to have image One (11pm)
                break; //Break will jump out of this section of code between {}
            case 24:
                imageDayNumber.sprite = day24; //Set GameObject/Image known as imageDayNumber to have image One (11pm)
                break; //Break will jump out of this section of code between {}
            case 25:
                imageDayNumber.sprite = day25; //Set GameObject/Image known as imageDayNumber to have image One (11pm)
                break; //Break will jump out of this section of code between {}
            case 26:
                imageDayNumber.sprite = day26; //Set GameObject/Image known as imageDayNumber to have image One (11pm)
                break; //Break will jump out of this section of code between {}
            case 27:
                imageDayNumber.sprite = day27; //Set GameObject/Image known as imageDayNumber to have image One (11pm)
                break; //Break will jump out of this section of code between {}
            case 28:
                imageDayNumber.sprite = day28; //Set GameObject/Image known as imageDayNumber to have image One (11pm)
                break; //Break will jump out of this section of code between {}
            case 29:
                imageDayNumber.sprite = day29; //Set GameObject/Image known as imageDayNumber to have image One (11pm)
                break; //Break will jump out of this section of code between {}
            case 30:
                imageDayNumber.sprite = day30; //Set GameObject/Image known as imageDayNumber to have image One (11pm)
                break; //Break will jump out of this section of code between {}
            case 31:
                imageDayNumber.sprite = day31; //Set GameObject/Image known as imageDayNumber to have image One (11pm)
                break; //Break will jump out of this section of code between {}
        }
    }

    //Untiliy function. Between. Check if a number is between 2 numbers and return a true
    bool Between(float num, float lower, float upper)
    {
        if (num >= lower && num <= upper)
        {
            return true;
        }

        return false;
    }

}
