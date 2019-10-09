using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUIController : MonoBehaviour
{

    TimeManagerController timeManagerController; //Creates a reference back to the TimeManagerControllerScript to get access to the different time varibales for the UI

    public Image imageHour; //This is the Hour GameObject located under the TimeManager
    public Image imageColonMinute; //
    public Image imageMinute; //
    public Image imageAMPM; //
    public Image imageDayName; //This is the DayName GameObject located under the TimeManager
    public Image imageDayNumber; //
    public Image imageSeason; //

    public Sprite zero; //This will be the sprite for 0
    public Sprite one; //This will be the sprite for 1
    public Sprite two; //This will be the sprite for 2
    public Sprite three; //This will be the sprite for 3
    public Sprite four; //This will be the sprite for 4
    public Sprite five; //This will be the sprite for 5
    public Sprite six; //This will be the sprite for 6
    public Sprite seven; //This will be the sprite for 7
    public Sprite eight; //This will be the sprite for 8
    public Sprite nine; //This will be the sprite for 9
    public Sprite ten; //This will be the sprite for 10
    public Sprite eleven; //This will be the sprite for 11
    public Sprite twelve; //This will be the sprite for 12
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
    public Sprite am; //
    public Sprite pm; //
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
    public Sprite spring; //
    public Sprite summer; //
    public Sprite autumn; //
    public Sprite winter; //



    // Start is called before the first frame update
    void Start()
    {
        timeManagerController = TimeManagerController.instance; //Completes the reference back to the TimeManagerController.cs script
    }

    // Update is called once per frame
    void Update()
    {
        UpdateImageAMPM();
        UpdateImageMinute();
        UpdateImageColonMinute();
        UpdateImageHour();
        UpdateImageDayName();
        UpdateImageDayNumber();
        UpdateImageSeason();
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

    void UpdateImageMinute() //This is used to update GameObject/Image known as Minutes located under TimeManager
    {
        //Switch is a nicer way of doing IF ELSE statements
        switch (timeManagerController.minutes) //This is the variable that the switch statement will use for each CASE statement
        {
            case 0:
                imageMinute.sprite = zero; //Set GameObject/Image known as Minutes to have image zero (0)
                break; //Break will jump out of this section of code between {}
            case 1:
                imageMinute.sprite = one; //Set GameObject/Image known as Minutes to have image one (1)
                break; //Break will jump out of this section of code between {}
            case 2:
                imageMinute.sprite = two; //Set GameObject/Image known as Minutes to have image two (2)
                break; //Break will jump out of this section of code between {}
            case 3:
                imageMinute.sprite = three; //Set GameObject/Image known as Minutes to have three (3)
                break; //Break will jump out of this section of code between {}
            case 4:
                imageMinute.sprite = four; //Set GameObject/Image known as Minutes to have four (4)
                break; //Break will jump out of this section of code between {}
            case 5:
                imageMinute.sprite = five; //Set GameObject/Image known as Minutes to have five (5)
                break; //Break will jump out of this section of code between {}
            case 6:
                imageMinute.sprite = six; //Set GameObject/Image known as Minutes to have six (6)
                break; //Break will jump out of this section of code between {}
            case 7:
                imageMinute.sprite = seven; //Set GameObject/Image known as Minutes to have seven (7)
                break; //Break will jump out of this section of code between {}
            case 8:
                imageMinute.sprite = eight; //Set GameObject/Image known as Minutes to have eight (8)
                break; //Break will jump out of this section of code between {}
            case 9:
                imageMinute.sprite = nine; //Set GameObject/Image known as Minutes to have nine (9)
                break; //Break will jump out of this section of code between {}
        }
    }

    void UpdateImageAMPM() //This is used to update GameObject/Image known as AMPM located under TimeManager
    {
        //Switch is a nicer way of doing IF ELSE statements
        switch (timeManagerController.AMPM) //This is the variable that the switch statement will use for each CASE statement
        {
            case 0:
                imageAMPM.sprite = am; //Set GameObject/Image known as AMPM to have image am (A)
                break; //Break will jump out of this section of code between {}
            case 1:
                imageAMPM.sprite = pm; //Set GameObject/Image known as AMPM to have image pm (P)
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

    void UpdateImageSeason() //This is used to update GameObject/Image known as AMPM located under TimeManager
    {
        //Switch is a nicer way of doing IF ELSE statements
        switch (timeManagerController.seasons) //This is the variable that the switch statement will use for each CASE statement
        {
            case 1:
                imageSeason.sprite = spring; //Set GameObject/Image known as Seasons to have image spring
                break; //Break will jump out of this section of code between {}
            case 2:
                imageSeason.sprite = summer; //Set GameObject/Image known as Seasons to have image summer
                break; //Break will jump out of this section of code between {}
            case 3:
                imageSeason.sprite = autumn; //Set GameObject/Image known as Seasons to have image autumn
                break; //Break will jump out of this section of code between {}
            case 4:
                imageSeason.sprite = winter; //Set GameObject/Image known as Seasons to have image winter
                break; //Break will jump out of this section of code between {}
        }
    }
}
