using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenCoopStartScene : MonoBehaviour
{

    public AnimalValue animalValue;
    public AnimalScript prefab;

    // Start is called before the first frame update
    void Start()
    {

        foreach (AnimalScript chicken in animalValue.listOfAnimals)
        {
            AnimalScript prefab = chicken;
            Vector3 position = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            Instantiate(prefab, position, Quaternion.identity);
        }

    }

}
