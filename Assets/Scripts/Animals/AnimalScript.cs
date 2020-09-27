using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalScript : MonoBehaviour
{
    public string animalUuid;
    public string animalName;
    public int animalAge;
    public int animalHealth;

    public void Initialize(string animalUuidValue, string animalNameValue, int animalAgeValue, int animalHealthValue)
    {
        animalUuid = animalUuidValue;
        animalName = animalNameValue;
        animalAge = animalAgeValue;
        animalHealth = animalHealthValue;
    }



}
