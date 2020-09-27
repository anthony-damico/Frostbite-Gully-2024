using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBuildingStartScene : MonoBehaviour
{

    public AnimalStatsSerialization chickenStatsSerialization; //Map in the editor to the SO ChickenStatsSerialization
    public VectorValue vectorValue; //Map in the editor to the SO VectorVale

    private void Start()
    {
        foreach (AnimalStats animal in chickenStatsSerialization.animalList)
        {
            if(animal.builidngUuid == vectorValue.buildingUuid)
            {
                AnimalScript chickenPrefab = animal.animalPrefab;
                Vector3 position = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
                AnimalScript ChickenToInstantiate = Instantiate(chickenPrefab, position, Quaternion.identity);
                ChickenToInstantiate.Initialize(animal.animalUuid, animal.animalName, animal.animalAge, animal.animalHealth);
            }
        }
    }
}
