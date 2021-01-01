using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAnimalScript : MonoBehaviour
{

    UniqueID uniqueID = new UniqueID();

    public AnimalScript chickenPrefab;
    public AnimalStatsSerialization chickenStatsSerialization;

    public void CreateChicken(AnimalScript animalPrefabValue, string buildingUuidValue, string animalNameValue, int animalAgeValue, int animalHealthValue)
    {
        AnimalStats chickenStats = new AnimalStats();
        chickenStats.animalPrefab = animalPrefabValue;
        chickenStats.builidngUuid = buildingUuidValue;
        chickenStats.animalUuid = uniqueID.GenerateUuid();
        chickenStats.animalName = animalNameValue;
        chickenStats.animalAge = animalAgeValue;
        chickenStats.animalHealth = animalHealthValue;
        chickenStatsSerialization.animalList.Add(chickenStats);
    }
    
    public void CreateChickenV2(AnimalStats animalStats)
    {

    }

}
