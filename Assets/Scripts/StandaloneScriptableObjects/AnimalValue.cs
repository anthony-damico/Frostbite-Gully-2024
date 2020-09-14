using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimalValue", menuName = "SceneData/AnimalValue", order = 1)]
public class AnimalValue : ScriptableObject
{

    public List<AnimalScript> listOfAnimals = new List<AnimalScript>();

}
