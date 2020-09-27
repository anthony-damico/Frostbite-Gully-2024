using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimalBuildingSerialization", menuName = "SceneData/AnimalBuildingSerialization", order = 3)]
public class AnimalBuildingSerialization : ScriptableObject
{
    public List<AnimalBuilingProperties> animalBuildingList = new List<AnimalBuilingProperties>();
}

[System.Serializable]
public class AnimalBuilingProperties
{
    public AnimalBuildingScript buildingPrefab;
    public string buildingUuid;
    public string buildingName;
    public int buildingSize;
    public Vector2 buildingPosition;
}