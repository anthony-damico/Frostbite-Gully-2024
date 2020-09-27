using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimalStatsSerialization", menuName = "SceneData/AnimalStatsSerialization", order = 2)]
public class AnimalStatsSerialization : ScriptableObject
{
    public List<AnimalStats> animalList = new List<AnimalStats>();
}

[System.Serializable]
public class AnimalStats
{
    public AnimalScript animalPrefab;
    public string builidngUuid;
    public string animalUuid;
    public string animalName;
    public int animalAge;
    public int animalHealth;
}